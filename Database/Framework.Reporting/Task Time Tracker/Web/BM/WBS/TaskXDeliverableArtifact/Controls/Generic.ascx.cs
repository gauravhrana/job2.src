using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.Task;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact.Controls
{
    public partial class Generic : ControlGeneric
    {
        #region properties

        public int? TaskXDeliverableArtifactId
        {
            get
            {
                if (txtTaskXDeliverableArtifactId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtTaskXDeliverableArtifactId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtTaskXDeliverableArtifactId.Text);
                }
            }
        }

        public int? DeliverableArtifactsId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtDeliverableArtifactsId.Text.Trim());
                else
                    return int.Parse(drpDeliverableArtifactsList.SelectedItem.Value);
            }

        }

        public int? TaskId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTaskId.Text.Trim());
                else
                    return int.Parse(drpTaskList.SelectedItem.Value);
            }

        }

        public int? DeliverableArtifactStatusId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtDeliverableArtifactStatusId.Text.Trim());
                else
                    return int.Parse(drpDeliverableArtifactStatusList.SelectedItem.Value);
            }

        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/TaskXDeliverableArtifact/Controls/Validation.xml"); //"R:\TaskXDeliverableArtifacts\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkTaskXDeliverableArtifactId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskXDeliverableArtifactId);
            txtTaskXDeliverableArtifactId.Enabled = chkTaskXDeliverableArtifactId;
            //txtTaskId.Enabled = !chkTaskXDeliverableArtifactId;
            //txtDeliverableArtifactStatusId.Enabled = !chkTaskXDeliverableArtifactId;
            //txtDeliverableArtifactsId.Enabled = !chkTaskXDeliverableArtifactId;

            //drpDeliverableArtifactStatusList.Enabled = !chkTaskXDeliverableArtifactId;
            //drpTaskList.Enabled = !chkTaskXDeliverableArtifactId;
            //drpDeliverableArtifactsList.Enabled = !chkTaskXDeliverableArtifactId;
        }

        public void LoadData(int TaskXDeliverableArtifactId, bool showId)
        {
            var data = new TaskXDeliverableArtifactDataModel();
            data.TaskXDeliverableArtifactId = TaskXDeliverableArtifactId;
            var oTaskXDeliverableArtifactTable = TaskXDeliverableArtifactDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oTaskXDeliverableArtifactTable.Rows.Count == 1)
            {
                var row = oTaskXDeliverableArtifactTable.Rows[0];

                if (!showId)
                {
                    txtTaskXDeliverableArtifactId.Text = Convert.ToString(row[TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId]);

                    dynAuditHistory.Visible = true;

                    // only show Audit History in case of Update page, not for Clone.
                    oHistoryList.Setup((int)SystemEntity.TaskXDeliverableArtifact, TaskXDeliverableArtifactId, "TaskXDeliverableArtifact");
                    dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "TaskXDeliverableArtifact");

                }
                else
                {
                    txtTaskXDeliverableArtifactId.Text = String.Empty;
                }
                txtDeliverableArtifactsId.Text = Convert.ToString(row[TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId]);
                txtTaskId.Text = Convert.ToString(row[TaskXDeliverableArtifactDataModel.DataColumns.TaskId]);
                txtDeliverableArtifactStatusId.Text = Convert.ToString(row[TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId]);

                drpDeliverableArtifactStatusList.SelectedValue = Convert.ToString(row[TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId]);
                drpTaskList.SelectedValue = Convert.ToString(row[TaskXDeliverableArtifactDataModel.DataColumns.TaskId]);
                drpDeliverableArtifactsList.SelectedValue = Convert.ToString(row[TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId]);
            }
            else
            {
                txtTaskXDeliverableArtifactId.Text = String.Empty;
                txtTaskId.Text = String.Empty;
                txtDeliverableArtifactStatusId.Text = String.Empty;
                txtDeliverableArtifactsId.Text = String.Empty;

            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var taskPriorityTypeData = DeliverableArtifactDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskPriorityTypeData, drpDeliverableArtifactsList, StandardDataModel.StandardDataColumns.Name,
                DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);

            var taskData = TaskDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskData, drpTaskList, StandardDataModel.StandardDataColumns.Name,
                TaskDataModel.DataColumns.TaskId);

            //var DeliverableArtifactStatusData = Framework.Components.ApplicationUser.ApplicationUser.GetList(SessionVariables.RequestProfile.AuditId);
            //UIHelper.LoadDropDown(DeliverableArtifactStatusData, drpDeliverableArtifactStatusList, ApplicationUserDataModel.DataColumns.FirstName, ApplicationUserDataModel.DataColumns.ApplicationUserId);
            var DeliverableArtifactStatusData = DeliverableArtifactStatusDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(DeliverableArtifactStatusData, drpDeliverableArtifactStatusList, StandardDataModel.StandardDataColumns.Name, DeliverableArtifactStatusDataModel.DataColumns.DeliverableArtifactStatusId);

            if (isTesting)
            {
                drpTaskList.AutoPostBack = true;
                drpDeliverableArtifactsList.AutoPostBack = true;
                drpDeliverableArtifactStatusList.AutoPostBack = true;
                if (drpDeliverableArtifactsList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtDeliverableArtifactsId.Text.Trim()))
                    {
                        drpDeliverableArtifactsList.SelectedValue = txtDeliverableArtifactsId.Text;
                    }
                    else
                    {
                        txtDeliverableArtifactsId.Text = drpDeliverableArtifactsList.SelectedItem.Value;
                    }
                }
                if (drpTaskList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
                    {
                        drpTaskList.SelectedValue = txtTaskId.Text;
                    }
                    else
                    {
                        txtTaskId.Text = drpTaskList.SelectedItem.Value;
                    }
                }
                if (drpDeliverableArtifactStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtDeliverableArtifactStatusId.Text.Trim()))
                    {
                        drpDeliverableArtifactStatusList.SelectedValue = txtDeliverableArtifactStatusId.Text;
                    }
                    else
                    {
                        txtDeliverableArtifactStatusId.Text = drpDeliverableArtifactStatusList.SelectedItem.Value;
                    }
                }
                txtDeliverableArtifactsId.Visible = true;
                txtTaskId.Visible = true;
                txtDeliverableArtifactStatusId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtDeliverableArtifactsId.Text.Trim()))
                {
                    drpDeliverableArtifactsList.SelectedValue = txtDeliverableArtifactsId.Text;
                }
                if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
                {
                    drpTaskList.SelectedValue = txtTaskId.Text;
                }
                if (!string.IsNullOrEmpty(txtDeliverableArtifactStatusId.Text.Trim()))
                {
                    drpDeliverableArtifactStatusList.SelectedValue = txtDeliverableArtifactStatusId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtTaskXDeliverableArtifactId.Visible = isTesting;
                lblTaskXDeliverableArtifactId.Visible = isTesting;
                SetupDropdown();
            }
        }

        

        protected void drpDeliverableArtifactsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeliverableArtifactsId.Text = drpDeliverableArtifactsList.SelectedItem.Value;
        }

        protected void drpTaskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTaskId.Text = drpTaskList.SelectedItem.Value;
        }

        protected void drpDeliverableArtifactStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeliverableArtifactStatusId.Text = drpDeliverableArtifactStatusList.SelectedItem.Value;
        }

        #endregion
    }
}