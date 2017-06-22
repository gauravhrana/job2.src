using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact
{
    public partial class Insert : PageInsert
    {

        #region private methods

        protected void InsertData()
        {
            var data = new TaskXDeliverableArtifactDataModel();

            data.TaskXDeliverableArtifactId = myGenericControl.TaskXDeliverableArtifactId;
            data.TaskId = myGenericControl.TaskId;
            data.DeliverableArtifactStatusId = myGenericControl.DeliverableArtifactStatusId;
            data.DeliverableArtifactId = myGenericControl.DeliverableArtifactsId;

            TaskXDeliverableArtifactDataManager.Create(data, SessionVariables.RequestProfile);
        }

        private int GetNextValidId(int tempId)
        {
            var dt = TaskXDeliverableArtifactDataManager.GetList(SessionVariables.RequestProfile);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[TaskXDeliverableArtifactDataModel.DataColumns.TaskXDeliverableArtifactId].ToString().Equals(tempId.ToString()))
                {
                    tempId -= 1;
                    return GetNextValidId(tempId);
                }
            }

            return tempId;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            SettingCategory = "TaskXDeliverableArtifactDefaultView";
            

        }

        

        

        #endregion
    }
}