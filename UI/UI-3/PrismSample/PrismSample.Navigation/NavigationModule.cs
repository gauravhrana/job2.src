using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using PrismSample.Navigation.Views;
using Microsoft.Practices.Unity;

namespace PrismSample.Navigation
{
    public class NavigationModule : IModule
    {
        protected IRegionManager _regionManager { get; private set; }
        protected IUnityContainer _container { get; private set; }

        public NavigationModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            //register type for navigation
            //_container.RegisterType(typeof(object), typeof(NavigationView), "NavigationView");

            //navigate to ListView
            //_regionManager.RequestNavigate(RegionNames.MainNavigationRegion, "NavigationView");

            _regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(NavigationView));

            //this.RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(NavigationView));
        }

    }
}
