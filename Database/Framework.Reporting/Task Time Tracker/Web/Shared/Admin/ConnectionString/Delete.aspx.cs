using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.ConnectionString
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

		#region Methods

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ConnectionString, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ConnectionStringEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.ConnectionString;
			PrimaryEntityKey   = "ConnectionString";
			BreadCrumbObject   = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("ConnectionString", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ConnectionStringDataModel();
                    data.ConnectionStringId = int.Parse(index);
					Framework.Components.Core.ConnectionStringDataManager.Delete(data, SessionVariables.RequestProfile);
                }
				DeleteAndRedirect();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #endregion

    }
}