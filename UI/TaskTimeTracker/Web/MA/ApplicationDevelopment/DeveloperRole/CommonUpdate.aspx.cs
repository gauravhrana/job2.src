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
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.DeveloperRole
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<DeveloperRoleDataModel>(); 
			var data = new DeveloperRoleDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.DeveloperRoleId =
					Convert.ToInt32(SelectedData.Rows[i][DeveloperRoleDataModel.DataColumns.DeveloperRoleId].ToString()); 
				data.Name = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new DeveloperRoleDataModel();
				data.DeveloperRoleId = Convert.ToInt32(SelectedData.Rows[i][DeveloperRoleDataModel.DataColumns.DeveloperRoleId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);


                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var developerRoledata = new DeveloperRoleDataModel();
            developerRoledata.DeveloperRoleId = entityKey;
			var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetEntityDetails(developerRoledata,
				SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DeveloperRole;
            PrimaryEntityKey = "DeveloperRole";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}