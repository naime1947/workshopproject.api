using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
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

        public async Task<IActionResult> Login(UserToLoginDto user)
        {
            var userToLogin = await _userManager.FindByNameAsync(user.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(userToLogin, user.Password, false);
            if(result.Succeeded){
                var appUser = await _userManager.Users.FirstOrDefaultAsync(user=> user.NormalizedUserName == userToLogin.UserName.ToUpper());
                var userToReturn = _mapper.Map<UserToReturn>(appUser);
                return Ok(new {
                    token="",
                    user=userToReturn
                });
            }

            return Unauthorized();            
        }
    }
}