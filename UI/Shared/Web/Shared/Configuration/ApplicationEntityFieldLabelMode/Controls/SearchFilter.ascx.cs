using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode.Controls
{
	public partial class SearchFilter : Shared.UI.WebFramework.BaseControl
	{
		public event System.EventHandler OnSearch;

		public Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Data SearchParameters
		{
			get
			{
				var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Data();

				data.Name = txtSearchConditionName.Text.Trim();

				return data;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			

		}
		protected void drpSearchConditionSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
		{
			RaiseSearch();
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			RaiseSearch();
		}

		private void RaiseSearch()
		{
			if (OnSearch != null)
			{
				OnSearch(this, EventArgs.Empty);
			}
		}
	}
}