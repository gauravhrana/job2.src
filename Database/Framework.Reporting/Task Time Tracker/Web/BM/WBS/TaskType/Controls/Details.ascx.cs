using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.WBS.TaskType.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int taskTypeId)
        {
            base.ShowData(taskTypeId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TaskTypeDataModel();
            data.TaskTypeId = taskTypeId;

            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, taskTypeId, "TaskType");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTaskTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText, });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskTypeDataModel item)
        {
            SystemKeyId = item.TaskTypeId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TaskTypeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskType;

            PlaceHolderCore = dynTaskTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblTaskTypeId;
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
                lblTaskTypeIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}