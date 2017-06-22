using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
//using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Scheduling.VacationPlan.Controls
{
	public partial class Details : ControlDetails
	{

		#region private methods

		protected override void ShowData(int vacationPlanId)
		{
			base.ShowData(vacationPlanId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new VacationPlanDataModel();
			data.VacationPlanId = vacationPlanId;

            var items = VacationPlanDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);			

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];
				lblVacationPlanId.Text		= item.VacationPlanId.ToString();
				lblName.Text				= item.Name;
				lblDescription.Text			= item.Description;
				lblApplicationUserId.Text	= item.ApplicationUserId.ToString();
				lblStartDate.Text			= item.StartDate.ToString();
				lblEndDate.Text				= item.EndDate.ToString();
				lblCreatedDate.Text			= item.CreatedDate.ToString();
				lblModifiedDate.Text		= item.ModifiedDate.ToString();
				lblCreatedByAuditId.Text	= item.CreatedByAuditId.ToString();
				lblModifiedByAuditId.Text	= item.ModifiedByAuditId.ToString();
				lblSortOrder.Text			= item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, vacationPlanId, "VacationPlan");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblVacationPlanIdText, lblNameText, lblDescriptionText, lblApplicationUserIdText, lblStartDateText, lblEndDateText, lblCreatedDateText, lblModifiedDateText, lblCreatedByAuditIdText, lblModifiedByAuditIdText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.VacationPlanLabelDictionary;
			PrimaryEntity = SystemEntity.VacationPlan;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynVacationPlanId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblVacationPlanIdText.Visible = isTesting;
				lblVacationPlanId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}

}