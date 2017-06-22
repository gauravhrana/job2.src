using System;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Audit;

namespace Shared.UI.Web.ApplicationManagement.Development.TestData.BrokenHistoryControls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        bool isSearch = false;

        public DataModel.Framework.Audit.AuditHistory SearchParameters
        {
            get
            {
                var data = new DataModel.Framework.Audit.AuditHistory();

                if (isSearch || !chkSearchOff.Checked)
                {
                    if (drpSearchConditionSystemEntityType.SelectedValue != "-1")
                        data.SystemEntityId = Convert.ToInt32(drpSearchConditionSystemEntityType.SelectedValue);

                    if (drpSearchConditionTypeOfIssue.SelectedValue != "-1")
                        data.TypeOfIssue = drpSearchConditionTypeOfIssue.SelectedValue;
                }
                return data;
            }
        }

        protected void chkSearchOff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearchOff.Checked)
            {
                drpSearchConditionSystemEntityType.AutoPostBack = false;
                RaiseSearch();

            }
            else
            {
                drpSearchConditionSystemEntityType.AutoPostBack = true;
                RaiseSearch();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var typeOfIssueData = Framework.Components.Audit.TypeOfIssueDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(typeOfIssueData, drpSearchConditionTypeOfIssue,
                    StandardDataModel.StandardDataColumns.Description, StandardDataModel.StandardDataColumns.Name);

                var systemEntityData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(systemEntityData, drpSearchConditionSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
            }
        }

        protected void drpSearchConditionSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaiseSearch();
        }

        protected void drpSearchConditionTypeOfIssue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chkSearchOff.Checked)
            {
                RaiseSearch();
            }

            if (drpSearchConditionTypeOfIssue.SelectedValue == "InvalidEntityKey")
            {
                rowEntityType.Visible = true;
            }
            else
            {
                rowEntityType.Visible = false;
            }
        }

    }
}