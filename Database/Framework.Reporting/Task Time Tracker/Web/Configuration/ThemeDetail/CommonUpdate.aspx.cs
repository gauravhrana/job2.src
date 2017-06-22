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
using System.Text;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace Shared.UI.Web.Configuration.ThemeDetail
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new ThemeDetailDataModel();
			UpdatedData = Framework.Components.Core.ThemeDetailDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ThemeDetailId =
					Convert.ToInt32(SelectedData.Rows[i][ThemeDetailDataModel.DataColumns.ThemeDetailId].ToString());

				data.ThemeKeyId =
					Convert.ToInt32(SelectedData.Rows[i][ThemeDetailDataModel.DataColumns.ThemeKeyId].ToString());

				data.ThemeCategoryId =
					Convert.ToInt32(SelectedData.Rows[i][ThemeDetailDataModel.DataColumns.ThemeCategoryId].ToString());

				data.ThemeId =
					Convert.ToInt32(SelectedData.Rows[i][ThemeDetailDataModel.DataColumns.ThemeId].ToString());

				data.Value =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ThemeDetailDataModel.DataColumns.Value))
					? CheckAndGetRepeaterTextBoxValue(ThemeDetailDataModel.DataColumns.Value)
					: SelectedData.Rows[i][ThemeDetailDataModel.DataColumns.Value].ToString();

				Framework.Components.Core.ThemeDetailDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ThemeDetailDataModel();
				data.ThemeDetailId =
					Convert.ToInt32(SelectedData.Rows[i][ThemeDetailDataModel.DataColumns.ThemeDetailId].ToString());
				var dt = Framework.Components.Core.ThemeDetailDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var ThemeDetaildata = new ThemeDetailDataModel();
			ThemeDetaildata.ThemeDetailId = entityKey;
			var results = Framework.Components.Core.ThemeDetailDataManager.Search(ThemeDetaildata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ThemeDetail;
			PrimaryEntityKey = "ThemeDetail";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}