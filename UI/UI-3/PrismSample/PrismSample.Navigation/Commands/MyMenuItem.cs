using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace PrismSample.Navigation
{

    public class MyMenuItem : ICommand
    {
        public MyMenuItem() { }

        public MyMenuItem(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }

        public string Name;
        private readonly IRegionManager _regionManager;
        public string Header { get; set; }
        public IEnumerable<MyMenuItem> Items { get; set; }
        public string CommandParameter { get; set; }
        public string Command { get; set; }
        public string Entity { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                var entityName = parameter.ToString();
                this._regionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(entityName + "ListView", UriKind.Relative));
            }
        }
    }

}
