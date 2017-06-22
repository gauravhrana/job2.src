using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Entity
{
	public partial class Export : Shared.UI.WebFramework.BasePage
	{

		#region variables

		string searchCondition = String.Empty;

		#endregion

		#region private methods

		private System.Data.DataTable GetData()
		{
			// TODO: on all export pages 
			var data = new EntityDataModel();

			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.Search(data, SessionVariables.RequestProfile);

			return dt;
		}

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Entity, "DBColumns", SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				// searchCondition = Request.QueryString["SearchCondition"];

				oList.Setup("Entity", " ", "EntityId", false, GetData, GetColumns, false);
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
			oList.ShowData(true, true);
		}

		#endregion
	}
}