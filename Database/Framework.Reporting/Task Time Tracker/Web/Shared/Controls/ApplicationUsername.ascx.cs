using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Framework.Components.ApplicationUser;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Controls
{
    public partial class ApplicationUserNameControl : BaseControl
    {

        #region Properties

        public int? ApplicationUserTitleId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                
				if (isTesting)
                    return int.Parse(txtApplicationUserTitle.Text.Trim());
                else
                    return int.Parse(drpApplicationUserTitle.SelectedItem.Value);
            }
        }

        public string ApplicationUserName
        {
            get
            {
                return txtApplicationUserName.Text;
            }
        }

		public string EmailAddress
		{
			get
			{
				return txtEmailAddress.Text;
			}
		}

        public string FirstName
        {
            get
            {
                return txtFirstName.Text;
            }
        }

        public string MiddleName
        {
            get
            {
                return txtMiddleName.Text;
            }
        }

        public string LastName
        {
            get
            {
                return txtLastName.Text;
            }
        }

        #endregion

        #region Methods

        public void LoadUsingRow(DataRow row)
        {
            try
            {
                txtApplicationUserName.Text           = row[ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
				txtEmailAddress.Text                  = row[ApplicationUserDataModel.DataColumns.EmailAddress].ToString();
                txtLastName.Text                      = row[ApplicationUserDataModel.DataColumns.LastName].ToString();
                txtFirstName.Text                     = row[ApplicationUserDataModel.DataColumns.FirstName].ToString();
                txtMiddleName.Text                    = row[ApplicationUserDataModel.DataColumns.MiddleName].ToString();

                drpApplicationUserTitle.SelectedValue = Convert.ToString(row[ApplicationUserDataModel.DataColumns.ApplicationUserTitleId]);
                txtApplicationUserTitle.Text          = Convert.ToString(row[ApplicationUserDataModel.DataColumns.ApplicationUserTitleId]);
            }
            catch { }
        }

		public void LoadApplicationUserName(ApplicationUserDataModel item)
		{
			try
			{
				txtApplicationUserName.Text           = item.ApplicationUserName;
				txtEmailAddress.Text                  = item.EmailAddress;
				txtLastName.Text                      = item.LastName;
				txtFirstName.Text                     = item.FirstName;
				txtMiddleName.Text                    = item.MiddleName;
				drpApplicationUserTitle.SelectedValue = item.ApplicationUserTitleId.ToString();
				txtApplicationUserTitle.Text          = item.ApplicationUserTitleId.ToString();
			}
			catch { }
		}

        public void LoadData(int applicationUserId)
        {
            var oData = new ApplicationUserDataModel();
            oData.ApplicationUserId = applicationUserId;

			var oApplicationUserTable = ApplicationUserDataManager.GetDetails(oData, SessionVariables.RequestProfile);

            if (oApplicationUserTable.Rows.Count == 1)
            {
                var row                               = oApplicationUserTable.Rows[0];   
                txtApplicationUserName.Text           = row[ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
				txtEmailAddress.Text                  = row[ApplicationUserDataModel.DataColumns.EmailAddress].ToString();
                txtLastName.Text                      = row[ApplicationUserDataModel.DataColumns.LastName].ToString();
                txtFirstName.Text                     = row[ApplicationUserDataModel.DataColumns.FirstName].ToString();
                txtMiddleName.Text                    = row[ApplicationUserDataModel.DataColumns.MiddleName].ToString();

                drpApplicationUserTitle.SelectedValue = Convert.ToString(row[ApplicationUserDataModel.DataColumns.ApplicationUserTitleId]);
                txtApplicationUserTitle.Text          = Convert.ToString(row[ApplicationUserDataModel.DataColumns.ApplicationUserTitleId]);
            }
            else
            {
                txtLastName.Text             = String.Empty;
                txtFirstName.Text            = String.Empty;
                txtMiddleName.Text           = String.Empty;
                txtApplicationUserTitle.Text = String.Empty;
				txtEmailAddress.Text         = string.Empty;
                txtApplicationUserName.Text  = String.Empty;
            }
        }

        private void SetupDropdown()
        {
			var applicationUserTitleData = ApplicationUserTitleDataManager.GetList(SessionVariables.RequestProfile);

            UIHelper.LoadDropDown(applicationUserTitleData, drpApplicationUserTitle, StandardDataModel.StandardDataColumns.Name, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId);

            if (SessionVariables.IsTesting)
            {
                drpApplicationUserTitle.AutoPostBack = true;                

                if (drpApplicationUserTitle.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationUserTitle.Text.Trim()))
                    {
                        drpApplicationUserTitle.SelectedValue = txtApplicationUserTitle.Text;
                    }
                    else
                    {
                        txtApplicationUserTitle.Text = drpApplicationUserTitle.SelectedItem.Value;
                    }
                }

                txtApplicationUserTitle.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplicationUserTitle.Text.Trim()))
                {
                    drpApplicationUserTitle.SelectedValue = txtApplicationUserTitle.Text;
                }
            }
        }

        #endregion

        #region Events

        protected void drpApplicationUserTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationUserTitle.Text = drpApplicationUserTitle.SelectedItem.Value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        #endregion

    }
}