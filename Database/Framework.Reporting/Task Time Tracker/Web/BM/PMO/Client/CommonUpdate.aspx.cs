using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Client
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new ClientDataModel();
            UpdatedData = ClientDataManager.Search(data, SessionVariables.RequestProfile).Clone();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ClientId =
                    Convert.ToInt32(SelectedData.Rows[i][ClientDataModel.DataColumns.ClientId].ToString());
                
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                ClientDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ClientDataModel();
                
				data.ClientId = Convert.ToInt32(SelectedData.Rows[i][ClientDataModel.DataColumns.ClientId].ToString());
                
				var dt = ClientDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }

            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var clientData = new ClientDataModel();
            clientData.ClientId = entityKey;
            var results = ClientDataManager.Search(clientData, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = SystemEntity.Client;
            PrimaryEntityKey = "Client";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}