using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
{
	public partial class Export : Shared.UI.WebFramework.BasePage
	{
		string searchCondition = String.Empty;

		private System.Data.DataTable GetData()
		{
			// TODO: on all export pages 
			var data = new ApplicationUserDataModel();

			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.Search(data, SessionVariables.RequestProfile);

			return dt;
		}

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ApplicationUser, "DBColumns", SessionVariables.RequestProfile);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				// searchCondition = Request.QueryString["SearchCondition"];

				oList.Setup("ApplicationUser", " ", "ApplicationUserId", false, GetData, GetColumns, false);
				oList.ExportMenu.Visible = false;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		protected override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			// only need to do first time
			// other time it will be fired by sort control and handled in its event handler
			if (!IsPostBack)
			{
				//oList.ShowData(SearchConditionLName, SearchConditionFName, true);
				oList.ShowData(true, true);
			}

		}
	}
}