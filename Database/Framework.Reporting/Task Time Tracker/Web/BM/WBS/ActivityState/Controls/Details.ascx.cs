using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.ActivityState.Controls
{
    public partial class Details : ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int activityStateId)
        {
            base.ShowData(activityStateId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new ActivityStateDataModel();
            data.ActivityStateId = activityStateId;

			var items = ActivityStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, activityStateId, "ActivityState");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblActivityStateIdText, lblNameText, lblDescriptionText, lblSortOrderText, });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ActivityStateDataModel();

            SetData(data);
        }

        public void SetData(ActivityStateDataModel item)
        {
            SystemKeyId = item.ActivityStateId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.ActivityStateLabelDictionary;
            PrimaryEntity = SystemEntity.ActivityState;

            PlaceHolderCore = dynActivityStateId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblActivityStateId;
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
                lblActivityStateIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}