using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.UseCaseXUseCaseStep
{
	public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep;

			GenericControlPath	= ApplicationCommon.GetControlPath("UseCaseXUseCaseStep", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
			PrimaryEntityKey	= "UseCaseXUseCaseStep";
			BreadCrumbObject	= Master.BreadCrumbObject;

			BtnUpdate			= btnUpdate;
			BtnClone			= btnClone;
			BtnCancel			= btnCancel;
		}

		#endregion


	}
}