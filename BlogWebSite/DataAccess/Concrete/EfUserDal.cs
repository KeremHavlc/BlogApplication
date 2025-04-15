using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfUserDal : RepositoryBase<User , AppDbContext>,IUserDal
    {
        public EfUserDal(AppDbContext context) : base(context)
        {
        }
    }
}
