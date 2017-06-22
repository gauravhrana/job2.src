using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Configuration.UserPreferenceKey.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {
        #region variables        //const string SearchSettingCategoryName = "FESSearchCriteria";

        public string Name
        {
            get
            {
                if (ViewState["Name"] != null)
                {
                    return ViewState["Name"].ToString();
                }
                return String.Empty;
            }
            set
            {
                ViewState["Name"] = value;
            }
        }

        public UserPreferenceKeyDataModel SearchParameters
        {
            get
            {
                var data = new UserPreferenceKeyDataModel();

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                    StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
                    && !string.IsNullOrEmpty(CheckAndGetFieldValue(
                        StandardDataModel.StandardDataColumns.Name).ToString()))
                {
                    data.Name = CheckAndGetFieldValue(
                       StandardDataModel.StandardDataColumns.Name).ToString();
                }
                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                    UserPreferenceKeyDataModel.DataColumns.DataType + "Visibility", SettingCategory)
                    && Convert.ToInt32(CheckAndGetFieldValue(
                        UserPreferenceKeyDataModel.DataColumns.DataType)) != -1)
                {
                    data.DataTypeId = Convert.ToInt32(CheckAndGetFieldValue(
                       UserPreferenceKeyDataModel.DataColumns.DataType));
                }

                return data;
            }
        }
        #endregion

        #region methods

        //public void SetupSearch()
        //{
        //    if (SearchColumns == null)
        //    {
                
        //        //Code to bind the Search fields repeater with SearchField Mode columns from FieldConfig table
        //        var colsData = new FieldConfigurationDataModel();
        //        colsData.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
        //        colsData.SystemEntityTypeId = Convert.ToInt32(SystemEntity.UserPreferenceKey);
        //        var cols = FieldConfigurationDataManager.Search(colsData, SessionVariables.RequestProfile);
        //        SearchColumns = cols;
        //    }

        //    SearchParametersRepeater.DataSource = SearchColumns;
        //    SearchParametersRepeater.DataBind();

        //    if (!string.IsNullOrEmpty(SettingCategory))
        //    {
        //        PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(SettingCategory, "Search Control Name");
        //    }
        //    else
        //    {
        //        throw new Exception("Search control is not named");
        //    }
            
        //    //var autoSearchOn = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.AutoSearchOn, SettingCategory);

        //    GetSettings();

        //    SaveSettings();

        //    RaiseSearch();
        //}

        #endregion

        #region Events

        //protected void SearchParametersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        var label = (Label)e.Item.FindControl("label");
        //        var dropdownlist = (DropDownList)e.Item.FindControl("dropdownlist");
        //        var txtbox1 = (TextBox)e.Item.FindControl("txtbox1");
        //        var hdnfield = (HiddenField)e.Item.FindControl("hdnfield");
        //        var txtbox = (TextBox)e.Item.FindControl("txtbox");

        //        if (label != null && txtbox != null)
        //        {
        //            var name = hdnfield.Value;
        //            var data = new FieldConfigurationDataModel();
        //            data.Name = name;
        //            data.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
        //            var dt = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);
        //            if (dt.Rows.Count >= 1)
        //            {
        //                var controltype = dt.Rows[0]
        //                    [FieldConfigurationDataModel.DataColumns.ControlType].ToString();
        //                if (controltype.Equals("TextBox"))
        //                {
        //                    txtbox.Visible = true;
        //                    txtbox1.Visible = false;
        //                    dropdownlist.Visible = false;
        //                }
        //                else if (controltype.Equals("DropDownList"))
        //                {
        //                    if (hdnfield.Value.Equals("DataType"))
        //                    {
        //                        var datatypeData = UserPreferenceDataTypeDataManager.GetList(SessionVariables.RequestProfile);
        //                        UIHelper.LoadDropDown(datatypeData, dropdownlist, StandardDataModel.StandardDataColumns.Name, UserPreferenceDataTypeDataModel.DataColumns.UserPreferenceDataTypeId);

        //                    }
        //                    dropdownlist.Items.Insert(0, new ListItem("All", "-1"));
        //                    dropdownlist.SelectedIndex = 0;
        //                    txtbox.Visible = false;
        //                    txtbox1.Visible = true;
        //                    dropdownlist.Visible = true;
        //                }
        //            }

        //        }
        //    }

        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            oSearchActionBar.Setup("FunctionalityEntityStatus", SaveSearchKey);

            //SetupSearch();
        }	

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.UserPreferenceKey;
            PrimaryEntityKey = "UserPreferenceKey";
            FolderLocationFromRoot = "/Shared/Configuration";

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion

    }
	}