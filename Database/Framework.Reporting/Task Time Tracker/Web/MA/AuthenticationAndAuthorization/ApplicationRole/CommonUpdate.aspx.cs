using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.AuthenticationAndAuthorization.ApplicationRole
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();
            var data = new ApplicationRoleDataModel();
			UpdatedData = Framework.Components.ApplicationUser.ApplicationRoleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ApplicationRoleId =
                    Convert.ToInt32(SelectedData.Rows[i][ApplicationRoleDataModel.DataColumns.ApplicationRoleId].ToString());
                data.Name = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name).ToString()
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description).ToString()
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();
               
                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());
				
				data.ApplicationId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRoleDataModel.StandardDataColumns.ApplicationId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRoleDataModel.StandardDataColumns.ApplicationId).ToString())
					: int.Parse(SelectedData.Rows[i][ApplicationRoleDataModel.StandardDataColumns.ApplicationId].ToString());

				Framework.Components.ApplicationUser.ApplicationRoleDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ApplicationRoleDataModel();
                data.ApplicationRoleId = Convert.ToInt32(SelectedData.Rows[i][ApplicationRoleDataModel.DataColumns.ApplicationRoleId].ToString());
				var dt = Framework.Components.ApplicationUser.ApplicationRoleDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }
        
        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationRoledata = new ApplicationRoleDataModel();
            applicationRoledata.ApplicationRoleId = entityKey;
			var results = Framework.Components.ApplicationUser.ApplicationRoleDataManager.Search(applicationRoledata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRole;
            PrimaryEntityKey = "ApplicationRole";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}