using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfRoleDal : RepositoryBase<Role, AppDbContext>, IRoleDal
    {
        public EfRoleDal(AppDbContext context) : base(context)
        {
        }
    }
   
}
