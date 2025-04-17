using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] CommentDto commentDto)
        {
            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }

            var result = _commentService.Add(commentDto, userId);

            if (!result.success)
                return BadRequest(result.message);

            return Ok(result.message);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _commentService.Delete(id , userId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpPut("update/{id}")]
        public IActionResult Update(Guid id, [FromBody] CommentDto commentDto)
        {
            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _commentService.Update(id, commentDto,userId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpGet("getbyuserid/{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            var result = _commentService.GetByUserId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No comments found for this user.");
        }
        [HttpGet("getbypostid/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _commentService.GetByPostId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No comments found for this post.");
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _commentService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No comments found.");
        }

    }
}
