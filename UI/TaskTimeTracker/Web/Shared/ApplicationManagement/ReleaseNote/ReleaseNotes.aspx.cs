using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.ReleaseLog;
using Framework.Components;
using Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System.Collections.Specialized;
using System.Collections;
using Shared.UI.Web.Controls;
using Framework.Components.ReleaseLog;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLog
{
	public partial class ReleaseNotes : BasePage
	{

		#region variable

		int AlternateCount = 0;

		DataTable AllDataRows;

		//int		ReleaseLogId;
		int TotalParentCount;
		//string	ReleaseLog;

		int ItemCount;

		List<decimal> lstMedians = new List<decimal>();
		List<decimal> lstTotal = new List<decimal>();
		List<decimal> lstCount = new List<decimal>();
		List<decimal> lstAverage = new List<decimal>();
		List<decimal> lstMax = new List<decimal>();
		List<decimal> lstMin = new List<decimal>();

		protected ControlVisibilityManager VisibilityManagerCore { get; set; }

		private int DetailUserPreferenceCategoryId
		{
			get
			{
				return Convert.ToInt32(ViewState["DetailUserPreferenceCategoryId"]);
			}
			set
			{
				ViewState["DetailUserPreferenceCategoryId"] = value;
			}
		}

		decimal TotalTimeSpentCount
		{
			get
			{
				return Convert.ToDecimal(ViewState["TotalTimeSpent"]);
			}
			set
			{
				ViewState["TotalTimeSpent"] = value;
			}
		}

		#endregion

		#region HTML Methods

		public HtmlGenericControl GetStatisticInfoHTMLHeaderRow(string headerName)
		{

			var row = new HtmlGenericControl("div");
            row.Attributes["class"] = "row gutter-border text-center";

			var cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-2";
			cell.InnerText = headerName;
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Total";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-2";
			cell.InnerText = "Total(%)";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Average";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Median";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Count";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-2";
			cell.InnerText = "Count(%)";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Max";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Min";
			row.Controls.Add(cell);

			return row;
		}

		public HtmlGenericControl GetStatisticInfoHTMLSummaryRow(DataTable releaseLogDetails, string groupName)
		{
			var lstValues = ReleaseLogDetailDataManager.GetStatisticDataSummary(releaseLogDetails, ReleaseLogDetailDataModel.DataColumns.ReleaseNotesTimeSpentConstant, ApplicationCommon.ReleaseNotesStatisticUnknown);
			var average = Math.Round(lstValues["Average"], 2);
			var median = lstValues["Median"];
			
			var recCount = releaseLogDetails.Rows.Count;
			decimal total = 0;

			decimal maxValue = 0;
			decimal minValue = 0;
			
			// if no records, no need to count total, search for max, min
			if (recCount > 0)
			{				
				var series = new decimal[recCount];
				var i = 0;

				foreach(var item in releaseLogDetails.AsEnumerable())
				{
					var timeSpent = item[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString();
				
					var timeSpentValue = 0m;

					if (!Decimal.TryParse(timeSpent, out timeSpentValue))
					{
						timeSpentValue = ReleaseLogDetailDataModel.DataColumns.ReleaseNotesTimeSpentConstant;
					}

					series[i++] = timeSpentValue;
				}

				total = series.Sum(x => x);
			
				maxValue = series.Select(x => x).Distinct().Max();
				minValue = series.Select(x => x).Distinct().Min();
			}

			HtmlGenericControl subDiv1 = new HtmlGenericControl("div");
            subDiv1.Attributes["class"] = "row gutter-border text-right";

			HtmlGenericControl divId1 = new HtmlGenericControl("div");
			divId1.Attributes["class"] = "col-sm-2";

			Label lblReleaseLogText1 = new Label();
			lblReleaseLogText1.Text = "Summary";
			divId1.Controls.Add(lblReleaseLogText1);
			subDiv1.Controls.Add(divId1);

			HtmlGenericControl divTotal1 = new HtmlGenericControl("div");
			divTotal1.Attributes["class"] = "col-sm-1";

			Label lblTotalText1 = new Label();
			lblTotalText1.Text = total.ToString("#0,0.00");
			divTotal1.Controls.Add(lblTotalText1);
			subDiv1.Controls.Add(divTotal1);

			var divTotalPer = new HtmlGenericControl("div");
			divTotalPer.Attributes["class"] = "col-sm-2";

			var lblTotalPerText = new Label();
			lblTotalPerText.Text = "100%";
			divTotalPer.Controls.Add(lblTotalPerText);
			subDiv1.Controls.Add(divTotalPer);

			HtmlGenericControl divAverage1 = new HtmlGenericControl("div");
			divAverage1.Attributes["class"] = "col-sm-1";

			Label lblAverageText1 = new Label();
			lblAverageText1.Text = average.ToString("#0,0.00");
			divAverage1.Controls.Add(lblAverageText1);
			subDiv1.Controls.Add(divAverage1);

			HtmlGenericControl divMedianSummary = new HtmlGenericControl("div");
			divMedianSummary.Attributes["class"] = "col-sm-1";

			Label lblMedianText1 = new Label();
			lblMedianText1.Text = median.ToString("#0,0.00");
			divMedianSummary.Controls.Add(lblMedianText1);
			subDiv1.Controls.Add(divMedianSummary);

			HtmlGenericControl divCountSummary = new HtmlGenericControl("div");
			divCountSummary.Attributes["class"] = "col-sm-1";

			Label lblCountText1 = new Label();
			lblCountText1.Text = recCount.ToString("#0,0.00");
			divCountSummary.Controls.Add(lblCountText1);
			subDiv1.Controls.Add(divCountSummary);

			var divCntPer = new HtmlGenericControl("div");
			divCntPer.Attributes["class"] = "col-sm-2";

			var lblCntPerText = new Label();
			lblCntPerText.Text = "100%";
			divCntPer.Controls.Add(lblCntPerText);
			subDiv1.Controls.Add(divCntPer);

			HtmlGenericControl divMaxSummary = new HtmlGenericControl("div");
			divMaxSummary.Attributes["class"] = "col-sm-1";

			Label lblMaxText1 = new Label();
			lblMaxText1.Text = maxValue.ToString("#0,0.00");
			divMaxSummary.Controls.Add(lblMaxText1);
			subDiv1.Controls.Add(divMaxSummary);

			HtmlGenericControl divMinSummary = new HtmlGenericControl("div");
			divMinSummary.Attributes["class"] = "col-sm-1";

			Label lblMinText1 = new Label();
			lblMinText1.Text = minValue.ToString("#0,0.00");
			divMinSummary.Controls.Add(lblMinText1);
			subDiv1.Controls.Add(divMinSummary);

			return subDiv1;

		}

		public HtmlGenericControl GetStatisticInfoHTMLRow(Statistic item, string key, decimal totalHoursCount = 0, decimal totalRowCount = 0)
		{
			var style = StyleStat();

			//var mainDiv = new HtmlGenericControl("table");
			//mainDiv.Attributes["class"] = "table table-bordered";

			var subDiv = new HtmlGenericControl("div");
            subDiv.Attributes["class"] = "row gutter-border text-right";

			var divId = new HtmlGenericControl("div");
			divId.Attributes["class"] = "col-sm-2";

			var lblReleaseLogText = new Label();
			lblReleaseLogText.Text = key;
			divId.Controls.Add(lblReleaseLogText);
			subDiv.Controls.Add(divId);

			var divTotal = new HtmlGenericControl("div");
			divTotal.Attributes["class"] = "col-sm-1";
			var lblTotalText = new Label();
			lblTotalText.Text = ((decimal)item.Total).ToString("#0,0.00");

			divTotal.Controls.Add(lblTotalText);
			subDiv.Controls.Add(divTotal);

			var divTotalPercentage = new HtmlGenericControl("div");
			divTotalPercentage.Attributes["class"] = "col-sm-2";

			if (totalHoursCount != 0)
			{
				item.TotalPercentage = (item.Total / totalHoursCount);

				var lblTotalPercentageText = new Label();				
				lblTotalPercentageText.Text += String.Format("{0:P2}", item.TotalPercentage);

				divTotalPercentage.Controls.Add(lblTotalPercentageText);
			}
			subDiv.Controls.Add(divTotalPercentage);

			var divAverage = new HtmlGenericControl("div");
			divAverage.Attributes["class"] = "col-sm-1";

			var lblAverageText = new Label();
			lblAverageText.Text = Math.Round(Convert.ToDecimal(item.Average), 2).ToString("#0,0.00");
			divAverage.Controls.Add(lblAverageText);
			subDiv.Controls.Add(divAverage);

			var divMedian = new HtmlGenericControl("div");
			divMedian.Attributes["class"] = "col-sm-1";

			var lblMedianText = new Label();
			if (item.Median != null)
				lblMedianText.Text = ((decimal)item.Median).ToString("#0,0.00");
			divMedian.Controls.Add(lblMedianText);
			subDiv.Controls.Add(divMedian);

			var divCount = new HtmlGenericControl("div");
			divCount.Attributes["class"] = "col-sm-1";
			var lblCountText = new Label();
			lblCountText.Text = ((decimal)item.Count).ToString("#0,0.00");
			divCount.Controls.Add(lblCountText);
			subDiv.Controls.Add(divCount);

			var divCountPercentage = new HtmlGenericControl("div");
			divCountPercentage.Attributes["class"] = "col-sm-2";

			if (totalRowCount != 0)
			{
				item.CountPercentage = (item.Count / totalRowCount);

				var lblCountPercentageText = new Label();				
				lblCountPercentageText.Text += String.Format("{0:P2}", item.CountPercentage); 

				divCountPercentage.Controls.Add(lblCountPercentageText);
			}
			subDiv.Controls.Add(divCountPercentage);

			var divMax = new HtmlGenericControl("div");
			divMax.Attributes["class"] = "col-sm-1";

			var lblMaxText = new Label();
			if (item.Max != null)
				lblMaxText.Text = ((decimal)item.Max).ToString("#0,0.00");
			divMax.Controls.Add(lblMaxText);
			subDiv.Controls.Add(divMax);

			var divMin = new HtmlGenericControl("div");
			divMin.Attributes["class"] = "col-sm-1";

			var lblMinText = new Label();
			if (item.Min != null)
				lblMinText.Text = ((decimal)item.Min).ToString("#0,0.00");
			divMin.Controls.Add(lblMinText);
			subDiv.Controls.Add(divMin);

			//mainDiv.Controls.Add(subDiv);

			//TableReportContent.Controls.Add(mainDiv);
			return subDiv;

		}

		public string StyleStat()
		{
			AlternateCount++;

			string style = string.Empty;

			if (AlternateCount % 2 == 0)

				style = "rowRN";
			else
				style = "rowAlternate";

			return style;
		}

		#endregion

		#region Data Methods

		private DataTable GetData()
		{
			// find 
			var ds = ReleaseLogDetailDataManager.ReleaseNotesSearch(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			
			AllDataRows = ds.Tables[1];
			DataTable dt = null;
			
			TotalTimeSpentCount = GetTotalTimeSpentCount();

			if (oSearchFilter.GroupBy == "" || oSearchFilter.GroupBy == "All" || oSearchFilter.GroupBy == "-1")
			{
				if (ds.Tables.Count > 1)
				{
					var tblKeyDescription = new DataTable();

					tblKeyDescription.AcceptChanges();
					tblKeyDescription.Columns.Add("Name");
					tblKeyDescription.Columns.Add("ReleaseLogId");

					var row = tblKeyDescription.NewRow();
					row["Name"] = "All";
					row["ReleaseLogId"] = "All";
					tblKeyDescription.Rows.Add(row);

					var dataView = tblKeyDescription.DefaultView;

					dt = dataView.ToTable();
					GrdParentGrid.DataSource = dt;
					GrdParentGrid.DataBind();

					TotalParentCount = dt.Rows.Count;
				}
			}
			else if (ds.Tables.Count > 1
				&& !String.IsNullOrEmpty(oSearchFilter.GroupBy)
				&& oSearchFilter.GroupBy != "-1"
				&& oSearchFilter.GroupBy != "All")
			{

				// adding statistic header row only if only Grouping is applied.
				if (String.IsNullOrEmpty(oSearchFilter.SubGroupBy) || oSearchFilter.SubGroupBy == "-1" || oSearchFilter.SubGroupBy == "All")
				{
					TableReportContent.Controls.Add(GetStatisticInfoHTMLHeaderRow(oSearchFilter.GroupBy));
				}

				var AllDataRows_RL = ds.Tables[0];
				var tblKeyDescription = new DataTable();
				tblKeyDescription.Columns.Add("Grouping");
				tblKeyDescription.Columns.Add("Key");

				// temp
				tblKeyDescription.Columns.Add("Name");
				tblKeyDescription.Columns.Add("ReleaseLogId");

				tblKeyDescription.AcceptChanges();

				AllDataRows_RL.Merge(AllDataRows);

				var distinctFieldValues = (from row in AllDataRows_RL.AsEnumerable()
										  .Where(row => row[oSearchFilter.GroupBy].ToString().Trim() != "")
										   orderby row[oSearchFilter.GroupBy].ToString().Trim() descending
										   select row[oSearchFilter.GroupBy].ToString().Trim()).Distinct(StringComparer.CurrentCultureIgnoreCase);

				foreach (var key in distinctFieldValues)
				{
					var row = tblKeyDescription.NewRow();
					row["Grouping"] = oSearchFilter.GroupBy;
					row["Key"] = key;

					// The Insert Link in the Repeater gets bound to the ReleaseLog Name instead of the ReleaseLogId 
					// this create issues while inserting new ReleaseNotes Data. releaseLogPKId is used to get the ReleaseLog Id and binds the value to Insert Link

					EnumerableRowCollection<object> releaseLogPKId = null;

					//Testing on the implementation of JIRA 3459. Code retrieves records within the given updated range.
					if (AllDataRows_RL.Columns.Contains("IsUpdated"))
					{
						releaseLogPKId = AllDataRows_RL.AsEnumerable()
											.Where(x => x[ReleaseLogDetailDataModel.DataColumns.ReleaseLog].ToString().ToLower() == key &&
											 x[ReleaseLogDetailDataModel.DataColumns.IsUpdated].ToString() == "1")
											.Select(x => x[ReleaseLogDetailDataModel.DataColumns.ReleaseLogId]);
					}
					else
					{
						releaseLogPKId = AllDataRows_RL.AsEnumerable()
											 .Where(x => x[ReleaseLogDetailDataModel.DataColumns.ReleaseLog].ToString().ToLower() == key)
											 .Select(x => x[ReleaseLogDetailDataModel.DataColumns.ReleaseLogId]);
					}

					if (oSearchFilter.GroupBy == ReleaseLogDetailDataModel.DataColumns.ReleaseLog)
					{
                        if (releaseLogPKId.Any())
						{
							var rowItem = from rowPK in releaseLogPKId.AsEnumerable() select rowPK;
							row["ReleaseLogId"] = rowItem.First();
							row["Name"] = key;
							tblKeyDescription.Rows.Add(row);
						}
					}
					else
					{
						row["Name"] = key;
						tblKeyDescription.Rows.Add(row);
					}
				}

				tblKeyDescription.AcceptChanges();

				// Parent
				var dataView = tblKeyDescription.DefaultView;// ds.Tables[0].DefaultView;
				//dataView.Sort = ReleaseLogDetailDataModel.DataColumns.ReleaseLogId + " DESC";

				//// not clear on what this is about
				// Testing on the implementation of JIRA 3459.
				//var exists = dataView.ToTable().Columns.Contains("IsUpdated");
				//if (exists)
				//{
				//	dataView.RowFilter = "IsUpdated = 1";
				//}

				dt = dataView.ToTable();
				TotalParentCount = dt.Rows.Count;

				GrdParentGrid.DataSource = dt;
				GrdParentGrid.DataBind();

			}



			if ((ds.Tables[1].Rows.Count == 0 && ds.Tables[0].Rows.Count < 1) || (ds.Tables[1].Rows.Count == 0 && ds.Tables[0].Rows.Count > 1))
			{
				lblSearchStatus.Text = "No records found for the given Search parameters.";
				//StatsDiv.Visible = false;
			}
			else
			{
				lblSearchStatus.Text = String.Empty;
				//StatsDiv.Visible = true;
			}

			return ds.Tables[0];
		}

		private DataTable GetDataDetailsByKey(string key)
		{
			DataTable dt = null;

			var groupOn = oSearchFilter.GroupBy;

			// this should not happen ...
			if (string.IsNullOrEmpty(groupOn) || groupOn == "-1")
			{
				groupOn = ReleaseLogDetailDataModel.DataColumns.ReleaseLogId;
				return AllDataRows;
			}

			if (AllDataRows != null)
			{
				var dataView = AllDataRows.DefaultView;

				dataView.RowFilter = groupOn + " = '" + key + "'";

				dt = dataView.ToTable();
			}

			return dt;
		}

		private DataTable GetSubGroupData(string groupKey, string subGroupKey)
		{
			DataTable dt = null;

			var groupOn = oSearchFilter.GroupBy;
			var subGroupOn = oSearchFilter.SubGroupBy;

			// this should not happen ...
			if (string.IsNullOrEmpty(groupOn) || groupOn == "-1" || string.IsNullOrEmpty(subGroupOn) || subGroupOn == "-1")
			{
				groupOn = ReleaseLogDetailDataModel.DataColumns.ReleaseLogId;
				return AllDataRows;
			}

			if (AllDataRows != null)
			{
				var dataView = AllDataRows.DefaultView;
				dataView.RowFilter = groupOn + " = '" + groupKey + "' and " + subGroupOn + " = '" + subGroupKey + "'";

				dt = dataView.ToTable();
			}

			return dt;
		}

		#endregion

		#region Methods

		private decimal GetTotalTimeSpentCount()
		{
			return ReleaseLogDetailDataManager.GetTotalTimeSpent(AllDataRows, ReleaseLogDetailDataModel.DataColumns.ReleaseNotesTimeSpentConstant);
		}

		protected void ClearValues()
		{
			TableReportContent.Controls.Clear();

			//TableReportContent.Controls.Add(StatsDiv);

			plcGroupByHolder.Controls.Clear();

			lstTotal.Clear();
			lstAverage.Clear();
			lstMax.Clear();
			lstMin.Clear();
			lstCount.Clear();
			lstMedians.Clear();
		}

		protected void TabSetUp()
		{
			var groupByField = oSearchFilter.GroupBy;

			//var dt = ReleaseLogDetails;

			if (AllDataRows != null && AllDataRows.Columns.Contains(groupByField))
			{
				// PARENT TAB
				var gTabControl = ApplicationCommon.GetNewDetailTabControl();
				gTabControl.Setup("ReleaseLogDetail");

				var distinctFieldValues = (from row in AllDataRows.AsEnumerable()
										   orderby row[groupByField].ToString().Trim()
										   select row[groupByField].ToString().Trim()).Distinct(StringComparer.CurrentCultureIgnoreCase);

				// remove duplicate cases ignoreing case comparision
				var iCount = 0;

				foreach (var value in distinctFieldValues)
				{
					var recordCount = AllDataRows.AsEnumerable().Where(a => Convert.ToString(a[groupByField]).ToLower().Trim() == value.ToLower()).Count();

					var ctlDetailsWithChildren = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
					ctlDetailsWithChildren.SettingCategory = SettingCategory + "DetailsWithChildrenListControl";
					ctlDetailsWithChildren.Setup("ReleaseLogDetail", "Shared/ApplicationManagement", "ReleaseLogDetailId", 0, true, GetDataDetailsByKey, GetColumns, "ReleaseLogDetail", DetailUserPreferenceCategoryId);

					var strHeader = value + " (" + recordCount + ")";

					// on intial bind, select first header
					if (iCount == 0)
					{
						gTabControl.AddTab(value, null, strHeader, true);
					}
					else
					{
						gTabControl.AddTab(value, null, strHeader);
					}

					iCount++;

					plcGroupByHolder.Controls.Add(gTabControl);
				}
			}
		}

		protected void GetTabControl()
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.Setup("ReleaseNotesView");

			var selected = false;

			if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
			{
				selected = true;
			}

			var divGrid = new HtmlGenericControl("div");
			divGrid.Attributes.Add("style", "padding:30px;");

			divGrid.Controls.Add(plcGroupByHolder);
			divGrid.Controls.Add(GrdParentGrid);

			tabControl.AddTab("Release Notes", divGrid, String.Empty, selected);

			selected = false;

			if (Request.QueryString["tab"] == "2")
			{
				selected = true;
			}

			// Statistic Info
			var subDiv = new HtmlGenericControl("div");
			subDiv.Attributes.Add("style", "padding:30px;");
			subDiv.Controls.Add(TableReportContent);

			tabControl.AddTab("Statistic Info", subDiv);

			//// Charts & Graphs
			var divGraph = new HtmlGenericControl("div");
			divGraph.Attributes.Add("style", "padding:30px;");
			divGraph.Controls.Add(dynChart);

			tabControl.AddTab("Charts & Graphs", divGraph);

			plcTabHolder.Controls.Add(tabControl);

		}

		private void AddSummaryStatisticLine(string parentKey, Statistic item)
		{
			// why would this happen?
			if (item.Count == 0) return;

			lstMedians.Add((decimal)item.Median);
			lstTotal.Add((decimal)item.Total);
			lstAverage.Add((decimal)item.Average);
			lstCount.Add((decimal)item.Count);
			lstMax.Add((decimal)item.Max);
			lstMin.Add((decimal)item.Min);

			ItemCount = GrdParentGrid.Items.Count;
		}

		private void SaveSessionInstanceFCMode(int selectedMode)
		{
			var currententity = "ReleaseLogDetail";

			if (Session[currententity + "SelectedMode"] == null)
				Session.Add(currententity + "SelectedMode", selectedMode);
			else
				Session[currententity + "SelectedMode"] = selectedMode;
		}

		private string[] GetColumns()
		{
			if (!ddlFieldConfigurationMode.SelectedItem.Text.Equals(String.Empty))
				return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail, ddlFieldConfigurationMode.SelectedValue, SessionVariables.RequestProfile);
			else
				return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail, "DBColumns", SessionVariables.RequestProfile);
		}

		private string[] GetMasterColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReleaseLog, "DBColumns", SessionVariables.RequestProfile);
		}

		private int GetSessionInstanceFCMode(string currentEntity)
		{
			if (Session[currentEntity + "SelectedMode"] != null)
				return Convert.ToInt32(Session[currentEntity + "SelectedMode"].ToString());
			else
				return -1;
		}





        private List<FieldConfigurationModeDataModel> GetApplicableModesList(int systemEntityTypeId)
        {
            var data = new FieldConfigurationDataModel();
            data.SystemEntityTypeId = systemEntityTypeId;

			var columns = FieldConfigurationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

            var modes = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);

            var validModes = new List<FieldConfigurationModeDataModel>();

            for (var j = 0; j < modes.Count; j++)
            {
                for (var i = 0; i < columns.Count; i++)
                {
                    if (
                            columns[i].FieldConfigurationModeId.Value == modes[j].FieldConfigurationModeId.Value
                        )
                    {
                        if (!validModes.Where(x => x.FieldConfigurationModeId == modes[j].FieldConfigurationModeId.Value).Any())
                        {
                            validModes.Add(modes[j]);
                        }
                    }
                }
            }

            validModes = validModes.OrderBy(x => x.SortOrder).ToList();
            return validModes;
        }

		private int SetUpDropDownFCMode()
		{
			var systemEntityTypeId = (int)Enum.Parse(typeof(SystemEntity), "ReleaseLogDetail");
			var dt = GetApplicableModesList(systemEntityTypeId);
			var modeSelected = GetSessionInstanceFCMode("ReleaseLogDetail");
			var settingCategory = "ReleaseLogNotesDefaultViewListControl";
			var upcId = PreferenceUtility.CreateUserPreferenceCategoryIfNotExists(settingCategory, settingCategory);
			var fcModeSelected = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.FieldConfigurationMode, settingCategory);

			if (dt.Count > 0)
			{
				ddlFieldConfigurationMode.DataSource = dt;
				ddlFieldConfigurationMode.DataTextField = "Name";
				ddlFieldConfigurationMode.DataValueField = "FieldConfigurationModeId";
				ddlFieldConfigurationMode.DataBind();
				ddlFieldConfigurationMode.SelectedValue = modeSelected.ToString();

				if (Convert.ToInt32(fcModeSelected) > 0)
				{
					ddlFieldConfigurationMode.SelectedValue = fcModeSelected.ToString();
					SaveSessionInstanceFCMode(Convert.ToInt32(fcModeSelected));
				}
				else
					ddlFieldConfigurationMode.SelectedValue = modeSelected.ToString();
			}
			else
			{
				ddlFieldConfigurationMode.Visible = false;
			}

			return modeSelected;
		}

		private void FormatListControl(bool addStyle)
		{
			if (addStyle)
			{
				for (var i = 0; i < GrdParentGrid.Items.Count; i++)
				{
					var list = (DetailsWithChildrenControl)GrdParentGrid.Items[i].FindControl("oList");

					if (list != null)
					{

						//list.MainGridViewList.RowStyle.Height = 10;
						//list.MainGridViewList.AlternatingRowStyle.Height = 10;
						//list.MainGridViewList.GridLines = GridLines.None;
						//list.MainGridViewList.RowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#696969");
						//list.MainGridViewList.AlternatingRowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#696969");
						//list.MainGridViewList.RowStyle.Font.Size = 8;
						//list.MainGridViewList.AlternatingRowStyle.Font.Size = 8;
						//list.MainGridViewList.HeaderStyle.Height = 10;
						//list.MainGridViewList.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#3A4F63");
						//list.MainGridViewList.HeaderStyle.ForeColor = System.Drawing.Color.White;
						//list.MainGridViewList.HeaderStyle.Height = 10;
						//list.MainGridViewList.HeaderStyle.Font.Size = 9;
						//list.MainGridViewList.GridLines = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesGridLines);

						// styling moved to StyleGrid.aspx

						//list.MainGridViewList.RowStyle.Height = new Unit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleHeight));
						//list.MainGridViewList.AlternatingRowStyle.Height = new Unit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight));
						//list.MainGridViewList.RowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleForeColor));
						//list.MainGridViewList.AlternatingRowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor));
						//list.MainGridViewList.RowStyle.Font.Size = new FontUnit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleFontSize));
						//list.MainGridViewList.AlternatingRowStyle.Font.Size = new FontUnit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize));
						//list.MainGridViewList.HeaderStyle.Height = new Unit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleHeight));
						//list.MainGridViewList.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderBackColor));
						//list.MainGridViewList.HeaderStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderForeColor));
						//list.MainGridViewList.HeaderStyle.Font.Size = new FontUnit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleFontSize));

						string value = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesGridLines);

						switch (value)
						{
							case "Both":
								list.MainGridViewList.GridLines = GridLines.Both;
								break;

							case "None":
								list.MainGridViewList.GridLines = GridLines.None;
								break;

							case "Horizontal":
								list.MainGridViewList.GridLines = GridLines.Horizontal;
								break;

							case "Vertical":
								list.MainGridViewList.GridLines = GridLines.Vertical;
								break;
						}

					}
				}
			}
			else
			{
				for (var i = 0; i < GrdParentGrid.Items.Count; i++)
				{
					var list = (DetailsWithChildrenControl)GrdParentGrid.Items[i].FindControl("oList");

					if (list != null)
					{
						for (var j = 0; j < list.MainGridViewList.Rows.Count; j++)
						{
							list.MainGridViewList.Rows[j].Style.Clear();
							list.MainGridViewList.GridLines = GridLines.Both;
						}

					}
				}
			}
		}

		protected DataView ChildRelation(object dataitem, string relation)
		{
			var drv = dataitem as DataRowView;

			if (drv != null)
				return drv.CreateChildView(relation);
			else
				return null;

		}

		private void ManageControlVisibility(string controlTitle)
		{
			var sbm = Master.SubMenuObject;

			switch (controlTitle)
			{
				case "Search Box":
					oSearchFilter.Visible = true;
					PreferenceUtility.UpdateUserPreference(oSearchFilter.SearchControl.SettingCategory, ApplicationCommon.ControlVisible, "true");
					break;

				case "Sub Menu":
					sbm.Visible = true;
					PreferenceUtility.UpdateUserPreference(sbm.SettingCategory, ApplicationCommon.ControlVisible, "true");
					break;
			}
		}

		#endregion

		#region Page Events

		protected override void OnInit(EventArgs e)
		{
			SettingCategory                             = "ReleaseNotesDefaultView";
			DetailUserPreferenceCategoryId              = PreferenceUtility.CreateUserPreferenceCategoryIfNotExists("ReleaseLogDetail", "ReleaseLogDetail");
			VisibilityManagerCore                       = oVC;
			oSearchFilter.SearchControl.SettingCategory = SettingCategory + "SearchControl";

			oSearchFilter.GetFilter(SystemEntity.ReleaseLogDetail, "ReleaseLogDetailId");

			GetTabControl();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			var sbm = Master.SubMenuObject;

			if (!IsPostBack)
			{
				oSearchFilter.SearchControl.Title = "Search Box";
				sbm.Title = "Sub Menu";
				SetUpDropDownFCMode();
			}

			VisibilityManagerCore.Setup(ManageControlVisibility, SettingCategory);

			oSearchFilter.SearchControl.SetupSearch();

			GetData();

			//TabSetUp();			

			FormatListControl(true);

			lnkGridStyle.Text = "Classic";

			//Log4Net.LogInfo("ReleaseNotes Page Load","PageLoad",100);
			myExportMenu.Setup("ReleaseLogDetail", "Shared/ApplicationManagement", GetData, GetMasterColumns, oSearchFilter.SearchParameters.ToURLQuery());
			oSearchFilter.SearchControl.OnSearch += oSearchFilter_OnSearch;
			//Log4Net.LogInfo("End ReleaseNotes Page Load", "PageLoad", 100);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var sbm = Master.SubMenuObject;

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup("");
			bcControl.GenerateMenu();

			VisibilityManagerCore = oVC;

			var isSubMenuVisible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, sbm.SettingCategory);
			var isSearchControlVisible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, oSearchFilter.SearchControl.SettingCategory);

			// set visibility
			oSearchFilter.Visible = isSearchControlVisible;
			sbm.Visible = isSubMenuVisible;

			VisibilityManagerCore.ClearChildMenuItems();

			VisibilityManagerCore.AddChildControl(oSearchFilter.SearchControl.Title, isSearchControlVisible);
			VisibilityManagerCore.AddChildControl(sbm.Title, isSubMenuVisible);

			// bccontrol.SettingCategory = SettingCategory + "BreadCrumbControl";
			// bccontrol.Setup("");
			// bccontrol.GenerateMenu();
		}

		#endregion

		#region Control Events

		protected void lnkGridStyle_Click(object sender, EventArgs e)
		{
			oSearchFilter.SearchControl.RaiseSearch();
			if (lnkGridStyle.Text.Contains("Classic"))
			{
				FormatListControl(false);
				lnkGridStyle.Text = "Latest";
			}
			else
			{
				FormatListControl(true);
				lnkGridStyle.Text = "Classic";
			}
		}

		void oSearchFilter_OnSearch(object sender, EventArgs e)
		{
			ClearValues();
			GetData();
		}

		protected void chkbox_Click(Object sender, EventArgs e)
		{
			if (sender != null)
			{
				try
				{
					var releaselogid = ((LinkButton)sender).CommandName;
					for (var i = 0; i < GrdParentGrid.Items.Count; i++)
					{
						var hdnfield = (HiddenField)GrdParentGrid.Items[i].FindControl("hdnReleaseLogId");
						if (hdnfield != null)
						{
							if (hdnfield.Value.Equals(releaselogid))
							{
								GrdParentGrid.Items[i].Visible = false;
							}
						}
					}
				}
				catch (Exception ex) { }
			}
		}

		protected void ddlFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			GetData();

			var fcMode = ddlFieldConfigurationMode.SelectedValue;
			SaveSessionInstanceFCMode(Convert.ToInt32(fcMode));

			var settingCategory = "ReleaseLogNotesDefaultViewListControl";
			PreferenceUtility.UpdateUserPreference(settingCategory, ApplicationCommon.FieldConfigurationMode, fcMode);
		}

		#endregion

		#region Grid Events

		protected void GrdParentGrid_RowDataBound(object sender, RepeaterItemEventArgs e)
		{
			var drv = e.Item.DataItem as DataRowView;

			if (drv == null) return;

			var hdnField = e.Item.FindControl("hdnReleaseLogId") as HiddenField;
			var btnInsert = e.Item.FindControl("btnInsert") as LinkButton;
			var btnCheckBox = e.Item.FindControl("chkbox") as LinkButton;

			//tab control or detail Grid Container DIV
			var detailsGridContainer = e.Item.FindControl("detailsGridContainer") as HtmlGenericControl;

			if (oSearchFilter.GroupBy == "ReleaseLog")
			{
				btnInsert.PostBackUrl = Page.GetRouteUrl("ReleaseLogDetailEntityRoute", new { Action = "Insert" });
				btnInsert.PostBackUrl += "/" + hdnField.Value;
			}

			var parentKey = string.Empty;

			var parentKeyName = parentKey = DataBinder.Eval(e.Item.DataItem, "Name").ToString();

			if (oSearchFilter.GroupBy == "" || oSearchFilter.GroupBy == "All" || oSearchFilter.GroupBy == "-1")
			{
				parentKey = hdnField.Value;
				btnInsert.Visible = false;
				btnCheckBox.Visible = false;
			}
			else
			{
				parentKey = DataBinder.Eval(e.Item.DataItem, "Key").ToString();
			}

			//data set for the current group
			var filteredGroup = GetDataDetailsByKey(parentKey);

			var statisticItem = ReleaseLogDetailDataManager.GetStatisticData(filteredGroup, ReleaseLogDetailDataModel.DataColumns.ReleaseNotesTimeSpentConstant, ApplicationCommon.ReleaseNotesStatisticUnknown);

			AddSummaryStatisticLine(parentKey, statisticItem);			

			if (!String.IsNullOrEmpty(oSearchFilter.GroupBy) 
				&& oSearchFilter.GroupBy != "-1" 
				&& oSearchFilter.GroupBy != "All"
				&& !String.IsNullOrEmpty(oSearchFilter.SubGroupBy) 
				&& oSearchFilter.SubGroupBy != "-1"
				&& oSearchFilter.SubGroupBy != "All")
			{

				// get new instance of tab control to added for the group
				var tabControl = ApplicationCommon.GetNewDetailTabControl();
				tabControl.Setup(SettingCategory);

				var subGroupByColumn = oSearchFilter.SubGroupBy;
				
				// Add Group By Key Information
				var grpByDiv = new HtmlGenericControl("div");
                grpByDiv.InnerHtml = "<strong>Group: " + parentKey + "</strong>";
				TableReportContent.Controls.Add(grpByDiv);

				// Add Header Row for Statistics table
				TableReportContent.Controls.Add(GetStatisticInfoHTMLHeaderRow(subGroupByColumn));

				// get distinct sub group by values
				var distinctFieldValues = (from row in filteredGroup.AsEnumerable()
										   .Where(row => row[subGroupByColumn].ToString().Trim() != "")
										   orderby row[subGroupByColumn].ToString().Trim() descending
										   select row[subGroupByColumn].ToString().Trim()).Distinct(StringComparer.CurrentCultureIgnoreCase);
				
				//var series = new decimal[filteredGroup.Rows.Count];
				//var i = 0;

				//foreach (DataRow item in filteredGroup.Rows)
				//{
				//	var timeSpent = item[ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString();

				//	var timeSpentValue = 0m;

				//	if (!Decimal.TryParse(timeSpent, out timeSpentValue))
				//	{
				//		timeSpentValue = ApplicationVariables.ReleaseNotesTimeSpentConstant;
				//	}

				//	series[i++] = timeSpentValue;
				//}

				//var timeSpentForGroup = series.Sum();

				var timeSpentForGroup = ReleaseLogDetailDataManager.GetTotalTimeSpent(filteredGroup, ReleaseLogDetailDataModel.DataColumns.ReleaseNotesTimeSpentConstant);

				// create tab for each sub group by distinct value
				foreach (var key in distinctFieldValues)
				{

					var detailContainer = new HtmlGenericControl("div");
					//detailContainer.Style.Add("Width", "100%");

					var ctlDetailsWithChildren = Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl) as DetailsWithChildrenControl;

					ctlDetailsWithChildren.SettingCategory = SettingCategory + "DetailsWithChildrenListControl";
					ctlDetailsWithChildren.IsFCModeVisible = false;
					ctlDetailsWithChildren.Setup("ReleaseLogDetail", "Shared/ApplicationManagement", "ReleaseLogDetailId", parentKey, subGroupByColumn, key, true, GetSubGroupData, GetColumns, "ReleaseLogDetail", DetailUserPreferenceCategoryId);
					ctlDetailsWithChildren.SetVisibilityOfListFeatures(false, false, false);
					ctlDetailsWithChildren.SetSession("true");
					//ctlDetailsWithChildren.Attributes.Add("width", "100%");

					var subGroupData = GetSubGroupData(parentKey, key);
					var item = ReleaseLogDetailDataManager.GetStatisticData(subGroupData, ReleaseLogDetailDataModel.DataColumns.ReleaseNotesTimeSpentConstant, ApplicationCommon.ReleaseNotesStatisticUnknown);

					var statisticControl = Page.LoadControl(ApplicationCommon.ReleaseNoteStatisticControlPath) as ReleaseNoteStatistics;
					statisticControl.SetStatistics(parentKey, item);

					detailContainer.Controls.Add(ctlDetailsWithChildren);
					detailContainer.Controls.Add(statisticControl);

					// add row for each sub grouping for statistic info tab
					var ctrlStat = GetStatisticInfoHTMLRow(item, key, timeSpentForGroup, filteredGroup.Rows.Count);
					TableReportContent.Controls.Add(ctrlStat);

					// add to tab control
					tabControl.AddTab(key, detailContainer);

					if (Page.IsPostBack)
					{
						ctlDetailsWithChildren.ShowData(false, true);
					}
				}

				// add summary row for sub grouping for statistics info tab
				var ctrlStatSummary = GetStatisticInfoHTMLSummaryRow(filteredGroup, oSearchFilter.SubGroupBy);					
				TableReportContent.Controls.Add(ctrlStatSummary);

				var seperator = new HtmlGenericControl("div");
				seperator.InnerHtml = "<br/>";
				TableReportContent.Controls.Add(seperator);

				if (detailsGridContainer != null)
				{
					detailsGridContainer.Controls.Add(tabControl);
				}
			}
			else // only Group By Case
			{
				var ctlDetailsWithChildren = Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl) as DetailsWithChildrenControl;

				ctlDetailsWithChildren.ID = "oList";
				ctlDetailsWithChildren.SettingCategory = SettingCategory + "DetailsWithChildrenListControl";
				ctlDetailsWithChildren.IsFCModeVisible = false;
				ctlDetailsWithChildren.Setup("ReleaseLogDetail", "Shared/ApplicationManagement", "ReleaseLogDetailId", parentKey, true, GetDataDetailsByKey, GetColumns, "ReleaseLogDetail", DetailUserPreferenceCategoryId);
				ctlDetailsWithChildren.SetVisibilityOfListFeatures(false, false, false);
				ctlDetailsWithChildren.SetSession("true");

				if (detailsGridContainer != null)
				{
					detailsGridContainer.Controls.Add(ctlDetailsWithChildren);
				}

				if (Page.IsPostBack)
				{
					ctlDetailsWithChildren.ShowData(false, true);
				}

				var statisticControlGroup = Page.LoadControl(ApplicationCommon.ReleaseNoteStatisticControlPath) as ReleaseNoteStatistics;
				statisticControlGroup.SetStatistics(parentKey, statisticItem);

				if (detailsGridContainer != null)
				{
					detailsGridContainer.Controls.Add(statisticControlGroup);
				}

				HtmlGenericControl ctrlStat = null;

				if (oSearchFilter.GroupBy == "-1")
				{
					ctrlStat = GetStatisticInfoHTMLRow(statisticItem, parentKeyName, TotalTimeSpentCount, AllDataRows.Rows.Count);
				}
				else
				{
					ctrlStat = GetStatisticInfoHTMLRow(statisticItem, parentKey, TotalTimeSpentCount, AllDataRows.Rows.Count);
				}

				//var ctrlStat = oSG.GetStatisticDataGrid(statisticItems, parentKey);
				TableReportContent.Controls.Add(ctrlStat);

				// check if all rows created, if yes then add, summary row in the end.
				if (TotalParentCount == GrdParentGrid.Items.Count + 1)
				{
					var ctrlStatSummary = GetStatisticInfoHTMLSummaryRow(AllDataRows, oSearchFilter.GroupBy);
					TableReportContent.Controls.Add(ctrlStatSummary);
				}

			}

			// chart related code
			if (oSearchFilter.GroupBy == "-1")
			{
				oSC.Setup(parentKeyName, statisticItem);
			}
			else
			{
				oSC.Setup(parentKey, statisticItem);
			}

		}

		#endregion

		#region font

		protected void lnkfontsmall_Click(object sender, EventArgs e)
		{
			//maindiv.Attributes.Add("class", "smallfontgrid");
			//maindiv.Style.Add("font-size", "12px");
			oSearchFilter.SearchControl.RaiseSearch();
		}

		protected void lnkfontmedium_Click(object sender, EventArgs e)
		{
			//maindiv.Attributes.Add("class", "mediumfontgrid");
			//maindiv.Style.Add("font-size", "14px");
			oSearchFilter.SearchControl.RaiseSearch();
		}

		protected void lnkfontlarger_Click(object sender, EventArgs e)
		{
			//maindiv.Attributes.Add("class", "largerfontgrid");
			//maindiv.Style.Add("font-size", "16px");
			oSearchFilter.SearchControl.RaiseSearch();
		}

		#endregion font

	}

}


