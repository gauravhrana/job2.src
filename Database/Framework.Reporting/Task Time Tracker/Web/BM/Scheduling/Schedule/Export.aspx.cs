using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;


namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{
    public partial class Export : BasePage
    {
        string searchCondition = String.Empty;

        private DataTable GetData()
        {
            // TODO: on all export pages 
            var data = new ScheduleDataModel();

            var dt = ScheduleDataManager.Search(data,SessionVariables.RequestProfile);

            return dt;
        }

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Schedule, "DBColumns", SessionVariables.RequestProfile);
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // searchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("Schedule", " ", "ScheduleId", false, GetData, GetColumns, false);
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
            oList.ShowData(true, true);
        }

    }
}