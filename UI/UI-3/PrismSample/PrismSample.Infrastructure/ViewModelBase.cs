using System;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Framework.Components.DataAccess;

namespace PrismSample.Infrastructure
{
    public abstract class ViewModelBase : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        protected readonly IRegionManager RegionManager;

        public bool KeepAlive
        {
            get { return false; }
        }

        protected SystemEntity PrimaryEntity { get; set; }
        protected string PrimaryEntityKey { get; set; }

        private string _settingCategory;
        public string SettingCategory
        {
            get { return _settingCategory; }
            set
            {
                SetProperty<string>(ref _settingCategory, value);
            }
        }

        public ViewModelBase(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        
    }
}
