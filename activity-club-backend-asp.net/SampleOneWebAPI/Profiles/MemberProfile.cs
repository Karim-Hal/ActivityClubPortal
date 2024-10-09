using AutoMapper;
using SampleOneWebAPI.DTOs.Member;
using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Profiles
{
    public class MemberProfile: Profile
    {
        public MemberProfile()
        {
            CreateMap<SignupRequest, Member>();
            CreateMap<Member, SignupRequest>();
        }

    }
}
