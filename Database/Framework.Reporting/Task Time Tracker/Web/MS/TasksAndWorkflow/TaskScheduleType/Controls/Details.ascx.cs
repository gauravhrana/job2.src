using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;
using Framework.Components.TasksAndWorkflow;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.TasksAndWorkflow.TaskScheduleType.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int taskScheduleTypeId)
        {
            base.ShowData(taskScheduleTypeId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TaskScheduleTypeDataModel();
            data.TaskScheduleTypeId = taskScheduleTypeId;

			var items = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, taskScheduleTypeId, "TaskScheduleType");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTaskScheduleTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText, lblActiveText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskScheduleTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskScheduleTypeDataModel item)
        {
            SystemKeyId = item.TaskScheduleTypeId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TaskScheduleTypeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskScheduleType;


            PlaceHolderCore = dynTaskScheduleTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblTaskScheduleTypeId;
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
                lblTaskScheduleTypeIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}