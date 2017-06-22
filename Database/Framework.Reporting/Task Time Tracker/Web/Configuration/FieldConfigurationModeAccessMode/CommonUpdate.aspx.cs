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

namespace Shared.UI.Web.Configuration.FieldConfigurationModeAccessMode
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new FieldConfigurationModeAccessModeDataModel();
			UpdatedData = FieldConfigurationModeAccessModeDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FieldConfigurationModeAccessModeId =
					Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId].ToString());

				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				FieldConfigurationModeAccessModeDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FieldConfigurationModeAccessModeDataModel();
				
				data.FieldConfigurationModeAccessModeId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId].ToString());
				var dt = FieldConfigurationModeAccessModeDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}			
			}

			return UpdatedData;
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var fieldConfigurationModeAccessModedata = new FieldConfigurationModeAccessModeDataModel();
			fieldConfigurationModeAccessModedata.FieldConfigurationModeAccessModeId = entityKey;
			var results = FieldConfigurationModeAccessModeDataManager.Search(fieldConfigurationModeAccessModedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity          = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeAccessMode;
			PrimaryEntityKey       = "FieldConfigurationModeAccessMode";
			BreadCrumbObject       = Master.BreadCrumbObject;
		}

		#endregion

    }
}