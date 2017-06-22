using PrismSample.Controls.ViewModels;
using System.Windows.Controls;

namespace PrismSample.Controls.Views
{

	public partial class LayerListView : UserControl
	{
		public LayerListView(LayerListViewModel viewModel)
		{
			InitializeComponent();
			this.DataContext = viewModel;
		}
	}
}
