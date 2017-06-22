using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Configuration.TabChildStructure
{
    public partial class CommonUpdate : PageCommonUpdate
    {
		#region Methods		

        protected override DataTable UpdateData()
        {      
            var UpdatedData = new List<TabChildStructureDataModel>();
            var data = new TabChildStructureDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TabChildStructureId =
                    Convert.ToInt32(SelectedData.Rows[i][TabChildStructureDataModel.DataColumns.TabChildStructureId].ToString());
                data.Name = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.Name))
                    ? CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.Name).ToString()
                    : SelectedData.Rows[i][TabChildStructureDataModel.DataColumns.Name].ToString();

                data.EntityName =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.EntityName))
                    ? CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.EntityName).ToString()
                    : SelectedData.Rows[i][TabChildStructureDataModel.DataColumns.EntityName].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][TabChildStructureDataModel.DataColumns.SortOrder].ToString());

                data.TabParentStructureId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.TabParentStructureId))
                    ? decimal.Parse(CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.TabParentStructureId).ToString())
                    : decimal.Parse(SelectedData.Rows[i][TabChildStructureDataModel.DataColumns.TabParentStructureId].ToString());

                data.InnerControlPath =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.InnerControlPath))
                    ? CheckAndGetRepeaterTextBoxValue(TabChildStructureDataModel.DataColumns.InnerControlPath).ToString()
                    : SelectedData.Rows[i][TabChildStructureDataModel.DataColumns.InnerControlPath].ToString();

                Framework.Components.Core.TabChildStructureDatManager.Update(data, SessionVariables.RequestProfile);
                data = new TabChildStructureDataModel();
                data.TabChildStructureId = Convert.ToInt32(SelectedData.Rows[i][TabChildStructureDataModel.DataColumns.TabChildStructureId].ToString());
				var dt = Framework.Components.Core.TabChildStructureDatManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var tabChildStructuredata = new TabChildStructureDataModel();
            tabChildStructuredata.TabChildStructureId = entityKey;
			var results = Framework.Components.Core.TabChildStructureDatManager.GetEntityDetails(tabChildStructuredata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TabChildStructure;
            PrimaryEntityKey = "TabChildStructure";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion


    }
}