using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.ApplicationRelation
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.ApplicationRelation;
            PrimaryEntityKey   = "ApplicationRelation";
            BreadCrumbObject   = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("ApplicationRelation", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void chkVisible_CheckedChanged(object sender, EventArgs e)
		{
            ShowAuditHistory(chkVisible.Checked);
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new ApplicationRelationDataModel();
					data.ApplicationRelationId = int.Parse(index);
					Framework.Components.Core.ApplicationRelationDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ApplicationRelation, SessionVariables.RequestProfile);
				Response.Redirect(Page.GetRouteUrl("ApplicationRelationEntityRoute", new { Action = "Default", SetId = true }), false);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		

		#endregion

	}
}