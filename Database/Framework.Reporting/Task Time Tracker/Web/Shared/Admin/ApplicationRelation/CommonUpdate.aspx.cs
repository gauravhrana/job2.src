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

namespace Shared.UI.Web.Admin.ApplicationRelation
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()	
		{
			var UpdatedData = new DataTable();
			var data = new ApplicationRelationDataModel();
			UpdatedData = Framework.Components.Core.ApplicationRelationDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ApplicationRelationId =
					Convert.ToInt32(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.ApplicationRelationId].ToString());
				
				data.PublisherApplicationId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.PublisherApplicationId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.PublisherApplicationId).ToString())
					: int.Parse(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.PublisherApplicationId].ToString());

				data.SubscriberApplicationId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationId).ToString())
					: int.Parse(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SubscriberApplicationId].ToString());

				data.SystemEntityTypeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SystemEntityTypeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SystemEntityTypeId).ToString())
					: int.Parse(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SystemEntityTypeId].ToString());

				data.SubscriberApplicationRoleId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId).ToString())
					: int.Parse(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId].ToString());

				Framework.Components.Core.ApplicationRelationDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ApplicationRelationDataModel();
				data.ApplicationRelationId = Convert.ToInt32(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.ApplicationRelationId].ToString());
				var dt = Framework.Components.Core.ApplicationRelationDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var ApplicationRelationdata = new ApplicationRelationDataModel();
            ApplicationRelationdata.ApplicationRelationId = entityKey;
			var results = Framework.Components.Core.ApplicationRelationDataManager.Search(ApplicationRelationdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRelation;
            PrimaryEntityKey = "ApplicationRelation";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}