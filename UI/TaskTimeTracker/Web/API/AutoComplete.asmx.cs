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
using Framework.Components.UserPreference;
using ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email;

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
			var lstSource = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);

            return lstSource.Select(x => x.Name).ToArray();
		}

		[WebMethod(EnableSession = true)]
		public string[] GetFeatureNames(string prefixText, int count)
		{
            var lstSource = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);

            return lstSource.Select(x => x.Name).ToArray();
		}

		[WebMethod(EnableSession = true)]
		public string[] GetReleaseLogStatusList(string prefixText, int count)
		{
            var lstSource = Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.GetList(SessionVariables.RequestProfile);

            return lstSource.Select(x => x.Name).ToArray();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.ReleaseLog.ReleaseLogDataModel> GetReleaseLogList()
		{
			var items = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).Reverse().ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.TimeTracking.ScheduleDetailActivityCategoryDataModel> GetScheduleDetailActivityCategoryList()
		{
			var items = ScheduleDetailActivityCategoryDataManager.GetList(SessionVariables.RequestProfile);
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
		public List<DataModel.TaskTimeTracker.ApplicationDevelopment.EntityDataModel> GetEntityDetailList()
		{
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.GetList(SessionVariables.RequestProfile);
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
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList();
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


		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Configuration.FieldConfigurationDataModel> GetSubGroupByList(string entityName, int mode)
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
					var oData = new DataModel.Framework.Configuration.FieldConfigurationDataModel()
					{
						Name = item.Name
						,
						FieldConfigurationDisplayName = item.FieldConfigurationDisplayName
						,
						Value = item.Value
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
			else if (primaryEntity == "Schedule")
			{
				for (var i = 0; i < items.Count(); i++)
				{
					var item = items[i];

					if (item.Name.Equals("Tabs"))
					{
						LstGroupByItems.Add(item.Name);
					}
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
			var lstResult = new List<string>();

			if (primaryEntity == "Client")
			{
                var list = TaskTimeTracker.Components.BusinessLayer.ClientDataManager.GetEntityDetails(DataModel.TaskTimeTracker.ClientDataModel.Empty, SessionVariables.RequestProfile, 0);
				lstResult = list.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ApplicationOperation")
			{
				var items = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "Application")
			{
                var items = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "Project")
			{
                var items = ProjectDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}

			else if (primaryEntity == "CustomTimeLog")
            {
                var items = CustomTimeLogDataManager.GetList(SessionVariables.RequestProfile);
				if (txtName.Equals("CustomTimeLogKey"))
				{
                    lstResult = items.Select(item => item.CustomTimeLogKey).ToList();
				}
			}

			else if (primaryEntity == "DateRangeTitle")
			{
                var items = Framework.Components.UserPreference.DateRangeTitleDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}  
            else if (primaryEntity == "Language")
            {
                var items = Framework.Components.Core.LanguageDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
            }
            else if (primaryEntity == "ApplicationRelation")
            {
                var items = Framework.Components.Core.ApplicationRelationDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.ApplicationRelationId.ToString()).ToList();
            }
            else if (primaryEntity == "TabParentStructure")
            {
                var items = Framework.Components.Core.TabParentStructureDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
            }
            else if (primaryEntity == "SystemForeignRelationshipType")
            {
                var items = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
            }
			else if (primaryEntity == "Layer")
			{
                var items = LayerDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
            else if (primaryEntity == "EntityDateRangeStateType")
            {
                var items = EntityDateRangeStateTypeDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
            }            
			else if (primaryEntity == "RunTimeFeature")
			{
                var items = TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
            else if (primaryEntity == "SubscriberApplicationRole")
            {
                var items = Framework.Components.Core.SubscriberApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
            }
            else if (primaryEntity == "ApplicationUserTitle")
            {
                var items = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
            }
            else if (primaryEntity == "ApplicationUser")
            {
                var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.FullName).ToList();
            }
			else if (primaryEntity == "Feature")
			{
                var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ApplicationMode")
			{
                var items = Framework.Components.UserPreference.ApplicationModeDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "FeatureGroup")
			{
                var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "FeatureRule")
			{
                var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "FeatureRuleStatus")
			{
                var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "FeatureRuleCategory")
			{
                var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
            else if (primaryEntity == "MenuCategory")
            {
                var items = Framework.Components.Core.MenuCategoryDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
            }
			else if (primaryEntity == "ApplicationRole")
			{
                var items = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
                lstResult = items.Select(item => item.Name).ToList();
			}
            else if (primaryEntity == "Menu")
            {
                var items = MenuDataManager.GetList(SessionVariables.RequestProfile);

                if (items != null)
                { 
                    if (txtName == "ApplicationModule")
                    {
                        lstResult = items.Select(item => item.ApplicationModule).ToList();
                    }
                }
            }

            else if (primaryEntity == "ApplicationUser")
            {
                var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);

                if (txtName == "ApplicationUserName")
                {
                    lstResult = items.Select(item => item.ApplicationUserName).ToList();
                }
                if (txtName == "LastName")
                {
                    lstResult = items.Select(item => item.LastName).ToList();
                }
                if (txtName == "FirstName")
                {
                    lstResult = items.Select(item => item.FirstName).ToList();
                }

                return lstResult.ToArray();
            }

            else if (primaryEntity == "ListSettings")
            {
                if (txtName == "ApplicationUser")
                {
                    var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
                    lstResult = items.Select(item => item.FullName).ToList();
                }
            }
            else if (primaryEntity == "ModuleOwner")
            {
                if (txtName == "Developer")
                {
                    var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetList(SessionVariables.RequestProfile);
                    lstResult = items.Select(item => item.Developer).ToList();
                }
            }
            else if (primaryEntity == "ApplicationUserProfileImageMaster")
            {
                if (txtName == "Title")
                {
                    var items = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetList(SessionVariables.RequestProfile);
                    lstResult = items.Select(item => item.Title).ToList();
                }
            }
            else if (primaryEntity == "FunctionalityOwner")
            {
                if (txtName == "Developer")
                {
                    var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetList(SessionVariables.RequestProfile);
                    lstResult = items.Select(item => item.Developer).ToList();
                }
            }
            else if (primaryEntity == "ApplicationRoute")
            {
                if (txtName == "RouteName" || txtName == "EntityName")
                {
                    var items = Framework.Components.Core.ApplicationRouteDataManager.GetList(SessionVariables.RequestProfile);
                    if (txtName == "RouteName")
                    {
                        lstResult = items.Select(item => item.RouteName).ToList();
                    }
                    else
                    {
                        lstResult = items.Select(item => item.EntityName).ToList();
                    }
                }
            }
            else if (primaryEntity == "EntityOwner")
            {
                if (txtName == "Developer")
                {
                    var itemsEO = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.GetList(SessionVariables.RequestProfile);
                    lstResult = itemsEO.Select(item => item.Developer).ToList();
                }
            }
            else if (primaryEntity == "Functionality")
            {
                if (txtName == "FunctionalityOwner")
                {
                    var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetList(SessionVariables.RequestProfile);
                    lstResult = items.Select(item => item.Developer).ToList();
                }
                if (txtName == "Name")
                {
                    var itemsFunc = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile); 
                    lstResult = itemsFunc.Select(item => item.Name).ToList();
                }

            }
			else if (primaryEntity == "FunctionalityStatus")
			{
				if (txtName == "Name")
				{
					var itemsFunc = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityStatusDataManager.GetList(SessionVariables.RequestProfile);
					lstResult = itemsFunc.Select(item => item.Name).ToList();
				}
			}
			else if (primaryEntity == "ReleaseLogDetail")
			{
				var items = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetList(SessionVariables.RequestProfile);

				if (txtName == "PrimaryDeveloper")
				{
					lstResult = items.Select(item => item.PrimaryDeveloper).ToList();
				}

				else if (txtName == "PrimaryEntity")
				{
					lstResult = items.Select(item => item.PrimaryEntity).ToList();
				}
				else if (txtName == "Module")
				{
					var itemsModule = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);
					lstResult = itemsModule.Select(item => item.Name).ToList();
				}

				else if (txtName == "JIRA")
				{
					lstResult = items.Select(item => item.JIRA).ToList();
				}

				else if (txtName == "Feature")
				{
					lstResult = items.Select(item => item.Feature).ToList();
				}

				else if (txtName == "ReleaseLogId")
				{
					var itemsRL = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetList(SessionVariables.RequestProfile);
					lstResult = itemsRL.Select(item => item.Name).ToList();
				}
			}

			else if (primaryEntity == "UseCase")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "UseCaseActor")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "UseCaseStep")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "UseCasePackage")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "UseCaseRelationship")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseRelationshipDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "UseCaseWorkFlowCatgeory")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ProjectUseCaseStatus")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "DeveloperRole")
			{
				var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ProjectUseCaseStatusArchive")
			{
				if (txtName == "Project")
				{
					var items = TaskTimeTracker.Components.BusinessLayer.ProjectDataManager.GetList(SessionVariables.RequestProfile);
					lstResult = items.Select(item => item.Name).ToList();
				}
				else if (txtName == "UseCase")
				{
					var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
					lstResult = items.Select(item => item.Name).ToList();
				}
				else if (txtName == "ProjectUseCaseStatus")
				{
					var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
					lstResult = items.Select(item => item.Name).ToList();
				}
			}
			else if (primaryEntity == "Report")
			{
				var items = Framework.Components.Core.ReportDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ReportCategory")
			{
				var items = Framework.Components.Core.ReportCategoryDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "VacationPlan")
			{
				var items = TaskTimeTracker.Components.Module.TimeTracking.VacationPlanDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "Skill")
			{
				var items = TaskTimeTracker.Components.Module.Competency.SkillDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "SkillLevel")
			{
				var items = TaskTimeTracker.Components.Module.Competency.SkillLevelDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "Competency")
			{
				var items = TaskTimeTracker.Components.Module.Competency.CompetencyDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ScheduleState")
			{
				var items = ScheduleStateDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ProductivityArea")
			{
				var items = ProductivityAreaDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ProductivityAreaFeature")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ProjectPortfolio")
			{
				var items = TaskTimeTracker.Components.BusinessLayer.ProjectPortfolioDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ProjectPortfolioGroup")
			{
				var items = ProjectPortfolioGroupDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "QuestionCategory")
			{
				var items = QuestionCategoryDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "ConnectionString")
			{
				var items = Framework.Components.Core.ConnectionStringDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}
			else if (primaryEntity == "AllEntityDetail")
			{
				if (txtName == "EntityName")
				{
					var items = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
					lstResult = items.Select(item => item.EntityName).ToList();
				}
				else if (txtName == "DBName")
				{
					lstResult.Add("Application Development");
					lstResult.Add("Authentication And Authorization");
					lstResult.Add("Common Services");
					lstResult.Add("Configuration");
					lstResult.Add("Human Resource Management");
					lstResult.Add("Logging And Tracing");
					lstResult.Add("Project Planning");
					lstResult.Add("Location");
					lstResult.Add("Task Time Tracker");
					lstResult.Add("Task And Workflow");
					lstResult.Add("Test Case Management");
					lstResult.Add("Time Entry");

					return lstResult.ToArray();
				}
				else if (txtName == "DBProjectName")
				{
					lstResult.Add("DB TaskTimeTracker");
					lstResult.Add("DB TCM");
					lstResult.Add("Framework");
					lstResult.Add("Framework.ApplicationUser");
					lstResult.Add("Framework.Audit");
					lstResult.Add("Framework.Configuration");
					lstResult.Add("Framework.EventMonitoring");
					lstResult.Add("Framework.Import");
					lstResult.Add("Framework.LogAndTrace");
					lstResult.Add("Framework.ReleaseLog");
					lstResult.Add("Framework.TasksAndWorkflow");

					return lstResult.ToArray();
				}
				else if (txtName == "DBComponentName")
				{
					lstResult.Add("Framework.Components.ApplicationUser");
					lstResult.Add("Framework.Components.Audit");
					lstResult.Add("Framework.Components.Configuration");
					lstResult.Add("Framework.Components.Core");
					lstResult.Add("Framework.Components.EventMonitoring");
					lstResult.Add("Framework.Components.Import");
					lstResult.Add("Framework.Components.LogAndTrace");
					lstResult.Add("Framework.Components.ReleaseLog");
					lstResult.Add("Framework.Components.TasksAndWorkflow");
					lstResult.Add("TaskTimeTracker.Components.ApplicationDevelopment");
					lstResult.Add("TaskTimeTracker.Components.BusinessLayer");
					lstResult.Add("TaskTimeTracker.Components.Module.Competency");
					lstResult.Add("TaskTimeTracker.Components.Module.Priority");
					lstResult.Add("TaskTimeTracker.Components.Module.RequirementAnalysis");
					lstResult.Add("TaskTimeTracker.Components.Module.RiskReward");
					lstResult.Add("TaskTimeTracker.Components.Module.TimeTracking");
					lstResult.Add("TestCaseManagement.Components.DataAccess");

					return lstResult.ToArray();
				}
			}
			else if (primaryEntity == "FunctionalityPriority")
			{
				var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}

			else if (primaryEntity == "FunctionalityActiveStatus")
			{
				var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}

			else if (primaryEntity == "FunctionalityEntityStatus")
			{
				if (txtName == "AssignedTo")
				{
					var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetList(SessionVariables.RequestProfile);
					lstResult = items.Select(item => item.Developer).ToList();
				}
			}
			else if (primaryEntity == "Module")
			{
				var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);
				lstResult = items.Select(item => item.Name).ToList();
			}

			return lstResult.ToArray();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<ListItemOption> GetAutoCompleteListForDropDown(string dropDownName)
		{
			var result = new List<ListItemOption>();

			if (dropDownName == "DailyTaskItemQueueStatus")
			{
				var lstSource = TaskTimeTracker.Components.Module.TimeTracking.DailyTaskItemQueueStatusDataManager.GetList(SessionVariables.RequestProfile);
                foreach (var x in lstSource)
				{
					var item = new ListItemOption()
					{
							Text = x.Name
						,	Value = x.DailyTaskItemQueueStatusId.ToString()
					};
					result.Add(item);
				}
			}
			
			return result;
		}

		[WebMethod(EnableSession = true)] 
		public string[] FillUpDate(string daterange)
		{
			var str = "All";
			if (daterange.StartsWith(str))
				daterange = daterange.Remove(0, str.Length);

            var dateRange = Shared.UI.Web.DateRangeHelper.FillUpDate(daterange.TrimStart(), SessionVariables.UserDateFormat);
            return dateRange;
		}

		[WebMethod(EnableSession = true)]
		public List<DateRangeTitleDataModel> FillUpDateGroup(string group)
		{
			var dateRangeData = DateRangeTitleDataManager.GetList(SessionVariables.RequestProfile);

			List<DateRangeTitleDataModel> data = new List<DateRangeTitleDataModel>();

			if (group != "All")
			{
				foreach (var x in dateRangeData)
				{
					if (x.Name.IndexOf(' ') != -1)
					{
						var dateGroup = x.Name.Substring(0, x.Name.IndexOf(' '));

						if (dateGroup == group.Split(' ')[0].ToString())
						{
							var dateSubGroup = x.Name.Substring(x.Name.IndexOf(' ') + 1, (x.Name.Length - 1 - dateGroup.Length));

							data.Add(x);
						}
					}
				}

				return data;
			}
			else
				return dateRangeData;
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

		static List<List<JiraDataModel>> CollectionOfStatus(DateTime scheduleWorkDate, int personId)
		{
			// Get the ApplicationUserName and Jira details
			var applicationUserName = ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email.SendEmail.GetApplicationUserName(personId);

			var status = new List<string>(new string[]
			{
				"Open",
				"QA",
				"Reopened", 
				"In Progress",
				"Resolved",
				"Review Ready",
				"Closed",
				"ClosedLastMonth"
			});

			var jiraList = new List<JiraDataModel>();

			//Create a list and add all sections jira list
			var collectionOfTotals = new List<List<JiraDataModel>>();

			foreach (var item in status)
			{
				jiraList = JiraDataManager.GetByStatus(applicationUserName, item, scheduleWorkDate);

				collectionOfTotals.Add(jiraList);
			}

			return collectionOfTotals;
		}


		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email.StatusDetailMetricModel> GetChartData(string personId)
		{
			var collectionOfTotals = new List<List<JiraDataModel>>();
			var personIdValue = Convert.ToInt16(personId);

			collectionOfTotals = CollectionOfStatus(DateTime.Today, personIdValue);

			var status = new List<string>(new string[]
			{
				"Open",
				"QA",
				"Reopened", 
				"In Progress",
				"Resolved",
				"Review Ready",
				"Closed",
				"ClosedLastMonth"
			});

			var objData = GetStatusMetricDetail(status, collectionOfTotals, DateTime.Today, personIdValue);
			return objData;

		}
		static List<ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email.StatusDetailMetricModel> GetStatusMetricDetail(List<string> status, List<List<JiraDataModel>> collectionOfTotals, DateTime scheduleWorkDate, int personId)
		{
			var listSummaryData = new List<ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email.StatusDetailMetricModel>();

			int i = 0;
			var lstScheduleDtls = ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email.SendEmail.GetDBData();//Get all DB Data

			foreach (var statusName in status)
			{
				var summaryData = new ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email.StatusDetailMetricModel();

				if (statusName == "Closed")
					summaryData.status = "Closed This Month";
				else if (statusName == "ClosedLastMonth")
					summaryData.status = "Closed Last Month";
				else
					summaryData.status = statusName;

				var sumDueMonths = 0;
				var sumWorkDaysByTicket = 0;
				var sumWorkDaysByPerson = 0;
				decimal? sumWorkHoursByTicket = 0;
				decimal? sumWorkHoursByPerson = 0;
				decimal? sumworkHoursByTeam = 0;

				var lstSumDueMonths = new List<int>();

				foreach (var collection in collectionOfTotals)
				{
					var statusDataCollection = collection.Where(x => x.Status == statusName).ToList();
					if (statusDataCollection.Count > 0)
					{
						summaryData.count = statusDataCollection.Count;
						//Filter DueDates
						var allDueMths = statusDataCollection.Select(d => d.DueDate).Where(p => p != null).ToList();

						//Calculate sum of Due Months
						foreach (var durmths in allDueMths)
							sumDueMonths += Shared.WebCommon.UI.Web.DateTimeHelper.GetMonthsDifference(DateTime.Now, Convert.ToDateTime(durmths).ToUniversalTime().ToLocalTime());

						//Filter Work Tickets
						var allWorkTickets = statusDataCollection.Select(d => d.WorkTicket).ToList();

						foreach (var tickets in allWorkTickets)
						{							
							var objEODMetrics = new EODMetrics();
							objEODMetrics.PersonId = personId;
							objEODMetrics.JiraKey = tickets;
							objEODMetrics.WorkDate = scheduleWorkDate;

							// Get the Work Days By Person & QATicket
							ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email.SendEmail.GetCountOfDaysWorked(objEODMetrics, lstScheduleDtls);
							//Get Work Hours by Person ,QA and Team						
							ApplicationContainer.UI.Web.BM.Scheduling.Schedule.Controls.Email.SendEmail.GetCountOfHoursWorked(objEODMetrics, lstScheduleDtls);

							sumWorkDaysByPerson += objEODMetrics.WorkDaysByPerson;
							sumWorkDaysByTicket += objEODMetrics.WorkDaysByTicket;

							//Calculate Sum of WorkHoursByTicket						
							sumWorkHoursByTicket += objEODMetrics.WorkHoursByQATicket;

							//Calculate Sum of WorkHoursByPerson						
							sumWorkHoursByPerson += objEODMetrics.WorkHoursByPerson;

							//Calculate Sum of WorkHoursByTeam
							sumworkHoursByTeam += objEODMetrics.WorkHoursByTeam;
						}
						break;
					}
				}

				// Calculate the average values for work days by Person and Ticket
				var avgWorkDaysByPerson = Framework.CommonServices.BusinessDomain.Utils.MathUtils.SafeDivideBy(sumWorkDaysByPerson, summaryData.count);
				var avgWorkDaysByTicket = Framework.CommonServices.BusinessDomain.Utils.MathUtils.SafeDivideBy(sumWorkDaysByTicket, summaryData.count);

				summaryData.dueMonths =Framework.CommonServices.BusinessDomain.Utils.MathUtils.SafeDivideBy(sumDueMonths, summaryData.count);
				summaryData.personDays = avgWorkDaysByPerson;
				summaryData.teamDays = avgWorkDaysByTicket;
				summaryData.personHours = sumWorkHoursByPerson;
				summaryData.qaHours = sumWorkHoursByTicket;
				summaryData.teamHours = sumworkHoursByTeam;
				listSummaryData.Add(summaryData);
				i++;
			}

			return listSummaryData;

		}


	}
}