using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.ApplicationRelation
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRelation;
			PrimaryEntityKey = "ApplicationRelation";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("ApplicationRelation", ControlType.GenericControl);
			PrimaryGenericControl = myGenericControl;
		}

		#endregion

	}
}