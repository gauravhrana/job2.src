using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace Shared.UI.Web.Configuration.Theme
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ThemeDataModel();

			var UpdatedData = new List<ThemeDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.ThemeId = Convert.ToInt32(SelectedData.Rows[i][ThemeDataModel.DataColumns.ThemeId].ToString());
				data.Name = SelectedData.Rows[i][ThemeDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ThemeDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(ThemeDataModel.DataColumns.Description) : SelectedData.Rows[i][ThemeDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ThemeDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(ThemeDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][ThemeDataModel.DataColumns.SortOrder].ToString());

				ThemeDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ThemeDataModel();

				data.ThemeId = Convert.ToInt32(SelectedData.Rows[i][ThemeDataModel.DataColumns.ThemeId].ToString());

				var dt = ThemeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ThemeDataModel();
			data.ThemeId = entityKey;
			var results = ThemeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.Theme;		
				PrimaryEntityKey	= "Theme";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
