using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolioGroup.Controls
{
    public partial class Generic : ControlGeneric
    {

        #region properties

        public int? ProjectPortfolioGroupId
        {
            get
            {
                if (txtProjectPortfolioGroupId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtProjectPortfolioGroupId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtProjectPortfolioGroupId.Text);
                }
            }
            set
            {
                txtProjectPortfolioGroupId.Text = (value == null) ? String.Empty : value.ToString();
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
       
        #endregion

        #region private methods

        public override int? Save(string action)
        {
            var data = new ProjectPortfolioGroupDataModel();

            data.ProjectPortfolioGroupId     = ProjectPortfolioGroupId;            
            data.Name                        = Name;
            data.Description                 = Description;
            data.SortOrder                   = SortOrder;

            if (action == "Insert")
            {
                var dtProjectPortfolioGroup = ProjectPortfolioGroupDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtProjectPortfolioGroup.Rows.Count == 0)
                {
                    ProjectPortfolioGroupDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ProjectPortfolioGroupDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return ProjectPortfolioGroupId;
        }

        public override void SetId(int setId, bool chkProjectPortfolioGroupId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkProjectPortfolioGroupId);
            txtProjectPortfolioGroupId.Enabled = chkProjectPortfolioGroupId;
            //txtDescription.Enabled = !chkCountryId;
            //txtName.Enabled = !chkCountryId;
            //txtSortOrder.Enabled = !chkCountryId;
        }

        public void LoadData(int projectPortfolioGroupId, bool showId)
        {
            // clear UI				

            Clear();

            var dataQuery = new ProjectPortfolioGroupDataModel();
            dataQuery.ProjectPortfolioGroupId = projectPortfolioGroupId;

            var items = ProjectPortfolioGroupDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            ProjectPortfolioGroupId  = item.ProjectPortfolioGroupId;
            Name                     = item.Name;
            Description              = item.Description;           
            SortOrder                = item.SortOrder;

            if (!showId)
            {
                txtProjectPortfolioGroupId.Text = item.ProjectPortfolioGroupId.ToString();

                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup((int)SystemEntity.ProjectPortfolioGroup, projectPortfolioGroupId, "ProjectPortfolioGroup");

            }
            else
            {
                txtProjectPortfolioGroupId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }


        protected override void Clear()
        {
            base.Clear();

            var data = new ProjectPortfolioGroupDataModel();

            ProjectPortfolioGroupId  = data.ProjectPortfolioGroupId;
            Name                     = data.Name;
            Description              = data.Description;           
            SortOrder                = data.SortOrder;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtProjectPortfolioGroupId.Visible = isTesting;
                lblProjectPortfolioGroupId.Visible = isTesting;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "ProjectPortfolioGroup";
            FolderLocationFromRoot = "ProjectPortfolioGroup";
            PrimaryEntity = SystemEntity.ProjectPortfolioGroup;

            // set object variable reference            
            PlaceHolderCore = dynProjectPortfolioGroupId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }



        #endregion

    }
}