using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int ScheduleQuestionId)
		{
			base.ShowData(ScheduleQuestionId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new ScheduleQuestionDataModel();
			dataQuery.ScheduleQuestionId = ScheduleQuestionId;

            var entityList = ScheduleQuestionDataManager.GetEntityDetails(dataQuery,SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{

					lblScheduleQuestionId.Text	= entityItem.ScheduleQuestionId.ToString();
					lblScheduleId.Text			= entityItem.ScheduleId.ToString();
					lblQuestionId.Text			= entityItem.QuestionId.ToString();
					lblAnswer.Text				= entityItem.Answer.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, ScheduleQuestionId, "ScheduleQuestion");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblScheduleQuestionIdText, lblScheduleIdText, lblQuestionText, lblAnswerText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblScheduleQuestionIdText.Visible = isTesting;
				lblScheduleQuestionId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ScheduleQuestionLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleQuestion;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynScheduleQuestionId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion  
          
    }
}