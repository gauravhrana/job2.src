using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityStatus
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
                var functionalityStatusdata = new FunctionalityStatusDataModel();

                selectedrows = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityStatusDataManager.GetDetails(functionalityStatusdata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        functionalityStatusdata.FunctionalityStatusId = entityKey;
                        var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityStatusDataManager.GetDetails(functionalityStatusdata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    functionalityStatusdata.FunctionalityStatusId = SetId;
                    var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityStatusDataManager.GetDetails(functionalityStatusdata, SessionVariables.RequestProfile);
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
            var data = new FunctionalityStatusDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityStatusDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityStatus;
            PrimaryEntityKey = "FunctionalityStatus";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}


