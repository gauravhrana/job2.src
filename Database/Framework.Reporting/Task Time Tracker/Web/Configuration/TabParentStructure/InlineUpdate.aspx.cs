using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.TabParentStructure
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
				var tabParentStructuredata = new TabParentStructureDataModel();

				selectedrows = Framework.Components.Core.TabParentStructureDataManager.GetDetails(tabParentStructuredata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        tabParentStructuredata.TabParentStructureId = entityKey;
						var result = Framework.Components.Core.TabParentStructureDataManager.GetDetails(tabParentStructuredata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
                    }
                }
				else 
				{
					tabParentStructuredata.TabParentStructureId = SetId;
					var result = Framework.Components.Core.TabParentStructureDataManager.GetDetails(tabParentStructuredata, SessionVariables.RequestProfile);
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
			var data = new TabParentStructureDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.TabParentStructureDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
            PrimaryEntity         = Framework.Components.DataAccess.SystemEntity.TabParentStructure;
			PrimaryEntityKey      = "TabParentStructure";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;
			
			base.OnInit(e);
        }

        #endregion

    }
}
       