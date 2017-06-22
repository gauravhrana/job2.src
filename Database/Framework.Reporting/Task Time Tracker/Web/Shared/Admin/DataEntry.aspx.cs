using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Admin
{
	public partial class DataEntry : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		
		

		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{
			var requestProfile = new RequestProfile();
			if (drpDBComponent.SelectedValue.Equals("DBName"))
			{
				DBNameDataManager.Save("Application Development", "DBName", 1, SessionVariables.RequestProfile);
				DBNameDataManager.Save("Authentication And Authorization", "DBName", 2,SessionVariables.RequestProfile);
				DBNameDataManager.Save("Common Services", "DBName", 3, SessionVariables.RequestProfile );
				DBNameDataManager.Save("Configuration", "DBName", 4, SessionVariables.RequestProfile);
				DBNameDataManager.Save("Logging And Tracing", "DBName", 5, SessionVariables.RequestProfile);
				DBNameDataManager.Save("Project Planning", "DBName", 6, SessionVariables.RequestProfile);
				DBNameDataManager.Save("Task And Workflow", "DBName", 7, SessionVariables.RequestProfile);
				DBNameDataManager.Save("Task Time Tracker", "DBName", 8, SessionVariables.RequestProfile);
				DBNameDataManager.Save("Test Case Management", "DBName", 9, SessionVariables.RequestProfile);
				DBNameDataManager.Save("Time Entry", "DBName", 10, SessionVariables.RequestProfile);
				DBNameDataManager.Save("Human Resource Management", "DBName", 11, SessionVariables.RequestProfile);
			}
			else if (drpDBComponent.SelectedValue.Equals("DBComponentName"))
			{
				DBComponentNameDataManager.Save("TaskTimeTracker.Components.Module.Competency", "DBComponentName", 1, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("TaskTimeTracker.Components.Module.Priority", "DBComponentName", 2, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("TaskTimeTracker.Components.Module.RequirementAnalysis", "DBComponentName", 3, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("TaskTimeTracker.Components.Module.RiskReward", "DBComponentName", 4, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("TaskTimeTracker.Components.Module.TimeTracking", "DBComponentName", 5, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("TaskTimeTracker.Components.ApplicationDevelopment", "DBComponentName", 6, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.Audit", "DBComponentName", 7, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.Configuration", "DBComponentName", 8, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.Core", "DBComponentName", 9, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.DataAccess", "DBComponentName", 10, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.EventMonitoring", "DBComponentName", 11, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.Import", "DBComponentName", 12, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.LogAndTrace", "DBComponentName", 13, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.ReleaseLog", "DBComponentName", 14, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.TasksAndWorkflow", "DBComponentName", 15, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("TestCaseManagement.Components.DataAccess", "DBComponentName", 16, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("TaskTimeTracker.Components.BusinessLayer", "DBComponentName", 17, SessionVariables.RequestProfile);
				DBComponentNameDataManager.Save("Framework.Components.ApplicationUser", "DBComponentName", 18, SessionVariables.RequestProfile);
			}
			else if (drpDBComponent.SelectedValue.Equals("DBProjectName"))
			{
				DBProjectNameDataManager.Save("DB TaskTimeTracker", "DBProjectName", 1, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework", "DBProjectName", 2, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.ApplicationUser", "DBProjectName", 3, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.Audit", "DBProjectName", 4, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.AuthenticationAndAuthorization", "DBProjectName", 5, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.Configuration", "DBProjectName", 6, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.EventMonitoring", "DBProjectName", 7, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.Import", "DBProjectName", 8, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.LogAndTrace", "DBProjectName", 9, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.ReleaseLog", "DBProjectName", 10, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.Reporting", "DBProjectName", 11, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.TasksAndWorkflow", "DBProjectName", 12, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("Framework.UserPreference", "DBProjectName", 13, SessionVariables.RequestProfile);
				DBProjectNameDataManager.Save("DB TCM", "DBProjectName", 14, SessionVariables.RequestProfile);
			}
				
			else
			{

				AllEntityDetailDataManager.SaveEntityDetails("Client", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Functionality", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityEntityStatus", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityActiveStatus", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityOwner", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityPriority", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityStatus", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityEntityStatusArchive", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityHistory", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityImage", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityImageInstance", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityImageAttribute", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityXFunctionalityActiveStatus", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FunctionalityXFunctionalityImage", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("AllEntityDetail", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DeveloperRole", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DBName", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DBProjectName", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DBComponentName", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FeatureOwnerStatus", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Module", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ModuleOwner", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("EntityOwner", "Application Development", "DB TaskTimeTracker", "TaskTimeTracker.Components.ApplicationDevelopment", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Competency", "Human Resource Management", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Competency", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("CompetencyXSkill", "Human Resource Management", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Competency", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Skill", "Human Resource Management", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Competency", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SkillLevel", "Human Resource Management", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Competency", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SkillXPersonXSkillLevel", "Human Resource Management", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Competency", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskXCompetency", "Human Resource Management", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Competency", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskPackage", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Priority", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskPriorityType", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Priority", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskPriorityXApplicationUser", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.Priority", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProjectUseCaseStatus", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProjectUseCaseStatusArchive", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProjectXUseCase", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCase", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCaseActor", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCaseActorXUseCase", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCasePackage", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCasePackageXUseCase", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCaseRelationship", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCaseStep", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCaseWorkflowCategory", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UseCaseXUseCaseStep", "Project Planning", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RequirementAnalysis", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Reward", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RiskReward", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Risk", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RiskReward", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskRiskRewardRankingXPerson", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.RiskReward", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Activity", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ActivityState", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DailyTaskItemQueue", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DailyTaskItemQueueStatus", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Schedule", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ScheduleDetail", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ScheduleHistory", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ScheduleItem", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ScheduleQuestion", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ScheduleState", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskAlgorithm", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskAlgorithmItem", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.Module.TimeTracking", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Application", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationOperation", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationOperationXApplicationRole", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationRole", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationUser", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationUserProfileImage", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationUserProfileImageMaster", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationUserTitle", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationUserXApplicationRole", "Authentication And Authorization", "Framework.ApplicationUser", "Framework.Components.ApplicationUser", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("AuditAction", "Common Services", "Framework.Audit", "Framework.Components.Audit", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("AuditHistory", "Common Services", "Framework.Audit", "Framework.Components.Audit", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Trace", "Common Services", "Framework.Audit", "Framework.Components.Audit", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TypeOfIssue", "Common Services", "Framework.Audit", "Framework.Components.Audit", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("AuditLog", "Common Services", "Framework.Audit", "Framework.Components.Audit", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationMode", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationModeXFieldConfigurationMode", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationSecurity", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DateRangeTitle", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FieldConfiguration", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FieldConfigurationDisplayName", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FieldConfigurationMode", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FieldconfigurationModeAccessMode", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FieldConfigurationModeCategory", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FieldConfigurationModeCategoryXFCMode", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FieldConfigurationModeXApplicationRole", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FieldConfigurationModeXApplicationUser", "Configuration", "Framework", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UserPreference", "Configuration", "Framework.Configuration", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UserPreferenceCategory", "Configuration", "Framework.Configuration", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UserPreferenceDataType", "Configuration", "Framework.Configuration", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UserPreferenceKey", "Configuration", "Framework.Configuration", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UserPreferenceSelectedItem", "Configuration", "Framework.Configuration", "Framework.Components.Configuration", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationEntityParentalHierachy", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationRoute", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationRouteParameter", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ConnectionString", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ConnectionStringXApplication", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Country", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("HelpPage", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("HelpPageContext", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Language", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Menu", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("MenuCategory", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("MenuCategoryXMenu", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("MenuDisplayName", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Report", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReportCategory", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReportXReportCategory", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SearchKey", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("QuickPaginationRun", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SearchKeyDetail", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SearchKeyDetailItem", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SystemEntityCategory", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SystemEntityType", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SystemDevNumbers", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SystemEntityXSystemEntityCategory", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SystemForeignRelationship", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SystemForeignRelationshipDatabase", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TabChildStructure", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TabParentStructure", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("SystemForeignRelationshipType", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ThemeCategory", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ThemeDetails", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ThemeKey", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Themes", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TimeZone", "Configuration", "Framework", "Framework.Components.Core", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationMonitoredEvent", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationMonitoredEventEmail", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationMonitoredEventProcessingState", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ApplicationMonitoredEventSource", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("NotificationEventType", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("NotificationPublisher", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("NotificationPublisherXEventType", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("NotificationPublisherXNotificationEventType", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("NotificationRegistrar", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("NotificationSubscriber", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("NotificationSubscriberXEventType", "Common Services", "Framework.EventMonitoring", "Framework.Components.EventMonitoring", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("BatchFile", "Common Services", "Framework.Import", "Framework.Components.Import", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("BatchFileHistory", "Common Services", "Framework.Import", "Framework.Components.Import", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("BatchFileSet", "Common Services", "Framework.Import", "Framework.Components.Import", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("BatchFileStatus", "Common Services", "Framework.Import", "Framework.Components.Import", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("FileType", "Common Services", "Framework.Import", "Framework.Components.Import", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DatabaseChangeLog", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Log4Net", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("RenumberMigration", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("StoredProcedureLog", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("StoredProcedureLogDetail", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("StoredProcedureLogRaw", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UserLogin", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UserLoginHistory", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("UserLoginStatus", "Logging And Tracing", "Framework.LogAndTrace", "Framework.Components.LogAndTrace", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DevelopmentCategory", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("PublishXDevelopment", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseFeature", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseIssueType", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseLog", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseLogDetail", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseLogDetailMapping", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseLogStatus", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseNoteBusinessDifficulty", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseNoteBusinessValue", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseNoteDeveloperValue", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseNoteLogisticsDifficulty", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseNoteQualitative", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseNotesWorkFlow", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleaseNotesTechnicalDifficulty", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ReleasePublishCategory", "Application Development", "Framework.ReleaseLog", "Framework.Components.ReleaseLog", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskEntity", "Task And Workflow", "Framework.TasksAndWorkflow", "Framework.Components.TasksAndWorkflow", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskEntityType", "Task And Workflow", "Framework.TasksAndWorkflow", "Framework.Components.TasksAndWorkflow", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskRun", "Task And Workflow", "Framework.TasksAndWorkflow", "Framework.Components.TasksAndWorkflow", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskSchedule", "Task And Workflow", "Framework.TasksAndWorkflow", "Framework.Components.TasksAndWorkflow", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskScheduleType", "Task And Workflow", "Framework.TasksAndWorkflow", "Framework.Components.TasksAndWorkflow", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TestCase", "Test Case Management", "DB TCM", "TestCaseManagement.Components.DataAccess", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TestCaseOwner", "Test Case Management", "DB TCM", "TestCaseManagement.Components.DataAccess", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TestCasePriority", "Test Case Management", "DB TCM", "TestCaseManagement.Components.DataAccess", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TestCaseStatus", "Test Case Management", "DB TCM", "TestCaseManagement.Components.DataAccess", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TestRun", "Test Case Management", "DB TCM", "TestCaseManagement.Components.DataAccess", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TestSuite", "Test Case Management", "DB TCM", "TestCaseManagement.Components.DataAccess", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TestSuiteXTestCase", "Test Case Management", "DB TCM", "TestCaseManagement.Components.DataAccess", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TestSuiteXTestCaseArchive", "Test Case Management", "DB TCM", "TestCaseManagement.Components.DataAccess", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ActivityXDeliverableArtifact", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Admin", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ClientXProject", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DeliverableArtifact", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("DeliverableArtifactStatus", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("EntityDateRangeState", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("EntityDateRangeStateType", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Layer", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Milestone", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("MilestoneFeatureState", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("MilestoneXFeature", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("MilestoneXFeatureArchive", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProductivityArea", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProductivityAreaFeature", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProductivityAreaFeatureXApplicationUser", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProductivityAreaXProductivityAreaFeature", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProjectPortfolio", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProjectPortfolioGroup", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("ProjectPortfolioGroupXProjectPortfolio", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("Question", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("QuestionCategory", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskRole", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("TaskXDeliverableArtifact", "Task Time Tracker", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				AllEntityDetailDataManager.SaveEntityDetails("VacationPlan", "Time Entry", "DB TaskTimeTracker", "TaskTimeTracker.Components.BusinessLayer", SessionVariables.RequestProfile);
				
			}
			Response.Redirect("~/Default.aspx");
		}


		protected void btnCancel_OnClick(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx");
		}
	}
}