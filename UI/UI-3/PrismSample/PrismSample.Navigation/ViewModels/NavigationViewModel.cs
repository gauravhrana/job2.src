using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PrismSample.Navigation.ViewModels
{
    public class NavigationViewModel : BindableBase
    {

        private readonly IRegionManager _regionManager;

        public ObservableCollection<MyMenuItem> MainMenu { get; set; }
        private List<MyMenuItem> _MainMenu { get; set; }   

        public DelegateCommand<string> NavigateCommand { get; set; }

        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            AddSubMenus();
            MainMenu = new ObservableCollection<MyMenuItem>(_MainMenu);

            NavigateCommand = new DelegateCommand<string>(RequestNavigation);
        }

        private void RequestNavigation(string uri)
        {
            //_regionManager.RequestNavigate(RegionNames.MainContentRegion, uri);
        }

        private void AddSubMenus()
        {
            _MainMenu = new List<MyMenuItem>();

            var tttMenu = new MyMenuItem(_regionManager);
            tttMenu.Header = "TTT";

            var tttSubMenuItems = new List<MyMenuItem>();

            var tttSubMenu1 = new MyMenuItem(_regionManager);
            tttSubMenu1.Header = "Client";
            tttSubMenu1.CommandParameter = "Client";

            tttSubMenuItems.Add(tttSubMenu1);

            var tttSubMenu2 = new MyMenuItem(_regionManager);
            tttSubMenu2.Header = "Layer";
            tttSubMenu2.CommandParameter = "Layer";

            tttSubMenuItems.Add(tttSubMenu2);

            tttMenu.Items = tttSubMenuItems;

            _MainMenu.Add(tttMenu);

            var reqMenu = new MyMenuItem(_regionManager);
            reqMenu.Header = "Req Analysis";

            var reqSubMenuItems = new List<MyMenuItem>();

            var reqSubMenu1 = new MyMenuItem(_regionManager);
            reqSubMenu1.Header = "Project";
            reqSubMenu1.CommandParameter = "Project";

            reqSubMenuItems.Add(reqSubMenu1);
            reqMenu.Items = reqSubMenuItems;

            _MainMenu.Add(reqMenu);

            var tSubMenu = new MyMenuItem(_regionManager);
            tSubMenu.Header = "Test Dummy";
            var tSubMenuChild = new List<MyMenuItem>();

            var testSubMenu = new MyMenuItem(_regionManager);
            testSubMenu.Header = "CRUD1";

            var childItems = new List<MyMenuItem>();

            var childMenu = new MyMenuItem(_regionManager);
            childMenu.Header = "Child Menu1";
            childMenu.Items = new List<MyMenuItem>();

            var childMenu2 = new MyMenuItem(_regionManager);
            childMenu2.Header = "Child Menu2";
            childMenu2.Items = new List<MyMenuItem>();

            var childMenu3 = new MyMenuItem(_regionManager);
            childMenu3.Header = "Child Menu3";

            var childMenu2Items = new List<MyMenuItem>();
            childMenu2Items.Add(childMenu3);
            childMenu2.Items = childMenu2Items;
            
            //childMenu3.Items

            childItems.Add(childMenu);
            childItems.Add(childMenu2);

            testSubMenu.Items = childItems;

            tSubMenuChild.Add(testSubMenu);

            var testSubMenu2 = new MyMenuItem(_regionManager);
            testSubMenu2.Header = "CRUD2";
            testSubMenu2.Items = new List<MyMenuItem>();

            tSubMenuChild.Add(testSubMenu2);

            tSubMenu.Items = tSubMenuChild;

            _MainMenu.Add(tSubMenu);
        }

    }

}
