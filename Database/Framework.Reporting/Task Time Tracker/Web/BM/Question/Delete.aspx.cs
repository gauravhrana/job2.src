using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Question
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Question;
            PrimaryEntityKey = "Question";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("Question", ControlType.DetailsControl);
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
                    var data = new QuestionDataModel();
                    data.QuestionId = int.Parse(index);

                    if (!QuestionDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.QuestionId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (var index in deleteIndexList)
                    {
                        var data = new QuestionDataModel();
                        data.QuestionId = int.Parse(index);

                        QuestionDataManager.Delete(data, SessionVariables.RequestProfile);
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
                        msg += "QuestionId: " + id + " has detail records";
                    }
                    Response.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Question, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("QuestionEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion     

    }
}