using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog
{
	public partial class Update : PageUpdate
	{

		//#region Methods

		//private DataTable GetCustomTimeLogItemData(int customTimeLogId)
		//{
		//	var customTimeLogItemdt = CustomTimeLogDataManager.GetList(SessionVariables.RequestProfile);
		//	var resultdt = customTimeLogItemdt.Clone();

		//	foreach (DataRow row in dt.Rows)
		//	{
		//		var rows = customTimeLogItemdt.Select("ScheduleItemId = " + row[ScheduleItemDataModel.DataColumns.ScheduleItemId]);
		//		resultdt.ImportRow(rows[0]);
		//	}

		//	return resultdt;
		//}

		//private string[] GetScheduleItemColumns()
		//{
		//	return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ScheduleItem, "DBColumns", SessionVariables.RequestProfile);
		//}

		//private DataTable GetScheduleDetailData(int scheduleId)
		//{
		//	var data = new ScheduleDetailDataModel();
		//	data.ScheduleId = scheduleId;

		//	var dt = ScheduleDetailDataManager.Search(data, SessionVariables.RequestProfile);
		//	return dt;
		//}

		//private string[] GetScheduleDetailColumns()
		//{
		//	return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ScheduleDetail, "DBColumns", SessionVariables.RequestProfile);
		//}

		//protected override Control GetTabControl(int setId, Control updateControl)
		//{
		//	var tabControl = ApplicationCommon.GetNewDetailTabControl();

		//	tabControl.Setup("ScheduleUpdateView");

		//	tabControl.AddTab("Schedule", updateControl, String.Empty, true);

		//	HtmlGenericControl divQuestion = new HtmlGenericControl("div");
		//	divQuestion.ID = "divQuestion";
		//	Button btnSyncQuestions = new Button { Text = "Sync Questions", CommandArgument = "SyncQuestions", ID = "SyncQuestions" };
		//	btnSyncQuestions.BackColor = Color.FromArgb(180, 4, 4);
		//	btnSyncQuestions.Font.Bold = true;
		//	btnSyncQuestions.ForeColor = Color.White;
		//	btnSyncQuestions.Click += new EventHandler(btnSyncQuestions_click);

		//	var editQuestionsControl = (EditScheduleQuestion)Page.LoadControl("~/Scheduling/Schedule/Controls/EditScheduleQuestion.ascx");
		//	editQuestionsControl.SetId(setId);


		//	divQuestion.Controls.Add(editQuestionsControl);
		//	divQuestion.Controls.Add(btnSyncQuestions);
		//	tabControl.AddTab("Question", divQuestion);

		//	HtmlGenericControl divDetails = new HtmlGenericControl("div");
		//	divDetails.ID = "divDetails";
		//	//var listControlSD = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
		//	var list1 = (ScheduleDetailInsert)Page.LoadControl("~/Scheduling/Schedule/Controls/ScheduleDetailInsert.ascx");

		//	divDetails.Controls.Add(list1);
		//	divDetails.Controls.Add(new LiteralControl("<br/><br/><br/><br/><br/><br/>"));
		//	tabControl.AddTab("Detail", divDetails);

		//	//tabControl.AddTab("Detail", listControlSD);
		//	list1.SetId(setId);
		//	//listControlSD.Setup("ScheduleDetail", "", "ScheduleDetailId", setId, true, GetScheduleDetailData, GetScheduleDetailColumns, "ScheduleDetail");
		//	//listControlSD.SetSession("true");

		//	//HtmlGenericControl divItem = new HtmlGenericControl("div");
		//	//divItem.ID = "divItem";
		//	var listControlSI = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
		//	listControlSI.Setup("ScheduleItem", "", "ScheduleItemId", setId, true, GetData, GetScheduleItemColumns, "ScheduleItem");
		//	listControlSI.SetSession("true");
		//	//divItem.Controls.Add(listControlSI);

		//	tabControl.AddTab("Item", listControlSI);



		//	return tabControl;
		//}

		//private DataTable GetData(string key)
		//{
		//	return GetScheduleItemData(int.Parse(key));
		//}

		//#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.CustomTimeLog;

			GenericControlPath = ApplicationCommon.GetControlPath("CustomTimeLog", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "CustomTimeLog";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;

		}

		
		#endregion

	}
}