using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.DateRangeTitle
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new DateRangeTitleDataModel();
			UpdatedData = Framework.Components.UserPreference.DateRangeTitleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.DateRangeTitleId =
					Convert.ToInt32(SelectedData.Rows[i][DateRangeTitleDataModel.DataColumns.DateRangeTitleId].ToString());

				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                Framework.Components.UserPreference.DateRangeTitleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new DateRangeTitleDataModel();
				data.DateRangeTitleId = Convert.ToInt32(SelectedData.Rows[i][DateRangeTitleDataModel.DataColumns.DateRangeTitleId].ToString());
				var dt = Framework.Components.UserPreference.DateRangeTitleDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}				
			}
			return UpdatedData;
		}
	
		protected override DataTable GetEntityData(int? entityKey)
		{
			var dateRangeTitledata = new DateRangeTitleDataModel();
			dateRangeTitledata.DateRangeTitleId = entityKey;
			var results = Framework.Components.UserPreference.DateRangeTitleDataManager.Search(dateRangeTitledata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DateRangeTitle;
			PrimaryEntityKey = "DateRangeTitle";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}