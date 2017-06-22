using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;



namespace Shared.UI.Web.SystemDevNumbers.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? SystemDevNumbersId
        {
            get
            {
                if (txtSystemDevNumbersId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSystemDevNumbersId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtSystemDevNumbersId.Text);
                }
            }
        }

        public int? PersonId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtPersonId.Text.Trim());
                else
                    return int.Parse(drpPerson.SelectedItem.Value);
            }
        }

        public int RangeFrom
        {
            get
            {
                return Convert.ToInt32(txtRangeFrom.Text);
            }
        }

        public int RangeTo
        {
            get
            {
                return Convert.ToInt32(txtRangeTo.Text);
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkSystemDevNumbersId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkSystemDevNumbersId);
            txtSystemDevNumbersId.Enabled = chkSystemDevNumbersId;
            txtPersonId.Enabled = !chkSystemDevNumbersId;
            drpPerson.Enabled = !chkSystemDevNumbersId;
            //txtName.Enabled = !chkSystemDevNumbersId;
            //txtSortOrder.Enabled = !chkSystemDevNumbersId;
        }

        public void LoadData(int systemDevNumbersId, bool showId)
        {
            var data = new SystemDevNumbersDataModel();
            data.SystemDevNumbersId = systemDevNumbersId;
            var oDetail = Framework.Components.Core.SystemDevNumbersDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {

                if (!showId)
                {

                    txtSystemDevNumbersId.Text = oDetail.SystemDevNumbersId.ToString();

                    dynAuditHistory.Visible = true;
                    // only show Audit History in case of Update page, not for Clone.
                    oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.SystemDevNumbers, systemDevNumbersId, "SystemDevNumbers");
                    dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SystemDevNumbers");

                 
                }
                else
                {
                    txtSystemDevNumbersId.Text = String.Empty;
                }

                txtRangeFrom.Text		 = oDetail.RangeFrom.ToString();
                txtRangeTo.Text			 = oDetail.RangeTo.ToString();
                txtPersonId.Text		 = oDetail.PersonId.ToString();
                drpPerson.SelectedValue	 = oDetail.PersonId.ToString();
            }
            else
            {
                txtSystemDevNumbersId.Text = String.Empty;
                txtRangeFrom.Text = String.Empty;
                txtRangeTo.Text = String.Empty;
                txtPersonId.Text = String.Empty;
            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var persondata = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(persondata, drpPerson, ApplicationUserDataModel.DataColumns.FirstName,
                ApplicationUserDataModel.DataColumns.ApplicationUserId);

            if (isTesting)
            {
                drpPerson.AutoPostBack = true;
                if (drpPerson.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
                    {
                        drpPerson.SelectedValue = txtPersonId.Text;
                    }
                    else
                    {
                        txtPersonId.Text = drpPerson.SelectedItem.Value;
                    }
                }
                txtPersonId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
                {
                    drpPerson.SelectedValue = txtPersonId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtSystemDevNumbersId.Visible = isTesting;
            lblSystemDevNumbersId.Visible = isTesting;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        protected void drpPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPersonId.Text = drpPerson.SelectedItem.Value;
        }

        

        #endregion

    }
}