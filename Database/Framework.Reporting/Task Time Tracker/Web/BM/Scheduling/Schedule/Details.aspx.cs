using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Web.UI.HtmlControls;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;
using ApplicationContainer.UI.Web.App_Workflow;
using ApplicationContainer.UI.Web.Scheduling.Schedule.Controls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{
    public partial class Details : PageDetails
	{

		#region Methods

		private DataTable GetScheduleQuestionData(int scheduleId)
		{
            var dt = ScheduleQuestionDataManager.GetBySchedule(scheduleId,SessionVariables.RequestProfile);
            var scheduleQuestiondt = ScheduleQuestionDataManager.GetList(SessionVariables.RequestProfile);
			var resultdt = scheduleQuestiondt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = scheduleQuestiondt.Select("ScheduleQuestionId = " + row[ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
		}

		private string[] GetScheduleQuestionColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ScheduleQuestion, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetScheduleItemData(int scheduleId)
		{
            var dt = ScheduleItemDataManager.GetBySchedule(scheduleId,SessionVariables.RequestProfile);
            var scheduleItemdt = ScheduleItemDataManager.GetList(SessionVariables.RequestProfile);
			var resultdt = scheduleItemdt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = scheduleItemdt.Select("ScheduleItemId = " + row[ScheduleItemDataModel.DataColumns.ScheduleItemId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
		}

		private string[] GetScheduleItemColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ScheduleItem, "DBColumns", SessionVariables.RequestProfile);
		}

		protected void btnSyncQuestions_click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			// instead of id use command arugement
			if (btn.CommandArgument == "SyncQuestions")
			{
				ScheduleWorkflow.SyncQuestions(SetId, SessionVariables.RequestProfile);				
				Response.Redirect(Request.RawUrl);
			}
		}

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("ScheduleDetailsView");  

			tabControl.AddTab("Schedule", detailsControl, String.Empty, true);

			//var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			//tabControl.AddTab("Question", listControl);
			HtmlGenericControl divQuestion = new HtmlGenericControl("div");
			divQuestion.ID = "divQuestion";
			Button btnSyncQuestions = new Button { Text = "Sync Questions", CommandArgument = "SyncQuestions", ID = "SyncQuestions" };
			btnSyncQuestions.BackColor = Color.FromArgb(180, 4, 4);
			btnSyncQuestions.Font.Bold = true;
			btnSyncQuestions.ForeColor = Color.White;
			btnSyncQuestions.Click += new EventHandler(btnSyncQuestions_click);

			var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			divQuestion.Controls.Add(listControl);
			divQuestion.Controls.Add(btnSyncQuestions);
			tabControl.AddTab("Question", divQuestion);

			listControl.Setup("ScheduleQuestion", "", "ScheduleQuestionId", setId, true, GetScheduleQuestionData, GetScheduleQuestionColumns, "ScheduleQuestion");
			listControl.SetSession("true");

			var listControlScheduleDetails = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);			
			tabControl.AddTab("ScheduleDetail", listControlScheduleDetails, "Details");
			

			listControlScheduleDetails.Setup("ScheduleDetail", "", "ScheduleDetailId", setId, true, GetScheduleDetailData, GetScheduleDetailColumns, "ScheduleDetail");
			listControlScheduleDetails.SetSession("true");

			var listControlSI = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Item", listControlSI);

			listControlSI.Setup("ScheduleItem", "", "ScheduleItemId", setId, true, GetScheduleItemData, GetScheduleItemColumns, "ScheduleItem");
			listControlSI.SetSession("true");		      
           		

			return tabControl;
		}

		private DataTable GetScheduleItemData(string key)
		{
			return GetScheduleItemData(int.Parse(key));
		}

		private DataTable GetScheduleQuestionData(string key)
		{
			return GetScheduleQuestionData(int.Parse(key));
		}

		private DataTable GetScheduleDetailData(string key)
		{
			var scheduleId = int.Parse(key);
			var dt = ScheduleDetailDataManager.GetBySchedule(scheduleId, SessionVariables.RequestProfile);
			//var scheduleQuestiondt = ScheduleQuestionDataManager.GetList(AuditId);
			//var resultdt = scheduleQuestiondt.Clone();

			//foreach (DataRow row in dt.Rows)
			//{
			//	var rows = scheduleQuestiondt.Select("ScheduleQuestionId = " + row[ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId]);
			//	resultdt.ImportRow(rows[0]);
			//}

			return dt;
		}

		private string[] GetScheduleDetailColumns()
		{
			var fc = SessionVariables.FieldConfigurationMode;
			var fcMode = string.Empty;
			if (fc == 0)
				fcMode = "DBColumns";
			else
				fcMode = fc.ToString();
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ScheduleDetail, fcMode, SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.Schedule;
			PrimaryEntityKey = "Schedule";
			DetailsControlPath = ApplicationCommon.GetControlPath("Schedule", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

    } 

}