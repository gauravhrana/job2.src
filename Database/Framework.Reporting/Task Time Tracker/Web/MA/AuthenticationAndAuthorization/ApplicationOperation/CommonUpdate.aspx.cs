using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.AuthenticationAndAuthorization.ApplicationOperation
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();
			var data = new ApplicationOperationDataModel();
			UpdatedData = Framework.Components.ApplicationUser.ApplicationOperationDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ApplicationOperationId =
					Convert.ToInt32(SelectedData.Rows[i][ApplicationOperationDataModel.DataColumns.ApplicationOperationId].ToString());
				data.Name = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name).ToString()
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description).ToString()
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.OperationValue =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationOperationDataModel.DataColumns.OperationValue))
					? CheckAndGetRepeaterTextBoxValue(ApplicationOperationDataModel.DataColumns.OperationValue).ToString()
					: SelectedData.Rows[i][ApplicationOperationDataModel.DataColumns.OperationValue].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.SystemEntityTypeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationOperationDataModel.DataColumns.SystemEntityTypeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationOperationDataModel.DataColumns.SystemEntityTypeId).ToString())
					: int.Parse(SelectedData.Rows[i][ApplicationOperationDataModel.DataColumns.SystemEntityTypeId].ToString());

				Framework.Components.ApplicationUser.ApplicationOperationDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ApplicationOperationDataModel();
				data.ApplicationOperationId = Convert.ToInt32(SelectedData.Rows[i][ApplicationOperationDataModel.DataColumns.ApplicationOperationId].ToString());
				var dt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			 }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationOperationdata = new ApplicationOperationDataModel();
            applicationOperationdata.ApplicationOperationId = entityKey;
			var results = Framework.Components.ApplicationUser.ApplicationOperationDataManager.Search(applicationOperationdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationOperation;
            PrimaryEntityKey = "ApplicationOperation";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}