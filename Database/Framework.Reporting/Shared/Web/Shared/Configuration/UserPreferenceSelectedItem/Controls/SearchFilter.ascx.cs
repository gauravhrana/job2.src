using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.UserPreferenceSelectedItem.Controls
{
    public partial class SearchFilter : ControlSearchFilter
	{
		#region variables			
     
        public Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel SearchParameters
        {
            get
            {
                var data = new Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel();
				
				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.ParentKey + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.ParentKey) != "")
				{
					data.ParentKey = CheckAndGetFieldValue(Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.ParentKey).ToString();
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUserId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUserId) != "")
				{
					data.ApplicationUserId = Convert.ToInt32(
						CheckAndGetFieldValue(Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUserId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKeyId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKeyId) != "")
				{
					data.UserPreferenceKeyId = Convert.ToInt32(
						CheckAndGetFieldValue(Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKeyId));
				}

                /*data.ParentKey = UIHelper.RefineAndGetSearchText(txtSearchConditionParentKey.Text.Trim(), SettingCategory);
				if (drpSearchConditionUserPreferenceKey.SelectedValue != "-1")
				{
					data.UserPreferenceKeyId = Convert.ToInt32(drpSearchConditionUserPreferenceKey.SelectedValue);
                    txtSearchConditionUserPreferenceKey.Text = drpSearchConditionUserPreferenceKey.SelectedValue;
				}

                if (drpSearchConditionApplicationUser.SelectedValue != "-1")
                {
                    data.ApplicationUserId = Convert.ToInt32(drpSearchConditionApplicationUser.SelectedValue);
                    txtSearchConditionApplicationUser.Text = drpSearchConditionApplicationUser.SelectedValue;
                }*/

                return data;
            }
        }

		

		#endregion

		#region private methods

		

		private void SetAutoSearchOn(bool enabled)
		{
			for (var i = 0; i < SearchParametersRepeater.Items.Count; i++)
			{
				var dropdownlist = (DropDownList)SearchParametersRepeater.Items[i].FindControl("dropdownlist");
				if (dropdownlist != null && dropdownlist.Visible)
				{
					dropdownlist.AutoPostBack = enabled;
				}
			}
		}

		private void ShowDebugTextBoxes(bool visible)
		{
			for (var i = 0; i < SearchParametersRepeater.Items.Count; i++)
			{
				var txtbox1 = (TextBox)SearchParametersRepeater.Items[i].FindControl("txtbox1");
				if (txtbox1 != null)
				{
					txtbox1.Visible = visible;
				}
			}
		}

			protected override void GetSettings()
		{

			var searchKeyId = Convert.ToString(Page.RouteData.Values["SearchKey"]);
			if (!string.IsNullOrEmpty(searchKeyId))
			{
				var dataSearchKey = new SearchKeyDataModel();
				dataSearchKey.SearchKeyId = Convert.ToInt32(searchKeyId);

				var ds = Framework.Components.Core.SearchKeyDataManager.SearchByKey(dataSearchKey, SessionVariables.RequestProfile);

				for (var i = 0; i < SearchColumns.Rows.Count; i++)
				{
					var colName = Convert.ToString(SearchColumns.Rows[i]["Name"]);


					CheckAndSetFieldValue(colName, GetSearchKeyValue(colName, ds) + "&" +
						GetSearchKeyValue(colName + "2", ds) + "&" +
						GetSearchKeyValue(colName + "Checked", ds));

				}
			}
			else
			{

				var category = SettingCategory;
				var value = String.Empty;

				for (var i = 0; i < SearchColumns.Rows.Count; i++)
				{
					CheckAndSetFieldValue(
SearchColumns.Rows[i]["Name"].ToString(), PerferenceUtility.GetUserPreferenceByKey(SearchColumns.Rows[i]["Name"].ToString(),
category));

				}
			}
		}


		#endregion

		#region Events

		protected void SearchParametersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var label = (Label)e.Item.FindControl("label");
				var hdnfield = (HiddenField)e.Item.FindControl("hdnfield");
				var txtbox = (TextBox)e.Item.FindControl("txtbox");
				var txtbox1 = (TextBox)e.Item.FindControl("txtbox1");
				var dropdownlist = (DropDownList)e.Item.FindControl("dropdownlist");

				if (label != null && txtbox != null && dropdownlist != null)
				{
					var name = hdnfield.Value;
					var data = new FieldConfigurationDataModel();
					data.Name = name;
					data.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
					var dt = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);
					if (dt.Rows.Count >= 1)
					{
						var controltype = dt.Rows[0]
							[FieldConfigurationDataModel.DataColumns.ControlType].ToString();
						if (controltype.Equals("TextBox"))
						{
							txtbox.Visible = true;
							txtbox1.Visible = false;
							dropdownlist.Visible = false;
						}
						else if (controltype.Equals("DropDownList"))
						{
							txtbox.Visible = false;
							txtbox1.Visible = true;
							dropdownlist.Visible = true;

							if (name.Equals("ApplicationUserId"))
							{
								var applicationUserData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
								UIHelper.LoadDropDown(applicationUserData, dropdownlist,
									ApplicationUserDataModel.DataColumns.FullName,
									ApplicationUserDataModel.DataColumns.ApplicationUserId);
							}

							if (name.Equals("UserPreferenceKeyId"))
							{
								var UserPreferenceKeyData = Framework.Components.UserPreference.UserPreferenceKeyDataManager.GetList(SessionVariables.RequestProfile);
								UIHelper.LoadDropDown(UserPreferenceKeyData, dropdownlist,
								        StandardDataModel.StandardDataColumns.Name,
								        UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId);
							}
						}
					}
					e.Item.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(name + "Visibility", SettingCategory);
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			oSearchActionBar.Setup("UserPreferenceSelectedItem", SaveSearchKey);
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.UserPreferenceSelectedItem;
            PrimaryEntityKey = "UserPreferenceSelectedItem";
            FolderLocationFromRoot = "/Shared/Configuration";

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

		
				
		#endregion
	}
}