using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using workshopproject.API.Data;
using workshopproject.API.IRepositories;
using workshopproject.API.Models;

namespace workshopproject.API.Repositories
{
    public class UserRepo : IUserRepository
    {
        private readonly IdentityDataContext _dataContext;
        public UserRepo(IdentityDataContext datacontext )
        {
            _dataContext = datacontext;
        }

        public async Task<User> GetUser (int id){
            return await _dataContext.Users.FirstOrDefaultAsync(x=> x.Id == id);
        }
    }
}