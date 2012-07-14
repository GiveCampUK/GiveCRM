[assembly: WebActivator.PreApplicationStartMethod(typeof(GiveCRM.Web.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(GiveCRM.Web.App_Start.NinjectMVC3), "Stop")]

namespace GiveCRM.Web.App_Start
{
    using System;
    using System.Configuration;
    using GiveCRM.BusinessLogic;
    using GiveCRM.DataAccess;
    using GiveCRM.Infrastructure;
    using GiveCRM.LoggingService;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Mvc;

    public static class NinjectMVC3 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILogService>().To<LogService>();
            kernel.Bind<ITenantCodeProvider>().To<DomainNameTenantCodeProvider>();
            kernel.Bind<LogService>().ToSelf().InSingletonScope();
            
            string databaseProviderTypeName = ConfigurationManager.AppSettings.Get("DatabaseProviderType");
            if (string.IsNullOrWhiteSpace(databaseProviderTypeName))
            {
                kernel.Bind<IDatabaseProvider>().To<SingleTenantDatabaseProvider>().InRequestScope();
            }
            else
            {
                Type databaseProviderType = Type.GetType(databaseProviderTypeName);
                kernel.Bind<IDatabaseProvider>().To(databaseProviderType).InRequestScope();
            }

            kernel.Scan(a =>
                            {
                                a.FromCallingAssembly();
                                a.FromAssembliesMatching("GiveCRM.*.dll");
                                a.Excluding(typeof(LogService));
                                a.Excluding<IDatabaseProvider>();
                                a.BindWithDefaultConventions();
                                a.BindWith(new RegexBindingGenerator("(I)(?<name>.+)(Repository)"));
                                a.BindWith(new GenericBindingGenerator(typeof(IRepository<>)));
                                a.InRequestScope();
                            });
        }
    }
}
