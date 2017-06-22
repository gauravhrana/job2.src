using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.TasksAndWorkflow.TaskRun
{
    public partial class Default : Shared.UI.WebFramework.BasePage
    {

        #region private methods

        private DataTable GetData()
        {
            var dt = Framework.Components.TasksAndWorkflow.TaskRun.Search(oSearchFilter.SearchParameters, AuditId);
            return dt;
        }

        private string[] GetColumns()
        {
			if (!oList.FieldConfigurationMode.Equals(string.Empty))
				return Framework.Components.ApplicationSecurity.GetTaskRunColumns(oList.FieldConfigurationMode, AuditId);
			else
				return Framework.Components.ApplicationSecurity.GetTaskRunColumns("DBColumns", AuditId);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SettingCategory = "TaskRunDefaultView";
                oSearchFilter.SettingCategory = SettingCategory + "SearchControl";
                oList.SettingCategory = SettingCategory + "ListControl";
            }
            oList.Setup("TaskRun", "TasksAndWorkflow", "TaskRunId", true, GetData, GetColumns, "TaskRun");
            oList.ExportMenu.Setup("TaskRun", "TasksAndWorkflow", GetData, GetColumns, oSearchFilter.SearchParameters.ToURLQuery());

            oSearchFilter.OnSearch += oSearchFilter_OnSearch;

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var sbm = this.Master.SubMenuObject;
            var bcControl = this.Master.BreadCrumbObject;

            Master.Setup("TaskRun");
            sbm.SettingCategory = SettingCategory + "SubMenuControl";
            sbm.Setup();
            sbm.GenerateMenu();

            bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
            bcControl.Setup("");
            bcControl.GenerateMenu();
        }

        void oSearchFilter_OnSearch(object sender, EventArgs e)
        {
            oList.ShowData(false, true);
        }       

        #endregion

    }
}