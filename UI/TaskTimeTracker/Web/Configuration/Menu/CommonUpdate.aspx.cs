using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Configuration.Menu
{
    public partial class CommonUpdate : PageCommonUpdate
    {
		#region Methods		

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<MenuDataModel>();
            var data = new MenuDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.MenuId = Convert.ToInt32(SelectedData.Rows[i][MenuDataModel.DataColumns.MenuId].ToString());

                data.Name = Convert.ToString(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name]);
                data.ParentMenuId = Convert.ToInt32(SelectedData.Rows[i][MenuDataModel.DataColumns.ParentMenuId]);
                data.NavigateURL = Convert.ToString(SelectedData.Rows[i][MenuDataModel.DataColumns.NavigateURL]);
                data.Value = Convert.ToString(SelectedData.Rows[i][MenuDataModel.DataColumns.Value]);

                data.IsChecked =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(MenuDataModel.DataColumns.IsChecked))
                    ? Convert.ToInt32(CheckAndGetRepeaterTextBoxValue(MenuDataModel.DataColumns.IsChecked))
                    : Convert.ToInt32(SelectedData.Rows[i][MenuDataModel.DataColumns.IsChecked]);

                data.IsVisible =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(MenuDataModel.DataColumns.IsVisible))
                    ? Convert.ToInt32(CheckAndGetRepeaterTextBoxValue(MenuDataModel.DataColumns.IsVisible))
                    : Convert.ToInt32(SelectedData.Rows[i][MenuDataModel.DataColumns.IsVisible]);

                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.PrimaryDeveloper =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(MenuDataModel.DataColumns.PrimaryDeveloper))
                    ? CheckAndGetRepeaterTextBoxValue(MenuDataModel.DataColumns.PrimaryDeveloper)
                    : SelectedData.Rows[i][MenuDataModel.DataColumns.PrimaryDeveloper].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                if (string.IsNullOrEmpty(data.PrimaryDeveloper))
                {
                    data.PrimaryDeveloper = " ";
                }

                Framework.Components.Core.MenuDataManager.Update(data, SessionVariables.RequestProfile);
                data = new MenuDataModel();
                data.MenuId = Convert.ToInt32(SelectedData.Rows[i][MenuDataModel.DataColumns.MenuId].ToString());
				var dt = Framework.Components.Core.MenuDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }                
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var data = new MenuDataModel();
            data.MenuId = entityKey;
			var results = Framework.Components.Core.MenuDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity          = Framework.Components.DataAccess.SystemEntity.Menu;
            PrimaryEntityKey       = "Menu";
            BreadCrumbObject       = Master.BreadCrumbObject;
        }

        #endregion

	}
}