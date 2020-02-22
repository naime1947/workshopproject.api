using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _config;
        

        public AuthController(UserManager<User> userManager, 
        IConfiguration config,
        SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserToRegisterDto user)
        {
            User userToCreate = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(userToCreate, user.Password);
            var userToReturn = _mapper.Map<UserToReturn>(userToCreate);

            if(result.Succeeded){
                return CreatedAtRoute("GetUser", new {
                    Controller = "Users", id=userToCreate.Id}, userToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserToLoginDto user)
        {
            var userToLogin = await _userManager.FindByNameAsync(user.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(userToLogin, user.Password, false);
            if(result.Succeeded){
                var appUser = await _userManager.Users.FirstOrDefaultAsync(user=> user.NormalizedUserName == userToLogin.UserName.ToUpper());
                var userToReturn = _mapper.Map<UserToReturn>(appUser);
                return Ok(new {
                    token=GenerateToken(appUser).Result,
                    user=userToReturn
                });
            }

            return Unauthorized();            
        }

        private async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}