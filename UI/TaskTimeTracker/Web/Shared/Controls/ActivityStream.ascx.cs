﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Audit;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.Web.ActivityStream;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using Shared.UI.Web.ActivityStream.Controls;
using System.Configuration;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{

    public partial class ActivityStreamControl : BaseControl
    {

        #region properties

        public int? ActivityStreamGroupIntervalInSeconds
        {
            get
            {
                if (ViewState["ActivityStreamGroupInterval"] != null)
                {
                    return Convert.ToInt32(ViewState["ActivityStreamGroupInterval"]);
                }
                return null;
            }
            set
            {
                ViewState["ActivityStreamGroupInterval"] = value;
            }
        }

        public int? ActivityStreamAuditId
        {
            get
            {
                if (ViewState["ActivityStreamAuditId"] != null)
                {
                    return Convert.ToInt32(ViewState["ActivityStreamAuditId"]);
                }
                return null;
            }
            set
            {
                ViewState["ActivityStreamAuditId"] = value;
            }
        }

        public string ActivityStreamName
        {
            get
            {
                return Convert.ToString(ViewState["ActivityStreamName"]);
            }
            set
            {
                ViewState["ActivityStreamName"] = value;
            }
        }

        public int ActivityStreamPageSize
        {
            get
            {
                return Convert.ToInt32(ViewState["ActivityStreamPageSize"]);
            }
            set
            {
                ViewState["ActivityStreamPageSize"] = value;
            }
        }

        public int ActivityStreamTempPageSize
        {
            get
            {
                return Convert.ToInt32(ViewState["ActivityStreamTempPageSize"]);
            }
            set
            {
                ViewState["ActivityStreamTempPageSize"] = value;
            }
        }

        public string DataViewMode
        {
            get
            {
                return Convert.ToString(ViewState["DataViewMode"]);
            }
            set
            {
                ViewState["DataViewMode"] = value;
            }
        }

        public string PagingStyle
        {
            get
            {
                return Convert.ToString(ViewState["PagingStyle"]);
            }
            set
            {
                ViewState["PagingStyle"] = value;
            }
        }

        public int DateInterval
        {
            get
            {
                return Convert.ToInt32(ViewState["DateInterval"]);
            }
            set
            {
                ViewState["DateInterval"] = value;
            }
        }

        private int RowCount
        {
            get
            {
                return Convert.ToInt32(ViewState["RowCount"]);
            }
            set
            {
                ViewState["RowCount"] = value;
            }
        }

        public int CurrentIndex
        {
            get
            {
                if (ViewState["CurrentIndex"] == null)
                {
                    ViewState["CurrentIndex"] = 1;
                }

                return int.Parse(ViewState["CurrentIndex"].ToString());
            }
            set
            {
                ViewState["CurrentIndex"] = value;
            }
        }

        public int TotalPages
        {
            get
            {
                if (ViewState["TotalPages"] == null)
                {
                    ViewState["TotalPages"] = 0;
                }

                return int.Parse(ViewState["TotalPages"].ToString());
            }
            set
            {
                ViewState["TotalPages"] = value;
            }
        }

        #endregion properties

        #region methods

        private string GetActivityTimeText(DateTime createdDate, double appTimeZoneDifference)
        {
            var ts = DateTime.UtcNow.AddHours(appTimeZoneDifference) - createdDate;
            var addTimeToAlert = String.Empty;

            if (ts.Days >= 31 && ts.Days <= 365)
            {
                //int monthsAgo = 0;
                addTimeToAlert = " About " + (ts.Days / 31) + " months ago ";
            }

            else if (ts.Days < 31 && ts.Days > 0)
            {
                addTimeToAlert = " About " + ts.Days + " days ago";
            }

            else if (ts.Hours < 24 && ts.Hours > 0)
            {
                addTimeToAlert = " About " + ts.Hours + " hours ago";
            }

            else if (ts.Minutes < 60 && ts.Minutes > 0)
            {
                addTimeToAlert = " About " + ts.Minutes + " minutes ago";
            }

            else if (ts.Seconds < 60 && ts.Seconds > 0)
            {
                addTimeToAlert = " About " + ts.Seconds + " seconds ago";
            }

            return addTimeToAlert;

        }

        private string GetActivityTimeGroup(DateTime createdDate, double appTimeZoneDifference)
        {
            var ts = DateTime.UtcNow.AddHours(appTimeZoneDifference) - createdDate;
            var timeGroup = String.Empty;

            if (ts.Days < 1)
            {
                timeGroup = "Today";
            }
            else if (ts.Days < 7 && ts.Days >= 1)
            {
                timeGroup = "This Week";
            }
            else if (ts.Days < 31 && ts.Days >= 7)
            {
                timeGroup = "This Month";
            }
            else if (ts.Days < 90 && ts.Days >= 31)
            {
                timeGroup = "This Quarter";
            }
            else if (ts.Days < 365 && ts.Days >= 90)
            {
                timeGroup = "This Year";
            }
            else
            {
                timeGroup = "Before This Year";
            }

            return timeGroup;

        }

        private void LoadActivityStream()
        {
            var data = new UserPreferenceCategoryDataModel();

            if (!string.IsNullOrEmpty(ActivityStreamName))
            {
                PreferenceUtility.CreateUserPreferenceCategoryIfNotExists(ActivityStreamName, SessionVariables.ApplicationUserFullName + " " + ActivityStreamName);
            }

            if (ActivityStreamAuditId != null)
            {
                PreferenceUtility.UpdateUserPreference(ActivityStreamName, ActivityStreamCommon.ActivityStreamAuditId, ActivityStreamAuditId.ToString());
            }

            hypSettings.NavigateUrl += Page.GetRouteUrl("ActivityStreamRoute", new { Action = "Settings", CategoryName = ActivityStreamName,SetId=true });
            try
            {
                var setId = ApplicationCommon.GetSetId();
                if (Convert.ToString(Page.RouteData.Values["Action"]).ToLower().Contains("detail") && !string.IsNullOrEmpty(Convert.ToString(Page.RouteData.Values["SetId"])))
                {
                    hypSettings.NavigateUrl += "&SetId=" + setId + "&returnPage=appUserDetail";
                }
                else if (Convert.ToString(Page.RouteData.Values["Action"]).ToLower().Contains("update") && !string.IsNullOrEmpty(Convert.ToString(Page.RouteData.Values["SetId"])))
                {
                    hypSettings.NavigateUrl += "&SetId=" + setId + "&returnPage=appUserUpdate";
                }
            }
            catch { }
            ProcessAlerts();
        }

        private void ProcessAlerts()
        {
            LoadActivityStreamSettings();
            BindData();
        }

        private void BindData()
        {
            var dtAuditdata = LoadData();
            var activityAlerts = CreateActivityStream(dtAuditdata);
            Bindrepeater(activityAlerts);
        }

        private void LoadActivityStreamSettings()
        {
            var userPreferenceCategoryName       = ActivityStreamName;
            var userPreferenceSettingsDictionary = ActivityStreamCommon.GetUserPreferencesForActivityStream(userPreferenceCategoryName);
            DateInterval                         = Convert.ToInt32(userPreferenceSettingsDictionary[ActivityStreamCommon.DateRangeKey]);
            var backGroundColor                  = userPreferenceSettingsDictionary[ActivityStreamCommon.BackGroungColorKey];
            var width                            = userPreferenceSettingsDictionary[ActivityStreamCommon.WidthKey];
            var height                           = userPreferenceSettingsDictionary[ActivityStreamCommon.HeightKey];
            DataViewMode                         = userPreferenceSettingsDictionary[ActivityStreamCommon.DataTypeKey];
            ActivityStreamPageSize               = Convert.ToInt32(userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamPageSize]);
            lblActivityStreamTitle.Text          = userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamTitle];
            PagingStyle                          = userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamPagingStyle];
            ActivityStreamGroupIntervalInSeconds = Convert.ToInt32(userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamGroupInterval]);

            ActivityStreamTempPageSize = ActivityStreamPageSize;
            lblDataViewMode.Text = "<font color='Teal'>  Data View Mode: <font/>" + " <font color='Black'>*" + DataViewMode + "<font/>";
            tblMain.Style["background-color"] = "#" + backGroundColor;
            //tblMain.Height = height;
            divContainer.Style.Add("width", width + "px");
            divContainer.Style.Add("height", height + "px");
            divContainer.Style.Add("overflow", "auto");

            var x = "<font color='Black'>" + String.Format("{0:MM/dd/yyyy}", DateTime.Now.AddDays(DateInterval * (-1))) + " - " +
                                            String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "<font/>";
            lblDateRange.Text = "<font color='Teal'>  Date Range: <font/>" + " " + x;

            try
            {
                ActivityStreamAuditId = Convert.ToInt32(userPreferenceSettingsDictionary[ActivityStreamCommon.ActivityStreamAuditId]);
                lblActivityStreamAuditId.Text = lblCount.Text = "<font color='Teal'>" + "Audit Id: " + "<font/>" + "<font color='Black'>" + ActivityStreamAuditId + "<font/>";
            }
            catch { }
        }

        private DataTable LoadData()
        {
            var data = new AuditHistory();
            data.FromSearchDate = DateTime.Now.AddDays(DateInterval * (-1));
            data.ToSearchDate = DateTime.Now;
            data.PersonId = ActivityStreamAuditId;
            data.DataViewMode = DataViewMode;
            return Framework.Components.Audit.AuditHistoryDataManager.SearchForActivityStream(data, ActivityStreamTempPageSize, CurrentIndex, SessionVariables.RequestProfile);
        }

        private DataRow[] GetGroupedRecords(string systemEntity, DateTime createdDate, DataTable dt)
        {
            var endDate = createdDate.AddSeconds(ActivityStreamGroupIntervalInSeconds.Value * -1);
            createdDate = createdDate.AddSeconds(1);
            var expression = AuditHistory.DataColumns.SystemEntity + " = '" + systemEntity + "'";
            expression += " and " + BaseDataModel.BaseDataColumns.CreatedDate + " <= '" + createdDate + "'";
            expression += " and " + BaseDataModel.BaseDataColumns.CreatedDate + " >= '" + endDate + "'";
            var drs = dt.Select(expression);
            return drs;
        }

        private List<UserActivityItem> CreateActivityStream(DataTable dtAuditdata)
        {
            var activitySteam = new List<UserActivityItem>();
            var dv = dtAuditdata.DefaultView;
            var rowFilter = String.Empty;
            rowFilter = AuditHistory.DataColumns.AuditAction + " in ('Insert', 'Update', 'Delete') ";
            //DataViewMode = "Test";
            //if (DataViewMode != "Both")
            //{
            //    if (DataViewMode == "Real Data")
            //    {
            //        rowFilter += " and " + DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey + " > 0";
            //    }
            //    else
            //    {
            //        rowFilter += " and " + DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey + " < 0";
            //    }
            //}
            var dtExcludedEntities = ActivityStreamCommon.GetExcludedSystemEntities(ActivityStreamName);
            if (dtExcludedEntities != null && dtExcludedEntities.Rows.Count > 0)
            {
                var strEntities = String.Empty;
                foreach (DataRow dr in dtExcludedEntities.Rows)
                {
                    if (!string.IsNullOrEmpty(strEntities))
                    {
                        strEntities += ", ";
                    }
                    strEntities += Convert.ToString(dr[UserPreferenceSelectedItemDataModel.DataColumns.Value]);
                }
                rowFilter += " and " + AuditHistory.DataColumns.SystemEntityId + " not in (" + strEntities + ")";
            }

            dv.RowFilter = rowFilter;
            //dv.Sort = "CreatedDate DESC";

            double appTimeZoneDifference = 0;
            try
            {
                appTimeZoneDifference = Convert.ToDouble(ConfigurationManager.AppSettings["UTCTimeZoneDifference"]);
            }
            catch { }
            var prevCreatedBy = String.Empty;
            var prevTimeGroup = String.Empty;
            if (dv.Count > 0)
            {
                RowCount = Convert.ToInt32(dtAuditdata.Rows[0]["TotalCount"]);
                    
                dtAuditdata = dv.ToTable();
                for (var iCount = 0; iCount < dtAuditdata.Rows.Count; iCount++)
                {
                    var drv = dtAuditdata.Rows[iCount];
                    var userActivity = new UserActivityItem();
                    var activityText = String.Empty;

                    var systemEntity          = Convert.ToString(drv[AuditHistory.DataColumns.SystemEntity]);
                    var entityRecordId        = Convert.ToInt32(drv[AuditHistory.DataColumns.EntityKey]);
                    var auditAction           = Convert.ToString(drv[AuditHistory.DataColumns.AuditAction]);
                    var createdBy             = Convert.ToString(drv[AuditHistory.DataColumns.Person]);
                    var createdDate           = Convert.ToDateTime(drv[BaseDataModel.BaseDataColumns.CreatedDate]);

                    var addTimeToAlert        = GetActivityTimeText(createdDate, appTimeZoneDifference);
                    userActivity.TimeGrouping = GetActivityTimeGroup(createdDate, appTimeZoneDifference);

                    //Check whether the entity record still exist in the DB,
                    //need a common search procedure in the Business Layer or change the way audit records are sent from DB

                    if (prevCreatedBy == createdBy)
                        userActivity.ShowImage = false;

                    if (userActivity.TimeGrouping != prevTimeGroup)
                    {
                        userActivity.ShowImage = true;
                        userActivity.ShowTimeGroup = true;
                    }

                    var drs = GetGroupedRecords(systemEntity, createdDate, dtAuditdata);

                    if (drs != null && drs.Length > 1)
                    {
                        userActivity.gropedItems = new List<UserActivityChildItem>();
                        foreach (var drChild in drs)
                        {
                            var childItem = new UserActivityChildItem();
                            var childAuditAction = Convert.ToString(drChild[AuditHistory.DataColumns.AuditAction]);
                            var childEntityKey = Convert.ToInt32(drChild[AuditHistory.DataColumns.EntityKey]);
                            childItem.ChildText = GetChildActivityText(systemEntity, childEntityKey, childAuditAction);
                            userActivity.gropedItems.Add(childItem);
                        }
                        iCount += drs.Length - 1;
                        userActivity.TimeStampText = "\t\t\t\t\t\t<font  size ='1.5'; color='Teal'>" + " - " + addTimeToAlert + "<font/>";
                        activityText = "<font  color='Black'>" + createdBy + " changed records in  " + systemEntity + "<font/>" + "<br />";
                    }
                    else
                    {
                        addTimeToAlert = "<font  size ='1.5'; color='Teal'>" + " - " + addTimeToAlert + "<font/>";
                        activityText = "<font  color='Black'>" + GetActivityText(createdBy, systemEntity, entityRecordId, auditAction) + "<font/>" + "<br />";
                        activityText += "\t\t\t\t\t\t" + addTimeToAlert;
                    }                    

                    userActivity.ActivityText = activityText;
                    activitySteam.Add(userActivity);
                    prevCreatedBy = createdBy;
                    prevTimeGroup = userActivity.TimeGrouping;
                }
            }
            lblCount.Text = "<font color='Teal'>" + "Total Records: " + "<font/>" + "<font color='Black'>" + RowCount + "<font/>";
            return activitySteam;
        }

        private string GetActivityText(string createdBy, string systemEntity, int entityKey, string auditAction)
        {
            var text = String.Empty;
            if (auditAction == "Delete") // no hyperlink
            {
                text = createdBy + " " + GetAuditActionText(auditAction) + " a record in " + systemEntity;
            }
            else
            {
                text = createdBy + GetClickableLinkText(systemEntity, entityKey, auditAction) + " a record in " + systemEntity;
            }
            return text;
        }

        private string GetChildActivityText(string systemEntity, int entityKey, string auditAction)
        {
            var text = String.Empty;
            if (auditAction == "Delete") // no hyperlink
            {
                text = GetAuditActionText(auditAction) + " a record";
            }
            else
            {
                text = GetClickableLinkText(systemEntity, entityKey, auditAction) + " a record";
            }
            return text;
        }

        private string GetClickableLinkText(string systemEntity, int entityKey, string auditAction)
        {
            var hLink = String.Empty;
            hLink = " <a href=" + ResolveUrl("~/" + systemEntity + "/Details.aspx?SetId=" + entityKey) + ">" + GetAuditActionText(auditAction) + "</a>";
            return hLink;
        }

        private string GetAuditActionText(string auditAction)
        {
            var text = String.Empty;
            if (auditAction == "Insert")
            {
                text = auditAction + "ed";
            }
            else
            {
                text = auditAction + "d";
            }
            return text;
        }

        private void Bindrepeater(List<UserActivityItem> activityAlerts)
        {

            TotalPages = int.Parse((RowCount / ActivityStreamPageSize).ToString());
            if (RowCount % ActivityStreamPageSize != 0)
            {
                TotalPages += 1;
            }
            txtPagernumber.Text = CurrentIndex.ToString();
            //var Pds1 = new PagedDataSource();
            //Pds1.DataSource = activityAlerts;
            //Pds1.AllowPaging = true;
            //Pds1.PageSize = ActivityStreamTempPageSize;
            //Pds1.CurrentPageIndex = 0;
            try
            {
                activityAlerts[0].ShowTimeGroup = true;
                activityAlerts[0].ShowImage = true;

                //activityAlerts[CurrentIndex * ActivityStreamPageSize].ShowTimeGroup = true;
                //activityAlerts[CurrentIndex * ActivityStreamPageSize].ShowImage = true;
            }
            catch { }
            repAlerts.DataSource = activityAlerts;
            repAlerts.DataBind();

            var startIndex = (ActivityStreamPageSize * CurrentIndex) + 1;
            var endIndex = (ActivityStreamPageSize * CurrentIndex) + ActivityStreamPageSize;
            if (endIndex > activityAlerts.Count)
                endIndex = activityAlerts.Count;

            if (PagingStyle == "ActivityStreamPagingStyle1")
            {
                pagingStyle1.Visible = true;
                lblText.Text = " of " + TotalPages + " Pages   ";
            }
            else if (PagingStyle == "ActivityStreamPagingStyle2")
            {
                pagingStyle2.Visible = true;
                lb_FirstPage.Enabled = true;
                lb_PreviousPage.Enabled = true;
                lb_NextPage.Enabled = true;
                lb_LastPage.Enabled = true;

                if (CurrentIndex == 0)
                {
                    lb_FirstPage.Enabled = false;
                    lb_PreviousPage.Enabled = false;
                }

                if (CurrentIndex == (TotalPages - 1))
                {
                    lb_NextPage.Enabled = false;
                    lb_LastPage.Enabled = false;
                }
                lblPagerDescription.Text = startIndex + " - " + endIndex + " of " + activityAlerts.Count;
            }
            else if (PagingStyle == "ActivityStreamPagingStyle3")
            {
                pagingStyle3.Visible = true;
            }
            else if (PagingStyle == "ActivityStreamPagingStyle4")
            {
                pagingStyle4.Visible = true;
                CreatePagingControl();
                lblPagerDescription1.Text = startIndex + " - " + endIndex + " of " + activityAlerts.Count;
            }

        }

        private void CreatePagingControl()
        {
            plcPaging.Controls.Clear();
            for (var i = 0; i < TotalPages; i++)
            {
                var lnk = new LinkButton();
                lnk.ID = "lnkPage" + (i + 1);
                lnk.Text = (i + 1).ToString();
                lnk.Click += new EventHandler(lnk_Click);
                lnk.Font.Underline = false;
                plcPaging.Controls.Add(lnk);

                var spacer = new Label();
                spacer.Text = "&nbsp;";
                plcPaging.Controls.Add(spacer);

                if (i == CurrentIndex)
                {
                    lnk.Enabled = false;
                }
            }
        }

        #endregion

        #region events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadActivityStream();
            }
            else if (PagingStyle == "ActivityStreamPagingStyle4")
            {
                CreatePagingControl();
            }
        }

        protected void repFriends_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var adAuditActionDisplay = e.Item.FindControl("auditAction") as AuditActionDisplay;
                adAuditActionDisplay.LoadAlerts((UserActivityItem)e.Item.DataItem);
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            CurrentIndex = int.Parse(txtPagernumber.Text) - 1;
            BindData();
        }

        protected void lb_FirstPage_Click(object sender, EventArgs e)
        {
            CurrentIndex = 0;
            BindData();
        }

        protected void lb_PreviousPage_Click(object sender, EventArgs e)
        {
            CurrentIndex -= 1;
            BindData();
        }

        protected void lb_NextPage_Click(object sender, EventArgs e)
        {
            CurrentIndex += 1;
            BindData();
        }

        protected void lb_LastPage_Click(object sender, EventArgs e)
        {
            CurrentIndex = TotalPages - 1;
            BindData();
        }

        void lnk_Click(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            CurrentIndex = Convert.ToInt32(lnk.Text) - 1;
            BindData();
        }

        protected void lb_ShowMore_Click(object sender, EventArgs e)
        {
            CurrentIndex = 0;
            ActivityStreamTempPageSize += ActivityStreamPageSize;
            BindData();
        }

        #endregion

    }
}