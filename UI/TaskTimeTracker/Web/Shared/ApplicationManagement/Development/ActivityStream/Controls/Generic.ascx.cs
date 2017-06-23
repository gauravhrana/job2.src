using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using Shared.UI.Web.ActivityStream;
using System.Collections;

namespace Shared.UI.Web.ApplicationManagement.Development.ActivityStream.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

        private List<SystemEntityTypeDataModel> SourceTable
        {
            get
            {
                return (List<SystemEntityTypeDataModel>)ViewState["SourceTable"];
            }
            set
            {
                ViewState["SourceTable"] = value;
            }
        }

        private string _applicationUserId;

        public string ApplicationUserId
        {
            get
            {
                return _applicationUserId;
            }
            set
            {
                _applicationUserId = value;
            }
        }

        public string DataType
        {
            get
            {
                return drpActivityAlertMode.SelectedValue;
            }
            set
            {
                drpActivityAlertMode.SelectedValue = value;
            }
        }

        public string PagingStyle
        {
            get
            {
                return drpPagingStyle.SelectedValue;
            }
            set
            {
                drpPagingStyle.SelectedValue = value;
            }
        }

        public string StartDate
        {
            get
            {
                return txtStartDate.Text;
            }
            set
            {
                txtStartDate.Text = value;
            }
        }

        public string ActivityStreamTitle
        {
            get
            {
                return txtActivityStreamTitle.Text;
            }
            set
            {
                txtActivityStreamTitle.Text = value;
            }
        }

        public string Interval
        {
            get
            {
                return txtInterval.Text;
            }
            set
            {
                txtInterval.Text = value;
            }
        }

        public string Color
        {
            get
            {
                return txtColor.Text;
            }
            set
            {
                txtColor.Text = value;
            }
        }

        public string Width
        {
            get
            {
                return txtWidth.Text;
            }
            set
            {
                txtWidth.Text = value;
            }
        }

        public string Height
        {
            get
            {
                return txtHeight.Text;
            }
            set
            {
                txtHeight.Text = value;
            }
        }

        public string ActivityStreamAuditId
        {
            get
            {
                return txtActivityStreamAuditId.Text;
            }
            set
            {
                txtActivityStreamAuditId.Text = value;
            }
        }

        public string ActivityStreamPageSize
        {
            get
            {
                return txtActivityStreamPageSize.Text;
            }
            set
            {
                txtActivityStreamPageSize.Text = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return Convert.ToString(ViewState["CategoryName"]);
            }
            set
            {
                ViewState["CategoryName"] = value;
            }
        }

        public List<string> ExcludedSystemEntities
        {
            get
            {
                return GetExcludedSystemEntities();
            }
        }

        #endregion properties

        #region methods

        public List<string> GetExcludedSystemEntities()
        {
            var lst = new List<string>();
            for (var i = 0; i < lstTarget.Items.Count; i++)
            {
                lst.Add(lstTarget.Items[i].Value);
            }
            return lst;
        }

        public void SetCategoryName(string categoryName, bool chkProjectId)
        {
            // load data
            LoadData(categoryName, chkProjectId);
            //txtName.Enabled = !chkProjectId;
            //txtClientId.Enabled = !chkProjectId;
            //txtDescription.Enabled = !chkProjectId;
            //txtSortOrder.Enabled = !chkProjectId;
            //drpClientList.Enabled = !chkProjectId;
        }

        public void LoadData(string categoryName, bool showId)
        {
            CategoryName = categoryName;
            var userPreferenceSettingsDictionary = ActivityStreamCommon.GetUserPreferencesForActivityStream(CategoryName);
            if (userPreferenceSettingsDictionary.Values.Count > 0)
            {
                //defaultCPE.SelectedColor           = userPreferenceSettingsDictionary[ActivityStreamCommon.BackGroungColorKey];
                txtHeight.Text                     = userPreferenceSettingsDictionary[ActivityStreamCommon.HeightKey];
                txtInterval.Text                   = userPreferenceSettingsDictionary[ActivityStreamCommon.DateRangeKey];
                txtColor.Text                      = userPreferenceSettingsDictionary[ActivityStreamCommon.BackGroungColorKey];
                txtWidth.Text                      = userPreferenceSettingsDictionary[ActivityStreamCommon.WidthKey];
                txtStartDate.Text                  = DateTime.Now.ToString(SessionVariables.UserDateFormat);
                drpActivityAlertMode.SelectedValue = userPreferenceSettingsDictionary[ActivityStreamCommon.DataTypeKey];
                txtActivityStreamAuditId.Text      = userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamAuditId];
                txtActivityStreamPageSize.Text     = userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamPageSize];
                txtActivityStreamTitle.Text        = userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamTitle];
                drpPagingStyle.SelectedValue       = userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamPagingStyle];

                LoadSource();
                LoadCurrentTarget();
            }
            else
            {
                txtHeight.Text = String.Empty;
                txtInterval.Text = String.Empty;
                txtColor.Text = String.Empty;
                txtWidth.Text = String.Empty;
                txtStartDate.Text = String.Empty;
                txtActivityStreamAuditId.Text = String.Empty;
                txtActivityStreamPageSize.Text = String.Empty;
                txtActivityStreamTitle.Text = String.Empty;
            }
        }

        private List<SystemEntityTypeDataModel> LoadSystemEntities()
        {
			var dtSystemEntities = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            dtSystemEntities = dtSystemEntities.OrderBy(x => x.EntityName).ToList();
            return dtSystemEntities;
        }

        private DataTable LoadExcludedSystemEntities(string categoryName)
        {
            DataTable dt = ActivityStreamCommon.GetExcludedSystemEntities(categoryName);
            return dt;
        }

        private void LoadSource()
        {
            if (SourceTable == null)
            {
                SourceTable = LoadSystemEntities();
            }

            var dt = SourceTable;
            lstSource.DataSource = dt;
            lstSource.DataTextField = SystemEntityTypeDataModel.DataColumns.EntityName;
            lstSource.DataValueField = SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId;
            lstSource.DataBind();
        }

        private void LoadCurrentTarget()
        {
            var dt = LoadExcludedSystemEntities(CategoryName);
            foreach (DataRow dr in dt.Rows)
            {
                var entityText = ((Framework.Components.DataAccess.SystemEntity)Convert.ToInt32(dr[Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.Value])).ToString();
                var lstItem = new ListItem(entityText, Convert.ToString(dr[Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.Value]));                
                if (lstSource.Items.Contains(lstItem))
                {
                    lstSource.Items.Remove(lstItem);
                }
                lstTarget.Items.Add(lstItem);
            }
        }

        private void ClearItems(ListBox lstBox)
        {
            if (lstBox.Items.Count > 0)
            {
                lstBox.Items.Clear();
            }
        }

        public void ReloadBuckerList()
        {
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
        }

        private void SwitchValues(ListBox source, ListBox target)
        {
            var listRemoval = new System.Collections.ArrayList();

            // Find the number of items selected in the List and items selected
            // Call the move function equal to the number of items selected
            // Remove from Source list, The items moved

            // iterate through source list
            foreach (ListItem itemCurrent in source.Items)
            {
                if (itemCurrent.Selected)
                {
                    // 1. DETERIMNE - find out which item(s) was selected of SOURCE LIST
                    //Response.Write(itemCurrent.ToString());

                    // 2. MOVE / COPY - Add it to TARGET LIST
                    var copy = new ListItem(itemCurrent.Text, itemCurrent.Value);
                    target.Items.Add(copy);

                    // 3. REMOVE - Add to external list so we can remove afterwards from the source
                    listRemoval.Add(itemCurrent);

                    // 4. Set the moved selection as selected, so quickly can move back
                    // avoiding the user from reselecting items, disable any preveiously selected items     
                    if (target.SelectedItem != null)
                    {
                        target.SelectedItem.Selected = false;
                    }
                    target.Items.FindByValue(copy.Value).Selected = true;
                }
            }

            foreach (ListItem itemToRemove in listRemoval)
            {
                source.Items.Remove(itemToRemove);
            }
        }

        #endregion

        #region events

        protected override void Page_Load(object sender, EventArgs e)
        {
            //CalendarExtenderStartDate.Format = SessionVariables.UserDateFormat;
            lblUserDateTimeFormat.Text = "Date Format: " + SessionVariables.UserDateFormat;
        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            SwitchValues(lstSource, lstTarget);
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            SwitchValues(lstTarget, lstSource);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchSource.Text.Trim()) && SourceTable != null)
            {
                ClearItems(lstSource);
                ClearItems(lstTarget);

                var sourceData = SourceTable.Where(x => x.EntityName.Contains(txtSearchSource.Text.Trim())).ToList();

                lstSource.DataSource = sourceData;
                lstSource.DataTextField = "EntityName";
                lstSource.DataValueField = "SystemEntityTypeId";
                lstSource.DataBind();

                LoadCurrentTarget();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchSource.Text = String.Empty;
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
        }

        protected void lnkTabGeneralSettings_Click(object sender, EventArgs e)
        {
            for (var iCount = 0; iCount < toc.Controls.Count; iCount++)
            {
                if (toc.Controls[iCount] == lnkTabGeneralSettings.Parent)
                {
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] = "selected";
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Add("background-color", "Yellow");
                }
                else
                {
                    try
                    {
                        if (((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] == "selected" || ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle["background-color"] == "Yellow")
                        {
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.Clear();
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Remove("background-color");
                        }
                    }
                    catch { }
                }
            }
            cosmeticSettings.Visible = false;
            otherSettings.Visible = false;
            generalSettings.Visible = true;
        }

        protected void lnkTabCosmeticSettings_Click(object sender, EventArgs e)
        {
            for (var iCount = 0; iCount < toc.Controls.Count; iCount++)
            {
                if (toc.Controls[iCount] == lnkTabCosmeticSettings.Parent)
                {
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] = "selected";
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Add("background-color", "Yellow");
                }
                else
                {
                    try
                    {
                        if (((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] == "selected" || ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle["background-color"] == "Yellow")
                        {
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.Clear();
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Remove("background-color");
                        }
                    }
                    catch { }
                }
            }
            otherSettings.Visible = false;
            generalSettings.Visible = false;
            cosmeticSettings.Visible = true;
        }

        protected void lnkTabOtherSettings_Click(object sender, EventArgs e)
        {
            for (var iCount = 0; iCount < toc.Controls.Count; iCount++)
            {
                if (toc.Controls[iCount] == lnkTabOtherSettings.Parent)
                {
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] = "selected";
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Add("background-color", "Yellow");
                }
                else
                {
                    try
                    {
                        if (((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] == "selected" || ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle["background-color"] == "Yellow")
                        {
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.Clear();
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Remove("background-color");
                        }
                    }
                    catch { }
                }
            }
            cosmeticSettings.Visible = false;
            generalSettings.Visible = false;
            otherSettings.Visible = true;
        }

        protected void lnkTabAll_Click(object sender, EventArgs e)
        {
            for (var iCount = 0; iCount < toc.Controls.Count; iCount++)
            {
                if (toc.Controls[iCount] == lnkTabAll.Parent)
                {
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] = "selected";
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Add("background-color", "Yellow");
                }
                else
                {
                    try
                    {
                        if (((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] == "selected" || ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle["background-color"] == "Yellow")
                        {
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.Clear();
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Remove("background-color");
                        }
                    }
                    catch { }
                }
            }
            otherSettings.Visible = true;
            generalSettings.Visible = true;
            cosmeticSettings.Visible = true;
        }

        #endregion

    }

}
