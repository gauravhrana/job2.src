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

namespace Shared.UI.Web.Admin.SystemEntityCategory
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<SystemEntityCategoryDataModel>();
			var data = new SystemEntityCategoryDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.SystemEntityCategoryId =
					Convert.ToInt32(SelectedData.Rows[i][SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId].ToString());

				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.Core.SystemEntityCategoryDataManager.Update(data, SessionVariables.RequestProfile);
				data = new SystemEntityCategoryDataModel();
				
				data.SystemEntityCategoryId = Convert.ToInt32(SelectedData.Rows[i][SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId].ToString());
				var dt = Framework.Components.Core.SystemEntityCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}				
			}
			return UpdatedData.ToDataTable();
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var systemEntityCategorydata = new SystemEntityCategoryDataModel();
			systemEntityCategorydata.SystemEntityCategoryId = entityKey;
			var results = Framework.Components.Core.SystemEntityCategoryDataManager.GetEntityDetails(systemEntityCategorydata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityCategory;
			PrimaryEntityKey = "SystemEntityCategory";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
		
	}
}