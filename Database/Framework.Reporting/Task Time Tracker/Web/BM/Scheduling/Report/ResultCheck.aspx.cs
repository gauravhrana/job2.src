using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationContainer.UI.Web.BaseUI;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.Framework.ReleaseLog;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.ApplicationUser;
using Framework.Components.Core;
using Framework.Components.ReleaseLog;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.Report
{
	public partial class ResultCheck : BasePage
	{

		public class ResultSet
		{
			string applicationUserName;
			string totalRNHrs;
			string totalScheduleHrs;

			public ResultSet(string name, string rnHrs, string scheduleHrs)
			{
				ApplicationUserName = name;
				TotalRNHrs = rnHrs;
				TotalScheduleHrs = scheduleHrs;
			}

			public string ApplicationUserName
			{
				get
				{
					return applicationUserName;
				}
				set
				{
					applicationUserName = value;
				}
			}

			public string TotalRNHrs
			{
				get
				{
					return totalRNHrs;
				}
				set
				{
					totalRNHrs = value;
				}
			}

			public string TotalScheduleHrs
			{
				get
				{
					return totalScheduleHrs;
				}
				set
				{
					totalScheduleHrs = value;
				}
			}

		}


		DateRangeControl oDateRange = new DateRangeControl();

		protected void Page_Load(object sender, EventArgs e)
		{
			SetUpDateRangeControl();
			if (!IsPostBack)
			{
				var auData = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);

				UIHelper.LoadDropDown(auData, drpApplicationUser, ApplicationUserDataModel.DataColumns.EmailAddress,
					ApplicationUserDataModel.DataColumns.EmailAddress);
				drpApplicationUser.Items.Insert(0, "All");

				var auData1 = ApplicationDataManager.GetList(SessionVariables.RequestProfile);

				UIHelper.LoadDropDown(auData1, drpApplicationId, ApplicationDataModel.DataColumns.Name,
					ApplicationDataModel.DataColumns.ApplicationId);
				drpApplicationId.Items.Insert(0, "All");

			}
		}

		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{	
			var appId = 0;
			var rs = new ResultSet("", "", "");
			var rnHrs = "0";
			if (drpApplicationUser.SelectedItem.Value.Equals("All"))
			{
				if (drpApplicationId.SelectedItem.Value.Equals("All"))
				{
					CalculateRNandSDWorkedHoursForAllUsersAllApps();
				}
				else
				{
					appId = int.Parse(drpApplicationId.SelectedValue);
					CalculateRNandSDWorkedHoursForAllUsers(appId);
				}
			}
			else
			{
				var applicationNames = new List<string>();
				if (drpApplicationId.SelectedItem.Value.Equals("All"))
				{
					var dt = ApplicationDataManager.GetList(SessionVariables.RequestProfile);
					var cell = 3;					
					var emailAddress = drpApplicationUser.SelectedValue;
					for (var i = 0; i < dt.Rows.Count; i++)
					{
						if (!dt.Rows[i][ApplicationDataModel.DataColumns.Code].ToString().Equals(""))
						{
							appId = int.Parse(dt.Rows[i][ApplicationDataModel.DataColumns.ApplicationId].ToString());
							rs = CalculateRNandSDWorkedHours(emailAddress, appId);
							rnHrs = (int.Parse(rs.TotalRNHrs) + int.Parse(rnHrs)).ToString();

							applicationNames.Add(dt.Rows[i][ApplicationDataModel.DataColumns.Name].ToString());
							
						}
					}
					var count = applicationNames.Count;
					var count1 = 3;
					var limit = count + count1;
					for (var i =3; i <limit; i++)
					{
						if (gv.Columns.Count <limit)
							{
								gv.Columns.Add(new BoundField());
								var applicationName = dt.Rows[i][ApplicationDataModel.DataColumns.Name];
								gv.Columns[cell].HeaderText = applicationName.ToString();
								cell++;
							}
							
					}
					
					rs.TotalRNHrs = string.Format("{0}.00", rnHrs);
					gv.DataSource = new List<ResultSet>() { rs };
					gv.DataBind();
					cell = 3;
					for (var i = 0; i < dt.Rows.Count; i++)
					{
						if (!dt.Rows[i][ApplicationDataModel.DataColumns.Code].ToString().Equals(""))
						{
							appId = int.Parse(dt.Rows[i][ApplicationDataModel.DataColumns.ApplicationId].ToString());
							rs = CalculateRNandSDWorkedHours(emailAddress, appId);
							gv.Rows[0].Cells[cell].HorizontalAlign = HorizontalAlign.Right;
							gv.Rows[0].Cells[cell].Text = string.Format("{0}.00", rs.TotalRNHrs);
							cell++;
						}
					}
					
				}
				else
				{
					gv.DataSource = "";
					gv.DataBind();
					appId = int.Parse(drpApplicationId.SelectedValue);
					var emailAddress = drpApplicationUser.SelectedValue;
					rs = CalculateRNandSDWorkedHours(emailAddress, appId);
					rnHrs = (int.Parse(rs.TotalRNHrs) + int.Parse(rnHrs)).ToString();
					rs.TotalRNHrs = string.Format("{0}.00", rnHrs);
					var listRs = new List<ResultSet> { rs };
					gv.DataSource = listRs;
					gv.DataBind();
				}
			}
		}






		protected void btnReset_OnClick(object sender, EventArgs e)
		{
			Response.Redirect("~/Admin/ResultCheck");
		}



		private void SetUpDateRangeControl()
		{
			//DateRangeControl oDateRange = new DateRangeControl();

			if (plcControlHolder != null)
			{
				oDateRange = (DateRangeControl)Page.LoadControl(ApplicationCommon.DateRangeControlPath);
				oDateRange.ID = "oDateRange";

				var dtPanel = new Panel();

				dtPanel.ID = "datepanel";
				//dtPanel.CssClass = "datepanel col-sm-10";

				dtPanel.Controls.Add(oDateRange);

				plcControlHolder.Controls.Add(dtPanel);


				//datepanel = dtPanel;
			}


			var funccall = "Fillup" + oDateRange.GetKey() + "();";
			oDateRange.DateRangeDropDown.Attributes.Add("onchange", funccall);
			oDateRange.HideLabel();


		}

		public ResultSet CalculateRNandSDWorkedHours(string emailAddress, int appId)
		{

			var rnTotalHours = 0.0;
			var srnTotalHours = 0.0;
			var sdTotalHours = 0;
			var temp = 0.0;
			var developer = string.Empty;
			var appUserId = 0;
			var appUserName = string.Empty;
			var fromDateTime = new DateTime();
			var toDateTime = new DateTime();
			var date = new ReleaseLogDetailDataModel();

			var dateValue = ReleaseLogDetailDataManager.GetDetails(date, SessionVariables.RequestProfile);
			var dv = dateValue.DefaultView;
			dv.Sort = "ReleaseDate ASC";

			if (oDateRange.FromDateTime.Equals("") || oDateRange.ToDateTime.Equals(""))
			{
				fromDateTime = Convert.ToDateTime(dv.ToTable().Rows[0][ReleaseLogDetailDataModel.DataColumns.ReleaseDate].ToString());
				toDateTime = Convert.ToDateTime(dv.ToTable().Rows[dateValue.Rows.Count - 1][ReleaseLogDetailDataModel.DataColumns.ReleaseDate].ToString());
			}
			else
			{
				fromDateTime = Convert.ToDateTime(DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.FromDateTime));
				toDateTime = Convert.ToDateTime(DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.ToDateTime));
			}
			var format = SessionVariables.UserDateFormat;
			var fromDate = fromDateTime.ToString(format);
			var toDate = toDateTime.ToString(format);


			//var appObj = new ApplicationDataModel();
			//appObj.ApplicationId = appId;
			//var dt3 = Framework.Components.ApplicationUser.ApplicationDataManager.Search(appObj, SessionVariables.RequestProfile);
			//var applicationName = dt3.Rows[0][ApplicationDataModel.DataColumns.Name];

			var obj1 = new ApplicationUserDataModel();
			obj1.EmailAddress = emailAddress;
			SessionVariables.RequestProfile.ApplicationId = 100;
			var dt1 = ApplicationUserDataManager.Search(obj1, SessionVariables.RequestProfile);

			//appUserId = (int)(dt1.Rows[0][ApplicationUserDataModel.DataColumns.ApplicationUserId]);
			appUserName = dt1.Rows[0][ApplicationUserDataModel.DataColumns.FirstName] + " " + dt1.Rows[0][ApplicationUserDataModel.DataColumns.LastName];

			if (dt1.Rows[0][ApplicationUserDataModel.DataColumns.LastName].ToString().Substring(1, 1).Equals("u"))
			{
				developer = (string)(dt1.Rows[0][ApplicationUserDataModel.DataColumns.FirstName].ToString().Substring(0, 1) + dt1.Rows[0][ApplicationUserDataModel.DataColumns.LastName].ToString().Substring(0, 1) + "2");
			}
			else
			{
				developer = (string)(dt1.Rows[0][ApplicationUserDataModel.DataColumns.FirstName].ToString().Substring(0, 1) + dt1.Rows[0][ApplicationUserDataModel.DataColumns.LastName].ToString().Substring(0, 1));
			}


			var tsObj = new ReleaseLogDetailDataModel();
			tsObj.ReleaseDateMin = Convert.ToDateTime(fromDate);
			tsObj.ReleaseDateMax = Convert.ToDateTime(toDate);
			tsObj.ApplicationId = appId;
			tsObj.PrimaryDeveloper = developer;
			var dt4 = ReleaseLogDetailDataManager.Search(tsObj, SessionVariables.RequestProfile);
			var temp1 = 0.0;

			for (var i = 0; i < dt4.Rows.Count; i++)
			{
				if (dt4.Rows[i][ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper].Equals(developer) || dt4.Rows[i][ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper].Equals("Admin"))
				{
					if (!dt4.Rows[i][ReleaseLogDetailDataModel.DataColumns.TimeSpent].Equals("Unknown"))
					{
						temp1 = (double)Convert.ToDouble(dt4.Rows[i][ReleaseLogDetailDataModel.DataColumns.TimeSpent]);
						srnTotalHours = srnTotalHours + temp1;
					}
				}
			}



			var sObj = new ApplicationUserDataModel();
			sObj.EmailAddress = emailAddress;
			sObj.ApplicationId = 100047;
			var sDt = ApplicationUserDataManager.Search(sObj, SessionVariables.RequestProfile);

			if (sDt.Rows.Count > 0)
			{
				var obj2 = new ScheduleDataModel();
				obj2.FromSearchDate = Convert.ToDateTime(fromDate);
				obj2.ToSearchDate = Convert.ToDateTime(toDate);
				obj2.Person = (sDt.Rows[0][ApplicationUserDataModel.DataColumns.ApplicationUserId]).ToString();
				obj2.ApplicationId = 100047;
				//SessionVariables.RequestProfile.ApplicationId = 100047;
				var dt2 = ScheduleDataManager.Search(obj2, SessionVariables.RequestProfile);


				for (var i = 0; i < dt2.Rows.Count; i++)
				{
					temp = Convert.ToInt32(dt2.Rows[i][ScheduleDataModel.DataColumns.TotalHoursWorked]);
					sdTotalHours = sdTotalHours + (int)temp;
				}

			}


			var resultSet = new ResultSet(appUserName, ((int)(rnTotalHours + srnTotalHours)).ToString(), string.Format("{0}.00", sdTotalHours));

			return resultSet;
		}

		public void CalculateRNandSDWorkedHoursForAllUsersAllApps()
		{
			gv.DataSource = "";
			gv.DataBind();
			var resultArray = new ArrayList();
			var appUserName = string.Empty;
			var emailAddress = string.Empty;
			var result = new ResultSet(string.Empty, "", "");
			var obj = new ApplicationUserDataModel();
			SessionVariables.RequestProfile.ApplicationId = 100047;
			var applicationUserList = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			var applicationList = ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			var bc = new BoundField();

			for (var i = 0; i < applicationUserList.Rows.Count; i++)
			{
				var rnHrs = "0";
				emailAddress = (string)(applicationUserList.Rows[i][ApplicationUserDataModel.DataColumns.EmailAddress]);
				for (var j = 0; j < applicationList.Rows.Count; j++)
				{
					var applicationName = applicationList.Rows[j][ApplicationDataModel.DataColumns.Name];
					result = CalculateRNandSDWorkedHours(emailAddress, (int)applicationList.Rows[j][ApplicationDataModel.DataColumns.ApplicationId]);
					rnHrs = (int.Parse(result.TotalRNHrs) + int.Parse(rnHrs)).ToString();
				}
				result.TotalRNHrs = string.Format("{0}.00", rnHrs);
				resultArray.Add(result);
			}

			var cell = 3;
			for (var i = 0; i < applicationList.Rows.Count; i++)
			{
				if (!applicationList.Rows[i][ApplicationDataModel.DataColumns.Code].ToString().Equals(""))
				{
					gv.Columns.Add(new BoundField());
					var applicationName = applicationList.Rows[i][ApplicationDataModel.DataColumns.Name];
					gv.Columns[cell].HeaderText = applicationName.ToString();
					cell++;
				}
			}
			gv.DataSource = resultArray;
			gv.DataBind();
			for (var i = 0; i < applicationUserList.Rows.Count; i++)
			{
				cell = 3;
				emailAddress = (string)(applicationUserList.Rows[i][ApplicationUserDataModel.DataColumns.EmailAddress]);
				for (var j = 0; j < applicationList.Rows.Count; j++)
				{
					if (!applicationList.Rows[j][ApplicationDataModel.DataColumns.Code].ToString().Equals(""))
					{
						var applicationName = applicationList.Rows[j][ApplicationDataModel.DataColumns.Name];
						result = CalculateRNandSDWorkedHours(emailAddress, (int)applicationList.Rows[j][ApplicationDataModel.DataColumns.ApplicationId]);
						gv.Rows[i].Cells[cell].HorizontalAlign = HorizontalAlign.Right;
						gv.Rows[i].Cells[cell].Text = string.Format("{0}.00", result.TotalRNHrs);
						cell++;
					}
				}
			}


			int rowscount = gv.Rows.Count;
			int columnscount = gv.Columns.Count;
			for (int j = 3; j < columnscount; j++)
			{
				var countRemove = 0;

				for (int i = 1; i < rowscount; i++)
				{
					if (Convert.ToDouble(gv.Rows[i].Cells[j].Text) != 0.00)
					{
						countRemove++;
					}

				}
				if (countRemove == 0)
				{
					gv.Columns[j].Visible = false;
				}
			}
					
		}

		public void CalculateRNandSDWorkedHoursForAllUsers(int appId)
		{
			var resultArray = new ArrayList();
			var appUserName = string.Empty;
			var emailAddress = string.Empty;
			var obj = new ApplicationUserDataModel();
			var dt = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			var dt1 = ApplicationDataManager.GetList(SessionVariables.RequestProfile);


			for (var i = 0; i < dt.Rows.Count; i++)
			{
				emailAddress = (string)(dt.Rows[i][ApplicationUserDataModel.DataColumns.EmailAddress]);
				//appUserId = (int)(dt.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserId]);
				//appId = (int)(dt1.Rows[i][ApplicationDataModel.DataColumns.ApplicationId]);
				appUserName = dt.Rows[i][ApplicationUserDataModel.DataColumns.FirstName] + " " + dt.Rows[i][ApplicationUserDataModel.DataColumns.LastName];
				var result = CalculateRNandSDWorkedHours(emailAddress, appId);
				result.TotalRNHrs = string.Format("{0}.00", result.TotalRNHrs);
				resultArray.Add(result);
			}

			gv.DataSource = resultArray;
			gv.DataBind();

		}

		protected void drpApplicationId_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationId.Text = drpApplicationId.SelectedValue;
		}

		protected void drpApplicationUser_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationUserId.Text = drpApplicationUser.SelectedValue;
		}

		protected void drpExcludedItems_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "RNvsScheduleCheckDefaultView";
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);


			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup(string.Empty);
			bcControl.GenerateMenu();
		}

	}
}