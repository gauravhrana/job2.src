using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Reflection;
using System.Web.UI.WebControls;
using Framework.Components.Core;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using DataModel.Framework.Configuration;

namespace Shared.UI.Web.Admin
{
	public partial class EntityAutomation : Framework.UI.Web.BaseClasses.PageBasePage
	{
		#region Variables
		
		public string PrimaryDeveloper	= "MM";
		public string NavigateURL		= "~/Shared/Admin/EntityAutomation.aspx";	
		
		public string ParentMenuName	= "Users";

		public int? SystemEntityTypeId;
		
		public int? fcModeId;		

		List<String> FCColumns = new List<string>();
		List<FieldConfigurationDataModel> result;

		#endregion

		#region Properties

		public int ApplicationId
		{
			get
			{
				return SessionVariables.RequestProfile.ApplicationId;
			}
		}

		public string EntityName
		{
			get
			{
				return txtEntityName.Text;
			}
		}

		public string ConnectionKeyName
		{
			get
			{
				return txtConnectionKey.Text;
			}
		}

		#endregion

		#region Methods

		public void CreateFCSearchModeEntries()
		{
			fcModeId = GetFCModeId("Developer");

			int? systemEntityTypeId = GetEntityTypeId();

			var dataQuery = new FieldConfigurationDataModel();

			dataQuery.ApplicationId = ApplicationId;
			dataQuery.FieldConfigurationModeId = fcModeId;
			dataQuery.SystemEntityTypeId = systemEntityTypeId;

			result = FieldConfigurationDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			gvSearchColumns.DataSource = result;
			gvSearchColumns.DataBind();

			btnCreateSeachFC.Visible = true;
		}

		public void CreateFCDeveloperModeEntries()
		{
			fcModeId = GetFCModeId("Developer");

			var objDataModel = CreateClassInstance(EntityName + "DataModel");

			var objType = objDataModel.GetType();

			var objProps = objType.GetProperties();

			int i = 0;

			foreach (var propInfo in objProps)
			{
				if (propInfo.Name != BaseModel.BaseColumns.EntityKey)
				{
					var data = new FieldConfigurationDataModel();
					data.Name = propInfo.Name;
					CreateFCEntries(data, fcModeId, i);
					i++;
				}
			}

			CreateFCStandardModeEntries();

			CreateFCSearchModeEntries();
		}

		public void CreateFCStandardModeEntries()
		{
			fcModeId = GetFCModeId("Standard");

			var entityDataModel=EntityName + "DataModel";


			var objDataModel = CreateClassInstance(entityDataModel);

			var objType = objDataModel.GetType();

			var objProps = objType.GetProperties();

			int i = 0;

			foreach (var propInfo in objProps)
			{
				if (propInfo.DeclaringType.Name == entityDataModel)
				{
					var data = new FieldConfigurationDataModel();
					data.Name = propInfo.Name;
					CreateFCEntries(data, fcModeId, i);
					i++;
				}
			}
			
		}

		private static object CreateClassInstance(string className)
		{
			Type type = null;

			var currentAssembly = Assembly.GetExecutingAssembly();
			var assembliyNames = currentAssembly.GetReferencedAssemblies();

			foreach (var aName in assembliyNames)
			{
				try
				{
					var assembly = Assembly.Load(aName);
					type = assembly.GetTypes().First(t => t.Name == className);
					return Activator.CreateInstance(type);
				}
				catch { }
			}

			return null;
		}
		

		public void CreateFCEntries(FieldConfigurationDataModel data, int? fcModeId, int index)
		{
			int? systemEntityTypeId = GetEntityTypeId();

			var dataFC = new FieldConfigurationDataModel();

			dataFC.SystemEntityTypeId = systemEntityTypeId;
			dataFC.ApplicationId = ApplicationId;
			dataFC.FieldConfigurationDisplayName = data.Name;
			dataFC.Name = data.Name;
			dataFC.Value = data.Name;
			
			dataFC.Formatting = "";
			dataFC.ControlType = "TextBox";
			dataFC.GridViewPriority = index;
			dataFC.DetailsViewPriority = index;
			dataFC.FieldConfigurationModeId = fcModeId;
			dataFC.DisplayColumn = 1;
			dataFC.CellCount = 3;

			if (data.Width == null)
			{
				dataFC.Width = 50;
			}
			else
			{
				dataFC.Width = data.Width; 
			}

			if (!string.IsNullOrEmpty(data.HorizontalAlignment))
			{
				dataFC.HorizontalAlignment = data.HorizontalAlignment;
			}
			else
			{
				dataFC.HorizontalAlignment = "Left";
			}		

			var dtFC = FieldConfigurationDataManager.DoesExist(dataFC, SessionVariables.RequestProfile);

			if (dtFC.Rows.Count == 0)
			{

				dataFC.FieldConfigurationId = FieldConfigurationDataManager.Create(dataFC, SessionVariables.RequestProfile);

				var dataDisplayName = new FieldConfigurationDisplayNameDataModel();
				dataDisplayName.FieldConfigurationId = dataFC.FieldConfigurationId;
				dataDisplayName.Value = data.Name;
				dataDisplayName.LanguageId = ApplicationCommon.LanguageId;
				dataDisplayName.IsDefault = 1;

				FieldConfigurationDisplayNameDataManager.Create(dataDisplayName, SessionVariables.RequestProfile);
			}
		}

		public void CreateMenuEntries()
		{
			var dataMenu = new MenuDataModel();

			dataMenu.Name			= ParentMenuName;
			dataMenu.ApplicationId	= ApplicationId;

			var dtMenu = Framework.Components.Core.MenuDataManager.Search(dataMenu, SessionVariables.RequestProfile);
			var parentMenuId = int.Parse(dtMenu.Rows[0][MenuDataModel.DataColumns.MenuId].ToString());

			var data = new MenuDataModel();

			data.Name = EntityName;			
			data.Description = EntityName;
			data.SortOrder = 1;
			data.MenuDisplayName = EntityName;
			data.NavigateURL = NavigateURL;
			data.IsChecked = 1;
			data.IsVisible = 1;
			data.ParentMenuId = parentMenuId;
			data.PrimaryDeveloper = PrimaryDeveloper;
			data.ApplicationId = ApplicationId;

			 var dt1Menu = MenuDataManager.DoesExist(data, SessionVariables.RequestProfile);

			 if (dt1Menu.Rows.Count == 0)
			 {
				 data.MenuId = MenuDataManager.Create(data, SessionVariables.RequestProfile);

				 var dataDisplayName = new MenuDisplayNameDataModel();
				 dataDisplayName.MenuId = data.MenuId;
				 dataDisplayName.Value = EntityName;
				 dataDisplayName.LanguageId = ApplicationCommon.LanguageId;
				 dataDisplayName.IsDefault = 1;

				 MenuDisplayNameDataManager.Create(dataDisplayName, SessionVariables.RequestProfile);

				 var dataCategory = new MenuCategoryDataModel();
				 dataCategory.ApplicationId = ApplicationId;
				 dataCategory.Name = "Standard";
				 var dt = Framework.Components.Core.MenuCategoryDataManager.Search(dataCategory, SessionVariables.RequestProfile);
				 int menuCategoryId = int.Parse(dt.Rows[0][MenuCategoryDataModel.DataColumns.MenuCategoryId].ToString());

				 var mcxmdata = new MenuCategoryXMenuDataModel();
				 mcxmdata.ApplicationId = ApplicationId;
				 mcxmdata.MenuId = data.MenuId;
				 mcxmdata.MenuCategoryId = menuCategoryId;

				 MenuCategoryXMenuDataManager.Create(mcxmdata, SessionVariables.RequestProfile);
			 }
		}

		public void DeleteMenuEntries()
		{
			var data = new MenuDataModel();

			data.Name = EntityName;
			data.ApplicationId = ApplicationId;

			var result = MenuDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			if (result.Count == 0)
			{
				var menuId = result[0].MenuId;

				var dataMenuDisplay = new MenuDisplayNameDataModel();

				dataMenuDisplay.MenuId = menuId;

				var dt = MenuDisplayNameDataManager.Search(dataMenuDisplay, SessionVariables.RequestProfile);

				dataMenuDisplay.MenuDisplayNameId = int.Parse(dt.Rows[0][MenuDisplayNameDataModel.DataColumns.MenuDisplayNameId].ToString());

				MenuDisplayNameDataManager.Delete(dataMenuDisplay, SessionVariables.RequestProfile);

				data.MenuId = menuId;

				MenuDataManager.Delete(data, SessionVariables.RequestProfile);

				MenuCategoryXMenuDataManager.DeleteByMenu(int.Parse(menuId.ToString()), SessionVariables.RequestProfile);
			}
		}

		public int? GetFCModeId(string  fcMode)
		{
			var dataQuery = new FieldConfigurationModeDataModel();
			dataQuery.Name = fcMode;
			dataQuery.ApplicationId = ApplicationId;

			var result = FieldConfigurationModeDataManager.GetEntityDetailsByApplication(dataQuery, SessionVariables.RequestProfile);
			var fcModeId = result[0].FieldConfigurationModeId;

			return fcModeId;
		}

		public void DeleteFCDeveloperModeData()
		{
			fcModeId = GetFCModeId("Developer");

			DeleteFCEntries(fcModeId);
		}

		public void DeleteFCSearchModeData()
		{
			fcModeId = GetFCModeId("SearchControlColumns");

			DeleteFCEntries(fcModeId);			
		}

		public void DeleteFCStandardModeData()
		{
			fcModeId = GetFCModeId("Standard");

			DeleteFCEntries(fcModeId);
		}


		public void DeleteFCEntries(int? fcModeId)
		{
			int? systemEntityTypeId = GetEntityTypeId();

			var dataQuery = new FieldConfigurationDataModel();

			dataQuery.ApplicationId = ApplicationId;
			dataQuery.FieldConfigurationModeId = fcModeId;
			dataQuery.SystemEntityTypeId = systemEntityTypeId;

			var result = FieldConfigurationDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (result.Count > 0)
			{
				for (var i = 0; i < result.Count; i++)
				{
					var fcId = result[i].FieldConfigurationId;

					var data = new FieldConfigurationDisplayNameDataModel();
					data.FieldConfigurationId = fcId;
					var dt = FieldConfigurationDisplayNameDataManager.Search(data, SessionVariables.RequestProfile, SessionVariables.ApplicationMode);
					foreach (DataRow dr in dt.Rows)
					{
						var fcdnid = int.Parse(dr[FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationDisplayNameId].ToString());
						data.FieldConfigurationDisplayNameId = fcdnid;
						FieldConfigurationDisplayNameDataManager.Delete(data, SessionVariables.RequestProfile);
					}
					var fcdata = new FieldConfigurationDataModel();
					fcdata.FieldConfigurationId = fcId;
					FieldConfigurationDataManager.Delete(fcdata, SessionVariables.RequestProfile);
				}
			}
		}

		public void DeleteSetupConfiguration()
		{
			var dt = SetupConfiguration.Search(EntityName, SessionVariables.RequestProfile);

			if (dt.Rows.Count != 0)
			{
				var setupConfigId = int.Parse(dt.Rows[0]["SetupConfigurationId"].ToString());

				SetupConfiguration.Delete(setupConfigId, SessionVariables.RequestProfile);
			}
		}
			
		public int? GetEntityTypeId()
		{
			int? systemEntityTypeId = 0;
            
            systemEntityTypeId = ((int)(SystemEntity)Enum.Parse(typeof(SystemEntity), EntityName));

			return systemEntityTypeId;
		}
		
		private void SetupDropdown()
		{
			var str = new StringBuilder();

			str = new StringBuilder();
			str.AppendLine("$(document).ready(function ()");
			str.AppendLine("        {");
			str.AppendLine("$.ajax(");
			str.AppendLine("        {");
			str.AppendLine("type: \"POST\",");
			str.AppendLine("url: \"http://localhost:53331/API/AutoComplete.asmx/GetConnectionKeyList\",");
			//str.AppendLine("data:\"{\'primaryEntity\':\'" + PrimaryEntity + "\',\'txtName\':\'" + name + "\',\'AuditId\':\'" + SessionVariables.RequestProfile.AuditId + "\'}\",");
			str.AppendLine("contentType: \"application/json; charset=utf-8\",");
			str.AppendLine("dataType: \"json\",");
			str.AppendLine("success: function (msg)");
			str.AppendLine("        {");
			str.AppendLine("$(\"#" + txtConnectionKey.ClientID + "\").kendoAutoComplete({");
			str.AppendLine("    dataSource: msg.d,filter: \"startswith\"");
			str.AppendLine("        });");
			str.AppendLine("        }");
			str.AppendLine("        });");
			str.AppendLine("        });");

			var configScript = str.ToString();

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ConnectionKey", configScript, true);

		}

		#endregion

		#region Events

		protected void btnAutomate_Click(object sender, EventArgs e)
		{
			// creates SystemEntityType table entry

			var data = new SystemEntityTypeDataModel();
			data.EntityName = EntityName;
			
			//var dtEntity = SystemEntityTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

			//if (dtEntity.Rows.Count == 0)
			//{
			//	SystemEntityTypeDataManager.Create(data, SessionVariables.RequestProfile);
			//}

			var dt = SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			SystemEntityTypeId = dt[0].SystemEntityTypeId;

			// creates SetupConfiguration table entry

			var dtConfig = SetupConfiguration.DoesExist(EntityName, SessionVariables.RequestProfile);
			if (dtConfig.Rows.Count == 0)
			{
				SetupConfiguration.Create(100, EntityName, ConnectionKeyName, SessionVariables.RequestProfile);
			}	

			CreateFCDeveloperModeEntries();			

			//CreateMenuEntries();

			lblStatus.Text = EntityName + " entries created. Select FC columns for SearchColumns FCMode ";
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			DeleteFCDeveloperModeData();

			DeleteFCSearchModeData();

			DeleteFCStandardModeData();

			//DeleteMenuEntries();

			DeleteSetupConfiguration();			

			//var data = new SystemEntityTypeDataModel();
			//data.EntityName = EntityName;

			//var dt = SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			//if (dt.Count == 0)
			//{

			//	data.SystemEntityTypeId = dt[0].SystemEntityTypeId;

			//	SystemEntityTypeDataManager.Delete(data, SessionVariables.RequestProfile);
			//}

			lblStatus.Text = EntityName + " entries deleted";

		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{				
			}

			SetupDropdown();			
		}		

		protected void btnCreateSeachFC_Click(object sender, EventArgs e)
		{
			fcModeId = GetFCModeId("SearchControlColumns");			

			for (int count = 0; count < gvSearchColumns.Rows.Count; count++)
			{
				if (((CheckBox)gvSearchColumns.Rows[count].FindControl("chkId")).Checked)
				{
					var data = new FieldConfigurationDataModel();
					data.Name = ((TextBox)gvSearchColumns.Rows[count].FindControl("txtName")).Text;
					data.Width = Convert.ToDecimal(((TextBox)gvSearchColumns.Rows[count].FindControl("txtWidth")).Text);
					data.HorizontalAlignment = ((TextBox)gvSearchColumns.Rows[count].FindControl("txtHorizontalAlignment")).Text;
					CreateFCEntries(data, fcModeId, count);
				}
			}

			lblStatus.Text = EntityName + " - SearchColumns FCMode entries created";
		}
		
		#endregion
	}
}