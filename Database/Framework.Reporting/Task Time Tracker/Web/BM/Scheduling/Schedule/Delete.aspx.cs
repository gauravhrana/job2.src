using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.Scheduling.Schedule.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;
using System.Web.UI.HtmlControls;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;
using ApplicationContainer.UI.Web.App_Workflow;
using DataModel.Framework.Configuration;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{
    public partial class Delete : PageDelete
    {        

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "Schedule";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("Schedule", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = SystemEntity.Schedule;
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
					var data = new ScheduleDataModel();
                    data.ScheduleId = int.Parse(index);
                    if (!ScheduleDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add(Convert.ToInt32(data.ScheduleId));
                    }
                }
                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
						var data = new ScheduleDataModel();
                        data.ScheduleId = int.Parse(index);
                        ScheduleDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
					DeleteAndRedirect();
                }
                else
                {
                    var msg = String.Empty;
                    foreach (var id in notDeletableIds)
                    {
                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg += ", <br/>";
                        }
                        msg += "ScheduleId: " + id + " has detail records";
                    }
                    Response.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }      

        #endregion

		#region Methods

		private void DeleteAndRedirect()
		{
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.ScheduleItem, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ScheduleItemEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		private DataTable GetScheduleQuestionData(int scheduleId)
		{
			var dt = ScheduleQuestionDataManager.GetBySchedule(scheduleId, SessionVariables.RequestProfile);
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
			var dt = ScheduleItemDataManager.GetBySchedule(scheduleId, SessionVariables.RequestProfile);
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
		
		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.Setup("ScheduleDetailsView");

			tabControl.AddTab("Schedule", detailsControl, String.Empty, true);

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Question", listControl);
			//HtmlGenericControl divQuestion = new HtmlGenericControl("div");
			//divQuestion.ID = "divQuestion";
			//Button btnSyncQuestions = new Button { Text = "Sync Questions", CommandArgument = "SyncQuestions", ID = "SyncQuestions" };
			//btnSyncQuestions.BackColor = Color.FromArgb(180, 4, 4);
			//btnSyncQuestions.Font.Bold = true;
			//btnSyncQuestions.ForeColor = Color.White;
			//btnSyncQuestions.Click += new EventHandler(btnSyncQuestions_click);

			//var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			//divQuestion.Controls.Add(listControl);
			//divQuestion.Controls.Add(btnSyncQuestions);
			//tabControl.AddTab("Question", divQuestion);

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

	}
}