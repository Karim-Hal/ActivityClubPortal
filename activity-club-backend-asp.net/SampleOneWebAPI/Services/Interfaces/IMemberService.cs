using SampleOneWebAPI.DTOs.Member;
using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetMember(int id);
        Task AddMember(Member member);
        Task<bool> CheckMemberExists(int id);
        Task EditMember(Member member);
        Task DeleteMember(int id);
        Task<Member> LoginRequest(LoginRequest loginRequest);
        Task<bool> SignUpRequest(Member membe);
        Task<int> GetMemberId(string email);
        Task AddFeedbackPost(FeedbackPost post);
        Task<List<FeedbackPost>> GetFeedbacks();
        Task UploadMemberPhoto(ImgUpload photo, string email);
        Task<byte[]> GetMemberPhoto(int id);
        Task<bool> IsEmailRegistered(string email);
        Task<bool> RegisterEvent(int memberId, int eventId);
        Task<bool> CheckRegisteredEvent(int memberId, int eventId);
    }
}
