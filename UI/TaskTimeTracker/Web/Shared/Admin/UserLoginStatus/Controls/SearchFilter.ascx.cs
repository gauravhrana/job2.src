using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.LogAndTrace;

namespace Shared.UI.Web.UserLoginStatus.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public ApplicationContainer.UI.Web.BaseUI.SearchFilterControl SearchControl
		{
			get
			{
				return SearchFilterControl;
			}
		}

		public string GroupBy
		{
			get
			{
				return SearchFilterControl.GroupBy;
			}
		}

		public string SubGroupBy
		{
			get
			{
				return SearchFilterControl.SubGroupBy;
			}
		}

		public UserLoginStatusDataModel SearchParameters
        {
            get
            {
				var data = new UserLoginStatusDataModel();

				SearchFilterControl.SetSearchParameters(data);
				
				return data;
			}
		}

		#endregion		

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
		}

		#endregion

	}
}