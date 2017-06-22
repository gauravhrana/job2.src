using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ActivityXDeliverableArtifact
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new ActivityXDeliverableArtifactDataModel();
            UpdatedData = ActivityXDeliverableArtifactDataManager.Search(data, SessionVariables.RequestProfile).Clone();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ActivityXDeliverableArtifactId =
					Convert.ToInt32(SelectedData.Rows[i][ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId].ToString());
				data.ActivityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId))
					: int.Parse(SelectedData.Rows[i][ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId].ToString());
				data.DeliverableArtifactId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId))
					: int.Parse(SelectedData.Rows[i][ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId].ToString());

				data.DeliverableArtifactStatusId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId).ToString())
					: int.Parse(SelectedData.Rows[i][ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId].ToString());

                ActivityXDeliverableArtifactDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ActivityXDeliverableArtifactDataModel();
				data.ActivityXDeliverableArtifactId = Convert.ToInt32(SelectedData.Rows[i][ActivityXDeliverableArtifactDataModel.DataColumns.ActivityXDeliverableArtifactId].ToString());
                var dt = ActivityXDeliverableArtifactDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}				
			}
			return UpdatedData;
		}        		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var activityXDeliverableArtifactdata = new ActivityXDeliverableArtifactDataModel();
			activityXDeliverableArtifactdata.ActivityXDeliverableArtifactId = entityKey;
            var results = ActivityXDeliverableArtifactDataManager.Search(activityXDeliverableArtifactdata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ActivityXDeliverableArtifact;
			PrimaryEntityKey = "ActivityXDeliverableArtifact";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
    }
}