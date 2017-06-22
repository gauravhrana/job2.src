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

namespace ApplicationContainer.UI.Web.WBS.TaskPackage.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int taskPackageId)
        {
            base.ShowData(taskPackageId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TaskPackageDataModel();
            data.TaskPackageId = taskPackageId;

            var items = TaskPackageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, taskPackageId, "TaskPackage");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTaskPackageIdText, lblNameText, lblDescriptionText, lblSortOrderText, });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskPackageDataModel();

            SetData(data);
        }

        public void SetData(TaskPackageDataModel item)
        {
            SystemKeyId = item.TaskPackageId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TaskPackageLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPackage;

            PlaceHolderCore = dynTaskPackageId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblTaskPackageId;
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
                lblTaskPackageIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}