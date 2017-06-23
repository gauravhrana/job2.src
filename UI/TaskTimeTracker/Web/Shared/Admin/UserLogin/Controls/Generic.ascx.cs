using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.LogAndTrace;
using Shared.WebCommon.UI.Web;
using System.Data;


namespace Shared.UI.Web.Admin.UserLogin.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

        #region properties		

		public int? UserLoginId
		{
			get
			{
				if (txtUserLoginId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtUserLoginId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtUserLoginId.Text);
				}
			}
            set
            {
                txtUserLoginId.Text = (value == null) ? String.Empty : value.ToString();
            }
		}

		public string UserName
		{
			get
			{
				return txtUserName.Text;
			}
            set
            {
                txtUserName.Text = value ?? String.Empty;
            }
		}

        public int? UserLoginStatusId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtUserLoginStatusId.Text.Trim());
                else
                    return int.Parse(drpUserLoginStatusList.SelectedItem.Value);
            }
            set
            {
                txtUserLoginStatusId.Text = (value == null) ? String.Empty : value.ToString();
            }

        }
		
		public DateTime? RecordDate
		{
			get
			{
				return DateTime.ParseExact(txtRecordDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
			}
			set
			{
				txtRecordDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
			}

		} 

		protected override string ValidationConfigFile
		{
			get
			{
				return Server.MapPath("~/Shared/Admin/UserLogin/Controls/Validation.xml"); //"R:\UserLogins\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
			}
		}

		#endregion properties

        #region private methods


        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var UserLoginStatusData = Framework.Components.LogAndTrace.UserLoginStatusDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(UserLoginStatusData, drpUserLoginStatusList, StandardDataModel.StandardDataColumns.Name,
                UserLoginStatusDataModel.DataColumns.UserLoginStatusId);

            if (isTesting)
            {
                drpUserLoginStatusList.AutoPostBack = true;
                if (drpUserLoginStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtUserLoginStatusId.Text.Trim()))
                    {
                        drpUserLoginStatusList.SelectedValue = txtUserLoginStatusId.Text;
                    }
                    else
                    {
                        txtUserLoginStatusId.Text = drpUserLoginStatusList.SelectedItem.Value;
                    }
                }
                txtUserLoginStatusId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtUserLoginStatusId.Text.Trim()))
                {
                    drpUserLoginStatusList.SelectedValue = txtUserLoginStatusId.Text;
                }
            }
        }

		public override void SetId(int setId, bool chkUserLoginId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkUserLoginId);
			txtUserLoginId.Enabled = chkUserLoginId;
			//txtUserLoginStatusId.Enabled = !chkUserLoginId;
			//txtName.Enabled = !chkUserLoginId;
			//txtRecordDate.Enabled = !chkUserLoginId;
		}

        public void LoadData(int userLoginId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new Framework.Components.LogAndTrace.UserLoginDataModel();
            data.UserLoginId = userLoginId;

            // get data
			var items = Framework.Components.LogAndTrace.UserLoginDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtUserName.Text = item.UserName.ToString();
            txtUserLoginStatusId.Text = item.UserLoginStatusId.ToString();
            txtRecordDate.Text = item.RecordDate.ToString();

            if (!showId)
            {
                txtUserLoginId.Text = item.UserLoginId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, userLoginId, PrimaryEntityKey);
            }
            else
            {
                txtUserLoginId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new Framework.Components.LogAndTrace.UserLoginDataModel();

            UserLoginId = data.UserLoginId;
            UserName = data.UserName;
            UserLoginStatusId = data.UserLoginStatusId;
            RecordDate = data.RecordDate;

        }

		#endregion

        #region Events

        protected void drpUserLoginStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserLoginStatusId.Text = drpUserLoginStatusList.SelectedItem.Value;
        }

		protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetupDropdown();
			var isTesting = SessionVariables.IsTesting;
			txtUserLoginId.Visible = isTesting;
			lblUserLoginId.Visible = isTesting;
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "UserLogin";
            FolderLocationFromRoot = "/Shared/Admin";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLogin;

            // set object variable reference            
            PlaceHolderCore = dynUserLoginId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

		#endregion

	}
}