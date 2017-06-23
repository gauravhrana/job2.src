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
using Dapper;

namespace ApplicationContainer.UI.Web.ReportCategory
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

				var selectedrows = new List<ReportCategoryDataModel>();
				var reportCategorydata = new ReportCategoryDataModel();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						reportCategorydata.ReportCategoryId = entityKey;
						var result = Framework.Components.Core.ReportCategoryDataManager.GetDetails(reportCategorydata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
					}
				}
				else
				{
					reportCategorydata.ReportCategoryId = SetId;
					var result = Framework.Components.Core.ReportCategoryDataManager.GetDetails(reportCategorydata, SessionVariables.RequestProfile);
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
			var data = new ReportCategoryDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.ReportCategoryDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReportCategory;
            PrimaryEntityKey = "ReportCategory";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}