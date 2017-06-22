using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.SystemIntegrity.QuickPaginationRun.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? QuickPaginationRunId
        {
            get
            {
                if (txtQuickPaginationRunId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtQuickPaginationRunId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtQuickPaginationRunId.Text);
                }
            }
        }

        public string SortClause
        {
            get
            {
                return txtSortClause.Text;
            }
        }

        public string WhereClause
        {
            get
            {
                return txtWhereClause.Text;
            }
        }

        public int ApplicationUserId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationUserId.Text.Trim());
                else
                    return int.Parse(drpApplicationUser.SelectedItem.Value);
            }
        }

        public int? SystemEntityTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtSystemEntityTypeId.Text.Trim());
                else
                    return int.Parse(drpSystemEntityType.SelectedItem.Value);
            }
        }

        public int? ExpirationTime
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtExpirationTime.Text);
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Shared/Admin/SystemIntegrity/QuickPaginationRun/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkApplicationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationId);
            txtQuickPaginationRunId.Enabled = chkApplicationId;
            //txtWhereClause.Disabled = chkApplicationId;
            //txtName.Enabled = !chkApplicationId;
            //txtApplicationUserId.Enabled = !chkApplicationId;
            //txtApplicationId.Enabled = !chkApplicationId;
            //txtSystemEntityTypeId.Enabled = !chkApplicationId;
            //txtExpirationTime.Enabled = !chkApplicationId;
        }

        public void LoadData(int QuickPaginationRunId, bool showId)
        {
            var data = new QuickPaginationRunDataModel();
            data.QuickPaginationRunId = QuickPaginationRunId;
			var oApplicationTable = Framework.Components.Core.QuickPaginationRunDatatManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oApplicationTable.Rows.Count == 1)
            {
                var row = oApplicationTable.Rows[0];

                if (!showId)
                {
                    txtQuickPaginationRunId.Text = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId]);
                    oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.QuickPaginationRun, QuickPaginationRunId, "QuickPaginationRun");
                    dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "QuickPaginationRun");
                   
                }
                else
                {
                    txtQuickPaginationRunId.Text = String.Empty;
                }

                txtWhereClause.Text			                = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.WhereClause]);
                txtSortClause.Text						    = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.SortClause]);
                txtApplicationUserId.Text					= Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.ApplicationUserId]);
                txtSystemEntityTypeId.Text			        = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.SystemEntityTypeId]);
                drpSystemEntityType.SelectedValue	        = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.SystemEntityTypeId]);
                txtExpirationTime.Text				        = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.ExpirationTime]);
                drpApplicationUser.SelectedValue            = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.ApplicationUserId]);

				oUpdateInfo.LoadText(row);
			
			}
            else
            {
                txtWhereClause.Text        = String.Empty;
                txtSortClause.Text         = String.Empty;
                txtApplicationUserId.Text  = String.Empty;
                txtSystemEntityTypeId.Text = String.Empty;
                txtExpirationTime.Text     = String.Empty;
            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var systemEntitydata = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(systemEntitydata, drpSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName,
                SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

			var applicationUserdata = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(applicationUserdata, drpApplicationUser, ApplicationUserDataModel.DataColumns.ApplicationUserName,
                ApplicationUserDataModel.DataColumns.ApplicationUserId);

            if (isTesting)
            {
                drpSystemEntityType.AutoPostBack = true;
                if (drpSystemEntityType.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                    {
                        drpSystemEntityType.SelectedValue = txtSystemEntityTypeId.Text;
                    }
                    else
                    {
                        txtSystemEntityTypeId.Text = drpSystemEntityType.SelectedItem.Value;
                    }
                }
                txtSystemEntityTypeId.Visible = true;
                drpApplicationUser.AutoPostBack = true;
                if (drpApplicationUser.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
                    {
                        drpApplicationUser.SelectedValue = txtApplicationUserId.Text;
                    }
                    else
                    {
                        txtApplicationUserId.Text = drpApplicationUser.SelectedItem.Value;
                    }
                }
                txtApplicationUserId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                {
                    drpSystemEntityType.SelectedValue = txtSystemEntityTypeId.Text;
                }
                if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
                {
                    drpApplicationUser.SelectedValue = txtApplicationUserId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            //var isTesting = SessionVariables.IsTesting;
            txtQuickPaginationRunId.Visible = false;
            lblQuickPaginationRunId.Visible = false;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        

        protected void drpSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSystemEntityTypeId.Text = drpSystemEntityType.SelectedItem.Value;
        }

        protected void drpApplicationUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationUserId.Text = drpApplicationUser.SelectedItem.Value;
        }

        #endregion

    }
}