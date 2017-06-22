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

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolioGroupXProjectPortfolio.Controls
{
	public partial class Details : ControlDetails
	{

		#region private methods

		protected override void ShowData(int projectPortfolioGroupXProjectPortfolioId)
		{
			base.ShowData(projectPortfolioGroupXProjectPortfolioId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ProjectPortfolioGroupXProjectPortfolioDataModel();
			data.ProjectPortfolioGroupXProjectPortfolioId = projectPortfolioGroupXProjectPortfolioId;

            var items = ProjectPortfolioGroupXProjectPortfolioDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];
				lblProjectPortfolioGroupXProjectPortfolioId.Text = item.ProjectPortfolioGroupXProjectPortfolioId.ToString();
				lblProjectPortfolioGroupId.Text					 = item.ProjectPortfolioGroupId.ToString();
				lblProjectPortfolioId.Text						 = item.ProjectPortfolioId.ToString();
				lblDescription.Text								 = item.Description;
				lblCreatedDate.Text								 = item.CreatedDate.ToString();
				lblModifiedDate.Text							 = item.ModifiedDate.ToString();
				lblCreatedByAuditId.Text						 = item.CreatedByAuditId.ToString();
				lblModifiedByAuditId.Text						 = item.ModifiedByAuditId.ToString();
				lblSortOrder.Text								 = item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, projectPortfolioGroupXProjectPortfolioId, "ProjectPortfolioGroupXProjectPortfolio");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblProjectPortfolioGroupXProjectPortfolioIdText,lblProjectPortfolioGroupIdText,lblProjectPortfolioIdText, lblDescriptionText, lblCreatedDateText, lblModifiedDateText, lblCreatedByAuditIdText, lblModifiedByAuditIdText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ProjectPortfolioGroupXProjectPortfolioLabelDictionary;
			PrimaryEntity = SystemEntity.ProjectPortfolioGroupXProjectPortfolio;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynProjectPortfolioGroupXProjectPortfolioId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblProjectPortfolioGroupXProjectPortfolioIdText.Visible = isTesting;
				lblProjectPortfolioGroupXProjectPortfolioId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}

}