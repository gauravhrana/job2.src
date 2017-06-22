using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleItem
{
    public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.ScheduleItem;
			PrimaryEntityKey		= "ScheduleItem";
			PrimaryGenericControl	= myGenericControl;
			BreadCrumbObject		= Master.BreadCrumbObject;
		}

		#endregion
        
    }
}