using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CommunityUserManager : ICommunityUserService
    {
        private readonly ICommunityUserDal _communityUserDal;
        private readonly ICommunityDal _communityDal;
        public CommunityUserManager(ICommunityUserDal communityUserDal, ICommunityDal communityDal)
        {
            _communityUserDal = communityUserDal;
            _communityDal = communityDal;
        }

        public (bool success, string message) AddCommunityUser(CommunityUserDto communityUserDto)
        {
            if (communityUserDto.CommunityId == Guid.Empty)
            {
                return (false, "Topluluk boş olamaz!");
            }
            if (communityUserDto.UserId == Guid.Empty)
            {
                return (false, "Kullanıcı boş olamaz!");
            }
            var existingCommunityUser = _communityUserDal.Get(x => x.CommunityId == communityUserDto.CommunityId && x.UserId == communityUserDto.UserId);
            if (existingCommunityUser != null)
            {
                return (false, "Bu kullanıcı zaten bu topluluğa eklenmiş.");
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

        public CommunityUsersCheckDto Check(Guid communityId, Guid joinUserId)
        {
            var communityUser = _communityUserDal.Get(x => x.CommunityId == communityId && x.UserId == joinUserId);
            if (communityUser == null)
            {
                return new CommunityUsersCheckDto
                {
                    Status = 0,
                    Message = "Kullanıcı toplulukta değil.",
                    UserId = joinUserId,
                };
            }
            return new CommunityUsersCheckDto
            {
                Status = 1,
                Message = "Kullanıcı toplulukta.",
                UserId = joinUserId,
            };
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

        public Dictionary<Guid, int> GetAllCommunityUserCount()
        {
           
            var communityUserCount = new Dictionary<Guid, int>();

            var communityUsers = _communityUserDal.GetAll(); 
            var groupedByCommunity = communityUsers.GroupBy(x => x.CommunityId); 

            foreach (var group in groupedByCommunity)
            {
                communityUserCount[group.Key] = group.Count(); 
            }

            return communityUserCount;
        }

        public List<CommunityDto> GetCommunitiesByUserId(Guid userId)
        {
            var communityUsers = _communityUserDal.GetAll(x => x.UserId == userId);

            var communityDtos = communityUsers
                .Select(cu =>
                {
                    var community = _communityDal.Get(c => c.Id == cu.CommunityId);
                    if (community == null) return null;

                    return new CommunityDto
                    {
                        CommunityId = community.Id,
                        Image = community.Image,
                        Name = community.Name,
                        Description = community.Description
                    };
                })
                .Where(dto => dto != null)
                .ToList();

            return communityDtos;
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
