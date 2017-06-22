using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.Import;
using Framework.Components.Import;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.BatchFile
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {      

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "BatchFile";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("BatchFile", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFile;
		}
        
		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.BatchFile, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("BatchFileEntityRoute", new { Action = "Default", SetId = true }), false);
		}
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new BatchFileDataModel();
                    data.BatchFileId = int.Parse(index);
					if (!Framework.Components.Import.BatchFileDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add(Convert.ToInt32(data.BatchFileId));
                    }
                }
                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new BatchFileDataModel();
                        data.BatchFileId = int.Parse(index);
						Framework.Components.Import.BatchFileDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
					DeleteAndRedirect();
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
                        msg += "BatchFileId: " + id + " has detail records";
                    }
                    Response.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("ImportEntityRoute", new { Action = "Default" }), false);
        }

        #endregion

    }
}