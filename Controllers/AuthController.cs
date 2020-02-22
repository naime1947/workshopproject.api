using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using workshopproject.API.Data;
using workshopproject.API.Dtos;
using workshopproject.API.Helpers;
using workshopproject.API.Models;

namespace workshopproject.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        

        public AuthController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserToRegisterDto user)
        {
            User userToCreate = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(userToCreate, user.Password);

            if(result.Succeeded){
                return StatusCode(201);
            }

            return BadRequest(result.Errors);
        }
    }
}