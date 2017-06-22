using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Admin.SystemEntityCategory.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables
      

        public SystemEntityCategoryDataModel SearchParameters
        {
            get
            {
                var data = new SystemEntityCategoryDataModel();

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                    StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
                    && !string.IsNullOrEmpty(CheckAndGetFieldValue(
                        StandardDataModel.StandardDataColumns.Name).ToString()))
                {
                    data.Name = CheckAndGetFieldValue(
                       StandardDataModel.StandardDataColumns.Name).ToString();
                }


                return data;
            }
        }

        #endregion

        #region methods

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "SystemEntityCategory";
            FolderLocationFromRoot = "/Shared/Admin";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityCategory;

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion

		}
}