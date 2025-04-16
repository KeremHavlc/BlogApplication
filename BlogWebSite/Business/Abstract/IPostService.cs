using Core.Dtos;

namespace Business.Abstract
{
    public interface IPostService
    {
        (bool success, string message) Add(PostDto postDto);
        (bool success, string message) Delete(Guid id);
        (bool success, string message) Update(Guid id, PostDto postDto);
        PostDto GetByUserId(Guid id);
        List<PostDto> GetAll();
    }
}
