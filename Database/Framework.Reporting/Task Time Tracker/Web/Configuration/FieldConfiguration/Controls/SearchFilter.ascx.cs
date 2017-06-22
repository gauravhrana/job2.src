using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.FieldConfiguration.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
		
        #region variables
		
		public FieldConfigurationDataModel SearchParameters
        {
            get
            {
				var data = new DataModel.Framework.Configuration.FieldConfigurationDataModel();
				SearchFilterControl.SetSearchParameters(data);
				CommonSearchParameters();

				//if (SearchParametersRepeater.Items.Count != 0)
				//{
				//	if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId).ToString()) != null)
				//		data.SystemEntityTypeId = int.Parse(CheckAndGetFieldValue(SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId).ToString());
					                    
				//	if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				//		FieldConfigurationDataModel.DataColumns.Name + "Visibility", SettingCategory))
				//	{
				//		if (!string.IsNullOrEmpty(CheckAndGetFieldValue(
				//			   FieldConfigurationDataModel.DataColumns.Name).ToString()))
				//		{
				//			data.Name = CheckAndGetFieldValue(
				//			   FieldConfigurationDataModel.DataColumns.Name).ToString();
				//		}
				//	}

				//	if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(ApplicationDataModel.DataColumns.ApplicationId).ToString()) != null)
				//		data.ApplicationId = int.Parse(CheckAndGetFieldValue(ApplicationDataModel.DataColumns.ApplicationId).ToString());
					

				//	if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				//		FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId + "Visibility", SettingCategory))
				//	{
				//		if (CheckAndGetFieldValue(FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId).ToString().Trim() != "-1")
				//		{
				//			data.FieldConfigurationModeId = Convert.ToInt32(CheckAndGetFieldValue(
				//			   FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId));
				//		}
				//	}

				//	GroupBy = GetParameterValue("GroupBy");

				//	SubGroupBy = GetParameterValue("SubGroupBy");             

				//}

                return data;
            }
			
        }

        #endregion

        #region methods

		//public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		//{
		//	base.LoadDropDownListSources(fieldName, dropDownListControl);

		//	if (fieldName.Equals("GroupBy") || fieldName.Equals("SubGroupBy"))
		//	{
		//	}


		//	if (fieldName.Equals(FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId))
		//	{
		//		var data = new FieldConfigurationModeDataModel();

		//		var fcModedata = FieldConfigurationModeDataManager.GetDetails(data, SessionVariables.RequestProfile);

		//		UIHelper.LoadDropDown(fcModedata, dropDownListControl,
		//			StandardDataModel.StandardDataColumns.Name,
		//			FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);
		//	}
		//}

		//public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		//{
		//	if (fieldName.Equals("SystemEntityTypeId"))
		//	{
		//		return AjaxHelper.GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId", plcControlHolder);
		//	}

		//	if (fieldName.Equals("ApplicationId"))
		//	{
		//		return AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", plcControlHolder);

		//	}

		//	return string.Empty;
		//}		

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;

			//PrimaryEntityKey = "FieldConfiguration";
			//FolderLocationFromRoot = "Shared/Configuration/FieldConfiguration";
			//PrimaryEntity = SystemEntity.FieldConfiguration;

			//SearchActionBarCore = oSearchActionBar;
			//SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion

    }
}