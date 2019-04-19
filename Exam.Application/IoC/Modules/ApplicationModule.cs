using Autofac;
using Exam.Application.Interfaces;
using Exam.Application.Services;
using Exam.Domain.Base.Services;
using Exam.Domain.Services;

namespace Exam.Application.IoC.Modules
{
    public class ApplicationModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterGeneric(typeof(ServiceBase<,>)).As(typeof(IService<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DomainBaseLogic<,>)).As(typeof(IDomainLogic<,>)).InstancePerLifetimeScope();
            //DOMAIN
            builder.RegisterType<UserDomainLogic>().As<IUserDomainLogic>().InstancePerLifetimeScope();
            builder.RegisterType<UserApplicationService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDownloaderService>().As<IUserDownloaderService>().InstancePerLifetimeScope();

       
            base.Load(builder);
        }
    }
}
