using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CommunityManager : ICommunityService
    {
        private readonly ICommunityDal _communityDal;
        public CommunityManager(ICommunityDal communityDal)
        {
            _communityDal = communityDal;
        }
        public (bool success, string message) Add(CommunityDto communityDto)
        {
            if (string.IsNullOrEmpty(communityDto.Name))
            {
                return (false, "Topluluk adı boş olamaz!");
            }
            if (string.IsNullOrEmpty(communityDto.Description))
            {
                return (false, "Topluluk açıklaması boş olamaz!");
            }
            if (communityDto.Image == null || communityDto.Image.Length == 0)
            {
                return (false, "Topluluk resmi boş olamaz!");
            }
            var community = new Community
            {
                Name = communityDto.Name,
                Description = communityDto.Description,
                Image = communityDto.Image,
                CreatedAt = DateTime.Now,
            };
            _communityDal.Add(community);
            return (true, "Topluluk başarıyla eklendi!");
        }

        public (bool success, string message) Delete(Guid id)
        {
            var community = _communityDal.Get(x => x.Id == id);
            if (community == null)
            {
                return (false, "Topluluk bulunamadı!");
            }
            _communityDal.Delete(community);
            return (true, "Topluluk başarıyla silindi!");
        }

        public List<CommunityDto> GetAll()
        {
            var community = _communityDal.GetAll();
            if (community == null)
            {
                return null;
            }
            var listCommunity = community.Select(c => new CommunityDto
            {
                Name = c.Name,
                Description = c.Description,
                Image = c.Image,
                CreatedAt = c.CreatedAt,
            }).ToList();
            return listCommunity;
        }

        public CommunityDto GetById(Guid id)
        {
            var communit = _communityDal.Get(x => x.Id == id);
            if (communit == null)
            {
                return null;
            }
            var communityDto = new CommunityDto
            {
                Name = communit.Name,
                Description = communit.Description,
                Image = communit.Image,
                CreatedAt = communit.CreatedAt,
            };
            return communityDto;
        }

        public (bool success, string message) Update(CommunityDto communityDto, Guid communityId)
        {
            var community = _communityDal.Get(x => x.Id == communityId);
            if (community == null)
            {
                return (false, "Topluluk bulunamadı!");
            }
            if (string.IsNullOrEmpty(communityDto.Name))
            {
                return (false, "Topluluk adı boş olamaz!");
            }
            if (string.IsNullOrEmpty(communityDto.Description))
            {
                return (false, "Topluluk açıklaması boş olamaz!");
            }
            if (communityDto.Image == null || communityDto.Image.Length == 0)
            {
                return (false, "Topluluk resmi boş olamaz!");
            }
            community.Name = communityDto.Name;
            community.Description = communityDto.Description;
            community.Image = communityDto.Image;
            community.CreatedAt = DateTime.Now;
            _communityDal.Update(community);
            return (true, "Topluluk başarıyla güncellendi!");

        }
    }
}
