using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.ApplicationUser;
using Framework.Components.Core;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Globalization;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using System.Text;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using ControlSearchFilter = Framework.UI.Web.BaseClasses.ControlSearchFilter;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {

        #region variables

        public bool ShowGraph
        {
            get { return chkShowGraph.Checked;  }
            set { chkShowGraph.Checked = value; }
        }

        public FunctionalityEntityStatusDataModel SearchParameters
        {
            get
            {
				Log4Net.LogInfo("Step2 Load SearchParameters Data Object START", "SearchParameter", SessionVariables.RequestProfile.ApplicationId);

                var data = new FunctionalityEntityStatusDataModel();

                var isFiltered = false;				

                if (SearchParametersRepeater.Items.Count != 0)
                {
                    var columnName = FunctionalityEntityStatusDataModel.DataColumns.SystemEntityType;

                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(columnName + "Visibility", SettingCategory))
                    {
                        if (!CheckAndGetFieldValue(columnName).ToString().Equals("-1") && !CheckAndGetFieldValue(columnName).ToString().Equals("All"))
                        {
                            var systemEntityType = CheckAndGetFieldValue(columnName).ToString();
							
							if (!string.IsNullOrEmpty(systemEntityType))
							{
								var sysdata = new SystemEntityTypeDataModel();
								sysdata.EntityName = systemEntityType;

								var sysdt = SystemEntityTypeDataManager.Search(sysdata, SessionVariables.RequestProfile);

								if (sysdt.Rows.Count == 1)
								{
									data.SystemEntityTypeId = int.Parse(sysdt.Rows[0][FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId].ToString());
								}

								isFiltered = true;
							}                            							
                        }
                    }

                    columnName = FunctionalityEntityStatusDataModel.DataColumns.Functionality;

                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(columnName + "Visibility", SettingCategory))
                    {
						if (!CheckAndGetFieldValue(columnName).ToString().Equals("-1") && !CheckAndGetFieldValue(columnName).ToString().Equals("All"))
                        {
                            //var functionality = CheckAndGetFieldValue(columnName).ToString();
							data.ApplicationId = GetParameterValueAsInt(FunctionalityEntityStatusDataModel.DataColumns.ApplicationId);

							data.FunctionalityId = GetParameterValueAsInt(FunctionalityEntityStatusDataModel.DataColumns.Functionality);

							if (data.FunctionalityId != -1)
							{
								isFiltered = true;
							}
                        
							//var funData = new FunctionalityDataModel();

							//if (!string.IsNullOrEmpty(functionality))
							//{
							//	funData.FunctionalityId = int.Parse(functionality);
							//	var fundt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Search(funData, SessionVariables.RequestProfile);
								
							//	if (fundt.Rows.Count == 1)
							//	{
							//		data.FunctionalityId = int.Parse(fundt.Rows[0][FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId].ToString());
							//	}

							//	
							//}
                        }
                    }
                
                    columnName = FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriority;
                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(columnName + "Visibility", SettingCategory))
                    {
                        if (Convert.ToInt32(CheckAndGetFieldValue(columnName)) != -1)
                        {
                            data.FunctionalityPriorityId = Convert.ToInt32(CheckAndGetFieldValue(columnName).ToString());
                            isFiltered = true;
                        }
                    }

                    columnName = FunctionalityEntityStatusDataModel.DataColumns.FunctionalityActiveStatus;
                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(columnName + "Visibility", SettingCategory))
                    {
                        if (Convert.ToInt32(CheckAndGetFieldValue(columnName)) != -1)
                        {
                            data.FunctionalityActiveStatusId = Convert.ToInt32(CheckAndGetFieldValue(columnName).ToString());
                            isFiltered = true;
                        }
                    }

                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean("GroupByVisibility", SettingCategory))
                    {
                        if (CheckAndGetFieldValue("GroupBy").ToString() != "-1")
                        {
                            GroupBy = CheckAndGetFieldValue("GroupBy").ToString();
                        }
                    }

                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean("SubGroupByVisibility", SettingCategory))
                    {
                        if (CheckAndGetFieldValue("SubGroupBy").ToString() != "-1")
                        {
                            SubGroupBy = CheckAndGetFieldValue("SubGroupBy").ToString();
                        }
                    }    
                    
                    columnName = FunctionalityEntityStatusDataModel.DataColumns.AssignedTo;
                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(columnName + "Visibility", SettingCategory))
                    {
                        if (!string.IsNullOrEmpty(CheckAndGetFieldValue(columnName).ToString()))
                        { 
                            data.AssignedTo = UIHelper.RefineAndGetSearchText(CheckAndGetFieldValue(columnName).ToString(), SettingCategory);
                            isFiltered = true;
                        }
                    }

                    columnName = FunctionalityEntityStatusDataModel.DataColumns.StartDate;
                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(columnName + "Visibility", SettingCategory))
                    {
                        var date = CheckAndGetFieldValue(columnName).ToString();
                        if (!string.IsNullOrEmpty(date))
                        {
                            var dates = date.Split('&');
                            if (Boolean.Parse(dates[2]))
                            {
                                data.StartDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
                                data.StartDateR2 = DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);
                                if (data.StartDate != null || data.StartDateR2 != null)
                                { 
                                    isFiltered = true;
                                }
                            }
                        }
                    }

                    columnName = FunctionalityEntityStatusDataModel.DataColumns.TargetDate;
                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(columnName + "Visibility", SettingCategory))
                    {
                        var date = CheckAndGetFieldValue(columnName).ToString();
                        if (!string.IsNullOrEmpty(date))
                        {
                            var dates = date.Split('&');
                            if (Boolean.Parse(dates[2]) && !string.IsNullOrEmpty(dates[0]) && !string.IsNullOrEmpty(dates[1]))
                            {
                                data.TargetDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
                                data.TargetDateR2 = DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);
                                if (data.TargetDate != null || data.TargetDateR2 != null)
                                {
                                    isFiltered = true;
                                }
                            }
                        }
                    }

                    columnName = "ShowGraph";
                    PerferenceUtility.UpdateUserPreference(SettingCategory, columnName, chkShowGraph.Checked.ToString());

                }

                if (!isFiltered)
                {
                    // fake/wrong search condition which will always return 0 results 
                    //data.FunctionalityPriorityId = 0;
                }

				Log4Net.LogInfo("Step2 Load SearchParameters Data Object END", "SearchParameter", SessionVariables.RequestProfile.ApplicationId);
                return data;
            }
        }
        
        #endregion

        #region methods		

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("Application"))
			{
				var applicationData = ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				var dv = applicationData.DefaultView;
				dv.Sort = "Name ASC";
				UIHelper.LoadDropDown(dv.ToTable(), dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					BaseDataModel.BaseDataColumns.ApplicationId);

				dropDownListControl.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();
			}
			else if (fieldName.Equals("systemEntityType"))
			{
				var systemEntityData = FunctionalityEntityStatusDataManager.GetUniqueIdList(
				"systemEntityType", SessionVariables.RequestProfile);
				var systemEntityDataList = SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
				var filteredsystemEntityData = GetFilteredList("systemEntityType", systemEntityData, systemEntityDataList);
				filteredsystemEntityData.DefaultView.Sort = SystemEntityTypeDataModel.DataColumns.EntityName + " ASC";
				var sorteddt = filteredsystemEntityData.DefaultView.ToTable();
				UIHelper.LoadDropDown(sorteddt, dropDownListControl, SystemEntityTypeDataModel.DataColumns.EntityName,
					SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
			}

			else if (fieldName.Equals("Functionality"))
			{
				var functionalityData = FunctionalityEntityStatusDataManager.GetUniqueIdList(
				"Functionality", SessionVariables.RequestProfile);
				var functionalityDataList = FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
				var filteredfunctionalityData = GetFilteredList("Functionality", functionalityData, functionalityDataList);
				UIHelper.LoadDropDown(filteredfunctionalityData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
				FunctionalityDataModel.DataColumns.FunctionalityId);
			}

			else if (fieldName.Equals("FunctionalityPriority"))
			{
				var functionalityPriorityData = FunctionalityPriorityDataManager.GetList(SessionVariables.RequestProfile);
				var dv = functionalityPriorityData.DefaultView;
				dv.Sort = "Name ASC";
				UIHelper.LoadDropDown(dv.ToTable(), dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					FunctionalityPriorityDataModel.DataColumns.FunctionalityPriorityId);
			}
			else if (fieldName.Equals("FunctionalityActiveStatus"))
			{
				var functionalityActiveStatusData = FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
				var dv = functionalityActiveStatusData.DefaultView;
				dv.Sort = "Name ASC";
				UIHelper.LoadDropDown(dv.ToTable(), dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);
			}

			dropDownListControl.SelectedIndex = 0;
		}

		public override void LoadListBoxSources(string fieldName, ListBox lstBoxControl)
		{
			if (fieldName.Equals("FunctionalityStatus"))
			{
				lstBoxControl.Items.Add(new ListItem("All", "-1"));
				var functionalityStatusData = FunctionalityStatusDataManager.GetDetails(FunctionalityStatusDataModel.Empty,SessionVariables.RequestProfile);
				var dv = functionalityStatusData.DefaultView;
				dv.Sort = "Name ASC";
				UIHelper.LoadDropDown(dv.ToTable(), lstBoxControl,
					StandardDataModel.StandardDataColumns.Name,
					FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId);

				lstBoxControl.SelectedValue = "-1";
			}

		}

		public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		{
			if (fieldName == "ApplicationId")
			{
				TextBoxApplicationIdClientId = txtBox.ClientID;
			}
			
			var configString = string.Empty;
			switch(fieldName)
			{
				case "Functionality":
					configString = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetFunctionalityList", "Name", "FunctionalityId",
						plcControlHolder, TextBoxApplicationIdClientId);
				break; 

				case "SystemEntityType":
				configString = AjaxHelper.GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "systemEntityTypeId", plcControlHolder);				
				break;

				case"ApplicationId":
				configString =  AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", plcControlHolder);
				break;

				case "FunctionalityActiveStatus":
				configString = AjaxHelper.GetKendoComboBoxConfigScript("GetFunctionalityActiveStatusList", "Name", "FunctionalityActiveStatusId", plcControlHolder);
				break;

			}
			return configString;
		}

        #endregion

        #region Events

        protected void SearchParametersRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var plcControlHolder = (PlaceHolder)e.Item.FindControl("plcControlHolder"); 
                var oDateRange = (DateRangeControl)e.Item.FindControl("oDateRange");

                if (oDateRange != null)
                {                    
                    oDateRange.Key = e.Item.ItemIndex.ToString();
                    var funccall = "Fillup" + oDateRange.GetKey() + "();";
                    oDateRange.DateRangeDropDown.Attributes.Add("onchange", funccall);
                    funccall = "chkdate_checkedchanged" + oDateRange.GetKey() + "();";
                    oDateRange.DateRangeCheckBox.Attributes.Add("onclick", funccall);
                    oDateRange.HideLabel();
                }
            }            
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey             = "FunctionalityEntityStatus";
            FolderLocationFromRoot       = "FunctionalityEntityStatus";
            PrimaryEntity                = SystemEntity.FunctionalityEntityStatus;

            SearchActionBarCore          = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;

            TabHeaderContainer = divTabHeaderList;
            TabContainer = divTabContentContainer;
        }
        
        #endregion

    }
}