using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Import;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.FileType
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FileType;
            PrimaryEntityKey = "FileType";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("FileType", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                var deleteIndexList = DeleteIds.Split(',');

                foreach (var index in deleteIndexList)
                {
                    var data = new FileTypeDataModel();
                    data.FileTypeId = int.Parse(index);

					if (!Framework.Components.Import.FileTypeDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.FileTypeId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (var index in deleteIndexList)
                    {
                        var data = new FileTypeDataModel();
                        data.FileTypeId = int.Parse(index);

						Framework.Components.Import.FileTypeDataManager.Delete(data, SessionVariables.RequestProfile);
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
                        msg += "FileTypeId: " + id + " has detail records";
                    }

                    foreach (string index in deleteIndexList)
                    {
                        var data = new FileTypeDataModel();
                        data.FileTypeId = int.Parse(index);

						Framework.Components.Import.FileTypeDataManager.DeleteChildren(data, SessionVariables.RequestProfile);
						Framework.Components.Import.FileTypeDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FileType, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("FileTypeEntityRoute", new { Action = "Default", SetId = true }), false);
        }
  
        #endregion

    }
}