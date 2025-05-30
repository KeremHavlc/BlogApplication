﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfCommunityDal : RepositoryBase<Community, AppDbContext>, ICommunityDal
    {
        public EfCommunityDal(AppDbContext context) : base(context)
        {
        }
    }
}
