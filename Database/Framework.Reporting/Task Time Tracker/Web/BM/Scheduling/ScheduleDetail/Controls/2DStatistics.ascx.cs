using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TaskTimeTracker.Components.Module.TimeTracking;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.ApplicationUser;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;
using System.Globalization;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Controls
{
    public partial class _2DStatistics : System.Web.UI.UserControl
    {

        #region Properties

        public string XAxisColumn
        {
            get { return drpXAxis.SelectedValue; }
        }

        public string YAxisColumn
        {
            get { return drpYAxis.SelectedValue; }
        }

        public string XAxisColumnText
        {
            get
            {
                if (drpXAxis.SelectedItem != null)
                {
                    return drpXAxis.SelectedItem.Text;
                }
                else
                {
                    return drpXAxis.SelectedValue;
                } 
            }
        }

        public string YAxisColumnText
        {
            get {
                if (drpYAxis.SelectedItem != null)
                {
                    return drpYAxis.SelectedItem.Text;
                }
                else
                {
                    return drpYAxis.SelectedValue;
                }
            }
        }

        public string ZAxisColumn
        {
            get { return drpZAxis.SelectedValue; }
        }

        public string AggeregateFunction
        {
            get { return drpFunction.SelectedValue; }
        }

        public string AggeregateVisibility
        {
            get { return drpShowAggeregate.SelectedValue; }
        }

        public string SettingCategory
        {
            get
            {
                return Convert.ToString(ViewState["SettingCategory"]);
            }
            set
            {
                ViewState["SettingCategory"] = value;
            }
        }

        public int SearchKeyId
        {
            get;
            set;
        }

        #endregion

        #region Methods

        private void BindComboBoxes()
        {
            var dtUsers = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            dtUsers = (from row in dtUsers.AsEnumerable()
                       where row["ApplicationId"].ToString() == SessionVariables.RequestProfile.ApplicationId.ToString()
                       select row).CopyToDataTable();

            UIHelper.LoadDropDown(dtUsers, drpPersons,
                ApplicationUserDataModel.DataColumns.FullName, ApplicationUserDataModel.DataColumns.ApplicationUserId);

            var dtCategory = ScheduleDetailActivityCategoryDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(dtCategory, drpWorkCategory,
                ScheduleDetailActivityCategoryDataModel.DataColumns.Name,
                ScheduleDetailActivityCategoryDataModel.DataColumns.ScheduleDetailActivityCategoryId);

            drpPersons.Items.Insert(0, new ListItem() { Text = "All", Value = "-1", Selected = true });
            drpWorkCategory.Items.Insert(0, new ListItem() { Text = "All", Value = "-1", Selected = true });

        }

        private void BindData()
        {
            if (XAxisColumn != YAxisColumn)
            {

                var objModel = new ScheduleDetailDataModel();

                if (drpPersons.SelectedValue != "-1")
                {
                    objModel.PersonId = Convert.ToInt32(drpPersons.SelectedValue);
                }

                if (drpWorkCategory.SelectedValue != "-1")
                {
                    objModel.ScheduleDetailActivityCategoryId = Convert.ToInt32(drpWorkCategory.SelectedValue);
                }

                objModel.FromSearchDate = oDateRange.FromDate;
                objModel.ToSearchDate = oDateRange.ToDate;
                objModel.Message = txtMessage.Text.Trim();

                var dt = ScheduleDetailDataManager.Search(objModel, SessionVariables.RequestProfile);

                if (dt.Rows.Count > 0)
                {

                    var xValues = (from row in dt.AsEnumerable()
                                   select row[XAxisColumn].ToString().Trim())
                                        .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

                    var yValues = (from row in dt.AsEnumerable()
                                   select row[YAxisColumn].ToString().Trim())
                                        .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

                    var tableElement = new HtmlGenericControl("table");
                    tableElement.Attributes["class"] = "table table-bordered";

                    AddHeaderRow(xValues, tableElement, dt);

                    foreach (var yValue in yValues)
                    {
                        var yTextValue = yValue;
                        if (YAxisColumn != YAxisColumnText)
                        {
                            yTextValue = (from row in dt.AsEnumerable()
                                          where row[YAxisColumn].ToString() == yValue
                                          select row[YAxisColumnText].ToString()).First();
                        }
                        AddDetailRow(yValue, yTextValue, xValues, dt, tableElement);
                    }

                    if (AggeregateVisibility == "xAxis" || AggeregateVisibility == "Both")
                    {
                        AddAggeregateRow(xValues, dt, tableElement);
                    }

                    contentHolder.Controls.Add(tableElement);

                }
            }
        }

        private void AddHeaderRow(List<string> xValues, HtmlGenericControl tableElement, DataTable dt)
        {
            var headerControl = new HtmlGenericControl("tr");
            headerControl.Attributes["class"] = "row text-center";

            var cell = new HtmlGenericControl("th");
            cell.Attributes["class"] = "col-sm-2";
            cell.InnerText = "";
            headerControl.Controls.Add(cell);

            cell = new HtmlGenericControl("th");
            cell.Attributes["class"] = "col-sm-11";
            cell.Attributes["colspan"] = (xValues.Count + 1).ToString();
            cell.InnerText = XAxisColumnText;
            headerControl.Controls.Add(cell);

            tableElement.Controls.Add(headerControl);

            var headerControl1 = new HtmlGenericControl("tr");
            headerControl1.Attributes["class"] = "row text-center";

            cell = new HtmlGenericControl("th");
            cell.Attributes["class"] = "col-sm-2";
            cell.InnerText = YAxisColumnText;
            headerControl1.Controls.Add(cell);

            foreach (var str in xValues)
            {
                var xTextValue = str;
                if (XAxisColumn != XAxisColumnText)
                {
                    xTextValue = (from row in dt.AsEnumerable()
                                     where row[XAxisColumn].ToString() == str
                                     select row[XAxisColumnText].ToString()).First();
                }

                cell = new HtmlGenericControl("th");
                cell.Attributes["class"] = "col-sm-1";
                cell.InnerText = xTextValue;
                headerControl1.Controls.Add(cell);
            }

            if (AggeregateVisibility == "yAxis" || AggeregateVisibility == "Both")
            {
                cell = new HtmlGenericControl("th");
                cell.Attributes["class"] = "col-sm-1";
                cell.InnerText = "Total";
                headerControl1.Controls.Add(cell);
            }

            tableElement.Controls.Add(headerControl1);
        }

        private void AddDetailRow(string yValue, string yTextValue, List<string> xValues, DataTable data, HtmlGenericControl tableElement)
        {
            var rowAggeregates = new List<decimal>();
            var headerControl1 = new HtmlGenericControl("tr");
            headerControl1.Attributes["class"] = "row text-right";

            var cell = new HtmlGenericControl("td");
            cell.Attributes["class"] = "col-sm-2";
            cell.InnerText = yTextValue;
            headerControl1.Controls.Add(cell);

            var middlePageUrl = Page.ResolveUrl("~/" + SessionVariables.CurrentApplicationCode + "/Admin/SearchKeyGenerator");                

            foreach (var xValue in xValues)
            {               

                decimal aggValue = 0;
                try
                {
                    var list = (from row in data.AsEnumerable()
                                where row[XAxisColumn].ToString() == xValue && row[YAxisColumn].ToString() == yValue
                                select Convert.ToDecimal(row[ZAxisColumn])).ToList();
                    aggValue = GetAggergateFunctionValue(list);
                }
                catch { }

                cell = new HtmlGenericControl("td");
                cell.Attributes["class"] = "col-sm-1";

                cell.InnerHtml = "<a href=\"" + middlePageUrl + "?SearchKey=" + SearchKeyId + "&" +
                                    "entityName=ScheduleDetail&" +
                                    drpXAxis.SelectedValue + "=" + xValue + "&" +
                                    drpYAxis.SelectedValue + "=" + yValue + " \">" +
                                        Convert.ToDecimal(aggValue)
										.ToString(("N")) +
                                "</a>";

                headerControl1.Controls.Add(cell);

                rowAggeregates.Add(aggValue);
            }

            if (AggeregateVisibility == "yAxis" || AggeregateVisibility == "Both")
            {
                cell = new HtmlGenericControl("td");
                cell.Attributes["class"] = "col-sm-1 success";
				cell.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
				cell.InnerText = NumberFormatHelper.SetNumberFormat(GetAggergateFunctionValue(rowAggeregates, true));
                headerControl1.Controls.Add(cell);
            }

            tableElement.Controls.Add(headerControl1);
        }

        private void AddAggeregateRow(List<string> xValues, DataTable data, HtmlGenericControl tableElement)
        {
            var rowAggeregates = new List<decimal>();

            var headerControl1 = new HtmlGenericControl("tr");
            headerControl1.Attributes["class"] = "row text-right";

            var cell = new HtmlGenericControl("td");
            cell.Attributes["class"] = "col-sm-2";
			cell.InnerText = "Total";
            headerControl1.Controls.Add(cell);

            foreach (var xValue in xValues)
            {
                decimal aggValue = 0;
                try
                {
                    var list = (from row in data.AsEnumerable()
                                where row[XAxisColumn].ToString() == xValue
                                select Convert.ToDecimal(row[ZAxisColumn])).ToList();
                    aggValue = GetAggergateFunctionValue(list);
                }
                catch { }

                cell = new HtmlGenericControl("td");
                cell.Attributes["class"] = "col-sm-1 success";
				cell.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
				cell.InnerText = NumberFormatHelper.SetNumberFormat(aggValue);
                headerControl1.Controls.Add(cell);

                rowAggeregates.Add(aggValue);
            }

            if (AggeregateVisibility == "yAxis" || AggeregateVisibility == "Both")
            {
                cell = new HtmlGenericControl("td");
                cell.Attributes["class"] = "col-sm-1 success";
                cell.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
				cell.InnerText = NumberFormatHelper.SetNumberFormat(GetAggergateFunctionValue(rowAggeregates, true));
				//cell.InnerText = GetAggergateFunctionValue(rowAggeregates, true).ToString(("N"));
				 
                headerControl1.Controls.Add(cell);
            }

            tableElement.Controls.Add(headerControl1);
        }

        private decimal GetAggergateFunctionValue(List<Decimal> list, bool isAggeregateRow = false)
        {
            decimal value = 0;

            if (AggeregateFunction == "Sum")
            {
                // get Sum
                value = list.Sum();
            }
            else if (AggeregateFunction == "Count")
            {
                if (!isAggeregateRow)
                {
                    // get Count
                    value = list.Count();
                }
                else
                {
                    value = list.Sum();
                }
            }
            else if (AggeregateFunction == "Average")
            {
                // get Average
                value = list.Average();
            }
            else if (AggeregateFunction == "Min")
            {
                // get Min
                value = list.Min();
            }
            else if (AggeregateFunction == "Max")
            {
                // get Max
                value = list.Max();
            }

            return value;
        }

        private void LoadUserPreferences()
        {

            drpPersons.SelectedValue        = PerferenceUtility.GetUserPreferenceByKey(ScheduleDetailDataModel.DataColumns.PersonId, SettingCategory);
            drpWorkCategory.SelectedValue   = PerferenceUtility.GetUserPreferenceByKey(ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategoryId, SettingCategory);
            txtMessage.Text                 = PerferenceUtility.GetUserPreferenceByKey(ScheduleDetailDataModel.DataColumns.Message, SettingCategory);

            drpXAxis.SelectedValue          = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.XAxisKey, SettingCategory);
            drpYAxis.SelectedValue          = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.YAxisKey, SettingCategory);
            drpZAxis.SelectedValue          = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ZAxisKey, SettingCategory);

            drpFunction.SelectedValue       = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.AggeregateFunctionKey, SettingCategory);
            drpShowAggeregate.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ShowAggeregateKey, SettingCategory);
            
            var searchDates               = PerferenceUtility.GetUserPreferenceByKey(ScheduleDetailDataModel.DataColumns.WorkDate, SettingCategory);
            var dates                     = searchDates.Split('&');

            oDateRange.SettingCategory = SettingCategory + "DateRangeControl";
            oDateRange.SetDateValues(searchDates);

            if (dates.Length > 2)
            {
                oDateRange.Checked = bool.Parse(dates[2]);
            }
        }

        private int GetSearchKey()
        {
            var searchKeyId = 0;
            
            var data = new SearchKeyDataModel();
            data.Name = DateTime.Now.ToLongTimeString();
            data.View = "ScheduleDetail";
            data.SortOrder = 1;
            data.Description = "ScheduleDetail";

            searchKeyId = SearchKeyDataManager.Create(data, SessionVariables.RequestProfile);

            var listKeys = new List<string>();

            listKeys.Add(ScheduleDetailDataModel.DataColumns.PersonId);
            listKeys.Add(ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategoryId);

            listKeys.Add(ScheduleDetailDataModel.DataColumns.Message);
            listKeys.Add(ScheduleDetailDataModel.DataColumns.WorkDate);

            listKeys.Add(ApplicationCommon.XAxisKey);
            listKeys.Add(ApplicationCommon.YAxisKey);
            listKeys.Add(ApplicationCommon.ZAxisKey);
            listKeys.Add(ApplicationCommon.AggeregateFunctionKey);
            listKeys.Add(ApplicationCommon.ShowAggeregateKey);

            foreach (var key in listKeys)
            {
                //try
                //{
                var columnName = key;
                var columnValue = PerferenceUtility.GetUserPreferenceByKey(key, SettingCategory);

                var dataDetail = new SearchKeyDetailDataModel();
                dataDetail.SearchKeyId = searchKeyId;

                //ApplicationCommon.UpdateUserPreference(SettingCategory, columnName, columnValue);
                dataDetail.SearchParameter = columnName;
                dataDetail.SortOrder = 1;
                var detailId = SearchKeyDetailDataManager.Create(dataDetail, SessionVariables.RequestProfile);

                var dataDetailItem = new SearchKeyDetailItemDataModel();
                dataDetailItem.SearchKeyDetailId = detailId;
                dataDetailItem.SortOrder = 1;

                dataDetailItem.Value = columnValue;
                SearchKeyDetailItemDataManager.Create(dataDetailItem, SessionVariables.RequestProfile);

            }

            return searchKeyId;
        }

        private void SaveUserPreferences()
        {
            PerferenceUtility.UpdateUserPreference(SettingCategory, ScheduleDetailDataModel.DataColumns.PersonId, drpPersons.SelectedValue);
            PerferenceUtility.UpdateUserPreference(SettingCategory, ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategoryId, drpWorkCategory.SelectedValue);
            PerferenceUtility.UpdateUserPreference(SettingCategory, ScheduleDetailDataModel.DataColumns.Message, txtMessage.Text.Trim());

            PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.XAxisKey, drpXAxis.SelectedValue);
            PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.YAxisKey, drpYAxis.SelectedValue);
            PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.ZAxisKey, drpZAxis.SelectedValue);

            PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.ShowAggeregateKey, drpShowAggeregate.SelectedValue);
            PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.AggeregateFunctionKey, drpFunction.SelectedValue);
            
            oDateRange.SaveDateValues(oDateRange.DateRangeDropDown.SelectedValue, oDateRange.FromDateTime, oDateRange.ToDateTime);

            var fromDate = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.FromDateTime);
            var toDate = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.ToDateTime);

            var searchDates = fromDate + "&" + toDate + "&" + oDateRange.Checked;
            PerferenceUtility.UpdateUserPreference(SettingCategory, ScheduleDetailDataModel.DataColumns.WorkDate, searchDates);
        }
        
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            SettingCategory = "TwoDimensionalStatiSticsScheduleDetail";
            if (!IsPostBack)
            {
                BindComboBoxes();
                LoadUserPreferences();
                SearchKeyId = GetSearchKey();
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveUserPreferences();
            SearchKeyId = GetSearchKey();
            BindData();
        }

        #endregion

    }
}