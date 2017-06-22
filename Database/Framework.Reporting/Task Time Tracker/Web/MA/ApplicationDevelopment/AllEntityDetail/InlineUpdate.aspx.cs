using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetail
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
				var allEntityDetaildata = new AllEntityDetailDataModel();

				selectedrows = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.GetDetails(allEntityDetaildata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						allEntityDetaildata.AllEntityDetailId = entityKey;
						var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.GetDetails(allEntityDetaildata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					allEntityDetaildata.AllEntityDetailId = SetId;
					var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.GetDetails(allEntityDetaildata, SessionVariables.RequestProfile);
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
			var data = new AllEntityDetailDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AllEntityDetail;
            PrimaryEntityKey = "AllEntityDetail";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}

