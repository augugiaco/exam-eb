using Exam.Domain.Base.Services;
using Exam.Domain.Entities;
using Exam.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Domain.Services
{
    public class UserDomainLogic:DomainBaseLogic<User,string>, IUserDomainLogic
    {
        public UserDomainLogic(IRepository<User,string> repository):base(repository) {}

        public override async Task<User> GetByIdAsync(string id)
        {
            return await _repository.GetAll(true).Where(u => u.IdValue == id)
                .Include(usr => usr.Location)
                .FirstOrDefaultAsync();
        }

    }
}
