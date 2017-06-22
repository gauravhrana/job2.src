using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;

namespace Shared.UI.Web.TasksAndWorkflow.TaskSchedule.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

		#region properties


		public int? TaskScheduleId
		{
			get
			{
				if (txtTaskScheduleId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTaskScheduleId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTaskScheduleId.Text);
				}
			}
			set
			{
				txtTaskScheduleId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? TaskScheduleTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskScheduleTypeId.Text.Trim());
				else
					return int.Parse(drpTaskScheduleTypeList.SelectedValue);
			}
			set
			{
				txtTaskScheduleTypeId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		public int? TaskEntityId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskEntityId.Text.Trim());
				else
					return int.Parse(drpTaskEntityList.SelectedItem.Value);
			}
			set
			{
				txtTaskEntityId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new TaskScheduleDataModel();

			data.TaskScheduleId = TaskScheduleId;
			data.TaskEntityId	= TaskEntityId;
			data.TaskScheduleTypeId = TaskScheduleTypeId;
			
			if (action == "Insert")
			{
				if(!Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TaskScheduleID ?
			return TaskScheduleId;
		}		

		public void LoadData(int taskScheduleId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new TaskScheduleDataModel();
			dataQuery.TaskScheduleId = taskScheduleId;

			var items = Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			TaskScheduleId = item.TaskScheduleId;
			TaskEntityId = item.TaskEntityId;
			TaskScheduleTypeId = item.TaskScheduleTypeId;			

			if (!showId)
			{
				txtTaskScheduleId.Text = item.TaskScheduleId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TaskSchedule, taskScheduleId, "TaskSchedule");

			}
			else
			{
				txtTaskScheduleId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new TaskScheduleDataModel();

			TaskScheduleId = data.TaskScheduleId;
			TaskEntityId = data.TaskEntityId;
			TaskScheduleTypeId = data.TaskScheduleTypeId;
			
		}

		#endregion        

        #region private methods        

        public override void SetId(int setId, bool chkTaskScheduleId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskScheduleId);
            txtTaskScheduleId.Enabled = chkTaskScheduleId;
            //txtTaskEntityId.Enabled = !chkTaskScheduleId;
            //txtTaskScheduleTypeId.Enabled = !chkTaskScheduleId;

            //drpTaskEntityList.Enabled = !chkTaskScheduleId;
            //drpTaskScheduleTypeList.Enabled = !chkTaskScheduleId;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var taskScheduleTypeData = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskScheduleTypeData, drpTaskScheduleTypeList, StandardDataModel.StandardDataColumns.Name, 
                TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId);

			var taskEntityData = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskEntityData, drpTaskEntityList, StandardDataModel.StandardDataColumns.Name,
                TaskEntityDataModel.DataColumns.TaskEntityId);

            if (isTesting)
            {
                drpTaskEntityList.AutoPostBack = true;
                drpTaskScheduleTypeList.AutoPostBack = true;
                if (drpTaskScheduleTypeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskScheduleTypeId.Text.Trim()))
                    {
                        drpTaskScheduleTypeList.SelectedValue = txtTaskScheduleTypeId.Text;
                    }
                    else
                    {
                        txtTaskScheduleTypeId.Text = drpTaskScheduleTypeList.SelectedItem.Value;
                    }
                }
                if (drpTaskEntityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskEntityId.Text.Trim()))
                    {
                        drpTaskEntityList.SelectedValue = txtTaskEntityId.Text;
                    }
                    else
                    {
                        txtTaskEntityId.Text = drpTaskEntityList.SelectedItem.Value;
                    }
                }
                txtTaskScheduleTypeId.Visible = true;
                txtTaskEntityId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTaskScheduleTypeId.Text.Trim()))
                {
                    drpTaskScheduleTypeList.SelectedValue = txtTaskScheduleTypeId.Text;
                }
                if (!string.IsNullOrEmpty(txtTaskEntityId.Text.Trim()))
                {
                    drpTaskEntityList.SelectedValue = txtTaskEntityId.Text;
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
                txtTaskScheduleId.Visible = isTesting;
                lblTaskScheduleId.Visible = isTesting;
                SetupDropdown();
            }
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey				= "TaskSchedule";
			FolderLocationFromRoot			= "TasksAndWorkFlow/TaskSchedule";
			PrimaryEntity					= Framework.Components.DataAccess.SystemEntity.TaskSchedule;

			// set object variable reference            
			PlaceHolderCore					= dynTaskScheduleId;
			PlaceHolderAuditHistory			= dynAuditHistory;
			BorderDiv						= borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

        protected void drpTaskScheduleTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTaskScheduleTypeId.Text = drpTaskScheduleTypeList.SelectedItem.Value;
        }

        protected void drpTaskEntityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTaskEntityId.Text = drpTaskEntityList.SelectedItem.Value;
        }

        #endregion

    }
}