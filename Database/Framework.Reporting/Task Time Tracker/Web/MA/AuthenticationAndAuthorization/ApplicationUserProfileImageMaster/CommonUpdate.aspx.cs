using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Text;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();
            var data = new ApplicationUserProfileImageMasterDataModel();
			UpdatedData = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ApplicationUserProfileImageMasterId =
                    Convert.ToInt32(SelectedData.Rows[i][ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId].ToString());

				data.ApplicationId =
				   Convert.ToInt32(SelectedData.Rows[i][ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationId].ToString());

                data.Title = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationUserProfileImageMasterDataModel.DataColumns.Title))
                    ? CheckAndGetRepeaterTextBoxValue(ApplicationUserProfileImageMasterDataModel.DataColumns.Title).ToString()
                    : SelectedData.Rows[i][ApplicationUserProfileImageMasterDataModel.DataColumns.Title].ToString();

                data.Image = ((byte[])(SelectedData.Rows[i][ApplicationUserProfileImageMasterDataModel.DataColumns.Image]));

				Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ApplicationUserProfileImageMasterDataModel();
                data.ApplicationUserProfileImageMasterId = Convert.ToInt32(SelectedData.Rows[i][ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId].ToString());
				var dt = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationUserProfileImageMasterdata = new ApplicationUserProfileImageMasterDataModel();
            applicationUserProfileImageMasterdata.ApplicationUserProfileImageMasterId = entityKey;
			var results = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.Search(applicationUserProfileImageMasterdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImageMaster;
            PrimaryEntityKey = "ApplicationUserProfileImageMaster";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}

    