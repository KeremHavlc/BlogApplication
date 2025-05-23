﻿using Business.Abstract;
using Core.Dtos;
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
        public IActionResult AddFriend(Guid receiverUserId,Guid senderUserId)
        {
            var result = _friendShipService.AddFriend(senderUserId, receiverUserId);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpDelete("removeFriend")]
        public IActionResult RemoveFriend(Guid receiverUserId , Guid senderUserId)
        {            
            var result = _friendShipService.RemoveFriend(receiverUserId, senderUserId);
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

        [HttpGet("getFriends")]
        public IActionResult GetFriends(Guid userId)
        {          
            var result = _friendShipService.GetFriends(userId);
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound("Arkadaş bulunamadı.");
        }
        [HttpGet("getPendingFriends")]
        public IActionResult GetPendingFriends(Guid userId)
        {
            var result = _friendShipService.GetPendingRequestSenders(userId);
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound("Arkadaş bulunamadı.");
        }
        [HttpGet("check")]
        public ActionResult<FriendShipsStatusDto> Check(Guid senderUserId, Guid receiverUserId)
        {
            var result = _friendShipService.Check(senderUserId, receiverUserId);
            return Ok(result);
        }
    }
}
