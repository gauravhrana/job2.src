using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Dapper;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Project
{
    public partial class Details : PageDetails
    {

        #region Methods

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("Project", String.Empty, "ProjectId", setId, true, GetData, GetClientColumns, "Project");
			listControl.SetSession("true");

			tabControl.Setup("ProjectDetailsView");
			tabControl.AddTab("Project", detailsControl, String.Empty, true);
			tabControl.AddTab("Need", listControl);

			//var tabControl = ApplicationCommon.GetTabControl("Project", SetId, detailsControl, "ProjectDetailsView");                    
			return tabControl;
		}

		private DataTable GetData(string key)
		{
			return GetClientData(int.Parse(key));
		}

        private DataTable GetClientData(int projectId)
        {
			var dt = ClientXProjectDataManager.GetByProject(projectId, SessionVariables.RequestProfile.AuditId);
            var clientList = ClientDataManager.GetEntityDetails(ClientDataModel.Empty, SessionVariables.RequestProfile, 0);

			var resultList = new List<ClientDataModel>();

            foreach (DataRow row in dt.Rows)
            {
				var tmpClientId = Convert.ToInt32(row[ClientDataModel.DataColumns.ClientId]);
				resultList.AddRange(clientList.Where(item => item.ClientId == tmpClientId));                
            }
			return resultList.ToDataTable();
        }

        private string[] GetClientColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Client, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetNeedData(int projectId)
        {
			var dt = ProjectXNeedDataManager.GetByProject(projectId, SessionVariables.RequestProfile.AuditId);
            var needdt = NeedDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = needdt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = needdt.Select("NeedId = " + row[NeedDataModel.DataColumns.NeedId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;
        }

        private string[] GetNeedColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Need, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion     
			

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "Project";
			var detailsPath = ApplicationCommon.GetControlPath("Project", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;			
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = SystemEntity.Project;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion       

    }
}
