using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask
{
	public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.TaskPackageXOwnerXTask;
			PrimaryEntityKey = "TaskPackageXOwnerXTask";
			PrimaryGenericControl = myGenericControl;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}