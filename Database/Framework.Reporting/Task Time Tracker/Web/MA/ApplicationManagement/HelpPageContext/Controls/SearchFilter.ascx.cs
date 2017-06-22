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
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.HelpPageContext.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{
		#region variables
		
		public HelpPageContextDataModel SearchParameters
		{
			get
			{
                var data = new HelpPageContextDataModel();


				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name) != "")
				{
					data.Name = CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name).ToString();
				}

				return data;
			}

			//get
			//{
			//    var data = new HelpPageContext();

			//    data.Name = UIHelper.RefineAndGetSearchText(txtSearchConditionName.Text, SettingCategory);

			//    return data;
			//}
		}
		
		#endregion

		#region private methods		

		//protected override void GetSettings()
		//{
		//	var searchKeyId = Convert.ToString(Page.RouteData.Values["SearchKey"]);

		//	if (!string.IsNullOrEmpty(searchKeyId))
		//	{
		//		var dataSearchKey = new SearchKeyDataModel();
		//		dataSearchKey.SearchKeyId = Convert.ToInt32(searchKeyId);

		//		var ds = SearchKeyDataManager.SearchByKey(dataSearchKey, SessionVariables.RequestProfile.AuditId);

		//		for (var i = 0; i < SearchColumns.Rows.Count; i++)
		//		{
		//			var colName = Convert.ToString(SearchColumns.Rows[i]["Name"]);

		//			CheckAndSetFieldValue(colName, GetSearchKeyValue(colName, ds) + "&" +
		//				GetSearchKeyValue(colName + "2", ds) + "&" +
		//				GetSearchKeyValue(colName + "Checked", ds));
		//		}
		//	}
		//	else
		//	{
		//		var category = SettingCategory;
		//		//var value = String.Empty;

		//		for (var i = 0; i < SearchColumns.Rows.Count; i++)
		//		{
		//			CheckAndSetFieldValue(
		//				SearchColumns.Rows[i]["Name"].ToString()
		//				, PerferenceUtility.GetUserPreferenceByKey(SearchColumns.Rows[i]["Name"].ToString()
		//				, category));
		//		}
		//	}
		//}

		//public void SetupSearch()
		//{
		//	if (SearchColumns == null)
		//	{
		//		//Code to bind the Search fields repeater with SearchField Mode columns from FieldConfig table
		//		var colsdata = new FieldConfigurationDataModel();
		//		colsdata.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
		//		colsdata.SystemEntityTypeId = Convert.ToInt32(SystemEntity.HelpPageContext);
		//		var cols = FieldConfigurationDataManager.Search(colsdata, AuditId, SessionVariables.ApplicationMode);
		//		SearchColumns = cols;
		//	}

		//	SearchParametersRepeater.DataSource = SearchColumns;
		//	SearchParametersRepeater.DataBind();

		//	if (!string.IsNullOrEmpty(SettingCategory))
		//	{
		//		PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(SettingCategory, "Search Control Name");
		//	}
		//	else
		//	{
		//		throw new Exception("Search control is not named");
		//	}

		//	GetSettings();
		//	SaveSettings();
		//	RaiseSearch();
		//}
		
		//private int SaveSearchKey()
		//{
		//	var searchKeyId = 0;

		//	if (SearchColumns != null)
		//	{
		//		var data = new SearchKeyDataModel();
		//		data.Name = DateTime.Now.ToLongTimeString();
		//		data.View = "HelpPageContext";
		//		data.SortOrder = 1;
		//		data.Description = "HelpPageContext";

		//		searchKeyId = SearchKeyDataManager.Create(data, SessionVariables.RequestProfile.AuditId);

		//		foreach (DataRow dr in SearchColumns.Rows)
		//		{
		//			try
		//			{
		//				var columnName = Convert.ToString(dr["Name"]);
		//				var columnValue = CheckAndGetFieldValue(columnName, false).ToString();

		//				var dataDetail = new SearchKeyDetailDataModel();
		//				dataDetail.SearchKeyId = searchKeyId;

		//				//ApplicationCommon.UpdateUserPreference(SettingCategory, columnName, columnValue);
		//				dataDetail.SearchParameter = columnName;
		//				dataDetail.SortOrder = 1;
		//				var detailId = SearchKeyDetailDataManager.Create(dataDetail, SessionVariables.RequestProfile.AuditId);

		//				var dataDetailItem = new SearchKeyDetailItemDataModel();
		//				dataDetailItem.SearchKeyDetailId = detailId;
		//				dataDetailItem.SortOrder = 1;

		//				dataDetailItem.Value = columnValue;
		//				SearchKeyDetailItemDataManager.Create(dataDetailItem, SessionVariables.RequestProfile.AuditId);

		//			}
		//			catch
		//			{ }
		//		}
		//	}

		//	return searchKeyId;
		//}
		 
		#endregion

		#region Events
		
		protected void Page_Load(object sender, EventArgs e)
		{
			oSearchActionBar.Setup("HelpPageContext", SaveSearchKey);
		}        

		#endregion
	}
}