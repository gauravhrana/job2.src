using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using System.Globalization;

namespace ApplicationContainer.UI.Web.ProjectManagement.ProjectTimeLine.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? ProjectTimeLineId
        {
            get
            {
                if (txtProjectTimeLineId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtProjectTimeLineId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtProjectTimeLineId.Text);
                }
            }
            set
            {
                txtProjectTimeLineId.Text = (value == null) ? String.Empty : value.ToString();
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

        public DateTime? StartDate
        {
            get
            {
                return DateTime.Parse(txtStartDate.Text.Trim());
            }
            set
            {
                txtStartDate.Text = (value == null) ? String.Empty : value.Value.ToString("MM/dd/yyyy");
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return DateTime.Parse(txtEndDate.Text.Trim());
            }
            set
            {
                txtEndDate.Text = (value == null) ? String.Empty : value.Value.ToString("MM/dd/yyyy");
            }
        }

        #endregion


        #region private methods

        public override int? Save(string action)
        {
            var data = new ProjectTimeLineDataModel();

            data.ProjectTimeLineId = ProjectTimeLineId;            
            data.ProjectId = ProjectId;
            data.StartDate = StartDate;
            data.EndDate = EndDate;
            
            if (action == "Insert")
            {
                var dtProjectTimeLine = ProjectTimeLineDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtProjectTimeLine.Rows.Count == 0)
                {
                    ProjectTimeLineDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ProjectTimeLineDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of CountryID ?
            return ProjectTimeLineId;
        }

        public override void SetId(int setId, bool chkProjectTimeLineId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkProjectTimeLineId);
            txtProjectTimeLineId.Enabled = chkProjectTimeLineId;
            //txtDescription.Enabled = !chkCountryId;
            //txtName.Enabled = !chkCountryId;
            //txtSortOrder.Enabled = !chkCountryId;
        }

        public void LoadData(int projectTimeLineId, bool showId)
        {
            // clear UI				

            Clear();

            var dataQuery = new ProjectTimeLineDataModel();
            dataQuery.ProjectTimeLineId = projectTimeLineId;

            var items = ProjectTimeLineDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            ProjectTimeLineId = item.ProjectTimeLineId;
            ProjectId = item.ProjectId;            
            StartDate = item.StartDate;
            EndDate = item.EndDate;

            if (!showId)
            {

                txtProjectTimeLineId.Text = item.ProjectTimeLineId.ToString();

                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ProjectTimeLine, projectTimeLineId, "ProjectTimeLine");

            }
            else
            {
                txtProjectTimeLineId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }


        protected override void Clear()
        {
            base.Clear();

            var data = new ProjectTimeLineDataModel();

            ProjectTimeLineId   = data.ProjectTimeLineId;
            ProjectId           = data.ProjectId;            
            StartDate           = data.StartDate;
            EndDate             = data.EndDate;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
            var projectData = ProjectDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(projectData, drpProjectList, StandardDataModel.StandardDataColumns.Name, ProjectDataModel.DataColumns.ProjectId);

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
            }
            else
            {
                if (!string.IsNullOrEmpty(txtProjectId.Text.Trim()))
                {
                    drpProjectList.SelectedValue = txtProjectId.Text;
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
                txtProjectTimeLineId.Visible = isTesting;
                lblProjectTimeLineId.Visible = isTesting;
                SetupDropdown();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "ProjectTimeLine";
            FolderLocationFromRoot = "ProjectTimeLine";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectTimeLine;

            // set object variable reference            
            PlaceHolderCore = dynProjectTimeLineId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        protected void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProjectId.Text = drpProjectList.SelectedItem.Value;
        }

        #endregion

    }
}