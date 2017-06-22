using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.ApplicationUser;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using DataModel.Framework.DataAccess;
using ApplicationContainer.UI.Web.BaseUI;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis;

namespace Framework.UI.Web.BaseClasses
{	
	public abstract class ControlSearchFilterEntity : UserControl 
	{
		
		protected SearchFilterControl BaseSearchFilterControl;

		public virtual string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcHolder)
		{

			var configString = string.Empty;

			switch (fieldName)
			{
				case "ApplicationId":					
				case "SubscriberApplicationId":
				case "PublisherApplicationId":
				case "Application":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", plcHolder, true);
					break;

				case "ApplicationUserId":
				case "ApplicationUser":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationUserList", "FullName", "ApplicationUserId", plcHolder);
					break;

				case "UserName":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationUserList", "FullName", "FullName", plcHolder);
					break;

				case "Task":
				case "TaskId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetTaskList", "Name", "TaskId", plcHolder);
					break;

				case "CompetencyId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetCompetencyList", "Name", "CompetencyId", plcHolder);
					break;

				case "ScheduleDetailActivityCategory":
				case "ScheduleDetailActivityCategoryId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetScheduleDetailActivityCategoryList", "Name", "ScheduleDetailActivityCategoryId", plcHolder);
					break;

				case "Activity":
				case "ActivityId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetTaskList", "Name", "TaskId", plcHolder);
					break;

                case "WorkTicket":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetEntityList", "FullName", "WorkTicket", plcHolder);
                    break;

				case "ScheduleStateId":
				case "ScheduleStateName":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetScheduleStateList", "Name", "ScheduleStateId", plcHolder);
					break;

				case "DeliverableArtifactId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetDeliverableArtifactList", "Name", "DeliverableArtifactId", plcHolder);
					break;

				case "DeliverableArtifactStatusId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetDeliverableArtifactStatusList", "Name", "DeliverableArtifactStatusId", plcHolder);
					break;
				case "Person":
				case "PersonId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationUserList", "FullName", "ApplicationUserId", plcHolder);
					break;

                case "ProjectId":
				case "Project":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetProjectList", "Name", "ProjectId", plcHolder);
                    break;

                case "FeatureXFeatureRuleId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetProjectList", "Name", "ProjectId", plcHolder);
                    break;


				//case "ReleaseLogId":
				//	configString = AjaxHelper.GetKendoComboBoxConfigScript("GetReleaseLogList", "Name", "ReleaseLogId", plcHolder);
				//	break;

				case "ReleasePublishCategoryId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetReleasePublishCategoryList", "Name", "ReleasePublishCategoryId", plcHolder);
					break;

				case "ReleaseIssueTypeId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetReleaseIssueTypeList", "Name", "ReleaseIssueTypeId", plcHolder);
					break;

				case "ThemeId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetThemeList", "Name", "ThemeId", plcHolder);
					break;

				case "ThemeCategoryId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetThemeCategoryList", "Name", "ThemeCategoryId", plcHolder);
					break;

				case "ThemeKeyId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetThemeKeyList", "Name", "ThemeKeyId", plcHolder);
					break;

				case "Entity":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetEntityList", "items", "SystemEntityTypeId", plcHolder);
					break;

				//case "ModuleId":
				//	configString = AjaxHelper.GetKendoComboBoxConfigScript("GetModuleList", "Name", "ModuleId", plcHolder);
				//	break;

				//case "ReleaseFeatureId":
				//	configString = AjaxHelper.GetKendoComboBoxConfigScript("GetReleaseFeatureList", "Name", "ReleaseFeatureId", plcHolder);
				//	break;


				case "FunctionalityActiveStatus":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFunctionalityActiveStatusList", "Name", "Value", plcHolder);
					break;

				case "TaskEntityId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetTaskEntityList", "Name", "TaskEntityId", plcHolder);
					break;

                case "TaskTypeId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetTaskTypeList", "Name", "TaskTypeId", plcHolder);
                    break;

				case "TaskScheduleId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetTaskScheduleList", "Name", "TaskScheduleId", plcHolder);
					break;

				case "TaskPackageId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetTaskPackageList", "Name", "TaskPackageId", plcHolder);
					break;

				case "FunctionalityImage":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFunctionalityImageList", "Name", "FunctionalityImageId", plcHolder);
					break;

				case "Title":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFunctionalityImageList", "Name", "FunctionalityImageId", plcHolder);
					break;

				case "ProfileTitle":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationUserProfileImageMasterList", "Title", "Title", plcHolder);
					break;

				case "FeatureOwnerStatusId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFeatureOwnerStatusList", "Name", "FeatureOwnerStatusId", plcHolder);
					break;

				case "DeveloperRoleId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetDeveloperRoleList", "Name", "DeveloperRoleId", plcHolder);
					break;

				case "FunctionalityImageAttribute":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFunctionalityImageAttributeList", "Name", "FunctionalityImageAttributeId", plcHolder);
					break;

				case "Module":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetModuleList", "Name", "ModuleId", plcHolder);
					break;

				case "FunctionalityId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFunctionalityList", "Name", "FunctionalityId", plcHolder);
					break;

				case "QuestionCategoryId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetQuestionCategoryList", "Name", "QuestionCategoryId", plcHolder);
					break;
				case "Category":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetQuestionCategoryList", "Name", "QuestionCategoryId", plcHolder);
					break;
				//case "SystemEntityType":
				//case "SystemEntityTypeId":
				//case "PrimaryEntity":
				//	configString = AjaxHelper.GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId", plcHolder);
				//	break;

				case "SubscriberApplicationRoleId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetSubscriberApplicationRoleList", "Name", "SubscriberApplicationRoleId", plcHolder);
					break;

				case "ParentMenuId":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetParentMenuList", "MenuDisplayName", "MenuId", 
						plcHolder, BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

				case "ModuleId":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetModuleList", "Name", "ModuleId", 
						plcHolder, BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

                case "NeedId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetNeedList", "Name", "NeedId", plcHolder);
                    break;

                case "UseCaseId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetUseCaseList", "Name", "UseCaseId", plcHolder);
                    break;

                case "ProjectUseCaseStatusId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetProjectUseCaseStatusList", "Name", "ProjectUseCaseStatusId", plcHolder);
                    break;

                case "UseCaseWorkFlowCatgeoryId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetUseCaseWorkFlowCatgeoryList", "Name", "UseCaseWorkFlowCatgeoryId", plcHolder);
                    break;

                case "UseCaseStepId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetUseCaseStepList", "Name", "UseCaseStepId", plcHolder);
                    break;

                case "UseCaseActorId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetUseCaseActorList", "Name", "UseCaseActorId",
                        plcHolder);
                    break;

                case "UseCaseRelationshipId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetUseCaseRelationshipList", "Name", "UseCaseRelationshipId",
                        plcHolder);
                    break;

                case "FeatureId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFeatureList", "Name", "FeatureId", plcHolder);
                    break;

				case "EntityId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetEntityList", "EntityName", "SystemEntityTypeId", plcHolder);
					break;

                case "FeatureRuleId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFeatureRuleList", "Name", "FeatureRuleId",
                        plcHolder);
                    break;

                case "FeatureRuleStatusId":
                    configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFeatureRuleStatusList", "Name", "FeatureRuleStatusId",
                        plcHolder);
                    break;

				case "TabParentStructureId":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetTabParentStructureList", "Name", "TabParentStructureId",
						plcHolder, BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

				case "ReleaseFeatureId":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetReleaseFeatureList", "Name", "ReleaseFeatureId",
						plcHolder, BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

				case "ReleaseLogId":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetReleaseLogList", "Name", "ReleaseLogId",
						plcHolder, BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

				case "NotificationEventTypeId":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetNotificationEventTypeList", "Name", "ReleaseLogId",
						plcHolder, BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

				case "NotificationPublisherId":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetNotificationPublisherList", "Name", "ReleaseLogId",
						plcHolder, BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

				case "SystemEntityTypeId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId", plcHolder);
					break;

				case "SystemEntityType":
				//case "SystemEntityTypeId":
				case "PrimaryEntity":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId",
						plcHolder , BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

				case "FieldConfigurationModeId":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetFieldConfigurationModeList", "Name", "FieldConfigurationModeId",
						plcHolder, BaseSearchFilterControl.TextBoxApplicationIdClientId);
					break;

				case "EntityDateRangeStateTypeId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetEnityDateRangeStateList", "Name", "EntityDateRangeStateId", plcHolder);
					break;

				case "UserLoginStatusId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetUserLoginStatusList", "UserLoginStatusCode", "UserLoginStatusId", plcHolder);
					break;

				case "ExcludeItems":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("CheckExcludeItemsListBoxItems", "Name", "Value", plcHolder,false);
					break;

				case "QuestionId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetScheduleQuestionList", "QuestionPhrase", "QuestionId", plcHolder);
					break;
				default:
					break;
			}

			return configString;
		}

		public virtual void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			IEnumerable lstSource = null;
			
			if (fieldName.Equals("FunctionalityId"))
			{
				lstSource = FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(lstSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					FunctionalityDataModel.DataColumns.FunctionalityId);
			}
			else if (fieldName.Equals("ProjectId"))
			{
				lstSource = ProjectDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(lstSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					ProjectDataModel.DataColumns.ProjectId);
			}
			else if (fieldName.Equals("DeveloperRoleId"))
			{
				lstSource = DeveloperRoleDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(lstSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					DeveloperRoleDataModel.DataColumns.DeveloperRoleId);
			}
            else if (fieldName.Equals("UseCaseId"))
            {
                lstSource = UseCaseDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(lstSource, dropDownListControl,
                    StandardDataModel.StandardDataColumns.Name,
                    UseCaseDataModel.DataColumns.UseCaseId);
            }

            else if (fieldName.Equals("NeedId"))
            {
                lstSource = NeedDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(lstSource, dropDownListControl,
                    StandardDataModel.StandardDataColumns.Name,
                    NeedDataModel.DataColumns.NeedId);
            }

            else if (fieldName.Equals("ProjectUseCaseStatusId"))
            {
                lstSource = ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(lstSource, dropDownListControl,
                    StandardDataModel.StandardDataColumns.Name,
                    ProjectUseCaseStatusDataModel.DataColumns.ProjectUseCaseStatusId);
            }
			else if (fieldName.Equals("FeatureOwnerStatusId"))
			{
				lstSource = FeatureOwnerStatusDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(lstSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId);
			}         

            else if (fieldName.Equals("EntityId"))
            {
				lstSource = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(lstSource, dropDownListControl,
                    StandardDataModel.StandardDataColumns.Name,
                    DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
            }

            else if (fieldName.Equals("ProjectId"))
            {
                lstSource = ProjectDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(lstSource, dropDownListControl,
                    StandardDataModel.StandardDataColumns.Name,
                    ProjectDataModel.DataColumns.ProjectId);
            }
          
			else if (fieldName.Equals("ApplicationId"))
			{
				var applicationData = ApplicationDataManager.GetList(SessionVariables.RequestProfile);
                applicationData = applicationData.OrderBy(x => x.Name).ToList();

                UIHelper.LoadDropDown(applicationData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					BaseDataModel.BaseDataColumns.ApplicationId);
			}
			else if (fieldName.Equals("FunctionalityOwner"))
			{
				var drData = FunctionalityOwnerDataManager.GetList(SessionVariables.RequestProfile);
                CommonSearchParameters();

                var finalData = new List<FunctionalityOwnerDataModel>();

                foreach (FunctionalityOwnerDataModel item in lstSource)
                {
                    if (!finalData.Where(x => x.Developer == item.Developer).Any())
                    {
                        finalData.Add(item);
                    }
                }
                UIHelper.LoadDropDown(finalData, dropDownListControl,
					FunctionalityOwnerDataModel.DataColumns.Developer,
				FunctionalityOwnerDataModel.DataColumns.FunctionalityOwnerId);
			}
			else if (fieldName.Equals("FunctionalityActiveStatus"))
			{
				var drData = FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
				CommonSearchParameters();
				UIHelper.LoadDropDown(drData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
				FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);
			}
			else if (fieldName.Equals("FunctionalityImage"))
			{
				var drData = FunctionalityImageDataManager.GetList(SessionVariables.RequestProfile);
				CommonSearchParameters();
				UIHelper.LoadDropDown(drData, dropDownListControl,
					FunctionalityImageDataModel.DataColumns.Title,
				FunctionalityImageDataModel.DataColumns.FunctionalityImageId);
			}
			else if (fieldName.Equals("FunctionalityImageAttribute"))
			{
				var drData = FunctionalityImageAttributeDataManager.GetList(SessionVariables.RequestProfile);
				CommonSearchParameters();
				UIHelper.LoadDropDown(drData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
				FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId);
			}
			else if (fieldName.Equals("TabParentStructure"))
			{
				var drData = TabParentStructureDataManager.GetList(SessionVariables.RequestProfile);
				CommonSearchParameters();
				UIHelper.LoadDropDown(drData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
				TabParentStructureDataModel.DataColumns.TabParentStructureId);
			} 
		}

		public virtual void LoadListBoxSources(string fieldName, ListBox lstBoxControl)
		{
			if (fieldName.Equals("CurrentActiveStatus"))
			{
				lstBoxControl.Items.Add(new ListItem("All", "-1"));
				var functionalityStatusData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
				functionalityStatusData = functionalityStatusData.OrderBy(x => x.Name).ToList();

                UIHelper.LoadDropDown(functionalityStatusData, lstBoxControl,
					StandardDataModel.StandardDataColumns.Name,
					FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);

				lstBoxControl.SelectedValue = "-1";
			}

		}

		public virtual void LoadCheckBoxListSources(string fieldName, CheckBoxList checkBoxListControl)
		{
			if (fieldName.Equals("Tabs"))
			{
				//checkBoxListControl.Items.Add(new ListItem("All", "-1"));
				checkBoxListControl.Items.Add("Schedule");
				checkBoxListControl.Items.Add("Statistic Info");
				checkBoxListControl.Items.Add("Charts & Graphs");
				checkBoxListControl.Items.Add("Angular Stats");
				checkBoxListControl.Items.Add("Angular Chart");				
			}
		}

		public SearchFilterControl GetFilter(SystemEntity entity, string key)
		{
			BaseSearchFilterControl.HookUp(entity, key);

			return BaseSearchFilterControl;
		}

		public virtual void CommonSearchParameters()
        {           
			// call this 
			BaseSearchFilterControl.GetParameterValue("GroupBy");
			BaseSearchFilterControl.GetParameterValue("SubGroupBy");			
        }

	}
}
