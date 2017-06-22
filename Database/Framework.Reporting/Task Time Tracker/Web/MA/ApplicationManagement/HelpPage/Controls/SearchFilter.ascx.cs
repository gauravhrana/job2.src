using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.HelpPage.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

		#region variables

		public HelpPageDataModel SearchParameters
		{
			get
			{
				var data = new HelpPageDataModel();				

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
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

		#endregion

    }
}
