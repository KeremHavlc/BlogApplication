using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfPostDal : RepositoryBase<Post, AppDbContext>, IPostDal
    {
        public EfPostDal(AppDbContext context) : base(context)
        {
        }
    }
}
