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

namespace Shared.UI.Web.TasksAndWorkflow.TaskEntityType.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int taskEntityTypeId)
        {
            base.ShowData(taskEntityTypeId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TaskEntityTypeDataModel();
            data.TaskEntityTypeId = taskEntityTypeId;

			var items = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, taskEntityTypeId, "TaskEntityType");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTaskEntityTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText, lblActiveText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskEntityTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskEntityTypeDataModel item)
        {
            SystemKeyId = item.TaskEntityTypeId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TaskEntityTypeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntityType;

                  
            PlaceHolderCore = dynTaskEntityTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblTaskEntityTypeId;
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
                lblTaskEntityTypeIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}