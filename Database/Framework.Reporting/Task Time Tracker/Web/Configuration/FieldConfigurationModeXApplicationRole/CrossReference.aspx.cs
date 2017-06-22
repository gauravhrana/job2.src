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

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole
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

        private DataTable GetFieldConfigurationModeList()
        {
            var dt = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetApplicationRoleList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetFieldConfigurationModeAccessModeList()
        {
            var dt = FieldConfigurationModeAccessModeDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedRecords(int fieldConfigurationModeId)
        {
            var data = new FieldConfigurationModeXApplicationRoleDataModel();
            data.FieldConfigurationModeId = fieldConfigurationModeId;
            var dt = FieldConfigurationModeXApplicationRoleDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private void RemoveByFieldConfigurationMode(int fieldConfigurationModeId)
        {
            var data = new FieldConfigurationModeXApplicationRoleDataModel();
            data.FieldConfigurationModeId = fieldConfigurationModeId;
            FieldConfigurationModeXApplicationRoleDataManager.Delete(data, SessionVariables.RequestProfile);
        }

        private void SaveFieldConfigurationModeXApplicationRole(int fieldConfigurationModeId, int ApplicationRoleId, int fieldConfigurationModeAccessModeId)
        {
            var data                                = new FieldConfigurationModeXApplicationRoleDataModel();
            data.FieldConfigurationModeId           = fieldConfigurationModeId;
            data.ApplicationRoleId                  = ApplicationRoleId;
            data.FieldConfigurationModeAccessModeId = fieldConfigurationModeAccessModeId;

            FieldConfigurationModeXApplicationRoleDataManager.Create(data, SessionVariables.RequestProfile);
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
                fcModeBucket.ConfigureBucket("ApplicationRole", "FieldConfigurationModeAccessMode", "Name", "Name", SetId,
                    GetApplicationRoleList, GetFieldConfigurationModeAccessModeList, GetAssociatedRecords, SaveFieldConfigurationModeXApplicationRole, RemoveByFieldConfigurationMode);
                       
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        protected void drpFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetId = Convert.ToInt32(drpFieldConfigurationMode.SelectedValue);
            fcModeBucket.ConfigureBucket("ApplicationRole", "FieldConfigurationModeAccessMode", "Name", "Name", SetId,
                GetApplicationRoleList, GetFieldConfigurationModeAccessModeList, GetAssociatedRecords, SaveFieldConfigurationModeXApplicationRole, RemoveByFieldConfigurationMode);                   
        }

        #endregion

    }
}