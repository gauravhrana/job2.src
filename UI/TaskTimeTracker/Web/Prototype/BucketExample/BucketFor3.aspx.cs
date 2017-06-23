using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.TaskTimeTracker.Competency;
using TaskTimeTracker.Components.Module.Competency;

namespace Shared.UI.Web.ApplicationManagement.Development.BucketExample
{
	public partial class BucketFor3 : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region variables

        public int setId = -999999;

        #endregion

        #region methods

        private List<SkillLevelDataModel> GetSkillLevelList()
        {		
			var dt = SkillLevelDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
        }

        private List<ApplicationUserDataModel> GetApplicationUserList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
        }

        private DataTable GetAssociatedRecords(int skillId)
        {
			//var data = new Components.SkillXPersonXSkillLevel.Data();
			//data.SkillId = skillId;
			//var dt = SkillXPersonXSkillLevelDataManager.Search(data, SessionVariables.RequestProfile.AuditId);
			//return dt;

			return new DataTable();
        }

        private void RemoveBySkill(int skillId)
        {
			//var data = new Components.SkillXPersonXSkillLevel.Data();
			//data.SkillId = skillId;
			//SkillXPersonXSkillLevelDataManager.Delete(data, SessionVariables.RequestProfile.AuditId);
        }

        private void SaveSkillXPersonXSkillLevel(int skillId, int applicationUserId, int skillLevelId)
        {
			//var data = new Components.SkillXPersonXSkillLevel.Data();
			//data.SkillId = skillId;
			//data.PersonId = applicationUserId;
			//data.SkillLevelId = skillLevelId;
			//SkillXPersonXSkillLevelDataManager.Create(data, SessionVariables.RequestProfile.AuditId);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            try
            {
                var key = setId;

                var detailTabControlPath = ApplicationCommon.DetailTabControlPath;
                var bucketControlPath = ApplicationCommon.BucketFor3ControlPath;

                var tabControl = (Shared.UI.Web.Controls.DetailTabControl)Page.LoadControl(detailTabControlPath);
                tabControl.Setup("SkillXPersonXSkillLevelDetailsView");           

                var selected = true;
                if (Request.QueryString["tab"] == "2")
                {
                    selected = true;
                }

                var bucketControl = (Shared.UI.Web.Controls.BucketFor3Control)Page.LoadControl(bucketControlPath);
                bucketControl.ConfigureBucket("ApplicationUser", "SkillLevel", "FullName", "Name", key,
                    GetApplicationUserList, GetSkillLevelList, GetAssociatedRecords, SaveSkillXPersonXSkillLevel, RemoveBySkill);

                tabControl.AddTab("SkillXPersonXSkillLevel", bucketControl, String.Empty, selected);

                //selected = false;
                //if (Request.QueryString["tab"] == "3")
                //{
                //    selected = true;
                //}

                //var bucketControl1 = (Shared.UI.Web.Controls.Bucket)Page.LoadControl(bucketControlPath);
                //bucketControl1.ConfigureBucket("ApplicationUser", key, 3, GetApplicationUserList, GetAssociatedApplicationUsers, SaveApplicationUserXApplicationRole);

                //tabControl.AddTab("ApplicationUser", selected, bucketControl1);

                plcUpdateList.Controls.Add(tabControl);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        #endregion

    }
}