using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Configuration.ApplicationModeXFieldConfigurationMode
{
	public partial class ApplicationModeXFieldConfigurationModeView : Framework.UI.Web.BaseClasses.PageBasePage
    {
        #region Methods

        private List<FieldConfigurationModeDataModel> GetFieldConfigurationModeList()
        {
            var dt = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedFieldConfigurationModes(int ApplicationModeId)
        {
            var id = Convert.ToInt32(drpApplicationMode.SelectedValue);
            var dt = Framework.Components.UserPreference.ApplicationModeXFieldConfigurationModeDataManager.GetByApplicationMode(id, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByApplicationMode(int ApplicationModeId, List<int> FieldConfigurationModeIds)
        {
            var id = Convert.ToInt32(drpApplicationMode.SelectedValue);
            Framework.Components.UserPreference.ApplicationModeXFieldConfigurationModeDataManager.DeleteByApplicationMode(id, SessionVariables.RequestProfile);
            Framework.Components.UserPreference.ApplicationModeXFieldConfigurationModeDataManager.CreateByApplicationMode(id, FieldConfigurationModeIds.ToArray(), SessionVariables.RequestProfile);
        }

        private System.Collections.IEnumerable GetApplicationModeList()
        {
            var dt = Framework.Components.UserPreference.ApplicationModeDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }


        private void BindLists()
        {
            drpApplicationMode.DataSource = GetApplicationModeList();
            drpApplicationMode.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpApplicationMode.DataValueField = ApplicationModeDataModel.DataColumns.ApplicationModeId;
            drpApplicationMode.DataBind();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            BindLists();
            
            BucketOfApplicationMode.ConfigureBucket("FieldConfigurationMode", 1, GetFieldConfigurationModeList, GetAssociatedFieldConfigurationModes, SaveByApplicationMode);
        }


        protected void drpApplicationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfApplicationMode.ReloadBucketList();
        }



        #endregion
    }
}