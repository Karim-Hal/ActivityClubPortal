using Microsoft.EntityFrameworkCore;
using SampleOneWebAPI.DTOs.FeedbackPost;
using SampleOneWebAPI.DTOs.Member;
using SampleOneWebAPI.Models;

using SampleOneWebAPI.Repositories.Interfaces;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IRepository<Member> _repository;

        public MemberService(IMemberRepository memberRepository, IRepository<Member> repository)
        {
            _memberRepository = memberRepository;
            _repository = repository;
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            var members = await _repository.GetAll();
            return members;
        }

        public async Task<Member> GetMember(int id)
        {
            var member = await _repository.GetById(id);
            return member;
        }

        public async Task AddMember(Member member)
        {
            await _repository.Add(member);
        }

        public async Task<bool> CheckMemberExists(int id)
        {
            return await _repository.CheckEntityExists(id);
        }

        public async Task EditMember(Member member)
        {

            await _repository.Update(member);
        }

        public async Task DeleteMember(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Member> LoginRequest(LoginRequest loginRequest)
        {
            return await _memberRepository.LoginRequest(loginRequest);
        }
        public async Task<bool> SignUpRequest(Member member)
        {
            return await _memberRepository.SignUpRequest(member);
        }

        public async Task<int> GetMemberId(string memberEmail)
        {
            return await _memberRepository.GetMemberId(memberEmail);
        }
        public async Task AddFeedbackPost(FeedbackPost post)
        {
            await _memberRepository.AddFeedbackPost(post);
        }
        public async Task<List<FeedbackPost>> GetFeedbacks()
        {
            return await _memberRepository.GetFeedbacks();
        }

        public async Task UploadMemberPhoto(ImgUpload photo, string email)
        {
            await _memberRepository.UploadMemberPhoto(photo, email);
        }
        public async Task<byte[]> GetMemberPhoto(int id)
        {
            return await _memberRepository.GetMemberPhoto(id);
        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            return await _memberRepository.IsEmailRegistered(email);
        }
        public async Task<bool> RegisterEvent(int memberId, int eventId)
        {
            return await _memberRepository.RegisterEvent(memberId, eventId);
        }
        public async Task<bool> CheckRegisteredEvent(int memberId, int eventId)
        {
            return await _memberRepository.CheckRegisteredEvent(memberId, eventId);
        }
    }
}
