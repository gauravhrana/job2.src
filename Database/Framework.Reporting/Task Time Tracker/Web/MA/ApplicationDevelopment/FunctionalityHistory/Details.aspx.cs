using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityHistory
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
        #region Methods

        //private DataTable GetFunctionalityList()
        //{			
        //    var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImage.GetList(AuditId);
        //    return dt;
        //}

        //private DataTable GetAssociatedFunctionality(int functionalityId)
        //{
        //    var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityImage.GetByFunctionality(functionalityId, AuditId);
        //    return dt;
        //}

        //private void SaveFunctionalityXFunctionalityImage(int functionalityId, List<int> functionalityImageIds)
        //{
        //    TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityImage.DeleteByFunctionality(functionalityId, AuditId);
        //    TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityImage.CreateByFunctionality(functionalityId, functionalityImageIds.ToArray(), AuditId);			
        //}


        private Shared.UI.Web.Controls.DetailTabControl GetTabControl(int setId, Control detailsControl)
        {

            //var bucketControlPath = ApplicationCommon.BubcketControlPath;
            var FunctionalityImageControlPath = "~/Shared/Controls/FunctionalityImageControl/FunctionalityImageControl.ascx";
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            var bucketControlFunctionality = (Shared.UI.Web.Controls.FunctionalityImageControl.FunctionalityImageControl)Page.LoadControl(FunctionalityImageControlPath);
            bucketControlFunctionality.FunctionalityId = setId;
            //bucketControlFunctionality.ConfigureBucket("FunctionalityImage", setId, 1, GetFunctionalityList, GetAssociatedFunctionality, SaveFunctionalityXFunctionalityImage);

            tabControl.Setup("FunctionalityDetailsView");
            tabControl.AddTab("Functionality", detailsControl, String.Empty, true);
            tabControl.AddTab("FunctionalityXFunctionalityImage", bucketControlFunctionality);

            return tabControl;

        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityHistory;
            PrimaryEntityKey = "FunctionalityHistory";
            DetailsControlPath = ApplicationCommon.GetControlPath("FunctionalityHistory", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }


        #endregion

    }
}