using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Priority;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.WBS.TaskPackage.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new TaskPackageDataModel();

            data.TaskPackageId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtTaskPackage = TaskPackageDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTaskPackage.Rows.Count == 0)
                {
                    TaskPackageDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskPackageDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TaskPackageId;
        }

        public override void SetId(int setId, bool chkTaskPackageId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskPackageId);
            CoreSystemKey.Enabled = chkTaskPackageId;
            //txtDescription.Enabled = !chkTaskPackageId;
            //txtName.Enabled = !chkTaskPackageId;
            //txtSortOrder.Enabled = !chkTaskPackageId;
        }

        public void LoadData(int taskPackageId, bool showId)
        {
            Clear();

            var data = new TaskPackageDataModel();
			data.TaskPackageId = taskPackageId;

            var items = TaskPackageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TaskPackageId;
                oHistoryList.Setup(PrimaryEntity, taskPackageId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskPackageDataModel();

            SetData(data);
        }

        public void SetData(TaskPackageDataModel data)
        {
            SystemKeyId = data.TaskPackageId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTaskPackageId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPackage;
            PrimaryEntityKey = "TaskPackage";
            FolderLocationFromRoot = "TaskPackage";

            PlaceHolderCore = dynTaskPackageId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTaskPackageId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}