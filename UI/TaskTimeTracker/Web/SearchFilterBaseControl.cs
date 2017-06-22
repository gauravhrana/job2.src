using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace TaskTimeTracker.UI.Web.BaseClasses
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:SearchFilterBaseControl runat=server></{0}:SearchFilterBaseControl>")]
    public abstract class SearchFilterBaseControl : BaseControl
    {

        #region Variables & Properties

        protected Repeater SearchParametersRepeaterCore { get; set; }

        protected SystemEntity PrimaryEntity { get; set; }

        public event EventHandler OnSearch;

        protected virtual DataTable SearchColumns
        {
            get
            {
                if (ViewState["ValidSearchColumns"] != null)
                {
                    return (DataTable)ViewState["ValidSearchColumns"];
                }
                return null;
            }
            set
            {
                ViewState["ValidSearchColumns"] = value;
            }
        }

        public virtual string GroupBy { get; set; }

        protected SearchActionBar SearchActionBarCore { get; set; }

        #endregion

        #region Methods

        protected virtual void IsControlValid()
        {
            if (!string.IsNullOrEmpty(SettingCategory))
            {
                ApplicationCommon.CreateUserPreferenceCategoryIfNotExists(SettingCategory, "Search Control Name");
            }
            else
            {
                throw new Exception("Search control is not named");
            }
        }

        protected virtual void SetDefaultValues()
        {
            for (var i = 0; i < SearchParametersRepeaterCore.Items.Count; i++)
            {
                var hdnfield = (HiddenField)SearchParametersRepeaterCore.Items[i].FindControl("hdnfield");

                var dropdownlist = (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlist");
                var txtbox = (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtbox");
                var chkbox = (LinkButton)SearchParametersRepeaterCore.Items[i].FindControl("chkbox");
                var txtbox1 = (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtbox1");
                var listbox = (ListBox)SearchParametersRepeaterCore.Items[i].FindControl("listbox");

                // guard clause
                if (hdnfield == null || txtbox == null)
                {
                    continue;
                }

                if (!SearchParametersRepeaterCore.Items[i].Visible)
                {
                    SearchParametersRepeaterCore.Items[i].Visible = true;
                    ApplicationCommon.UpdateUserPreference(SettingCategory, hdnfield.Value + "Visibility", "true");
                }

                if (txtbox.Visible)
                {
                    txtbox.Text = string.Empty;
                }

                if (dropdownlist.Visible)
                {
                    dropdownlist.SelectedIndex = -1;
                    txtbox1.Text = "-1";
                }

                if (listbox.Visible)
                {
                    listbox.SelectedIndex = -1;
                    txtbox1.Text = "-1";
                }

            }
        }

        // this needs to be implemented in the inheriting class to load the drop downs in search control
        public virtual void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
        {
	        
        }

        // this needs to be implemented in the inheriting class to load the list boxes in search control
        public virtual void LoadListBoxSources(ListBox lstBoxControl)
        {
	        
        }

        protected virtual object CheckAndGetFieldValue(string field, bool ddlreturnvalue = true)
        {
            for (var i = 0; i < SearchParametersRepeaterCore.Items.Count; i++)
            {
                var label = (Label)SearchParametersRepeaterCore.Items[i].FindControl("label");
                var hdnfield = (HiddenField)SearchParametersRepeaterCore.Items[i].FindControl("hdnfield");

                // guard clause
                if (hdnfield == null || !SearchParametersRepeaterCore.Items[i].Visible)
                {
                    continue;
                }

                if (hdnfield.Value.Equals(field))
                {
                    var txtbox = (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtbox");
                    var dropdownlist = (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlist");
                    var listbox = (ListBox)SearchParametersRepeaterCore.Items[i].FindControl("listbox");
                    var oDateRange = (DateRange)SearchParametersRepeaterCore.Items[i].FindControl("oDateRange");

                    if (txtbox.Visible)
                    {
                        return txtbox.Text;
                    }
                    else if (dropdownlist != null && dropdownlist.Visible)
                    {
                        if (ddlreturnvalue)
                        {
                            return dropdownlist.SelectedValue;
                        }
                        else
                        {
                            return dropdownlist.SelectedItem.Text;
                        }
                    }
                    else if (listbox != null && listbox.Visible)
                    {
                        var values = "";
                        if (ddlreturnvalue)
                        {
                            var indices = listbox.GetSelectedIndices();

                            for (int j = 0; j < indices.Length; j++)
                            {
                                values += listbox.Items[indices[j]].Value;
                                if (j != indices.Length - 1)
                                    values += "/";
                            }
                            return values;
                        }
                        else
                        {
                            var indices = listbox.GetSelectedIndices();
                            for (int j = 0; j < indices.Length; j++)
                            {
                                values += listbox.Items[indices[j]].Text;
                                if (j != indices.Length - 1)
                                    values += "/";
                            }
                            return values;
                        }
                    }
                    else if(oDateRange != null)
                    {
                        return oDateRange.FromDateTime.ToString() + "&" + oDateRange.ToDateTime.ToString() + "&" + oDateRange.Checked;
                    }
                }
            }

            return string.Empty;
        }

        protected virtual string CheckAndSetFieldValue(string field, string value)
        {
            for (var i = 0; i < SearchParametersRepeaterCore.Items.Count; i++)
            {
                var label = (Label)SearchParametersRepeaterCore.Items[i].FindControl("label");
                var hdnfield = (HiddenField)SearchParametersRepeaterCore.Items[i].FindControl("hdnfield");

                if (hdnfield != null)
                {
                    if (hdnfield.Value.Equals(field))
                    {
                        var txtbox = (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtbox");
                        var txtbox1 = (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtbox1");
                        var dropdownlist = (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlist");
                        var listbox = (ListBox)SearchParametersRepeaterCore.Items[i].FindControl("listbox");
                        var oDateRange = (DateRange)SearchParametersRepeaterCore.Items[i].FindControl("oDateRange");
                        var datepanel = (Panel)SearchParametersRepeaterCore.Items[i].FindControl("datepanel");

                        if (txtbox.Visible)
                        {
                            txtbox.Text = value;
                        }
                        else if (dropdownlist != null && dropdownlist.Visible)
                        {
                            dropdownlist.SelectedIndex = UIHelper.GetDropDownSelectedIndex(dropdownlist, field, SettingCategory); ;
                            txtbox1.Text = dropdownlist.SelectedValue;
                            if (field == "GroupBy")
                            {
                                if (dropdownlist.SelectedItem.Text != "All")
                                {
                                    GroupBy = dropdownlist.SelectedValue;
                                }
                                else
                                {
                                    GroupBy = string.Empty;
                                }
                            }
                        }
                        else if (listbox != null && listbox.Visible)
                        {
                            listbox.ClearSelection();
                            if (value.Contains("/"))
                            {
                                var indices = value.Split('/');
                                var txtvalue = "";
                                txtbox1.Text = string.Empty;
                                for (int j = 0; j < indices.Length; j++)
                                {
                                    foreach (ListItem item in listbox.Items)
                                    {
                                        if (item.Text.Equals(indices[j]))
                                        {
                                            item.Selected = true;
                                            txtvalue += item.Value;
                                            txtvalue += "/";

                                        }
                                    }

                                }
                                txtvalue.Remove(txtvalue.Length - 1);
                                txtbox1.Text = txtvalue;
                            }
                            else
                            {
                                listbox.SelectedIndex = listbox.Items.IndexOf(
                                        listbox.Items.FindByText(value));
                                txtbox1.Text = listbox.SelectedValue.ToString();
                            }
                        }
                        else if (datepanel != null && datepanel.Visible)
                        {
                            var dates = value.Split('&');
                            oDateRange.SetDateValues(value);
                            if (dates.Length > 2)
                            {
                                oDateRange.Checked = bool.Parse(dates[2]);
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        protected virtual string GetSearchKeyValue(string colName, DataSet ds)
        {
            var strValue = string.Empty;

            if (ds.Tables.Count > 2)
            {
                var drDetails = ds.Tables[1].Select(Framework.Components.Core.SearchKeyDetail.DataColumns.SearchParameter + " = '" + colName + "'");
                if (drDetails.Length > 0)
                {
                    var detailId = Convert.ToString(drDetails[0][Framework.Components.Core.SearchKeyDetail.DataColumns.SearchKeyDetailId]);

                    var drItems = ds.Tables[2].Select(Framework.Components.Core.SearchKeyDetailItem.DataColumns.SearchKeyDetailId + " = " + detailId);
                    if (drItems.Length > 0)
                    {
                        strValue = Convert.ToString(drItems[0][Framework.Components.Core.SearchKeyDetailItem.DataColumns.Value]);
                        if (drItems.Length > 1)
                        {
                            for (var iCount = 1; iCount < drItems.Length; iCount++)
                            {
                                strValue += "/" + Convert.ToString(drItems[iCount][Framework.Components.Core.SearchKeyDetailItem.DataColumns.Value]);
                            }
                        }
                    }
                }
            }

            return strValue;
        }

        public virtual void RaiseSearch()
        {
            if (OnSearch != null)
            {
                OnSearch(this, EventArgs.Empty);
            }
        }

        protected override void GetSettings()
        {
            var searchKeyId = Convert.ToString(Page.RouteData.Values["SearchKey"]);

            if (!string.IsNullOrEmpty(searchKeyId))
            {
                var dataSearchKey = new Framework.Components.Core.SearchKey.Data();
                dataSearchKey.SearchKeyId = Convert.ToInt32(searchKeyId);

                var ds = Framework.Components.Core.SearchKey.SearchByKey(dataSearchKey, SessionVariables.AuditId);

                for (var i = 0; i < SearchColumns.Rows.Count; i++)
                {
                    var colName = Convert.ToString(SearchColumns.Rows[i]["Name"]);

                    CheckAndSetFieldValue(colName, GetSearchKeyValue(colName, ds) + "&" +
                        GetSearchKeyValue(colName + "2", ds) + "&" +
                        GetSearchKeyValue(colName + "Checked", ds));

                }
            }
            else
            {

                var category = SettingCategory;
                var value = string.Empty;

                for (var i = 0; i < SearchColumns.Rows.Count; i++)
                {
                    CheckAndSetFieldValue(
                        SearchColumns.Rows[i]["Name"].ToString(), ApplicationCommon.GetUserPreferenceByKey(SearchColumns.Rows[i]["Name"].ToString(), category));

                }
            }
        }

        protected override void SaveSettings()
        {
            if (SearchColumns == null) return;

            foreach (DataRow dr in SearchColumns.Rows)
            {
                try
                {
	                var columnName = Convert.ToString(dr["Name"]);
	                var columnValue = CheckAndGetFieldValue(columnName, false).ToString();
	                ApplicationCommon.UpdateUserPreference(SettingCategory, columnName, columnValue);
                }
                catch
                {
	                
                }
            }
        }

        protected virtual int SaveSearchKey()
        {
            var searchKeyId = 0;

            if (SearchColumns != null)
            {
                var data = new Framework.Components.Core.SearchKey.Data();
                data.Name = DateTime.Now.ToLongTimeString();
                data.View = PrimaryEntity.ToString();
                data.SortOrder = 1;
                data.Description = PrimaryEntity.ToString();

                searchKeyId = Framework.Components.Core.SearchKey.Create(data, SessionVariables.AuditId);

                foreach (DataRow dr in SearchColumns.Rows)
                {
                    try
                    {
	                    var columnName = Convert.ToString(dr["Name"]);
	                    var columnValue = CheckAndGetFieldValue(columnName, false).ToString();

	                    var dataDetail = new Framework.Components.Core.SearchKeyDetail.Data();
	                    dataDetail.SearchKeyId = searchKeyId;

	                    //ApplicationCommon.UpdateUserPreference(SettingCategory, columnName, columnValue);
	                    dataDetail.SearchParameter = columnName;
	                    dataDetail.SortOrder = 1;
	                    var detailId = Framework.Components.Core.SearchKeyDetail.Create(dataDetail,
	                                                                                    SessionVariables.AuditId);

	                    var dataDetailItem = new Framework.Components.Core.SearchKeyDetailItem.Data();
	                    dataDetailItem.SearchKeyDetailId = detailId;
	                    dataDetailItem.SortOrder = 1;

	                    dataDetailItem.Value = columnValue;
	                    Framework.Components.Core.SearchKeyDetailItem.Create(dataDetailItem, SessionVariables.AuditId);

                    }
                    catch
                    {
	                    
                    }
                }
            }

            return searchKeyId;
        }

        public void SetupSearch()
        {
            if (SearchColumns == null)
            {
                //Code to bind the Search fields repeater with SearchField Mode columns from FieldConfig table
                var colsdata = new Framework.Components.UserPreference.FieldConfiguration.Data();
                colsdata.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
                colsdata.SystemEntityTypeId = Convert.ToInt32(PrimaryEntity);

                SearchColumns = Framework.Components.UserPreference.FieldConfiguration.Search(colsdata, AuditId);
            }

            SearchParametersRepeaterCore.DataSource = SearchColumns;
            SearchParametersRepeaterCore.DataBind();

            if (!string.IsNullOrEmpty(SettingCategory))
            {
                ApplicationCommon.CreateUserPreferenceCategoryIfNotExists(SettingCategory, "Search Control Name");
            }
            else
            {
                throw new Exception("Search control is not named");
            }

            GetSettings();

            SaveSettings();

            RaiseSearch();
        }

        #endregion

        #region Events

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveSettings();
            RaiseSearch();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            SetDefaultValues();
            SaveSettings();
            RaiseSearch();
        }

        protected void chkbox_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                try
                {
                    var name = ((LinkButton)sender).CommandName;
                    for (int i = 0; i < SearchParametersRepeaterCore.Items.Count; i++)
                    {
                        var hdnfield = (HiddenField)SearchParametersRepeaterCore.Items[i].FindControl("hdnfield");
                        if (hdnfield != null)
                        {
                            if (hdnfield.Value.Equals(name))
                            {
                                SearchParametersRepeaterCore.Items[i].Visible = false;
                            }
                        }
                    }
                    //ApplicationCommon.UpdateUserPreference(SettingCategory, name + "Visibility", "false");

                    SaveSettings();
                    RaiseSearch();
                }
                catch { }
            }
        }

        protected virtual void SearchParametersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var label = (Label)e.Item.FindControl("label");
                var hdnfield = (HiddenField)e.Item.FindControl("hdnfield");
                var txtbox = (TextBox)e.Item.FindControl("txtbox");
                var txtbox1 = (TextBox)e.Item.FindControl("txtbox1");
                var dropdownlist = (DropDownList)e.Item.FindControl("dropdownlist");
                var listbox = (ListBox)e.Item.FindControl("listbox");
                var datepanel = (Panel)e.Item.FindControl("datepanel");

                if (label != null && txtbox != null && dropdownlist != null)
                {
                    var name = hdnfield.Value;

                    var data = new Framework.Components.UserPreference.FieldConfiguration.Data();
                    data.Name = name;
                    data.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;

                    var dt = Framework.Components.UserPreference.FieldConfiguration.Search(data, AuditId);

                    if (dt.Rows.Count >= 1)
                    {
                        var controltype = dt.Rows[0]
                            [Framework.Components.UserPreference.FieldConfiguration.DataColumns.ControlType].ToString();
                        if (controltype.Equals("TextBox"))
                        {
                            txtbox.Visible = true;
                            txtbox1.Visible = false;
                            if (dropdownlist != null)
                            {
                                dropdownlist.Visible = false;
                            }
                            if (listbox != null)
                            {
                                listbox.Visible = false;
                            }
                            if (datepanel != null)
                            {
                                datepanel.Visible = false;
                            }
                        }
                        else if (controltype.Equals("DropDownList"))
                        {
                            txtbox.Visible = false;
                            txtbox1.Visible = true;
                            dropdownlist.Visible = true;

                            if (listbox != null)
                            {
                                listbox.Visible = false;
                            }
                            if (datepanel != null)
                            {
                                datepanel.Visible = false;
                            }

                            LoadDropDownListSources(name, dropdownlist);

                            if (!name.Equals("GroupBy") && !name.Contains("GroupBy"))
                                dropdownlist.Items.Insert(0, new ListItem("All", "-1"));
                            else
                                dropdownlist.Items.Insert(0, new ListItem("None", "-1"));
                            dropdownlist.SelectedIndex = 0;
                            if (SessionVariables.IsTesting)
                            {
                                txtbox1.Visible = true;
                                txtbox1.Text = dropdownlist.SelectedValue;
                            }
                        }
                        else if (controltype.Equals("MultiSelectListBox"))
                        {
                            txtbox.Visible = false;
                            txtbox1.Visible = true;
                            listbox.Visible = true;

                            if (dropdownlist != null)
                            {
                                dropdownlist.Visible = false;
                            }
                            if (datepanel != null)
                            {
                                datepanel.Visible = false;
                            }

                            LoadListBoxSources(listbox);
                        }
                        else
                        {
                            txtbox.Visible = false;
                            txtbox1.Visible = false;
                            datepanel.Visible = true;
                            if (dropdownlist != null)
                            {
                                dropdownlist.Visible = false;
                            }
                            if (listbox != null)
                            {
                                listbox.Visible = false;
                            }
                            var oDateRange = (DateRange)e.Item.FindControl("oDateRange");
                            oDateRange.HideLabel();
                        }
                    }

                    e.Item.Visible = Boolean.Parse(ApplicationCommon.GetUserPreferenceByKey(name + "Visibility", SettingCategory));
                }
            }
        }

        protected void dropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            RepeaterItem item = ddl.Parent as RepeaterItem;
            var tb = item.FindControl("txtbox1") as TextBox;
            if (tb != null)
                tb.Text = ddl.SelectedValue;
        }

        protected void OnPageLoad(object sender, EventArgs e)
        {
            SearchActionBarCore.Setup(PrimaryEntityKey, SaveSearchKey);
        }

        #endregion

    }
}
