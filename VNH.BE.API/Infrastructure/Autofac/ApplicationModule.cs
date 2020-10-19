using Autofac;
using VNH.BE.API.Infrastructure.Services;
using VNH.BE.Infrastructure.Repositories;

namespace VNH.BE.API.Infrastructure.Autofac
{
    public class ApplicationModule : Module
    {
        public ApplicationModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            #region Services
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            //.AddScoped<IAccountService, AccountService>();
            #endregion

            #region Repository
            builder.RegisterType<ApplicationUserRepository>().As<IApplicationUserRepository>().InstancePerLifetimeScope();
            #endregion
        }
    }
}
