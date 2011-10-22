using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace GiveCRM.Web.Infrastructure.IoC
{
    public class ExportInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            throw new NotImplementedException();
        }
    }
}