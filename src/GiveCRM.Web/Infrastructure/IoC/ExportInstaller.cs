using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GiveCRM.ImportExport;

namespace GiveCRM.Web.Infrastructure.IoC
{
    public class ExportInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IExcelExport>().ImplementedBy<ExcelExport>().LifeStyle.PerWebRequest
                );
        }
    }
}