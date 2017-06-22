using System;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.ApplicationManagement.Development.TestData.NoHistoryControls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables
        public int SystemEntityTypeId
        {
            get
            {
                return Convert.ToInt32(drpSearchConditionSystemEntityType.SelectedValue);
            }
        }

        #endregion

        #region private methods
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var domainData = Framework.Components.DataAccess.SetupConfiguration.GetListConnectionKeyName(SessionVariables.AuditId);
                UIHelper.LoadDropDown(domainData, drpSearchConditionDomain, "ConnectionKeyName", "ConnectionKeyName");

                var connectionKeyName = drpSearchConditionDomain.SelectedValue;
                var systemEntityData = Framework.Components.DataAccess.SetupConfiguration.Search(connectionKeyName, SessionVariables.AuditId);
                if (systemEntityData.Rows.Count > 0)
                {
                    drpSearchConditionSystemEntityType.Items.Clear();
                    //drpSearchConditionDomain.Items.Add(new ListItem("None", "-1"));
                }
                UIHelper.LoadDropDown(systemEntityData, drpSearchConditionSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
            }
        }

        protected void drpSearchConditionDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chkSearchOff.Checked)
            {
                RaiseSearch();
            }

            var connectionKeyName = drpSearchConditionDomain.SelectedValue;
            var systemEntityData = Framework.Components.DataAccess.SetupConfiguration.Search(connectionKeyName, SessionVariables.AuditId);
            if (systemEntityData.Rows.Count > 0)
            {
                drpSearchConditionSystemEntityType.Items.Clear();
                //drpSearchConditionDomain.Items.Add(new ListItem("None", "-1"));
            }
            UIHelper.LoadDropDown(systemEntityData, drpSearchConditionSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

        }

        protected void drpSearchConditionSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaiseSearch();
        }

        protected void chkSearchOff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearchOff.Checked)
            {
                drpSearchConditionSystemEntityType.AutoPostBack = false;
            }
            else
            {
                drpSearchConditionSystemEntityType.AutoPostBack = true;

            }
            RaiseSearch();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RaiseSearch();
        }

        #endregion

    }
}