using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.Aptitude.Competency
{
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Competency;
            PrimaryEntityKey = "Competency";
            BreadCrumbObject = Master.BreadCrumbObject;
            GenericControlPath = ApplicationCommon.GetControlPath("Competency", ControlType.GenericControl);
            PrimaryGenericControl = myGenericControl;
        }

        #endregion

    }
}