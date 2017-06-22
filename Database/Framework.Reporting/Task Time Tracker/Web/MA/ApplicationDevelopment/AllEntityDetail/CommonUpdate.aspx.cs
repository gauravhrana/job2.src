using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetail
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();

			var data = new AllEntityDetailDataModel();
			UpdatedData = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.AllEntityDetailId =
					Convert.ToInt32(SelectedData.Rows[i][AllEntityDetailDataModel.DataColumns.AllEntityDetailId].ToString());
				data.EntityName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(AllEntityDetailDataModel.DataColumns.EntityName))
					? CheckAndGetRepeaterTextBoxValue(AllEntityDetailDataModel.DataColumns.EntityName)
					: SelectedData.Rows[i][AllEntityDetailDataModel.DataColumns.EntityName].ToString();
				data.DB_Name =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(AllEntityDetailDataModel.DataColumns.DB_Name))
					? CheckAndGetRepeaterTextBoxValue(AllEntityDetailDataModel.DataColumns.DB_Name)
					: SelectedData.Rows[i][AllEntityDetailDataModel.DataColumns.DB_Name].ToString();

				data.DB_Project_Name =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(AllEntityDetailDataModel.DataColumns.DB_Project_Name))
					? CheckAndGetRepeaterTextBoxValue(AllEntityDetailDataModel.DataColumns.DB_Project_Name)
					: SelectedData.Rows[i][AllEntityDetailDataModel.DataColumns.DB_Project_Name].ToString();

				data.Component_Project_Name =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(AllEntityDetailDataModel.DataColumns.Component_Project_Name))
					? CheckAndGetRepeaterTextBoxValue(AllEntityDetailDataModel.DataColumns.Component_Project_Name)
					: SelectedData.Rows[i][AllEntityDetailDataModel.DataColumns.Component_Project_Name].ToString();

				TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.Update(data, SessionVariables.RequestProfile);
				data = new AllEntityDetailDataModel();
				data.AllEntityDetailId = Convert.ToInt32(SelectedData.Rows[i][AllEntityDetailDataModel.DataColumns.AllEntityDetailId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var allEntityDetaildata = new AllEntityDetailDataModel();
			allEntityDetaildata.AllEntityDetailId = entityKey;
			var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.Search(allEntityDetaildata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AllEntityDetail;
			PrimaryEntityKey = "AllEntityDetail";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}