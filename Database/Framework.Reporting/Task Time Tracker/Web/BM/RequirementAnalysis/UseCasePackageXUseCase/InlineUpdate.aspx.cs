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

namespace ApplicationContainer.UI.Web.UseCasePackageXUseCase
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

				var useCasePackageXUseCasedata = new UseCasePackageXUseCaseDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.GetDetails(useCasePackageXUseCasedata, SessionVariables.RequestProfile).Clone();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						useCasePackageXUseCasedata.UseCasePackageXUseCaseId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.GetDetails(useCasePackageXUseCasedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else if (SetId != 0)
				{
					useCasePackageXUseCasedata.UseCasePackageXUseCaseId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.GetDetails(useCasePackageXUseCasedata, SessionVariables.RequestProfile);
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

		protected override string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(PrimaryEntity, "DBColumns", SessionVariables.RequestProfile);
		}

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new UseCasePackageXUseCaseDataModel();
			data.UseCasePackageXUseCaseId	 = int.Parse(values[UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId].ToString());
			data.UseCasePackageId			 = int.Parse(values[UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId].ToString());
			data.UseCaseId					 = int.Parse(values[UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId].ToString());

            TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCasePackageXUseCase;
			PrimaryEntityKey = "UseCasePackageXUseCase";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}

}