using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.Task;
using TaskTimeTracker.Components.BusinessLayer;


namespace ApplicationContainer.UI.Web.Feature
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        #region Get Methods	
        
        private DataTable GetTaskData(int featureId)
        {
            var data = new FeatureXTaskDataModel();
			data.FeatureId = featureId;
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

		private string[] GetTaskColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Task, "DBColumns", SessionVariables.RequestProfile);
		}

        private DataTable GetTaskFormulationData(int featureId)
        {
            var data = new TaskFormulationDataModel();
            data.FeatureId = featureId;
            var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskFormulationDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

		private string[] GetTaskFormulationColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.TaskFormulation, "DBColumns", SessionVariables.RequestProfile);
		}

        private DataTable GetNeedXFeatureData(int featureId)
        {
			var dt = NeedXFeatureDataManager.GetByFeature(featureId, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private string[] GetNeedXFeatureColumns()
        {
			var validColumns = FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.NeedXFeature, "Default", SessionVariables.RequestProfile);
            return validColumns;
        }

		private void SaveNeedXFeature(int featureId, List<int> needIds)
		{
			NeedXFeatureDataManager.DeleteByFeature(featureId, SessionVariables.RequestProfile.AuditId);
            NeedXFeatureDataManager.CreateByFeature(featureId, needIds.ToArray(), SessionVariables.RequestProfile);
		}


		private DataTable GetNeedList()
		{
            var dt = NeedDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedNeeds(int featureId)
		{
			var dt = NeedXFeatureDataManager.GetByFeature(featureId, SessionVariables.RequestProfile.AuditId);
			return dt;
		}


        private string[] GetFeatureXFeatureRuleColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetFeatureXFeatureRuleData(int featureId)
        {
            var data = new FeatureXFeatureRuleDataModel();
            data.FeatureId=featureId;
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetFeatureGroupXFeatureColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FeatureGroupXFeature, "DBColumns", SessionVariables.RequestProfile);
        
        }

        private DataTable GetFeatureGroupXFeatureData(int featureId)
        {
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.GetByFeature(featureId, SessionVariables.RequestProfile);
            return dt;
        }


		private DataTable GetTaskFormulationData(string key)
		{
			return GetTaskFormulationData(int.Parse(key));
		}


		private DataTable GetFeatureXFeatureRuleData(string key)
		{
			return GetFeatureXFeatureRuleData(int.Parse(key));
		}


		private DataTable GetFeatureGroupXFeatureData(string key)
		{
			return GetFeatureGroupXFeatureData(int.Parse(key));
		}


		private DataTable GetTaskData(string key)
		{
			return GetTaskData(int.Parse(key));
		}

		private DataTable GetFeatureGroupList()
		{
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFeatureGroups(int featureId)
		{

            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.GetByFeature(featureId, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveFeatureGroupXFeature(int featureId, List<int> featureGroupIds)
		{
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.DeleteByFeature(featureId, SessionVariables.RequestProfile);
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupXFeatureDataManager.CreateByFeature(featureId, featureGroupIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetTaskList()
		{
            var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedTasks(int featureId)
		{
			var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.GetByFeature(featureId, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByFeature(int featureId, List<int> TaskIds)
		{
			TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.DeleteByFeature(featureId, SessionVariables.RequestProfile.AuditId);
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.CreateByFeature(featureId, TaskIds.ToArray(), SessionVariables.RequestProfile);
		}

		protected override Control GetTabControl(int setId, Control detailsControl)
        {
            
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

			//var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			//listControl.Setup("Task", "WBS", "TaskId", setId, true, GetTaskData, GetTaskColumns, "Task");
			//listControl.SetSession("true");

			var bucketControlTask = ApplicationCommon.GetNewBucketControl();
			bucketControlTask.ConfigureBucket("Task", setId, GetTaskList, GetAssociatedTasks, SaveByFeature);		

			var listControlTF = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControlTF.Setup("TaskFormulation", "WBS", "TaskFormulationId", setId, true, GetTaskFormulationData, GetTaskFormulationColumns, "TaskFormulation");
			listControlTF.SetSession("true");

			//var listControlNF = (Shared.UI.Web.Controls.DetailsWithChildren)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			//listControlNF.Setup("NeedXFeature", String.Empty, "NeedXFeatureId", setId, true, GetData, GetNeedXFeatureColumns, "NeedXFeature");
			//listControlNF.SetSession("true");

            var bucketControlNeed = ApplicationCommon.GetNewBucketControl();
			bucketControlNeed.ConfigureBucket("Need", setId, GetNeedList, GetAssociatedNeeds, SaveNeedXFeature);

			var listControlFF = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControlFF.Setup("FeatureXFeatureRule", String.Empty, "FeatureXFeatureRuleId", setId, true, GetFeatureXFeatureRuleData, GetFeatureXFeatureRuleColumns, "FeatureXFeatureRule");
			listControlFF.SetSession("true");

			//var listControlFG = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			//listControlFG.Setup("FeatureGroupXFeature", String.Empty, "FeatureGroupXFeatureId", setId, true, GetFeatureGroupXFeatureData, GetFeatureGroupXFeatureColumns, "FeatureGroupXFeature");
			//listControlFG.SetSession("true");

			var bucketControlFeatureGroup = ApplicationCommon.GetNewBucketControl();
			bucketControlFeatureGroup.ConfigureBucket("FeatureGroup", ApplicationCommon.GetSetId(), GetFeatureGroupList, GetAssociatedFeatureGroups, SaveFeatureGroupXFeature);

            tabControl.Setup("FeatureDetailsView"); 
            tabControl.AddTab("Feature", detailsControl, String.Empty, true);
			tabControl.AddTab("Task", bucketControlTask);
			tabControl.AddTab("Task Formulation", listControlTF);
			tabControl.AddTab("Need", bucketControlNeed);
			tabControl.AddTab("Feature Rule", listControlFF);
			tabControl.AddTab("Feature Group", bucketControlFeatureGroup);
            return tabControl;
        }

        #endregion

		#region Events 

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "Feature";
			var detailsPath = ApplicationCommon.GetControlPath("Feature", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Feature;
			BreadCrumbObject = Master.BreadCrumbObject;
		}	

		#endregion

    }
}