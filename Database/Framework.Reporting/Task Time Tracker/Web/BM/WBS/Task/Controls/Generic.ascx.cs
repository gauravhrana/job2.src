using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.WBS.Task.Controls
{
    public partial class Generic : ControlGeneric

    {

        #region properties

        public int? TaskId
        {
            get
            {
                if (txtTaskId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtTaskId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtTaskId.Text);
                }
            }
			set
			{
				txtTaskId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? TaskTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTaskTypeId.Text.Trim());
                else
                    return int.Parse(drpTaskTypeList.SelectedItem.Value);
            }

			set
			{
				txtTaskTypeId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public string Name
        {
            get
            {
                return txtName.Text;
            }
			set
			{
				txtName.Text = value ?? String.Empty;
			}
        }

        public string Description
        {
            get
            {
                return DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
            }
			set
			{
				txtDescription.InnerText = value ?? String.Empty;
			}
        }

        public int? SortOrder
        {
            get
            {
                return DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
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
			var data = new TaskDataModel();

			data.TaskId			= TaskId;
			data.TaskTypeId		= TaskTypeId;
			data.Name			= Name;
			data.Description	= Description;
			data.SortOrder		= SortOrder;

			if (action == "Insert")
			{
                var dtTask = TaskDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtTask.Rows.Count == 0)
				{
                    TaskDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TaskID ?
			return TaskId;
		}

		public override void SetId(int setId, bool chkTaskId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskId);
			txtTaskId.Enabled = chkTaskId;
			//txtDescription.Enabled = !chkTaskId;
			//txtName.Enabled = !chkTaskId;
			//txtSortOrder.Enabled = !chkTaskId;
		}

		public void LoadData(int taskId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new TaskDataModel();
			data.TaskId = taskId;

			// get data
            var items = TaskDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			
			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			TaskId		= item.TaskId;
			TaskTypeId	= item.TaskTypeId;
			Name		= item.Name;
			Description = item.Description;
			SortOrder	= item.SortOrder;

			if (!showId)
			{
				txtTaskId.Text = item.TaskId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, taskId, PrimaryEntityKey);
			}
			else
			{
				txtTaskId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskDataModel();

			TaskId		= data.TaskId;
			TaskId		= data.TaskTypeId;
			Description = data.Description;
			Name		= data.Name;
			SortOrder	= data.SortOrder;
		}		

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
            var taskTypeData = TaskTypeDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskTypeData, drpTaskTypeList, StandardDataModel.StandardDataColumns.Name, TaskTypeDataModel.DataColumns.TaskTypeId);

            if (isTesting)
            {
                drpTaskTypeList.AutoPostBack = true;
                if (drpTaskTypeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskTypeId.Text.Trim()))
                    {
                        drpTaskTypeList.SelectedValue = txtTaskTypeId.Text;
                    }
                    else
                    {
                        txtTaskTypeId.Text = drpTaskTypeList.SelectedItem.Value;
                    }
                }
            	txtTaskTypeId.Visible = true;
            }
            else
            {
				if (!string.IsNullOrEmpty(txtTaskTypeId.Text.Trim()))
				{
					drpTaskTypeList.SelectedValue = txtTaskTypeId.Text;
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
                txtTaskId.Visible = isTesting;
                lblTaskId.Visible = isTesting;
                SetupDropdown();
            }
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "Task";
			FolderLocationFromRoot = "WBS/Task";
			PrimaryEntity = SystemEntity.Task;

			// set object variable reference            
			PlaceHolderCore = dynTaskId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpTaskTypeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTaskTypeId.Text = drpTaskTypeList.SelectedItem.Value;
		}
		#endregion
	}
}