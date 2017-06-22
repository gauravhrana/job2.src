using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Controls
{
	public partial class QuickSearch : UserControl
	{
		public event EventHandler OnSearch;

		public void RaiseSearch()
		{
			if (OnSearch != null)
			{
				OnSearch(this, EventArgs.Empty);
			}
		}

		public void SetUp()
		{
			RaiseSearch();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//RaiseSearch();
			}
		}		

		//protected void btnSearch_Click(object sender, EventArgs e)
		//{
		//    RaiseSearch();
			
		//}
	}
}