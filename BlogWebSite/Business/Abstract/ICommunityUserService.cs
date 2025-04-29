using Core.Dtos;

namespace Business.Abstract
{
    public interface ICommunityUserService
    {
        (bool success , string message) AddCommunityUser(CommunityUserDto communityUserDto);
        (bool success, string message) DeleteCommunityUser(CommunityUserDto communityUserDto);
        int GetCommunityUserCount(Guid communityId);
        List<CommunityUserDto> GetCommunityUsersByCommunityId(Guid communityId);
        Dictionary<Guid, int> GetAllCommunityUserCount();
        CommunityUsersCheckDto Check(Guid communityId, Guid joinUserId);

    }
}
