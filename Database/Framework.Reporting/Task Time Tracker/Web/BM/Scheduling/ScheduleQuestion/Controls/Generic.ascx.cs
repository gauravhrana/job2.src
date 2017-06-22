using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

		#region properties

		public int? ScheduleQuestionId
		{
			get
			{
				if (txtScheduleQuestionId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtScheduleQuestionId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtScheduleQuestionId.Text);
				}
			}
			set
			{
				txtScheduleQuestionId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ScheduleId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtScheduleId.Text.Trim());
				else
					return int.Parse(drpScheduleList.SelectedItem.Value);
			}
			set
			{
				txtScheduleId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? QuestionId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtQuestionId.Text.Trim());
				else
					return int.Parse(drpQuestionList.SelectedItem.Value);
			}
			set
			{
				txtQuestionId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Answer
		{
			get
			{
				return txtAnswer.Text;
			}
			set
			{
				txtAnswer.Text = value ?? String.Empty;
			}
		}
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ScheduleQuestionDataModel();

			data.ScheduleQuestionId = ScheduleQuestionId;
			data.ScheduleId			= ScheduleId;
			data.QuestionId			= QuestionId;
			data.Answer				= Answer;

			if (action == "Insert")
			{
                var dtScheduleQuestion = ScheduleQuestionDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtScheduleQuestion.Rows.Count == 0)
				{
                    ScheduleQuestionDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                ScheduleQuestionDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ScheduleID ?
			return ScheduleQuestionId;
		}

		public override void SetId(int setId, bool chkScheduleQuestionId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkScheduleQuestionId);
			txtScheduleId.Enabled = chkScheduleQuestionId;

		}

		public void LoadData(int scheduleQuestionId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new ScheduleQuestionDataModel();
			dataQuery.ScheduleQuestionId = scheduleQuestionId;

            var items = ScheduleQuestionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			ScheduleQuestionId	= item.ScheduleQuestionId;
			ScheduleId			= item.ScheduleId;
			QuestionId			= item.QuestionId;
			Answer				= item.Answer;

			if (!showId)
			{
				txtScheduleQuestionId.Text = item.ScheduleQuestionId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ScheduleQuestion, scheduleQuestionId, "ScheduleQuestion");

			}
			else
			{
				txtScheduleQuestionId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new ScheduleQuestionDataModel();

			ScheduleQuestionId	= data.ScheduleQuestionId;
			ScheduleId			= data.ScheduleId;
			QuestionId			= data.QuestionId;
			Answer				= data.Answer;

		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ScheduleQuestion";
			FolderLocationFromRoot = "Scheduling/ScheduleQuestion";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleQuestion;

			// set object variable reference            
			PlaceHolderCore = dynScheduleQuestionId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				var isTesting = SessionVariables.IsTesting;
				txtScheduleQuestionId.Visible = isTesting;
				lblScheduleQuestionId.Visible = isTesting;

				SetupDropdown();
			}
		}
		
		#endregion       
		       
		#region methods

		private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
            var questionData = QuestionDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(questionData, drpQuestionList, QuestionDataModel.DataColumns.QuestionPhrase,
					QuestionDataModel.DataColumns.QuestionId);

            var scheduleData = TaskTimeTracker.Components.Module.TimeTracking.ScheduleDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(scheduleData, drpScheduleList, ScheduleDataModel.DataColumns.ScheduleId,
				ScheduleDataModel.DataColumns.ScheduleId);

            if (isTesting)
            {
                drpQuestionList.AutoPostBack = true;
                drpScheduleList.AutoPostBack = true;
                if (drpQuestionList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtQuestionId.Text.Trim()))
                    {
                        drpQuestionList.SelectedValue = txtQuestionId.Text;
                    }
                    else
                    {
                        txtQuestionId.Text = drpQuestionList.SelectedItem.Value;
                    }
                }
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
                txtScheduleId.Visible = true;
                txtQuestionId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtQuestionId.Text.Trim()))
                {
                    drpQuestionList.SelectedValue = txtQuestionId.Text;
                }
                if (!string.IsNullOrEmpty(txtScheduleId.Text.Trim()))
                {
                    drpScheduleList.SelectedValue = txtScheduleId.Text;
                }
            }
        }       

        protected void drpQuestionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuestionId.Text = drpQuestionList.SelectedItem.Value;
        }

        protected void drpScheduleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtScheduleId.Text = drpScheduleList.SelectedItem.Value;
        }
		#endregion       

    }
}