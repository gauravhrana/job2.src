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

namespace Shared.UI.Web.TasksAndWorkflow.TaskScheduleType.Controls
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
            var data = new TaskScheduleTypeDataModel();

            data.TaskScheduleTypeId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
			data.Active = Active;

            if (action == "Insert")
            {
				var dtTaskScheduleType = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTaskScheduleType.Rows.Count == 0)
                {
					Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TaskScheduleTypeId;
        }

        public override void SetId(int setId, bool chkTaskScheduleTypeId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskScheduleTypeId);
            CoreSystemKey.Enabled = chkTaskScheduleTypeId;
            //txtDescription.Enabled = !chkTaskScheduleTypeId;
            //txtName.Enabled = !chkTaskScheduleTypeId;
            //txtSortOrder.Enabled = !chkTaskScheduleTypeId;
        }

        public void LoadData(int taskScheduleTypeId, bool showId)
        {
            Clear();

            var data = new TaskScheduleTypeDataModel();
			data.TaskScheduleTypeId = taskScheduleTypeId;

			var items = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TaskScheduleTypeId;
				Active = item.Active;
                oHistoryList.Setup(PrimaryEntity, taskScheduleTypeId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TaskScheduleTypeDataModel();

            SetData(data);
        }

        public void SetData(TaskScheduleTypeDataModel data)
        {
            SystemKeyId = data.TaskScheduleTypeId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTaskScheduleTypeId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskScheduleType;
            PrimaryEntityKey = "TaskScheduleType";
            FolderLocationFromRoot = "TaskScheduleType";

            PlaceHolderCore = dynTaskScheduleTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTaskScheduleTypeId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}