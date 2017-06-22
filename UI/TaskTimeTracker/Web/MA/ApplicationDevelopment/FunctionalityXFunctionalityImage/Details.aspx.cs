using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
        #region Methods

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.AddTab("Functionality", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("FunctionalityImage", listControl);

			listControl.Setup("FunctionalityImage", String.Empty, "FunctionalityImageId", setId, true, GetData, GetFunctionalityImageColumns, "Functionality");
            listControl.SetSession("true");

            tabControl.Setup("FunctionalityDetailsView");

            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetFunctionalityImageData(int.Parse(key));
		}

        private Shared.UI.Web.Controls.DetailTab1Control GetVerticalTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetail1TabControl();;

            tabControl.AddTab("Functionality", detailsControl, "Functionality", false);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("FunctionalityImage", listControl, "FunctionalityImage");

            listControl.Setup("FunctionalityImage", String.Empty, "FunctionalityImageId", setId, true, GetData, GetFunctionalityImageColumns, "FunctionalityImage");
            listControl.SetSession("true");

            //tabControl.AddTab("FunctionalityImage", "", 2, "FunctionalityImageId", setId, true, GetData, GetFunctionalityImageColumns);
            //tabControl.AddLastTab();
            return tabControl;
        }

        private DataTable GetFunctionalityImageData(int functionalityId)
        {
			var dt = FunctionalityXFunctionalityImageDataManager.GetByFunctionality(functionalityId, SessionVariables.RequestProfile);
			var FunctionalityImagedt = FunctionalityImageDataManager.Search(
                FunctionalityImageDataModel.Empty, SessionVariables.RequestProfile);
            var resultdt = FunctionalityImagedt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                var rows = FunctionalityImagedt.Select("FunctionalityImageId = " + row[FunctionalityImageDataModel.DataColumns.FunctionalityImageId]);
                resultdt.ImportRow(rows[0]);
            }

            return resultdt;
        }

        private string[] GetFunctionalityImageColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityImage, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityImage;
            PrimaryEntityKey = "FunctionalityXFunctionalityImage";
            DetailsControlPath = ApplicationCommon.GetControlPath("FunctionalityXFunctionalityImage", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}