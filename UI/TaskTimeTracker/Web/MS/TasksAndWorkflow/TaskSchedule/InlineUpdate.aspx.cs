using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.TasksAndWorkFlow;
using Framework.Components.Core;
using Dapper;

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskSchedule
{
    public partial class InlineUpdate : Shared.UI.WebFramework.BasePage
    {
        public delegate void UpdateDelegate(Dictionary<string, string> values);

        private DataTable GetData()
        {
            try
            {
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
                var selectedrows = new List<TaskScheduleDataModel>();
                var TaskScheduledata = new TaskScheduleDataModel();
                if (!string.IsNullOrEmpty(SuperKey))
                {


                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);
                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.TaskSchedule;
					var listSuperKeyDetails = SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        var keys = new int[listSuperKeyDetails.Count];
                        for (var i = 0; i < listSuperKeyDetails.Count; i++)
                        {

                            keys[i] = listSuperKeyDetails[i].EntityKey.Value;
                            TaskScheduledata.TaskScheduleId = keys[i];
							var result = Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.GetDetails(TaskScheduledata, SessionVariables.RequestProfile);
                            selectedrows.Add(result);
                        }
                    }
                }
                else
                {
                    var key = SetId;
                    TaskScheduledata.TaskScheduleId = key;
					var result = Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.GetDetails(TaskScheduledata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);
                }
                return selectedrows.ToDataTable();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }

        private string[] GetColumns()
        {

			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.TaskSchedule, "DBColumns", SessionVariables.RequestProfile);
        }

        protected override void OnInit(EventArgs e)
        {
            InlineEditingList.AddColumns(GetColumns());

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            

            SettingCategory = "TaskScheduleDefaultView";
           

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateDelegate delupdate = new UpdateDelegate(Update);
            this.InlineEditingList.DelUpdateRef = delupdate;
            if (!IsPostBack)
            {
                InlineEditingList.SetUp(GetColumns(), "TaskSchedule", GetData());
            }
        }

        private void Update(Dictionary<string, string> values)
        {
            var data = new TaskScheduleDataModel();
            data.TaskScheduleId = int.Parse(values[TaskScheduleDataModel.DataColumns.TaskScheduleId].ToString());
            data.TaskEntityId = int.Parse(values[TaskScheduleDataModel.DataColumns.TaskEntityId].ToString());
            data.TaskScheduleId = int.Parse(values[TaskScheduleDataModel.DataColumns.TaskScheduleTypeId].ToString());

			Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.Update(data, SessionVariables.RequestProfile);
            InlineEditingList.Data = GetData();
        }
    }
}