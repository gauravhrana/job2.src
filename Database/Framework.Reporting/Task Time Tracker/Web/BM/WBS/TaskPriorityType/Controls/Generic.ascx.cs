using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Priority;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.WBS.TaskPriorityType.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{
		#region properties

		public decimal? Weight
		{
			get
			{		
					return decimal.Parse(txtWeight.Text.Trim());			
			}
			set
			{
				txtWeight.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion properties
		
		#region methods

		public override int? Save(string action)
        {
			var data = new TaskPriorityTypeDataModel();

            data.TaskPriorityTypeId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
			data.Weight = Weight;

            if (action == "Insert")
            {
                var dtTaskPriorityType = TaskPriorityTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTaskPriorityType.Rows.Count == 0)
                {
                    TaskPriorityTypeDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskPriorityTypeDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TaskPriorityTypeId;
        }

        public override void SetId(int setId, bool chkTaskPriorityTypeId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskPriorityTypeId);
            CoreSystemKey.Enabled = chkTaskPriorityTypeId;
            //txtDescription.Enabled = !chkTaskPriorityTypeId;
            //txtName.Enabled = !chkTaskPriorityTypeId;
            //txtSortOrder.Enabled = !chkTaskPriorityTypeId;
        }

        public void LoadData(int taskPriorityTypeId, bool showId)
        {
            Clear();

			var data = new TaskPriorityTypeDataModel();
			data.TaskPriorityTypeId = taskPriorityTypeId;

            var items = TaskPriorityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TaskPriorityTypeId;
				Weight = item.Weight;
                oHistoryList.Setup(PrimaryEntity, taskPriorityTypeId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

			var data = new TaskPriorityTypeDataModel();

            SetData(data);
        }

		public void SetData(TaskPriorityTypeDataModel data)
        {
            SystemKeyId = data.TaskPriorityTypeId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTaskPriorityTypeId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPriorityType;
            PrimaryEntityKey = "TaskPriorityType";
            FolderLocationFromRoot = "TaskPriorityType";

            PlaceHolderCore = dynTaskPriorityTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTaskPriorityTypeId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}