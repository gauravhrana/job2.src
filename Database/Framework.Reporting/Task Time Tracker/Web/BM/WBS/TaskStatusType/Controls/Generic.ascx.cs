using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.WBS.TaskStatusType.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new TaskStatusTypeDataModel();

            data.TaskStatusTypeId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtTaskStatusType = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTaskStatusType.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TaskStatusTypeId;
        }

        public override void SetId(int setId, bool chkTaskStatusTypeId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskStatusTypeId);
            CoreSystemKey.Enabled = chkTaskStatusTypeId;
            //txtDescription.Enabled = !chkTaskStatusTypeId;
            //txtName.Enabled = !chkTaskStatusTypeId;
            //txtSortOrder.Enabled = !chkTaskStatusTypeId;
        }

        public void LoadData(int taskStatusTypeId, bool showId)
        {
            Clear();

            var data = new TaskStatusTypeDataModel();
			data.TaskStatusTypeId = taskStatusTypeId;

            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TaskStatusTypeId;
                oHistoryList.Setup(PrimaryEntity, taskStatusTypeId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskStatusTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskStatusTypeDataModel data)
        {
            SystemKeyId = data.TaskStatusTypeId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTaskStatusTypeId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskStatusType;
            PrimaryEntityKey = "TaskStatusType";
            FolderLocationFromRoot = "TaskStatusType";

            PlaceHolderCore = dynTaskStatusTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTaskStatusTypeId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}