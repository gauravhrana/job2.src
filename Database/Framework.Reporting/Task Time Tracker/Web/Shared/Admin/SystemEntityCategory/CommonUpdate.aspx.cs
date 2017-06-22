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

namespace Shared.UI.Web.Admin.SystemEntityCategory
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new SystemEntityCategoryDataModel();
			UpdatedData = Framework.Components.Core.SystemEntityCategoryDataManager.Search(data, SessionVariables.RequestProfile).Clone();
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
				var dt = Framework.Components.Core.SystemEntityCategoryDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}				
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var systemEntityCategorydata = new SystemEntityCategoryDataModel();
			systemEntityCategorydata.SystemEntityCategoryId = entityKey;
			var results = Framework.Components.Core.SystemEntityCategoryDataManager.Search(systemEntityCategorydata, SessionVariables.RequestProfile);
			return results;
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