
using SampleOneWebAPI.DTOs.Member;
using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member> LoginRequest(LoginRequest loginRequest);
        Task<bool> SignUpRequest(Member member);
        Task<int> GetMemberId(string memberEmail);
        Task AddFeedbackPost(FeedbackPost post);
        Task<List<FeedbackPost>> GetFeedbacks();
        Task UploadMemberPhoto(ImgUpload photo, string email);
        Task<byte[]> GetMemberPhoto(int id);
        Task<bool> IsEmailRegistered(string email);
        Task<bool> RegisterEvent(int memberId, int eventId);

        Task<bool> CheckRegisteredEvent(int memberId, int eventId);
    }
}
