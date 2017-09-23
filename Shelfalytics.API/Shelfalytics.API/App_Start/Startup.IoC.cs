using System.Reflection;
using System.Web.Http.Controllers;
using Autofac;
using Autofac.Integration.WebApi;
using Shelfalytics.Repository;
using Shelfalytics.Repository.Repositories;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.Service;
using Shelfalytics.ServiceInterface;

namespace Shelfalytics.API.App_Start
{
    public class StartupIoC
    {
        public static AutofacWebApiDependencyResolver ConfigureIoC()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetCallingAssembly());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IHttpController).IsAssignableFrom(t) && t.Name.EndsWith("Controller"));

            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();

            builder.RegisterType<EquipmentDataRepository>().As<IEquipmentDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductDataRepository>().As<IProductDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PointOfSaleRepository>().As<IPointOfSaleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthRepository>().As<IAuthRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExceptionLogRepository>().As<IExceptionLogRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SaleRepository>().As<ISaleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MailQueueRepository>().As<IMailQueueRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentDataService>().As<IEquipmentDataService>().InstancePerLifetimeScope();
            builder.RegisterType<PointOfSaleService>().As<IPointOfSaleService>().InstancePerLifetimeScope();
            builder.RegisterType<StatisticsService>().As<IStatisticsService>().InstancePerLifetimeScope();
            builder.RegisterType<MailService>().As<IMailService>().InstancePerLifetimeScope();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            return resolver;
        }
    }
}