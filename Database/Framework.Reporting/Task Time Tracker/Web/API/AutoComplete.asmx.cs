using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Web.Script.Serialization;
using System.Xml;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using System.Collections;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.TaskTimeTracker.TimeTracking;
using DataModel.Framework.Audit;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web
{
	/// <summary>
	/// Summary description for AutoComplete
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]

	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class AutoComplete : System.Web.Services.WebService
	{
		List<string> LstGroupByItems = new List<string>();		

		[WebMethod(EnableSession = true)]
		public string[] GetModuleNames(string prefixText, int count)
		{
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);

			var uerNames = new List<string>();

			for (var i = 0; i < dt.Rows.Count; i++)
			{
				uerNames.Add(dt.Rows[i][2].ToString());
			}

			return uerNames.ToArray();
		}

		[WebMethod(EnableSession = true)]
		public string[] GetFeatureNames(string prefixText, int count)
		{
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetList(prefixText, SessionVariables.RequestProfile);

			var lstNames = new List<string>();

			for (var i = 0; i < dt.Rows.Count; i++)
			{
				lstNames.Add(dt.Rows[i]["Feature"].ToString());
			}

			return lstNames.Distinct().ToArray();
		}

		[WebMethod(EnableSession = true)]
		public string[] GetReleaseLogStatusList(string prefixText, int count)
		{
			var dt = Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.GetList(SessionVariables.RequestProfile);

			var lstNames = new List<string>();

			for (var i = 0; i < dt.Rows.Count; i++)
			{
				lstNames.Add(dt.Rows[i][2].ToString());
			}

			return lstNames.ToArray();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.ReleaseLog.ReleaseLogDataModel> GetReleaseLogList()
		{
			var items = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetReleaseLogList(SessionVariables.RequestProfile);
           return items.OrderBy(o => o.Name).Reverse().ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.TimeTracking.ScheduleDetailActivityCategoryDataModel> GetScheduleDetailActivityCategoryList()
		{
			var items = ScheduleDetailActivityCategoryDataManager.GetScheduleDetailActivityCategoryList(SessionVariables.RequestProfile);
           return items.OrderBy(o => o.Name).Reverse().ToList();
		}
		

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.ClientDataModel> GetClientList()
		{
			var items = TaskTimeTracker.Components.BusinessLayer.ClientDataManager.GetEntityDetails(DataModel.TaskTimeTracker.ClientDataModel.Empty,SessionVariables.RequestProfile,0);
			return items;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<QuestionCategoryDataModel> GetQuestionCategoryList()
		{
            var items = QuestionCategoryDataManager.GetEntityDetails(QuestionCategoryDataModel.Empty,SessionVariables.RequestProfile);
			return items;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.EventMonitoring.NotificationPublisherDataModel> GetNotificationPublisherList()
		{
			var items = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetNotificationPublisherList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Core.ThemeDataModel> GetThemeList()
		{
			var items = Framework.Components.Core.ThemeDataManager.GetThemeList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Core.ThemeKeyDataModel> GetThemeKeyList()
		{
			var items = Framework.Components.Core.ThemeKeyDataManager.GetThemeKeyList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Core.ThemeCategoryDataModel> GetThemeCategoryList()
		{
			var items = Framework.Components.Core.ThemeCategoryDataManager.GetThemeCategoryList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.Core.SystemEntityTypeDataModel> GetEntityList()
        {
            var items = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityDetails(DataModel.Framework.Core.SystemEntityTypeDataModel.Empty,SessionVariables.RequestProfile);
            return items;
        }

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.EntityDateRangeStateDataModel> GetEnityDateRangeStateList()
		{
			var items = EntityDateRangeStateDataManager.GetEntityDetails(DataModel.TaskTimeTracker.EntityDateRangeStateDataModel.Empty, SessionVariables.RequestProfile);
			return items;
		}
		

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.EventMonitoring.NotificationEventTypeDataModel> GetNotificationEventTypeList()
		{
			var items = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetNotificationEventTypeList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.RequirementAnalysis.ProjectDataModel> GetProjectList()
		{
            var items = ProjectDataManager.GetProjectList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.RequirementAnalysis.UseCaseDataModel> GetUseCaseList()
        {
            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetUseCaseList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.RequirementAnalysis.NeedDataModel> GetNeedList()
        {
            var items = TaskTimeTracker.Components.BusinessLayer.NeedDataManager.GetNeedList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.RequirementAnalysis.ProjectUseCaseStatusDataModel> GetProjectUseCaseStatusList()
        {
            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetProjectUseCaseStatusList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
        }

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.ApplicationDevelopment.ModuleDataModel> GetModuleList()
		{
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetEntityDetails(DataModel.TaskTimeTracker.ApplicationDevelopment.ModuleDataModel.Empty,SessionVariables.RequestProfile, 0);
			return items.OrderBy(o => o.Name).ToList();
		}
		
		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel> GetFunctionalityActiveStatusList()
		{
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetFunctionalityActiveStatusList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.TasksAndWorkFlow.TaskEntityDataModel> GetTaskEntityList()
		{
			var items = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetTaskEntityList(SessionVariables.RequestProfile); 
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.Task.TaskDataModel> GetTaskList()
		{
			var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetEntityDetails(DataModel.TaskTimeTracker.Task.TaskDataModel.Empty,SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.Task.TaskTypeDataModel> GetTaskTypeList()
        {
            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskTypeDataManager.GetEntityDetails(DataModel.TaskTimeTracker.Task.TaskTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
        }

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.TimeTracking.ActivityDataModel> GetAcitivityList()
		{
			var items = ActivityDataManager.GetEntityDetails(DataModel.TaskTimeTracker.TimeTracking.ActivityDataModel.Empty, SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.TasksAndWorkFlow.TaskScheduleDataModel> GetTaskScheduleList()
		{
			var items = Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.GetTaskScheduleList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.TaskScheduleId).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.Priority.TaskPackageDataModel> GetTaskPackageList()
		{
			var items = TaskTimeTracker.Components.Module.Priority.TaskPackageDataManager.GetTaskPackageList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.TaskPackageId).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.Competency.CompetencyDataModel> GetCompetencyList()
		{
			var items = TaskTimeTracker.Components.Module.Competency.CompetencyDataManager.GetCompetencyList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.CompetencyId).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.DeliverableArtifactDataModel> GetDeliverableArtifactList()
		{
			var items = TaskTimeTracker.Components.BusinessLayer.DeliverableArtifactDataManager.GetDeliverableArtifactList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.DeliverableArtifactId).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.DeliverableArtifactStatusDataModel> GetDeliverableArtifactStatusList()
		{
			var items = TaskTimeTracker.Components.BusinessLayer.DeliverableArtifactStatusDataManager.GetDeliverableArtifactStatusList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.DeliverableArtifactStatusId).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<StandardListDataModel> GetFunctionalityImageList()
		{
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageDataManager.GetFunctionalityImageList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<StandardListDataModel> GetFunctionalityImageAttributeList()
		{
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.GetFunctionalityImageAttributeList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}
		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<Framework.Components.ReleaseLog.DomainModel.ReleaseFeatureDataModel> GetReleaseFeatureList() 
		{
			var items = Framework.Components.ReleaseLog.ReleaseFeatureDataManager.GetEntityDetails(Framework.Components.ReleaseLog.DomainModel.ReleaseFeatureDataModel.Empty, SessionVariables.RequestProfile, 0);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.ReleaseLog.ReleasePublishCategoryDataModel> GetReleasePublishCategoryList()
		{
			var items = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetEntityDetails(DataModel.Framework.ReleaseLog.ReleasePublishCategoryDataModel.Empty,SessionVariables.RequestProfile, 0);
            return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.ReleaseLog.ReleaseIssueTypeDataModel> GetReleaseIssueTypeList()
		{
            var data = new DataModel.Framework.ReleaseLog.ReleaseIssueTypeDataModel();
            var items = Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, 0);
            return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.TimeTracking.ScheduleStateDataModel> GetScheduleStateList()
		{
            var items = TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.GetScheduleStateList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.TimeTracking.ScheduleQuestionDataModel> GetScheduleQuestionList()
		{
            var items = ScheduleQuestionDataManager.GetScheduleQuestionList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.QuestionPhrase).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.Core.SystemEntityTypeDataModel> GetSystemEntityList()
		{
			var items = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.EntityName).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Core.SubscriberApplicationRoleDataModel> GetSubscriberApplicationRoleList()
		{
			var items = Framework.Components.Core.SubscriberApplicationRoleDataManager.GetSubscriberApplicationRoleList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Core.MenuDataModel> GetParentMenuList()
		{
			var items = Framework.Components.Core.MenuDataManager.GetParentMenuList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.MenuDisplayName).ToList();
		}		

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel> GetApplicationList()
		{
			var items = Framework.Components.ApplicationUser.ApplicationDataManager.GetEntityDetails(ApplicationDataModel.Empty, SessionVariables.RequestProfile);
            return items; 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.AuthenticationAndAuthorization.ApplicationRoleDataModel> GetApplicationRoleList()
		{
			var items = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetEntityDetails(ApplicationRoleDataModel.Empty,SessionVariables.RequestProfile);
			return items;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel> GetApplicationUserList()
		{
			var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetApplicationUserList(SessionVariables.RequestProfile);
			return items;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel> GetAllApplicationUserList()
		{
			var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetAllApplicationUserList(SessionVariables.RequestProfile);
			return items;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.LogAndTrace.UserLoginStatusDataModel> GetUserLoginStatusList()
		{
			var items = Framework.Components.LogAndTrace.UserLoginStatusDataManager.GetUserLoginStatusList(SessionVariables.RequestProfile);
			return items;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel> GetRelativeApplicationUserList()
		{
			var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetApplicationUserList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.FirstName).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel> GetAppCodeApplicationUserList()
		{
			var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetApplicationUserList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.FirstName).ToList();
		}

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.Feature.FeatureDataModel> GetFeatureList()
        {
            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetFeatureList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.Feature.FeatureRuleDataModel> GetFeatureRuleList()
        {
            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetFeatureRuleList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.Feature.FeatureRuleStatusDataModel> GetFeatureRuleStatusList()
        {
            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.GetFeatureRuleStatusList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
        }

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<FieldConfigurationModeCategoryDataModel> GetFieldConfigurationModeCategoryList()
		{
			var items = Framework.Components.UserPreference.FieldConfigurationModeCategoryDataManager.GetEntityDetails(FieldConfigurationModeCategoryDataModel.Empty, SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<FieldConfigurationModeDataModel> GetFieldConfigurationModeList()
		{
			var items = Framework.Components.UserPreference.FieldConfigurationModeDataManager.GetEntityDetails(FieldConfigurationModeDataModel.Empty, SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}
		
		
		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Audit.AuditActionDataModel> GetAuditActionList()
		{
			var items = Framework.Components.Audit.AuditActionDataManager.GetEntityDetails(AuditActionDataModel.Empty, SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Audit.TraceDataModel> GetTraceList()
		{
			var items = Framework.Components.Audit.TraceDataManager.GetEntityDetails(TraceDataModel.Empty, SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<UserPreferenceDataTypeDataModel> GetUserPreferenceDataTypeList()
		{
			var items = Framework.Components.UserPreference.UserPreferenceDataTypeDataManager.GetUserPreferenceDataTypeList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.Configuration.UserPreferenceKeyDataModel> GetUserPreferenceKeyList()
		{
			var items = Framework.Components.UserPreference.UserPreferenceKeyDataManager.GetUserPreferenceKeyList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList(); 
		}

        [WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.Configuration.UserPreferenceCategoryDataModel> GetUserPreferenceCategoryList()
		{
			var items = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.GetUserPreferenceCategoryList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<string> GetComputerList()
		{
			var items = Framework.Components.LogAndTrace.Log4NetDataManager.GetComputerDetails(SessionVariables.RequestProfile);
			return items;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<string> GetConnectionKeyList()
		{
			var items = Framework.Components.LogAndTrace.Log4NetDataManager.GetConnectionKeyList(SessionVariables.RequestProfile);
			return items;
		}

		
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityDataModel> GetFunctionalityList()
        {
            var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityDataModel();
            var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityDataModel>();

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetFunctionalityList(SessionVariables.RequestProfile);

            for (var i = 0; i < items.Count(); i++)
            {
                var oData = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityDataModel();
                var item = items[i];
				oData.ApplicationId = (int)item.ApplicationId;
                oData.FunctionalityId = (int)item.FunctionalityId;
                oData.Name = item.Name.ToString();

                dataList.Add(oData);
            }

            return dataList.OrderBy(o => o.Name).ToList();
        }

        public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel> GetFunctionalityOwnerList()
        {
            var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel();
            var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel>();

            var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetEntityDetails(DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel.Empty, SessionVariables.RequestProfile); 

            for (var i = 0; i < items.Count(); i++)
            {
                var oData = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel();
                var item = items[i];
                oData.FunctionalityOwnerId = (int)item.FunctionalityOwnerId;
                oData.Developer = item.Developer.ToString();

                dataList.Add(oData);
            }

            return dataList.OrderBy(o => o.Developer).ToList();
        }

        //public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel>GetFunctionalityActiveStatusList()
        //{
        //    var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel();
        //    var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetFunctionalityActiveStatusList(SessionVariables.RequestProfile);

        //    var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel>();


        //    for (var i = 0; i < items.Count(); i++)
        //    {
        //        var oData = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel();
        //        var item = items[i];
        //        oData.FunctionalityActiveStatusId = (int)item.FunctionalityActiveStatusId;
        //        oData.Name = item.Name.ToString();

        //        dataList.Add(oData);
        //    }

        //    return items.OrderBy(o => o.Name).ToList();
        //}

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FeatureOwnerStatusDataModel> GetFeatureOwnerStatusList()
        {
            var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.FeatureOwnerStatusDataModel();
            var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.FeatureOwnerStatusDataModel>();

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, 0);

            for (var i = 0; i < items.Count(); i++)
            {
                var oData = new DataModel.TaskTimeTracker.ApplicationDevelopment.FeatureOwnerStatusDataModel();
                var item = items[i];
                oData.FeatureOwnerStatusId = (int)item.FeatureOwnerStatusId;
                oData.Name = item.Name.ToString();

                dataList.Add(oData);
            }

            return dataList.OrderBy(o => o.Name).ToList();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.ApplicationDevelopment.DeveloperRoleDataModel> GetDeveloperRoleList()
        {
            var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.DeveloperRoleDataModel();
            var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.DeveloperRoleDataModel>();

			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetList(SessionVariables.RequestProfile);

            foreach (DataRow dr in dt.Rows)
            {
                var oData = new DataModel.TaskTimeTracker.ApplicationDevelopment.DeveloperRoleDataModel();
                
                oData.DeveloperRoleId = (int)dr["DeveloperRoleId"];
                oData.Name = (string)dr["Name"];

                dataList.Add(oData);
            }

            return dataList.OrderBy(o => o.Name).ToList();
        }

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Core.TabParentStructureDataModel> GetTabParentStructureList()
		{
			var items = Framework.Components.Core.TabParentStructureDataManager.GetTabParentStructureList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Configuration.FieldConfigurationDataModel> GetGroupByList(string entityName, int mode)
		{

			var systemEntityTypeId = (Framework.Components.DataAccess.SystemEntity)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), entityName);
			var dataList = new List<DataModel.Framework.Configuration.FieldConfigurationDataModel>();

			var data = new DataModel.Framework.Configuration.FieldConfigurationDataModel();
			data.SystemEntityTypeId = (int)systemEntityTypeId;
			data.FieldConfigurationModeId = mode;

			var items = Framework.Components.UserPreference.FieldConfigurationDataManager.GetFieldConfigurationList(data, SessionVariables.RequestProfile);

			// method used to get the list of items not required to be bound to the GroupBy dropdownlist
			CheckGroupByListBoxItems(items, entityName);

			for (var i = 0; i < items.Count(); i++)
			{
				var item = items[i];				

				if (!item.Name.Contains("GroupBy") && !LstGroupByItems.Contains(item.Name))
                {
                    var oData = new DataModel.Framework.Configuration.FieldConfigurationDataModel(){				
					                    Name = item.Name
					                ,   FieldConfigurationDisplayName = item.FieldConfigurationDisplayName
                                    ,   Value = item.Value
                                };
					dataList.Add(oData);
				}
			}

			return dataList.OrderBy(o => o.Name).ToList();
		}

		public List<string> CheckGroupByListBoxItems(List<FieldConfigurationDataModel> items, string primaryEntity)
		{
			
				if (primaryEntity == "ReleaseLogDetail")
				{
					for (var i = 0; i < items.Count(); i++)
					{
						var item = items[i];

						if (item.Name.Equals("UpdatedRange"))
						{
							LstGroupByItems.Add(item.Name);
						}
					}
					if (primaryEntity == "DataBaseChangeLog")
					{
					}
			}
			return LstGroupByItems;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<CustomObject> CheckExcludeItemsListBoxItems()
		{
			var excludeItem = new List<CustomObject>();

			excludeItem.Add(new CustomObject("None", 0));
			excludeItem.Add(new CustomObject("0 Sum Hours", 1));			 
			
			return excludeItem;			
		}

		public class CustomObject
		{
			public string Name { get; set; }
			public int Value { get; set; }

			public CustomObject()
			{
			}

			public CustomObject(string stringValue, int intValue)
			{
				Name = stringValue;
				Value = intValue;
			}
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetAutoCompleteList(string primaryEntity, string txtName, int AuditId)
		{

			DataTable dt = null;
			var lstNames = new List<string>();

			if (primaryEntity == "Client")
			{
                var list = TaskTimeTracker.Components.BusinessLayer.ClientDataManager.GetEntityDetails(DataModel.TaskTimeTracker.ClientDataModel.Empty, SessionVariables.RequestProfile, 0);
				lstNames = list.Select(item => item.Name).ToList();
				return lstNames.ToArray();
			}
			else if (primaryEntity == "ApplicationOperation")
			{
				dt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "Application")
			{
				dt = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "Project")
			{
                dt = ProjectDataManager.GetList(SessionVariables.RequestProfile);
			}

			else if (primaryEntity == "CustomTimeLog")
			{
				if (txtName.Equals("CustomTimeLogKey"))
				{
					dt = CustomTimeLogDataManager.GetDetails(CustomTimeLogDataModel.Empty,SessionVariables.RequestProfile);
					if (dt != null)
					{
						for (var i = 0; i < dt.Rows.Count; i++)
						{
							if (!lstNames.Contains(dt.Rows[i]["CustomTimeLogKey"].ToString()))
								lstNames.Add(dt.Rows[i]["CustomTimeLogKey"].ToString());
						}
					}
				}

				return lstNames.ToArray();
			}

			else if (primaryEntity == "DateRangeTitle")
			{
				dt = Framework.Components.UserPreference.DateRangeTitleDataManager.GetList(SessionVariables.RequestProfile);
			}          

            else if (primaryEntity == "Language")
            {
                dt = Framework.Components.Core.LanguageDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ApplicationRelation")
            {
                dt = Framework.Components.Core.ApplicationRelationDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "TabParentStructure")
            {
                dt = Framework.Components.Core.TabParentStructureDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "SystemForeignRelationshipType")
            {
                dt = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.GetList(SessionVariables.RequestProfile);
            }
			else if (primaryEntity == "Layer")
			{
                dt = LayerDataManager.GetList(SessionVariables.RequestProfile);
			}
            else if (primaryEntity == "EntityDateRangeStateType")
            {
                dt = EntityDateRangeStateTypeDataManager.GetList(SessionVariables.RequestProfile);
            }            
			else if (primaryEntity == "RunTimeFeature")
			{
				dt = TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.GetList(SessionVariables.RequestProfile);
			}
            else if (primaryEntity == "SubscriberApplicationRole")
            {
                dt = Framework.Components.Core.SubscriberApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ApplicationUserTitle")
            {
                dt = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ApplicationUser")
            {
                dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            }
			else if (primaryEntity == "Feature")
			{
                dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "ApplicationMode")
			{
				dt = Framework.Components.UserPreference.ApplicationModeDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "FeatureGroup")
			{
                dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "FeatureRule")
			{
                dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "FeatureRuleStatus")
			{
                dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "FeatureRuleCategory")
			{
                dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.GetList(SessionVariables.RequestProfile);
			}
            else if (primaryEntity == "MenuCategory")
            {
                dt = Framework.Components.Core.MenuCategoryDataManager.GetList(SessionVariables.RequestProfile);
            }
			else if (primaryEntity == "ApplicationRole")
			{
				dt = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
			}
            else if (primaryEntity == "Menu")
            {
                dt = MenuDataManager.GetList(SessionVariables.RequestProfile);

                if (dt != null)
                { 
                    if (txtName == "ApplicationModule")
                    {
                        if (dt.Columns.Contains("ApplicationModule"))
                        {
                            lstNames = (from menuData in dt.AsEnumerable() 
                                       select menuData.Field<string>("ApplicationModule")).Distinct().ToList();
                        }
                    }
                }
            }

            else if (primaryEntity == "ApplicationUser")
            {
                dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);

                if (dt != null)
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        if (txtName == "ApplicationUserName")
                        {
                            if (dt.Columns.Contains("ApplicationUserName"))
                            {
                                lstNames.Add(dt.Rows[i]["ApplicationUserName"].ToString());
                            }
                        }
                        if (txtName == "LastName")
                        {
                            if (dt.Columns.Contains("LastName"))
                            {
                                lstNames.Add(dt.Rows[i]["LastName"].ToString());
                            }
                        }
                        if (txtName == "FirstName")
                        {
                            if (dt.Columns.Contains("FirstName"))
                            {
                                lstNames.Add(dt.Rows[i]["FirstName"].ToString());
                            }
                        }
                    }
                }

                return lstNames.ToArray();
            }

            else if (primaryEntity == "ListSettings")
            {
                if (txtName == "ApplicationUser")
                {
                    dt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetList(SessionVariables.RequestProfile);
                }
            }
            else if (primaryEntity == "ModuleOwner")
            {
                if (txtName == "Developer")
                {
                    dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetList(SessionVariables.RequestProfile);
                    if (dt != null)
                    {
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!lstNames.Contains(dt.Rows[i]["Developer"].ToString()))
                                lstNames.Add(dt.Rows[i]["Developer"].ToString());
                        }
                    }

                    return lstNames.ToArray();
                }
            }
            else if (primaryEntity == "ApplicationUserProfileImageMaster")
            {
                if (txtName == "Title")
                {
                    dt = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetList(SessionVariables.RequestProfile);
                    if (dt != null)
                    {
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!lstNames.Contains(dt.Rows[i]["Title"].ToString()))
                                lstNames.Add(dt.Rows[i]["Title"].ToString());
                        }
                    }

                    return lstNames.ToArray();
                }
            }
            else if (primaryEntity == "FunctionalityOwner")
            {
                if (txtName == "Developer")
                {
                    dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetList(SessionVariables.RequestProfile);
                    if (dt != null)
                    {
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!lstNames.Contains(dt.Rows[i]["Developer"].ToString()))
                                lstNames.Add(dt.Rows[i]["Developer"].ToString());
                        }
                    }

                    return lstNames.ToArray();
                }
            }
            else if (primaryEntity == "ApplicationRoute")
            {
                if (txtName == "RouteName" || txtName == "EntityName")
                {
                    dt = Framework.Components.Core.ApplicationRouteDataManager.GetList(SessionVariables.RequestProfile);
                    if (dt != null)
                    {
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            if (txtName == "RouteName")
                            {
                                if (!lstNames.Contains(dt.Rows[i]["RouteName"].ToString()))
                                {
                                    lstNames.Add(dt.Rows[i]["RouteName"].ToString());
                                }
                            }
                            else
                            {
                                if (!lstNames.Contains(dt.Rows[i]["EntityName"].ToString()))
                                {
                                    lstNames.Add(dt.Rows[i]["EntityName"].ToString());
                                }
                            }
                        }
                    }

                    return lstNames.ToArray();
                }
            }
            else if (primaryEntity == "EntityOwner")
            {
                if (txtName == "Developer")
                {
                    dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.GetList(SessionVariables.RequestProfile);
                    if (dt != null)
                    {
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!lstNames.Contains(dt.Rows[i]["Developer"].ToString()))
                                lstNames.Add(dt.Rows[i]["Developer"].ToString());
                        }
                    }

                    return lstNames.ToArray();
                }
            }
            else if (primaryEntity == "Functionality")
            {
                dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetDetails(DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel.Empty, SessionVariables.RequestProfile);

                if (txtName == "FunctionalityOwner")
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        lstNames.Add(dt.Rows[i]["Developer"].ToString());
                    }

                    return lstNames.Distinct().ToArray();
                }
                if (txtName == "Name")
                {
                    dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
                }


            }
            else if (primaryEntity == "ReleaseLogDetail")
            {
                dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetList(SessionVariables.RequestProfile);

                if (txtName == "PrimaryDeveloper")
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        lstNames.Add(dt.Rows[i]["PrimaryDeveloper"].ToString());
                    }

                    return lstNames.Distinct().ToArray();
                }

                else if (txtName == "PrimaryEntity")
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        lstNames.Add(dt.Rows[i]["PrimaryEntity"].ToString());
                    }

                    return lstNames.Distinct().ToArray();
                }

                else if (txtName == "Module")
                {
                    dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);
                }

                else if (txtName == "JIRA")
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        lstNames.Add(dt.Rows[i]["JIRA"].ToString());
                    }

                    return lstNames.Distinct().ToArray();
                }

                else if (txtName == "Feature")
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        lstNames.Add(dt.Rows[i]["Feature"].ToString());
                    }

                    return lstNames.Distinct().ToArray();
                }

                else if (txtName == "ReleaseLogId")
                {
                    dt = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetList(SessionVariables.RequestProfile);

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        //lstNames.Add(String.Format("{0}-{1}", dt.Rows[i]["Name"].ToString(),dt.Rows[i]["ReleaseLogId"].ToString()));
                        lstNames.Add(dt.Rows[i]["Name"].ToString());
                    }
                    return lstNames.ToArray();
                }
            }

            else if (primaryEntity == "UseCase")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "UseCaseActor")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "UseCaseStep")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "UseCasePackage")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "UseCaseRelationship")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseRelationshipDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "UseCaseWorkFlowCatgeory")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ProjectUseCaseStatus")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "DeveloperRole")
            {
                dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ProjectUseCaseStatusArchive")
            {
                if (txtName == "Project")
                {
                    dt = TaskTimeTracker.Components.BusinessLayer.ProjectDataManager.GetList(SessionVariables.RequestProfile);
                }
                else if (txtName == "UseCase")
                {
                    dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
                }
                else if (txtName == "ProjectUseCaseStatus")
                {
                    dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
                }
            }
            else if (primaryEntity == "Report")
            {
                dt = Framework.Components.Core.ReportDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ReportCategory")
            {
                dt = Framework.Components.Core.ReportCategoryDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "VacationPlan")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.VacationPlanDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "Skill")
            {
                dt = TaskTimeTracker.Components.Module.Competency.SkillDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "SkillLevel")
            {
                dt = TaskTimeTracker.Components.Module.Competency.SkillLevelDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "Competency")
            {
                dt = TaskTimeTracker.Components.Module.Competency.CompetencyDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ScheduleState")
            {
                dt = ScheduleStateDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ProductivityArea")
            {
                dt = ProductivityAreaDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ProductivityAreaFeature")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ProjectPortfolio")
            {
                dt = TaskTimeTracker.Components.BusinessLayer.ProjectPortfolioDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ProjectPortfolioGroup")
            {
                dt = ProjectPortfolioGroupDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "QuestionCategory")
            {
                dt = QuestionCategoryDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "ConnectionString")
            {
                dt = Framework.Components.Core.ConnectionStringDataManager.GetList(SessionVariables.RequestProfile);
            }
            else if (primaryEntity == "AllEntityDetail")
            {
                if (txtName == "EntityName")
                {
                    dt = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
                }
                else if (txtName == "DBName")
                {
                    lstNames.Add("Application Development");
                    lstNames.Add("Authentication And Authorization");
                    lstNames.Add("Common Services");
                    lstNames.Add("Configuration");
                    lstNames.Add("Human Resource Management");
                    lstNames.Add("Logging And Tracing");
                    lstNames.Add("Project Planning");
                    lstNames.Add("Location");
                    lstNames.Add("Task Time Tracker");
                    lstNames.Add("Task And Workflow");
                    lstNames.Add("Test Case Management");
                    lstNames.Add("Time Entry");

                    return lstNames.ToArray();
                }
                else if (txtName == "DBProjectName")
                {
                    lstNames.Add("DB TaskTimeTracker");
                    lstNames.Add("DB TCM");
                    lstNames.Add("Framework");
                    lstNames.Add("Framework.ApplicationUser");
                    lstNames.Add("Framework.Audit");
                    lstNames.Add("Framework.Configuration");
                    lstNames.Add("Framework.EventMonitoring");
                    lstNames.Add("Framework.Import");
                    lstNames.Add("Framework.LogAndTrace");
                    lstNames.Add("Framework.ReleaseLog");
                    lstNames.Add("Framework.TasksAndWorkflow");

                    return lstNames.ToArray();
                }
                else if (txtName == "DBComponentName")
                {
                    lstNames.Add("Framework.Components.ApplicationUser");
                    lstNames.Add("Framework.Components.Audit");
                    lstNames.Add("Framework.Components.Configuration");
                    lstNames.Add("Framework.Components.Core");
                    lstNames.Add("Framework.Components.EventMonitoring");
                    lstNames.Add("Framework.Components.Import");
                    lstNames.Add("Framework.Components.LogAndTrace");
                    lstNames.Add("Framework.Components.ReleaseLog");
                    lstNames.Add("Framework.Components.TasksAndWorkflow");
                    lstNames.Add("TaskTimeTracker.Components.ApplicationDevelopment");
                    lstNames.Add("TaskTimeTracker.Components.BusinessLayer");
                    lstNames.Add("TaskTimeTracker.Components.Module.Competency");
                    lstNames.Add("TaskTimeTracker.Components.Module.Priority");
                    lstNames.Add("TaskTimeTracker.Components.Module.RequirementAnalysis");
                    lstNames.Add("TaskTimeTracker.Components.Module.RiskReward");
                    lstNames.Add("TaskTimeTracker.Components.Module.TimeTracking");
                    lstNames.Add("TestCaseManagement.Components.DataAccess");

                    return lstNames.ToArray();
                }
            }
            else if (primaryEntity == "FunctionalityPriority")
            {
                dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.GetList(SessionVariables.RequestProfile);
            }

            else if (primaryEntity == "FunctionalityActiveStatus")
            {
                dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
            }

            else if (primaryEntity == "FunctionalityEntityStatus")
            {
                if (txtName == "AssignedTo")
                {
                    dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetList(SessionVariables.RequestProfile);
                    if (dt != null)
                    {
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!lstNames.Contains(dt.Rows[i]["Developer"].ToString()))
                                lstNames.Add(dt.Rows[i]["Developer"].ToString());
                        }
                    }

                    return lstNames.ToArray();
                }
            }
            else if (primaryEntity == "Module")
            {
                dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);
            }

			if (dt != null)
			{
				for (var i = 0; i < dt.Rows.Count; i++)
				{
					if (dt.Columns.Contains("Name"))
					{
						lstNames.Add(dt.Rows[i]["Name"].ToString());
					}
				}
			}

			return lstNames.ToArray();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<ListItemOption> GetAutoCompleteListForDropDown(string dropDownName)
		{
			var result = new List<ListItemOption>();

			if (dropDownName == "DailyTaskItemQueueStatus")
			{
				var dt = TaskTimeTracker.Components.Module.TimeTracking.DailyTaskItemQueueStatusDataManager.GetList(SessionVariables.RequestProfile);
				foreach (DataRow dr in dt.Rows)
				{
					var item = new ListItemOption()
					{
							Text = Convert.ToString(dr[StandardDataModel.StandardDataColumns.Name])
						,	Value = Convert.ToString(dr[DataModel.TaskTimeTracker.TimeTracking.DailyTaskItemQueueStatusDataModel.DataColumns.DailyTaskItemQueueStatusId])
					};
					result.Add(item);
				}
			}
			
			return result;
		}

		[WebMethod(EnableSession = true)] 
		public string[] FillUpDate(string daterange)
		{
			var format = HttpContext.Current.Session["UserDateFormat"].ToString();
			
			var dateRange = Shared.UI.Web.DateRangeHelper.FillUpDate(daterange,format);
			
			var dates = new List<string>();
			dates.Add(dateRange[0]);
			dates.Add(dateRange[1]);
			
			return dates.ToArray();
		}

		public class Schedule
		{
			public decimal? TotalHoursWorked { get; set; }
			public string FirstName { get; set; }
		}

		public class ReleaseLog
		{
			public int? ReleaseLogId { get; set; }
			public string Name { get; set; }
		}

		public class SystemEntityType
		{
			public int? SystemEntityTypeId { get; set; }
			public string EntityName { get; set; }
		}

		public class ListItemOption
		{
			public string Value;
			public string Text;
		}


		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<Schedule> GetScheduleData()
		{
			List<Schedule> rows = new List<Schedule>();
			Schedule dt;
			var dt1 = new DataTable();
            dt1 =ScheduleDataManager.GetSampleSearch();

			foreach (DataRow dr in dt1.Rows)
			{
				dt = new Schedule();

				dt.FirstName = dr["FirstName"].ToString();
				dt.TotalHoursWorked = Convert.ToDecimal(dr["TotalHoursWorked"].ToString());

				rows.Add(dt);
			}
			return rows;
		}

		//[WebMethod(EnableSession = true)]
		//[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		//public string GetScheduleData()
		//{
		//	var dt = new DataTable();
		//	dt = Schedule.GetSampleSearch();


		//	List<Dictionary<string, object>> rows =
		//		 new List<Dictionary<string, object>>();
		//	Dictionary<string, object> row;

		//	//string[][] JaggedArray = new string[dt.Rows.Count][];
		//	//int i = 0;		   

		//	foreach (DataRow rs in dt.Rows)
		//	{
		//		row = new Dictionary<string, object>();
		//		foreach (DataColumn col in dt.Columns)
		//		{
		//			row.Add(col.ColumnName, rs[col]);
		//		}
		//		rows.Add(row);
		//		//JaggedArray[0] = new string[] { "TotalHoursWorked", "FirstName" };
		//		//JaggedArray[i] = new string[] { rs["TotalHoursWorked"].ToString(), rs["FirstName"].ToString()};
		//		//i = i + 1;
		//	}

		//	// Return JSON data
		//	JavaScriptSerializer js = new JavaScriptSerializer();
		//	string strJSON = js.Serialize(rows);
		//	return strJSON;



		//}
	}
}