using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKeyDetail.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? SuperKeyDetailId
        {
            get
            {
                if (txtSuperKeyDetailId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSuperKeyDetailId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtSuperKeyDetailId.Text);
                }
            }
        }

        public int? SuperKeyId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtSuperKeyId.Text.Trim());
                else
                    return int.Parse(drpSuperKey.SelectedItem.Value);
            }
        }

        public int? EntityKey
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtEntityKey.Text);
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/SystemIntegrity/SuperKeyDetail/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkApplicationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationId);
            txtSuperKeyDetailId.Enabled = chkApplicationId;
            //txtDescription.Disabled = chkApplicationId;
            //txtName.Enabled = !chkApplicationId;
            //txtSortOrder.Enabled = !chkApplicationId;
            //txtApplicationId.Enabled = !chkApplicationId;
            //txtSuperKeyId.Enabled = !chkApplicationId;
            //txtEntityKey.Enabled = !chkApplicationId;
        }

        public void LoadData(int SuperKeyDetailId, bool showId)
        {
            var data = new SuperKeyDetailDataModel();
            data.SuperKeyDetailId = SuperKeyDetailId;
			var oDetail = Framework.Components.Core.SuperKeyDetailDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {

                if (!showId)
                {

                    txtSuperKeyDetailId.Text = oDetail.SuperKeyDetailId.ToString();
                    oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.SuperKeyDetail, SuperKeyDetailId, "SuperKeyDetail");
                    dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SuperKeyDetail");

                }
                else
                {
                    txtSuperKeyDetailId.Text = String.Empty;
                }
                                
                drpSuperKey.SelectedValue = oDetail.SuperKeyId.ToString();
                txtEntityKey.Text = oDetail.EntityKey.ToString();

				}
            else
            {
                txtSuperKeyId.Text = String.Empty;  
                txtEntityKey.Text = String.Empty;
            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var superKeydata = Framework.Components.Core.SuperKeyDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(superKeydata, drpSuperKey, StandardDataModel.StandardDataColumns.Name, SuperKeyDataModel.DataColumns.SuperKeyId);

            if (isTesting)
            {
                drpSuperKey.AutoPostBack = true;
                if (drpSuperKey.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtSuperKeyId.Text.Trim()))
                    {
                        drpSuperKey.SelectedValue = txtSuperKeyId.Text;
                    }
                    else
                    {
                        txtSuperKeyId.Text = drpSuperKey.SelectedItem.Value;
                    }
                }
                txtSuperKeyId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSuperKeyId.Text.Trim()))
                {
                    drpSuperKey.SelectedValue = txtSuperKeyId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            //var isTesting = SessionVariables.IsTesting;
            txtSuperKeyDetailId.Visible = false;
            lblSuperKeyDetailId.Visible = false;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }
               

        protected void drpSuperKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSuperKeyId.Text = drpSuperKey.SelectedItem.Value;
        }

        #endregion

    }
}