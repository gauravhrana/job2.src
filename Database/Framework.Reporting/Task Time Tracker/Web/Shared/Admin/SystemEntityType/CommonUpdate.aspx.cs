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

namespace Shared.UI.Web.Admin.SystemEntityType
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()      
	    {
			var UpdatedData = new DataTable();
			var data = new SystemEntityTypeDataModel();
			UpdatedData = Framework.Components.Core.SystemEntityTypeDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.SystemEntityTypeId =
					Convert.ToInt32(SelectedData.Rows[i][SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId].ToString());
				data.EntityName = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemEntityTypeDataModel.DataColumns.EntityName))
					? CheckAndGetRepeaterTextBoxValue(SystemEntityTypeDataModel.DataColumns.EntityName).ToString()
					: SelectedData.Rows[i][SystemEntityTypeDataModel.DataColumns.EntityName].ToString();

				data.EntityDescription =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemEntityTypeDataModel.DataColumns.EntityDescription))
					? CheckAndGetRepeaterTextBoxValue(SystemEntityTypeDataModel.DataColumns.EntityDescription).ToString()
					: SelectedData.Rows[i][SystemEntityTypeDataModel.DataColumns.EntityDescription].ToString();

				data.NextValue =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemEntityTypeDataModel.DataColumns.NextValue))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(SystemEntityTypeDataModel.DataColumns.NextValue).ToString())
					: int.Parse(SelectedData.Rows[i][SystemEntityTypeDataModel.DataColumns.NextValue].ToString());

				data.IncreaseBy =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemEntityTypeDataModel.DataColumns.IncreaseBy))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(SystemEntityTypeDataModel.DataColumns.IncreaseBy).ToString())
					: int.Parse(SelectedData.Rows[i][SystemEntityTypeDataModel.DataColumns.IncreaseBy].ToString());

				Framework.Components.Core.SystemEntityTypeDataManager.Update(data, SessionVariables.RequestProfile);
				data = new SystemEntityTypeDataModel();
				data.SystemEntityTypeId = Convert.ToInt32(SelectedData.Rows[i][SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId].ToString());
				var dt = Framework.Components.Core.SystemEntityTypeDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var systemEntityTypedata = new SystemEntityTypeDataModel();
            systemEntityTypedata.SystemEntityTypeId = entityKey;
			var results = Framework.Components.Core.SystemEntityTypeDataManager.Search(systemEntityTypedata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityType;
            PrimaryEntityKey = "SystemEntityType";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}