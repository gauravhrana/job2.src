using PrismSample.Controls.ViewModels;
using System.Windows.Controls;

namespace PrismSample.Controls.Views
{

    public partial class ClientSaveView : UserControl
    {
        public ClientSaveView(ClientSaveViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
