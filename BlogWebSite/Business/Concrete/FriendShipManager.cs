﻿using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class FriendShipManager : IFriendShipService
    {
        private readonly IFriendShipDal _friendShipDal;
        private readonly IUserDal _userDal;
        public FriendShipManager(IFriendShipDal friendShipDal , IUserDal userDal)
        {
            _friendShipDal = friendShipDal;
            _userDal = userDal;
        }
        public (bool success, string message) AcceptFriendRequest(Guid receiverUserId, Guid senderUserId)
        {
            var friendShip = _friendShipDal.Get(x =>
                x.SenderId == senderUserId &&
                x.ReceiverId == receiverUserId &&
                x.Status == false);     

            if (friendShip == null)
            {
                return (false, "Böyle bir arkadaşlık isteği bulunamadı!");
            }

            friendShip.Status = true;
            friendShip.UpdatedAt = DateTime.UtcNow;

            _friendShipDal.Update(friendShip);
            return (true, "Arkadaşlık isteği kabul edildi!");
        }


        public (bool success, string message) AddFriend(Guid senderUserId, Guid receiverUserId)
        {
            var senderUser = _userDal.Get(x => x.Id == senderUserId);
            if (senderUser == null)
            {
                return (false, "İstek atan Kullanıcı Bulunamadı!");
            }
            var receiverUser = _userDal.Get(x => x.Id == receiverUserId);
            if (receiverUser == null)
            {
                return (false, "İstek atılan Kullanıcı Bulunamadı!");
            }
            if (senderUserId == receiverUserId)
            {
                return (false, "Kendi kendinize istek atamazsınız!");
            }
            var existingFriendShip = _friendShipDal.Get(x =>
                 (x.SenderId == senderUserId && x.ReceiverId == receiverUserId) ||
                 (x.SenderId == receiverUserId && x.ReceiverId == senderUserId));
            if (existingFriendShip != null)
            {
                return (false, "Zaten bir arkadaşlık isteği mevcut!");
            }
            FriendShip friendShip = new FriendShip
            {
                Id = Guid.NewGuid(),
                SenderId = senderUserId,
                ReceiverId = receiverUserId,
                Status = false
            };
            _friendShipDal.Add(friendShip);
            return (true, "Arkadaşlık isteği gönderildi!");
        }

        public List<UserDto> GetFriends(Guid userId)
        {            
            var friendships = _friendShipDal.GetAll(x =>
                (x.SenderId == userId || x.ReceiverId == userId) &&
                x.Status == true);
         
            var friendIds = friendships
                .Select(x => x.SenderId == userId ? x.ReceiverId : x.SenderId)
                .ToList();
            //x.SenderId== userId ? true dönerse => userId isteği gönderen kişidir ve receiverId dönderilir.
            //x.ReceiverId== userId ? true dönerse => userId isteği alan kişidir ve senderId dönderilir.

            var users = _userDal.GetAll(x => friendIds.Contains(x.Id));

            
            var userDtos = users.Select(user => new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                UserId = user.Id
            }).ToList();

            return userDtos;
        }
        public (bool success, string message) RemoveFriend(Guid senderUserId, Guid receiverUserId)
        {
            var friendShip = _friendShipDal.Get(x =>
                (x.SenderId == senderUserId && x.ReceiverId == receiverUserId) ||
                (x.SenderId == receiverUserId && x.ReceiverId == senderUserId));

            if (friendShip == null)
            {
                return (false, "Böyle bir arkadaşlık ilişkisi bulunamadı!");
            }

            _friendShipDal.Delete(friendShip);
            return (true, "Arkadaşlık ilişkisi silindi!");
        }
        public FriendShipsStatusDto Check(Guid senderUserId, Guid receiverUserId)
        {
            var friendShip = _friendShipDal.Get(f =>
                (f.SenderId == senderUserId && f.ReceiverId == receiverUserId)
                || (f.SenderId == receiverUserId && f.ReceiverId == senderUserId));

            if (friendShip == null)
                return new FriendShipsStatusDto { Status = 0, Message = "Arkadaşlık ilişkisi yok" };

            if (friendShip.Status == false)
                return new FriendShipsStatusDto { Status = 1, Message = "Arkadaşlık isteği bekleniyor" };

            return new FriendShipsStatusDto { Status = 2, Message = "Arkadaşsınız" };
        }

        public List<FriendShipsStatusDto> GetPendingRequestSenders(Guid userId)
        {
            // Kullanıcıya gelen TÜM BEKLEYEN istekleri al (Status = false)
            var pendingRequests = _friendShipDal.GetAll(x =>
                x.ReceiverId == userId &&
                x.Status == false
            );

            // Her istek için gönderenin detaylarını DTO'ya ekle
            var result = new List<FriendShipsStatusDto>();

            foreach (var request in pendingRequests)
            {
                var senderUser = _userDal.Get(x => x.Id == request.SenderId);

                if (senderUser != null)
                {
                    result.Add(new FriendShipsStatusDto
                    {
                        Status = 1, // Bekleyen istek
                        Message = "Arkadaşlık isteği bekleniyor",
                        UserId = senderUser.Id,
                        Username = senderUser.Username,
                        Email = senderUser.Email
                    });
                }
            }

            return result;
        }

    }
}
