using Core.Dtos;

namespace Business.Abstract
{
    public interface ICommunityCommentService
    {
        (bool success, string message) Add(CommunityCommentDto communityCommentDto);
        (bool success, string message) Update(CommunityCommentDto communityCommentDto , Guid communityCommendId);
        (bool success, string message) Delete(Guid communityCommendId);
        List<CommunityCommentDto> GetByPostId(Guid communityPostId);
        List<CommunityCommentDto> GetByUserId(Guid userId);
    }
}
