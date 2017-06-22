using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.HelpPage.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables        

        public HelpPageDataModel SearchParameters
        {
            get
            {
                var data = new HelpPageDataModel();

                data.Name = UIHelper.RefineAndGetSearchText(txtSearchConditionName.Text, SettingCategory);
                if (drpSearchConditionSystemEntityType.SelectedValue != "-1")
                {
                    data.SystemEntityTypeId = Convert.ToInt32(drpSearchConditionSystemEntityType.SelectedValue);
                }
                if (drpSearchConditionHelpPageContext.SelectedValue != "-1")
                {
                    data.HelpPageContextId = Convert.ToInt32(drpSearchConditionHelpPageContext.SelectedValue);
                }

                return data;
            }
        }

        #endregion

        #region private methods
        protected override void GetSettings()
        {
			txtSearchConditionName.Text = PerferenceUtility.GetUserPreferenceByKey(HelpPageDataModel.DataColumns.Name, SettingCategory);
            drpSearchConditionHelpPageContext.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(HelpPageContexDataModel.DataColumns.HelpPageContextId, SettingCategory);
            drpSearchConditionSystemEntityType.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId, SettingCategory);
        }

        private void SetDefaultValues()
        {
            txtSearchConditionName.Text = String.Empty;            
            drpSearchConditionSystemEntityType.SelectedIndex = 0;
            drpSearchConditionHelpPageContext.SelectedIndex = 0;
        }

        protected override void SaveSettings()
        {
            PerferenceUtility.UpdateUserPreference
                (
                    SettingCategory,
                    SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId,
                    drpSearchConditionSystemEntityType.SelectedValue
                 );
            PerferenceUtility.UpdateUserPreference
            (
                  SettingCategory
                , HelpPageContexDataModel.DataColumns.HelpPageContextId
                , drpSearchConditionHelpPageContext.SelectedValue
            );

            

            PerferenceUtility.UpdateUserPreference
            (
                  SettingCategory
                , HelpPageDataModel.DataColumns.Name
                , txtSearchConditionName.Text
            );
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SettingCategory))
                {
                    PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(SettingCategory, "Search Control Name");
                }
                else
                {
                    throw new Exception("Search control is not named");
                }


				var helpPageContextdata = Framework.Components.Core.HelpPageContextDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(helpPageContextdata, drpSearchConditionHelpPageContext, HelpPageContexDataModel.DataColumns.HelpPageContextId,
                    HelpPageContexDataModel.DataColumns.HelpPageContextId);

				var systemEntityData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(systemEntityData, drpSearchConditionSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

                GetSettings();
                RaiseSearch();
            }

            {
                oSearchActionBar.Setup("HelpPage");
            }

        }

        protected void drpSearchConditionHelpPageContext_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void drpSearchConditionSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        

        #endregion

    }
}
