using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.WBS.TaskStatusType.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int taskStatusTypeId)
        {
            base.ShowData(taskStatusTypeId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TaskStatusTypeDataModel();
            data.TaskStatusTypeId = taskStatusTypeId;

            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, taskStatusTypeId, "TaskStatusType");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTaskStatusTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText, });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskStatusTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskStatusTypeDataModel item)
        {
            SystemKeyId = item.TaskStatusTypeId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TaskStatusTypeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskStatusType;

            PlaceHolderCore = dynTaskStatusTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblTaskStatusTypeId;
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
                lblTaskStatusTypeIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}