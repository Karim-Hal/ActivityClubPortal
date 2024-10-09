using AutoMapper;
using SampleOneWebAPI.DTOs.UserDTOs;
using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Profiles
{
    public class UserProfile: Profile
    {

        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            
        }
    }
}
