using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.FileType
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FileType;
            PrimaryEntityKey = "FileType";
            DetailsControlPath = ApplicationCommon.GetControlPath("FileType", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}