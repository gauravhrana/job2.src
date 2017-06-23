using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace ApplicationContainer.UI.Web.ReportCategory
{
    public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<ReportCategoryDataModel>();
			var data = new ReportCategoryDataModel();
			
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ReportCategoryId =
					Convert.ToInt32(SelectedData.Rows[i][ReportCategoryDataModel.DataColumns.ReportCategoryId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.ApplicationId = Convert.ToInt32(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.Core.ReportCategoryDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ReportCategoryDataModel();
				data.ReportCategoryId = Convert.ToInt32(SelectedData.Rows[i][ReportCategoryDataModel.DataColumns.ReportCategoryId].ToString());
				var dt = Framework.Components.Core.ReportCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}				
			}
			return UpdatedData.ToDataTable();
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var reportCategorydata = new ReportCategoryDataModel();
			reportCategorydata.ReportCategoryId = entityKey;
			var results = Framework.Components.Core.ReportCategoryDataManager.GetEntityDetails(reportCategorydata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReportCategory;
			PrimaryEntityKey = "ReportCategory";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}