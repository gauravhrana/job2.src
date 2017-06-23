using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

		#region properties

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

		public int? ReleaseLogId
		{
			get
			{
				if (txtReleaseLogId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtReleaseLogId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtReleaseLogId.Text);
				}
			}
			set
			{
				txtReleaseLogId.Text = (value == null) ? String.Empty : value.ToString();
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

		public int? ReleaseLogStatusId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtReleaseLogStatusId.Text.Trim());
				else
					return int.Parse(drpReleaseLogStatusList.SelectedItem.Value);
			}
			set
			{
				txtReleaseLogStatusId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string VersionNo
		{
			get
			{
				return txtVersionNo.Text;
			}
			set
			{
				txtVersionNo.Text = value ?? String.Empty;
			}
		}

		public DateTime? ReleaseDate
		{	
			get
			{
                return DateTimeHelper.FromUserDateFormatToDate(txtReleaseDate.Text.Trim());
			}
			set
			{
				txtReleaseDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);				
			}
		}

		public string Name
		{
			get
			{
				return txtName.Text;
			}
			set
			{
				txtName.Text = value ?? String.Empty;
			}
		}

		public string Description
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
			}
			set
			{
				txtDescription.InnerText = value ?? String.Empty;
			}
		}

		public int? SortOrder
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
			}
			set
			{
				txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ReleaseLogDataModel();

			data.ReleaseLogId		= ReleaseLogId;
			data.ApplicationId		= ApplicationId;
			data.ReleaseLogStatusId = ReleaseLogStatusId;
			data.Name				= Name;
			data.ReleaseDate		= ReleaseDate;
			data.Description		= Description;
			data.SortOrder			= SortOrder;
			data.VersionNo			= VersionNo;

			if (action == "Insert")
			{
				Framework.Components.ReleaseLog.ReleaseLogDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.ReleaseLog.ReleaseLogDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ReleaseLogID ?
			return ReleaseLogId;
		}

		public override void SetId(int setId, bool chkReleaseLogId)
		{
			ViewState["SetId"] = setId;

			//load data
			LoadData((int)ViewState["SetId"], chkReleaseLogId);
			txtReleaseLogId.Enabled = chkReleaseLogId;
			//txtDescription.Enabled = !chkReleaseLogId;
			//txtName.Enabled = chkReleaseLogId;
			//txtSortOrder.Enabled = !chkReleaseLogId;
		}

		public void LoadData(int releaseLogId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ReleaseLogDataModel();
			data.ReleaseLogId = releaseLogId;

			// get data
			var items = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			ReleaseLogId		= item.ReleaseLogId;
			ApplicationId		= item.ApplicationId;
			ReleaseLogStatusId	= item.ReleaseLogStatusId;
			ReleaseDate			= item.ReleaseDate;
			VersionNo			= item.VersionNo;
			Name				= item.Name;
			Description			= item.Description;
			SortOrder			= item.SortOrder;

			if (!showId)
			{
				txtReleaseLogId.Text = item.ReleaseLogId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, releaseLogId, PrimaryEntityKey);
			}
			else
			{
				txtReleaseLogId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ReleaseLogDataModel();

			ReleaseLogId		= data.ReleaseLogId;
			ApplicationId		= data.ApplicationId;
			ReleaseLogStatusId	= data.ReleaseLogStatusId;
			ReleaseDate			= data.ReleaseDate;
			VersionNo			= data.VersionNo;
			Description			= data.Description;
			Name				= data.Name;
			SortOrder			= data.SortOrder;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var ReleaseLogStatusData = Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(ReleaseLogStatusData, drpReleaseLogStatusList,
				StandardDataModel.StandardDataColumns.Name,
				ReleaseLogStatusDataModel.DataColumns.ReleaseLogStatusId);

			var applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(applicationdata, drpApplicationIdList,
								StandardDataModel.StandardDataColumns.Name,
								BaseDataModel.BaseDataColumns.ApplicationId);
			drpApplicationIdList.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();

			if (isTesting)
			{
				drpReleaseLogStatusList.AutoPostBack = true;
				drpApplicationIdList.AutoPostBack = true;

				if (drpReleaseLogStatusList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtReleaseLogStatusId.Text.Trim()))
					{
						drpReleaseLogStatusList.SelectedValue = txtReleaseLogStatusId.Text;
					}
					else
					{
						txtReleaseLogStatusId.Text = drpReleaseLogStatusList.SelectedItem.Value;
					}
				}

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

				txtReleaseLogStatusId.Visible = true;
				txtApplicationId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtReleaseLogStatusId.Text.Trim()))
				{
					drpReleaseLogStatusList.SelectedValue = txtReleaseLogStatusId.Text;
				}

				if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
				{
					drpApplicationIdList.SelectedValue = txtApplicationId.Text;
				}
			}
		}		

		public void LoadTabData(int releaseLogId)
		{
			LoadData(releaseLogId,false);
		}

		public void EnableDropDown()
		{
			var data = new ReleaseLogDataModel();
			data.ReleaseLogId = ReleaseLogId;
			var dt = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetChildren(data, SessionVariables.RequestProfile);
			if (dt.Tables.Count > 0)
			{
				foreach (DataTable dr in dt.Tables)
				{
					if (dr.Rows.Count > 0)
					{
						drpApplicationIdList.Enabled = false;
						txtApplicationId.Enabled = false;
					}
				}
			}
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtReleaseLogId.Visible = isTesting;
			lblReleaseLogId.Visible = isTesting;
			
			if (!IsPostBack)
			{				
				SetupDropdown();
                EnableDropDown();

                //CalendarExtenderReleaseDate.Format = SessionVariables.UserDateFormat;
                lblUserDateTimeFormat.Text = "Date Format: " + SessionVariables.UserDateFormat;
			}
		}		

		protected void drpApplicationIdList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationId.Text = drpApplicationIdList.SelectedItem.Value;
		}

		protected void drpReleaseLogStatusList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtReleaseLogStatusId.Text = drpReleaseLogStatusList.SelectedItem.Value;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLog;
			PrimaryEntityKey = "ReleaseLog";
			FolderLocationFromRoot = "ReleaseLog";

			// set object variable reference            
			PlaceHolderCore = dynReleaseLogId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion       

    }
}