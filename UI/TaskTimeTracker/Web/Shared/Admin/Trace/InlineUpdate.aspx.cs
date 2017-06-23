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
using Dapper;
using DataModel.Framework.Audit;

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

                var selectedrows = new List<TraceDataModel>();
                var tracedata = new TraceDataModel();

                if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                     foreach (var entityKey in lstEntityKeys)
					 {
						    tracedata.TraceId = entityKey;
							var result = Framework.Components.Audit.TraceDataManager.GetDetails(tracedata, SessionVariables.RequestProfile);
                            selectedrows.Add(result);
                      }
                    
                }
                else
                {
					tracedata.TraceId = SetId;
					var result = Framework.Components.Audit.TraceDataManager.GetDetails(tracedata, SessionVariables.RequestProfile);
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