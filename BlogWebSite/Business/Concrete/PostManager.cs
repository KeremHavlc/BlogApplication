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
                return (false, "Başlık Giriniz!");

            if (string.IsNullOrEmpty(postDto.Description))
                return (false, "Açıklama Giriniz!");

            byte[] imageBytes = null;           
            if (!string.IsNullOrEmpty(postDto.Image))
            {
                try
                {                    
                    var base64Data = postDto.Image.Split(',').LastOrDefault();
                    imageBytes = Convert.FromBase64String(base64Data);
                }
                catch (FormatException)
                {
                    return (false, "Geçersiz resim formatı. Lütfen Base64 kodlanmış bir resim gönderin.");
                }
            }
            Post post = new Post
            {
                Id = Guid.NewGuid(),
                Title = postDto.Title,
                Description = postDto.Description,
                Image = imageBytes,
                UserId = userId
            };
            try
            {
                _postDal.Add(post);
                return (true, "Post başarıyla eklendi!");
            }
            catch (Exception ex)
            {
                return (false, "Post eklenirken bir hata oluştu: " + ex.Message);
            }
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

        public Post GetByPostId(Guid postId)
        {
            var post = _postDal.Get(x => x.Id == postId);
            if(post is null)
            {
                return null;
            }
            return post;
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
                Image = post.Image != null ? Convert.ToBase64String(post.Image) : null
            }).ToList();
            return listposts;
        }

        public (bool success, string message) Update(Guid id, PostDto postDto, Guid userId)
        {
            // Önce post'un varlığını kontrol et
            var post = _postDal.Get(x => x.Id == id);
            if (post is null)
            {
                return (false, "Post Bulunamadı!");
            }

            // Yetki kontrolünü sonra yap
            if (post.UserId != userId)
            {
                return (false, "Postu Güncellemeye Yetkiniz Yok!");
            }

            // Validation kontrolleri
            if (string.IsNullOrEmpty(postDto.Title))
            {
                return (false, "Başlık Giriniz!");
            }
            if (string.IsNullOrEmpty(postDto.Description))
            {
                return (false, "Açıklama Giriniz!");
            }

            // Image dönüşümü ve validasyon
            byte[] imageBytes = null;
            if (!string.IsNullOrEmpty(postDto.Image))
            {
                try
                {
                    imageBytes = Convert.FromBase64String(postDto.Image);
                }
                catch (FormatException)
                {
                    return (false, "Geçersiz resim formatı. Base64 string bekleniyor.");
                }
            }

            // Güncelleme işlemleri
            post.Title = postDto.Title;
            post.Description = postDto.Description;
            post.Image = imageBytes; // Dönüştürülmüş byte dizisini ata

            try
            {
                _postDal.Update(post);
                return (true, "Post başarıyla güncellendi!");
            }
            catch (Exception ex)
            {
                // Loglama yapılmalı
                return (false, $"Güncelleme hatası: {ex.Message}");
            }
        }
    }
}
