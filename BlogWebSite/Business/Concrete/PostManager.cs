using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class PostManager : IPostService
    {
        private readonly IPostDal _postDal;
        public PostManager(IPostDal postdal)
        {
            _postDal = postdal;
        }
        public (bool success, string message) Add(PostDto postDto)
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
                UserId = postDto.UserId
            };
            _postDal.Add(post);
            return (true, "Post Ekleme İşlemi Başarılı!");
        }

        public (bool success, string message) Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<PostDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public PostDto GetByUserId(Guid id)
        {
            throw new NotImplementedException();
        }

        public (bool success, string message) Update(Guid id, PostDto postDto)
        {
            throw new NotImplementedException();
        }
    }
}
