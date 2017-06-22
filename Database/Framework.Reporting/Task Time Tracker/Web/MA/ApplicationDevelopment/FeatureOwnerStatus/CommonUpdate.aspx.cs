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
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FeatureOwnerStatus
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()      
        {
            var UpdatedData = new DataTable();
            var data = new FeatureOwnerStatusDataModel();
			UpdatedData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.FeatureOwnerStatusId =
                    Convert.ToInt32(SelectedData.Rows[i][FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId].ToString());

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

				TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.Update(data, SessionVariables.RequestProfile);
                data = new FeatureOwnerStatusDataModel();
                data.FeatureOwnerStatusId = Convert.ToInt32(SelectedData.Rows[i][FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.Search(data, SessionVariables.RequestProfile);


                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var featureOwnerStatusdata = new FeatureOwnerStatusDataModel();
            featureOwnerStatusdata.FeatureOwnerStatusId = entityKey;
			var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.Search(featureOwnerStatusdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureOwnerStatus;
            PrimaryEntityKey = "FeatureOwnerStatus";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}