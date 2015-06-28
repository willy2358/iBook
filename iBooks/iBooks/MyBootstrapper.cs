using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace iBooks
{
    class MyBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Shell shell = Container.Resolve<Shell>();
            shell.Show();

            return shell;
        }

        protected override void InitializeModules()
        {
            IModule shelfFrame = Container.Resolve<iBooks.MainModule>();
            shelfFrame.Initialize();
        }

        //protected override void ConfigureServiceLocator()
        //{
        //    var container = new UnityContainer();
        //    ServiceLocator.SetLocatorProvider(() => new UnityServiceLocatorAdapter(container));
        //    base.ConfigureServiceLocator();
        //}
    }
}
