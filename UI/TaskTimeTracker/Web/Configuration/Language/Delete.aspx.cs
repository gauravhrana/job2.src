using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.Core;
using Framework.Components.Audit;

namespace Shared.UI.Web.Configuration.Language
{
    public partial class Delete : PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.Language;
            PrimaryEntityKey = "Language";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("Language", ControlType.DetailsControl);
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
                    var data = new LanguageDataModel();
                    data.LanguageId = int.Parse(index);

                    if (!LanguageDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.LanguageId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (var index in deleteIndexList)
                    {
                        var data = new LanguageDataModel();
                        data.LanguageId = int.Parse(index);

                        LanguageDataManager.Delete(data, SessionVariables.RequestProfile);
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
                        msg += "LanguageId: " + id + " has detail records";
                    }

                    foreach (string index in deleteIndexList)
                    {
                        var data = new LanguageDataModel();
                        data.LanguageId = int.Parse(index);

                        LanguageDataManager.DeleteChildren(data, SessionVariables.RequestProfile);
                        LanguageDataManager.Delete(data, SessionVariables.RequestProfile);
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
            AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.Language, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("LanguageEntityRoute", new { Action = "Default", SetId = true }), false);
        }   

        #endregion

    }
}