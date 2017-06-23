using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.ApplicationManagement.ReleasePublishCategory
{
    public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<ReleasePublishCategoryDataModel>();
			var data = new ReleasePublishCategoryDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ReleasePublishCategoryId =
					Convert.ToInt32(SelectedData.Rows[i][ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ReleasePublishCategoryDataModel();
				data.ReleasePublishCategoryId = Convert.ToInt32(SelectedData.Rows[i][ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId].ToString());
				var dt = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}				
			}

			return UpdatedData.ToDataTable();
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var releasePublishCategorydata = new ReleasePublishCategoryDataModel();
			releasePublishCategorydata.ReleasePublishCategoryId = entityKey;
			var results = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetEntityDetails(releasePublishCategorydata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion		

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleasePublishCategory;
			PrimaryEntityKey = "ReleasePublishCategory";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
    }
}