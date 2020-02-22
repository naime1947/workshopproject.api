using AutoMapper;
using workshopproject.API.Dtos;
using workshopproject.API.Models;

namespace workshopproject.API.Helpers
{
    public class AutoMapperHelperProfiles:Profile
    {
        public AutoMapperHelperProfiles()
        {
            CreateMap<UserToRegisterDto,User>();
            CreateMap<UserToLoginDto, User>();
            CreateMap<User, UserToReturn>();
        }
    }
}