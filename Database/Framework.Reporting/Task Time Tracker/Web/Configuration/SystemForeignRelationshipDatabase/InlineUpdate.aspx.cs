using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipDatabase
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
                var systemForeignRelationshipDatabasedata = new SystemForeignRelationshipDatabaseDataModel();

				selectedrows = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetDetails(systemForeignRelationshipDatabasedata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
                        systemForeignRelationshipDatabasedata.SystemForeignRelationshipDatabaseId = entityKey;
						var result = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetDetails(systemForeignRelationshipDatabasedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
                    systemForeignRelationshipDatabasedata.SystemForeignRelationshipDatabaseId = SetId;
					var result = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetDetails(systemForeignRelationshipDatabasedata, SessionVariables.RequestProfile);
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
			var data = new SystemForeignRelationshipDatabaseDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipDatabase;
            PrimaryEntityKey = "SystemForeignRelationshipDatabase";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}