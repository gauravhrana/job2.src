using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{
        #region Methods

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.AddTab("Functionality", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("FunctionalityActiveStatus", listControl);

            listControl.Setup("FunctionalityActiveStatus", String.Empty, "FunctionalityActiveStatusId", setId, true, GetData, GetFunctionalityActiveStatusColumns, "Functionality");
            listControl.SetSession("true");

            tabControl.Setup("FunctionalityDetailsView");

            return tabControl;
        }


		private DataTable GetData(string key)
		{
			return GetFunctionalityActiveStatusData(int.Parse(key));
		}

        private Shared.UI.Web.Controls.DetailTab1Control GetVerticalTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetail1TabControl();;

            tabControl.AddTab("Functionality", detailsControl, "Functionality", false);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("FunctionalityActiveStatus", listControl, "FunctionalityActiveStatus");

            listControl.Setup("FunctionalityActiveStatus", String.Empty, "FunctionalityActiveStatusId", setId, true, GetData, GetFunctionalityActiveStatusColumns, "FunctionalityActiveStatus");
            listControl.SetSession("true");

            //tabControl.AddTab("FunctionalityActiveStatus", "", 2, "FunctionalityActiveStatusId", setId, true, GetData, GetFunctionalityActiveStatusColumns);
            //tabControl.AddLastTab();
            return tabControl;
        }

		private DataTable GetFunctionalityActiveStatusData(int functionalityId)
        {
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.GetByFunctionality(functionalityId, SessionVariables.RequestProfile);
            var FunctionalityActiveStatusdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = FunctionalityActiveStatusdt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                var rows = FunctionalityActiveStatusdt.Select("FunctionalityActiveStatusId = " + row[FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId]);
                resultdt.ImportRow(rows[0]);
            }

            return resultdt;
        }

        private string[] GetFunctionalityActiveStatusColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityActiveStatus, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

		#region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityOwner;
            PrimaryEntityKey = "FunctionalityOwner";
            DetailsControlPath = ApplicationCommon.GetControlPath("FunctionalityOwner", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }

		#endregion

	}
}