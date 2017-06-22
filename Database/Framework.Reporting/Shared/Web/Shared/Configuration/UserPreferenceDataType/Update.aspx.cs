using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.UserReferenceDataType;

namespace Shared.UI.Web.Configuration.UserPreferenceDataType
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreferenceDataType;

            GenericControlPath = ApplicationCommon.GetControlPath("UserPreferenceDataType", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "UserPreferenceDataType";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;
        }

    }
}