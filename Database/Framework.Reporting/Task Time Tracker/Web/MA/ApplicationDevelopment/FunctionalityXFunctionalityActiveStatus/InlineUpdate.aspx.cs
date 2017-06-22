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

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus
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
                var functionalityXFunctionalityActiveStatusdata = new FunctionalityXFunctionalityActiveStatusDataModel();

				selectedrows = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.GetDetails(functionalityXFunctionalityActiveStatusdata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        functionalityXFunctionalityActiveStatusdata.FunctionalityXFunctionalityActiveStatusId = entityKey;
						var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.GetDetails(functionalityXFunctionalityActiveStatusdata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    functionalityXFunctionalityActiveStatusdata.FunctionalityXFunctionalityActiveStatusId = SetId;
					var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.GetDetails(functionalityXFunctionalityActiveStatusdata, SessionVariables.RequestProfile);
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
            var data = new FunctionalityXFunctionalityActiveStatusDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);


            TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityActiveStatus;
            PrimaryEntityKey = "FunctionalityXFunctionalityActiveStatus";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}

