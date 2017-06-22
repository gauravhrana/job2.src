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

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolioGroup.Controls
{
    public partial class Details : ControlDetails
    {

        #region private methods

        protected override void ShowData(int projectPortfolioGroupId)
        {
            base.ShowData(projectPortfolioGroupId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new ProjectPortfolioGroupDataModel();
            data.ProjectPortfolioGroupId = projectPortfolioGroupId;

            var items = ProjectPortfolioGroupDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match 
            if (items.Count == 1)
            {
                var item = items[0];
                lblProjectPortfolioGroupId.Text = item.ProjectPortfolioGroupId.ToString();
                //lblApplicationId.Text = item.ApplicationId.ToString();
                lblName.Text             = item.Name;
                lblDescription.Text      = item.Description;               
                lblSortOrder.Text = item.SortOrder.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, projectPortfolioGroupId, "ProjectPortfolioGroup");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblProjectPortfolioGroupIdText,  lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.ProjectPortfolioGroupLabelDictionary;
            PrimaryEntity = SystemEntity.ProjectPortfolioGroup;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynProjectPortfolioGroupId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblProjectPortfolioGroupIdText.Visible = isTesting;
                lblProjectPortfolioGroupId.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}