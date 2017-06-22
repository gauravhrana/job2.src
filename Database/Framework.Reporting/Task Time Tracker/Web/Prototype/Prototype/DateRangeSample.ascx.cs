using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;


namespace Shared.UI.Web.ApplicationManagement.Development
{
	public partial class DateRangeSample : Framework.UI.Web.BaseClasses.PageBasePage
	{

		#region Variables

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

		public DateTime? FromDate
		{
			get
			{
				if (!string.IsNullOrEmpty(txtSearchVerticalFromDate.Text.ToString()) && chkDate.Checked)
				{
					return DateTime.Parse(txtSearchVerticalFromDate.Text.Trim());					
				}
				return null;
			}
		}

		public string FromDateTime
		{
			get
			{
				var date = DateTime.ParseExact(txtSearchVerticalFromDate.Text, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
				return date.ToString();
				
			}
		}

		public string ToDateTime
		{
			get
			{
				var date = DateTime.ParseExact(txtSearchVerticalToDate.Text, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
				return date.ToString();
			}
		}

		public DateTime? ToDate
		{
			get
			{
				if (!string.IsNullOrEmpty(txtSearchVerticalFromDate.Text.ToString()) && chkDate.Checked)
				{
					return DateTime.Parse(txtSearchVerticalToDate.Text.Trim());			
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
			var date = DateTime.ParseExact(todate, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
			var formatteddate = date.ToString(SessionVariables.UserDateFormat);
			txtSearchVerticalToDate.Text = formatteddate;
			date = DateTime.ParseExact(fromdate, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
			formatteddate = date.ToString(SessionVariables.UserDateFormat);
			txtSearchVerticalFromDate.Text = formatteddate;
		}

		public DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, 1);
		}

		public DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
		{
			DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
			return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (this.Parent.ID != null)
				{
					if (!this.Parent.ID.Equals("datepanel"))
						SettingCategory = this.Parent.ID;
					else
					{
						var parent = (Panel)this.Parent;
						if (parent.ToolTip.Contains("Date"))
							SettingCategory = parent.ToolTip;
						else
							return;
					}
				}
				else
					SettingCategory = "General";
				LoadText();

                var dateRangeData = Framework.Components.UserPreference.DateRangeTitleDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dateRangeData, drpDateRange, StandardDataModel.StandardDataColumns.Name,
					DateRangeTitleDataModel.DataColumns.DateRangeTitleId);

				drpDateRange.SelectedItem.Value = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.DateRangeFormat, SettingCategory);

				var Dates = DateRangeHelper.FillUpDate(drpDateRange.SelectedItem.Text, SessionVariables.UserDateFormat);
				txtSearchVerticalFromDate.Text = Dates[0];
				txtSearchVerticalToDate.Text = Dates[1];
				lblUserDateFormat.Text = SessionVariables.UserDateFormat;

			}
		}

		protected void chkDate_CheckedChanged(object sender, EventArgs e)
		{
			txtSearchVerticalFromDate.Enabled = chkDate.Checked;
			txtSearchVerticalToDate.Enabled = chkDate.Checked;

		}

		protected void drpDateRange_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (this.Parent.ID != null)
			//{
			//    if (!this.Parent.ID.Equals("datepanel"))
			//        SettingCategory = this.Parent.ID;
			//    else
			//    {
			//        var parent = (Panel)this.Parent;
			//        SettingCategory = parent.ToolTip;
			//    }
			//}
			//else
			//    SettingCategory = "General";

			//DateRangeFormatId = Convert.ToInt32(daterange);
			//ApplicationCommon.UpdateUserPreference(SettingCategory, ApplicationCommon.DateRangeFormat, DateRangeFormatId.ToString());
			//DateRangeHelper.FillUpDate(drpDateRange, txtSearchVerticalFromDate, txtSearchVerticalToDate);

		}

		
		

		#endregion

	}
}