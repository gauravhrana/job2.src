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

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()       
		{
			var UpdatedData = new DataTable();
			var data = new ApplicationUserDataModel();
			UpdatedData = Framework.Components.ApplicationUser.ApplicationUserDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ApplicationUserId =
					Convert.ToInt32(SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserId].ToString());
				data.ApplicationUserName = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.ApplicationUserName))
					? CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.ApplicationUserName).ToString()
					: SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();

				data.FirstName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.FirstName))
					? CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.FirstName).ToString()
					: SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.FirstName].ToString();

				data.LastName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.LastName))
					? CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.LastName).ToString()
					: SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.LastName].ToString();

				data.FullName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.FullName))
					? CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.FullName).ToString()
					: SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.FullName].ToString();

				data.MiddleName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.MiddleName))
					? CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.MiddleName).ToString()
					: SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.MiddleName].ToString();

				data.EmailAddress =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.EmailAddress))
					? CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.EmailAddress).ToString()
					: SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.EmailAddress].ToString();

				data.ApplicationId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ApplicationId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ApplicationId).ToString())
					: int.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());

				data.ApplicationUserTitleId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.ApplicationUserTitleId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationUserDataModel.DataColumns.ApplicationUserTitleId).ToString())
					: int.Parse(SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserTitleId].ToString());

				Framework.Components.ApplicationUser.ApplicationUserDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ApplicationUserDataModel();
				data.ApplicationUserId = Convert.ToInt32(SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserId].ToString());
				var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationUserdata = new ApplicationUserDataModel();
            applicationUserdata.ApplicationUserId = entityKey;
			var results = Framework.Components.ApplicationUser.ApplicationUserDataManager.Search(applicationUserdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUser;
            PrimaryEntityKey = "ApplicationUser";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}