using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.EntityDateRangeState.Controls
{
	public partial class Details : ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int entityDateRangeStateId)
		{
			base.ShowData(entityDateRangeStateId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new EntityDateRangeStateDataModel();
			dataQuery.EntityDateRangeStateId = entityDateRangeStateId;

            var entityList = EntityDateRangeStateDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblEntityDateRangeStateId.Text       = entityItem.EntityDateRangeStateId.ToString();
					lblStartDate.Text                    = entityItem.StartDate.ToString();
					lblEndDate.Text                      = entityItem.EndDate.ToString();
					lblKeyId.Text                        = entityItem.KeyId.ToString();
					lblSystemEntityId.Text               = entityItem.SystemEntityId.ToString();
					lblEntityDateRangeStateType.Text     = entityItem.EntityDateRangeStateType.ToString();
					lblNotes.Text                        = entityItem.Notes.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)SystemEntity.EntityDateRangeState, entityDateRangeStateId, "EntityDateRangeState");

				}
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblEntityDateRangeStateIdText, lblStartDateText, lblEndDateText, lblKeyIdText, lblSystemEntityIdText, lblEntityDateRangeStateTypeText, lblNotesText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.EntityDateRangeStateLabelDictionary;
			PrimaryEntity = SystemEntity.EntityDateRangeState;

			PlaceHolderCore = dynEntityDateRangeStateId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblEntityDateRangeStateIdText.Visible = isTesting;
				lblEntityDateRangeStateId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
	}
}