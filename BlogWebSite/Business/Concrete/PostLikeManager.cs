using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class PostLikeManager : IPostLikeService
    {
        private readonly IPostLikeDal _postLikeDal;
        private readonly IPostDal _postDal;
        private readonly IUserDal _userDal;
        public PostLikeManager(IPostLikeDal postLikeDal , IPostDal postDal, IUserDal userDal)
        {
            _postLikeDal = postLikeDal;
            _postDal = postDal;
            _userDal = userDal;
        }
        public (bool success, string message) Add(Guid postId, Guid userId)
        {
            var post = _postDal.Get(x => x.Id == postId);
            if (post == null)
            {
                return (false, "Post Bulunamadı!");
            }
            var user = _userDal.Get(x => x.Id == userId);
            if(user.Id == Guid.Empty)
            {
                return (false, "Kullanıcı Bulunamadı!");
            }
            var existingLike = _postLikeDal.Get(x => x.PostId == postId && x.UserId == userId);
            if (existingLike != null)
            {
                if (existingLike.IsActive)
                {
                return (false, "Bu postu zaten beğendiniz!");
                }
                existingLike.IsActive = true;
                existingLike.UpdatedAt = DateTime.Now;
                _postLikeDal.Update(existingLike);
                return (true, "Beğeni tekrar aktif edildi!");
            }
            PostLike postLike = new PostLike
            {
                Id = Guid.NewGuid(),
                PostId = postId,
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            _postLikeDal.Add(postLike);
            return (true, "Post Beğenildi!");
        }

        public (bool success, string message) Delete(Guid postId, Guid userId)
        {
            var post = _postDal.Get(x => x.Id == postId);
            if (post == null)
            {
                return (false, "Post Bulunamadı!");
            }
            var user = _userDal.Get(x => x.Id == userId);
            if (user == null)
            {
                return (false, "Kullanıcı Bulunamadı!");
            }
            var postLike = _postLikeDal.Get(x => x.PostId == post.Id);
            _postLikeDal.Delete(postLike);
            return (true, "Beğeni kaldırıldı!");
        }

        public List<UserDto> GetAllPostLikeUser(Guid postId)
        { 
            var postLikes = _postLikeDal.GetAll(x => x.PostId == postId && x.IsActive);

            if (postLikes == null || !postLikes.Any())
            {
                return new List<UserDto>(); 
            }          
            var users = postLikes
                .Select(like => _userDal.Get(u => u.Id == like.UserId))
                .Where(user => user != null)
                .Select(user => new UserDto
                {
                    Username = user.Username,
                    Email = user.Email,                   
                })
                .ToList();

            return users;
        }

        public bool IsPostLikedByUser(Guid postId, Guid userId)
        {
            var like = _postLikeDal.Get(x => x.PostId == postId && x.UserId == userId && x.IsActive);
            return like != null;
        }
    }
}
