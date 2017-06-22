using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using PrismSample.TTT.Views;
using Microsoft.Practices.Unity;
using PrismSample.Infrastructure;

namespace PrismSample.TTT
{
    public class TTTModule : ModuleBase
    {
        public TTTModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager) { }

        protected override void InitializeModule()
        {
            //register type for navigation
            //_container.RegisterTypeForNavigation<ProjectListView>();
            //_container.RegisterType(typeof(object), typeof(ProjectSaveView), "ProjectSaveView");
            //_container.RegisterType(typeof(object), typeof(ProjectDetailView), "ProjectDetailView");

            //navigate to ListView
            //_regionManager.RequestNavigate(RegionNames.MainContentRegion, "EntityListView");

            //_regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ProjectListView));
            //_regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ProjectSaveView));
            //_regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ProjectDetailView));

            //RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ProjectListView));
            //RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ProjectSaveView));
            //RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ProjectDetailView));
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<ProjectListView>();
            Container.RegisterTypeForNavigation<ProjectSaveView>();
            Container.RegisterTypeForNavigation<ProjectDetailView>();
        }

    }

}
