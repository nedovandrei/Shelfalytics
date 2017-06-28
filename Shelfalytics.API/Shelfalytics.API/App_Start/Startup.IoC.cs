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
            builder.RegisterType<EquipmentDataService>().As<IEquipmentDataService>().InstancePerLifetimeScope();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            return resolver;
        }
    }
}