using PrismSample.Controls.ViewModels;
using System.Windows.Controls;

namespace PrismSample.Controls.Views
{

	public partial class LayerDetailView : UserControl
	{
		public LayerDetailView(LayerDetailViewModel viewModel)
		{
			InitializeComponent();
			this.DataContext = viewModel;
		}
	}
}
