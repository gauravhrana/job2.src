﻿using System;
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
using System.Data;

namespace Shared.UI.Web.Admin
{
	public partial class SubMenuSettings : Framework.UI.Web.BaseClasses.PageBasePage
	{
		string SettingCategory = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);			
			var bcControl = this.Master.BreadCrumbObject;
			SettingCategory = "SubMenuSettingsDefaultView";
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup("");
			bcControl.GenerateMenu();

		}

		private void SetUpStandardSettings(string settingCategory, bool setupSubMenu)
		{
			var parentmenuId = -1;
			var activemenuId = -1;

			PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(settingCategory, settingCategory);
		   
		   if (parentmenuId < 0 && activemenuId < 0)
		   {
			   var postfix = "DefaultViewSubMenuControl";
			   var entity = settingCategory.Remove(settingCategory.Length - postfix.Length);
			   entity = entity.Replace(" ", "");
			   var menudata = new MenuDataModel();
			   menudata.Name = entity;
			   var menudt = Framework.Components.Core.MenuDataManager.Search(menudata, SessionVariables.RequestProfile);
			   if (menudt.Rows.Count == 1)
			   {
				   try
				   {
					   parentmenuId = int.Parse(menudt.Rows[0][MenuDataModel.DataColumns.ParentMenuId].ToString());
					   activemenuId = int.Parse(menudt.Rows[0][MenuDataModel.DataColumns.MenuId].ToString());

					   PerferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ParentMenuId, parentmenuId.ToString());
					   PerferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ActiveMenuId, activemenuId.ToString());
				   }
				   catch (Exception ex) { }
			   }
			   if (menudt.Rows.Count > 1)
			   {
				   for (var i = 0; i < menudt.Rows.Count; i++)
				   {
					   if (!(menudt.Rows[i][MenuDataModel.DataColumns.NavigateURL].ToString().Equals("#")) &&
						   menudt.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString().Equals(entity))
					   {
						   try
						   {
							   parentmenuId = int.Parse(menudt.Rows[i][MenuDataModel.DataColumns.ParentMenuId].ToString());
							   activemenuId = int.Parse(menudt.Rows[i][MenuDataModel.DataColumns.MenuId].ToString());

							   PerferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ParentMenuId, parentmenuId.ToString());
							   PerferenceUtility.UpdateApplicationUserPreference(settingCategory, ApplicationCommon.ActiveMenuId, activemenuId.ToString());
						   }
						   catch (Exception ex) { }
					   }
				   }
			   }
		   }
		   if (setupSubMenu)
		   {
			   oSubMenu.SettingCategory = settingCategory;
			   oSubMenu.Setup(parentmenuId, activemenuId);
			   oSubMenu.GenerateMenu(true);
		   }
		}

		protected void btnReturn_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx");
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			
			if (!string.IsNullOrEmpty(txtSearchConditionCategory.Text))
			{
				SettingCategory = txtSearchConditionCategory.Text;
				SetUpStandardSettings(SettingCategory, true);
				var data = new UserPreferenceCategoryDataModel();
				var updata = new UserPreferenceDataModel();
				data.Name = SettingCategory;
				var upcdt = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.Search(data, SessionVariables.RequestProfile);
				//if (upcdt.Rows.Count == 1)
				//{
				//    updata.UserPreferenceCategoryId = Convert.ToInt32(
				//        upcdt.Rows[0][UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId].ToString());
				//    updata.ApplicationUserId = ApplicationCommon.SystemAuditId;
				//}
				//var updt = Framework.Components.UserPreference.UserPreference.Search(updata, SessionVariables.RequestProfile.AuditId);
				SubMenuSettingsRepeater.DataSource = upcdt.DefaultView;
				SubMenuSettingsRepeater.DataBind();

			}
			else
			{
				var data = new UserPreferenceCategoryDataModel();
				var updata = new UserPreferenceDataModel();
				data.Name = "";
				var upcdt = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.Search(data, SessionVariables.RequestProfile);
				var dt = new DataTable();
				dt = upcdt.Clone();
				for (var i = 0; i < upcdt.Rows.Count; i++)
				{
					if (upcdt.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString().EndsWith("DefaultViewSubMenuControl"))
						dt.ImportRow(upcdt.Rows[i]);
				}
				SubMenuSettingsRepeater.DataSource = dt.DefaultView;
				SubMenuSettingsRepeater.DataBind();
				
			}
			SetupSubMenu();

		}

		private void SetupSubMenu()
		{
			for (var i = 0; i < SubMenuSettingsRepeater.Items.Count; i++)
			{
				if (SubMenuSettingsRepeater.Items[i].ItemType == ListItemType.Item ||
					SubMenuSettingsRepeater.Items[i].ItemType == ListItemType.AlternatingItem)
				{
					var lbl = SubMenuSettingsRepeater.Items[i].FindControl("lblCategory") as Label;
					SetUpStandardSettings(lbl.Text, true);
					break;
				}
			}
		}
		protected void SubMenuSettingsRepeater_ItemDataBound(object source, DataListItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//DataRowView drv = e.Item.DataItem as DataRowView;
				
				var innerDataList = e.Item.FindControl("KeyValueList") as DataList;
				var category = e.Item.FindControl("lblCategory") as Label;

				SetUpStandardSettings(category.Text, false);
				var data = new UserPreferenceCategoryDataModel();
				var updata = new UserPreferenceDataModel();
				data.Name = category.Text;
				
				var upcdt = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.Search(data, SessionVariables.RequestProfile);
				
				if (upcdt.Rows.Count == 1)
				{
					updata.UserPreferenceCategoryId = Convert.ToInt32(upcdt.Rows[0][UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId].ToString());

					updata.ApplicationUserId = SessionVariables.SystemRequestProfile.AuditId;
				}
				
				var updt = UserPreferenceDataManager.Search(updata, SessionVariables.RequestProfile);
				
				innerDataList.DataSource = updt.DefaultView;
				innerDataList.RepeatDirection = RepeatDirection.Horizontal;
				innerDataList.DataBind();
			}
		}

		protected void SubMenuSettingsRepeater_ItemCommand(object source, DataListCommandEventArgs e)
		{			
			switch (e.CommandName)
			{
				case "GO":
				   SettingCategory = txtSearchConditionCategory.Text;
			var parentmenuId = 0;
			var activemenuId = 0;
			if (SubMenuSettingsRepeater.Items[e.Item.ItemIndex].ItemType == ListItemType.Item ||
					SubMenuSettingsRepeater.Items[e.Item.ItemIndex].ItemType == ListItemType.AlternatingItem)
			{
				var keyvaluelist = (DataList)SubMenuSettingsRepeater.Items[e.Item.ItemIndex].FindControl("KeyValueList");
				for (var j = 0; j < keyvaluelist.Items.Count; j++)
				{
					if (keyvaluelist.Items[j].ItemType == ListItemType.Item ||
						keyvaluelist.Items[j].ItemType == ListItemType.AlternatingItem)
					{
						var label = (Label)keyvaluelist.Items[j].FindControl("lblKey");
						var txtbox = (TextBox)keyvaluelist.Items[j].FindControl("txtValue");

						if (label != null && txtbox != null)
						{

							if (label.Text.Equals(ApplicationCommon.ParentMenuId))
								parentmenuId = int.Parse(txtbox.Text);
							if (label.Text.Equals(ApplicationCommon.ActiveMenuId))
								activemenuId = int.Parse(txtbox.Text);
						}
					}
				}


			}

			oSubMenu.SettingCategory = SettingCategory + "SubMenuControl";
			oSubMenu.Setup(parentmenuId, activemenuId);
			oSubMenu.GenerateMenu(true);

					break;

				// Other commands here.

				default:
					break;
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			SettingCategory = txtSearchConditionCategory.Text;
			var parentmenuId = 0;
			var activemenuId = 0;
			for (var i = 0; i < SubMenuSettingsRepeater.Items.Count; i++)
			{
				if (SubMenuSettingsRepeater.Items[i].ItemType == ListItemType.Item ||
					SubMenuSettingsRepeater.Items[i].ItemType == ListItemType.AlternatingItem)
				{
					var label = (Label)SubMenuSettingsRepeater.Items[i].FindControl("lblKey");
					var txtbox = (TextBox)SubMenuSettingsRepeater.Items[i].FindControl("txtValue");

					if (label != null && txtbox != null)
					{
						PerferenceUtility.UpdateApplicationUserPreference
						(
							SettingCategory
							, label.Text
							, txtbox.Text
						);
						if (label.Text.Equals(ApplicationCommon.ParentMenuId))
							parentmenuId = int.Parse(txtbox.Text);
						if (label.Text.Equals(ApplicationCommon.ActiveMenuId))
							activemenuId = int.Parse(txtbox.Text);
					}
				}

			}

			oSubMenu.SettingCategory = SettingCategory + "SubMenuControl";
			oSubMenu.Setup(parentmenuId, activemenuId);
			oSubMenu.GenerateMenu(true);

		}
	}
}