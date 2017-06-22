﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using System.Text;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster
{
    public partial class InlineUpdate : PageInlineUpdate
    {
        #region Methods

		public static byte[] profileImage;

        protected override DataTable GetData()
        {
            try
            {
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                var selectedrows = new DataTable();
                var applicationUserProfileImageMasterdata = new ApplicationUserProfileImageMasterDataModel();

				selectedrows = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetDetails(applicationUserProfileImageMasterdata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        applicationUserProfileImageMasterdata.ApplicationUserProfileImageMasterId = entityKey;
						var result = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetDetails(applicationUserProfileImageMasterdata, SessionVariables.RequestProfile);
						profileImage = ((byte[])(result.Rows[0][ApplicationUserProfileImageMasterDataModel.DataColumns.Image]));
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    applicationUserProfileImageMasterdata.ApplicationUserProfileImageMasterId = SetId;
					var result = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetDetails(applicationUserProfileImageMasterdata, SessionVariables.RequestProfile);
					profileImage = ((byte[])(result.Rows[0][ApplicationUserProfileImageMasterDataModel.DataColumns.Image]));
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

        protected override void Update(Dictionary<string, string> values)
        {
            var data = new ApplicationUserProfileImageMasterDataModel();

            // copies properties from values dictionary object to data object
			 PropertyMapper.CopyProperties(data, values);
			 data.Image = profileImage;

			Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImageMaster;
            PrimaryEntityKey = "ApplicationUserProfileImageMaster";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}


	