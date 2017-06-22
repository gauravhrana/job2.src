using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.TasksAndWorkflow.TaskEntityType.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {
        #region properties

        public int? Active
        {
            get
            {
                return int.Parse(txtActive.Text.Trim());

            }
            set
            {
                txtActive.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        #endregion

        #region methods

        public override int? Save(string action)
        {
            var data = new TaskEntityTypeDataModel();

            data.TaskEntityTypeId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
            data.Active = Active;

            if (action == "Insert")
            {
				if(!Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
					Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TaskEntityTypeId;
        }

        public override void SetId(int setId, bool chkTaskEntityTypeId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskEntityTypeId);
            CoreSystemKey.Enabled = chkTaskEntityTypeId;
            //txtDescription.Enabled = !chkTaskEntityTypeId;
            //txtName.Enabled = !chkTaskEntityTypeId;
            //txtSortOrder.Enabled = !chkTaskEntityTypeId;
        }

        public void LoadData(int taskEntityTypeId, bool showId)
        {
            Clear();

            var data = new TaskEntityTypeDataModel();
            data.TaskEntityTypeId = taskEntityTypeId;

			var items = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TaskEntityTypeId;
                Active = item.Active;
                oHistoryList.Setup(PrimaryEntity, taskEntityTypeId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskEntityTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskEntityTypeDataModel data)
        {
            SystemKeyId = data.TaskEntityTypeId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTaskEntityTypeId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntityType;
            PrimaryEntityKey = "TaskEntityType";
            FolderLocationFromRoot = "TaskEntityType";

            PlaceHolderCore = dynTaskEntityTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTaskEntityTypeId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}