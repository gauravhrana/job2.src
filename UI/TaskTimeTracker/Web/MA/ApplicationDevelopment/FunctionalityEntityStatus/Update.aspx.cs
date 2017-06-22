using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
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
            var fdt = new DataTable();
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
            //return Framework.Components.ApplicationSecurity.GetFunctionalityEntityStatusArchiveColumns("FunctionalityEntityStatusArchive_PC", AuditId);
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatusArchive, "Developer", SessionVariables.RequestProfile);
        }

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var plcHolder = new PlaceHolder();   
            var tabControl = ApplicationCommon.GetNewDetailTabControl();
            plcHolder.Controls.Add(detailsControl);
            plcHolder.Controls.Add(new Literal() { Text = "<br /><br />" });
                
            //var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
            //listControl.Setup("FunctionalityEntityStatusArchive", "", "FunctionalityEntityStatusArchiveId", setId, true, GetData, GetFunctionalityEntityStatusArchiveColumns, "FunctionalityEntityStatusArchive");
            //listControl.SetSession("true");
            //listControl.HideControls();
            //plcHolder.Controls.Add(listControl);
            //plcHolder.Controls.Add(new Literal() { Text = "<br /><br />" });

     
            var listControl2 = (ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality.Controls.Details)Page.LoadControl("~/MA/ApplicationDevelopment/Functionality/Controls/Details.ascx");
            var fid = int.Parse(GetAssociatedFunctionalityIds(setId).Rows[0][FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId].ToString());
            listControl2.Setup(fid);

            tabControl.Setup("FunctionalityEntityStatusDetailsView");
            tabControl.AddTab("FunctionalityEntityStatus", detailsControl, String.Empty, true);
            //tabControl.AddTab("FunctionalityEntityStatusArchive", listControl, "History");
            tabControl.AddTab("Functionality", listControl2, string.Empty, false);

            //listControl.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
            

            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetFunctionalityEntityStatusArchiveData(int.Parse(key));
		}

        #endregion


        protected override void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            var isVisible = ((CheckBox)sender).Checked;

            foreach (var control in PrimaryPlaceHolder.Controls)
            {
                try
                {
                    if (control is Shared.UI.Web.Controls.DetailTabControl)
                    {
                        var cntrl = (Shared.UI.Web.Controls.DetailTabControl)control;
						//var plcholder = (PlaceHolder)cntrl.GenericControl;
						//((ControlCommon)plcholder.Controls[0]).IsHistoryVisible = isVisible;
						((ControlCommon)cntrl.GenericControl ).IsHistoryVisible = isVisible;
                    }
                    else
                    {
                        ((ControlCommon)control).IsHistoryVisible = isVisible;
                    }
                }
                catch { }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus;

            GenericControlPath = ApplicationCommon.GetControlPath("FunctionalityEntityStatus", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "FunctionalityEntityStatus";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;


        }

    }
} 