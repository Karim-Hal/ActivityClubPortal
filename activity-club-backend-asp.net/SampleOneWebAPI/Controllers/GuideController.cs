using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleOneWebAPI.DTOs.Guide;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuideController: ControllerBase
    {
        private readonly IGuideService _guideService;
        private readonly IMapper _mapper;
        private readonly ILogger<GuideController> _logger;
        public GuideController(IGuideService guideService, IMapper mapper, ILogger<GuideController> logger)
        {
            _guideService = guideService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("GetGuides")]
        public async Task<ActionResult<IEnumerable<Guide>>> GetGuides()
        {
            try
            {
                var guides = await _guideService.GetGuides();
                var guideDTOs = _mapper.Map<List<GuideDTO>>(guides);
                foreach(var guide in guideDTOs)
                {
                    guide.PhotoBase64 = Convert.ToBase64String(guide.Photo);
                }
                
             
                return Ok(guideDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddGuide")]
        public async Task<ActionResult> AddGuide(GuideDTO guideDTO)
        {
            try

            {
               var guide = _mapper.Map<Guide>(guideDTO);
                guide.Photo = Convert.FromBase64String(guideDTO.PhotoBase64);
                if (guide.Id == 0)
                {
                    await _guideService.AddGuide(guide);

                }
                else
                {
                    var checkIfExists = await _guideService.CheckGuideExists(guide.Id);
                    if (checkIfExists)
                    {
                        await _guideService.EditGuide(guide);
                    }
                    else
                    {
                        return NotFound("Guide not found");
                    }
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGuide/{id}")]
        public async Task<ActionResult<Guide>> GetGuide(int id)
        {
            try
            {
                if (await _guideService.CheckGuideExists(id) is false)
                {
                    return NotFound($"Guide with id {id} not found!");
                }
                var guide = await _guideService.GetGuide(id);
                return Ok(guide);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the guide.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteGuide/{id}")]
        public async Task<ActionResult> DeleteGuide(int id)
        {
            try
            {
                if (await _guideService.CheckGuideExists(id))
                {
                    await _guideService.DeleteGuide(id);
                    return Ok("Guide Deleted Successfully!");
                }
                else
                {
                    return NotFound($"Guide with id {id} does not exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
