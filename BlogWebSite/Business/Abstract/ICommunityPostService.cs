
using Core.Dtos;

namespace Business.Abstract
{
    public interface ICommunityPostService
    {
        (bool success , string message) AddCommunityPost(CommunityPostDto communityPostDto);
        (bool success, string message) UpdateCommunityPost(CommunityPostDto communityPostDto, Guid communityPostId);
        (bool success, string message) DeleteCommunityPost(Guid communityPostId);
        List<CommunityPostDto> GetPostByCommunity(Guid communityId);
        List<CommunityPostDto> GetPostByUser(Guid userId);

    }
}
