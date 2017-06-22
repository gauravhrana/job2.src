using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Configuration;

namespace Shared.UI.Web.Controls
{
    public partial class DateRangeControl : BaseControl
    {

        #region Variables

        private string _key = "6";

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

        public DropDownList DateRangeDropDown
        {
            get
            {
                return drpDateRange;
            }
        }

        public CheckBox DateRangeCheckBox
        {
            get
            {
                return chkDate;
            }
        }

        public DateTime? FromDate
        {
            get
            {
                if (!string.IsNullOrEmpty(txtSearchVerticalFromDate.Text) && chkDate.Checked)
                {
					return DateTimeHelper.FromUserDateFormatToDate(txtSearchVerticalFromDate.Text.Trim());				
                }
                return null;
            }
        }

        public string FromDateTime
        {
            get
            {
                if (!string.IsNullOrEmpty(txtSearchVerticalFromDate.Text))
                {
                    return txtSearchVerticalFromDate.Text.Trim();
                }
                else
                    return String.Empty;
            }
        }

        public string ToDateTime
        {
            get
            {
                if (!string.IsNullOrEmpty(txtSearchVerticalToDate.Text))
                {
                    return txtSearchVerticalToDate.Text.Trim();
                }
                else
                    return String.Empty;
            }
        }

        public DateTime? ToDate
        {
            get
            {
                if (!string.IsNullOrEmpty(txtSearchVerticalToDate.Text) && chkDate.Checked)
                {
                    return DateTimeHelper.FromUserDateFormatToDate(txtSearchVerticalToDate.Text.Trim());	
                }

                return null;
            }
        }

        public int DateRangeFormatId
        {
            get
            {
                return Convert.ToInt32(ViewState["DateRangeFormatId"]);
            }
            set
            {
                ViewState["DateRangeFormatId"] = value;
            }
        }

        public bool Checked
        {
            get
            {
                return chkDate.Checked;
            }
            set
            {
                chkDate.Checked = value;
            }
        }

        #endregion

        #region Methods

        private void SetDateRangeStyle()
        {
            var dateRangeStyle = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DateRangeStyle).ToLower();

            if (dateRangeStyle == "horizontal")
            {
                if (spacecreator.Controls.Count != 0)
                {
                    spacecreator.Controls.RemoveAt(0);
                }
            }
            else
            {
                var lit = new LiteralControl();
                lit.Text = "<br /><br />";
                if (spacecreator.Controls.Count == 0)
                {
                    spacecreator.Controls.Add(lit);
                }
            }
        }

        public void LoadText()
        {
            var userDateRangeStyle = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DateRangeStyle);

            if (userDateRangeStyle == "Vertical")
            {
                VerticalStyle.Visible = true;
                txtSearchVerticalFromDate.Enabled = chkDate.Checked;
                txtSearchVerticalToDate.Enabled = chkDate.Checked;
                //VerticalFromDateCalender.Format = SessionVariables.UserDateFormat;
                //VerticalToDateCalender.Format = SessionVariables.UserDateFormat;

            }
        }

        public void HideLabel()
        {
            labelcell1.Visible = false;
        }

        public void SetDateValues(string fromdate, string todate)
        {
            txtSearchVerticalFromDate.Text = DateTimeHelper.FromApplicationDateFormatToUserDateFormat(fromdate);
            txtSearchVerticalToDate.Text = DateTimeHelper.FromApplicationDateFormatToUserDateFormat(todate);
        }

        public void SetDateValues(DateTime? fromdate, DateTime? todate)
        {
            if (fromdate != null)
            {
                txtSearchVerticalFromDate.Text = fromdate.Value.ToString(SessionVariables.UserDateFormat);
            }
            if (todate != null)
            {
                txtSearchVerticalToDate.Text = todate.Value.ToString(SessionVariables.UserDateFormat);
            }
        }

        public void SetDateValues(string value)
        {
            drpDateRange.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.DateRangeFormat, SettingCategory);

            if (drpDateRange.SelectedValue != "24" && drpDateRange.SelectedValue != "-1")
            {
                // this will directly return date values as string in user's preferred format
                var dates = DateRangeHelper.FillUpDate(drpDateRange.SelectedItem.Text, SessionVariables.UserDateFormat);

                if (!string.IsNullOrEmpty(dates[0]) || !string.IsNullOrEmpty(dates[1]))
                {
                    // alread in user's preferred format
                    txtSearchVerticalFromDate.Text = dates[0];
                    txtSearchVerticalToDate.Text = dates[1];
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var dates = value.Split(new string[] { "&" }, StringSplitOptions.None);
                    if (dates.Length > 1)
                    {
                        txtSearchVerticalFromDate.Text = DateTimeHelper.FromApplicationDateFormatToUserDateFormat(dates[0]);
                        txtSearchVerticalToDate.Text = DateTimeHelper.FromApplicationDateFormatToUserDateFormat(dates[1]);
                    }
                }
            }
        }

        public DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
        {
            var firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        public string GetKey()
        {
            return Key;
        }

        public string GetCheckBoxClientId()
        {
            return chkDate.ClientID;
        }

        public string GetDateRangeControlClientId()
        {
            return drpDateRange.ClientID;
        }

        public string GetFromDateTextBoxClientId()
        {
            return txtSearchVerticalFromDate.ClientID;
        }

        public string GetToDateTextBoxClientId()
        {
            return txtSearchVerticalToDate.ClientID;
        }

		public void SaveDateValues(string daterangevalue, string fromdate, string todate)
		{

			DateRangeFormatId = Convert.ToInt32(drpDateRange.SelectedValue);

			//saves the dropdownlist selected value
			PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.DateRangeFormat, daterangevalue);

            fromdate = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(fromdate);

			//saves the from date in the from textbox
			PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.FromDateRange, fromdate);

            todate = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(todate);

			//saves the To date in th To textbox
			PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.ToDateRange, todate);

		}

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadText();

                txtSearchVerticalFromDate.Enabled = chkDate.Checked;
                txtSearchVerticalToDate.Enabled = chkDate.Checked;

                var dateRangeData = DateRangeTitleDataManager.GetList(SessionVariables.RequestProfile);

                UIHelper.LoadDropDown
                (
                    dateRangeData
                    , drpDateRange
                    , StandardDataModel.StandardDataColumns.Name
                    , DateRangeTitleDataModel.DataColumns.DateRangeTitleId
                );

                //drpDateRange.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.DateRangeFormat, SettingCategory);
                //var Dates = DateRangeHelper.FillUpDate(drpDateRange.SelectedItem.Text, SessionVariables.UserDateFormat);

                //txtSearchVerticalFromDate.Text = Dates[0];
                //txtSearchVerticalToDate.Text = Dates[1];

                lblUserDateFormat.Text = "("+ SessionVariables.UserDateFormat + ")";
            }
            else
            {

            }

            //FromDate_CalendarExtender.Format = SessionVariables.UserDateFormat;
            //ToDate_CalendarExtender.Format   = SessionVariables.UserDateFormat;
            //VerticalFromDateCalender.Format  = SessionVariables.UserDateFormat;
            //VerticalToDateCalender.Format    = SessionVariables.UserDateFormat;

            SetDateRangeStyle();
        }

        protected void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            txtSearchVerticalFromDate.Enabled = chkDate.Checked;
            txtSearchVerticalToDate.Enabled = chkDate.Checked;
        }

        protected void drpDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateRangeFormatId              = Convert.ToInt32(drpDateRange.SelectedValue);
            PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.DateRangeFormat, DateRangeFormatId.ToString());
            var Dates                      = DateRangeHelper.FillUpDate(drpDateRange.SelectedItem.Text, SessionVariables.UserDateFormat);
            txtSearchVerticalFromDate.Text = Dates[0];
            txtSearchVerticalToDate.Text   = Dates[1];
        }

        #endregion

		protected void txtSearchVerticalFromDate_TextChanged(object sender, EventArgs e)
		{
			drpDateRange.SelectedValue = drpDateRange.Text;
			
		}

		protected void txtSearchVerticalToDate_TextChanged(object sender, EventArgs e)
		{
			drpDateRange.SelectedValue = drpDateRange.Text;
			
		}

    }
}