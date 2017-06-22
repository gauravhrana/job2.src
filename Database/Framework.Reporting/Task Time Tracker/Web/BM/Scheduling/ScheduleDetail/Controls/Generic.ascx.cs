using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Data;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Net.Mail;
using System.Configuration;
using System.Web.UI.WebControls;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using DataModel.TaskTimeTracker.TimeTracking;
using DataModel.TaskTimeTracker.Task;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Controls
{
	public partial class Generic : ControlGeneric
	{

		#region properties

		public int? ScheduleDetailId
		{
			get
			{
				if (txtScheduleDetailId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtScheduleDetailId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtScheduleDetailId.Text);
				}
			}			
			set
			{
				txtScheduleDetailId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ScheduleId
		{
			get
			{
				return int.Parse(txtScheduleId.Text.Trim());			
			}
			set
			{
				txtScheduleId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ScheduleDetailActivityCategoryId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtScheduleDetailActivityCategoryId.Text.Trim());
				else
					return int.Parse(drpScheduleDetailActivityCategoryList.SelectedItem.Value);	
			}
			set
			{
				txtScheduleDetailActivityCategoryId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public DateTime? InTime
		{
			get
			{
				DateTime dt = Convert.ToDateTime(txtInTime.Text.Trim());
				return DateTime.Parse(dt.ToString("t"), null);
			}
			set
			{
				txtInTime.Text = (value == null) ? String.Empty : value.Value.ToString("t");
			}
		}

		public DateTime? OutTime
		{
			get
			{
				DateTime dt = Convert.ToDateTime(txtOutTime.Text.Trim());
				return DateTime.Parse(dt.ToString("t"), null);
			}
			set
			{
				txtOutTime.Text = (value == null) ? String.Empty : value.Value.ToString("t");
			}
		}

        public string WorkTicket
        {
            get
            {
                return txtWorkTicket.Text;
            }
            set
            {
                txtWorkTicket.Text = value ?? String.Empty;
            }
        }

		public string Message
		{
			get
			{
				return txtMessage.Text;
			}
			set
			{
				txtMessage.Text = value ?? String.Empty;
			}
		}

        //public int? CreatedByAuditId
        //{
        //    get
        //    {
        //        return int.Parse(txtCreatedByAuditId.Text);
        //    }
        //    set
        //    {
        //        txtCreatedByAuditId.Text = (value == null) ? String.Empty : value.ToString();
        //    }
        //}

        //public int? ModifiedByAuditId
        //{
        //    get
        //    {
        //        return int.Parse(txtModifiedByAuditId.Text);
        //    }
        //    set
        //    {
        //        txtModifiedByAuditId.Text = (value == null) ? String.Empty : value.ToString();
        //    }
        //}

        //public DateTime? CreatedDate
        //{
        //    get
        //    {
        //        return DateTime.ParseExact(txtCreatedDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
        //        //return DateTime.Parse(txtCreatedDate.Text.Trim());
        //    }
        //    set
        //    {
				
        //        txtCreatedDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
        //    }
        //    //get
        //    //{
        //    //	//DateTime dt = Convert.ToDateTime(txtCreatedDate.Text.Trim());
        //    //	//return DateTime.ParseExact(dt.ToString(this.DateTimeFormat), this.DateTimeFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        //    //	DateTime dt = Convert.ToDateTime(txtCreatedDate.Text.Trim());
        //    //	return DateTime.Parse(dt.ToString("t"), null);
        //    //}
        //    //set
        //    //{
        //    //	txtCreatedDate.Text = (value == null) ? String.Empty : value.Value.ToString("t");				
        //    //}
        //}

        //public DateTime? ModifiedDate
        //{
        //    get
        //    {
        //        DateTime dt = Convert.ToDateTime(txtModifiedDate.Text.Trim());
        //        return DateTime.ParseExact(dt.ToString(SessionVariables.UserDateFormat), SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
        //    }
        //    set
        //    {
        //        txtModifiedDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
        //    }
        //}

		#endregion properties

		#region private method		

		private int? InsertOrUpdate(ScheduleDetailDataModel data, string action)
		{
			if (action == "Insert")
				{
					var dtScheduleDetail = ScheduleDetailDataManager.DoesExist(data, SessionVariables.RequestProfile);

					if (dtScheduleDetail.Rows.Count == 0)
					{
						ScheduleDetailId=ScheduleDetailDataManager.Create(data, SessionVariables.RequestProfile);
					}
					else
					{
						throw new Exception("Record with given ID already exists.");
					}
				}
				else
				{
                    //data.CreatedDate = CreatedDate;
					ScheduleDetailDataManager.Update(data, SessionVariables.RequestProfile);
				}
			return ScheduleDetailId;
		}

		public override int? Save(string action)
		{
			var data = new ScheduleDetailDataModel();

			data.ScheduleDetailId                 = ScheduleDetailId;
			data.ScheduleId                       = ScheduleId;
			data.InTime                           = InTime;
			data.OutTime                          = OutTime;
            data.WorkTicket                       = WorkTicket;
			data.Message                          = Message;
			data.ScheduleDetailActivityCategoryId = ScheduleDetailActivityCategoryId;

			//if (chkSendEmail.Checked)
			//{
			//	//Save Data & Send Email
			//	string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
			//	ScheduleDetailId = InsertOrUpdate(data, action);
			//	//SendMail(data.ScheduleId, txtEmailAddress.Text, txtCCAddress.Text, fromEmail, SessionVariables.RequestProfile);
			//}
			//else
			//{
			//	//Save Data Only
			//	ScheduleDetailId = InsertOrUpdate(data, action);
			//}
			//// not correct ... when doing insert, we didn't get/change the value of ScheduleID ?
			return ScheduleDetailId;
		}

		public override void SetId(int setId, bool chkScheduleDetailId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkScheduleDetailId);
			txtScheduleDetailId.Enabled = chkScheduleDetailId;

		}

		public void LoadData(int scheduleDetailId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new ScheduleDetailDataModel();
			dataQuery.ScheduleDetailId = scheduleDetailId;

            var items = ScheduleDetailDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item                         = items[0];

			ScheduleDetailId                 = item.ScheduleDetailId;
			ScheduleId                       = item.ScheduleId;
			InTime                           = item.InTime;
			OutTime                          = item.OutTime;
            WorkTicket                       = item.WorkTicket;
			Message                          = item.Message;
			ScheduleDetailActivityCategoryId = item.ScheduleDetailActivityCategoryId;
            //CreatedByAuditId                 = item.CreatedByAuditId;
            //ModifiedByAuditId                = item.ModifiedByAuditId;
            //CreatedDate                      = item.CreatedDate;
            //ModifiedDate                     = item.ModifiedDate;

			if (!showId)
			{
				txtScheduleDetailId.Text = item.ScheduleDetailId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)SystemEntity.ScheduleDetail, scheduleDetailId, "ScheduleDetail");

			}
			else
			{
				txtScheduleDetailId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data                         = new ScheduleDetailDataModel();

			ScheduleDetailId                 = data.ScheduleDetailId;
			ScheduleId                       = data.ScheduleId;
			InTime                           = data.InTime;
			OutTime                          = data.OutTime;
			Message                          = data.Message;
            WorkTicket                       = data.WorkTicket;
			ScheduleDetailActivityCategoryId = data.ScheduleDetailActivityCategoryId;
            //CreatedDate                      = data.CreatedDate;
            //ModifiedDate                     = data.ModifiedDate;
            //CreatedByAuditId                 = data.CreatedByAuditId;
            //ModifiedByAuditId                = data.ModifiedByAuditId;
		}

		#endregion

		#region Events
		
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ScheduleDetail";
			FolderLocationFromRoot = "Scheduling/ScheduleDetail";
			PrimaryEntity = SystemEntity.ScheduleDetail;

			// set object variable reference            
			PlaceHolderCore = dynScheduleDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				var isTesting = SessionVariables.IsTesting;
				txtScheduleDetailId.Visible = isTesting;
				lblScheduleDetailId.Visible = isTesting;
				
				SetupDropdown();
				
			}			
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
            
			var scheduleData = ScheduleDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(scheduleData, drpScheduleList, ScheduleDataModel.DataColumns.ScheduleId,
				ScheduleDataModel.DataColumns.ScheduleId);

			var categoryData = ScheduleDetailActivityCategoryDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(categoryData, drpScheduleDetailActivityCategoryList, ScheduleDetailActivityCategoryDataModel.DataColumns.Name,
				ScheduleDetailActivityCategoryDataModel.DataColumns.ScheduleDetailActivityCategoryId);

			if (isTesting)
			{
				drpScheduleList.AutoPostBack = true;
				drpScheduleDetailActivityCategoryList.AutoPostBack = true;

				if (drpScheduleList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtScheduleId.Text.Trim()))
					{
						drpScheduleList.SelectedValue = txtScheduleId.Text;
					}
					else
					{
						txtScheduleId.Text = drpScheduleList.SelectedItem.Value;
					}
				}

				if (drpScheduleDetailActivityCategoryList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtScheduleDetailActivityCategoryId.Text.Trim()))
					{
						drpScheduleDetailActivityCategoryList.SelectedValue = txtScheduleDetailActivityCategoryId.Text;
					}
					else
					{
						txtScheduleDetailActivityCategoryId.Text = drpScheduleDetailActivityCategoryList.SelectedItem.Value;
					}
				}

				txtScheduleDetailActivityCategoryId.Visible = true;
				txtScheduleId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtScheduleId.Text.Trim()))
				{
					drpScheduleList.SelectedValue = txtScheduleId.Text;
				}

				if (!string.IsNullOrEmpty(txtScheduleDetailActivityCategoryId.Text.Trim()))
				{
					drpScheduleDetailActivityCategoryList.SelectedValue = txtScheduleDetailActivityCategoryId.Text;
				}
			}
		}

		protected void drpScheduleList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtScheduleId.Text = drpScheduleList.SelectedItem.Value;
		}

		protected void drpScheduleDetailActivityCategoryList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtScheduleDetailActivityCategoryId.Text = drpScheduleDetailActivityCategoryList.SelectedItem.Value;
		}

		#endregion

	}
}