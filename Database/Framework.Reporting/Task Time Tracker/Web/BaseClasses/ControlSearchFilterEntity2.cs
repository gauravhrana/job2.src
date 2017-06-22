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
using System.Web.UI.HtmlControls;

namespace Framework.UI.Web.BaseClasses
{
	
	public abstract class ControlSearchFilterEntity2 : UserControl
	{

		protected SearchFilterControl2 BaseSearchFilterControl;

		public virtual string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, HtmlGenericControl divControlContainer)
		{

			var configString = string.Empty;

			switch (fieldName)
			{
				case "ApplicationId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", divControlContainer, false);
					break;

				case "ApplicationUserId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationUserList", "FullName", "ApplicationUserId", divControlContainer);
					break;

				case "EntityId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetEntityList", "FullName", "EntityId", divControlContainer);
					break;

				case "ScheduleStateId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetScheduleStateList", "Name", "ScheduleStateId", divControlContainer);
					break;

				case "PersonId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationUserList", "FullName", "ApplicationUserId", divControlContainer);
					break;

				case "ProjectId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetProjectList", "Name", "ProjectId", divControlContainer);
					break;

				case "ReleaseLogId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetReleaseLogList", "Name", "ReleaseLogId", divControlContainer);
					break;

				case "ReleasePublishCategoryId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetReleasePublishCategoryList", "Name", "ReleasePublishCategoryId", divControlContainer);
					break;

				case "ReleaseIssueTypeId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetReleaseIssueTypeList", "Name", "ReleaseIssueTypeId", divControlContainer);
					break;

				case "PrimaryEntity":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId", divControlContainer);
					break;

				case "ModuleId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetModuleList", "Name", "ModuleId", divControlContainer);
					break;

				case "ReleaseFeatureId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetReleaseFeatureList", "Name", "ReleaseFeatureId", divControlContainer);
					break;

				case "ScheduleDetailActivityCategoryId":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetScheduleDetailActivityCategoryList", "Name", "ScheduleDetailActivityCategoryId", divControlContainer);
					break;

				case "FunctionalityOwner":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFunctionalityOwnerList", "Name", "Value", divControlContainer);
					break;

				case "Application":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", divControlContainer);
					break;

				case "FunctionalityActiveStatus":
					configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFunctionalityActiveStatusList", "Name", "Value", divControlContainer);
					break;
				default:
					break;
			}

			return configString;
		}

		public virtual void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			DataTable dataSource = null;
			if (fieldName.Equals("Application"))
			{
				dataSource = ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dataSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					BaseDataModel.BaseDataColumns.ApplicationId);

				dropDownListControl.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();
			}
			else if (fieldName.Equals("ParentMenu"))
			{
				var data = new MenuDataModel();

				var appId = BaseSearchFilterControl.GetParameterValueAsInt("Application");
				if (appId.HasValue)
				{
					data.ApplicationId = appId.Value;
				}
				else
				{
					data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
				}

				dataSource = MenuDataManager.ListOfParentMenuOnly(data, SessionVariables.RequestProfile);
				dataSource.DefaultView.Sort = StandardDataModel.StandardDataColumns.Name + " ASC";
				dataSource = dataSource.DefaultView.ToTable();

				dropDownListControl.Items.Clear();

				dropDownListControl.Items.Add(new ListItem("All", "-1"));

				dropDownListControl.DataSource = dataSource;
				dropDownListControl.DataTextField = StandardDataModel.StandardDataColumns.Name;
				dropDownListControl.DataValueField = MenuDataModel.DataColumns.MenuId;

				dropDownListControl.DataBind();
			}
			else if (fieldName.Equals("FunctionalityId"))
			{
				dataSource = FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dataSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					FunctionalityDataModel.DataColumns.FunctionalityId);
			}
			else if (fieldName.Equals("DeveloperRoleId"))
			{
				dataSource = DeveloperRoleDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dataSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					DeveloperRoleDataModel.DataColumns.DeveloperRoleId);
			}
			else if (fieldName.Equals("FeatureOwnerStatusId"))
			{
				dataSource = FeatureOwnerStatusDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dataSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId);
			}

			else if (fieldName.Equals("EntityId"))
			{
				dataSource = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dataSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
			}

			else if (fieldName.Equals("ProjectId"))
			{
                dataSource = ProjectDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dataSource, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					ProjectDataModel.DataColumns.ProjectId);
			}
			else if (fieldName.Equals("ApplicationId"))
			{
				var applicationData = ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				var dv = applicationData.DefaultView;
				dv.Sort = "Name ASC";
				UIHelper.LoadDropDown(dv.ToTable(), dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					BaseDataModel.BaseDataColumns.ApplicationId);
			}
			else if (fieldName.Equals("FunctionalityOwner"))
			{
				var drData = FunctionalityOwnerDataManager.GetList(SessionVariables.RequestProfile);
				CommonSearchParameters();
				var originalList = new ArrayList();
				var duplicateList = new ArrayList();
				foreach (DataRow dtRow in drData.Rows)
				{
					if (originalList.Contains(dtRow["Developer"]))
					{
						duplicateList.Add(dtRow);
					}
					else
					{
						originalList.Add(dtRow["Developer"]);
					}
				}
				foreach (DataRow dtRow in duplicateList)
				{
					drData.Rows.Remove(dtRow);
				}
				UIHelper.LoadDropDown(drData, dropDownListControl,
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
		}

		public SearchFilterControl2 GetFilter(SystemEntity entity, string key)
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
