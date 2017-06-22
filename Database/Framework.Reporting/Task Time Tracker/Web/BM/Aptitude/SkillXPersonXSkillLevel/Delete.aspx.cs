using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {       
        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SkillXPersonXSkillLevel;
			PrimaryEntityKey = "SkillXPersonXSkillLevel";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("SkillXPersonXSkillLevel", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;	
        }      		
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
					var data = new DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel();
                    data.SkillXPersonXSkillLevelId = int.Parse(index);
                    SkillXPersonXSkillLevelDataManager.Delete(data, SessionVariables.RequestProfile);
                }

				DeleteAndRedirect();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }       

        #endregion

        #region Methods

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SkillXPersonXSkillLevel, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("SkillXPersonXSkillLevelEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion

    }
}