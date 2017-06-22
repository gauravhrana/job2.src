using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperation
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.ApplicationOperation;

            GenericControlPath = ApplicationCommon.GetControlPath("ApplicationOperation", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey   = "ApplicationOperation";
            BreadCrumbObject   = Master.BreadCrumbObject;

            BtnUpdate          = btnUpdate;
            BtnClone           = btnClone;
            BtnCancel          = btnCancel;
        }        

        #endregion

    }
}