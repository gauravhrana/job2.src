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
                upcId = PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(settingCategory, settingCategory);
            }

            var fcModeSelected = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.FieldConfigurationMode, settingCategory);
            
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
            
			PerferenceUtility.UpdateUserPreference(settingCategory, ApplicationCommon.FieldConfigurationMode, fcMode);
            
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
            var sortedValidModes = new DataTable();

            if (Session["ValidModes_" + systemEntityTypeId] != null)
            {
				sortedValidModes = (DataTable)Session["ValidModes_" + systemEntityTypeId];
            }
            else
            {
                var validModes = new DataTable();
				var data = new FieldConfigurationDataModel();
                data.SystemEntityTypeId = systemEntityTypeId;

				var columns = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);
                var modes = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);

                //var modeapplicable = false;

                var validModes1 = new DataTable();
                var validModes2 = new DataTable();
                var modesbysystementity = new DataTable();
                validModes1 = modes.Clone();
                validModes2 = modes.Clone();
                validModes = modes.Clone();
                modesbysystementity = modes.Clone();

                //hdnFieldConfigurationModeCategory.Value = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.FieldConfigurationModeCategoryKey, SettingCategory);

				var fcModeCategoryData = new FieldConfigurationModeCategoryDataModel();
				fcModeCategoryData.Name = "List";

				var fcModeCategoryDt = FieldConfigurationModeCategoryDataManager.Search(fcModeCategoryData, SessionVariables.SystemRequestProfile);
				hdnFieldConfigurationModeCategory.Value = fcModeCategoryDt.Rows[0][FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId].ToString();

                var modeId = 0;
				var modedData = new ApplicationModeDataModel();

                if (SessionVariables.IsTesting)
                    modedData.Name = "Testing";
                else
                    modedData.Name = "Live";

				modedData.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

                //modedata.Name = SessionVariables.UserApplicationMode;
				var dtMode = ApplicationModeDataManager.Search(modedData, SessionVariables.RequestProfile);

                if (dtMode.Rows.Count > 0)
                {
					modeId = Convert.ToInt32(dtMode.Rows[0][ApplicationModeDataModel.DataColumns.ApplicationModeId].ToString());                    
                }

                //Added SystemEntityTypeId = 3000 only for testing FCModeCategory
                if (!string.IsNullOrEmpty(hdnFieldConfigurationModeCategory.Value) && modeId != 0)
                {
                    var appmodedata = new ApplicationModeXFieldConfigurationModeDataModel();
                    appmodedata.ApplicationModeId = modeId;
					appmodedata.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
                    var appmodedt = ApplicationModeXFieldConfigurationModeDataManager.GetByApplicationMode(modeId, SessionVariables.RequestProfile);

                    //var tempModesIds =  appmodedt.AsEnumerable().se

                    var distinctFieldValues = (from row in appmodedt.AsEnumerable()
                                               orderby row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString()
                                               select row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString()).Distinct().ToList();


                    var modeIds = String.Empty;

                    foreach (var valModeId in distinctFieldValues)
                    {
                        if (!string.IsNullOrEmpty(modeIds))
                        {
                            modeIds += ", " + valModeId;
                        }
                        else
                        {
                            modeIds += valModeId;
                        }
                    }

                    if (!string.IsNullOrEmpty(modeIds))
                    {
                        var rows = modes.Select(FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId + " in (" + modeIds + ")");
                        if (rows.Length > 0)
                        {
                            foreach (var dr in rows)
                            {
                                validModes1.ImportRow(dr);
                            }
                        }
                    }

                    var aeflmodecatmodesdata = new FieldConfigurationModeCategoryXFCModeDataModel();
                    aeflmodecatmodesdata.FieldConfigurationModeCategoryId = int.Parse(hdnFieldConfigurationModeCategory.Value);
                    var aeflmodecatmodesdt = FieldConfigurationModeCategoryXFCModeDataManager.GetByFieldConfigurationModeCategory(int.Parse(hdnFieldConfigurationModeCategory.Value), SessionVariables.RequestProfile);

                    distinctFieldValues = (from row in aeflmodecatmodesdt.AsEnumerable()
                                           orderby row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString()
                                           select row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString()).Distinct().ToList();

                    modeIds = String.Empty;
                    foreach (var valModeId in distinctFieldValues)
                    {
                        if (!string.IsNullOrEmpty(modeIds))
                        {
                            modeIds += ", " + valModeId;
                        }
                        else
                        {
                            modeIds += valModeId;
                        }
                    }

                    if (!string.IsNullOrEmpty(modeIds))
                    {
                        var rows = modes.Select(FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId + " in (" + modeIds + ")");
                        if (rows.Length > 0)
                        {
                            foreach (var dr in rows)
                            {
                                validModes2.ImportRow(dr);
                            }
                        }
                    }

					
						validModes = validModes1.AsEnumerable()
								 .Intersect(validModes2.AsEnumerable(), DataRowComparer.Default).CopyToDataTable();

						modesbysystementity = GetApplicableModesListByEntity(columns, modes);

						validModes.Merge(modesbysystementity, false);
					
                }
                else
                {
                    //for (var j = 0; j < modes.Rows.Count; j++)
                    //{
                    //    for (var i = 0; i < columns.Rows.Count; i++)
                    //    {

                    //        if (
                    //            int.Parse(
                    //                columns.Rows[i][
                    //                    FieldConfiguration.DataColumns.FieldConfigurationModeId].
                    //                    ToString()) ==
                    //            int.Parse(
                    //                modes.Rows[j][
                    //                    FieldConfigurationMode.DataColumns.
                    //                        FieldConfigurationModeId].ToString())
                    //            )
                    //        {
                    //            var zKey = FieldConfigurationMode.DataColumns.FieldConfigurationModeId;

                    //            var temp = validmodes.Select(zKey + " = " + int.Parse(
                    //                modes.Rows[j][
                    //                    FieldConfigurationMode.DataColumns.
                    //                        FieldConfigurationModeId].ToString()));
                    //            if (temp.Length == 0)
                    //                validmodes.ImportRow(modes.Rows[j]);


                    //        }
                    //    }                    

                    //}

                    validModes = GetApplicableModesListByEntity(columns, modes);
                }

                var dv = validModes.DefaultView;
                dv.Sort = "SortOrder ASC";
				sortedValidModes = dv.ToTable(true, "FieldConfigurationModeId", "Name", "Description", "SortOrder");
				Session["ValidModes_" + systemEntityTypeId] = sortedValidModes;
            }

			return sortedValidModes;
        }

        private DataTable GetApplicableModesListByEntity(DataTable columns, DataTable modes)
        {
            var validmodes = new DataTable();
            validmodes = modes.Clone();

            for (var j = 0; j < modes.Rows.Count; j++)
            {
                for (var i = 0; i < columns.Rows.Count; i++)
                {
	                var a = columns.Rows[i][FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId].ToString();
	                var b = modes.Rows[j][FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString();

                    if (int.Parse(a) == int.Parse(b))
                    {
                        var temp = validmodes.Select(FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId + " = " + int.Parse(b));

                        if (temp.Length == 0)
                            validmodes.ImportRow(modes.Rows[j]);
                    }
                }
            }

            return validmodes;

        }

    }
}