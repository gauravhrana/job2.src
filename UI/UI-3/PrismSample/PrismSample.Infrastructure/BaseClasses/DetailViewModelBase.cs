
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;

namespace PrismSample.Infrastructure
{
    public class DetailViewModelBase : ViewModelBase
    {

        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand NavigateCommand { get; set; }

        public DetailViewModelBase(IRegionManager regionManager) : base(regionManager)
        {
            DeleteCommand = new DelegateCommand(ExecuteDelete);
            NavigateCommand = new DelegateCommand(ExecuteNavigatation);
        }

        protected virtual void ExecuteDelete()
        {
            throw new NotImplementedException();
        }

        protected void ExecuteNavigatation()
        {
            NavigateToListView();
        }

        protected void NavigateToListView()
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(PrimaryEntityKey + "ListView", UriKind.Relative));
        }

    }
}
