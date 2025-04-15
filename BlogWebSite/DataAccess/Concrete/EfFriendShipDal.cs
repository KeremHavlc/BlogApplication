using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfFriendShipDal : RepositoryBase<FriendShip, AppDbContext>, IFriendShipDal
    {
        public EfFriendShipDal(AppDbContext context) : base(context)
        {
        }
    }
   
}
