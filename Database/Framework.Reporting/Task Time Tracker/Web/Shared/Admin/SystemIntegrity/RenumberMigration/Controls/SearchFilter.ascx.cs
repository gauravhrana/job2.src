using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.RenumberMigration.Controls
{

    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

        public RenumberMigrationDataModel SearchParameters
        {
            get
            {
                var data = new RenumberMigrationDataModel();

                if (SearchParametersRepeater.Items.Count != 0)
                {
                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                        RenumberMigrationDataModel.DataColumns.SystemEntityTypeId + "Visibility", SettingCategory))
                    {
                        if (CheckAndGetFieldValue(RenumberMigrationDataModel.DataColumns.SystemEntityTypeId).ToString().Trim() != "-1")
                        {
                            data.SystemEntityTypeId = Convert.ToInt32(CheckAndGetFieldValue(
                               RenumberMigrationDataModel.DataColumns.SystemEntityTypeId));
                        }
                    }

                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                        RenumberMigrationDataModel.DataColumns.OriginalKey + "Visibility", SettingCategory))
                    {
                        if (!string.IsNullOrEmpty(CheckAndGetFieldValue(RenumberMigrationDataModel.DataColumns.OriginalKey).ToString()))
                        {
                            data.OriginalKey = int.Parse(CheckAndGetFieldValue(RenumberMigrationDataModel.DataColumns.OriginalKey).ToString());
                        }
                    }

                    if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                        RenumberMigrationDataModel.DataColumns.MigratedKey + "Visibility", SettingCategory))
                    {
                        if (!string.IsNullOrEmpty(CheckAndGetFieldValue(RenumberMigrationDataModel.DataColumns.MigratedKey).ToString()))
                        {
                            data.MigratedKey = int.Parse(CheckAndGetFieldValue(RenumberMigrationDataModel.DataColumns.MigratedKey).ToString());
                        }
                    }
                }

                return data;

            }
        }

        #endregion

        #region methods

        public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
        {
            base.LoadDropDownListSources(fieldName, dropDownListControl);

            if (fieldName.Equals(RenumberMigrationDataModel.DataColumns.SystemEntityTypeId))
            {
                var systemEntityTypedata = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.AuditId);
                UIHelper.LoadDropDown(systemEntityTypedata, dropDownListControl,
                    DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.EntityName,
                    DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity                = Framework.Components.DataAccess.SystemEntity.RenumberMigration;
            PrimaryEntityKey             = "RenumberMigration";
            FolderLocationFromRoot       = "RenumberMigration";

            SearchActionBarCore          = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion

    }

}