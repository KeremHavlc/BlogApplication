using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CommunityUserManager : ICommunityUserService
    {
        private readonly ICommunityUserDal _communityUserDal;
        public CommunityUserManager(ICommunityUserDal communityUserDal)
        {
            _communityUserDal = communityUserDal;
        }

        public (bool success, string message) AddCommunityUser(CommunityUserDto communityUserDto)
        {
            if(communityUserDto.CommunityId == Guid.Empty)
            {
                return (false, "Toplululk boş olamaz!");
            }
            if (communityUserDto.UserId == Guid.Empty)
            {
                return (false, "Kullanıcı boş olamaz!");
            }

            var communityUser = new CommunityUser
            {
                CommunityId = communityUserDto.CommunityId,
                UserId = communityUserDto.UserId,
                CreatedAt = DateTime.Now
            };
            _communityUserDal.Add(communityUser);
            return (true, "Topluluk kullanıcısı başarıyla eklendi.");
        }

        public (bool success, string message) DeleteCommunityUser(CommunityUserDto communityUserDto)
        {
            var communityUser = _communityUserDal.Get(x => x.CommunityId == communityUserDto.CommunityId && x.UserId == communityUserDto.UserId);
            if (communityUser == null)
            {
                return (false, "Topluluk kullanıcısı bulunamadı.");
            }
            _communityUserDal.Delete(communityUser);
            return (true, "Topluluk kullanıcısı başarıyla silindi.");
        }

        public int GetCommunityUserCount(Guid communityId)
        {
            var communityUserCount = _communityUserDal.GetAll(x => x.CommunityId == communityId).Count();
            if (communityUserCount == 0)
            {
                return 0;
            }
            return communityUserCount;
        }

        public List<CommunityUserDto> GetCommunityUsersByCommunityId(Guid communityId)
        {
            var community = _communityUserDal.Get(x => x.CommunityId == communityId);
            if (community == null)
            {
                return null;
            }
            var listCommunityUser = _communityUserDal.GetAll(x => x.CommunityId == communityId);
            var communityUserList = listCommunityUser.Select(communityUser => new CommunityUserDto
            {
                UserId = communityUser.UserId,
                CreatedAt = communityUser.CreatedAt
            }).ToList();
            return communityUserList;
        }
    }
}
