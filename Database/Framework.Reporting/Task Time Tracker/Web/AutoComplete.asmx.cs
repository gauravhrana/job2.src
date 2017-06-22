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

namespace TaskTimeTracker.UI.Web
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
		public List<StandardListDataModel> GetClientList()
		{
			var items = TaskTimeTracker.Components.BusinessLayer.ClientDataManager.GetClientList(SessionVariables.RequestProfile);
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
        public List<DataModel.Framework.Core.SystemEntityTypeDataModel> GetEntityList()
        {
            var items = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityList(SessionVariables.RequestProfile);
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
            var items = TaskTimeTracker.Components.BusinessLayer.ProjectDataManager.GetProjectList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.TaskTimeTracker.ApplicationDevelopment.ModuleDataModel> GetModuleList()
		{
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetModuleList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<StandardListDataModel> GetFunctionalityActiveStatusList()
		{
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetFunctionalityActiveStatusList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.TasksAndWorkFlow.TaskEntityDataModel> GetTaskEntityList()
		{
			var items = Framework.Components.TasksAndWorkflow.TaskEntity.GetTaskEntityList(SessionVariables.RequestProfile); 
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.TasksAndWorkFlow.TaskScheduleDataModel> GetTaskScheduleList()
		{
			var items = Framework.Components.TasksAndWorkflow.TaskSchedule.GetTaskScheduleList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.TaskScheduleId).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<StandardListDataModel> GetFunctionalityOwnerList()
		{
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetFunctionalityOwnerList(SessionVariables.RequestProfile);
			var result =	items.Select(item => new StandardListDataModel()
							{ Name = item.Name}).Distinct();
					return result.OrderBy(o => o.Name).ToList();
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
			var items = Framework.Components.ReleaseLog.ReleaseFeatureDataManager.GetReleaseFeatureList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList();
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.ReleaseLog.ReleasePublishCategoryDataModel> GetReleasePublishCategoryList()
		{
			var items = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetReleasePublishCategoryList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.ReleaseLog.ReleaseIssueTypeDataModel> GetReleaseIssueTypeList()
		{
			var items = Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.GetReleaseIssueTypeList(SessionVariables.RequestProfile);
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
            var items = TaskTimeTracker.Components.Module.TimeTracking.ScheduleQuestionDataManager.GetScheduleQuestionList(SessionVariables.RequestProfile);
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
        public List<DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel> GetApplicationList()
		{
			var items = Framework.Components.ApplicationUser.ApplicationDataManager.GetApplicationList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.Name).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel> GetApplicationUserList()
		{
			var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetApplicationUserList(SessionVariables.RequestProfile);
            return items.OrderBy(o => o.FullName).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Audit.AuditAction> GetAuditActionList()
		{
			var items = Framework.Components.Audit.AuditActionDataManager.GetEntityList(SessionVariables.RequestProfile);
			return items.OrderBy(o => o.Name).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<DataModel.Framework.Audit.Trace> GetTraceList()
		{
			var items = Framework.Components.Audit.TraceDataManager.GetEntityList(SessionVariables.AuditId);
			return items.OrderBy(o => o.Name).ToList(); 
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<Framework.Components.UserPreference.UserPreferenceDataTypeDataModel> GetUserPreferenceDataTypeList()
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
        public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityDataModel> GetFunctionalityList()
        {
            var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityDataModel();
            var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityDataModel>();

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetFunctionalityList(SessionVariables.RequestProfile);

            for (var i = 0; i < items.Count(); i++)
            {
                var oData = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityDataModel();
                var item = items[i];
                oData.FunctionalityId = (int)item.FunctionalityId;
                oData.Name = item.Name.ToString();

                dataList.Add(oData);
            }

            return dataList.OrderBy(o => o.Name).ToList();
        }

		//public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel> GetFunctionalityOwnerList()
		//{
		//	var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel();
		//	var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel>();

		//	var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetFunctionalityOwnerList(SessionVariables.AuditId);

		//	for (var i = 0; i < items.Count(); i++)
		//	{
		//		var oData = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityOwnerDataModel();
		//		var item = items[i];
		//		oData.FunctionalityOwnerId = (int)item.FunctionalityOwnerId;
		//		oData.Developer = item.Developer.ToString();

		//		dataList.Add(oData);
		//	}

		//	return dataList.OrderBy(o => o.Developer).ToList();
		//}

		//public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel> GetFunctionalityActiveStatusList()
		//{
		//	var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel();
		//	var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetFunctionalityActiveStatusList(SessionVariables.AuditId);

		//	var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel>();
				

		//	for (var i = 0; i < items.Count(); i++)
		//	{
		//		var oData = new DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityActiveStatusDataModel();
		//		var item = items[i];
		//		oData.FunctionalityActiveStatusId = (int)item.FunctionalityActiveStatusId;
		//		oData.Name = item.Name.ToString();

		//		dataList.Add(oData);
		//	}

		//	return items.OrderBy(o => o.Name).ToList();
		//}

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DataModel.TaskTimeTracker.ApplicationDevelopment.FeatureOwnerStatusDataModel> GetFeatureOwnerStatusList()
        {
            var data = new DataModel.TaskTimeTracker.ApplicationDevelopment.FeatureOwnerStatusDataModel();
            var dataList = new List<DataModel.TaskTimeTracker.ApplicationDevelopment.FeatureOwnerStatusDataModel>();

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.GetFeatureOwnerStatausList(SessionVariables.RequestProfile);

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
				var oData = new DataModel.Framework.Configuration.FieldConfigurationDataModel();
				var item = items[i];				

				if (!item.Name.Contains("GroupBy") && !LstGroupByItems.Contains(item.Name))
				{
					if (item.Name.Contains("Id"))
					{
						oData.Name = item.Name.Remove(item.Name.Count() - 2, 2);
					}
					else
					{
						oData.Name = item.Name.ToString();
					}
					oData.FieldConfigurationDisplayName = item.FieldConfigurationDisplayName;
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
			}
			return LstGroupByItems;
		}

		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetAutoCompleteList(string primaryEntity, string txtName, int AuditId)
		{

			DataTable dt = null;
			var lstNames = new List<string>();

			if (primaryEntity == "Client")
			{
				var list = TaskTimeTracker.Components.BusinessLayer.ClientDataManager.GetList(SessionVariables.RequestProfile);
				lstNames = list.Select(item => item.Name).ToList();
				return lstNames.ToArray();
			}
			else if (primaryEntity == "Project")
			{
                dt = TaskTimeTracker.Components.BusinessLayer.ProjectDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "Layer")
			{
                dt = TaskTimeTracker.Components.BusinessLayer.LayerDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "Feature")
			{
                dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);
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
			else if (primaryEntity == "ApplicationRole")
			{
				dt = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
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
                dt = Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "UseCaseActor")
			{
                dt = Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "UseCaseStep")
			{
                dt = Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "UseCasePackage")
			{
                dt = Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "UseCaseRelationship")
			{
                dt = Components.BusinessLayer.RequirementAnalysis.UseCaseRelationshipDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "UseCaseWorkFlowCatgeory")
			{
                dt = Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "ProjectUseCaseStatus")
			{
                dt = Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
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
                    dt = Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
				}
				else if (txtName == "ProjectUseCaseStatus")
				{
                    dt = Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
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
                dt = Components.Module.Competency.SkillDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "SkillLevel")
			{
                dt = Components.Module.Competency.SkillLevelDataManger.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "Competency")
			{
                dt = Components.Module.Competency.CompetencyDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "ScheduleState") 
			{
                dt = TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "ProductivityArea")
			{
                dt = Components.BusinessLayer.ProductivityAreaDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "ProductivityAreaFeature")
			{
                dt = Components.BusinessLayer.ProductivityAreaFeatureDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "ProjectPortfolio")
			{
                dt = Components.BusinessLayer.ProjectPortfolioDataManager.GetList(SessionVariables.RequestProfile);
			}
			else if (primaryEntity == "ProjectPortfolioGroup")
			{
                dt = Components.BusinessLayer.ProjectPortfolioGroupDataManager.GetList(SessionVariables.RequestProfile);
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
					lstNames.Add("Human Resource");
					lstNames.Add("Location");
					lstNames.Add("Logging And Trace");
					lstNames.Add("Project Planning");
					lstNames.Add("Task Time Tracker");
					lstNames.Add("TaskAndWorkFlow");
					lstNames.Add("Test Case Management");
					lstNames.Add("Time Entry");

					return lstNames.ToArray();
				}
				else if (txtName == "DBProjectName")
				{
					lstNames.Add("DB Full TTT");
					lstNames.Add("DB TTT");
					lstNames.Add("DB Full Framework");
					lstNames.Add("DB TaskTimeTracker");
					lstNames.Add("DB Framework");

					return lstNames.ToArray();
				}
				else if (txtName == "DBComponentName")
				{
					lstNames.Add("Frameowkr.Component.Core");
					lstNames.Add("Frameowkr.Component.EventMonitoring");
					lstNames.Add("Frameowkr.Components.ReleaseLog");
					lstNames.Add("Framewokr.Component.Core");
					lstNames.Add("Framewokr.Component.UserPreference");
					lstNames.Add("Framework.Component.ApplicationUser");
					lstNames.Add("Framework.Component.Audit");
					lstNames.Add("Framework.Component.Configuration");
					lstNames.Add("Framework.Component.Import");
					lstNames.Add("Framework.Component.LogAndTrace");
					lstNames.Add("TaskTimeTracker.Component.BusinessLayer");
					lstNames.Add("TaskTimeTracker.Component.ApplicationDevelopment");
					lstNames.Add("TaskTimeTracker.Component.Competency");
					lstNames.Add("TaskTimeTracker.Component.RiskAndReward");
					lstNames.Add("TaskTimeTracker.Component.TimeTracking");
					lstNames.Add("TTT.Component.ApplicationDevelopment");
					lstNames.Add("TTT.Component.BusinessLayer");
					lstNames.Add("TTT.Component.Module.Competency");
					lstNames.Add("TTT.Component.Module.RiskAndReward");
					lstNames.Add("TTT.Components.Module.TimeTracking");
					lstNames.Add("TTT.Componets.Module.RequirementAnalysis");

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
			else
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
            dt1 = Components.Module.TimeTracking.ScheduleDataManager.GetSampleSearch();

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
		//	dt = Components.Module.TimeTracking.Schedule.GetSampleSearch();


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