using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipType
{
	public partial class Insert : PageInsert
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.SystemForeignRelationshipType;
			PrimaryEntityKey = "SystemForeignRelationshipType";
			PrimaryGenericControl = myGenericControl;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}