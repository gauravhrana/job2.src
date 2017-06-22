using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact
{
	public partial class InlineUpdate : PageInlineUpdate
    {
		#region Methods

        protected override DataTable GetData()
		{
			try
			{
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
				
                var selectedrows = new DataTable();
				var taskXDeliverableArtifactdata = new TaskXDeliverableArtifactDataModel();

                selectedrows = TaskXDeliverableArtifactDataManager.GetDetails(taskXDeliverableArtifactdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        taskXDeliverableArtifactdata.TaskXDeliverableArtifactId = entityKey;
                        var result = TaskXDeliverableArtifactDataManager.GetDetails(taskXDeliverableArtifactdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
                    }
                }
				else 
				{
					taskXDeliverableArtifactdata.TaskXDeliverableArtifactId = SetId;
                    var result = TaskXDeliverableArtifactDataManager.GetDetails(taskXDeliverableArtifactdata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);

				}
				return selectedrows;
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}

			return null;
		}

        protected override void Update(Dictionary<string, string> values)
		{
           
			var data = new TaskXDeliverableArtifactDataModel();

            PropertyMapper.CopyProperties(data, values);

            TaskXDeliverableArtifactDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
            PrimaryEntity         = SystemEntity.TaskXDeliverableArtifact;
			PrimaryEntityKey      = "TaskXDeliverableArtifact";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;
			
			base.OnInit(e);
        }

        #endregion

    }
}
       