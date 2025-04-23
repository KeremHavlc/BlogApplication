using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody] PostDto postDto,Guid userId)
        {            
            var result = _postService.Add(postDto, userId);

            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _postService.Delete(id, userId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpPut("update/{id}")]
        public IActionResult Update(Guid id, [FromBody] PostDto postDto)
        {
            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _postService.Update(id, postDto , userId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpGet("getbyuserid/{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            var result = _postService.GetByUserId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No posts found for this user.");
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _postService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No posts found.");
        }
        [HttpGet("getBypostId")]
        public IActionResult GetById(Guid postId)
        {
            var post = _postService.GetByPostId(postId);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

    }
}
