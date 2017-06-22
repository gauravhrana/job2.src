using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem
{
	public partial class Export : BasePage
	{

		#region variables

		#endregion

		#region private methods

		private DataTable GetData()
		{
			var data = new TaskAlgorithmItemDataModel();
			var dt = TaskAlgorithmItemDataManager.Search(data,SessionVariables.RequestProfile);
			return dt;
		}

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.TaskAlgorithmItem, "DBColumns", SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			oList.Setup("TaskAlgorithmItem", " ", "TaskAlgorithmItemId", false, GetData, GetColumns, false);
			oList.ExportMenu.Visible = false;
		}

		protected override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
			oList.ShowData(true, true);
		}

		#endregion
	}
}