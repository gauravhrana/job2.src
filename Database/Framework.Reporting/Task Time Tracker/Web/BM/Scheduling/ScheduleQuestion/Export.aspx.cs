using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {
        string searchCondition = String.Empty;

        private System.Data.DataTable GetData()
        {
            // TODO: on all export pages 
            var data = new ScheduleQuestionDataModel();

            var dt = ScheduleQuestionDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ScheduleQuestion, "DBColumns", SessionVariables.RequestProfile);
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // searchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("ScheduleQuestion", " ", "ScheduleQuestionId", false, GetData, GetColumns, false);
				oList.ExportMenu.Visible = false;
			}
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            oList.ShowData(true, true);
        }

    }
}