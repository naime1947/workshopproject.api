using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using workshopproject.API.Dtos;

namespace workshopproject.API.Controllers
{
    [Route("api/[Contrroller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        
        public AuthController()
        {

        }

        public async Task<IActionResult> Register(UserToRegisterDto user)
        {
            return null;    
        }
    }
}