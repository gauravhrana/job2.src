using PrismSample.Controls.ViewModels;
using System.Windows.Controls;

namespace PrismSample.Controls.Views
{

	public partial class LayerSaveView : UserControl
	{
		public LayerSaveView(LayerSaveViewModel viewModel)
		{
			InitializeComponent();
			this.DataContext = viewModel;
		}
	}
}
