using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleItem.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int scheduleItemId)
		{
			base.ShowData(scheduleItemId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new ScheduleItemDataModel();
			dataQuery.ScheduleItemId = scheduleItemId;

            var entityList = TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{

					lblScheduleItemId.Text		= entityItem.ScheduleItemId.ToString();
					lblScheduleId.Text			= entityItem.ScheduleId.ToString();
					lblTaskFormulationId.Text	= entityItem.TaskFormulationId.ToString();
					lblTotalTimeSpent.Text		= entityItem.TotalTimeSpent.ToString();					

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, scheduleItemId, "ScheduleItem");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblScheduleItemText, lblScheduleIdText, lblTaskFormulationIdText, lblTotalTimeSpentText });
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
				lblScheduleItemText.Visible = isTesting;
				lblScheduleItemId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ScheduleItemLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleItem;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynScheduleItemId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion        
	
        
    }
}