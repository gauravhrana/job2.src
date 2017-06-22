using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;

namespace PrismSample.Infrastructure
{
    public class SaveViewModelBase : ViewModelBase
    {

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public SaveViewModelBase(IRegionManager regionManager) : base(regionManager)
        {            
            SaveCommand = new DelegateCommand(ExecuteSave);
            CancelCommand = new DelegateCommand(ExecuteCancel);
        }

        protected void NavigateToListView()
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(PrimaryEntityKey + "ListView", UriKind.Relative));
        }

        protected void ExecuteCancel()
        {
            NavigateToListView();
        }

        protected virtual void ExecuteSave()
        {
            throw new NotImplementedException();
        }
    }
}
