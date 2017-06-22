using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Configuration.Application
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

		#region Methods

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			detailsControl.ID = "Details";

			tabControl.Setup("ApplicationDetailsView");

			tabControl.AddTab("Application", detailsControl, String.Empty, true);

			var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("Project", listControl);
			listControl.Setup("Project", string.Empty, "ProjectId", setId, true, GetProjectData, GetProjectColumns, "Application");				
			listControl.SetSession("true");

			var listControlApplicationMode = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("ApplicationMode", listControlApplicationMode, "Application Mode");
			listControlApplicationMode.Setup("ApplicationMode", string.Empty, "ApplicationModeId", setId, true, GetApplicationModeData, GetApplicationModeColumns, "Application");
			listControlApplicationMode.SetSession("true");

			return tabControl;
		}

		private DataTable GetApplicationModeData(string applicationId)
		{
			var testReqProfile = SessionVariables.RequestProfile;
			testReqProfile.ApplicationId = int.Parse(applicationId);
			return ApplicationModeDataManager.GetList(testReqProfile);
		}

		private static string[] GetApplicationModeColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.ApplicationMode, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetProjectData(string applicationId)
		{
			var testReqProfile = SessionVariables.RequestProfile;
			testReqProfile.ApplicationId = int.Parse(applicationId);
			return ProjectDataManager.GetList(testReqProfile);			
		}

		private static string[] GetProjectColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Project, "DBColumns", SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Application;
            PrimaryEntityKey = "Application";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("Application", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                var deleteIndexList = DeleteIds.Split(',');

                foreach (var index in deleteIndexList)
                {
					var data = new ApplicationDataModel();
                    data.ApplicationId = int.Parse(index);

					if (!Framework.Components.ApplicationUser.ApplicationDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.ApplicationId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (var index in deleteIndexList)
                    {
						var data = new ApplicationDataModel();
                        data.ApplicationId = int.Parse(index);

						Framework.Components.ApplicationUser.ApplicationDataManager.Delete(data, SessionVariables.RequestProfile);
                    }

                    DeleteAndRedirect();
                }
                else
                {
                    var msg = String.Empty;

                    foreach (var id in notDeletableIds)
                    {
                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg += ", <br/>";
                        }
                        msg += "ApplicationId: " + id + " has detail records";
                    }
                    Response.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Application, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("ApplicationEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion      

    }
}