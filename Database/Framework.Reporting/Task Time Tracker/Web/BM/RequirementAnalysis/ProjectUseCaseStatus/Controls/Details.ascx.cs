using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.ProjectUseCaseStatus.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{

		#region private methods

		protected override void ShowData(int projectUseCaseStatusId)
		{
			base.ShowData(projectUseCaseStatusId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ProjectUseCaseStatusDataModel();
			data.ProjectUseCaseStatusId = projectUseCaseStatusId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

                lblCreatedByAuditId.Text = item.CreatedByAuditId.ToString();
                lblModifiedByAuditId.Text = item.ModifiedByAuditId.ToString();
                lblCreatedDate.Text = item.CreatedDate.Value.ToString(SessionVariables.UserDateFormat);
                lblModifiedDate.Text = item.ModifiedDate.Value.ToString(SessionVariables.UserDateFormat);

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, projectUseCaseStatusId, "ProjectUseCaseStatus");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
                LabelListCore = new List<Label>(new Label[] { lblProjectUseCaseStatusIdText, lblNameText, lblDescriptionText, lblSortOrderText,
                lblCreatedByAuditIdText, lblCreatedDateText, lblModifiedDateText,  lblModifiedByAuditIdText });
            }

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ProjectUseCaseStatusDataModel();

			SetData(data);
		}

		public void SetData(ProjectUseCaseStatusDataModel item)
		{
			SystemKeyId = item.ProjectUseCaseStatusId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.ProjectUseCaseStatusLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectUseCaseStatus;

			PlaceHolderCore = dynProjectUseCaseStatusId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblProjectUseCaseStatusId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblProjectUseCaseStatusIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}
}