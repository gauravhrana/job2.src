using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

using System.Data;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperation.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? ApplicationOperationId
        {
            get
            {
                if (txtApplicationOperationId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationOperationId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtApplicationOperationId.Text);
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

        public int ApplicationId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationId.Text.Trim());
                else
                    return int.Parse(drpApplicationList.SelectedItem.Value);
            }
        }

        public int SystemEntityTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtSystemEntityTypeId.Text.Trim());
                else
                    return int.Parse(drpSystemEntityTypeList.SelectedItem.Value);
            }
        }

        public string OperationValue
        {
            get
            {
                return txtOperationValue.Text;
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Shared/AuthenticationAndAuthorization/ApplicationOperation/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods
    
        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var applicationData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(applicationData, drpApplicationList,
				StandardDataModel.StandardDataColumns.Name,
				BaseDataModel.BaseDataColumns.ApplicationId);

			var systemEntityTypeData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(systemEntityTypeData, drpSystemEntityTypeList, 
                SystemEntityTypeDataModel.DataColumns.EntityName,
                SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

            if (isTesting)
            {
                drpApplicationList.AutoPostBack = true;
                drpSystemEntityTypeList.AutoPostBack = true;
                if (drpApplicationList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                    {
                        drpApplicationList.SelectedValue = txtApplicationId.Text;
                    }
                    else
                    {
                        txtApplicationId.Text = drpApplicationList.SelectedItem.Value;
                    }
                }
                if (drpSystemEntityTypeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                    {
                        drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
                    }
                    else
                    {
                        txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
                    }
                }
                txtApplicationId.Visible = true;
                txtSystemEntityTypeId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                {
                    drpApplicationList.SelectedValue = txtApplicationId.Text;
                }
                if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                {
                    drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
                }
                txtApplicationId.Visible = false;
                txtSystemEntityTypeId.Visible = false;
            }
        }

        public override void SetId(int setId, bool chkApplicationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationId);
            txtApplicationOperationId.Enabled = chkApplicationId;
            //txtDescription.Disabled = chkApplicationId;
            //txtName.Enabled = !chkApplicationId;
            //txtSortOrder.Enabled = !chkApplicationId;
            //txtApplicationId.Enabled = !chkApplicationId;
            //txtSystemEntityTypeId.Enabled = !chkApplicationId;
            //txtOperationValue.Enabled = !chkApplicationId;
        }

        public void LoadData(int applicationoperationid, bool showId)
        {
            var data = new ApplicationOperationDataModel();
            data.ApplicationOperationId = applicationoperationid;
			var oApplication = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oApplication != null)
            {
                if (!showId)
                {

                    txtApplicationOperationId.Text = Convert.ToString(oApplication.ApplicationOperationId);
                    oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ApplicationOperation, applicationoperationid, "ApplicationOperation");
                    dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ApplicationOperation");
                }
                else
                {
                    txtApplicationOperationId.Text = String.Empty;
                }
                txtDescription.InnerText              = oApplication.Description;
                txtName.Text                          = oApplication.Name;
                txtSortOrder.Text                     = oApplication.SortOrder.ToString();
                txtApplicationId.Text                 = oApplication.ApplicationId.ToString();
                txtSystemEntityTypeId.Text            = oApplication.SystemEntityTypeId.ToString();
                txtOperationValue.Text                = oApplication.OperationValue;
                drpApplicationList.SelectedValue      = oApplication.ApplicationId.ToString();
                drpSystemEntityTypeList.SelectedValue = oApplication.SystemEntityTypeId.ToString();

				oUpdateInfo.LoadText(oApplication);
            }
            else
            {
                txtApplicationId.Text		          = String.Empty;
                txtDescription.InnerText	          = String.Empty;
                txtName.Text				          = String.Empty;
                txtSortOrder.Text			          = String.Empty;
                txtApplicationId.Text		          = String.Empty;
                txtSystemEntityTypeId.Text	          = String.Empty;
                txtOperationValue.Text		          = String.Empty;
                drpSystemEntityTypeList.SelectedValue = "-1";
                drpApplicationList.SelectedValue      = "-1";
            }
        }

        public override int? Save(string action)
        {
            var data = new ApplicationOperationDataModel();

            data.ApplicationOperationId = ApplicationOperationId;
            data.Name                   = Name;
            data.Description            = Description;
            data.SortOrder              = SortOrder;
            data.ApplicationId          = ApplicationId;
            data.SystemEntityTypeId     = SystemEntityTypeId;
            data.OperationValue         = OperationValue;

            

            if (action == "Insert")
            {
				if(!Framework.Components.ApplicationUser.ApplicationOperationDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
					Framework.Components.ApplicationUser.ApplicationOperationDataManager.Create(data, SessionVariables.RequestProfile);                    
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.ApplicationUser.ApplicationOperationDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of ClientID ?
            return data.ApplicationOperationId;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               SetupDropdown();
            }
            var isTesting                     = SessionVariables.IsTesting;
            txtApplicationOperationId.Visible = isTesting;
            lblApplicationOperationId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey        = "ApplicationOperation";
            PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.ApplicationOperation;

            // set object variable reference            
            PlaceHolderCore         = dynApplicationOperationId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv               = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        protected void drpApplicationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationId.Text = drpApplicationList.SelectedItem.Value;
        }

        protected void drpSystemEntityTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
        }

        #endregion

    }
}