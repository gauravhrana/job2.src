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

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipType
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
                var systemForeignRelationshipTypedata = new SystemForeignRelationshipTypeDataModel();

				selectedrows = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.GetDetails(systemForeignRelationshipTypedata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
                        systemForeignRelationshipTypedata.SystemForeignRelationshipTypeId = entityKey;
						var result = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.GetDetails(systemForeignRelationshipTypedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
                    systemForeignRelationshipTypedata.SystemForeignRelationshipTypeId = SetId;
					var result = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.GetDetails(systemForeignRelationshipTypedata, SessionVariables.RequestProfile);
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
			var data = new SystemForeignRelationshipTypeDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.SystemForeignRelationshipTypeDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipType;
            PrimaryEntityKey = "SystemForeignRelationshipType";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}