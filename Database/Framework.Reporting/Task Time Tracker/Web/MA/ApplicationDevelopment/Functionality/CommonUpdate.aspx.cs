using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()   
        {
			var UpdatedData = new DataTable();
			var data = new FunctionalityDataModel();
			UpdatedData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FunctionalityId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityDataModel.DataColumns.FunctionalityId].ToString());

				data.FunctionalityActiveStatusId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityDataModel.DataColumns.FunctionalityActiveStatusId].ToString());

				data.FunctionalityPriorityId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityDataModel.DataColumns.FunctionalityPriorityId].ToString());

				data.Name =
					 !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
					 ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name)
					 : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FunctionalityDataModel();
				data.FunctionalityId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityDataModel.DataColumns.FunctionalityId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var functionalitydata = new FunctionalityDataModel();
            functionalitydata.FunctionalityId = entityKey;
            var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Search(functionalitydata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Functionality;
            PrimaryEntityKey = "Functionality";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}