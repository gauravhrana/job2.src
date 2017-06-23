using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SearchKeyDetailItem.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{

		#region variables
		
		public SearchKeyDetailItemDataModel SearchParameters
		{
			get
			{
				var data = new SearchKeyDetailItemDataModel();
								
				if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
					SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailId + "Visibility", SettingCategory)
					&& CheckAndGetFieldValue(SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailId) != "")
				{
					data.SearchKeyDetailId = Convert.ToInt32(CheckAndGetFieldValue(
					   SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailId));
				}	

			return data;
			}
		}

		#endregion

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);
			if (fieldName.Equals("SearchKeyDetailId"))
			{
				var SearchKeyData = Framework.Components.Core.SearchKeyDetailDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(SearchKeyData, dropDownListControl, SearchKeyDetailDataModel.DataColumns.SearchParameter, SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId);
			}
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "SearchKeyDetailItem";
			FolderLocationFromRoot = "Shared/Admin/SystemIntegrity/SearchKeyDetailItem";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKeyDetailItem;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}