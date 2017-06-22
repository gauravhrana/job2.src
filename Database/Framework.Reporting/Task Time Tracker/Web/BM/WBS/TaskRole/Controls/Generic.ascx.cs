using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.TaskRole.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? TaskRoleId
        {
            get
            {
                if (txtTaskRoleId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTaskRoleId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtTaskRoleId.Text);
                }
            }
            set
            {
                txtTaskRoleId.Text = (value == null) ? String.Empty : value.ToString();
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

        #endregion


        #region private methods

        public override int? Save(string action)
        {
            var data = new TaskRoleDataModel();

            data.TaskRoleId = TaskRoleId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtTaskRole = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTaskRole.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of CountryID ?
            return TaskRoleId;
        }

        public override void SetId(int setId, bool chkTaskRoleId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTaskRoleId);
            txtTaskRoleId.Enabled = chkTaskRoleId;
        }

        public void LoadData(int taskRoleId, bool showId)
        {
            // clear UI				

            Clear();

            var dataQuery = new TaskRoleDataModel();
            dataQuery.TaskRoleId = taskRoleId;

            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            TaskRoleId = item.TaskRoleId;
            Name = item.Name;
            Description = item.Description;
            SortOrder = item.SortOrder;

            if (!showId)
            {
                txtTaskRoleId.Text = item.TaskRoleId.ToString();

                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TaskRole, taskRoleId, "TaskRole");

            }
            else
            {
                txtTaskRoleId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }


        protected override void Clear()
        {
            base.Clear();

            var data = new TaskRoleDataModel();

            TaskRoleId = data.TaskRoleId;
            Name = data.Name;
            Description = data.Description;
            SortOrder = data.SortOrder;
        }



        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtTaskRoleId.Visible = isTesting;
                lblTaskRoleId.Visible = isTesting;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "TaskRole";
            FolderLocationFromRoot = "TaskRole";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRole;

            // set object variable reference            
            PlaceHolderCore = dynTaskRoleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }



        #endregion

    }
}