using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.SkillLevel
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "SkillLevel";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("SkillLevel", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SkillLevel;
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new SkillLevelDataModel();
                    data.SkillLevelId = int.Parse(index);
                    SkillLevelDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SkillLevel, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("SkillLevelEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}