using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
{
	public partial class EditScheduleQuestion : System.Web.UI.UserControl
	{

		#region Methods

		public void SetId(int setId)
		{
			ViewState["SetId"] = setId;
		}

		private void GetData(int scheduleId)
		{
			var dtScheduleQuestions = ScheduleQuestionDataManager.GetBySchedule(scheduleId, SessionVariables.RequestProfile);
			gvScheduleQuestions.DataSource = dtScheduleQuestions;
			gvScheduleQuestions.DataBind();
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetData((int)ViewState["SetId"]);
			}
		}

		protected void gvScheduleQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var answerCombo = e.Row.FindControl("drpAnswer");
				if (answerCombo != null)
				{
					var drpAnswer = (DropDownList)answerCombo;
					
					if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Answer"] != DBNull.Value)
					{
						drpAnswer.SelectedValue = Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row["Answer"]);
					}
				}
			}
		}

		protected void lbtnSubmit_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gvScheduleQuestions.Rows)
			{
				var lblScheduleQuestionId             = (Label)row.FindControl("lblScheduleQuestionId");
				var lblQuestionId                     = (Label)row.FindControl("lblQuestionId");
				var drpAnswer                         = (DropDownList)row.FindControl("drpAnswer");

				var data                              = new ScheduleQuestionDataModel();

				data.ScheduleId                       = (int)ViewState["SetId"];
				data.QuestionId                       = Convert.ToInt32(lblQuestionId.Text);
				data.Answer                           = drpAnswer.SelectedValue;

				data.ScheduleQuestionId               = Convert.ToInt32(lblScheduleQuestionId.Text);

				ScheduleQuestionDataManager.Update(data, SessionVariables.RequestProfile);
			}

			GetData((int)ViewState["SetId"]);
		}

		#endregion

	}
}