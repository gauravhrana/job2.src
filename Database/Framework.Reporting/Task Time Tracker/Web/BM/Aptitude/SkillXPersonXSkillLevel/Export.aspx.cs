using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {

        #region variables

        string searchCondition = String.Empty;

        #endregion

        #region private methods

        private System.Data.DataTable GetData()
        {
            // TODO: on all export pages 
			var data = new DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel();

            var dt = SkillXPersonXSkillLevelDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetColumns()
        {
			var validColumns = FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.SkillXPersonXSkillLevel, "Default", SessionVariables.RequestProfile);
            return validColumns;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // searchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("SkillXPersonXSkillLevel", " ", "SkillXPersonXSkillLevelId", false, GetData, GetColumns, false);
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

        #endregion

    }
}