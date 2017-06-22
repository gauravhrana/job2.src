using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.Skill
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "Skill";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("Skill", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Skill;
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new SkillDataModel();
                    data.SkillId = int.Parse(index);
                    SkillDataManager.Delete(data, SessionVariables.RequestProfile);
                    DeleteAndRedirect();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Skill, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("SkillEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}