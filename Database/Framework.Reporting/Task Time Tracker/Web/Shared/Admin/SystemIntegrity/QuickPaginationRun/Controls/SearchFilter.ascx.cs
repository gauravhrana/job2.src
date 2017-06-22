using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Text;
using ApplicationContainer.UI.Web.BaseUI;
using ControlSearchFilter = Framework.UI.Web.BaseClasses.ControlSearchFilter;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;

namespace Shared.UI.Web.SystemIntegrity.QuickPaginationRun.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		#region variables		

		public QuickPaginationRunDataModel SearchParameters
		{
			get
			{
				var data = new QuickPaginationRunDataModel();

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