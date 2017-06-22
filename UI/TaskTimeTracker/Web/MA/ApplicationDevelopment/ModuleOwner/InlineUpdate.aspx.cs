using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner
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

				var moduleOwnerdata = new ModuleOwnerDataModel();
				var selectedrows = new List<ModuleOwnerDataModel>();

				if (!string.IsNullOrEmpty(SuperKey))
				{
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        moduleOwnerdata.ModuleOwnerId = entityKey;
						var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetDetails(moduleOwnerdata, SessionVariables.RequestProfile);
						selectedrows.Add(result);
                    }
                }
				else 
				{
					moduleOwnerdata.ModuleOwnerId = SetId;
					var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetDetails(moduleOwnerdata, SessionVariables.RequestProfile);
					selectedrows.Add(result);

				}
				return selectedrows.ToDataTable();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}

			return null;
		}

        protected override void Update(Dictionary<string, string> values)
		{
			var data = new ModuleOwnerDataModel();
			var datas= GetData();
            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);
			data.TotalHoursWorked = (int)datas.Rows[0][ModuleOwnerDataModel.DataColumns.TotalHoursWorked];
			data.ApplicationId = (int)datas.Rows[0][ModuleOwnerDataModel.DataColumns.ApplicationId];
			TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
            PrimaryEntity         = Framework.Components.DataAccess.SystemEntity.ModuleOwner;
			PrimaryEntityKey      = "ModuleOwner";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;
			
			base.OnInit(e);
        }

        #endregion

    }
}