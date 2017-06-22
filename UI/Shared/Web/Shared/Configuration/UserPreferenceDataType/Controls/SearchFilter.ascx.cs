using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.UserPreferenceDataType.Controls
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

        public Framework.Components.UserPreference.UserPreferenceDataTypeDataModel SearchParameters
        {
            get
            {
                var data = new Framework.Components.UserPreference.UserPreferenceDataTypeDataModel();

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                    StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
                    && !string.IsNullOrEmpty(CheckAndGetFieldValue(
                        StandardDataModel.StandardDataColumns.Name).ToString()))
                {
                    data.Name = CheckAndGetFieldValue(
                       StandardDataModel.StandardDataColumns.Name).ToString();
                }


                return data;
            }
        }
        #endregion

        #region methods
    
        private void SetAutoSearchOn(bool enabled)
        {
            for (var i = 0; i < SearchParametersRepeater.Items.Count; i++)
            {
                var dropdownlist = (DropDownList)SearchParametersRepeater.Items[i].FindControl("dropdownlist");
                if (dropdownlist != null && dropdownlist.Visible)
                {
                    dropdownlist.AutoPostBack = enabled;
                }
            }
        }

        private void ShowDebugTextBoxes(bool visible)
        {
            for (var i = 0; i < SearchParametersRepeater.Items.Count; i++)
            {
                var txtbox1 = (TextBox)SearchParametersRepeater.Items[i].FindControl("txtbox1");
                if (txtbox1 != null)
                {
                    txtbox1.Visible = visible;
                }
            }
        }

        private object CheckAndGetFieldValue(string field, bool ddlreturnvalue = true)
        {
            for (var i = 0; i < SearchParametersRepeater.Items.Count; i++)
            {
                var label = (Label)SearchParametersRepeater.Items[i].FindControl("label");
                var hdnfield = (HiddenField)SearchParametersRepeater.Items[i].FindControl("hdnfield");
                if (hdnfield != null && SearchParametersRepeater.Items[i].Visible)
                {
                    if (hdnfield.Value.Equals(field))
                    {
                        var txtbox = (TextBox)SearchParametersRepeater.Items[i].FindControl("txtbox");

                        if (txtbox.Visible)
                            return txtbox.Text;

                    }
                }
            }
            return "";
        }
      
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.UserPreferenceDataType;
            PrimaryEntityKey = "UserPreferenceDataType";
            FolderLocationFromRoot = "/Shared/Configuration";

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion

	}
}