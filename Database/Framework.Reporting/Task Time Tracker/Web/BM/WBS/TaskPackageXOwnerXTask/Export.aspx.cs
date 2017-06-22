using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.Task;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask
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
			var data = new TaskPackageXOwnerXTaskDataModel();

            var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Search(data, SessionVariables.RequestProfile);

			return dt;
		}

		private string[] GetColumns()
		{
			var validColumns = FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask, "Default", SessionVariables.RequestProfile);
			return validColumns;
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				// searchCondition = Request.QueryString["SearchCondition"];

				oList.Setup("TaskPackageXOwnerXTask", " ", "TaskPackageXOwnerXTaskId", false, GetData, GetColumns, false);
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