using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.TasksAndWorkflow;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.Framework.TasksAndWorkFlow;

namespace Shared.UI.Web.TasksAndWorkflow.TaskRun.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
		#region private methods

		protected override void ShowData(int taskRunId)
		{
			base.ShowData(taskRunId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new TaskRunDataModel();
			dataQuery.TaskRunId = taskRunId;

			var entityList = Framework.Components.TasksAndWorkflow.TaskRunDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{

					lblTaskRunId.Text		= entityItem.TaskRunId.ToString();
					lblTaskEntity.Text		= entityItem.TaskEntityId.ToString();
					lblTaskScheduleId.Text	= entityItem.TaskScheduleId.ToString();
					lblBusinessDate.Text	= entityItem.BusinessDate.ToString();
					lblStartTime.Text		= entityItem.StartTime.ToString();
					lblEndTime.Text			= entityItem.EndTime.ToString();
					lblRunBy.Text			= entityItem.RunBy.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, taskRunId, "TaskRun");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskRunIdText, lblBusinessDateText,lblRunByText,lblStartTimeText, lblEndTimeText,lblTaskEntityText, lblTaskScheduleIdText });
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
				lblTaskRunIdText.Visible = isTesting;
				lblTaskRunId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TaskRunLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRun;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTaskRunId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion
        
	}

}