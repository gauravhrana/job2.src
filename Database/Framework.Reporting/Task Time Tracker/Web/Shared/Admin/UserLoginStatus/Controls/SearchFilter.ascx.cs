using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.LogAndTrace;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.UserLoginStatus.Controls
{ 
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
		#region variables		

		public UserLoginStatusDataModel SearchParameters
		{
			get
			{
				var data = new UserLoginStatusDataModel();

				GetParameterValue(data, StandardDataModel.StandardDataColumns.Name);

				return data;
			}
		}		

		#endregion		

		#region Events			

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "UserLoginStatus";
			FolderLocationFromRoot = "/Shared/Admin/UserLoginStatus";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginStatus;

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

		#endregion

	}
}