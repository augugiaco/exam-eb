using Exam.Application.Dtos;
using Exam.Common.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exam.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(string id);
        PagedResult<UserDto> GetPaged(int page, int pageSize);
        void Create(UserDto userInfo);
        void CreateMassive(List<UserDto> usersToAdd);
        Task UpdateAsync(UserDto userInfo);
        Task DeleteAsync(string id);
    }
}
