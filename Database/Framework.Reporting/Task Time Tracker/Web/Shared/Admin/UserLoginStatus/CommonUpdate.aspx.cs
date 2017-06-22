using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.LogAndTrace;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.UserLoginStatus
{
	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

			var data = new UserLoginStatusDataModel();
			UpdatedData = Framework.Components.LogAndTrace.UserLoginStatusDataManager.Search(data, SessionVariables.RequestProfile).Clone();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
				data.UserLoginStatusId =
					Convert.ToInt32(SelectedData.Rows[i][UserLoginStatusDataModel.DataColumns.UserLoginStatusId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.LogAndTrace.UserLoginStatusDataManager.Update(data, SessionVariables.RequestProfile);
				data = new UserLoginStatusDataModel();
				data.UserLoginStatusId = Convert.ToInt32(SelectedData.Rows[i][UserLoginStatusDataModel.DataColumns.UserLoginStatusId].ToString());
				var dt = Framework.Components.LogAndTrace.UserLoginStatusDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }
	
		protected override DataTable GetEntityData(int? entityKey)
		{
            var userLoginStatusdata = new UserLoginStatusDataModel();
            userLoginStatusdata.UserLoginStatusId = entityKey;
			var results = Framework.Components.LogAndTrace.UserLoginStatusDataManager.Search(userLoginStatusdata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginStatus;
			PrimaryEntityKey = "UserLoginStatus";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

		
	}
}