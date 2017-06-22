using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using System.Data;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatusArchive.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {

        #region variables

        public FunctionalityEntityStatusArchiveDataModel SearchParameters
        {
            get
            {
                var data = new FunctionalityEntityStatusArchiveDataModel();
                var entityId = Convert.ToInt32(Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus);

                var columnname = FunctionalityEntityStatusDataModel.DataColumns.SystemEntityType;
                if (!string.IsNullOrEmpty(ApplicationCommon.CheckAndGetFieldValue(SearchParametersRepeater, entityId, columnname, SettingCategory, SessionVariables.RequestProfile.AuditId)))
                {
                    data.SystemEntityTypeId = Convert.ToInt32(ApplicationCommon.CheckAndGetFieldValue(SearchParametersRepeater, entityId, columnname, SettingCategory, SessionVariables.RequestProfile.AuditId));
                }

                columnname = FunctionalityEntityStatusDataModel.DataColumns.Functionality;
                if (!string.IsNullOrEmpty(ApplicationCommon.CheckAndGetFieldValue(SearchParametersRepeater, entityId, columnname, SettingCategory, SessionVariables.RequestProfile.AuditId)))
                {
                    data.FunctionalityId = Convert.ToInt32(ApplicationCommon.CheckAndGetFieldValue(SearchParametersRepeater, entityId, columnname, SettingCategory, SessionVariables.RequestProfile.AuditId));
                }

                columnname = FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatus;
                if (!string.IsNullOrEmpty(ApplicationCommon.CheckAndGetFieldValue(SearchParametersRepeater, entityId, columnname, SettingCategory, SessionVariables.RequestProfile.AuditId)))
                {
                    data.FunctionalityStatusId = Convert.ToInt32(ApplicationCommon.CheckAndGetFieldValue(SearchParametersRepeater, entityId, columnname, SettingCategory, SessionVariables.RequestProfile.AuditId));
                }

                columnname = FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriority;
                if (!string.IsNullOrEmpty(ApplicationCommon.CheckAndGetFieldValue(SearchParametersRepeater, entityId, columnname, SettingCategory, SessionVariables.RequestProfile.AuditId)))
                {
                    data.FunctionalityPriorityId = Convert.ToInt32(ApplicationCommon.CheckAndGetFieldValue(SearchParametersRepeater, entityId, columnname, SettingCategory, SessionVariables.RequestProfile.AuditId));
                }
                return data;
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.FunctionalityEntityStatusArchive;
            PrimaryEntityKey = "FunctionalityActiveStatusArchive";
            FolderLocationFromRoot = "/Shared/QualityAssurarnce";

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;

        }

        #endregion

    }
}