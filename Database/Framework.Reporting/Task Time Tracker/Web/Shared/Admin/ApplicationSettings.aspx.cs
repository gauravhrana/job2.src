using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin
{
	public partial class ApplicationSettings : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Methods

        private void BindLists()
        {
            drpFCModeCategory.DataSource = FieldConfigurationModeCategoryDataManager.GetList(SessionVariables.RequestProfile);
            drpFCModeCategory.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpFCModeCategory.DataValueField = FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId;
            drpFCModeCategory.DataBind();
        }

        private DataTable GetControlsCategoryList()
        {
            var data = new UserPreferenceDataModel();
            data.UserPreferenceKey = ApplicationCommon.FieldConfigurationModeCategoryKey;
			var dt = UserPreferenceDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedControlsCategory(int fcModeCategoryId)
        {
            var id = Convert.ToInt32(drpFCModeCategory.SelectedValue);
            var data = new UserPreferenceDataModel();
            data.UserPreferenceKey = ApplicationCommon.FieldConfigurationModeCategoryKey;
            data.Value = id.ToString();
			var dt = UserPreferenceDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByFieldConfigurtionModeCategory(int fcModeCategoryId, List<int> upIds)
        {
            var id = Convert.ToInt32(drpFCModeCategory.SelectedValue);
            foreach (var upId in upIds)
            {
                var data = new UserPreferenceDataModel();
                data.Value = id.ToString();
                data.UserPreferenceId = upId;
				UserPreferenceDataManager.UpdateValueOnly(data, SessionVariables.RequestProfile);
            }
        }

        #endregion
		
        #region Events       

        protected override void OnInit(EventArgs e)
        {
            BindLists();
            BucketOfControlsCategory.ConfigureBucket("UserPreferenceCategory", 1, GetControlsCategoryList, GetAssociatedControlsCategory, SaveByFieldConfigurtionModeCategory, 
                UserPreferenceDataModel.DataColumns.UserPreferenceCategory,
                UserPreferenceDataModel.DataColumns.UserPreferenceId);

			SettingCategory = "ApplicationSettingsDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
        }

        protected void drpFCModeCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfControlsCategory.ReloadBucketList();			
		}

        #endregion

    }
}