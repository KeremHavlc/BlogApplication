using Core.Dtos;


namespace Business.Abstract
{
    public interface IFriendShipService
    {
        (bool success, string message) AddFriend(Guid senderUserId, Guid receiverUserId);
        (bool success, string message) RemoveFriend(Guid senderUserId, Guid receiverUserId);
        (bool success, string message) AcceptFriendRequest(Guid senderUserId, Guid receiverUserId);
        List<UserDto> GetFriends(Guid userId);
        List<FriendShipsStatusDto> GetPendingRequestSenders(Guid userId);
        FriendShipsStatusDto Check(Guid senderUserId, Guid receiverUserId);
    }
}

