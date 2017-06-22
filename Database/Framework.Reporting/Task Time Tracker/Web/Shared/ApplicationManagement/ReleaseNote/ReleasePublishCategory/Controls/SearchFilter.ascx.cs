using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.ApplicationManagement.ReleasePublishCategory.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		public ApplicationContainer.UI.Web.BaseUI.SearchFilterControl SearchControl
		{
			get
			{
				return SearchFilterControl;
			}
		}

		public ReleasePublishCategoryDataModel SearchParameters
		{
			get
			{
				var data = new ReleasePublishCategoryDataModel();

                SearchFilterControl.SetSearchParameters(data);			

				CommonSearchParameters();

				return data;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

	}
}