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
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityOwner
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<EntityOwnerDataModel>();
            var data = new EntityOwnerDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.EntityOwnerId =
                    Convert.ToInt32(SelectedData.Rows[i][EntityOwnerDataModel.DataColumns.EntityOwnerId].ToString());

                data.Developer =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityOwnerDataModel.DataColumns.Developer))
                    ? CheckAndGetRepeaterTextBoxValue(EntityOwnerDataModel.DataColumns.Developer)
                    : SelectedData.Rows[i][EntityOwnerDataModel.DataColumns.Developer].ToString();

                data.ApplicationId =
                    Convert.ToInt32(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());
                data.DeveloperRoleId =
                    Convert.ToInt32(SelectedData.Rows[i][EntityOwnerDataModel.DataColumns.DeveloperRoleId].ToString());
				data.EntityId = Convert.ToInt32(SelectedData.Rows[i][EntityOwnerDataModel.DataColumns.EntityId].ToString());
				data.FeatureOwnerStatusId = Convert.ToInt32(SelectedData.Rows[i][EntityOwnerDataModel.DataColumns.FeatureOwnerStatusId].ToString());

                EntityOwnerDataManager.Update(data, SessionVariables.RequestProfile);
                data = new EntityOwnerDataModel();
                data.EntityOwnerId =
                    Convert.ToInt32(SelectedData.Rows[i][EntityOwnerDataModel.DataColumns.EntityOwnerId].ToString());
				var dt = EntityOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }

            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var EntityOwnerdata = new EntityOwnerDataModel();
            EntityOwnerdata.EntityOwnerId = entityKey;
			var results = EntityOwnerDataManager.GetEntityDetails(EntityOwnerdata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityOwner;
            PrimaryEntityKey = "EntityOwner";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}