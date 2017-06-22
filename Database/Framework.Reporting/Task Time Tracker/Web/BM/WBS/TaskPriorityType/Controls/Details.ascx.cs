using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Priority;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.WBS.TaskPriorityType.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int taskPriorityTypeId)
        {
            base.ShowData(taskPriorityTypeId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

			var data = new TaskPriorityTypeDataModel();
            data.TaskPriorityTypeId = taskPriorityTypeId;

            var items = TaskPriorityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

				lblWeight.Text = item.Weight.ToString();

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, taskPriorityTypeId, "TaskPriorityType");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTaskPriorityTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText, lblWeightText});
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

			var data = new TaskPriorityTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskPriorityTypeDataModel item)
        {
            SystemKeyId = item.TaskPriorityTypeId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TaskPriorityTypeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPriorityType;

            PlaceHolderCore = dynTaskPriorityTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblTaskPriorityTypeId;
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
                lblTaskPriorityTypeIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}