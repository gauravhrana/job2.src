using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Need
{
    public partial class Update : PageUpdate
    {

        #region Methods

        protected override Control GetTabControl(int setId, Control updateControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("NeedUpdateView");

            var selected = false;

            if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
            {
                selected = true;
            }

            tabControl.AddTab("Need", updateControl, String.Empty, selected);

            // not making sense ?
            selected = false;

            if (Request.QueryString["tab"] == "2")
            {
                selected = true;
            }

            if (Request.QueryString["tab"] == "3")
            {
                selected = true;
            }

            var bucketControl = ApplicationCommon.GetNewBucketControl();
            bucketControl.ConfigureBucket("Project", setId, GetProjectList, GetAssociatedProjects, SaveProjectXNeed);
            tabControl.AddTab("Project", bucketControl, String.Empty, selected);

            return tabControl;
        }

        private DataTable GetProjectList()
        {
            var dt = ProjectDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedProjects(int needId)
        {
            var dt = ProjectXNeedDataManager.GetByNeed(needId, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveProjectXNeed(int needId, List<int> projectIds)
        {
            ProjectXNeedDataManager.DeleteByNeed(needId, SessionVariables.RequestProfile);
            ProjectXNeedDataManager.CreateByNeed(needId, projectIds.ToArray(), SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.Need;

            GenericControlPath = ApplicationCommon.GetControlPath("Need", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "Need";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;
        }

        #endregion

    }
}