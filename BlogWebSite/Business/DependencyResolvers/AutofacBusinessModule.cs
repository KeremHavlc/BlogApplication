using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Context;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<EfCommentDal>().As<ICommentDal>();
            builder.RegisterType<EfFriendShipDal>().As<IFriendShipDal>();
            builder.RegisterType<EfPostDal>().As<IPostDal>();
            builder.RegisterType<EfPostLikeDal>().As<IPostLikeDal>();
            builder.RegisterType<EfRoleDal>().As<IRoleDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AppDbContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            builder.RegisterType<PostManager>().As<IPostService>();
        }
    }
    
}
