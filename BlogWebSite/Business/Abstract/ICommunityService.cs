using Core.Dtos;

namespace Business.Abstract
{
    public interface ICommunityService
    {
        (bool success, string message) Add(CommunityDto communityDto);
        (bool success, string message) Update(CommunityDto communityDto , Guid communityId);
        (bool success, string message) Delete(Guid id);
        CommunityDto GetById(Guid id);
        List<CommunityDto> GetAll();

    }
}
