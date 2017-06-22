using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Configuration.ApplicationEntityParentalHierarchy.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {
        #region variables
        public ApplicationEntityParentalHierarchyDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationEntityParentalHierarchyDataModel();


                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                   StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
                   && CheckAndGetFieldValue(
                       StandardDataModel.StandardDataColumns.Name) != "")
                {
                    data.Name = CheckAndGetFieldValue(
                       StandardDataModel.StandardDataColumns.Name).ToString();
                }

                return data;
            }
        }
        #endregion

        #region private methods        

        public void SetupSearch()
        {
            if (SearchColumns == null)
            {
                //Code to bind the Search fields repeater with SearchField Mode columns from FieldConfig table
                var colsdata = new FieldConfigurationDataModel();
                colsdata.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
                colsdata.SystemEntityTypeId = Convert.ToInt32(Framework.Components.DataAccess.SystemEntity.Application);
				var cols = FieldConfigurationDataManager.Search(colsdata, AuditId, SessionVariables.ApplicationMode);
                SearchColumns = cols;
            }

            SearchParametersRepeater.DataSource = SearchColumns;
            SearchParametersRepeater.DataBind();

            if (!string.IsNullOrEmpty(SettingCategory))
            {
                PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(SettingCategory, "Search Control Name");
            }
            else
            {
                throw new Exception("Search control is not named");
            }

            GetSettings();

            SaveSettings();

            RaiseSearch();
        }
        private int SaveSearchKey()
        {
            var searchKeyId = 0;
            if (SearchColumns != null)
            {
                var data = new SearchKeyDataModel();
                data.Name = DateTime.Now.ToLongTimeString();
                data.View = "Application";
                data.SortOrder = 1;
                data.Description = "Application";

				searchKeyId = Framework.Components.Core.SearchKeyDataManager.Create(data, SessionVariables.RequestProfile);

                foreach (DataRow dr in SearchColumns.Rows)
                {
                    try
                    {
                        var columnName = Convert.ToString(dr["Name"]);
                        var columnValue = CheckAndGetFieldValue(columnName, false).ToString();

                        var dataDetail = new SearchKeyDetailDataModel();
                        dataDetail.SearchKeyId = searchKeyId;

                        //ApplicationCommon.UpdateUserPreference(SettingCategory, columnName, columnValue);
                        dataDetail.SearchParameter = columnName;
                        dataDetail.SortOrder = 1;
						var detailId = Framework.Components.Core.SearchKeyDetailDataManager.Create(dataDetail, SessionVariables.RequestProfile);

                        var dataDetailItem = new SearchKeyDetailItemDataModel();
                        dataDetailItem.SearchKeyDetailId = detailId;
                        dataDetailItem.SortOrder = 1;

                        dataDetailItem.Value = columnValue;
						Framework.Components.Core.SearchKeyDetailItemDataManager.Create(dataDetailItem, SessionVariables.RequestProfile);

                    }
                    catch
                    { }
                }
            }
            return searchKeyId;
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

            oSearchActionBar.Setup("Application", SaveSearchKey);
        
        }

        #endregion
    }
}