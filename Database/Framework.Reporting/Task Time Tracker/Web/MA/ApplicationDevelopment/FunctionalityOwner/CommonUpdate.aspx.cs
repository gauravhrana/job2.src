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
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityOwner
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();
            var data = new FunctionalityOwnerDataModel();
            UpdatedData = FunctionalityOwnerDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.FunctionalityOwnerId =
                    Convert.ToInt32(SelectedData.Rows[i][FunctionalityOwnerDataModel.DataColumns.FunctionalityOwnerId].ToString());
				data.FunctionalityId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityOwnerDataModel.DataColumns.FunctionalityId].ToString());
				data.FeatureOwnerStatusId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatusId].ToString());
                data.Developer =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityOwnerDataModel.DataColumns.Developer))
                    ? CheckAndGetRepeaterTextBoxValue(FunctionalityOwnerDataModel.DataColumns.Developer)
                    : SelectedData.Rows[i][FunctionalityOwnerDataModel.DataColumns.Developer].ToString();
				
                data.ApplicationId =
                    Convert.ToInt32(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());
                data.FeatureOwnerStatusId =
                    Convert.ToInt32(SelectedData.Rows[i][FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatusId].ToString());

                FunctionalityOwnerDataManager.Update(data, SessionVariables.RequestProfile);
                data = new FunctionalityOwnerDataModel();
                data.FunctionalityOwnerId =
                    Convert.ToInt32(SelectedData.Rows[i][FunctionalityOwnerDataModel.DataColumns.FunctionalityOwnerId].ToString());
                var dt = FunctionalityOwnerDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }

            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var FunctionalityOwnerdata = new FunctionalityOwnerDataModel();
            FunctionalityOwnerdata.FunctionalityOwnerId = entityKey;
            var results = FunctionalityOwnerDataManager.Search(FunctionalityOwnerdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityOwner;
            PrimaryEntityKey = "FunctionalityOwner";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}