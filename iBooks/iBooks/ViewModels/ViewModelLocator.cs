using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBooks.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocatorAdapter(container));

            //container.RegisterInstance<ILoggingService>(new ConsoleLoggingService());
            //container.RegisterInstance<IMessageBoxService>(new SimpleMessageBoxService());
            //container.RegisterInstance<ITestSuiteService>(new TestSuiteService());
            //container.RegisterInstance<IApplicationService>(new ApplicationService());
        }
    }
}
