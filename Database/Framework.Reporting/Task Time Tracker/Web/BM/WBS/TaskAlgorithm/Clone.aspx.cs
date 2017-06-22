using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithm
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskAlgorithm;
			PrimaryEntityKey = "TaskAlgorithm";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("TaskAlgorithm", ControlType.GenericControl);
			PrimaryGenericControl = myGenericControl;
		}

		#endregion

	}
}