using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.BatchFileSet
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileSet;

			GenericControlPath = ApplicationCommon.GetControlPath("BatchFileSet", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "BatchFileSet";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnCancel = btnCancel;
			BtnClone = btnClone;
		}

		#endregion

    }
}