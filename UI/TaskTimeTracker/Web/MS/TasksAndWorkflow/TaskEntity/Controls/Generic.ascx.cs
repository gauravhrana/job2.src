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
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.TasksAndWorkflow.TaskEntity.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {
		#region properties


		public int? TaskEntityId
		{
			get
			{
				if (txtTaskEntityId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTaskEntityId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTaskEntityId.Text);
				}
			}
			set
			{
				txtTaskEntityId.Text = (value == null) ? String.Empty : value.ToString();
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
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
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
				return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
			}
			set
			{
				txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? Active
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtActive.Text);
			}
			set
			{
				txtActive.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? TaskEntityTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskEntityTypeId.Text.Trim());
				else
					return int.Parse(drpTaskEntityTypeList.SelectedItem.Value);

			}
			set
			{
				txtTaskEntityTypeId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}


		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new TaskEntityDataModel();

			data.TaskEntityId		= TaskEntityId;
			data.Name				= Name;
			data.Description		= Description;
			data.SortOrder			= SortOrder;
			data.Active				= Active;
			data.TaskEntityTypeId	= TaskEntityTypeId;

			if (action == "Insert")
			{
				if(!Framework.Components.TasksAndWorkflow.TaskEntityDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.TasksAndWorkflow.TaskEntityDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.TasksAndWorkflow.TaskEntityDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TaskEntityID ?
			return TaskEntityId;
		}

		public override void SetId(int setId, bool chkTaskEntityId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskEntityId);
			txtTaskEntityId.Enabled = chkTaskEntityId;
			//txtDescription.Enabled = !chkTaskEntityId;
			//txtName.Enabled = !chkTaskEntityId;
			//txtSortOrder.Enabled = !chkTaskEntityId;
		}

		public void LoadData(int taskEntityId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new TaskEntityDataModel();
			dataQuery.TaskEntityId = taskEntityId;

			var items = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			TaskEntityId	= item.TaskEntityId;
			Name			= item.Name;
			Description		= item.Description;
			SortOrder		= item.SortOrder;
			Active			= item.Active;
			TaskEntityTypeId = item.TaskEntityTypeId;

			drpTaskEntityTypeList.SelectedValue = item.TaskEntityTypeId.ToString(); 

			if (!showId)
			{
				txtTaskEntityId.Text = item.TaskEntityId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TaskEntity, taskEntityId, "TaskEntity");

			}
			else
			{
				txtTaskEntityId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new TaskEntityDataModel();

			TaskEntityId		= data.TaskEntityId;
			Description			= data.Description;
			Name				= data.Name;
			SortOrder			= data.SortOrder;
			Active				= data.Active;
			TaskEntityTypeId	= data.TaskEntityTypeId;
		}


		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtTaskEntityId.Visible = isTesting;
			lblTaskEntityId.Visible = isTesting;
			if (!IsPostBack)
			{
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "TaskEntity";
			FolderLocationFromRoot = "TasksAndWorkFlow/TaskEntity";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntity;

			// set object variable reference            
			PlaceHolderCore = dynTaskEntityId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion        

        #region private methods     
      
        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var taskEntityTypeData = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskEntityTypeData, drpTaskEntityTypeList, StandardDataModel.StandardDataColumns.Name, 
                TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId);

            if (isTesting)
            {
                drpTaskEntityTypeList.AutoPostBack = true;
                if (drpTaskEntityTypeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskEntityTypeId.Text.Trim()))
                    {
                        drpTaskEntityTypeList.SelectedValue = txtTaskEntityTypeId.Text;
                    }
                    else
                    {
                        txtTaskEntityTypeId.Text = drpTaskEntityTypeList.SelectedItem.Value;
                    }
                }
                txtTaskEntityTypeId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTaskEntityTypeId.Text.Trim()))
                {
                    drpTaskEntityTypeList.SelectedValue = txtTaskEntityTypeId.Text;
                }
            }
        }

        #endregion

        #region Events       

        protected void drpTaskEntityTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTaskEntityTypeId.Text = drpTaskEntityTypeList.SelectedItem.Value;
        }

        #endregion

    }
}