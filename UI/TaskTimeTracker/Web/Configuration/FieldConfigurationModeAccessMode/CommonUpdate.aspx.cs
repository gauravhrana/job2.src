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

namespace Shared.UI.Web.Configuration.FieldConfigurationModeAccessMode
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new FieldConfigurationModeAccessModeDataModel();

			var UpdatedData = new List<FieldConfigurationModeAccessModeDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.FieldConfigurationModeAccessModeId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId].ToString());
				data.Name = SelectedData.Rows[i][FieldConfigurationModeAccessModeDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeAccessModeDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeAccessModeDataModel.DataColumns.Description) : SelectedData.Rows[i][FieldConfigurationModeAccessModeDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeAccessModeDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeAccessModeDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][FieldConfigurationModeAccessModeDataModel.DataColumns.SortOrder].ToString());

				FieldConfigurationModeAccessModeDataManager.Update(data, SessionVariables.RequestProfile);

				data = new FieldConfigurationModeAccessModeDataModel();

				data.FieldConfigurationModeAccessModeId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId].ToString());

				var dt = FieldConfigurationModeAccessModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new FieldConfigurationModeAccessModeDataModel();
			data.FieldConfigurationModeAccessModeId = entityKey;
			var results = FieldConfigurationModeAccessModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.FieldConfigurationModeAccessMode;		
				PrimaryEntityKey	= "FieldConfigurationModeAccessMode";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
