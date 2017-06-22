using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public class ApplicationSecurity
    {
        public static int AuditId = 0;

        private static int GetModeId(string key)
        {
            if (string.IsNullOrEmpty(key) || key == "DBColumns")
            {
                key = "-1";
            }
            var modeId = Convert.ToInt32(key);
            return modeId;
        }

        public static string[] GetEntityColumns(string fieldConfigurationMode, SystemEntity systemEntityType, RequestProfile requestProfile)
        {
            string[] columns;

            var fieldConfigModeId = GetModeId(fieldConfigurationMode);

            if (fieldConfigModeId > 0)
				columns = GetGridViewColumns(systemEntityType, fieldConfigModeId, requestProfile);
            else
				columns = GetGridViewColumns(systemEntityType, requestProfile);

            return columns;
        }

		public static string[] GetGridViewColumns(SystemEntity systemEntityType, RequestProfile requestProfile)
	    {
		    return GetGridViewColumns((int)systemEntityType, requestProfile);
	    }

		public static string[] GetGridViewColumns(int systemEntityTypeId, RequestProfile requestProfile)
	    {
			var result = Framework.Components.ApplicationSecurity.GetGridViewColumns(systemEntityTypeId, requestProfile);
			return result;
	    }

		public static string[] GetGridViewColumns(SystemEntity systemEntityType, int key, RequestProfile requestProfile)
		{
			return GetGridViewColumns((int) systemEntityType, key, requestProfile);
		}

		public static string[] GetGridViewColumns(int systemEntityTypeId, int key, RequestProfile requestProfile)
		{
			var result = Framework.Components.ApplicationSecurity.GetGridViewColumns(systemEntityTypeId, key, requestProfile);
			return result;
	    } 	   

		public static string[] GetTaskAlgorithmColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;

			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskAlgorithm, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskAlgorithm, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetTaskAlgorithmItemColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskAlgorithmItem, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskAlgorithmItem, requestProfile);

			// returnValues = new[] { "ActivityId", "Description", "SortOrder", "ApplicationId" };

			return returnValues;
		}       

        public static string[] GetRiskColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Risk, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Risk, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                   
            return returnValues;
        }

        public static string[] GetRewardColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Reward, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Reward, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

        public static string[] GetProjectColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
 			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Project, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Project, requestProfile);

           //returnValues = new[] { "Client", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action", "ApplicationId" };
                    
            return returnValues;
        }

		public static string[] GetClientXProjectColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
 			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ClientXProject, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ClientXProject, requestProfile);

           //returnValues = new[] { "Client", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action", "ApplicationId" };
                    
            return returnValues;
        }		

        public static string[] GetProjectTimeLineColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ProjectTimeLine, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ProjectTimeLine, requestProfile);

            // returnValues = new[] { "Project", "StartDate", "EndDate" };
                    
            return returnValues;
        }

		public static string[] GetLayerColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Layer, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Layer, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
                   
            return returnValues;
        }
        
        public static string[] GetFeatureColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Feature, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Feature, requestProfile);

            //returnValues = new[] { "SortOrder", "Client", "Name", "Description", "Project", "Need", "ApplicationId" };
                   
            return returnValues;
        }

		public static string[] GetRunTimeFeatureColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.RunTimeFeature, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.RunTimeFeature, requestProfile);

			//returnValues = new[] { "SortOrder", "Client", "Name", "Description", "Project", "Need", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetVacationPlanColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.VacationPlan, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.VacationPlan, requestProfile);

			//returnValues = new[] {"ApplicationId", "Name", "Description", "ApplicationUserId", "StartDate", "EndDate", "SortOrder"  };
					
            return returnValues;
        }

		public static string[] GetReportCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ReportCategory, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ReportCategory, requestProfile);

			//returnValues = new[] {"ApplicationId", "Name", "Description",  "SortOrder"  };

			return returnValues;
		}

		public static string[] GetReportColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Report, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Report, requestProfile);

			//returnValues = new[] { "Name", "Description", "Title", "SortOrder"  };

			return returnValues;
		}

        public static string[] GetActivityColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Activity, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Activity, requestProfile);

            //returnValues = new[] { "Name", "Layer", "Description", "SortOrder" };
                    
            return returnValues;
        }

        public static string[] GetActivityStateColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ActivityState, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ActivityState, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
                  
            return returnValues;
        }

        public static string[] GetNeedColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Need, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Need, requestProfile);

            //returnValues = new[] { "Client", "Project", "Name", "Description", "SortOrder", "ApplicationId" };
                    
            return returnValues;
        }

		public static string[] GetNeedXFeatureColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.NeedXFeature, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.NeedXFeature, requestProfile);

            //returnValues = new[] { "Need", "Feature", "ApplicationId" };
                    
            return returnValues;
        }

        public static string[] GetTaskPackageColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskPackage, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskPackage, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
                   
            return returnValues;
        }

        public static string[] GetMilestoneColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Milestone, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Milestone, requestProfile);

           //returnValues = new[] { "Project", "Name", "Description", "SortOrder", "ApplicationId" };
                    
            return returnValues;
        }

        public static string[] GetClientColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Client, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Client, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }


		public static string[] GetEntityDateRangeStateTypeColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.EntityDateRangeStateType, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.EntityDateRangeStateType, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

		public static string[] GetEntityDateRangeStateColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.EntityDateRangeState, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.EntityDateRangeState, requestProfile);			

			return returnValues;
		}

        public static string[] GetTaskColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Task, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Task, requestProfile);

            //returnValues = new[] { "Feature", "ApplicationId", "Name", "Description", "TaskType", "SortOrder", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

        public static string[] GetTaskTypeColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskType, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskType, requestProfile);

            //returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

		public static string[] GetTaskXActivityInstanceColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskXActivityInstance, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskXActivityInstance, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

        public static string[] GetTaskPriorityTypeColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskPriorityType, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskPriorityType, requestProfile);

            //returnValues = new[] { "Name", "ApplicationId", "Description", "SortOrder", "Weight", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

		public static string[] GetTaskPriorityXApplicationUserColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskPriorityXApplicationUser, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskPriorityXApplicationUser, requestProfile);

			//returnValues = new[] { "TaskPriorityType", "ApplicationUser", "Task", "UpdatedByPerson", "UpdatedDate" };
					
			return returnValues;
		}

        public static string[] GetTaskRiskRewardRankingXPersonColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskRiskRewardRankingXPerson, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskRiskRewardRankingXPerson, requestProfile);

            //returnValues = new[] { "Task", "Risk", "Reward", "Person", "Ranking", "ApplicationId", "UpdatedByPerson", "UpdatedDate" };
                   
            return returnValues;
        }

        public static string[] GetTaskFormulationColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskFormulation, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskFormulation, requestProfile);

            //returnValues = new[] { "ProjectName", "TaskName", "FeatureName", "ProjectId", "LayerId", "TaskId", "FeatureId", "ApplicationId", "SortOrder" };
                    
            return returnValues;
        }

        public static string[] GetQuestionColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Question, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Question, requestProfile);
            // returnValues = new[] { "Category", "ApplicationId", "Question", "SortOrder", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

        public static string[] GetScheduleColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Schedule, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Schedule, requestProfile);
			//returnValues = new[] { "Person", "WorkDate", "StartTime", "EndTime", "TotalHoursWorked", "NextWorkDate",
				//		"NextWorkTime", "ApplicationId" };
                   
            return returnValues;
        }

        public static string[] GetScheduleItemColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ScheduleItem, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ScheduleItem, requestProfile);

            //returnValues = new[] { "ScheduleId", "TaskFormulationId", "TotalTimeSpent", "ApplicationId" };
                   
            return returnValues;
        }
		
		public static string[] GetScheduleStateColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ScheduleState, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ScheduleState, requestProfile);			

			return returnValues;
		}

        public static string[] GetScheduleQuestionColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ScheduleQuestion, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ScheduleQuestion, requestProfile);
			//returnValues = new[] { "ScheduleId", "Question", "Answer", "ApplicationId" };
                   
            return returnValues;
        }

		public static string[] GetScheduleDetailColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ScheduleDetail, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ScheduleDetail, requestProfile);
			

			return returnValues;
		}

		public static string[] GetFeatureGroupColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.FeatureGroup, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.FeatureGroup, requestProfile);
			
			//returnValues = new[] { "FeatureGroupId", "Name", "Description", "SortOrder"};
					
			return returnValues;
		}

		public static string[] GetQuestionCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.QuestionCategory, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.QuestionCategory, requestProfile);

			//returnValues = new[] { "QuestionCategoryId","Name", "Description", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetFeatureRuleCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.FeatureRuleCategory, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.FeatureRuleCategory, requestProfile);

			//returnValues = new[] { "FeatureRuleCategoryId", "Name", "Description", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetFeatureRuleColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.FeatureRule, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.FeatureRule, requestProfile);

			//returnValues = new[] { "FeatureRuleId", "Name", "Description", "FaetureRuleCategoryId", "SortOrder" };
					
			return returnValues;
		}
		
        #region Competency Module

        public static string[] GetSkillColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Skill, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Skill, requestProfile);

            // returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

        public static string[] GetSkillLevelColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.SkillLevel, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.SkillLevel, requestProfile);

           //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
 
            return returnValues;
        }

        public static string[] GetCompetencyColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Competency, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Competency, requestProfile);

           // returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

        public static string[] GetSkillXPersonXSkillLevelColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.SkillXPersonXSkillLevel, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.SkillXPersonXSkillLevel, requestProfile);
            //returnValues = new[] { "Skill", "Person", "SkillLevel", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                    
            return returnValues;
        }

		public static string[] GetCompetencyXSkillColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.CompetencyXSkill, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.CompetencyXSkill, requestProfile);

			//returnValues = new[] { "Competency", "Skill", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                   
            return returnValues;
        }

		public static string[] GetTaskXCompetencyColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskXCompetency, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskXCompetency , requestProfile);

			//returnValues = new[] { "Task", "Competency", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
                   
            return returnValues;

        }
		
        public static string[] GetNotificationEventTypeColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.NotificationEventType, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.NotificationEventType, requestProfile);

            //returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }

		public static string[] GetNotificationSubscriberColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.NotificationSubscriber, AEFLModeId, requestProfile);
            else
				returnValues = GetGridViewColumns(SystemEntity.NotificationSubscriber, requestProfile);

            //returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }

		public static string[] GetNotificationPublisherColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.NotificationPublisher, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.NotificationPublisher, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetNotificationPublisherXEventTypeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.NotificationPublisherXEventType, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.NotificationPublisherXEventType, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetTestSuiteXTestCaseColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.TestSuiteXTestCase, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.TestSuiteXTestCase, requestProfile);

            //returnValues = new[] { "Task", "Competency", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }
        public static string[] GetNotificationRegistrarColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.NotificationRegistrar, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.NotificationRegistrar, requestProfile);

            //returnValues = new[] { "Task", "Competency", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }
        public static string[] GetMenuCategoryColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
			    returnValues = GetGridViewColumns(SystemEntity.MenuCategory, AEFLModeId, requestProfile);
			else
			    returnValues = GetGridViewColumns(SystemEntity.MenuCategory, requestProfile);

            //returnValues = new[] { "Task", "Competency", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }
        #endregion
		
		public static string[] GetTaskStatusTypeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskStatusType, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.TaskStatusType, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static string[] GetTaskPackageXOwnerXTaskColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.TaskPackageXOwnerXTask, AEFLModeId, requestProfile);
            else
				returnValues = GetGridViewColumns(SystemEntity.TaskPackageXOwnerXTask, requestProfile);

            //returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }

        public static string[] GetTaskNoteColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.TaskNote, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.TaskNote, requestProfile);

            //returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }

        public static string[] GetTestSuiteColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.TestSuite, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.TestSuite, requestProfile);

            //returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }


        public static string[] GetTestCaseColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.TestCase, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.TestCase, requestProfile);

            //returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }

        public static string[] GetTestRunColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.TestRun, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.TestRun, requestProfile);

            //returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }

		public static string[] GetThemeDetailsColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ThemeDetails, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ThemeDetails, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}


		public static string[] GetThemesColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.Themes, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.Themes, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetThemeKeyColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ThemeKey, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ThemeKey, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}
        
		public static string[] GetUserPreferenceSelectedItemColumns(string key, RequestProfile requestProfile)
		{
			
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.FeatureGroup, AEFLModeId, requestProfile);
			else
			{
				switch (key)
				{
					case "DBColumns":
						{
							returnValues = GetGridViewColumns(SystemEntity.UserPreferenceSelectedItem, requestProfile);
						}
						break;
					case "top":
						returnValues = new[] {"Value", "Count"};
						break;
					default:
						returnValues = new[]
						               	{
						               		"Application", "ApplicationUser", "UserPreferenceKey", "ParentKey", "Value", "SortOrder",
						               		"ApplicationId", "Updated Date", "Updated By", "Last Action"
						               	};
						break;
				}
			}

			return returnValues;
		}

        public static string[] GetAEFLModeCategoryColumns(string key, RequestProfile requestProfile)
        {
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.FieldConfigurationModeCategory, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.FieldConfigurationModeCategory, requestProfile);

           //returnValues = new[] { "AEFLModeCategoryId", "Name", "Description", "SortOrder" };
                   
            return returnValues;
        }

        public static string[] GetFieldConfigurationModeCategoryColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues =null;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.FieldConfigurationModeCategory, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.FieldConfigurationModeCategory, requestProfile);

            //returnValues = new[] { "AEFLModeCategoryId", "Name", "Description", "SortOrder" };

            return returnValues;
        }

        public static string[] GetFieldConfigurationModeColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.FieldConfigurationMode, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.FieldConfigurationMode, requestProfile);

            //returnValues = new[] { "AEFLModeCategoryId", "Name", "Description", "SortOrder" };

            return returnValues;
        }

        public static string[] GetFieldConfigurationColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.FieldConfiguration, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.FieldConfiguration, requestProfile);

            //returnValues = new[] { "AEFLModeCategoryId", "Name", "Description", "SortOrder" };

            return returnValues;
        }


        public static string[] GetDeliverableArtifactsColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.DeliverableArtifact, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.DeliverableArtifact, requestProfile);

            //returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };

            return returnValues;
        }

        public static string[] GetFeatureRuleStatusColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.FeatureRuleStatus, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.FeatureRuleStatus, requestProfile);

            return returnValues;
        }

		public static string[] GetDeliverableArtifactStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.DeliverableArtifactStatus, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.DeliverableArtifactStatus, requestProfile);

			returnValues = new[] { "DeliverableArtifactStatusId", "Name", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetUseCaseColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.UseCase, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.UseCase, requestProfile);			

			return returnValues;
		}        

        public static string[] GetFeatureGroupXFeatureColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.FeatureGroupXFeature, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.FeatureGroupXFeature, requestProfile);

            return returnValues;
        }

        public static string[] GetFeatureXFeatureRuleColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.FeatureXFeatureRule, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.FeatureXFeatureRule, requestProfile);

            return returnValues;
        }


        public static string[] GetActivityXDeliverableArtifactColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ActivityXDeliverableArtifact, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ActivityXDeliverableArtifact, requestProfile);

            return returnValues;
        }

		public static string[] GetUseCaseActorColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.UseCaseActor, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.UseCaseActor, requestProfile);

			returnValues = new[] { "UseCaseActorId", "Name", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetUseCaseRelationshipColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.UseCaseRelationship, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.UseCaseRelationship, requestProfile);

			returnValues = new[] { "UseCaseRelationshipId", "Name", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetUseCaseActorXUseCaseColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.UseCaseActorXUseCase, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.UseCaseActorXUseCase, requestProfile);

			return returnValues;
		}

        public static string[] GetTaskXDeliverableArtifactColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.TaskXDeliverableArtifact, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.TaskXDeliverableArtifact, requestProfile);

            return returnValues;
        }

		public static string[] GetUseCaseStepColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.UseCaseStep, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.UseCaseStep, requestProfile);

			returnValues = new[] { "UseCaseStepId", "Name", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetUseCasePackageColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.UseCasePackage, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.UseCasePackage, requestProfile);

			returnValues = new[] { "UseCasePackageId", "Name", "Description", "SortOrder" };

			return returnValues;
		}

        public static string[] GetMilestoneFeatureStateColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.MilestoneFeatureState, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.MilestoneFeatureState, requestProfile);

            return returnValues;
        }

        public static string[] GetMilestoneXFeatureColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.MilestoneXFeature, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.MilestoneXFeature, requestProfile);

            return returnValues;
        }

		public static string[] GetProjectXUseCaseColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ProjectXUseCase, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ProjectXUseCase, requestProfile);

			return returnValues;
		}

		public static string[] GetProjectUseCaseStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ProjectUseCaseStatus, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ProjectUseCaseStatus, requestProfile);

			returnValues = new[] { "ProjectUseCaseStatusId", "Name", "Description", "SortOrder" };

			return returnValues;
		}

        public static string[] GetUseCaseWorkFlowCategoryColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.UseCaseWorkFlowCategory, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.UseCaseWorkFlowCategory, requestProfile);

            returnValues = new[] { "UseCaseWorkFlowCategoryId", "Name", "Description", "SortOrder" };

            return returnValues;
        }

        public static string[] GetUseCaseXUseCaseStepColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.UseCaseXUseCaseStep, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.UseCaseXUseCaseStep, requestProfile);

            return returnValues;
        }

        public static string[] GetApplicationModeXRunTimeFeatureColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ApplicationModeXRunTimeFeature, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ApplicationModeXRunTimeFeature, requestProfile);

            return returnValues;
        }

		public static string[] GetProjectUseCaseStatusArchiveColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ProjectUseCaseStatusArchive, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ProjectUseCaseStatusArchive, requestProfile);

            return returnValues;
        }

        public static string[] GetProductivityAreaColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ProductivityArea, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ProductivityArea, requestProfile);

            returnValues = new[] { "ProductivityAreaId", "Name", "Description", "SortOrder" };

            return returnValues;
        }

        public static string[] GetProductivityAreaFeatureColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ProductivityAreaFeature, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ProductivityAreaFeature, requestProfile);

            returnValues = new[] { "ProductivityAreaFeatureId", "Name", "Description", "SortOrder" };

            return returnValues;
        }

        public static string[] GetProductivityAreaXProductivityAreaFeatureColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ProductivityAreaXProductivityAreaFeature, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ProductivityAreaXProductivityAreaFeature, requestProfile);

            return returnValues;
        }

        public static string[] GetProductivityAreaFeatureXApplicationUserColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ProductivityAreaFeatureXApplicationUser, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ProductivityAreaFeatureXApplicationUser, requestProfile);

            return returnValues;
        }

		public static string[] GetUseCasePackageXUseCaseColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.UseCasePackageXUseCase, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.UseCasePackageXUseCase, requestProfile);

			return returnValues;
		}

        public static string[] GetProjectPortfolioGroupColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues = null;
            var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ProjectPortfolioGroup, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ProjectPortfolioGroup, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }

		public static string[] GetProjectPortfolioGroupXProjectPortfolioColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues = null;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns(SystemEntity.ProjectPortfolioGroupXProjectPortfolio, AEFLModeId, requestProfile);
			else
				returnValues = GetGridViewColumns(SystemEntity.ProjectPortfolioGroupXProjectPortfolio, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

        public static string[] GetTaskRoleColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues = null;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.TaskRole, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.TaskRole, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

            return returnValues;
        }

        public static string[] GetProjectPortfolioColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ProjectPortfolio, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ProjectPortfolio, requestProfile);

            //returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };

            return returnValues;
        }

        public static string[] GetReportXReportCategoryColumns(string key, RequestProfile requestProfile)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns(SystemEntity.ReportXReportCategory, AEFLModeId, requestProfile);
            else
                returnValues = GetGridViewColumns(SystemEntity.ReportXReportCategory, requestProfile);

            return returnValues;
        }
    }
}
