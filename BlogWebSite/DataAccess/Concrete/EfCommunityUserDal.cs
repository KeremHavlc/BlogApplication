using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfCommunityUserDal : RepositoryBase<CommunityUser, AppDbContext>, ICommunityUserDal
    {
        public EfCommunityUserDal(AppDbContext context) : base(context)
        {
        }
    }
}
