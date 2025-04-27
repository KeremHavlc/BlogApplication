using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityCommentsController : ControllerBase
    {
        private readonly ICommunityCommentService communityCommentService;
        public CommunityCommentsController(ICommunityCommentService communityCommentService)
        {
            this.communityCommentService = communityCommentService;
        }
        [HttpPost("addCommunityComment")]
        public IActionResult AddCommunityComment(CommunityCommentDto communityCommentDto)
        {
            var result = communityCommentService.Add(communityCommentDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpDelete("deleteCommunityComment")]
        public IActionResult DeleteCommunityComment(Guid communityCommentId)
        {
            var result = communityCommentService.Delete(communityCommentId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpGet("getByPostId")]
        public IActionResult GetByPostId(Guid communityPostId)
        {
            var result = communityCommentService.GetByPostId(communityPostId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Yorum bulunamadı!");
        }
        [HttpGet("getByUserId")]
        public IActionResult GetByUserId(Guid userId)
        {
            var result = communityCommentService.GetByUserId(userId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Yorum bulunamadı!");
        }
        [HttpPut("updateCommunityComment")]
        public IActionResult UpdateCommunityComment(Guid communityCommentId, CommunityCommentDto communityCommentDto)
        {
            var result = communityCommentService.Update(communityCommentDto, communityCommentId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }

    }
}
