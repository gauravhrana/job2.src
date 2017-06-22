using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Configuration.FieldConfiguration
{
    public partial class CommonUpdate : PageCommonUpdate
	{
		#region Method

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<FieldConfigurationDataModel>();
			var data = new FieldConfigurationDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FieldConfigurationId =
					Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.FieldConfigurationId].ToString());
				data.Name = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.Name))
					? CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.Name).ToString()
					: SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.Name].ToString();

				data.Value =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.Value))
					? CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.Value).ToString()
					: SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.Value].ToString();

				data.ControlType =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.ControlType))
					? CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.ControlType).ToString()
					: SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.ControlType].ToString();

				data.Formatting =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.Formatting))
					? CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.Formatting).ToString()
					: SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.Formatting].ToString();

				data.HorizontalAlignment =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.HorizontalAlignment))
					? CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.HorizontalAlignment).ToString()
					: SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString();

				data.FieldConfigurationModeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId).ToString())
					: int.Parse(SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId].ToString());

				data.SystemEntityTypeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.SystemEntityTypeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.SystemEntityTypeId).ToString())
					: int.Parse(SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.SystemEntityTypeId].ToString());

				data.GridViewPriority =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.GridViewPriority))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.GridViewPriority).ToString())
					: int.Parse(SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.GridViewPriority].ToString());

				data.DetailsViewPriority =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.DetailsViewPriority))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.DetailsViewPriority).ToString())
					: int.Parse(SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.DetailsViewPriority].ToString());

                data.DisplayColumn =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.DisplayColumn))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationDataModel.DataColumns.DisplayColumn).ToString())
                    : int.Parse(SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.DisplayColumn].ToString());

				FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FieldConfigurationDataModel();
				data.FieldConfigurationId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationDataModel.DataColumns.FieldConfigurationId].ToString());
				var dt = FieldConfigurationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
				
			}
			return UpdatedData.ToDataTable();
		}
        
        protected override DataTable GetEntityData(int? entityKey)
		{
			var fieldConfigurationdata = new FieldConfigurationDataModel();
			fieldConfigurationdata.FieldConfigurationId = entityKey;
			var results = FieldConfigurationDataManager.GetEntityDetails(fieldConfigurationdata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}
		
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfiguration;
			PrimaryEntityKey = "FieldConfiguration";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion	

    }
}