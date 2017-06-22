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

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogStatus.Controls
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
			var data = new ReleaseLogStatusDataModel();

			data.ReleaseLogStatusId = SystemKeyId;
			data.ApplicationId		= ApplicationId;
			data.Name				= Name;
			data.Description		= Description;
			data.SortOrder			= SortOrder;

			if (action == "Insert")
			{
				var dtReleaseLogStatus = Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtReleaseLogStatus.Rows.Count == 0)
				{
					Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ReleaseLogStatusID ?
			return data.ReleaseLogStatusId;
		}

		public override void SetId(int setId, bool chkReleaseLogStatusId)
		{
			ViewState["SetId"] = setId;

			//load data
			LoadData((int)ViewState["SetId"], chkReleaseLogStatusId);
			txtReleaseLogStatusId.Enabled = chkReleaseLogStatusId;
			//txtDescription.Enabled = !chkReleaseLogStatusId;
			//txtName.Enabled = chkReleaseLogStatusId;
			//txtSortOrder.Enabled = !chkReleaseLogStatusId;
		}

		public void LoadData(int releaseLogStatusId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ReleaseLogStatusDataModel();
			data.ReleaseLogStatusId = releaseLogStatusId;

			// get data
			var items = Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.ReleaseLogStatusId;
				ApplicationId = item.ApplicationId;
				drpApplicationIdList.Enabled = false;
				txtApplicationId.Enabled = false;
				oHistoryList.Setup(PrimaryEntity, releaseLogStatusId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ReleaseLogStatusDataModel();			

			SetData(data);

		}

		public void SetData(ReleaseLogStatusDataModel data)
		{
			SystemKeyId = data.ReleaseLogStatusId;

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
			txtReleaseLogStatusId.Visible = isTesting;
			lblReleaseLogStatusId.Visible = isTesting;
			if (!IsPostBack)
			{
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLogStatus;
			PrimaryEntityKey = "ReleaseLogStatus";
			FolderLocationFromRoot = "Shared/ApplicationManagement/ReleaseLogStatus";

			// set object variable reference            
			PlaceHolderCore = dynReleaseLogStatusId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey	= txtReleaseLogStatusId;
			CoreControlName = txtName;
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