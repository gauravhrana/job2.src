using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using System.Globalization;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? FieldConfigurationModeXApplicationRoleId
        {
            get
            {
                if (txtFieldConfigurationModeXApplicationRoleId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtFieldConfigurationModeXApplicationRoleId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtFieldConfigurationModeXApplicationRoleId.Text);
                }
            }
			set
			{
				txtFieldConfigurationModeXApplicationRoleId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? FieldConfigurationModeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFieldConfigurationModeId.Text.Trim());
                else
                    return int.Parse(drpFieldConfigurationModeList.SelectedItem.Value);
            }
			set
			{
				txtFieldConfigurationModeId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? ApplicationRoleId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationRoleId.Text.Trim());
                else
                    return int.Parse(drpApplicationRoleList.SelectedItem.Value);
            }
			set
			{
				txtApplicationRoleId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? FieldConfigurationModeAccessModeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFieldConfigurationModeAccessModeId.Text.Trim());
                else
                    return int.Parse(drpFieldConfigurationModeAccessModeList.SelectedItem.Value);
            }
            set
            {
                txtFieldConfigurationModeAccessModeId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }      

        #endregion properties

        #region private methods

		public override int? Save(string action)
		{
			var data                                      = new FieldConfigurationModeXApplicationRoleDataModel();

			data.FieldConfigurationModeXApplicationRoleId = FieldConfigurationModeXApplicationRoleId;
			data.FieldConfigurationModeId                 = FieldConfigurationModeId;
			data.ApplicationRoleId                        = ApplicationRoleId;
            data.FieldConfigurationModeAccessModeId       = FieldConfigurationModeAccessModeId;

			if (action == "Insert")
			{
				FieldConfigurationModeXApplicationRoleDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				FieldConfigurationModeXApplicationRoleDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.FieldConfigurationModeXApplicationRoleId;
		}

        public override void SetId(int setId, bool chkFieldConfigurationModeXApplicationRoleId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFieldConfigurationModeXApplicationRoleId);
            txtFieldConfigurationModeXApplicationRoleId.Enabled = chkFieldConfigurationModeXApplicationRoleId;
            //txtApplicationRoleId.Enabled = !chkFieldConfigurationModeXApplicationRoleId;
            //txtPersonId.Enabled = !chkFieldConfigurationModeXApplicationRoleId;
            //txtFieldConfigurationModeId.Enabled = !chkFieldConfigurationModeXApplicationRoleId;

            //drpPersonList.Enabled = !chkFieldConfigurationModeXApplicationRoleId;
            //drpApplicationRoleList.Enabled = !chkFieldConfigurationModeXApplicationRoleId;
            //drpFieldConfigurationModeList.Enabled = !chkFieldConfigurationModeXApplicationRoleId;
        }

        public void LoadData(int FieldConfigurationModeXApplicationRoleId, bool showId)
        {
            var data = new FieldConfigurationModeXApplicationRoleDataModel();
            data.FieldConfigurationModeXApplicationRoleId = FieldConfigurationModeXApplicationRoleId;

			var oFieldConfigurationModeXApplicationRoleTable = FieldConfigurationModeXApplicationRoleDataManager.GetEntityList(data, SessionVariables.RequestProfile);

			if (oFieldConfigurationModeXApplicationRoleTable.Count != 1) return;

			var item = oFieldConfigurationModeXApplicationRoleTable[0];			

	        if (!showId)
	        {
					FieldConfigurationModeXApplicationRoleId         = item.FieldConfigurationModeXApplicationRoleId.Value;					
					FieldConfigurationModeId                         = item.FieldConfigurationModeId;
					ApplicationRoleId                                = item.ApplicationRoleId;
                    FieldConfigurationModeAccessModeId               = item.FieldConfigurationModeAccessModeId;
                    dynAuditHistory.Visible                          = true;

                    txtFieldConfigurationModeXApplicationRoleId.Text = item.FieldConfigurationModeXApplicationRoleId.Value.ToString();					

                    // only show Audit History in case of Update page, not for Clone.
                    oHistoryList.Setup(PrimaryEntity, FieldConfigurationModeXApplicationRoleId, PrimaryEntityKey);
                    dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "FieldConfigurationModeXApplicationRole");
                }

                else
                {
                    txtFieldConfigurationModeXApplicationRoleId.Text  = String.Empty;
                }
                
                //drpApplicationRoleList.SelectedValue      = Convert.ToString(row[FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId]);
                //drpFieldConfigurationModeList.SelectedValue = Convert.ToString(row[FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId]);

				//oUpdateInfo.LoadText(oFieldConfigurationModeXApplicationRoleTable.Rows[0]);
			
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var fcModeData = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(fcModeData, drpFieldConfigurationModeList,
                StandardDataModel.StandardDataColumns.Name,
                FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);

			var applicationRoleData = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(applicationRoleData, drpApplicationRoleList, 
                StandardDataModel.StandardDataColumns.Name,
                ApplicationRoleDataModel.DataColumns.ApplicationRoleId);

			var fcModeAMData = FieldConfigurationModeAccessModeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(fcModeAMData, drpFieldConfigurationModeAccessModeList,
                StandardDataModel.StandardDataColumns.Name,
                FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId);

            if (isTesting)
            {
                drpApplicationRoleList.AutoPostBack                  = true;
                drpFieldConfigurationModeList.AutoPostBack           = true;
                drpFieldConfigurationModeAccessModeList.AutoPostBack = true;

                if (drpFieldConfigurationModeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFieldConfigurationModeId.Text.Trim()))
                    {
                        drpFieldConfigurationModeList.SelectedValue = txtFieldConfigurationModeId.Text;
                    }
                    else
                    {
                        txtFieldConfigurationModeId.Text = drpFieldConfigurationModeList.SelectedItem.Value;
                    }
                }

                if (drpApplicationRoleList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationRoleId.Text.Trim()))
                    {
                        drpApplicationRoleList.SelectedValue = txtApplicationRoleId.Text;
                    }
                    else
                    {
                        txtApplicationRoleId.Text = drpApplicationRoleList.SelectedItem.Value;
                    }
                }

                if (drpFieldConfigurationModeAccessModeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFieldConfigurationModeAccessModeId.Text.Trim()))
                    {
                        drpFieldConfigurationModeAccessModeList.SelectedValue = txtFieldConfigurationModeAccessModeId.Text;
                    }
                    else
                    {
                        txtFieldConfigurationModeAccessModeId.Text = drpFieldConfigurationModeAccessModeList.SelectedItem.Value;
                    }
                }

                txtFieldConfigurationModeId.Visible           = true;
                txtApplicationRoleId.Visible                  = true;
                txtFieldConfigurationModeAccessModeId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFieldConfigurationModeId.Text.Trim()))
                {
                    drpFieldConfigurationModeList.SelectedValue = txtFieldConfigurationModeId.Text;
                }
                if (!string.IsNullOrEmpty(txtApplicationRoleId.Text.Trim()))
                {
                    drpApplicationRoleList.SelectedValue = txtApplicationRoleId.Text;
                }
                if (!string.IsNullOrEmpty(txtFieldConfigurationModeAccessModeId.Text.Trim()))
                {
                    drpFieldConfigurationModeAccessModeList.SelectedValue = txtFieldConfigurationModeAccessModeId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtFieldConfigurationModeXApplicationRoleId.Visible = isTesting;
                lblFieldConfigurationModeXApplicationRoleId.Visible = isTesting;
                SetupDropdown();
            }
        }        

        protected void drpFieldConfigurationModeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFieldConfigurationModeId.Text = drpFieldConfigurationModeList.SelectedItem.Value;
        }

        protected void drpFieldConfigurationModeAccessModeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFieldConfigurationModeAccessModeId.Text = drpFieldConfigurationModeAccessModeList.SelectedItem.Value;
        }

        protected void drpApplicationRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationRoleId.Text = drpApplicationRoleList.SelectedItem.Value;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity                   = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;
			PrimaryEntityKey                = "FieldConfigurationModeXApplicationRole";
			FolderLocationFromRoot          = "FieldConfigurationModeXApplicationRole";

			// set object variable reference            
			PlaceHolderCore                 = dynFieldConfigurationModeXApplicationRoleId;
			PlaceHolderAuditHistory         = dynAuditHistory;
			BorderDiv                       = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
			
		}


        #endregion

    }
}