using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Windows;

namespace PrismSample.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    //[Export]
    public partial class Shell : Window
    {
        private const string ControlsModuleName = "ControlsModule";

        //ControlsModule
        private static Uri ListViewUri = new Uri("/EntityListView", UriKind.Relative);
        private static Uri SaveViewUri = new Uri("/EntitySaveView", UriKind.Relative);

        public Shell()
        {
            InitializeComponent();
            //this.RegionManager.RequestNavigate(
            //                RegionNames.MainContentRegion,
            //                SaveViewUri);
        }

        //[Import(AllowRecomposition = false)]
        public IModuleManager ModuleManager;

        //[Import(AllowRecomposition = false)]
        public IRegionManager RegionManager;

        public void OnImportsSatisfied()
        {
            this.ModuleManager.LoadModuleCompleted +=
                (s, e) =>
                {
                    if (e.ModuleInfo.ModuleName == ControlsModuleName)
                    {
                        //this.RegionManager.RequestNavigate(
                        //    RegionNames.MainContentRegion,
                        //    ListViewUri);
                    }
                };
        }
    }
}
