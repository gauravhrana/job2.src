using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact
{
    public partial class CommonUpdate : PageCommonUpdate
    {
		#region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new TaskXDeliverableArtifactDataModel();

            UpdatedData = TaskXDeliverableArtifactDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
				data.TaskXDeliverableArtifactId =
								Convert.ToInt32(SelectedData.Rows[i][TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId].ToString());
				data.TaskId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskXDeliverableArtifactDataModel.DataColumns.TaskId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskXDeliverableArtifactDataModel.DataColumns.TaskId))
					: int.Parse(SelectedData.Rows[i][TaskXDeliverableArtifactDataModel.DataColumns.TaskId].ToString());
				data.DeliverableArtifactId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId))
					: int.Parse(SelectedData.Rows[i][TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId].ToString());

				data.DeliverableArtifactStatusId =
								!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId))
								? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId).ToString())
								: int.Parse(SelectedData.Rows[i][TaskXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId].ToString());

				TaskXDeliverableArtifactDataManager.Update(data, SessionVariables.RequestProfile);
                data = new TaskXDeliverableArtifactDataModel();
                data.TaskXDeliverableArtifactId = Convert.ToInt32(SelectedData.Rows[i][TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId].ToString());
                var dt = TaskXDeliverableArtifactDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var TaskXDeliverableArtifactdata = new TaskXDeliverableArtifactDataModel();
            TaskXDeliverableArtifactdata.TaskXDeliverableArtifactId = entityKey;
            var results = TaskXDeliverableArtifactDataManager.Search(TaskXDeliverableArtifactdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = SystemEntity.TaskXDeliverableArtifact;
            PrimaryEntityKey = "TaskXDeliverableArtifact";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion	
		
       
    }
}