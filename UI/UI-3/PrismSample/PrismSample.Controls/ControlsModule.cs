using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using PrismSample.Controls.Views;
using Microsoft.Practices.Unity;

namespace PrismSample.Controls
{
    public class ControlsModule : ModuleBase
    {
        public ControlsModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager) { }

        protected override void InitializeModule()
        {
            //navigate to ListView
            //RegionManager.RequestNavigate(RegionNames.MainContentRegion, "EntityListView");
        }

        protected override void RegisterTypes()
        {
            //register type for navigation
            //Container.RegisterType(typeof(object), typeof(EntityListView), "EntityListView");

            Container.RegisterTypeForNavigation<ClientListView>();
            Container.RegisterTypeForNavigation<ClientSaveView>();
            Container.RegisterTypeForNavigation<ClientDetailView>();

            Container.RegisterTypeForNavigation<LayerListView>();
            Container.RegisterTypeForNavigation<LayerSaveView>();
            Container.RegisterTypeForNavigation<LayerDetailView>();
        }
    }
}
