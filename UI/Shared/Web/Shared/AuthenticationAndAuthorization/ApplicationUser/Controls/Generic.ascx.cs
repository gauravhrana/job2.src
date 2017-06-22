using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

        #region properties

        public int? ApplicationUserId
        {
            get
            {
                if (txtApplicationUserId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationUserId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtApplicationUserId.Text);
                }
            }
        }

		public int? ApplicationUserTitleId
		{
			get
			{
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationUserTitleId.Text.Trim());
                else
                    return int.Parse(drpApplicationUserTitleList.SelectedItem.Value);
			}
			
		}   

        public int? ApplicationId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationId.Text.Trim());
                else
                    return int.Parse(drpApplicationIdList.SelectedItem.Value);
            }
			set
			{
				txtApplicationId.Text = (value == null) ? String.Empty : value.ToString();
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
       

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ApplicationUserDataModel();

			data.ApplicationUserId		= ApplicationUserId;
			data.ApplicationUserName	= ApplicationUserName;
			data.EmailAddress           = EmailAddress;
			data.FirstName				= FirstName;
			data.MiddleName				= MiddleName;
			data.LastName				= LastName;
			data.ApplicationUserTitleId = ApplicationUserTitleId;
			data.ApplicationId			= ApplicationId;

			if (action == "Insert")
			{
				var dtApplicationUser = Framework.Components.ApplicationUser.ApplicationUserDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtApplicationUser.Rows.Count == 0)
				{
					Framework.Components.ApplicationUser.ApplicationUserDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.ApplicationUser.ApplicationUserDataManager.Update(data, SessionVariables.RequestProfile);
			}

            return ApplicationUserId;
		}

		public override void SetId(int setId, bool chkApplicationUserId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationUserId);
			txtApplicationUserId.Enabled = chkApplicationUserId;
			
		}

		public void LoadData(int applicationUserId, bool showId)
		{
			Clear();

			var dataQuery = new ApplicationUserDataModel();
			dataQuery.ApplicationUserId = applicationUserId;

			var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

           
            ApplicationId                  = item.ApplicationId;
            txtApplicationId.Text          = item.ApplicationId.ToString();
            txtLastName.Text               = item.LastName.ToString();
            txtFirstName.Text              = item.FirstName.ToString();
            txtMiddleName.Text             = item.MiddleName.ToString();
            txtEmailAddress.Text           = item.EmailAddress.ToString();
            txtApplicationUserName.Text    = item.ApplicationUserName.ToString();
            txtApplicationUserTitleId.Text = item.ApplicationUserTitleId.ToString(); 
           
           // txtApplicationUserName.LoadApplicationUserName(item);

			if (!showId)
			{
				txtApplicationUserId.Text = item.ApplicationUserId.ToString();

				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ApplicationUser, applicationUserId, "ApplicationUser");

			}
			else
			{
				txtApplicationUserId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationUserDataModel();

           
			ApplicationId		= data.ApplicationId;	
		
		}

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var Applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(Applicationdata, drpApplicationIdList,
                StandardDataModel.StandardDataColumns.Name,
                BaseDataModel.BaseDataColumns.ApplicationId);

            var ApplicationTitle = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(ApplicationTitle, drpApplicationUserTitleList,
                StandardDataModel.StandardDataColumns.Name,
                ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId);

         

            if (isTesting)
            {
                drpApplicationIdList.AutoPostBack = true;
                if (drpApplicationIdList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                    {
                        drpApplicationIdList.SelectedValue = txtApplicationId.Text;
                    }
                    else
                    {
                        txtApplicationId.Text = drpApplicationIdList.SelectedItem.Value;
                    }
                }
                txtApplicationId.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                {
                    drpApplicationIdList.SelectedValue = txtApplicationId.Text;
                }
            }
        }

		#endregion

        #region Events

        protected void drpApplicationIdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationId.Text = drpApplicationIdList.SelectedItem.Value;
        }

        protected void drpApplicationUserTitleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationUserTitleId.Text = drpApplicationUserTitleList.SelectedItem.Value;
        }             

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtApplicationUserId.Visible = true;
            if (!IsPostBack)
            {
                SetupDropdown();
            }				
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ApplicationUser";
			FolderLocationFromRoot = "ApplicationUser";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUser;

			// set object variable reference            
			PlaceHolderCore = dynApplicationUserId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}		

		#endregion

	}
}