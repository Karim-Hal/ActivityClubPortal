using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Microsoft.EntityFrameworkCore;

using SampleOneWebAPI.Data;
using SampleOneWebAPI.DTOs.FeedbackPost;
using SampleOneWebAPI.DTOs.Member;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories;
using SampleOneWebAPI.Services;
using SampleOneWebAPI.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SampleOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {

        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;
        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        [HttpGet("GetMembers")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            try
            {
                var members = await _memberService.GetMembers();
                return Ok(members);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddMember")]
        public async Task<ActionResult> AddMember(Member member)
        {
            try
            {
                if (member.Id == 0)
                {
                    await _memberService.AddMember(member);

                }
                else
                {
                    var checkIfExists = await _memberService.CheckMemberExists(member.Id);
                    if (checkIfExists)
                    {
                        await _memberService.EditMember(member);
                    }
                    else
                    {
                        return Ok("Member not found");
                    }
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMember/{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            try
            {
                var member = await _memberService.GetMember(id);

                if (await _memberService.CheckMemberExists(id) is false)
                {
                    return NotFound($"Member with id {id} not found!");
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteMember(int id)
        {
            try
            {
                if (await _memberService.CheckMemberExists(id))
                {
                    await _memberService.DeleteMember(id);

                    return Ok("Member Deleted Successfully!");
                }
                else
                {
                    return BadRequest($"Member with id {id} does not exist");
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<SignupRequest?>> processLogin(LoginRequest loginRequest)
        {
            var emailRegistered = await _memberService.IsEmailRegistered(loginRequest.Email);
            if (emailRegistered)
            {
                var member = await _memberService.LoginRequest(loginRequest);
                var memberDTO = _mapper.Map<SignupRequest>(member);
                return Ok(memberDTO);

            }
            else
            {
                return Ok(false);
            }
   
        }

        [HttpPost("Sign Up")]
        public async Task<ActionResult<bool>> SignUpRequest([FromBody] CombinedMemberPhoto signupRequest)
        {
            if (ModelState.IsValid)
            {
               
                var signupInfo = signupRequest.SignupRequest;
                var photo = signupRequest.Photo;
                var memberFromDTO = _mapper.Map<Member>(signupInfo);
                byte[] photoBytes;


                var base64String = signupRequest.Photo.Split(',')[1]; // Remove the data URL scheme
                photoBytes = Convert.FromBase64String(base64String);
                // Store or process the photoBytes as needed
                ImgUpload imgUpload = new ImgUpload
                {
                    Photo = photoBytes,
                };


                var signedUp = await _memberService.SignUpRequest(memberFromDTO);
                if (signedUp)
                {
                    await _memberService.UploadMemberPhoto(imgUpload, memberFromDTO.Email);
                }
                return signedUp;

            }
            return false;


        }

        [HttpPost("Submit Feedback")]
        public async Task<ActionResult<bool>> SubmitFeedback([FromBody] FeedbackRequest request)
        {
            var memberEmail = request.Email;
            var memberDescription = request.MemberPost;
            if (memberEmail is not null && memberDescription is not null)
            {
                var memberId = await _memberService.GetMemberId(memberEmail);
                var fdPost = new FeedbackPost
                {
                    Description = memberDescription,

                    MemberId = memberId,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),


                };
                await _memberService.AddFeedbackPost(fdPost);
                return Ok("Added feedback!");
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpGet("GetFeedbacks")]
        public async Task<ActionResult<List<FeedbackPostDTO>>> GetFeedbacks()
        {
            var feedbacks = await _memberService.GetFeedbacks();
            List<FeedbackPostDTO> feedbackDTOs = new List<FeedbackPostDTO>();
            foreach (var feedback in feedbacks)
            {
                var member = await _memberService.GetMember(feedback.MemberId);
                var memberPhoto = await _memberService.GetMemberPhoto(feedback.MemberId);


                var feedbackDTO = new FeedbackPostDTO
                {
                    MemberName = member.FullName,
                    Description = feedback.Description,
                    CreatedDate = feedback.CreatedDate,
                    Photo = Convert.ToBase64String(memberPhoto)
                    
                };
                feedbackDTOs.Add(feedbackDTO);
            }
            return Ok(feedbackDTOs);


        }
        [HttpGet("GetImage/{id}")]
        public async Task<ActionResult> GetImage(int id)
        {
            var imageBytes =  await _memberService.GetMemberPhoto(id);

            if (imageBytes == null)
            {
                return NotFound();
            }

            var base64String = Convert.ToBase64String(imageBytes);
            return Ok(base64String);
        }
    }
}
