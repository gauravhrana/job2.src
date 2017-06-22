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
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.DeliverableArtifact
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
                var deliverableArtifactdata = new DeliverableArtifactDataModel();

                selectedrows = DeliverableArtifactDataManager.GetDetails(deliverableArtifactdata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        deliverableArtifactdata.DeliverableArtifactId = entityKey;
                        var result = DeliverableArtifactDataManager.GetDetails(deliverableArtifactdata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    deliverableArtifactdata.DeliverableArtifactId = SetId;
                    var result = DeliverableArtifactDataManager.GetDetails(deliverableArtifactdata, SessionVariables.RequestProfile);
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
            var data = new DeliverableArtifactDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            DeliverableArtifactDataManager.Update(data, SessionVariables.RequestProfile);
            InlineEditingList.Data = GetData();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DeliverableArtifact;
            PrimaryEntityKey = "DeliverableArtifact";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}