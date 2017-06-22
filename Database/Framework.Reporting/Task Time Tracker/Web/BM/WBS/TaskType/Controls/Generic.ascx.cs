using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.WBS.TaskType.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new TaskTypeDataModel();

            data.TaskTypeId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtTaskType = TaskTimeTracker.Components.BusinessLayer.Task.TaskTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTaskType.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.Task.TaskTypeDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Task.TaskTypeDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TaskTypeId;
        }

        public override void SetId(int setId, bool chkTaskTypeId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskTypeId);
            CoreSystemKey.Enabled = chkTaskTypeId;
            //txtDescription.Enabled = !chkTaskTypeId;
            //txtName.Enabled = !chkTaskTypeId;
            //txtSortOrder.Enabled = !chkTaskTypeId;
        }

        public void LoadData(int taskTypeId, bool showId)
        {
            Clear();

            var data = new TaskTypeDataModel();
			data.TaskTypeId = taskTypeId;

            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TaskTypeId;
                oHistoryList.Setup(PrimaryEntity, taskTypeId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskTypeDataModel data)
        {
            SystemKeyId = data.TaskTypeId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTaskTypeId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskType;
            PrimaryEntityKey = "TaskType";
            FolderLocationFromRoot = "TaskType";

            PlaceHolderCore = dynTaskTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTaskTypeId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}