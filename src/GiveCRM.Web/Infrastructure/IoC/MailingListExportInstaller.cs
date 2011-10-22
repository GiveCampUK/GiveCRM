using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GiveCRM.Web.Services;

namespace GiveCRM.Web.Infrastructure.IoC
{
    public class MailingListExportInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMailingListService>().ImplementedBy<MailingListService>().LifeStyle.PerWebRequest
                );
        }
    }
}