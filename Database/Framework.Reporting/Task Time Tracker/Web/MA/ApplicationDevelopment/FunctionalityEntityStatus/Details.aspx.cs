using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
       
        #region Methods

        private DataTable GetFunctionalityEntityStatusArchiveData(int functionalityEntityStatusId)
        {
            var data = new FunctionalityEntityStatusArchiveDataModel();
            data.FunctionalityEntityStatusId = functionalityEntityStatusId;
            var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusArchiveDataManager.SearchHistory(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetFunctionalityColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Functionality, "DBColumns", SessionVariables.RequestProfile);
        }

        protected DataTable GetAssociatedFunctionalityIds(int functionalityEntityStatusId)
        {
            var data = new FunctionalityEntityStatusDataModel();
            data.FunctionalityEntityStatusId = functionalityEntityStatusId;
			var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Search(data, SessionVariables.RequestProfile);
            var fdt = new DataTable() ;
            foreach (DataRow dr in results.Rows)
            {
                var funcId = dr[FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId].ToString();
                var fdata = new FunctionalityDataModel();
                fdata.FunctionalityId = int.Parse(funcId);
				fdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Search(fdata, SessionVariables.RequestProfile);
            }
            //var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.Functionality.GetList(SessionVariables.RequestProfile); 
            return fdt;
        }

        private string[] GetFunctionalityEntityStatusArchiveColumns()
        {
            //return Framework.Components.ApplicationSecurity.GetFunctionalityEntityStatusArchiveColumns("FunctionalityEntityStatusArchive_PC", SessionVariables.RequestProfile);
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatusArchive, "DBColumns", SessionVariables.RequestProfile);
        }

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            
            var tabControl = ApplicationCommon.GetNewDetailTabControl();
			detailsControl.ID = "Details";
            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
            listControl.Setup("FunctionalityEntityStatusArchive", "", "FunctionalityEntityStatusArchiveId", setId, true, GetData, GetFunctionalityEntityStatusArchiveColumns, "FunctionalityEntityStatusArchive");
            listControl.SetSession("true");
            var listControl2 = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
            listControl2.Setup("Functionality", "", "FunctionalityId", setId, true, GetAssociatedFunctionalityIds, GetFunctionalityColumns, "Functionality");
            listControl2.SetSession("true");

            tabControl.Setup("FunctionalityEntityStatusDetailsView");
            tabControl.AddTab("FunctionalityEntityStatus", detailsControl, String.Empty, true); 
            tabControl.AddTab("FunctionalityEntityStatusArchive", listControl, "History");
            tabControl.AddTab("Functionality", listControl2, string.Empty, true);
            
            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetFunctionalityEntityStatusArchiveData(int.Parse(key));
		}


		private DataTable GetAssociatedFunctionalityIds(string key)
		{
			return GetAssociatedFunctionalityIds(int.Parse(key));
		}

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus;
            PrimaryEntityKey = "FunctionalityEntityStatus";
            DetailsControlPath = ApplicationCommon.GetControlPath("FunctionalityEntityStatus", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}