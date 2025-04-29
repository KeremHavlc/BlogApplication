using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunitiesController : ControllerBase
    {
        private readonly ICommunityService _communityService;
        public CommunitiesController(ICommunityService communityService)
        {
            _communityService = communityService;
        }
        [HttpPost("addCommunity")]
        public IActionResult AddCommunity(CommunityDto communityDto)
        {
            var roleIdClaim = User.FindFirst("roleId")?.Value;
            if (roleIdClaim != "3fa85f64-5717-4562-b3fc-2c963f66afa5")
            {
                return Forbid();
            }
            var result = _communityService.Add(communityDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            else
            {
                return BadRequest(result.message);
            }
        }
        [HttpPut("updateCommunity")]

        public IActionResult UpdateCommunity(CommunityDto communityDto, Guid communityId)
        {
            var roleIdClaim = User.FindFirst("roleId")?.Value;
            if (roleIdClaim != "3fa85f64-5717-4562-b3fc-2c963f66afa5")
            {
                return Forbid(); // Admin değilse 403
            }
            var result = _communityService.Update(communityDto, communityId);
            if (result.success)
            {
                return Ok(result.message);
            }
            else
            {
                return BadRequest(result.message);
            }
        }
        [HttpDelete("deleteCommunity")]

        public IActionResult DeleteCommunity(Guid id)
        {
            var roleIdClaim = User.FindFirst("roleId")?.Value;
            if (roleIdClaim != "3fa85f64-5717-4562-b3fc-2c963f66afa5")
            {
                return Forbid(); // Admin değilse 403
            }
            var result = _communityService.Delete(id);
            if (result.success)
            {
                return Ok(result.message);
            }
            else
            {
                return BadRequest(result.message);
            }
        }
        [HttpGet("getCommunityById")]
        public IActionResult GetCommunityById(Guid id)
        {
            var result = _communityService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Topluluk bulunamadı!");
            }
        }
        [HttpGet("getAllCommunities")]
        public IActionResult GetAllCommunities()
        {
            var result = _communityService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Topluluk bulunamadı!");
            }
        }

    }
}
