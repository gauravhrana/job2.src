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

namespace Shared.UI.Web.Configuration.ThemeCategory
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ThemeCategoryDataModel();

			var UpdatedData = new List<ThemeCategoryDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.ThemeCategoryId = Convert.ToInt32(SelectedData.Rows[i][ThemeCategoryDataModel.DataColumns.ThemeCategoryId].ToString());
				data.Name = SelectedData.Rows[i][ThemeCategoryDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ThemeCategoryDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(ThemeCategoryDataModel.DataColumns.Description) : SelectedData.Rows[i][ThemeCategoryDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ThemeCategoryDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(ThemeCategoryDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][ThemeCategoryDataModel.DataColumns.SortOrder].ToString());

				ThemeCategoryDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ThemeCategoryDataModel();

				data.ThemeCategoryId = Convert.ToInt32(SelectedData.Rows[i][ThemeCategoryDataModel.DataColumns.ThemeCategoryId].ToString());

				var dt = ThemeCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ThemeCategoryDataModel();
			data.ThemeCategoryId = entityKey;
			var results = ThemeCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.ThemeCategory;		
				PrimaryEntityKey	= "ThemeCategory";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
