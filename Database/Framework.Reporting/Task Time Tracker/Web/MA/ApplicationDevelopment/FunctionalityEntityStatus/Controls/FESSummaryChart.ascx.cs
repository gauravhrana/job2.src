using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Controls
{
	public partial class FESSummaryChart : BaseControl
	{

		#region Properties

		public string GroupBy
		{
			get
			{
				if (ddlGroupBy.SelectedValue != "-1")
				{
					return ddlGroupBy.SelectedValue;
				}
				return String.Empty;
			}
		}

		public string SubGroupBy
		{
			get
			{
				if (ddlSubGroupBy.SelectedValue != "-1")
				{
					return ddlSubGroupBy.SelectedValue;
				}
				return String.Empty;
			}
		}

		#endregion

		#region Methods

		public void GenerateChart()
		{
			var startDate = DateTime.Now.AddMonths(-3);
			var endDate = DateTime.Now;

			DataTable functionalityEntityStatusGroupByData = null;

			if (txtSearchConditionToDate1.Text != "" && txtSearchConditionToDate2.Text != "")
			{
				startDate = DateTime.ParseExact(txtSearchConditionToDate1.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
				endDate = DateTime.ParseExact(txtSearchConditionToDate2.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
			}

			if (ddlGroupBy.SelectedValue == "Date")
			{
				//functionalityEntityStatusGroupByData = FunctionalityEntityStatusDataManager.GetDateRangeList(drpSearchConditionFunctionality.SelectedItem.Text, startDate, endDate);
			}
			else
			{
				if (lnkGridSummary.Text.Contains("Stack"))
				{
					functionalityEntityStatusGroupByData = FunctionalityEntityStatusDataManager.GetAggregateList(Convert.ToInt32(drpSearchConditionFunctionality.SelectedItem.Value), ddlSubGroupBy.SelectedValue, ddlGroupBy.SelectedValue, startDate, endDate, SessionVariables.RequestProfile);
				}
				else
				{
					functionalityEntityStatusGroupByData = FunctionalityEntityStatusDataManager.GetAggregateList(Convert.ToInt32(drpSearchConditionFunctionality.SelectedItem.Value), ddlGroupBy.SelectedValue, ddlSubGroupBy.SelectedValue, startDate, endDate, SessionVariables.RequestProfile);
				}
			}

			if (functionalityEntityStatusGroupByData != null)
			{
				for (var i = 0; i < functionalityEntityStatusGroupByData.Rows.Count; i++)
				{
					var strSubGroup = functionalityEntityStatusGroupByData.Rows[i][3].ToString();

					if (Chart1.Series.Count == 0)
						Chart1.Series.Add(strSubGroup);

					Series series = Chart1.Series.FindByName(strSubGroup);

					if (series == null)
						Chart1.Series.Add(strSubGroup);

					if (lnkGridSummary.Text.Contains("Stack"))
						Chart1.Series[strSubGroup].ChartType = SeriesChartType.Pie;

					Chart1.Series[strSubGroup].ChartType = SeriesChartType.StackedColumn;

					Chart1.Series[strSubGroup].Points.AddXY(functionalityEntityStatusGroupByData.Rows[i][2].ToString(), Convert.ToInt32(functionalityEntityStatusGroupByData.Rows[i][0]));
					Chart1.Series[strSubGroup].IsValueShownAsLabel = true;

					if (lnkGridSummary.Text.Contains("Stack"))
					{
						Chart1.Series[strSubGroup].ChartType = SeriesChartType.Pie;
					}
					else
					{
						Chart1.Series[strSubGroup].ChartType = SeriesChartType.StackedColumn;
					}
				}
			}
		}

		public void LoadSubGroupData()
		{

		}

		private void SetDefaultValues()
		{
			drpSearchConditionFunctionality.SelectedIndex = 0;
			ddlGroupBy.SelectedIndex = 0;
			ddlSubGroupBy.SelectedIndex = 0;
			txtSearchConditionFunctionality.Text = String.Empty;
			txtSearchConditionGroupBy.Text = String.Empty;
			txtSearchConditionSubGroupBy.Text = String.Empty;
		}

		private DataTable GetFilteredList(string Entity, DataTable systemEntityData, DataTable systemEntityDataList)
		{
			var dt = systemEntityDataList.Clone();

			for (var i = 0; i < systemEntityData.Rows.Count; i++)
			{
				for (var j = 0; j < systemEntityDataList.Rows.Count; j++)
				{
					if (systemEntityData.Rows[i][Entity + "Id"].ToString().Equals(systemEntityDataList.Rows[j][Entity + "Id"].ToString()))
					{
						dt.ImportRow(systemEntityDataList.Rows[j]);
					}
				}
			}

			return dt;
		}

		#endregion

		#region Events

		protected override void GetSettings()
		{
            SettingCategory = "FunctionalityEntityStatusDefaultViewSummaryControl";
			
			var category = SettingCategory;
			//var value = String.Empty;

			drpSearchConditionFunctionality.SelectedIndex = UIHelper.GetDropDownSelectedIndex(drpSearchConditionFunctionality, FunctionalityEntityStatusDataModel.DataColumns.Functionality,category);
			txtSearchConditionFunctionality.Text = drpSearchConditionFunctionality.SelectedValue;

			ddlGroupBy.SelectedIndex = UIHelper.GetDropDownSelectedIndex(ddlGroupBy, "GroupBy", category);
			txtSearchConditionGroupBy.Text = ddlGroupBy.SelectedIndex.ToString();

			ddlSubGroupBy.SelectedIndex = UIHelper.GetDropDownSelectedIndex(ddlSubGroupBy, "SubGroupBy", category);
			txtSearchConditionSubGroupBy.Text = ddlSubGroupBy.SelectedIndex.ToString();
		}

		protected override void SaveSettings()
		{
			PerferenceUtility.UpdateUserPreference(SettingCategory, FunctionalityEntityStatusDataModel.DataColumns.Functionality, drpSearchConditionFunctionality.SelectedItem.Text);
			PerferenceUtility.UpdateUserPreference(SettingCategory, "GroupBy", ddlGroupBy.SelectedItem.Text);
			PerferenceUtility.UpdateUserPreference(SettingCategory, "SubGroupBy", ddlSubGroupBy.SelectedItem.Text);
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var functionalityData = FunctionalityEntityStatusDataManager.GetUniqueIdList("Functionality", SessionVariables.RequestProfile);
				var functionalityDataList = FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
				var filteredfunctionalityData = GetFilteredList("Functionality", functionalityData, functionalityDataList);
				
				UIHelper.LoadDropDown(
						filteredfunctionalityData
					,	drpSearchConditionFunctionality
					,	StandardDataModel.StandardDataColumns.Name
					,	FunctionalityDataModel.DataColumns.FunctionalityId
				);
				
				ddlGroupBy.Items.Add("Date");
				LoadSubGroupData();
				GetSettings();
				GenerateChart();
				oSearchActionBar.Setup("FESSummaryChart");	
			}

            //GenerateChart();
            SaveSettings();
		}

        protected void lnkGridSummary_Click(object sender, EventArgs e)
        {
            if (lnkGridSummary.Text.Contains("Pie"))
            {
                
                lnkGridSummary.Text = "Show Stack Chart";
            }
            else
            {
                lnkGridSummary.Text = "Show Pie Chart";
            }

            GenerateChart();
        }

		protected void ddlGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchConditionGroupBy.Text = ddlGroupBy.SelectedIndex.ToString();
		}

		protected void ddlSubGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchConditionSubGroupBy.Text = ddlSubGroupBy.SelectedIndex.ToString();
		}

		protected void drpSearchConditionFunctionality_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchConditionFunctionality.Text = drpSearchConditionFunctionality.SelectedValue;
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			GenerateChart();
			SaveSettings();
		}

		protected void btnReset_Click(object sender, EventArgs e)
		{
			SetDefaultValues();
			SaveSettings();
		}

		protected void Chart1_Load(object sender, EventArgs e)
		{

		}

		#endregion

	}
}