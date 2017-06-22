using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SearchKeyDetail.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{

		#region variables

		public SearchKeyDetailDataModel SearchParameters
		{
			get
			{
				var data = new SearchKeyDetailDataModel();

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
					SearchKeyDetailDataModel.DataColumns.SearchKeyId + "Visibility", SettingCategory)
					&& CheckAndGetFieldValue(SearchKeyDetailDataModel.DataColumns.SearchKeyId) != "")
				{
					data.SearchKeyId = Convert.ToInt32(CheckAndGetFieldValue(
					   SearchKeyDetailDataModel.DataColumns.SearchKeyId));
				}				

				return data;
			}
		}

		#endregion

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);
			if (fieldName.Equals("SearchKeyId"))
			{
				var SearchKeyData = Framework.Components.Core.SearchKeyDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(SearchKeyData, dropDownListControl, 
					StandardDataModel.StandardDataColumns.Name, 
					SearchKeyDataModel.DataColumns.SearchKeyId);
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

			PrimaryEntityKey = "SearchKeyDetail";
			FolderLocationFromRoot = "Shared/Admin/SystemIntegrity/SearchKeyDetail";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKeyDetail;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}