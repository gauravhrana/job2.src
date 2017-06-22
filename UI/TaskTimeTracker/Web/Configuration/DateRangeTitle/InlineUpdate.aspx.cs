using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Configuration.DateRangeTitle
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
				
				var selectedrows = new List<DateRangeTitleDataModel>();
				var dateRangeTitledata = new DateRangeTitleDataModel();

				if (!string.IsNullOrEmpty(SuperKey))				
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						dateRangeTitledata.DateRangeTitleId = entityKey;
                        var result = Framework.Components.UserPreference.DateRangeTitleDataManager.GetDetails(dateRangeTitledata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
					}
				}
				else
				{
					dateRangeTitledata.DateRangeTitleId = SetId;
                    var result = Framework.Components.UserPreference.DateRangeTitleDataManager.GetDetails(dateRangeTitledata, SessionVariables.RequestProfile);
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
			var data = new DateRangeTitleDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            Framework.Components.UserPreference.DateRangeTitleDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DateRangeTitle;
            PrimaryEntityKey = "DateRangeTitle";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}