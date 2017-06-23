using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKey.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? SuperKeyId
        {
            get
            {
                if (txtSuperKeyId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSuperKeyId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtSuperKeyId.Text);
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

        public string Description
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
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

        public DateTime? ExpirationDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(txtExpirationDate.Text);

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/SystemIntegrity/SuperKey/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkApplicationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationId);
            txtSuperKeyId.Enabled = chkApplicationId;
            //txtDescription.Disabled = chkApplicationId;
            //txtName.Enabled = !chkApplicationId;
            //txtSortOrder.Enabled = !chkApplicationId;
            //txtApplicationId.Enabled = !chkApplicationId;
            //txtSystemEntityTypeId.Enabled = !chkApplicationId;
            //txtExpirationDate.Enabled = !chkApplicationId;
        }

        public void LoadData(int superKeyId, bool showId)
        {
            var data = new SuperKeyDataModel();
            data.SuperKeyId = superKeyId;
            var oDetail = Framework.Components.Core.SuperKeyDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {

                if (!showId)
                {

                    txtSuperKeyId.Text = oDetail.SuperKeyId.ToString();
                    oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.SuperKey, superKeyId, "SuperKey");
                    dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SuperKey");
                   
                }
                else
                {
                    txtSuperKeyId.Text = String.Empty;
                }

                txtDescription.InnerText			= oDetail.Description;
                txtName.Text						= oDetail.Name;
                txtSortOrder.Text					= oDetail.SortOrder.ToString();
                txtSystemEntityTypeId.Text			= oDetail.SystemEntityTypeId.ToString();
                drpSystemEntityType.SelectedValue	= oDetail.SystemEntityTypeId.ToString();
                txtExpirationDate.Text				= oDetail.ExpirationDate.ToString();

                oUpdateInfo.LoadText(oDetail);
			
			}
            else
            {
                txtDescription.InnerText = String.Empty;
                txtName.Text = String.Empty;
                txtSortOrder.Text = String.Empty;
                txtSystemEntityTypeId.Text = String.Empty;
                txtExpirationDate.Text = String.Empty;
            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var systemEntitydata = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(systemEntitydata, drpSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName,
                SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

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
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                {
                    drpSystemEntityType.SelectedValue = txtSystemEntityTypeId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            //var isTesting = SessionVariables.IsTesting;
            txtSuperKeyId.Visible = false;
            lblSuperKeyId.Visible = false;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }        

        protected void drpSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSystemEntityTypeId.Text = drpSystemEntityType.SelectedItem.Value;
        }

        #endregion

    }
}