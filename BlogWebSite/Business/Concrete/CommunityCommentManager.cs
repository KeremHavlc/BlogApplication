using Business.Abstract;
using Core.Dtos;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CommunityCommentManager : ICommunityCommentService
    {
        private readonly ICommunityCommentDal _communityCommentDal;
        public CommunityCommentManager(ICommunityCommentDal communityCommentDal)
        {
            _communityCommentDal = communityCommentDal;
        }
        public (bool success, string message) Add(CommunityCommentDto communityCommentDto)
        {
            if (string.IsNullOrEmpty(communityCommentDto.Comment))
            {
                return (false, "Yorum boş olamaz!");
            }
            var communityComment = new CommunityComment
            {
                Comment = communityCommentDto.Comment,
                CommunityPostId = communityCommentDto.CommunityPostId,
                UserId = communityCommentDto.UserId
            };
            _communityCommentDal.Add(communityComment);
            return (true, "Yorum başarıyla eklendi!");
        }   

        public (bool success, string message) Delete(Guid communityCommendId)
        {
            var comment = _communityCommentDal.Get(x => x.Id == communityCommendId);
            if (comment == null)
            {
                return (false, "yorum Bulunamadı!");
            }
            _communityCommentDal.Delete(comment);
            return (true, "Yorum başarıyla silindi!");
        }

            public List<CommunityCommentDto> GetByPostId(Guid communityPostId)
            {
                var communityComment = _communityCommentDal.GetAll(x => x.CommunityPostId == communityPostId);
                if (communityComment == null)
                {
                    return null;
                }
                var listCommunityComment = communityComment.Select(comments => new CommunityCommentDto
                {
                    Comment = comments.Comment,
                    CommunityPostId = comments.CommunityPostId,
                    UserId = comments.UserId
                }).ToList();
                return listCommunityComment;
            }

        public List<CommunityCommentDto> GetByUserId(Guid userId)
        {
            var communityComment = _communityCommentDal.GetAll(x => x.UserId == userId);
            if (communityComment == null)
            {
                return null;
            }
            var listCommunityComment = communityComment.Select(comments => new CommunityCommentDto
            {
                Comment = comments.Comment,
                CommunityPostId = comments.CommunityPostId,
                UserId = comments.UserId
            }).ToList();
            return listCommunityComment;
        }

        public (bool success, string message) Update(CommunityCommentDto communityCommentDto, Guid communityCommendId)
        {
            var communityComment = _communityCommentDal.Get(x => x.Id == communityCommendId);
            if(communityComment == null)
            {
                return (false, "Yorum Bulunamadı!");
            }
            if (string.IsNullOrEmpty(communityCommentDto.Comment))
            {
                return (false, "Yorum boş olamaz!");
            }
            if (communityCommentDto.CommunityPostId == Guid.Empty)
            {
                return (false, "Topluluk Gönderisi Boş olamaz!");
            }
            if (communityCommentDto.UserId == Guid.Empty)
            {
                return (false, "Kullanıcı Boş olamaz!");
            }
            communityComment.Comment = communityCommentDto.Comment;
            communityComment.CommunityPostId = communityCommentDto.CommunityPostId;
            communityComment.UserId = communityCommentDto.UserId;
            communityComment.CreatedAt = DateTime.Now;
            _communityCommentDal.Update(communityComment);
            return (true, "Yorum başarıyla güncellendi!");

        }
    }
}
