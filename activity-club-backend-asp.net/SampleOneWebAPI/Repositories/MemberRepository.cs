using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SampleOneWebAPI.Data;
using SampleOneWebAPI.DTOs.FeedbackPost;
using SampleOneWebAPI.DTOs.Member;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories.Interfaces;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace SampleOneWebAPI.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ActivityPortalDbContext _context;
        public MemberRepository(ActivityPortalDbContext context)
        {
            _context = context;
        }

        public async Task<Member?> LoginRequest(LoginRequest loginRequest)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Email == loginRequest.Email && m.Password == loginRequest.Password);

        }

        public async Task<bool> SignUpRequest(Member member)
        {
            var emailInUse = await _context.Members.AnyAsync(m => m.Email == member.Email);
            if (!emailInUse)
            {

                await _context.Members.AddAsync(member);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<int> GetMemberId(string memberEmail)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Email == memberEmail);

            return member.Id;
        }

        public async Task AddFeedbackPost(FeedbackPost post)
        {
            await _context.FeedbackPosts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FeedbackPost>> GetFeedbacks()
        {
            return await _context.FeedbackPosts.ToListAsync();
        }
        public async Task UploadMemberPhoto(ImgUpload photo, string email)
        {
            var member = await _context.Members.Where(m => m.Email == email).FirstOrDefaultAsync();
            if (member is not null)
            {
                var memberId = member.Id;
                var memberPhoto = new MemberPhoto
                {
                    MemberId = memberId,
                    Photo = photo.Photo
                };
                await _context.MemberPhotos.AddAsync(memberPhoto);
                await _context.SaveChangesAsync();
            }



        }
        public async Task<byte[]> GetMemberPhoto(int id)
        {
            var photo = await _context.MemberPhotos.Where(mp => mp.MemberId == id)
                             .Select(mp => mp.Photo)
                             .FirstOrDefaultAsync();
            return photo;

        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            return await _context.Members.AnyAsync(m => m.Email == email);


        }

        public async Task<bool> RegisterEvent(int memberId, int eventId)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == memberId);
            if (member is not null)
            {
                var eventMember = new EventMember
                {
                    MemberId = memberId,
                    EventId = eventId,

                };
                member.EventMembers.Add(eventMember);
                await  _context.SaveChangesAsync();
                return true;

            }
            return false;

        }
        public async Task<bool> CheckRegisteredEvent(int memberId, int eventId)
        {
            return await _context.EventMembers.FirstOrDefaultAsync(em => em.MemberId == memberId && em.EventId == eventId) is not null;
        }

    }
}
