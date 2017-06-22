using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Collections;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Controls
{
	public partial class Details : ControlDetails
	{

		#region private methods

		protected override void ShowData(int scheduleDetailId)
		{
			base.ShowData(scheduleDetailId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ScheduleDetailDataModel();
			data.ScheduleDetailId = scheduleDetailId;

            var items = ScheduleDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item                               = items[0];

				lblScheduleDetailId.Text               = item.ScheduleDetailId.ToString();
				lblScheduleId.Text                     = item.ScheduleId.ToString();
				lblInTime.Text                         = String.Format("{0:t}",item.InTime);
				lblOutTime.Text                        = String.Format("{0:t}",item.OutTime);
				lblMessage.Text                        = item.Message;
                lblWorkTicket.Text                     = item.WorkTicket;
				lblScheduleDetailActivityCategory.Text = item.ScheduleDetailActivityCategory;
				lblCreatedByAuditId.Text               = item.CreatedByAuditId.ToString();
				lblModifiedByAuditId.Text              = item.ModifiedByAuditId.ToString();
				lblCreatedDate.Text                    = item.CreatedDate.Value.ToString(SessionVariables.UserDateFormat);
				lblModifiedDate.Text                   = item.ModifiedDate.Value.ToString(SessionVariables.UserDateFormat);
				
				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, scheduleDetailId, "ScheduleDetail");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblScheduleDetailIdText, lblScheduleIdText, lblScheduleDetailActivityCategoryText, 
                    lblMessageText, lblWorkTicketText, lblInTimeText, lblOutTimeText,
					lblCreatedByAuditIdText, lblCreatedDateText, lblModifiedDateText,  lblModifiedByAuditIdText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			DictionaryLabel = CacheConstants.ScheduleDetailLabelDictionary;
			PrimaryEntity = SystemEntity.ScheduleDetail;

			base.OnInit(e);

			PlaceHolderCore = dynScheduleDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblScheduleDetailIdText.Visible = isTesting;
				lblScheduleDetailId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}
}