using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolio.Controls
{
    public partial class Details : ControlDetails
    {

        #region private methods

        protected override void ShowData(int ProjectPortfolioId)
        {
            base.ShowData(ProjectPortfolioId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new ProjectPortfolioDataModel();
            data.ProjectPortfolioId = ProjectPortfolioId;

            var items = ProjectPortfolioDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblProjectPortfolioId.Text = item.ProjectPortfolioId.ToString();
                lblName.Text                = item.Name;
                lblDescription.Text         = item.Description;                
                lblSortOrder.Text           = item.SortOrder.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, ProjectPortfolioId, "ProjectPortfolio");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblProjectPortfolioIdText, lblNameText, lblDescriptionText, lblSortOrderText});
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            DictionaryLabel = CacheConstants.ProjectPortfolioLabelDictionary;
            PrimaryEntity = SystemEntity.ProjectPortfolio;

            base.OnInit(e);
       
            PlaceHolderCore = dynProjectPortfolioId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblProjectPortfolioIdText.Visible = isTesting;
                lblProjectPortfolioId.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}