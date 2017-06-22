using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.UserPreference.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {

        #region variables

        public string CategoryLIKE
        {
            get
            {
                return GetParameterValue("CategoryLike");
            }
        }

        public UserPreferenceDataModel SearchParameters
        {
            get
            {
                var data = new UserPreferenceDataModel();

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.UserPreferenceDataType).ToString()) != null)
					data.DataTypeId = int.Parse(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.UserPreferenceDataType).ToString());

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.Application).ToString()) != null)
					data.ApplicationId = int.Parse(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.Application).ToString());

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.ApplicationUser).ToString()) != null)
					data.ApplicationUserId = int.Parse(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.ApplicationUser).ToString());

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.UserPreferenceKey).ToString()) != null)
					data.UserPreferenceKeyId = int.Parse(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.UserPreferenceKey).ToString());

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.UserPreferenceCategory).ToString()) != null)
					data.UserPreferenceCategoryId = int.Parse(CheckAndGetFieldValue(UserPreferenceDataModel.DataColumns.UserPreferenceCategory).ToString());
				
				if(!string.IsNullOrEmpty(GetParameterValue("Value")))
					data.Value = GetParameterValue("Value");

                GroupBy = GetParameterValue("GroupBy");

                SubGroupBy = GetParameterValue("SubGroupBy");

                return data;
            }
        }

        #endregion

        #region methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);
			//dropDownListControl.Width = 500;

			//if (fieldName.Equals("Application"))
			//{
			//	var applicationData = Framework.Components.ApplicationUser.Application.GetList(SessionVariables.RequestProfile);
			//	UIHelper.LoadDropDown(applicationData, dropDownListControl,
			//		ApplicationDataModel.DataColumns.Name,
			//		ApplicationDataModel.DataColumns.ApplicationId);

			//	dropDownListControl.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();
			//}
			//if (fieldName.Equals("UserPreferenceDataType"))
			//{
			//	var updtData = Framework.Components.UserPreference.UserPreferenceDataTypeDataManager.GetList(AuditId);
			//	updtData.DefaultView.Sort = UserPreferenceDataTypeDataModel.DataColumns.Name + " ASC";
			//	var sorteddt = updtData.DefaultView.ToTable();
			//	UIHelper.LoadDropDown(sorteddt, dropDownListControl, UserPreferenceDataTypeDataModel.DataColumns.Name,
			//		UserPreferenceDataTypeDataModel.DataColumns.UserPreferenceDataTypeId);
			//}
			//if (fieldName.Equals("UserPreferenceKey"))
			//{
			//	var upkData = Framework.Components.UserPreference.UserPreferenceKeyDataManager.GetList(AuditId);
			//	upkData.DefaultView.Sort = UserPreferenceKeyDataModel.DataColumns.Name + " ASC";
			//	var sorteddt = upkData.DefaultView.ToTable();
			//	UIHelper.LoadDropDown(sorteddt, dropDownListControl, UserPreferenceKeyDataModel.DataColumns.Name,
			//		UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId);
			//}

			//else if (fieldName.Equals("UserPreferenceCategory"))
			//{
			//	var upkData = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.GetList(AuditId);
			//	upkData.DefaultView.Sort = UserPreferenceCategoryDataModel.DataColumns.Name + " ASC";
			//	var sorteddt = upkData.DefaultView.ToTable();
			//	UIHelper.LoadDropDown(sorteddt, dropDownListControl, UserPreferenceCategoryDataModel.DataColumns.Name,
			//		UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId);
			//}

			//else if (fieldName.Equals("ApplicationUser"))
			//{
			//	var auData = Framework.Components.ApplicationUser.ApplicationUser.GetList(AuditId);
			//	 UIHelper.LoadDropDown(auData, dropDownListControl, DataModel.Framework.Application.ApplicationUserDataModel.DataColumns.FullName,
			//		DataModel.Framework.Application.ApplicationUserDataModel.DataColumns.ApplicationUserId);
			//}

			if (fieldName.Equals("GroupBy"))
			{

				dropDownListControl.Items.Add(new ListItem("Application", "Application"));
				dropDownListControl.Items.Add(new ListItem("User Preference Category", "UserPreferenceCategory"));
				dropDownListControl.Items.Add(new ListItem("User Preference DataType", "UserPreferenceDataType"));
				dropDownListControl.Items.Add(new ListItem("User Preference Key", "UserPreferenceKey"));
				dropDownListControl.Items.Add(new ListItem("Application User", "ApplicationUser"));
				dropDownListControl.Items.Add(new ListItem("Value", "Value"));
			}

			else if (fieldName.Equals("SubGroupBy"))
			{
				dropDownListControl.Items.Add(new ListItem("User Preference Category", "UserPreferenceCategory"));
				dropDownListControl.Items.Add(new ListItem("User Preference DataType", "UserPreferenceDataType"));
				dropDownListControl.Items.Add(new ListItem("User Preference Key", "UserPreferenceKey"));
				dropDownListControl.Items.Add(new ListItem("Application User", "ApplicationUser"));
				dropDownListControl.Items.Add(new ListItem("Value", "Value"));
			}
			//dropDownListControl.Width = 100;
			dropDownListControl.SelectedIndex = 0;
		}

		public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		{
			if ((!fieldName.Equals("CategoryLike")) && (!fieldName.Equals("Value")))
			{
				if (fieldName.Equals("ApplicationUser"))
				{
					return AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationUserList", "ApplicationUserName", "ApplicationUserId", plcControlHolder);
				}
				else
				{
					return AjaxHelper.GetKendoComboBoxConfigScript("Get" + fieldName + "List", "Name", "" + fieldName + "Id", plcControlHolder);
				}
			}

			return string.Empty;
		}
		
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.UserPreference;
            PrimaryEntityKey = "UserPreference";
            FolderLocationFromRoot = "/Shared/Configuration";

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion

    }
}