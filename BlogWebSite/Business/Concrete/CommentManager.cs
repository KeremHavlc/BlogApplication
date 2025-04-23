using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public (bool success, string message) Add(CommentDto commentDto, Guid userId)
        {
            if (string.IsNullOrEmpty(commentDto.Message))
            {
                return (false, "Yorum Giriniz!");
            }
            if(commentDto.PostId == Guid.Empty)
            {
                return (false, "Geçerli bir gönderi değildir!");
            }
            Comment comment = new Comment
            {
                Message = commentDto.Message,
                PostId = commentDto.PostId,
                UserId = commentDto.UserId,
                CreatedAt = DateTime.Now,
            };
            _commentDal.Add(comment);
            return (true, "Yorum Başarılı bir şekilde kayıt edildi!");
        }

        public (bool success, string message) Delete(Guid id , Guid userId)
        {
            var comment = _commentDal.Get(X => X.Id == id);
            if(id == Guid.Empty)
            {
                return (false, "Yorum Id Giriniz!");
            }
            if (comment is null)
            {
                return (false, "Yorum Bulunamadı!");
            }
            if(comment.UserId != userId)
            {
                return (false, "Bu yorumu silme yetkiniz yok!");
            }
            _commentDal.Delete(comment);
            return (true, "Post Başarıyla silindi!");
        }

        public List<CommentDto> GetAll()
        {
            var comments = _commentDal.GetAll();
            if (comments is null)
            {
                return null;
            }
            var commentsDto = comments.Select(comment => new CommentDto
            {
                Message = comment.Message,
                PostId = comment.PostId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt
            }).ToList();
            return commentsDto;
        }

        public List<CommentDto> GetByPostId(Guid id)
        {
            var comments = _commentDal.GetAll(x => x.PostId == id);
            if (comments is null)
            {
                return null;
            }
            var commentsDto = comments.Select(comment => new CommentDto
            {
                Message = comment.Message,
                PostId = comment.PostId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt
            }).ToList();
            return commentsDto;
        }

        public List<CommentDto> GetByUserId(Guid id)
        {
            var comments = _commentDal.GetAll(x => x.UserId == id);
            if(comments is null)
            {
                return null;
            }
            var commentsDto = comments.Select(comment => new CommentDto
            {
                Message = comment.Message,
                PostId = comment.PostId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt
            }).ToList();
            return commentsDto;
        }

        public (bool success, string message) Update(Guid id, CommentDto commentDto, Guid userId)
        {
            var comment = _commentDal.Get(x => x.Id == id);
            if(comment.UserId != userId)
            {
                return (false, "Bu yorumu güncelleme yetkiniz yok!");
            }
            if(comment is null)
            {
                return (false, "Yorum Bulunamadı!");
            }
            if (string.IsNullOrEmpty(commentDto.Message))
            {
                return (false, "Mesaj Giriniz!");
            }
            if (comment.PostId != commentDto.PostId)
            {
                return (false, "Gönderi ID'si değiştirilemez!");
            }
            comment.Message = commentDto.Message;
            _commentDal.Update(comment);
            return (true, "Yorum Güncelleme İşlemi Başarılı!");
        }
    }
}
