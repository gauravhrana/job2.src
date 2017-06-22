using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Competency;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;
using System.Data;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.WBS.TaskXCompetency.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric

	{

		#region properties		

		public int? TaskXCompetencyId
		{
			get
			{
				if (txtTaskXCompetencyId.Enabled)
				{
					// review
					//return -1;//Framework.Components.DefaultDataRules.CheckAndGetApplicationId(txtTaskPackageXOwnerXTaskId.Text);
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTaskXCompetencyId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTaskXCompetencyId.Text);
				}
			}
			set
			{
				txtTaskXCompetencyId.Text = (value == null) ? String.Empty : value.ToString();
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
			set
			{
				txtTaskId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? CompetencyId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtCompetencyId.Text.Trim());
				else
					return int.Parse(drpCompetencyList.SelectedItem.Value);
			}
			set
			{
				txtCompetencyId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}
		

		#endregion properties

		public override int? Save(string action)
		{
			var data = new TaskXCompetencyDataModel();

			data.TaskXCompetencyId			= TaskXCompetencyId;
			data.TaskId						= TaskId;
			data.CompetencyId				= CompetencyId;
			
			if (action == "Insert")
			{
                TaskTimeTracker.Components.Module.Competency.TaskXCompetencyDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
                TaskTimeTracker.Components.Module.Competency.TaskXCompetencyDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TaskXCompetencyID ?
			return TaskXCompetencyId;
		}
	
		public override void SetId(int setId, bool chkTaskXCompetencyId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskXCompetencyId);
			txtTaskId.Enabled = chkTaskXCompetencyId;
			//txtName.Enabled = !chkTaskId;
			//txtDescription.Enabled = !chkTaskId;
			//txtSortOrder.Enabled = !chkTaskId;
			//txtCompetencyId.Enabled = !chkTaskId;
			//drpCompetencyList.Enabled = !chkTaskId;
		}

		public void LoadData(int taskXCompetencyId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new TaskXCompetencyDataModel();
			data.TaskXCompetencyId = taskXCompetencyId;

			// get data
            var items = TaskTimeTracker.Components.Module.Competency.TaskXCompetencyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			TaskXCompetencyId		= item.TaskXCompetencyId;
			TaskId					= item.TaskId;
			CompetencyId			= item.CompetencyId;
			
			if (!showId)
			{
				txtTaskXCompetencyId.Text = item.TaskXCompetencyId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, taskXCompetencyId, PrimaryEntityKey);
			}
			else
			{
				txtTaskXCompetencyId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskXCompetencyDataModel();

			TaskXCompetencyId	= data.TaskXCompetencyId;
			TaskId				= data.TaskId;
			CompetencyId		= data.CompetencyId;		
			
		}

		
		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
            var CompetencyData = CompetencyDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(CompetencyData, drpCompetencyList, StandardDataModel.StandardDataColumns.Name, CompetencyDataModel.DataColumns.CompetencyId);

            var taskData = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskData, drpTaskList, StandardDataModel.StandardDataColumns.Name, TaskDataModel.DataColumns.TaskId);

			if (isTesting)
			{
				drpCompetencyList.AutoPostBack = true;
				drpTaskList.AutoPostBack = true;

				if (drpCompetencyList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtCompetencyId.Text.Trim()))
					{
						drpCompetencyList.SelectedValue = txtCompetencyId.Text;
					}
					else
					{
						txtCompetencyId.Text = drpCompetencyList.SelectedItem.Value;
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
				txtCompetencyId.Visible = true;
				txtTaskId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtCompetencyId.Text.Trim()))
				{
					drpCompetencyList.SelectedValue = txtCompetencyId.Text;
				}
				if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
				{
					drpTaskList.SelectedValue = txtTaskId.Text;
				}
			}
		}


		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtTaskXCompetencyId.Visible = isTesting;
				lblTaskXCompetencyId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskXCompetency;
			PrimaryEntityKey = "TaskXCompetency";
			FolderLocationFromRoot = "TaskXCompetency";

			// set object variable reference            
			PlaceHolderCore = dynTaskXCompetencyId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}		

        #region Events       

        protected void drpCompetencyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCompetencyId.Text = drpCompetencyList.SelectedItem.Value;
        }

        protected void drpTaskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTaskId.Text = drpTaskList.SelectedItem.Value;
        }
		
        #endregion

    }
}