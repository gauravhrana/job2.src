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

namespace Shared.UI.Web.TasksAndWorkflow.TaskSchedule.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int taskScheduleId)
		{
			base.ShowData(taskScheduleId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new TaskScheduleDataModel();
			dataQuery.TaskScheduleId = taskScheduleId;

			var entityList = Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblTaskScheduleId.Text		= entityItem.TaskScheduleId.ToString();
					lblApplicationId.Text		= entityItem.ApplicationId.ToString();					
					lblTaskEntity.Text			= entityItem.TaskEntity.ToString();
					lblTaskScheduleType.Text	= entityItem.TaskScheduleType.ToString();					

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, taskScheduleId, "TaskSchedule");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskScheduleIdText,  lblTaskEntityText, lblTaskScheduleTypeText });
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
				lblTaskScheduleIdText.Visible = isTesting;
				lblTaskScheduleId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TaskScheduleLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskSchedule;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTaskScheduleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion

	}

}