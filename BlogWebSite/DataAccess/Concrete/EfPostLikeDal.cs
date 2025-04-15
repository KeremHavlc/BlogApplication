using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfPostLikeDal : RepositoryBase<PostLike, AppDbContext>, IPostLikeDal
    {
        public EfPostLikeDal(AppDbContext context) : base(context)
        {
        }
    }
    
}
