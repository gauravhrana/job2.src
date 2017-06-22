using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.EntityDateRangeStateType
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
				var entityDateRangeStateTypedata = new EntityDateRangeStateTypeDataModel();

                selectedrows = EntityDateRangeStateTypeDataManager.GetDetails(entityDateRangeStateTypedata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						entityDateRangeStateTypedata.EntityDateRangeStateTypeId = entityKey;
                        var result = EntityDateRangeStateTypeDataManager.GetDetails(entityDateRangeStateTypedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					entityDateRangeStateTypedata.EntityDateRangeStateTypeId = SetId;
                    var result = EntityDateRangeStateTypeDataManager.GetDetails(entityDateRangeStateTypedata, SessionVariables.RequestProfile);
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
			var data = new EntityDateRangeStateTypeDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            EntityDateRangeStateTypeDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityDateRangeStateType;
            PrimaryEntityKey = "EntityDateRangeStateType";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}