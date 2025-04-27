using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfCommunityCommentDal : RepositoryBase<CommunityComment, AppDbContext>, ICommunityCommentDal
    {
        public EfCommunityCommentDal(AppDbContext context) : base(context)
        {
        }
    }
}
