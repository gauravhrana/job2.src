using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Text;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
		#region variables

		public ScheduleQuestionDataModel SearchParameters
		{
			get
			{
				var data = new ScheduleQuestionDataModel();

				var columnName = ScheduleQuestionDataModel.DataColumns.UpdatedRange;
				var date = Convert.ToString(CheckAndGetFieldValue(columnName));
				
				if (!string.IsNullOrEmpty(date))
				{
					var dates = date.Split('&');
					if (Boolean.Parse(dates[2]))
					{
                        data.FromSearchDate = Shared.WebCommon.UI.Web.DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
                        data.ToSearchDate = Shared.WebCommon.UI.Web.DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);

					}
				}

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(ScheduleQuestionDataModel.DataColumns.QuestionId).ToString()) != null)
					data.QuestionId = (int)CheckIfValueIsValidAsInt(CheckAndGetFieldValue(ScheduleQuestionDataModel.DataColumns.QuestionId).ToString());

				data.Answer = CheckAndGetFieldValue(ScheduleQuestionDataModel.DataColumns.Answer).ToString();
				//data.QuestionId = GetParameterValueAsInt(ScheduleQuestionDataModel.DataColumns.QuestionId);

				//if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				//   ScheduleQuestionDataModel.DataColumns.Answer + "Visibility", SettingCategory)
				//   && CheckAndGetFieldValue(
				//	   ScheduleQuestionDataModel.DataColumns.Answer) != "")
				//{
				//	data.Answer = CheckAndGetFieldValue(
				//		ScheduleQuestionDataModel.DataColumns.Answer).ToString();
				//}

				return data;
			}
		}		

		#endregion

		#region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			//if (fieldName.Equals("QuestionId"))
			//{
			//	var questionData = QuestionDataManager.GetList(SessionVariables.RequestProfile.AuditId);
			//	UIHelper.LoadDropDown(questionData, dropDownListControl, QuestionDataModel.DataColumns.QuestionPhrase,
			//		QuestionDataModel.DataColumns.QuestionId);
			//}
		}

		public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		{
			if (fieldName.Equals("QuestionId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetScheduleQuestionList", "QuestionPhrase", "QuestionId", plcControlHolder);	
			}
			return string.Empty;
		}

		//public override Dictionary<string, string> CheckGroupByListBoxSources(string name)
		//{
		//	if (name.Contains("Date") || name.Contains("UpdatedRange"))
		//	{
		//		LstGroupByItems.Add(name, name);
		//	}
		//	return LstGroupByItems;
		//}

		//public override Dictionary<string, string> CheckAddGroupByListBoxSources()
		//{
		//	Dictionary<string, string> lstGroupByAddItems = new Dictionary<string, string>();

		//	//lstGroupByItems.Add("TestGroupBy","TestGroupBy");

		//	return lstGroupByAddItems;
		//}

		private string CheckIfValueIsValid(string strValue)
		{
			if (strValue != "-1" && !string.IsNullOrEmpty(strValue))
			{
				return strValue;
			}

			return null;
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey		= "ScheduleQuestion";
			FolderLocationFromRoot	= "Scheduling/ScheduleQuestion";
			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.ScheduleQuestion;

			SearchActionBarCore		= oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}