using Core.Dtos;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IPostService
    {
        (bool success, string message) Add(PostDto postDto , Guid userId);
        (bool success, string message) Delete(Guid id , Guid userId);
        (bool success, string message) Update(Guid id, PostDto postDto , Guid userId);
        List<PostDto> GetByUserId(Guid id);
        List<Post> GetAll();
        Post GetByPostId(Guid postId);
    }
}
