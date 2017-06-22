using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ProjectPortfolio
{
    public partial class Clone : PageClone
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ProjectPortfolio;
            PrimaryEntityKey = "ProjectPortfolio";
            BreadCrumbObject = Master.BreadCrumbObject;
            GenericControlPath = ApplicationCommon.GetControlPath("ProjectPortfolio", ControlType.GenericControl);
            PrimaryGenericControl = myGenericControl;
        }

        #endregion



    }
}