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

namespace Shared.UI.Web.Configuration.MenuCategory
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new MenuCategoryDataModel();

			var UpdatedData = new List<MenuCategoryDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.MenuCategoryId = Convert.ToInt32(SelectedData.Rows[i][MenuCategoryDataModel.DataColumns.MenuCategoryId].ToString());
				data.Name = SelectedData.Rows[i][MenuCategoryDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(MenuCategoryDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(MenuCategoryDataModel.DataColumns.Description) : SelectedData.Rows[i][MenuCategoryDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(MenuCategoryDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(MenuCategoryDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][MenuCategoryDataModel.DataColumns.SortOrder].ToString());

				MenuCategoryDataManager.Update(data, SessionVariables.RequestProfile);

				data = new MenuCategoryDataModel();

				data.MenuCategoryId = Convert.ToInt32(SelectedData.Rows[i][MenuCategoryDataModel.DataColumns.MenuCategoryId].ToString());

				var dt = MenuCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new MenuCategoryDataModel();
			data.MenuCategoryId = entityKey;
			var results = MenuCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.MenuCategory;		
				PrimaryEntityKey	= "MenuCategory";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
