using PrismSample.Navigation.ViewModels;
using System.Windows.Controls;

namespace PrismSample.Navigation.Views
{

    public partial class NavigationView : UserControl
    {
        public NavigationView(NavigationViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

    }

}
