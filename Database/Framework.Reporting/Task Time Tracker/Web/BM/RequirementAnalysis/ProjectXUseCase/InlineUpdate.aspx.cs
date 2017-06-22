using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.ProjectXUseCase
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

                var projectXUseCasedata = new ProjectXUseCaseDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.GetDetails(projectXUseCasedata, SessionVariables.RequestProfile).Clone();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						projectXUseCasedata.ProjectXUseCaseId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.GetDetails(projectXUseCasedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					projectXUseCasedata.ProjectXUseCaseId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.GetDetails(projectXUseCasedata, SessionVariables.RequestProfile);
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
            var data = new ProjectXUseCaseDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectXUseCase;
            PrimaryEntityKey = "ProjectXUseCase";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}