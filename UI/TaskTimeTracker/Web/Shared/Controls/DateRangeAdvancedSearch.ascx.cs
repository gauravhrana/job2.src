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

	public partial class DateRangeAdvancedSearch : BaseControl
	{
		#region Variables

		private string _key = "6";

		List<DateRangeTitleDataModel> dateRangeData = new List<DateRangeTitleDataModel>();

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

		public DropDownList DateRangeGroupDropDown
		{
			get
			{
				return drpDateRangeGroup;
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
			var dateRangeStyle = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DateRangeStyle).ToLower();

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
			var userDateRangeStyle = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.DateRangeStyle);

			if (userDateRangeStyle == "Vertical")
			{
				//VerticalStyle.Visible = true;
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
			drpDateRange.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.DateRangeFormat, SettingCategory);

			if (value != "Custom" && value != "-1")
			{
				// this will directly return date values as string in user's preferred format
				var dates = DateRangeHelper.FillUpDate(value, SessionVariables.UserDateFormat);

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

		public string GetDateRangeGroupControlClientId()
		{
			return drpDateRangeGroup.ClientID;
		}

		public string GetFromDateTextBoxClientId()
		{
			return txtSearchVerticalFromDate.ClientID;
		}

		public string GetToDateTextBoxClientId()
		{
			return txtSearchVerticalToDate.ClientID;
		}

		public void SaveDateValues(string daterangeGroupValue, string daterangevalue, string fromdate, string todate)
		{

			var dateRange = dateRangeData.AsEnumerable().Select(r => r).Where(r => (r.Name.Equals(daterangeGroupValue + ' ' + daterangevalue))).ToList();

			DateRangeFormatId = daterangeGroupValue != "-1" ? Convert.ToInt32(dateRange[0].DateRangeTitleId) : -1;

			//saves the dropdownlist selected value
			PreferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.DateRangeFormat, DateRangeFormatId.ToString());

			fromdate = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(fromdate);

			//saves the from date in the from textbox
			PreferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.FromDateRange, fromdate);

			todate = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(todate);

			//saves the To date in th To textbox
			PreferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.ToDateRange, todate);

		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			dateRangeData = DateRangeTitleDataManager.GetList(SessionVariables.RequestProfile);

			if (!IsPostBack)
			{

				LoadText();

				txtSearchVerticalFromDate.Enabled = chkDate.Checked;
				txtSearchVerticalToDate.Enabled = chkDate.Checked;

				dateRangeData = DateRangeTitleDataManager.GetList(SessionVariables.RequestProfile);
				List<string> dateGp = new List<string>();
				List<string> dateSubGp = new List<string>();

				SettingCategory = "DateFormatNewControl";


				var dateRangeFormatId = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.DateRangeFormat, SettingCategory);

				IEnumerable<DateRangeTitleDataModel> dtgrp = null;

				drpDateRangeGroup.Items.Insert(0, new ListItem() { Text = "All", Value = "-1" });

				dtgrp = dateRangeFormatId == "-1" || dateRangeFormatId == "0" ? dateRangeData.AsEnumerable() 
					: dateRangeData.AsEnumerable().Select(r => r).Where(r => (r.DateRangeTitleId == Convert.ToInt32(dateRangeFormatId)));				

				var drpText = dtgrp.Select(o => o.Name).ToList();

				foreach (var x in dateRangeData)
				{
					if (x.Name.IndexOf(' ') != -1)
					{
						var dateGroup = x.Name.Substring(0, x.Name.IndexOf(' '));
						dateGp.Add(dateGroup);
						if (dateGroup == drpText[0].ToString().Split(' ')[0].ToString())
						{
							var dateSubGroup = x.Name.Substring(x.Name.IndexOf(' ') + 1, (x.Name.Length - 1 - dateGroup.Length));

							dateSubGp.Add(dateSubGroup);

							if (dateRangeFormatId == "-1")
							{
								drpDateRangeGroup.SelectedIndex = -1;
							}
							else
							{
								drpDateRangeGroup.SelectedValue = drpText[0].ToString().Split(' ')[0].ToString();

								drpDateRange.SelectedValue = drpText[0].ToString().Split(' ')[1].ToString();
							}
						}
					}
				}

				drpDateRangeGroup.DataSource = dateGp.Distinct();
				drpDateRangeGroup.DataBind();

				drpDateRange.DataSource = dateSubGp;
				drpDateRange.DataBind();

				var dateGroupRange = drpDateRange.SelectedItem != null ? drpDateRangeGroup.SelectedItem.Text + ' ' + drpDateRange.SelectedItem.Text : drpDateRangeGroup.SelectedItem.Text;
				if (drpDateRangeGroup.SelectedIndex != 0)
				{
					var dates = DateRangeHelper.FillUpDate(dateGroupRange, SessionVariables.UserDateFormat);
					if (!string.IsNullOrEmpty(dates[0]) || !string.IsNullOrEmpty(dates[1]))
					{

						txtSearchVerticalFromDate.Text = dates[0];
						txtSearchVerticalToDate.Text = dates[1];
					}
				}
				else
				{
					var upDateFromRange = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.FromDateRange, SettingCategory);
					var upDateToRange = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ToDateRange, SettingCategory);
					if (!string.IsNullOrEmpty(upDateFromRange.Trim()) && !string.IsNullOrEmpty(upDateToRange.Trim()))
					{
						txtSearchVerticalFromDate.Text = DateTimeHelper.FromApplicationDateFormatToUserDateFormat(upDateFromRange);
						txtSearchVerticalToDate.Text = DateTimeHelper.FromApplicationDateFormatToUserDateFormat(upDateToRange);
					}
				}


				lblUserDateFormat.Text = "(" + SessionVariables.UserDateFormat + ")";
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
			var DateRangeFormatId = dateRangeData.AsEnumerable().Select(r => r).Where(r => (r.Name.StartsWith(drpDateRange.SelectedItem.Text +' '+ drpDateRangeGroup.SelectedItem.Text))).ToList();

			//var DateRangeFormatId = dateRangeData.Select(o=>o.Name).Where(o=>o.Name==drpDateRange.SelectedItem.Text);
			//DateRangeFormatId = Convert.ToInt32(drpDateRange.SelectedValue);
			//PreferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.DateRangeFormat, DateRangeFormatId.ToString());
			var Dates = DateRangeHelper.FillUpDate(drpDateRange.SelectedItem.Text, SessionVariables.UserDateFormat);
			txtSearchVerticalFromDate.Text = Dates[0];
			txtSearchVerticalToDate.Text = Dates[1];
		}

		protected void drpDateRangeGrouping_SelectedIndexChanged(object sender, EventArgs e)
		{
			var DateRangeFormatId = dateRangeData.AsEnumerable().Select(r => r).Where(r => (r.Name.StartsWith(drpDateRange.SelectedItem.Text + ' ' + drpDateRangeGroup.SelectedItem.Text))).ToList();

			//DateRangeFormatId = Convert.ToInt32(drpDateRange.SelectedValue);
			//PreferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.DateRangeFormat, DateRangeFormatId.ToString());
			var Dates = DateRangeHelper.FillUpDate(drpDateRange.SelectedItem.Text, SessionVariables.UserDateFormat);
			txtSearchVerticalFromDate.Text = Dates[0];
			txtSearchVerticalToDate.Text = Dates[1];
		}

		protected void txtSearchVerticalFromDate_TextChanged(object sender, EventArgs e)
		{
			drpDateRange.SelectedValue = drpDateRange.Text;

		}

		protected void txtSearchVerticalToDate_TextChanged(object sender, EventArgs e)
		{
			drpDateRange.SelectedValue = drpDateRange.Text;

		}

		#endregion
	}
}