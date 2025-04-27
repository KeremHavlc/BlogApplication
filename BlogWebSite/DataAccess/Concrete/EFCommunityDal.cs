using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EFCommunityDal : RepositoryBase<Community, AppDbContext>, ICommunityDal
    {
        public EFCommunityDal(AppDbContext context) : base(context)
        {
        }
    }
}
