using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using System.Text;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Dapper;

namespace Shared.UI.Web.Configuration.ThemeKey
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<ThemeKeyDataModel>();
            var data = new ThemeKeyDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {

                data.ThemeKeyId =
                    Convert.ToInt32(SelectedData.Rows[i][ThemeKeyDataModel.DataColumns.ThemeKeyId].ToString());

                data.SortOrder =					
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());


                data.Name =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				Framework.Components.Core.ThemeKeyDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ThemeKeyDataModel();
                data.ThemeKeyId =
                    Convert.ToInt32(SelectedData.Rows[i][ThemeKeyDataModel.DataColumns.ThemeKeyId].ToString());
				var dt = Framework.Components.Core.ThemeKeyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }

            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var ThemeKeydata = new ThemeKeyDataModel();
            ThemeKeydata.ThemeKeyId = entityKey;
			var results = Framework.Components.Core.ThemeKeyDataManager.GetEntityDetails(ThemeKeydata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ThemeKey;
            PrimaryEntityKey = "ThemeKey";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}