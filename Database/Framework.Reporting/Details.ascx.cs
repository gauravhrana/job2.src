using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.UI.Web.CommonCode;

namespace TaskTimeTracker.UI.Web.Aptitude.Competency.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region private methods

        protected override void ShowData(int competencyId)
        {
            base.ShowData(competencyId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new Components.Module.Competency.Competency.Data();
            data.CompetencyId = competencyId;

            var items = Components.Module.Competency.Competency.GetEntityDetails(data, AuditId);

            if (items.Count == 1)
            {
                var item = items[0];

                lblCompetencyId.Text     = item.CompetencyId.ToString();
                lblName.Text             = item.Name;
                lblDescription.Text      = item.Description;
                lblSortOrder.Text        = item.SortOrder.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, competencyId, "Competency");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblCompetencyIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            DictionaryLabel = CacheConstants.CompetencyLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Competency;

            base.OnInit(e);
        
            PlaceHolderCore = dynCompetencyId;
            PlaceHolderAuditHistory = dynAuditHistory;
            MainTable = tblMain1;
            BorderDiv = borderdiv;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblCompetencyIdText.Visible = isTesting;
                lblCompetencyId.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}