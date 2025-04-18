using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendShipsController : ControllerBase
    {
        private readonly IFriendShipService _friendShipService;
        public FriendShipsController(IFriendShipService friendShipService)
        {
            _friendShipService = friendShipService;
        }

        [HttpPost("addFriend")]
        public IActionResult AddFriend(Guid receiverUserId)
        {

            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _friendShipService.AddFriend(userId, receiverUserId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpDelete("removeFriend")]
        public IActionResult RemoveFriend(Guid receiverUserId)
        {
            var userIdString = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }
            var result = _friendShipService.RemoveFriend(userId, receiverUserId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpPost("acceptFriend")]
        public IActionResult AcceptFriend(Guid receiverUserId , Guid senderUserId)
        {
            var result = _friendShipService.AcceptFriendRequest(receiverUserId, senderUserId); 
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
    }
}
