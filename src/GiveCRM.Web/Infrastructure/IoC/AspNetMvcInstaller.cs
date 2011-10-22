using System;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace GiveCRM.Web.Infrastructure.IoC
{
    public class AspNetMvcInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<Controller>()
                    .Configure(registration => registration.Named(registration.Implementation.Name))
                    .LifestylePerWebRequest()
                );
        }
    }
}