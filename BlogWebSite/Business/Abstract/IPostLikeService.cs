using Core.Dtos;

namespace Business.Abstract
{
    public interface IPostLikeService
    {
        (bool success, string message) Add(Guid postId, Guid userId);
        (bool success, string message) Delete(Guid postId, Guid userId);
        List<UserDto> GetAllPostLikeUser(Guid postId);
        public bool IsPostLikedByUser(Guid postId, Guid userId);
    }
}
