using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.ThemeCategory
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Method

        protected override DataTable UpdateData()	      
        {
            var UpdatedData = new DataTable();
            var data = new ThemeCategoryDataModel();
			UpdatedData = Framework.Components.Core.ThemeCategoryDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ThemeCategoryId =
                    Convert.ToInt32(SelectedData.Rows[i][ThemeCategoryDataModel.DataColumns.ThemeCategoryId].ToString());

                data.Name =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name).ToString()
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description).ToString()
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.Core.ThemeCategoryDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ThemeCategoryDataModel();
                data.ThemeCategoryId = Convert.ToInt32(SelectedData.Rows[i][ThemeCategoryDataModel.DataColumns.ThemeCategoryId].ToString());
				var dt = Framework.Components.Core.ThemeCategoryDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var themeCategorydata = new ThemeCategoryDataModel();
            themeCategorydata.ThemeCategoryId = entityKey;
			var results = Framework.Components.Core.ThemeCategoryDataManager.Search(themeCategorydata, SessionVariables.RequestProfile);
            return results;
        }
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ThemeCategory;
            PrimaryEntityKey = "ThemeCategory";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}