using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.RiskAndReward.Risk
{
    public partial class Update : PageUpdate
    {

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

            PrimaryEntity = SystemEntity.Risk;

            GenericControlPath  = ApplicationCommon.GetControlPath("Risk", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
            PrimaryEntityKey    = "Risk";
			BreadCrumbObject	= Master.BreadCrumbObject;

            BtnUpdate			= btnUpdate;
            BtnClone			= btnClone;
            BtnCancel			= btnCancel;
		}
		
        #endregion       
    }
}