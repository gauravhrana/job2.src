using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Project
{
    public partial class Export : BasePage
    {

        #region private methods

        private DataTable GetData()
        {
			var data = new ProjectDataModel();
            var dt = ProjectDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Project, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            oList.Setup("Project", " ", "ProjectId", false, GetData, GetColumns, false);
            oList.ExportMenu.Visible = false;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            oList.ShowData(true, true);
        }

        #endregion
    }
}