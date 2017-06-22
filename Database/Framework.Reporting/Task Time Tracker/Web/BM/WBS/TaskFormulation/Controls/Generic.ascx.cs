using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using DataModel.TaskTimeTracker.Task;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.WBS.TaskFormulation.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? TaskFormulationId
        {
            get
            {
                if (txtTaskFormulationId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTaskFormulationId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtTaskFormulationId.Text);
                }
            }
			set
			{
				txtTaskFormulationId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? ProjectId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtProjectId.Text.Trim());
                else
                    return int.Parse(drpProjectList.SelectedItem.Value);
            }
			set
			{
				txtProjectId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? FeatureId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFeature.Text.Trim());
                else
                    return int.Parse(drpFeatureList.SelectedItem.Value);
            }
			set
			{
				txtFeature.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? TaskId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTask.Text.Trim());
                else
                    return int.Parse(drpTaskList.SelectedItem.Value);
            }
			set
			{
				txtTask.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? SortOrder
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
            }

			set
			{
				txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
			}
        }       

        #endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new TaskFormulationDataModel();

			data.TaskFormulationId = TaskFormulationId;
			data.TaskId = TaskId;
			data.FeatureId = FeatureId;
			data.ProjectId = ProjectId;
			data.SortOrder = SortOrder;			

			if (action == "Insert")
			
			{
                var dtTaskFormulation = TaskTimeTracker.Components.BusinessLayer.Task.TaskFormulationDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtTaskFormulation.Rows.Count == 0)
				{
                    TaskTimeTracker.Components.BusinessLayer.Task.TaskFormulationDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskTimeTracker.Components.BusinessLayer.Task.TaskFormulationDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return TaskFormulationId;
		}

		public void LoadData(int taskFormulationId, bool showId)
		{

			Clear();

			var data = new TaskFormulationDataModel();
			data.TaskFormulationId = taskFormulationId;
            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskFormulationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);


			if (items.Count != 1) return;

			var item = items[0];

			TaskFormulationId = item.TaskFormulationId;
			TaskId = item.TaskId;
			FeatureId = item.FeatureId;
			ProjectId = item.ProjectId;
			SortOrder = item.SortOrder;			

			if (!showId)
			{
				txtTaskFormulationId.Text = item.TaskFormulationId.ToString();
				oHistoryList.Setup(PrimaryEntity, taskFormulationId, PrimaryEntityKey);
			}
			else
			{
				txtTaskFormulationId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskFormulationDataModel();

			TaskFormulationId = data.TaskFormulationId;
			TaskId = data.TaskId;
			FeatureId = data.FeatureId;
			ProjectId = data.ProjectId;
			SortOrder = data.SortOrder;
		}		

		#endregion		

        #region private methods
	   

        public override void SetId(int setId, bool chkTaskFormulationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskFormulationId);
            txtTaskFormulationId.Enabled = chkTaskFormulationId;
            //txtDescription.Enabled = !chkTaskFormulationId;
            //txtName.Enabled = !chkTaskFormulationId;
            //txtSortOrder.Enabled = !chkTaskFormulationId;
        }
        
        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
            var projectData = ProjectDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(projectData, drpProjectList, StandardDataModel.StandardDataColumns.Name, ProjectDataModel.DataColumns.ProjectId);
            var FeatureData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(FeatureData, drpFeatureList, StandardDataModel.StandardDataColumns.Name, FeatureDataModel.DataColumns.FeatureId);
            var TaskData = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(TaskData, drpTaskList, StandardDataModel.StandardDataColumns.Name, TaskDataModel.DataColumns.TaskId);

            if (isTesting)
            {
                drpProjectList.AutoPostBack = true;
                if (drpProjectList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtProjectId.Text.Trim()))
                    {
                        drpProjectList.SelectedValue = txtProjectId.Text;
                    }
                    else
                    {
                        txtProjectId.Text = drpProjectList.SelectedItem.Value;
                    }
                }
                txtProjectId.Visible = true;
                drpFeatureList.AutoPostBack = true;
                if (drpFeatureList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFeature.Text.Trim()))
                    {
                        drpFeatureList.SelectedValue = txtFeature.Text;
                    }
                    else
                    {
                        txtFeature.Text = drpFeatureList.SelectedItem.Value;
                    }
                }
                txtFeature.Visible = true;
                drpTaskList.AutoPostBack = true;
                if (drpTaskList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTask.Text.Trim()))
                    {
                        drpTaskList.SelectedValue = txtTask.Text;
                    }
                    else
                    {
                        txtTask.Text = drpTaskList.SelectedItem.Value;
                    }
                }
                txtTask.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFeature.Text.Trim()))
                {
                    drpFeatureList.SelectedValue = txtFeature.Text;
                }
                if (!string.IsNullOrEmpty(txtProjectId.Text.Trim()))
                {
                    drpProjectList.SelectedValue = txtProjectId.Text;
                }
                if (!string.IsNullOrEmpty(txtTask.Text.Trim()))
                {
                    drpTaskList.SelectedValue = txtTask.Text;
                }
            }

        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetupDropdown();
            var isTesting = SessionVariables.IsTesting;
            txtTaskFormulationId.Visible = isTesting;
            lblTaskFormulationId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskFormulation;
			PrimaryEntityKey = "TaskFormulation";
			FolderLocationFromRoot = "WBS/TaskFormulation";

			PlaceHolderCore = dynTaskFormulationId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

        protected void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProjectId.Text = drpProjectList.SelectedItem.Value;
        }
        protected void drpFeatureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFeature.Text = drpFeatureList.SelectedItem.Value;
        }
        protected void drpTaskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTask.Text = drpTaskList.SelectedItem.Value;
        }

        #endregion

    }
}