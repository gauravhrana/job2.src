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

namespace Shared.UI.Web.Configuration.FieldConfigurationMode
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new FieldConfigurationModeDataModel();

			var UpdatedData = new List<FieldConfigurationModeDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.FieldConfigurationModeId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString());
				data.Name = SelectedData.Rows[i][FieldConfigurationModeDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeDataModel.DataColumns.Description) : SelectedData.Rows[i][FieldConfigurationModeDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][FieldConfigurationModeDataModel.DataColumns.SortOrder].ToString());

				FieldConfigurationModeDataManager.Update(data, SessionVariables.RequestProfile);

				data = new FieldConfigurationModeDataModel();

				data.FieldConfigurationModeId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString());

				var dt = FieldConfigurationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new FieldConfigurationModeDataModel();
			data.FieldConfigurationModeId = entityKey;
			var results = FieldConfigurationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.FieldConfigurationMode;		
				PrimaryEntityKey	= "FieldConfigurationMode";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
