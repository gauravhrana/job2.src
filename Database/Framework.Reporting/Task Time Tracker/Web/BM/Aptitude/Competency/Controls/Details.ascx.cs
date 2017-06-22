using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.Competency.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int competencyId)
        {
            base.ShowData(competencyId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new CompetencyDataModel();
            data.CompetencyId = competencyId;

            var items = CompetencyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, competencyId, "Competency");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblCompetencyIdText, lblNameText, lblDescriptionText, lblSortOrderText});
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new CompetencyDataModel();

            SetData(data);
        }

        public void SetData(CompetencyDataModel item)
        {
            SystemKeyId = item.CompetencyId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.CompetencyLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Competency;

            PlaceHolderCore = dynCompetencyId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblCompetencyId;
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
                lblCompetencyIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}