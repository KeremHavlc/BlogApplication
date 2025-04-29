using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityUsersController : ControllerBase
    {
        private readonly ICommunityUserService _communityUserService;
        public CommunityUsersController(ICommunityUserService communityUserService)
        {
            _communityUserService = communityUserService;
        }
        [HttpPost("addCommunityUser")]
        public IActionResult AddCommunityUser(CommunityUserDto communityUserDto)
        {
            var result = _communityUserService.AddCommunityUser(communityUserDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpDelete("deleteCommunityUser")]
        public IActionResult DeleteCommunityUser(CommunityUserDto communityUserDto)
        {
            var result = _communityUserService.DeleteCommunityUser(communityUserDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpGet("getCommunityUserCount")]
        public IActionResult GetCommunityUserCount(Guid communityId)
        {
            var result = _communityUserService.GetCommunityUserCount(communityId);
            if (result == 0)
            {
                return NotFound("Topluluk kullanıcısı bulunamadı.");
            }
            return Ok(result);
        }
        [HttpGet("getCommunityUsersByCommunityId")]
        public IActionResult GetCommunityUsersByCommunityId(Guid communityId)
        {
            var result = _communityUserService.GetCommunityUsersByCommunityId(communityId);
            if (result == null || result.Count == 0)
            {
                return NotFound("Topluluk kullanıcısı bulunamadı.");
            }
            return Ok(result);
        }
        [HttpGet("getAllCommunityUsersCount")]
        public IActionResult GetAllCommunityUsersCount()
        {
            var result = _communityUserService.GetAllCommunityUserCount();
            return Ok(result);
        }
        [HttpGet("check")]  
        public IActionResult Check(Guid communityId, Guid joinUserId)
        {
            var result = _communityUserService.Check(communityId, joinUserId);
            return Ok(result);
        }
    }
}
