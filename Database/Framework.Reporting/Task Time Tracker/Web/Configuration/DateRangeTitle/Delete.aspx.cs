using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.DateRangeTitle
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DateRangeTitle;
			PrimaryEntityKey = "DateRangeTitle";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("DateRangeTitle", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new DateRangeTitleDataModel();
					data.DateRangeTitleId = int.Parse(index);
					Framework.Components.UserPreference.DateRangeTitleDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				DeleteAndRedirect();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.DateRangeTitle, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("DateRangeTitleEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion
	}
}