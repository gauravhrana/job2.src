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

namespace Shared.UI.Web.Configuration.Theme
{

	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new ThemeDataModel();
			UpdatedData = Framework.Components.Core.ThemeDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{

				data.ThemeId =
					Convert.ToInt32(SelectedData.Rows[i][ThemeDataModel.DataColumns.ThemeId].ToString());

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.Name =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				Framework.Components.Core.ThemeDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ThemeDataModel();
				data.ThemeId =
					Convert.ToInt32(SelectedData.Rows[i][ThemeDataModel.DataColumns.ThemeId].ToString());
				var dt = Framework.Components.Core.ThemeDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var Themedata = new ThemeDataModel();
			Themedata.ThemeId = entityKey;
			var results = Framework.Components.Core.ThemeDataManager.Search(Themedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Theme;
			PrimaryEntityKey = "Theme";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}