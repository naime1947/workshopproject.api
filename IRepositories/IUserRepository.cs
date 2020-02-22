using System.Threading.Tasks;
using workshopproject.API.Models;

namespace workshopproject.API.IRepositories
{
    public interface IUserRepository
    {
        Task<User>  GetUser (int id);
    }
}