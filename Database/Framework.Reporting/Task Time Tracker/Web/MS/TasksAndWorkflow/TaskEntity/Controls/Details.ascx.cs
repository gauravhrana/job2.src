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

namespace Shared.UI.Web.TasksAndWorkflow.TaskEntity.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int taskEntityId)
        {
            base.ShowData(taskEntityId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TaskEntityDataModel();
            data.TaskEntityId = taskEntityId;

			var items = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

				lblActive.Text = item.Active.ToString();
				lblTaskEntityType.Text = item.TaskEntityTypeId.ToString();
                oHistoryList.Setup(PrimaryEntity, taskEntityId, "TaskEntity");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTaskEntityIdText, lblNameText, lblDescriptionText, lblSortOrderText, lblActiveText, lblTaskEntityTypeText});
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskEntityDataModel();

            SetData(data);
        }

        public void SetData(TaskEntityDataModel item)
        {
            SystemKeyId = item.TaskEntityId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TaskEntityLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntity;


            PlaceHolderCore = dynTaskEntityId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblTaskEntityId;
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
                lblTaskEntityIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}