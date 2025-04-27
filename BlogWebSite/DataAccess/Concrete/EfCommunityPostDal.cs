using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfCommunityPostDal : RepositoryBase<CommunityPost, AppDbContext>, ICommunityPostDal
    {
        public EfCommunityPostDal(AppDbContext context) : base(context)
        {
        }
    }
    
}

