using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogStatus.Controls
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

		public ReleaseLogStatusDataModel SearchParameters
		{
			get
			{
				var data = new ReleaseLogStatusDataModel();

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