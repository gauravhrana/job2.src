using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.Competency;
using DataModel.TaskTimeTracker.Feature;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.BusinessLayer.Feature;
using TaskTimeTracker.Components.BusinessLayer.Task;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.WBS.Task
{
    public partial class Details : PageDetails
    {

		#region Methods

		private DataTable GetCompetencyData(int taskId)
		{
			var dt = TaskXCompetencyDataManager.GetByTask(taskId, SessionVariables.RequestProfile.AuditId);
            var compdt = CompetencyDataManager.GetList(SessionVariables.RequestProfile);
			var resultdt = compdt.Clone();
			foreach (DataRow row in dt.Rows)
			{
				var rows = compdt.Select("CompetencyId = " + row[CompetencyDataModel.DataColumns.CompetencyId]);
				resultdt.ImportRow(rows[0]);
			}
			return resultdt;
		}

		private string[] GetCompetencyColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Competency, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetApplicationUserData(int taskId)
		{
            var dt = TaskXApplicationUserDataManager.GetByTask(taskId, SessionVariables.RequestProfile);
			var audt = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			var resultdt = audt.Clone();
			foreach (DataRow row in dt.Rows)
			{
				var rows = audt.Select("ApplicationUserId = " + row[ApplicationUserDataModel.DataColumns.ApplicationUserId]);
				resultdt.ImportRow(rows[0]);
			}
			return resultdt;
		}

		private string[] GetApplicationUserColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.ApplicationUser, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetFeatureData(int taskId)
		{
			var dt = FeatureXTaskDataManager.GetByTask(taskId, SessionVariables.RequestProfile.AuditId);
            var fdt = FeatureDataManager.GetList(SessionVariables.RequestProfile);
			var resultdt = fdt.Clone();
			foreach (DataRow row in dt.Rows)
			{
				var rows = fdt.Select("FeatureId = " + row[FeatureDataModel.DataColumns.FeatureId]);
				resultdt.ImportRow(rows[0]);
			}
			return resultdt;
		}


		private string[] GetFeatureColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Feature, "DBColumns", SessionVariables.RequestProfile);
		}

		private string[] GetTaskNoteColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.TaskNote, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetTaskNoteData(int taskId)
		{
            var dt = TaskXTaskNote.GetByTask(taskId, SessionVariables.RequestProfile);
            var fdt = TaskNoteDataManager.GetList(SessionVariables.RequestProfile);
			var resultdt = fdt.Clone();
			foreach (DataRow row in dt.Rows)
			{
				var rows = fdt.Select("TaskNoteId = " + row[TaskNoteDataModel.DataColumns.TaskNoteId]);
				resultdt.ImportRow(rows[0]);
			}
			return resultdt;
		}
		private string[] GetDeliverableArtifactColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.DeliverableArtifact, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetDeliverableArtifactData(int taskId)
		{
			var dt = TaskXDeliverableArtifactDataManager.GetByTask(taskId, SessionVariables.RequestProfile.AuditId);
            var fdt = DeliverableArtifactDataManager.GetList(SessionVariables.RequestProfile);
			var resultdt = fdt.Clone();
			foreach (DataRow row in dt.Rows)
			{
				var rows = fdt.Select("DeliverableArtifactId = " + row[DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId]);
				resultdt.ImportRow(rows[0]);
			}
			return resultdt;
		}

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();
			tabControl.AddTab("Task", detailsControl, String.Empty, true);

			var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("Competency", listControl);

			listControl.Setup("Competency", "Aptitude", "CompetencyId", setId, true, GetCompetencyData, GetCompetencyColumns, "Competency");
			listControl.SetSession("true");

			var listControlUser = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("ApplicationUser", listControlUser);

			listControlUser.Setup("ApplicationUser", "Shared/AuthenticationAndAuthorization", "ApplicationUserId", setId, true, GetApplicationUserData, GetApplicationUserColumns, "ApplicationUser");
			listControlUser.SetSession("true");

			var listControlFeature = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("Feature", listControlFeature);

			listControlFeature.Setup("Feature", "", "FeatureId", setId, true, GetFeatureData, GetFeatureColumns, "Feature");
			listControlFeature.SetSession("true");

			var listControlTN = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("TaskNote", listControlTN);

			listControlTN.Setup("TaskNote", "", "TaskNoteId", setId, true, GetTaskNoteData, GetTaskNoteColumns, "TaskNote");
			listControlTN.SetSession("true");

			var listControlDA = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("DeliverableArtifact", listControlDA);

			listControlDA.Setup("DeliverableArtifact", "", "DeliverableArtifactId", setId, true, GetDeliverableArtifactData, GetDeliverableArtifactColumns, "DeliverableArtifact");
			listControlDA.SetSession("true");

			tabControl.Setup("TaskDetailsView");
			

			return tabControl;

		}

		private DataTable GetCompetencyData(string key)
		{
			return GetCompetencyData(int.Parse(key));
		}

		private DataTable GetApplicationUserData(string key)
		{
			return GetApplicationUserData(int.Parse(key));
		}

		private DataTable GetFeatureData(string key)
		{
			return GetFeatureData(int.Parse(key));
		}

		private DataTable GetTaskNoteData(string key)
		{
			return GetTaskNoteData(int.Parse(key));
		}

		private DataTable GetDeliverableArtifactData(string key)
		{
			return GetDeliverableArtifactData(int.Parse(key));
		}



		#endregion



        #region Events


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.Task;
            PrimaryEntityKey = "Task";
            DetailsControlPath = ApplicationCommon.GetControlPath("Task", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
			BreadCrumbObject = Master.BreadCrumbObject;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

    }
}