using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.ThemeCategory
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
				var themeCategorydata = new ThemeCategoryDataModel();

				selectedrows = Framework.Components.Core.ThemeCategoryDataManager.GetDetails(themeCategorydata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        themeCategorydata.ThemeCategoryId = entityKey;
						var result = Framework.Components.Core.ThemeCategoryDataManager.GetDetails(themeCategorydata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
                    }
                }
				else 
				{
					themeCategorydata.ThemeCategoryId = SetId;
					var result = Framework.Components.Core.ThemeCategoryDataManager.GetDetails(themeCategorydata, SessionVariables.RequestProfile);
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
			var data = new ThemeCategoryDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.ThemeCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
            PrimaryEntity         = Framework.Components.DataAccess.SystemEntity.ThemeCategory;
			PrimaryEntityKey      = "ThemeCategory";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;
			
			base.OnInit(e);
        }

        #endregion

    }
}
        