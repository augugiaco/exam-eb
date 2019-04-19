using Autofac;
using Exam.Domain.Interfaces.UnitOfWork;
using Exam.Domain.Repository;
using Exam.Infraestructure.Repository;
using Exam.Infraestructure.UnitOfWork;
//using Exam.Infraestructure.UnitOfWork;

namespace Exam.Application.IoC.Modules
{
    public class DataAccessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
