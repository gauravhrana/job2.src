using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.Controls
{
	public partial class Generic : ControlGenericStandard
    {
        #region methods

        public override int? Save(string action)
        {
            var data = new ProjectDataModel();

            data.ProjectId		= SystemKeyId;
            data.Name			= Name;
            data.Description	= Description;
            data.SortOrder		= SortOrder;

            if (action == "Insert")
            {
                var dtProject = ProjectDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtProject.Rows.Count == 0)
                {
                    ProjectDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ProjectDataManager.Update(data, SessionVariables.RequestProfile);
            }

			// not correct ... when doing insert, we didn't get/change the value of ProjectID ?
			return data.ProjectId;
        }

        public override void SetId(int setId, bool chkProjectId)
        {			
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkProjectId);
			
			CoreSystemKey.Enabled = chkProjectId;            
        }

        public void LoadData(int ProjectId, bool showId)
        {
			// clear UI				
			Clear();

			// set up parameters
			var data = new ProjectDataModel();
            data.ProjectId = ProjectId;

			// get data
			var items = ProjectDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
	        if (items.Count != 1) return;

	        var item = items[0];

			SetData(item);

	        if (!showId)
	        {
		        SystemKeyId = item.ProjectId;

		        //PlaceHolderAuditHistory.Visible = true;
		        // only show Audit History in case of Update page, not for Clone.
		        oHistoryList.Setup(PrimaryEntity, ProjectId, PrimaryEntityKey);					
	        }
	        else
	        {
				CoreSystemKey.Text = String.Empty;
	        }
        }

		protected override void Clear()
		{
			base.Clear();

			var data = new ProjectDataModel();

			SetData(data);
		}

		public void SetData(ProjectDataModel data)
		{
			SystemKeyId		= data.ProjectId;

			base.SetData(data);
		}

		#endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
            lblProjectId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{			
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.Project;
			PrimaryEntityKey		= "Project";
			FolderLocationFromRoot  = "Project";
			
			// set object variable reference            
			PlaceHolderCore			= dynProjectId;
			PlaceHolderAuditHistory = dynAuditHistory;
			//BorderDiv				= borderdiv;
			
			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey			= txtProjectId;			
			CoreControlName			= txtName;
			CoreControlDescription	= txtDescription;
			CoreControlSortOrder	= txtSortOrder;

			CoreUpdateInfo			= oUpdateInfo;
		}

        #endregion

    }
}