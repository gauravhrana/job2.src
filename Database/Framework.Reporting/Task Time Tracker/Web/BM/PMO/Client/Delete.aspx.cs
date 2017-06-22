using System;
using System.Collections.Generic;
using System.Web.UI;
using DataModel.TaskTimeTracker;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.PMO.Client
{
	public partial class Delete : PageDelete
    {       

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= SystemEntity.Client;			
			PrimaryEntityKey	= "Client";
			BreadCrumbObject	= Master.BreadCrumbObject;

			DetailsControlPath	= ApplicationCommon.GetControlPath("Client", ControlType.DetailsControl);
			PrimaryPlaceHolder	= plcDetailsList;			
		}       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
				var deleteIndexList = DeleteIds.Split(',');

				foreach (var index in deleteIndexList)
                {
                    var data = new ClientDataModel();
                    data.ClientId = int.Parse(index);

                    if (!ClientDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.ClientId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (var index in deleteIndexList)
                    {
                        var data = new ClientDataModel();
                        data.ClientId = int.Parse(index);

                        ClientDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
                }
                else
                {
                    var msg = String.Empty;

                    foreach (var id in notDeletableIds)
                    {
                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg += ", <br/>";
                        }
                        msg += "ClientId: " + id + " has detail records";
                    }
                    
					foreach (string index in deleteIndexList)
                    {
                        var data = new ClientDataModel();
                        data.ClientId = int.Parse(index);

                        ClientDataManager.DeleteChildren(data, SessionVariables.RequestProfile);
                        ClientDataManager.Delete(data, SessionVariables.RequestProfile);
                    }					
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
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.Client, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ClientEntityRoute", new {Action = "Default", SetId = true}), false);
		}        

		#endregion

	}
}