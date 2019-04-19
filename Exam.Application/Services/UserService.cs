using AutoMapper;
using Exam.Application.Dtos;
using Exam.Application.Exceptions;
using Exam.Application.Interfaces;
using Exam.Common.Extensions;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces.UnitOfWork;
using Exam.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Exam.Application.Services
{
    public class UserApplicationService : ServiceBase<User,string>, IUserService
    {

        #region Ctor
        public UserApplicationService(IUserDomainLogic userService,IUnitOfWork uow) : base(userService,uow){}
        #endregion

        #region Methods

        /// <summary>
        /// Insert a massive set of users into db.
        /// </summary>
        /// <param name="usersToAdd"></param>
        public void CreateMassive(List<UserDto> usersToAdd)
        {
            foreach (var user in usersToAdd)
            {
                Create(user);
            }
        }

        /// <summary>
        /// Inserts a new User to the database.
        /// </summary>
        /// <param name="userInfo"></param>
        public void Create(UserDto userInfo)
        {
            var user = new User
            {
                IdValue = Guid.NewGuid().ToString(),
                Name = userInfo.Name,
                Gender = userInfo.Gender,
                UserName = userInfo.UserName,
                BirthDate = userInfo.BirthDate,
                Email = userInfo.Email,
                Uuid = userInfo.Uuid,
                Location = new Location
                {
                    State = userInfo.Location.State,
                    Street = userInfo.Location.Street,
                    City = userInfo.Location.City,
                    PostCode = userInfo.Location.PostCode
                }
            };

            _domainService.Insert(user);

            _uow.Save();

        }   

        /// <summary>
        /// Get a paged result of employees from the db.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedResult<UserDto> GetPaged(int page, int pageSize)
        {
            //limit to pageSize.
            if (pageSize > 50)
                pageSize = 50;

            var pagedResult = _domainService.GetAll(false).OrderBy(u => u.Name).ToPagedResult(page, pageSize, usr => new UserDto
            {
                Email = usr.Email,
                Gender = usr.Gender,        
                UserName = usr.UserName,
                Name = usr.Name,
                BirthDate = usr.BirthDate,
                Uuid = usr.Uuid,
                IdValue = usr.IdValue,
                Location = new LocationDto
                {
                    City = usr.Location.City,
                    State = usr.Location.State,
                    Street = usr.Location.Street,
                    PostCode = usr.Location.PostCode
                }
            });

            //marking the oldest user on the result set.
            pagedResult.Data.MarkOldestUser();

            //pagedResult.Data.MarkMinInList(c => c.BirthDate, d => d.IsOldest);

            return pagedResult;
        }

        /// <summary>
        /// Returns an user by idValue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async Task<UserDto> IUserService.GetByIdAsync(string id)
        {
            var user = await GetByIdAsync(id);
            
            return Mapper.Map<UserDto>(user);
            
        }

        public async Task UpdateAsync(UserDto userInfo)
        {
            var user = await _domainService.GetByIdAsync(userInfo.IdValue);

            user.Gender = userInfo.Gender;
            user.BirthDate = userInfo.BirthDate;
            user.Email = userInfo.Email;
            user.Location.City = userInfo.Location.City;
            user.Location.PostCode = userInfo.Location.PostCode;
            user.Location.Street = userInfo.Location.Street;
            user.Location.Street = userInfo.Location.State;
            user.Name = userInfo.Name;
            user.Uuid = userInfo.Uuid;

            _domainService.Update(user);

            _uow.Save();
        }

        public async Task DeleteAsync(string id)
        {
            await _domainService.DeleteAsync(id);

            _uow.Save();
        }

        #endregion

    }

    public static class UserCollectionExtensions
    {
        public static void MarkOldestUser(this List<UserDto> users)
        {

            var maxAge = DateTime.Today;
            string userId = null;

            users.ForEach(usr =>
            {
                if (usr.BirthDate <= maxAge)
                {
                    maxAge = usr.BirthDate;
                    userId = usr.Uuid;
                }

            });

            //robust checking 
            if (userId != null)
                //set the property for the oldest user
                users.FirstOrDefault(u => u.Uuid == userId).IsOldest = true;
                          
        }

        public static void MarkMinInList<T, F, C>(this List<T> list, Func<T, F> fieldToCompare, Expression<Func<T, C>> fieldToMark) where F : IComparable
        {

            if (list.Any() == false)
                return;

            T maxValue = list.First();
            int idxMax = list.IndexOf(maxValue);

            var expressionToMark = (MemberExpression)fieldToMark.Body;
            string namePropertyToMark = expressionToMark.Member.Name;

            list.ForEach(usr =>
            {
                if (fieldToCompare(usr).CompareTo(fieldToCompare(maxValue)) < 0)
                {
                    maxValue = usr;
                    idxMax = list.IndexOf(usr);
                }

            });

            list[idxMax].GetType().GetProperty(namePropertyToMark).SetValue(list[idxMax], true);
        }
    }
}
