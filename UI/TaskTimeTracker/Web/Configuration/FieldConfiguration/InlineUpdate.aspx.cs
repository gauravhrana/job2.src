using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Configuration.FieldConfiguration
{
    public partial class InlineUpdate : PageInlineUpdate
	{
		
        #region Methods

		protected override DataTable GetData()
        {
            try
            {
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();

				var selectedrows = new List<FieldConfigurationDataModel>();
				var fieldConfigurationdata = new FieldConfigurationDataModel();

                if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						fieldConfigurationdata.FieldConfigurationId = entityKey;
						var result = FieldConfigurationDataManager.GetDetails(fieldConfigurationdata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
					}
                }
                else 
                {                    
					fieldConfigurationdata.FieldConfigurationId = SetId;
					var result = FieldConfigurationDataManager.GetDetails(fieldConfigurationdata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);

                }
                return selectedrows.ToDataTable();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }

		protected override string[] GetColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(PrimaryEntity, "DBColumns", SessionVariables.RequestProfile);
        }

		protected override void Update(Dictionary<string, string> values)
        {
            var data                      = new FieldConfigurationDataModel();

			data.FieldConfigurationId     = int.Parse(values[FieldConfigurationDataModel.DataColumns.FieldConfigurationId].ToString());
			data.FieldConfigurationModeId = int.Parse(values[FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId].ToString());
			data.Name                     = values[FieldConfigurationDataModel.DataColumns.Name].ToString();
			data.Value                    = values[FieldConfigurationDataModel.DataColumns.Value].ToString();
			data.HorizontalAlignment      = values[FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString();
			data.Formatting               = values[FieldConfigurationDataModel.DataColumns.Formatting].ToString();
			data.GridViewPriority         = int.Parse(values[FieldConfigurationDataModel.DataColumns.GridViewPriority].ToString());
			data.DetailsViewPriority      = int.Parse(values[FieldConfigurationDataModel.DataColumns.DetailsViewPriority].ToString());
			data.SystemEntityTypeId       = int.Parse(values[FieldConfigurationDataModel.DataColumns.SystemEntityTypeId].ToString());
            data.DisplayColumn            = int.Parse(values[FieldConfigurationDataModel.DataColumns.DisplayColumn].ToString());

            FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfiguration;
			PrimaryEntityKey = "FieldConfiguration";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}