using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.ScheduleQuestion;

			GenericControlPath	= ApplicationCommon.GetControlPath("ScheduleQuestion", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
			PrimaryEntityKey	= "ScheduleQuestion";
			BreadCrumbObject	= Master.BreadCrumbObject;

			BtnUpdate	= btnUpdate;
			BtnClone	= btnClone;
			BtnCancel	= btnCancel;

		}
		#endregion

    }
}