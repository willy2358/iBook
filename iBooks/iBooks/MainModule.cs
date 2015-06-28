using iBooks.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBooks
{
    class MainModule : IModule
    {
        public MainModule(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;
        }

        public void Initialize()
        {
            var addFundView = Container.Resolve<ShelfFrameView>();
            RegionManager.Regions["MainRegion"].Add(addFundView);
        }

        public IUnityContainer Container 
        {
            get; 
            private set; 
        }
        public IRegionManager RegionManager 
        { 
            get; 
            private set; 
        }

    }
}
