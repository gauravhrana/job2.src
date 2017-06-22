using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Reflection;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Text;
using System.Globalization;
using Framework.UI.Web.BaseClasses;
using System.Web.UI.HtmlControls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.ApplicationManagement.Development
{
	public partial class CheckDefaultView : Framework.UI.Web.BaseClasses.PageBasePage
	{
		public int? fcModeId;

		List<String> FCColumns = new List<string>();
		
		private int DetailUserPreferenceCategoryId
		{
			get
			{
				return Convert.ToInt32(ViewState["DetailUserPreferenceCategoryId"]);
			}
			set
			{
				ViewState["DetailUserPreferenceCategoryId"] = value;
			}
		}

		private string ApplicationId
		{
			get
			{
				if (ViewState["ApplicationId"] == null)
				{
					ViewState["ApplicationId"] = SessionVariables.RequestProfile.ApplicationId.ToString();
				}
				return ViewState["ApplicationId"].ToString();
			}
			set
			{
				ViewState["ApplicationId"] = value;
			}
		}		

		#region Methods

		public void CreateFCStandardModeEntries(string entityName)
		{
			fcModeId = GetFCModeId("Standard");

			var entityDataModel = entityName + "DataModel";

			var objDataModel = CreateClassInstance(entityDataModel);

			if (objDataModel == null)
			{
				var objType = objDataModel.GetType();

				var objProps = objType.GetProperties();

				int i = 0;

				foreach (var propInfo in objProps)
				{
					if (propInfo.DeclaringType.Name == entityDataModel)
					{
						var data = new FieldConfigurationDataModel();
						data.Name = propInfo.Name;
						CreateFCEntries(data, fcModeId, i, entityName);
						i++;
					}
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

		public void CreateFCEntries(FieldConfigurationDataModel data, int? fcModeId, int index, string entityName)
		{
			int? systemEntityTypeId = GetEntityTypeId(entityName);

			var dataFC = new FieldConfigurationDataModel();

			dataFC.SystemEntityTypeId = systemEntityTypeId;
			dataFC.ApplicationId = Convert.ToInt32(ddlApplication.SelectedValue);
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

		public int? GetEntityTypeId(string entityName)
		{
			int? systemEntityTypeId = 0;

			systemEntityTypeId = ((int)(SystemEntity)Enum.Parse(typeof(SystemEntity), entityName));

			return systemEntityTypeId;
		}

		public void CreateFCDeveloperModeEntries(string entityName)
		{
			fcModeId = GetFCModeId("Developer");

			var objDataModel = CreateClassInstance(entityName + "DataModel");
			if (objDataModel == null)
			{
				var objType = objDataModel.GetType();

				var objProps = objType.GetProperties();

				int i = 0;

				foreach (var propInfo in objProps)
				{
					var data = new FieldConfigurationDataModel();
					data.Name = propInfo.Name;
					CreateFCEntries(data, fcModeId, i, entityName);
					i++;
				}				
			}
			
		}

		public int? GetFCModeId(string fcMode)
		{
			var dataQuery = new FieldConfigurationModeDataModel();
			dataQuery.Name = fcMode;
			dataQuery.ApplicationId = Convert.ToInt32(ApplicationId);

			var result = FieldConfigurationModeDataManager.GetEntityDetailsByApplication(dataQuery, SessionVariables.RequestProfile);
			var fcModeId = result[0].FieldConfigurationModeId;

			return fcModeId;
		}

		private void DataBind()
		{
			var data = new FieldConfigurationDataModel(); 

			data.FieldConfigurationModeId = GetFCModeId(ddlView.SelectedValue);
			data.ApplicationId = Convert.ToInt32(ApplicationId);

			var dt = FieldConfigurationDataManager.CheckDefaultView(data, SessionVariables.RequestProfile);

			var GroupByField = drpGroupBy.SelectedValue;
			
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			var distinctTabNames = (from row in dt.AsEnumerable()
									orderby row[GroupByField].ToString().Trim()
									select row[GroupByField].ToString().Trim())
											.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

			foreach (var tabName in distinctTabNames)
			{

				var dtGroupingResult = dt.AsEnumerable().Where(t => t[GroupByField].ToString() == tabName).CopyToDataTable();

				var totalRecordsInTab = dtGroupingResult.Rows.Count;

				var groupHeader = tabName;

				var strHeader = groupHeader + " (" + totalRecordsInTab.ToString(CultureInfo.InvariantCulture) + ")";

				if (tabName == "True")
				{
					chkListDataWithview.DataSource = dtGroupingResult;
					chkListDataWithview.DataTextField = "EntityName";
					chkListDataWithview.DataValueField = "EntityName";
					chkListDataWithview.DataBind();
					lblWithView.Text = strHeader;

				}
				else
				{
					chkListData.DataSource = dtGroupingResult;
					chkListData.DataTextField = "EntityName";
					chkListData.DataValueField = "EntityName";
					chkListData.DataBind();

					lblView.Text = strHeader;					
				}
			}
		}

		#endregion

		#region Events
		
		protected override void OnInit(EventArgs e)
		{			
			SettingCategory = "CheckDefaultViewSettingsDefaultView";
			DetailUserPreferenceCategoryId = PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(SettingCategory, SettingCategory);
			
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup(string.Empty);
			bcControl.GenerateMenu();
		}		
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{				

				var appData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(appData, ddlApplication, DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.Name,
					DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.ApplicationId);
				

				drpGroupBy.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GroupBy, SettingCategory);				
				//ddlApplication.SelectedValue = PerferenceUtility.GetUserPreferenceByKey("DefaultViewApplicationId", SettingCategory);

				DataBind();
							
			}			
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			ApplicationId = ddlApplication.SelectedValue;

			DataBind();
			
			//PerferenceUtility.UpdateUserPreference(SettingCategory, "DefaultViewApplicationId", ddlApplication.SelectedValue);			
			PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.GroupBy, drpGroupBy.SelectedValue);
		}

		protected void btnDefault_Click(object sender, EventArgs e)
		{
			var selectedItems = chkListData.Items.Cast<ListItem>().Where(x => x.Selected).Select(li => li.Value).ToList();

			for (int i = 0; i < selectedItems.Count; i++)
			{
				CreateFCDeveloperModeEntries(selectedItems[i]);
			}

			DataBind();
		}

		protected void btnStandard_Click(object sender, EventArgs e)
		{
			var selectedItems = chkListData.Items.Cast<ListItem>().Where(x => x.Selected).Select(li => li.Value).ToList();

			for (int i = 0; i < selectedItems.Count; i++)
			{
				CreateFCStandardModeEntries(selectedItems[i]);
			}

			DataBind();
		}
		
		#endregion
	}
}