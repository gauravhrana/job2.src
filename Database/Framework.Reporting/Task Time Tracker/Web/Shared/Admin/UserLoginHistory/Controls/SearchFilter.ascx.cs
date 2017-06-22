using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Globalization;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Admin.UserLoginHistory.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{

		#region variables

		public Framework.Components.LogAndTrace.UserLoginHistoryDataModel SearchParameters
		{
			get
			{
				var data = new Framework.Components.LogAndTrace.UserLoginHistoryDataModel();

				if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(Framework.Components.LogAndTrace.UserLoginHistoryDataModel.DataColumns.UserName + "Visibility", SettingCategory))
				{
					data.UserName = CheckAndGetFieldValue(Framework.Components.LogAndTrace.UserLoginHistoryDataModel.DataColumns.UserName).ToString();
				}

				if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean("GroupByVisibility", SettingCategory))
				{
					if (CheckAndGetFieldValue("GroupBy").ToString() != "-1")
					{
						GroupBy = CheckAndGetFieldValue("GroupBy").ToString();
					}
				}

				if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(Framework.Components.LogAndTrace.UserLoginHistoryDataModel.DataColumns.DateRangeValue + "Visibility", SettingCategory))
				{
					var date = CheckAndGetFieldValue(Framework.Components.LogAndTrace.UserLoginHistoryDataModel.DataColumns.DateRangeValue).ToString();
					if (!string.IsNullOrEmpty(date))
					{
						var dates = date.Split('&');
						if (Boolean.Parse(dates[2]) && !string.IsNullOrEmpty(dates[0]) && !string.IsNullOrEmpty(dates[1]))
						{
							var strDate1 = DateTime.Parse(dates[0]);
							var strDate2 = DateTime.Parse(dates[1]);
							var date1 = DateTime.ParseExact(strDate1.ToString(SessionVariables.UserDateFormat), SessionVariables.UserDateFormat, CultureInfo.InvariantCulture);
							var date2 = DateTime.ParseExact(strDate2.ToString(SessionVariables.UserDateFormat), SessionVariables.UserDateFormat, CultureInfo.InvariantCulture);
							data.FromSearchDate = date1.Date;
							data.ToSearchDate = date2.Date;

						}
					}
				}
				
				return data;
			}
		}

		#endregion

		#region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
            
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "UserLoginHistory";
			FolderLocationFromRoot = "Shared/Admin/UserLoginHistory";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginHistory;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		protected void SearchParametersRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var plcControlHolder = (PlaceHolder)e.Item.FindControl("plcControlHolder");
				var oDateRange = (DateRangeControl)e.Item.FindControl("oDateRange");
				if (oDateRange != null)
				{
					oDateRange.Key = e.Item.ItemIndex.ToString();
					var funccall = "Fillup" + oDateRange.GetKey() + "();";
					oDateRange.DateRangeDropDown.Attributes.Add("onchange", funccall);
					funccall = "chkdate_checkedchanged" + oDateRange.GetKey() + "();";
					oDateRange.DateRangeCheckBox.Attributes.Add("onclick", funccall);
					oDateRange.HideLabel();
				}
			}
		}

		#endregion     
	
	}
}