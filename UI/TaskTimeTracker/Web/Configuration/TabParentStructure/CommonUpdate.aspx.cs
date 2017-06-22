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

namespace Shared.UI.Web.Configuration.TabParentStructure
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Method

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<TabParentStructureDataModel>();
            var data = new TabParentStructureDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TabParentStructureId =
                    Convert.ToInt32(SelectedData.Rows[i][TabParentStructureDataModel.DataColumns.TabParentStructureId].ToString());
               
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

                data.IsAllTab =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TabParentStructureDataModel.DataColumns.IsAllTab))
                    ? decimal.Parse(CheckAndGetRepeaterTextBoxValue(TabParentStructureDataModel.DataColumns.IsAllTab).ToString())
                    : decimal.Parse(SelectedData.Rows[i][TabParentStructureDataModel.DataColumns.IsAllTab].ToString());

				Framework.Components.Core.TabParentStructureDataManager.Update(data, SessionVariables.RequestProfile);
                data = new TabParentStructureDataModel();
                data.TabParentStructureId = Convert.ToInt32(SelectedData.Rows[i][TabParentStructureDataModel.DataColumns.TabParentStructureId].ToString());
				var dt = Framework.Components.Core.TabParentStructureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
               }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var tabParentStructuredata = new TabParentStructureDataModel();
            tabParentStructuredata.TabParentStructureId = entityKey;
			var results = Framework.Components.Core.TabParentStructureDataManager.GetEntityDetails(tabParentStructuredata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TabParentStructure;
            PrimaryEntityKey = "TabParentStructure";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion


    }
}