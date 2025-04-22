using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class PostManager : IPostService
    {
        private readonly IPostDal _postDal;
        private readonly IUserDal _userDal;
        public PostManager(IPostDal postdal , IUserDal userDal)
        {
            _postDal = postdal;
            _userDal = userDal;
        }
        public (bool success, string message) Add(PostDto postDto, Guid userId)
        {
            if (string.IsNullOrEmpty(postDto.Title))
            {
                return (false, "Başlık Giriniz!");
            }
            if (string.IsNullOrEmpty(postDto.Description))
            {
                return (false, "Açıklama Giriniz!");
            }
            Post post = new Post
            {
                Id = Guid.NewGuid(),
                Title = postDto.Title,
                Description = postDto.Description,
                Image = postDto.Image,
                UserId = userId
            };
            _postDal.Add(post);
            return (true, "Post Ekleme İşlemi Başarılı!");
        }

        public (bool success, string message) Delete(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
            {
                return (false, "Post Id Giriniz!");
            }

            var post = _postDal.Get(x => x.Id == id);
            if (post is null)
            {
                return (false, "Post Bulunamadı!");
            }

            if (post.UserId != userId)
            {
                return (false, "Bu post'u silme yetkiniz yok!");
            }

            _postDal.Delete(post);
            return (true, "Post Başarıyla silindi!");
        }

        public List<Post> GetAll()
        {
            var posts = _postDal.GetAll();
            if (posts is null)
            {
                return null;
            }         
            return posts;
        }

        public List<PostDto> GetByUserId(Guid id)
        {
            var user = _userDal.Get(x => x.Id == id);
            if(user is null)
            {
                return null;
            }
            var posts = _postDal.GetAll(x => x.UserId == id);
            if(posts == null || !posts.Any())
            {
                return null;
            }
            var listposts = posts.Select(post => new PostDto
            {
                Title = post.Title,
                Description = post.Description,
                Image = post.Image
            }).ToList();
            return listposts;
        }

        public (bool success, string message) Update(Guid id, PostDto postDto , Guid userId)
        {
            var post = _postDal.Get(x => x.Id == id);
            if (post.UserId != userId)
            {
                return (false, "Postu Güncellemeye Yetkiniz Yok!");
            }
            if (post is null)
            {
                return (false, "Post Bulunamadı!");
            }
            if (string.IsNullOrEmpty(postDto.Title))
            {
                return (false, "Başlık Giriniz!");
            }
            if (string.IsNullOrEmpty(postDto.Description))
            {
                return (false, "Açıklama Giriniz!");
            }
            post.Title = postDto.Title;
            post.Description = postDto.Description;
            post.Image = postDto.Image;
           
            _postDal.Update(post);
            return (true, "Post Güncelleme İşlemi Başarılı!");  
        }
    }
}
