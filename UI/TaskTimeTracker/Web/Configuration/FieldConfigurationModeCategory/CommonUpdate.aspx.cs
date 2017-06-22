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

namespace Shared.UI.Web.Configuration.FieldConfigurationModeCategory
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new FieldConfigurationModeCategoryDataModel();

			var UpdatedData = new List<FieldConfigurationModeCategoryDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.FieldConfigurationModeCategoryId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId].ToString());
				data.Name = SelectedData.Rows[i][FieldConfigurationModeCategoryDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeCategoryDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeCategoryDataModel.DataColumns.Description) : SelectedData.Rows[i][FieldConfigurationModeCategoryDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeCategoryDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeCategoryDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][FieldConfigurationModeCategoryDataModel.DataColumns.SortOrder].ToString());

				FieldConfigurationModeCategoryDataManager.Update(data, SessionVariables.RequestProfile);

				data = new FieldConfigurationModeCategoryDataModel();

				data.FieldConfigurationModeCategoryId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId].ToString());

				var dt = FieldConfigurationModeCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new FieldConfigurationModeCategoryDataModel();
			data.FieldConfigurationModeCategoryId = entityKey;
			var results = FieldConfigurationModeCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.FieldConfigurationModeCategory;		
				PrimaryEntityKey	= "FieldConfigurationModeCategory";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
