using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityPostsController : ControllerBase
    {
        private readonly ICommunityPostService _communityPostService;
        public CommunityPostsController(ICommunityPostService communityPostService)
        {
            _communityPostService = communityPostService;
        }
        [HttpPost("addCommunityPost")]
        public IActionResult AddCommunityPost([FromBody] CommunityPostDto communityPostDto)
        {
            var result = _communityPostService.AddCommunityPost(communityPostDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpPut("updateCommunityPost/{communityPostId}")]
        public IActionResult UpdateCommunityPost(CommunityPostDto communityPostDto, Guid communityPostId)
        {
            var result = _communityPostService.UpdateCommunityPost(communityPostDto, communityPostId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpDelete("deleteCommunityPost/{communityPostId}")]
        public IActionResult DeleteCommunityPost(Guid communityPostId)
        {
            var result = _communityPostService.DeleteCommunityPost(communityPostId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpGet("getPostByCommunity")]
        public IActionResult GetPostByCommunity(Guid communityId)
        {
            var result = _communityPostService.GetPostByCommunity(communityId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Bu toplulukta gönderi bulunmamaktadır!");
        }
        [HttpGet("getPostByUser")]
        public IActionResult GetPostByUser(Guid userId)
        {
            var result = _communityPostService.GetPostByUser(userId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Bu kullanıcıya ait gönderi bulunmamaktadır!");
        }
    }
}
