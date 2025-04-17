using Core.Dtos;

namespace Business.Abstract
{
    public interface ICommentService
    {
        (bool success, string message) Add(CommentDto commentDto , Guid userId);
        (bool success, string message) Delete(Guid id,Guid userId);
        (bool success, string message) Update(Guid id , CommentDto commentDto,Guid userId);
        List<CommentDto> GetByPostId(Guid id);
        List<CommentDto> GetByUserId(Guid id);
        List<CommentDto> GetAll();

    }
}
