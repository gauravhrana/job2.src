using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.Need.Controls
{
    public partial class Details : ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int needId)
        {
            base.ShowData(needId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new NeedDataModel();
            data.NeedId = needId;

            var items = NeedDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, needId, "Need");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblNeedIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new NeedDataModel();

            SetData(data);
        }

        public void SetData(NeedDataModel item)
        {
            SystemKeyId = item.NeedId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.NeedLabelDictionary;
            PrimaryEntity = SystemEntity.Need;

            PlaceHolderCore = dynNeedId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblNeedId;
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
                lblNeedIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}