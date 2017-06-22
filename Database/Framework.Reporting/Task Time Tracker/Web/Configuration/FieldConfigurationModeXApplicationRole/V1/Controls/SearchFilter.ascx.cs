using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {
        #region variables               

        public FieldConfigurationModeXApplicationRoleDataModel SearchParameters
        {
            get
            {
                var data = new FieldConfigurationModeXApplicationRoleDataModel();

                if (SearchParametersRepeater.Items.Count != 0)
                {
                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                        FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId + "Visibility", SettingCategory))
                    {   
                        if(!CheckAndGetFieldValue(
                            FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId).ToString().Equals("-1"))				
                        {
                            data.FieldConfigurationModeId = Convert.ToInt32(
						        CheckAndGetFieldValue(FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId));
                        }
                    }
                      
                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                       FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId + "Visibility", SettingCategory))
                    {  
                       if( !CheckAndGetFieldValue(
					       FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId).ToString().Equals("-1"))				
                        {
                            data.ApplicationRoleId = Convert.ToInt32(CheckAndGetFieldValue(
                               FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId));
                        }                        
                    }

                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                        FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessModeId + "Visibility", SettingCategory))
                    {
                        if (!CheckAndGetFieldValue(
                            FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessModeId).ToString().Equals("-1"))
                        {
                            data.FieldConfigurationModeId = Convert.ToInt32(
                                CheckAndGetFieldValue(FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessModeId));
                        }
                    }

                }            

                return data;
            }
        }

        #endregion

        #region methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("FieldConfigurationModeId"))
			{
				var FieldConfigurationModedata = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(FieldConfigurationModedata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
                    FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);
			}
			else if (fieldName.Equals("ApplicationRoleId"))
			{
				var ApplicationRoledata = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(ApplicationRoledata, dropDownListControl,
                    StandardDataModel.StandardDataColumns.Name,
                    ApplicationRoleDataModel.DataColumns.ApplicationRoleId);
			}
            else if (fieldName.Equals("FieldConfigurationModeAccessModeId"))
            {
                var FieldConfigurationModeAccessModedata = FieldConfigurationModeAccessModeDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(FieldConfigurationModeAccessModedata, dropDownListControl,
                    StandardDataModel.StandardDataColumns.Name,
                    FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId);
            }
		}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey             = "FieldConfigurationModeXApplicationRole";
			FolderLocationFromRoot       = "FieldConfigurationModeXApplicationRole";
			PrimaryEntity                = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;

			SearchActionBarCore          = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

        #endregion

    }
}