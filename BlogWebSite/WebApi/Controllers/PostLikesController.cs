using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikesController : ControllerBase
    {
        private readonly IPostLikeService _postLikeService;
        public PostLikesController(IPostLikeService postLikeService)
        {
            _postLikeService = postLikeService; 
        }

        [HttpPost("addLike")]
        public IActionResult AddLike(Guid postId)
        {
            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _postLikeService.Add(postId, userId);
            if (!result.success)
            {
                return BadRequest(result.message);
            }
            return Ok(result.message);
        }
        [HttpDelete("removeLike")]
        public IActionResult RemoveLike(Guid postId)
        {
            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _postLikeService.Delete(postId, userId);
            if (!result.success)
            {
                return BadRequest(result.message);
            }
            return Ok(result.message);
        }
        [HttpGet("getAllPostLikes")]
        public IActionResult GetPostLikes(Guid postId)
        {
            var userIdString = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _postLikeService.GetAllPostLikeUser(postId);
            if (result == null)
            {
                return NotFound("Post beğenileri bulunamadı.");
            }
            return Ok(result);
        }
        [HttpGet("isLiked")]
        public IActionResult IsPostLikedByCurrentUser(Guid postId)
        {
            var userIdString = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }

            var isLiked = _postLikeService.IsPostLikedByUser(postId, userId);
            return Ok(isLiked); 
        }
    }
}
