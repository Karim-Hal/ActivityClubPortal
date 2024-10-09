using AutoMapper;
using SampleOneWebAPI.DTOs.Guide;
using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Profiles
{
    public class GuideProfile: Profile
    {
        public GuideProfile() { 

            CreateMap<Guide, GuideDTO>();
            CreateMap<GuideDTO, Guide>();
        }
    }
}
