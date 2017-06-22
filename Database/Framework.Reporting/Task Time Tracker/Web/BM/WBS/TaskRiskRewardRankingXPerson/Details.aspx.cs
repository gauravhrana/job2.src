using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.WBS.TaskRiskRewardRankingXPerson
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRiskRewardRankingXPerson;
			PrimaryEntityKey = "TaskRiskRewardRankingXPerson";
			DetailsControlPath = ApplicationCommon.GetControlPath("TaskRiskRewardRankingXPerson", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

    }
}