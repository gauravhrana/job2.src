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

namespace Shared.UI.Web.Configuration.FieldConfigurationModeCategoryXFCMode
{
	public partial class CrossReference : Framework.UI.Web.BaseClasses.PageBasePage
    {
        #region Methods

        private DataTable GetFieldConfigurationModeList()
        {
			var dt = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedFieldConfigurationMode(int FieldConfigurationModeCategoryId)
        {
            var id = Convert.ToInt32(drpFieldConfigurationModeCategory.SelectedValue);
            var dt = FieldConfigurationModeCategoryXFCModeDataManager.GetByFieldConfigurationModeCategory(id, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByFieldConfigurationModeCategory(int FieldConfigurationModeCategoryId, List<int> FieldConfigurationModeIds)
        {
            var id = Convert.ToInt32(drpFieldConfigurationModeCategory.SelectedValue);
            FieldConfigurationModeCategoryXFCModeDataManager.DeleteByFieldConfigurationModeCategory(id, SessionVariables.RequestProfile);
            FieldConfigurationModeCategoryXFCModeDataManager.CreateByFieldConfigurationModeCategory(id, FieldConfigurationModeIds.ToArray(), SessionVariables.RequestProfile);
        }

        private DataTable GetFieldConfigurationModeCategoryList()
        {
			var dt = FieldConfigurationModeCategoryDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }


        private void BindLists()
        {
            drpFieldConfigurationModeCategory.DataSource = GetFieldConfigurationModeCategoryList();
            drpFieldConfigurationModeCategory.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpFieldConfigurationModeCategory.DataValueField = FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId;
            drpFieldConfigurationModeCategory.DataBind();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            BindLists();

            //BucketOfFieldConfigurationModeCategory.ConfigureBucket("FieldConfigurationModeCategory", 1, 2, GetFieldConfigurationModeCategoryList, GetAssociatedFieldConfigurationModeCategorys, SaveByMenu);
            BucketOfFieldConfiguration.ConfigureBucket("FieldConfigurationMode", 1, GetFieldConfigurationModeList, GetAssociatedFieldConfigurationMode, SaveByFieldConfigurationModeCategory);
        }


        protected void drpFieldConfigurationModeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfFieldConfiguration.ReloadBucketList();
        }



        #endregion
    }
}