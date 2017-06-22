using System.Windows.Controls;
using PrismSample.TTT.ViewModels;

namespace PrismSample.TTT.Views
{

    public partial class ProjectListView : UserControl
    {
        public ProjectListView(ProjectListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
