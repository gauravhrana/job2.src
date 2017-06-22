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

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolio.Controls
{
    public partial class Generic : ControlGeneric
    {
        #region properties     

        public int? ProjectPortfolioId
        {
            get
            {
                if (txtProjectPortfolioId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtProjectPortfolioId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtProjectPortfolioId.Text);
                }
            }
            set
            {
                txtProjectPortfolioId.Text = (value == null) ? String.Empty : value.ToString();
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
            var data = new ProjectPortfolioDataModel();

            data.ProjectPortfolioId = ProjectPortfolioId; 
            data.Name = Name;
            data.Description = Description;           
            data.SortOrder = SortOrder;
            //data.CreatedByAuditId = CreatedByAuditId;
            //data.ModifiedByAuditId = ModifiedByAuditId;
            //data.CreatedDate = CreatedDate;
            //data.ModifiedDate = ModifiedDate;


            if (action == "Insert")
            {
                var dtProjectPortfolio = ProjectPortfolioDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtProjectPortfolio.Rows.Count == 0)
                {
                    ProjectPortfolioDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ProjectPortfolioDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return ProjectPortfolioId;
        }

        public override void SetId(int setId, bool chkProjectPortfolioId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkProjectPortfolioId);
            txtProjectPortfolioId.Enabled = chkProjectPortfolioId;
            //txtDescription.Enabled = !chkProjectPortfolioId;
            //txtName.Enabled = !chkProjectPortfolioId;
            //txtSortOrder.Enabled = !chkProjectPortfolioId;

        }

        public void LoadData(int ProjectPortfolioId, bool showId)
        {
          
            Clear();

            var dataQuery = new ProjectPortfolioDataModel();
            dataQuery.ProjectPortfolioId = ProjectPortfolioId;

            var items = ProjectPortfolioDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            ProjectPortfolioId   = (int)item.ProjectPortfolioId;
            Name                 = item.Name;
            Description          = item.Description;
            SortOrder            = item.SortOrder;
            

            if (!showId)
            {
                txtProjectPortfolioId.Text = item.ProjectPortfolioId.ToString();

                oHistoryList.Setup((int)SystemEntity.ProjectPortfolio, ProjectPortfolioId, "ProjectPortfolio");

            }
            else
            {
                txtProjectPortfolioId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }


        protected override void Clear()
        {
            base.Clear();

            var data = new ProjectPortfolioDataModel();

            ProjectPortfolioId   = data.ProjectPortfolioId;            
            Name                 = data.Name;
            Description          = data.Description;
            SortOrder            = data.SortOrder;            
        }



        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtProjectPortfolioId.Visible = isTesting;
                lblProjectPortfolioId.Visible = isTesting;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "ProjectPortfolio";
            FolderLocationFromRoot = "ProjectPortfolio";
            PrimaryEntity = SystemEntity.ProjectPortfolio;
          
            PlaceHolderCore = dynProjectPortfolioId;
            PlaceHolderAuditHistory = dynAuditHistory;
        
            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }



        #endregion

    }
}