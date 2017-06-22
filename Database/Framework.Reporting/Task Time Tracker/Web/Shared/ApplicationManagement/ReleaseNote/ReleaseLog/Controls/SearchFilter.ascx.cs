using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.ReleaseLog;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls
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

		public ReleaseLogDataModel SearchParameters
		{
			get
			{
				var data = new ReleaseLogDataModel();

                SearchFilterControl.SetSearchParameters(data);

				data.ApplicationId = SearchFilterControl.GetParameterValueAsInt(BaseDataModel.BaseDataColumns.ApplicationId);

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