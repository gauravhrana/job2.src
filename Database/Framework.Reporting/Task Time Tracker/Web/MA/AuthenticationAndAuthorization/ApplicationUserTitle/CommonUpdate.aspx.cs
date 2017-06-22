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

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserTitle
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
			var UpdatedData = new DataTable();
			var data = new ApplicationUserTitleDataModel();
			UpdatedData = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ApplicationUserTitleId =
					Convert.ToInt32(SelectedData.Rows[i][ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId].ToString());
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

				Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ApplicationUserTitleDataModel();
				data.ApplicationUserTitleId = Convert.ToInt32(SelectedData.Rows[i][ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId].ToString());
				var dt = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.Search(data, SessionVariables.RequestProfile);
                
                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }        

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationUserTitledata = new ApplicationUserTitleDataModel();
            applicationUserTitledata.ApplicationUserTitleId = entityKey;
			var results = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.Search(applicationUserTitledata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserTitle;
            PrimaryEntityKey = "ApplicationUserTitle";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}