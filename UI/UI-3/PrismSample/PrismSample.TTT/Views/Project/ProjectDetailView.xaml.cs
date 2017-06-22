using PrismSample.TTT.ViewModels;
using System.Windows.Controls;

namespace PrismSample.TTT.Views
{
    public partial class ProjectDetailView : UserControl
    {
        public ProjectDetailView(ProjectDetailViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
