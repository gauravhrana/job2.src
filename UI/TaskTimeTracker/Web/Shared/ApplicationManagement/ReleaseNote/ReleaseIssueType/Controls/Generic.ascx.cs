using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {
        #region properties       

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
       
        #endregion properties

        #region private methods

		public override int? Save(string action)
		{
			var data = new ReleaseIssueTypeDataModel();

			data.ReleaseIssueTypeId = SystemKeyId;
			data.ApplicationId		= ApplicationId;
			data.Name				= Name;
			data.Description		= Description;
			data.SortOrder			= SortOrder;

			if (action == "Insert")
			{
				if(!Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ReleaseIssueTypeID ?
			return data.ReleaseIssueTypeId;
		}

        public override void SetId(int setId, bool chkReleaseIssueTypeId)
        {
            ViewState["SetId"] = setId;

            //load data
            LoadData((int)ViewState["SetId"], chkReleaseIssueTypeId);
            txtReleaseIssueTypeId.Enabled = chkReleaseIssueTypeId;
            //txtDescription.Enabled = !chkReleaseIssueTypeId;
            //txtName.Enabled = chkReleaseIssueTypeId;
            //txtSortOrder.Enabled = !chkReleaseIssueTypeId;
        }

        public void LoadData(int releaseIssueTypeId, bool showId)
        {
			// clear UI				
			Clear();

			// set up parameters
			var data = new ReleaseIssueTypeDataModel();
			data.ReleaseIssueTypeId = releaseIssueTypeId;

			// get data
			var items = Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId		= item.ReleaseIssueTypeId;
				ApplicationId	= item.ApplicationId;
				drpApplicationIdList.Enabled = false;
				txtApplicationId.Enabled = false;
				oHistoryList.Setup(PrimaryEntity, releaseIssueTypeId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
        }

		protected override void Clear()
		{
			base.Clear();

			var data = new ReleaseIssueTypeDataModel();
			
			SetData(data);

		}

		public void SetData(ReleaseIssueTypeDataModel data)
		{
			SystemKeyId = data.ReleaseIssueTypeId;

			base.SetData(data);
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(applicationdata, drpApplicationIdList,
								StandardDataModel.StandardDataColumns.Name,
								BaseDataModel.BaseDataColumns.ApplicationId);
			drpApplicationIdList.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();

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
				
				txtApplicationId.Visible = true;
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

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtReleaseIssueTypeId.Visible = isTesting;
            lblReleaseIssueTypeId.Visible = isTesting;
			if (!IsPostBack)
			{				
				SetupDropdown();
			}
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseIssueType;
			PrimaryEntityKey = "ReleaseIssueType";
			FolderLocationFromRoot = "ReleaseIssueType";

			// set object variable reference            
			PlaceHolderCore = dynReleaseIssueTypeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey			= txtReleaseIssueTypeId;
			CoreControlName			= txtName;
			CoreControlDescription	= txtDescription;
			CoreControlSortOrder	= txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		protected void drpApplicationIdList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationId.Text = drpApplicationIdList.SelectedItem.Value;
		}

        #endregion
    }
}