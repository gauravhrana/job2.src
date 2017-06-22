using PrismSample.TTT.ViewModels;
using System.Windows.Controls;

namespace PrismSample.TTT.Views
{

    public partial class ProjectSaveView : UserControl
    {

        public ProjectSaveView(ProjectSaveViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
