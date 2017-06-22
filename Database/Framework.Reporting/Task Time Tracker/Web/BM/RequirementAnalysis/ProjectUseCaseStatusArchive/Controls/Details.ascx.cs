using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer.DataModel.RequirementAnalysis;
using TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.ProjectUseCaseStatusArchive.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int projectUseCaseStatusArchiveId)
		{
			base.ShowData(projectUseCaseStatusArchiveId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ProjectUseCaseStatusArchiveDataModel();
			data.ProjectUseCaseStatusArchiveId = projectUseCaseStatusArchiveId;

			var items = ProjectUseCaseStatusArchiveDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblProjectUseCaseStatusArchiveId.Text = item.ProjectUseCaseStatusArchiveId.ToString();
				lblProject.Text = item.Project;
				lblUseCase.Text = item.UseCase;
				lblProjectUseCaseStatus.Text = item.ProjectUseCaseStatus;
				lblAcknowledgedBy.Text = item.AcknowledgedBy;
				lblAcknowledgedById.Text = item.AcknowledgedById.ToString();
				//lblKnowledgeDate.Text = item.KnowledgeDate.ToString();
				lblMemo.Text = item.Memo;
				lblProjectUseCaseStatusId.Text = item.ProjectUseCaseStatusId.ToString();
				//oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, projectUseCaseStatusArchiveId, "ProjectUseCaseStatus");
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblProjectUseCaseStatusIdText, lblProjectUseCaseStatusArchiveIdText, lblUseCaseText, lblProjectText, lblProjectUseCaseStatusText });
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
				lblProjectUseCaseStatusArchiveIdText.Visible = isTesting;
				lblProjectUseCaseStatusArchiveId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			DictionaryLabel = CacheConstants.ProjectUseCaseStatusArchiveLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectUseCaseStatusArchive;

			base.OnInit(e);           
			PlaceHolderCore = dynProjectUseCaseStatusArchiveId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion
	}
}