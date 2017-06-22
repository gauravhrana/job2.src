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
using Dapper;

namespace Shared.UI.Web.Configuration.ApplicationRoute
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

                var selectedrows = new List<ApplicationRouteDataModel>();
				var applicationRoutedata = new ApplicationRouteDataModel();

                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        applicationRoutedata.ApplicationRouteId = entityKey;
						var result = Framework.Components.Core.ApplicationRouteDataManager.GetDetails(applicationRoutedata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
                    }
                }
                else
                {
                    applicationRoutedata.ApplicationRouteId = SetId;
					var result = Framework.Components.Core.ApplicationRouteDataManager.GetDetails(applicationRoutedata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);

                }
                return selectedrows.ToDataTable(); ;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            return null;
        }

        protected override void Update(Dictionary<string, string> values)
        {
			var data = new ApplicationRouteDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.ApplicationRouteDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRoute;
            PrimaryEntityKey = "ApplicationRoute";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}