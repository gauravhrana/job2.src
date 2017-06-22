using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.Trace
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
                var tracedata = new DataModel.Framework.Audit.TraceDataModel();

				selectedrows = Framework.Components.Audit.TraceDataManager.GetDetails(tracedata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                     foreach (var entityKey in lstEntityKeys)
					 {
						    tracedata.TraceId = entityKey;
							var result = Framework.Components.Audit.TraceDataManager.GetDetails(tracedata, SessionVariables.RequestProfile);
                            selectedrows.ImportRow(result.Rows[0]);
                      }
                    
                }
                else
                {
					tracedata.TraceId = SetId;
					var result = Framework.Components.Audit.TraceDataManager.GetDetails(tracedata, SessionVariables.RequestProfile);
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

            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Trace, "DBColumns", SessionVariables.RequestProfile);
        }

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new DataModel.Framework.Audit.TraceDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.Audit.TraceDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Trace;
			PrimaryEntityKey = "Trace";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
    }
}