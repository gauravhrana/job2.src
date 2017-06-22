using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.HelpPage.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? HelpPageId
        {
            get
            {
                if (txtHelpPageId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtHelpPageId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtHelpPageId.Text);
                }
            }
        }

        public string Name
        {
            get
            {
                return txtName.Text;
            }
        }

        public string Content
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtHelpContent.Text);
            }
        }

        public int SortOrder
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
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

        public int? HelpPageContextId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtHelpPageContextId.Text.Trim());
                else
                    return int.Parse(drpHelpPageContext.SelectedItem.Value);
            }
        }



        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Shared/ApplicationManagement/HelpPage/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkApplicationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationId);
            txtHelpPageId.Enabled = chkApplicationId;
            //txtContent.Disabled = chkApplicationId;
            //txtName.Enabled = !chkApplicationId;
            //txtSortOrder.Enabled = !chkApplicationId;
            //txtApplicationId.Enabled = !chkApplicationId;
            //txtSystemEntityTypeId.Enabled = !chkApplicationId;
            //txtExpirationDate.Enabled = !chkApplicationId;
        }

        public void LoadData(int HelpPageId, bool showId)
        {
            var data = new HelpPageDataModel();
            data.HelpPageId = HelpPageId;
			var oApplicationTable = Framework.Components.Core.HelpPageDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oApplicationTable.Rows.Count == 1)
            {
                var row = oApplicationTable.Rows[0];

                if (!showId)
                {

                    txtHelpPageId.Text = Convert.ToString(row[HelpPageDataModel.DataColumns.HelpPageId]);
                    oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.HelpPage, HelpPageId, "HelpPage");
                    dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "HelpPage");                   
                }
                else
                {
                    txtHelpPageId.Text = String.Empty;
                }
                txtHelpContent.Text               = Convert.ToString(row[HelpPageDataModel.DataColumns.Content]);
                txtName.Text                      = Convert.ToString(row[HelpPageDataModel.DataColumns.Name]);
                txtSortOrder.Text                 = Convert.ToString(row[HelpPageDataModel.DataColumns.SortOrder]);
                txtSystemEntityTypeId.Text        = Convert.ToString(row[HelpPageDataModel.DataColumns.SystemEntityTypeId]);
                drpSystemEntityType.SelectedValue = Convert.ToString(row[HelpPageDataModel.DataColumns.SystemEntityTypeId]);
                drpHelpPageContext.SelectedValue  = Convert.ToString(row[HelpPageDataModel.DataColumns.HelpPageContextId]);
                txtHelpPageContextId.Text         = Convert.ToString(row[HelpPageDataModel.DataColumns.HelpPageContextId]);
               
				oUpdateInfo.LoadText(row);
            }
            else
            {
                txtHelpContent.Text        = String.Empty;
                txtName.Text               = String.Empty;
                txtSortOrder.Text          = String.Empty;
                txtSystemEntityTypeId.Text = String.Empty;
                txtHelpPageContextId.Text  = String.Empty;
            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var systemEntitydata = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(systemEntitydata, drpSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName,
                SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

			var helpPageContextdata = Framework.Components.Core.HelpPageContextDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(helpPageContextdata, drpHelpPageContext, StandardDataModel.StandardDataColumns.Name,
                HelpPageContextDataModel.DataColumns.HelpPageContextId);

            if (isTesting)
            {
                drpSystemEntityType.AutoPostBack = true;
                drpHelpPageContext.AutoPostBack = true;
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
                } if (drpHelpPageContext.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtHelpPageContextId.Text.Trim()))
                    {
                        drpHelpPageContext.SelectedValue = txtHelpPageContextId.Text;
                    }
                    else
                    {
                        txtHelpPageContextId.Text = drpHelpPageContext.SelectedItem.Value;
                    }
                }
                txtHelpPageContextId.Visible = true;
                txtSystemEntityTypeId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                {
                    drpSystemEntityType.SelectedValue = txtSystemEntityTypeId.Text;
                }
                if (!string.IsNullOrEmpty(txtHelpPageContextId.Text.Trim()))
                {
                    drpHelpPageContext.SelectedValue = txtHelpPageContextId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            //var isTesting = SessionVariables.IsTesting;
            txtHelpPageId.Visible = false;
            lblHelpPageId.Visible = false;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        

        protected void drpSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSystemEntityTypeId.Text = drpSystemEntityType.SelectedItem.Value;
        }

        protected void drpHelpPageContext_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHelpPageContextId.Text = drpHelpPageContext.SelectedItem.Value;
        }

        #endregion

    }
}
