﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ActivityXDeliverableArtifact
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
                var activityXDeliverableArtifactdata = new ActivityXDeliverableArtifactDataModel();

                selectedrows = ActivityXDeliverableArtifactDataManager.GetDetails(activityXDeliverableArtifactdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						activityXDeliverableArtifactdata.ActivityXDeliverableArtifactId = entityKey;
                        var result = ActivityXDeliverableArtifactDataManager.GetDetails(activityXDeliverableArtifactdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
                }
                else
                {
					activityXDeliverableArtifactdata.ActivityXDeliverableArtifactId = SetId;
                    var result = ActivityXDeliverableArtifactDataManager.GetDetails(activityXDeliverableArtifactdata, SessionVariables.RequestProfile);
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
            var data = new ActivityXDeliverableArtifactDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            ActivityXDeliverableArtifactDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ActivityXDeliverableArtifact;
            PrimaryEntityKey = "ActivityXDeliverableArtifact";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}