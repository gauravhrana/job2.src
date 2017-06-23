using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.RenumberMigration
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey   = "RenumberMigration";
            var detailsPath    = ApplicationCommon.GetControlPath("RenumberMigration", ControlType.DetailsControl);
            DetailsControlPath = detailsPath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.RenumberMigration;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion       

    }
}