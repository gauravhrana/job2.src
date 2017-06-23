using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System.Collections.Specialized;
using System.Collections;
using Dapper;

namespace Shared.UI.Web.Controls
{
    public partial class ListControl
    {
        private void SaveSessionInstanceFieldConfigurtionMode(int selectedMode)
        {
            if (Session[ViewState["TableName"] + "SelectedMode"] == null)
                Session.Add(ViewState["TableName"] + "SelectedMode", selectedMode);
            else
                Session[ViewState["TableName"] + "SelectedMode"] = selectedMode;
        }

        private int GetSessionInstanceFieldConfigurationMode(string currentEntity)
        {
            if (Session[ViewState["TableName"] + "SelectedMode"] != null)
                return Convert.ToInt32(Session[currentEntity + "SelectedMode"].ToString());
            else
                return -1;
        }

        private int SetUpDropDownFieldConfigurationMode()
        {
            var systementitytypeId = (int)Enum.Parse(typeof(SystemEntity), ViewState["TableName"].ToString());
            
			SettingCategory = ViewState["TableName"] + "DefaultViewListControl";
            var settingCategory = SettingCategory;

            var dt = GetApplicableModesList(systementitytypeId);
            var modeSelected = GetSessionInstanceFieldConfigurationMode(ViewState["TableName"].ToString());

            var upcId = 0;
            if (!string.IsNullOrEmpty(settingCategory))
            {
                upcId = PreferenceUtility.CreateUserPreferenceCategoryIfNotExists(settingCategory, settingCategory);
            }

            var fcModeSelected = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.FieldConfigurationMode, settingCategory);
            
			if (dt.Rows.Count > 0)
            {
	            try
	            {
		            ddlFieldConfigurationMode.ClearSelection();
		            ddlFieldConfigurationMode.SelectedIndex = -1;
		            ddlFieldConfigurationMode.DataSource = dt;
		            ddlFieldConfigurationMode.DataTextField = "Name";
		            ddlFieldConfigurationMode.DataValueField = "FieldConfigurationModeId";
		            ddlFieldConfigurationMode.DataBind();

		            if (Convert.ToInt32(fcModeSelected) > 0)
			            ddlFieldConfigurationMode.SelectedValue = fcModeSelected;
		            else
			            ddlFieldConfigurationMode.SelectedValue = modeSelected.ToString();
		            
					FieldConfigurationMode = ddlFieldConfigurationMode.SelectedValue;
					modeSelected = int.Parse(FieldConfigurationMode);
	            }
	            catch (Exception ex)
	            {
		            
	            }

                //modeselected = int.Parse(ddlFieldConfigurationMode.SelectedValue);

            }
            else
            {
                ddlFieldConfigurationMode.Visible = false;
            }

            return modeSelected;
        }

        protected void ddlFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFieldConfigurationMode(ddlFieldConfigurationMode.SelectedValue, ddlFieldConfigurationMode.SelectedItem.Value);          
        }

        public void ApplyFieldConfigurationMode(string fcMode, string fcModeText)
        {
            FieldConfigurationMode = fcMode;
            FieldConfigurationModeText = fcModeText;

            SessionVariables.FieldConfigurationMode = Convert.ToInt32(fcMode);
            var settingCategory = ViewState["TableName"] + "DefaultViewListControl";
            
			PreferenceUtility.UpdateUserPreference(settingCategory, ApplicationCommon.FieldConfigurationMode, fcMode);
            
			while (MainGridView.Columns.Count > 1)
            {
                if (!MainGridView.Columns[MainGridView.Columns.Count - 1].HeaderText.Equals("All"))
                {
                    MainGridView.Columns.RemoveAt(MainGridView.Columns.Count - 1);
                }
            }

            SaveSessionInstanceFieldConfigurtionMode(Convert.ToInt32(fcMode));
            
			if (_getData != null)
            {
                Setup(ViewState["TableName"].ToString(), ViewState["TableFolder"].ToString(), ViewState["PrimaryKey"].ToString(), true, _getData, _getColumnDelegate, UserPreferenceCategory);
            }
            else
            {
                if (!string.IsNullOrEmpty(GroupByField) && !string.IsNullOrEmpty(GroupByFieldValue))
                {
                    Setup(ViewState["TableName"].ToString(), ViewState["TableFolder"].ToString(), ViewState["PrimaryKey"].ToString(), true, _getColumnDelegate, GroupByField, GroupByFieldValue, UserPreferenceCategory);
                }
            }

            SortGridView(String.Empty, SortDirection.Ascending.ToString());

            ListHelper.AddCheckBox(MainGridView);
        }

        private DataTable GetApplicableModesList(int systemEntityTypeId)
        {
            var sortedValidModes = new List<FieldConfigurationModeDataModel>();

            if (HttpContext.Current.Session["ValidModes_" + systemEntityTypeId] != null)
            {
                sortedValidModes = (List<FieldConfigurationModeDataModel>)HttpContext.Current.Session["ValidModes_" + systemEntityTypeId];
            }
            else
            {
                var data = new FieldConfigurationDataModel();
                data.SystemEntityTypeId = systemEntityTypeId;
                data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

				var fcRecordsByEntity = FieldConfigurationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);


                var dataMode = new FieldConfigurationModeDataModel();
                dataMode.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
				var fcModeList = FieldConfigurationModeDataManager.GetEntityDetails(dataMode, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                //var modeapplicable = false;

                var validModes1 = new List<FieldConfigurationModeDataModel>();
                var validModes2 = new List<FieldConfigurationModeDataModel>();
                var validModes = new List<FieldConfigurationModeDataModel>();

                var fcModesByEntity = new List<FieldConfigurationModeDataModel>();

                //hdnFieldConfigurationModeCategory.Value = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.FieldConfigurationModeCategoryKey, SettingCategory);

                var fcModeCategoryData = new FieldConfigurationModeCategoryDataModel();
                fcModeCategoryData.Name = "List";

				var fcModeCategoryDt = FieldConfigurationModeCategoryDataManager.GetEntityDetails(fcModeCategoryData, SessionVariables.SystemRequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
                var fcModeCategoryId = fcModeCategoryDt[0].FieldConfigurationModeCategoryId.ToString();

                var applicationModeId = 0;
                var modedData = new ApplicationModeDataModel();

                if (SessionVariables.IsTesting)
                    modedData.Name = "Testing";
                else
                    modedData.Name = "Live";

                modedData.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
                //modedata.Name = SessionVariables.UserApplicationMode;
                var obj = ApplicationModeDataManager.GetDetails(modedData, SessionVariables.RequestProfile);

                if (obj != null)
                {
                    applicationModeId = obj.ApplicationModeId.Value;
                }

                //Added SystemEntityTypeId = 3000 only for testing FCModeCategory
                if (!string.IsNullOrEmpty(fcModeCategoryId) && applicationModeId != 0)
                {
                    var appModedt = ApplicationModeXFieldConfigurationModeDataManager.GetByApplicationMode(applicationModeId, SessionVariables.RequestProfile);

                    //var tempModesIds =  appmodedt.AsEnumerable().se

                    var distinctFieldValues = (from row in appModedt.AsEnumerable()
                                               orderby row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString()
                                               select row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString()).Distinct().ToList();


                    var modeIds = new List<int>();

                    foreach (var valModeId in distinctFieldValues)
                    {
                        modeIds.Add(int.Parse(valModeId));
                    }

                    var rows = fcModeList.Where(x => modeIds.Contains(x.FieldConfigurationModeId.Value)).ToList();
                    if (rows.Count > 0)
                    {
                        foreach (var dr in rows)
                        {
                            validModes1.Add(dr);
                        }
                    }

                    var fcModeCategorydt = FieldConfigurationModeCategoryXFCModeDataManager.GetByFieldConfigurationModeCategory(int.Parse(fcModeCategoryId), SessionVariables.RequestProfile);

                    distinctFieldValues = (from row in fcModeCategorydt.AsEnumerable()
                                           orderby row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString()
                                           select row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString()).Distinct().ToList();

                    modeIds = new List<int>();

                    foreach (var valModeId in distinctFieldValues)
                    {
                        modeIds.Add(int.Parse(valModeId));
                    }

                    rows = fcModeList.Where(x => modeIds.Contains(x.FieldConfigurationModeId.Value)).ToList();
                    if (rows.Count > 0)
                    {
                        foreach (var dr in rows)
                        {
                            validModes2.Add(dr);
                        }
                    }

                    validModes = validModes1.Intersect(validModes2).ToList();

                    fcModesByEntity = GetApplicableModesListByEntity(fcRecordsByEntity, validModes);

                    //Last Step (join the result with all the fc modes list by entity)
                    if (validModes.Count != 0 && fcModesByEntity.Count != 0)
                    {
                        fcModeList = (from a in fcModeList
                                      join b in fcModesByEntity
                                 on a.FieldConfigurationModeId equals b.FieldConfigurationModeId
                                 into g
                                      where g.Count() > 0
                                      select a).ToList();

                    }
                    else
                    {
                        fcModeList.Clear();
                    }

                    fcModeList = fcModeList.Distinct().ToList();

                    validModes = fcModeList;

                }
                else
                {

                    validModes = GetApplicableModesListByEntity(fcRecordsByEntity, fcModeList);
                }

                sortedValidModes = validModes.OrderBy(x => x.SortOrder).ToList();
            }

            return sortedValidModes.ToDataTable();
        }

        static List<FieldConfigurationModeDataModel> GetApplicableModesListByEntity(List<FieldConfigurationDataModel> fcRecordsByEntity, List<FieldConfigurationModeDataModel> modes)
        {
            var listValidModes = new List<FieldConfigurationModeDataModel>();

            foreach (var fcModeItem in modes)
            {
                foreach (var fcItem in fcRecordsByEntity)
                {
                    var a = fcItem.FieldConfigurationModeId;
                    var b = fcModeItem.FieldConfigurationModeId;

                    if (a == b)
                    {
                        var count = listValidModes.Where(x => x.FieldConfigurationModeId == a).ToList().Count;
                        if (count == 0)
                        {
                            listValidModes.Add(fcModeItem);
                        }
                    }
                }
            }

            return listValidModes;

        }

    }
}