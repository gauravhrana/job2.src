using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Aptitude.CompetencyXSkill
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);
			PrimaryEntityKey = "CompetencyXSkill";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("CompetencyXSkill", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.CompetencyXSkill;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();
                    data.CompetencyXSkillId = int.Parse(index);
                    TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.CompetencyXSkill, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("CompetencyXSkillEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion        

    }
}