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
using DataModel.Framework.AuthenticationAndAuthorization;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationUser
{
	public partial class CrossReference : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Methods

        private void BindLists()
        {
            drpFieldConfigurationMode.DataSource = GetFieldConfigurationModeList();
            drpFieldConfigurationMode.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpFieldConfigurationMode.DataValueField = FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId;
            drpFieldConfigurationMode.DataBind();
        }

        private List<FieldConfigurationModeDataModel> GetFieldConfigurationModeList()
        {
            var dt = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private List<ApplicationUserDataModel> GetApplicationUserList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private List<FieldConfigurationModeAccessModeDataModel> GetFieldConfigurationModeAccessModeList()
        {
            var dt = FieldConfigurationModeAccessModeDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedRecords(int fieldConfigurationModeId)
        {
            var data = new FieldConfigurationModeXApplicationUserDataModel();
            data.FieldConfigurationModeId = fieldConfigurationModeId;
            var dt = FieldConfigurationModeXApplicationUserDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private void RemoveByFieldConfigurationMode(int fieldConfigurationModeId)
        {
            var data = new FieldConfigurationModeXApplicationUserDataModel();
            data.FieldConfigurationModeId = fieldConfigurationModeId;
            FieldConfigurationModeXApplicationUserDataManager.Delete(data, SessionVariables.RequestProfile);
        }

        private void SaveFieldConfigurationModeXApplicationUser(int fieldConfigurationModeId, int applicationUserId, int fieldConfigurationModeAccessModeId)
        {
            var data                                = new FieldConfigurationModeXApplicationUserDataModel();
            data.FieldConfigurationModeId           = fieldConfigurationModeId;
            data.ApplicationUserId                  = applicationUserId;
            data.FieldConfigurationModeAccessModeId = fieldConfigurationModeAccessModeId;

            FieldConfigurationModeXApplicationUserDataManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindLists();
                    SetId = Convert.ToInt32(drpFieldConfigurationMode.SelectedValue);
                }
                fcModeBucket.ConfigureBucket("ApplicationUser", "FieldConfigurationModeAccessMode", "ApplicationUserName", "Name", SetId,
                    GetApplicationUserList, GetFieldConfigurationModeAccessModeList, GetAssociatedRecords, SaveFieldConfigurationModeXApplicationUser, RemoveByFieldConfigurationMode);
                       
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        protected void drpFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetId = Convert.ToInt32(drpFieldConfigurationMode.SelectedValue);
            fcModeBucket.ConfigureBucket("ApplicationUser", "FieldConfigurationModeAccessMode", "ApplicationUserName", "Name", SetId,
                GetApplicationUserList, GetFieldConfigurationModeAccessModeList, GetAssociatedRecords, SaveFieldConfigurationModeXApplicationUser, RemoveByFieldConfigurationMode);                   
        }

        #endregion

    }
}