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
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer.Task;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TaskNote.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new TaskNoteDataModel();

            data.TaskNoteId      = SystemKeyId;
            data.Name            = Name;
            data.Description     = Description;
            data.SortOrder       = SortOrder;

            if (action == "Insert")
            {
                var dtTaskNote = TaskNoteDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTaskNote.Rows.Count == 0)
                {
                    TaskNoteDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskNoteDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TaskNoteId;
        }

        public override void SetId(int setId, bool chkTaskNoteId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskNoteId);
            CoreSystemKey.Enabled = chkTaskNoteId;
            //txtDescription.Enabled = !chkTaskNoteId;
            //txtName.Enabled = !chkTaskNoteId;
            //txtSortOrder.Enabled = !chkTaskNoteId;
        }

        public void LoadData(int taskNoteId, bool showId)
        {
            Clear();

            var data = new TaskNoteDataModel();
            data.TaskNoteId = taskNoteId;

            var items = TaskNoteDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TaskNoteId;
                oHistoryList.Setup(PrimaryEntity, taskNoteId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskNoteDataModel();

            SetData(data);
        }

        public void SetData(TaskNoteDataModel data)
        {
            SystemKeyId = data.TaskNoteId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTaskNoteId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.TaskNote;
            PrimaryEntityKey = "TaskNote";
            FolderLocationFromRoot = "TaskNote";

            PlaceHolderCore = dynTaskNoteId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTaskNoteId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}