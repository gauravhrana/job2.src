using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		public ReleaseIssueTypeDataModel SearchParameters
		{
			get
			{
				var data = new ReleaseIssueTypeDataModel();

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