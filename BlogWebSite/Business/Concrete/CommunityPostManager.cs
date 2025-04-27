using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CommunityPostManager : ICommunityPostService
    {
        private readonly ICommunityPostDal _communityPostDal;
        private readonly IUserDal _userDal;
        public CommunityPostManager(ICommunityPostDal communityPostDal, IUserDal userDal)
        {
            _communityPostDal = communityPostDal;
            _userDal = userDal;
        }

        public (bool success, string message) AddCommunityPost(CommunityPostDto communityPostDto)
        {
            if (string.IsNullOrEmpty(communityPostDto.Title))
            {
                return (false, "Başlık Boş olamaz!");
            }
            if (string.IsNullOrEmpty(communityPostDto.Description))
            {
                return (false, "Açıklama Boş olamaz!");
            }
            if (communityPostDto.CommunityId == Guid.Empty)
            {
                return (false, "Topluluk Boş olamaz!");
            }
            if (communityPostDto.UserId == Guid.Empty)
            {
                return (false, "Kullanıcı Boş olamaz!");
            }
            var communityPost = new CommunityPost
            {
                Title = communityPostDto.Title,
                Description = communityPostDto.Description,
                CommunityId = communityPostDto.CommunityId,
                UserId = communityPostDto.UserId,
                CreatedAt = DateTime.Now
            };
            _communityPostDal.Add(communityPost);
            return (true, "Topluluk Gönderisi Başarıyla Eklendi!");
        }

        public (bool success, string message) DeleteCommunityPost(Guid communityPostId)
        {
            var communityPost = _communityPostDal.Get(x => x.Id == communityPostId);
            if (communityPost == null)
            {
                return (false, "Topluluk Gönderisi Bulunamadı!");
            }
            _communityPostDal.Delete(communityPost);
            return (true, "Topluluk Gönderisi Başarıyla Silindi!");
        }

        public List<CommunityPostDto> GetPostByCommunity(Guid communityId)
        {
            var communityPosts = _communityPostDal.GetAll(x => x.CommunityId == communityId);
            if (communityPosts == null)
            {
                return null;
            }
            var listCommunityPost = communityPosts.Select(c => new CommunityPostDto
            {
                Title = c.Title,
                Description = c.Description,
                CommunityId = c.CommunityId,
                UserId = c.UserId,
                CreatedAt = c.CreatedAt,
            }).ToList();
            return listCommunityPost;
        }

        public List<CommunityPostDto> GetPostByUser(Guid userId)
        {
            var users = _userDal.GetAll(x => x.Id == userId);
            if (users == null)
            {
                return null;
            }
            var communityPosts = _communityPostDal.GetAll(x => x.UserId == userId);
            if (communityPosts == null)
            {
                return null;
            }
            var listCommunityPost = communityPosts.Select(c => new CommunityPostDto
            {
                Title = c.Title,
                Description = c.Description,
                CommunityId = c.CommunityId,
                UserId = c.UserId,
                CreatedAt = c.CreatedAt,
            }).ToList();
            return listCommunityPost;
        }

        public (bool success, string message) UpdateCommunityPost(CommunityPostDto communityPostDto , Guid communityPostId)
        {
             var communityPost = _communityPostDal.Get(x => x.Id == communityPostId);
            if (communityPost == null)
            {
                return (false, "Topluluk Gönderisi Bulunamadı!");
            }
            if (string.IsNullOrEmpty(communityPostDto.Title))
            {
                return (false, "Başlık Boş olamaz!");
            }
            if (string.IsNullOrEmpty(communityPostDto.Description))
            {
                return (false, "Açıklama Boş olamaz!");
            }
            if (communityPostDto.CommunityId == Guid.Empty)
            {
                return (false, "Topluluk Boş olamaz!");
            }
            if (communityPostDto.UserId == Guid.Empty)
            {
                return (false, "Kullanıcı Boş olamaz!");
            }
            communityPost.Title = communityPostDto.Title;
            communityPost.Description = communityPostDto.Description;
            communityPost.CommunityId = communityPostDto.CommunityId;
            communityPost.UserId = communityPostDto.UserId;
            communityPost.CreatedAt = DateTime.Now;
            _communityPostDal.Update(communityPost);
            return (true, "Topluluk Gönderisi Başarıyla Güncellendi!");
        }

        public (bool success, string message) UpdateCommunityPost(CommunityPostDto communityPostDto)
        {
            throw new NotImplementedException();
        }
    }
}
