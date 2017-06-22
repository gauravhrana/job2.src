using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImageInstance;

            GenericControlPath = ApplicationCommon.GetControlPath("FunctionalityImageInstance", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "FunctionalityImageInstance";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;
        }
    }
}