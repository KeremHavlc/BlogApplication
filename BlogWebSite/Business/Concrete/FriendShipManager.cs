using Business.Abstract;
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

        public (bool success, string message) RejectFriendRequest(Guid senderUserId, Guid receiverUserId)
        {
            throw new NotImplementedException();
        }

        public (bool success, string message) RemoveFriend(Guid senderUserId, Guid receiverUserId)
        {
           var friendShip = _friendShipDal.Get(x => x.SenderId == senderUserId && x.ReceiverId == receiverUserId);
            if (friendShip == null)
            {
                return (false, "Arkadaşlık isteği bulunamadı!");
            }
            _friendShipDal.Delete(friendShip);
            return (true, "Arkadaşlık isteği iptal edildi!");
        }
    }
}
