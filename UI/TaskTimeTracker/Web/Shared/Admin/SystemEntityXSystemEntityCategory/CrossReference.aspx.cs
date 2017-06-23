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

namespace Shared.UI.Web.Admin.SystemEntityXSystemEntityCategory
{
	public partial class CrossReference : Shared.UI.WebFramework.BasePage
	{
		#region Methods

		private List<SystemEntityTypeDataModel> GetSystemEntityList()
		{
			var dt = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedSystemEntities(int SystemEntityCategoryId)
		{
			var id = Convert.ToInt32(drpSystemEntityCategory.SelectedValue);
			var dt = Framework.Components.Core.SystemEntityXSystemEntityCategoryDataManager.GetBySystemEntityCategory(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveBySystemEntityCategory(int SystemEntityCategoryId, List<int> SystemEntityIds)
		{
			var id = Convert.ToInt32(drpSystemEntityCategory.SelectedValue);
			Framework.Components.Core.SystemEntityXSystemEntityCategoryDataManager.DeleteBySystemEntityCategory(id, SessionVariables.RequestProfile);
			Framework.Components.Core.SystemEntityXSystemEntityCategoryDataManager.CreateBySystemEntityCategory(id, SystemEntityIds.ToArray(), SessionVariables.RequestProfile);
		}

		private List<SystemEntityCategoryDataModel> GetSystemEntityCategoryList()
		{
			var dt = Framework.Components.Core.SystemEntityCategoryDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedSystemEntityCategories(int SystemEntityId)
		{
			var id = Convert.ToInt32(drpSystemEntity.SelectedValue);
			var dt = Framework.Components.Core.SystemEntityXSystemEntityCategoryDataManager.GetBySystemEntity(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveBySystemEntity(int SystemEntityId, List<int> SystemEntityCategoryIds)
		{
			var id = Convert.ToInt32(drpSystemEntity.SelectedValue);
			Framework.Components.Core.SystemEntityXSystemEntityCategoryDataManager.DeleteBySystemEntity(id, SessionVariables.RequestProfile);
			Framework.Components.Core.SystemEntityXSystemEntityCategoryDataManager.CreateBySystemEntity(id, SystemEntityCategoryIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpSystemEntityCategory.DataSource = GetSystemEntityCategoryList();
			drpSystemEntityCategory.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpSystemEntityCategory.DataValueField = SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId;
			drpSystemEntityCategory.DataBind();

			drpSystemEntity.DataSource = GetSystemEntityList();
			drpSystemEntity.DataTextField = SystemEntityTypeDataModel.DataColumns.EntityName;
			drpSystemEntity.DataValueField = SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId;
			drpSystemEntity.DataBind();
		}

		#endregion

		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "SystemEntityXSystemEntityCategoryDefaultView";
			
		}

		protected override void OnInit(EventArgs e)
		{

			BindLists();

            BucketOfSystemEntityCategory.ConfigureBucket("SystemEntityCategory", 1, GetSystemEntityCategoryList, GetAssociatedSystemEntityCategories, SaveBySystemEntity);
            BucketOfSystemEntity.ConfigureBucket("SystemEntity", 1, GetSystemEntityList, GetAssociatedSystemEntities, SaveBySystemEntityCategory, "", "SystemEntityId");
            //, "EntityName", "SystemEntityId"
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "BySystemEntity")
			{
				dynSystemEntity.Visible = true;
				dynSystemEntityCategory.Visible = false;
				BucketOfSystemEntityCategory.ReloadBucketList();				
			}
			else if (drpSelection.SelectedValue == "BySystemEntityCategory")
			{
				dynSystemEntity.Visible = false;
				dynSystemEntityCategory.Visible = true;
				BucketOfSystemEntity.ReloadBucketList();				
			}
		}

		protected void drpSystemEntityCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfSystemEntity.ReloadBucketList();			
		}

		protected void drpSystemEntity_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfSystemEntityCategory.ReloadBucketList();			
		}

		#endregion
	}
}