using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;

namespace Shared.UI.Web.Configuration.DateRangeTitle
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new DateRangeTitleDataModel();

			var UpdatedData = new List<DateRangeTitleDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.DateRangeTitleId = Convert.ToInt32(SelectedData.Rows[i][DateRangeTitleDataModel.DataColumns.DateRangeTitleId].ToString());
				data.Name = SelectedData.Rows[i][DateRangeTitleDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(DateRangeTitleDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(DateRangeTitleDataModel.DataColumns.Description) : SelectedData.Rows[i][DateRangeTitleDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(DateRangeTitleDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(DateRangeTitleDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][DateRangeTitleDataModel.DataColumns.SortOrder].ToString());

				DateRangeTitleDataManager.Update(data, SessionVariables.RequestProfile);

				data = new DateRangeTitleDataModel();

				data.DateRangeTitleId = Convert.ToInt32(SelectedData.Rows[i][DateRangeTitleDataModel.DataColumns.DateRangeTitleId].ToString());

				var dt = DateRangeTitleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new DateRangeTitleDataModel();
			data.DateRangeTitleId = entityKey;
			var results = DateRangeTitleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.DateRangeTitle;		
				PrimaryEntityKey	= "DateRangeTitle";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
