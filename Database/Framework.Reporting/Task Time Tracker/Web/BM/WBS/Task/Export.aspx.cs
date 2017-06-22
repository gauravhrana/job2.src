using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.WBS.Task
{
    public partial class Export : BasePage
    {
        string searchCondition = String.Empty;

        private DataTable GetData()
        {
            // TODO: on all export pages 
            var data = new TaskDataModel();

            var dt = TaskDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Task, "DBColumns", SessionVariables.RequestProfile);
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // searchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("Task", " ", "TaskId", false, GetData, GetColumns, false);
				oList.ExportMenu.Visible = false;
			}
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!IsPostBack)
            {
                oList.ShowData(true, true);
            }
        }
    }
}