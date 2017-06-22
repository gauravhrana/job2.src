using PrismSample.Controls.ViewModels;
using System.Windows.Controls;

namespace PrismSample.Controls.Views
{

    public partial class ClientListView : UserControl
    {
        public ClientListView(ClientListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
