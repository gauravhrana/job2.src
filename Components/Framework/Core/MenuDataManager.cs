using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class MenuDataManager : StandardDataManager
	{
        static readonly string DataStoreKey = "";

		static MenuDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Menu");
		}

		#region GetList

        public static List<MenuDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(MenuDataModel.Empty, requestProfile, 0);
		}

		public static List<MenuDataModel> GetList(int? applicationId, RequestProfile requestProfile)
		{
            var data = new MenuDataModel() { ApplicationId = applicationId };
            return GetEntityDetails(data, requestProfile, 0);
		}

		#endregion

		#region ToSQLParameter

		public static string ToSQLParameter(MenuDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case MenuDataModel.DataColumns.MenuId:
					if (data.MenuId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDataModel.DataColumns.MenuId, data.MenuId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.MenuId);
					}
					break;

				case MenuDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.ApplicationId);
					}
					break;

				case MenuDataModel.DataColumns.ParentMenuId:
					if (data.ParentMenuId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDataModel.DataColumns.ParentMenuId, data.ParentMenuId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.ParentMenuId);

					}
					break;

				case MenuDataModel.DataColumns.IsVisible:
					if (data.IsVisible != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDataModel.DataColumns.IsVisible, data.IsVisible);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.IsVisible);

					}
					break;

				case MenuDataModel.DataColumns.IsChecked:
					if (data.IsChecked != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDataModel.DataColumns.IsChecked, data.IsChecked);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.IsChecked);

					}
					break;

				case MenuDataModel.DataColumns.MenuDisplayName:
					if (!string.IsNullOrEmpty(data.MenuDisplayName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDataModel.DataColumns.MenuDisplayName, data.MenuDisplayName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.MenuDisplayName);

					}
                    break;

                case MenuDataModel.DataColumns.ApplicationModule:
                    if (!string.IsNullOrEmpty(data.ApplicationModule))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDataModel.DataColumns.ApplicationModule, data.ApplicationModule.Trim());
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.ApplicationModule);
                    }
                    break;

				case MenuDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDataModel.DataColumns.Application, data.Application.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.Application);
					}
					break;

				case MenuDataModel.DataColumns.PrimaryDeveloper:
					if (!string.IsNullOrEmpty(data.PrimaryDeveloper))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDataModel.DataColumns.PrimaryDeveloper, data.PrimaryDeveloper.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.PrimaryDeveloper);

					}
					break;

				case MenuDataModel.DataColumns.Value:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDataModel.DataColumns.Value, data.Value.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.Value);

					}
					break;

				case MenuDataModel.DataColumns.NavigateURL:
					if (!string.IsNullOrEmpty(data.NavigateURL))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDataModel.DataColumns.NavigateURL, data.NavigateURL.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDataModel.DataColumns.NavigateURL);

					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetDetails

        public static MenuDataModel GetDetails(MenuDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<MenuDataModel> GetEntityDetails(MenuDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MenuSearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
				,   ApplicationId       = dataQuery.ApplicationId
				,   MenuId              = dataQuery.MenuId
				,   Name                = dataQuery.Name
				,   ApplicationMode     = requestProfile.ApplicationModeId
				,   Value               = dataQuery.Value
				,   PrimaryDeveloper    = dataQuery.PrimaryDeveloper
				,   ParentMenuId        = dataQuery.ParentMenuId
				,   IsVisible           = dataQuery.IsVisible
				,   IsChecked           = dataQuery.IsChecked
                ,   ApplicationModule   = dataQuery.ApplicationModule
				,   ReturnAuditInfo     = returnAuditInfo
			};

			List<MenuDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MenuDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static int Create(MenuDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			var menuId = DBDML.RunScalarSQL("Menu.Insert", sql, DataStoreKey);
			return Convert.ToInt32(menuId);
		}

		#endregion

		#region Update

		public static void Update(MenuDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("Menu.Update", sql, DataStoreKey);
		}

		public static void UpdateParentMenuOnly(MenuDataModel data, int auditId)
		{
			var sql = String.Empty;
			sql = "dbo.MenuUpdateParentMenuOnly  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
						", " + ToSQLParameter(data, MenuDataModel.DataColumns.MenuId) +
						", " + ToSQLParameter(data, MenuDataModel.DataColumns.ParentMenuId);

			DBDML.RunSQL("Menu.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MenuDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.MenuDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				MenuId = dataQuery.MenuId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

        public static void DeleteHard(MenuDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.MenuDeleteHard ";

            var parameters =
            new
            {
                    AuditId = requestProfile.AuditId
                ,   KeyId   = dataQuery.MenuId
                ,   KeyType = "MenuId"

            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }

		#endregion

		#region Search

		public static DataTable Search(MenuDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(MenuDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.MenuInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MenuUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, MenuDataModel.DataColumns.MenuId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, MenuDataModel.DataColumns.Value) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, MenuDataModel.DataColumns.IsVisible) +
						", " + ToSQLParameter(data, MenuDataModel.DataColumns.IsChecked) +
						", " + ToSQLParameter(data, MenuDataModel.DataColumns.ParentMenuId) +
						//", " + ToSQLParameter(data, MenuDataModel.DataColumns.ApplicationId) +
						", " + ToSQLParameter(data, MenuDataModel.DataColumns.PrimaryDeveloper) +
                        ", " + ToSQLParameter(data, MenuDataModel.DataColumns.ApplicationModule) +
						", " + ToSQLParameter(data, MenuDataModel.DataColumns.NavigateURL);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(MenuDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest               = new MenuDataModel();
			doesExistRequest.Name              = data.Name;
			doesExistRequest.ParentMenuId      = data.ParentMenuId;
            doesExistRequest.ApplicationId     = data.ApplicationId;
            doesExistRequest.ApplicationModule = data.ApplicationModule;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region MenuListOfParentMenuOnly

		public static DataTable ListOfParentMenuOnly(MenuDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.MenuListOfParentMenuOnly " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("Menu.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		public static List<MenuDataModel> GetParentMenuList(RequestProfile requestProfile)
		{
			const string sql = @"dbo.MenuListOfParentMenuOnly ";

			var parameters =	new
								{
									AuditId = requestProfile.AuditId
								};

			List<MenuDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MenuDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

                result = result.OrderBy(x => x.Name).ToList();    
			}

			return result;
		}

        #endregion

        public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile, int languageId)
        {
            var tempMapping = new Dictionary<int, int>();

            // get all records for old Application Id
            var menuData = new MenuDataModel();
            menuData.ApplicationId = oldApplicationId;
            menuData.ParentMenuId = 12393;
            var menuList = MenuDataManager.GetEntityDetails(menuData, requestProfile, 0);

            var menuDisplayNameData = new MenuDisplayNameDataModel();
            var menuDisplayNameList = MenuDisplayNameDataManager.GetEntityDetails(menuDisplayNameData, requestProfile);

            // formaulate a new request Profile which will have new Applicationid
            var newRequestProfile = new RequestProfile();
            newRequestProfile.ApplicationId = newApplicationId;
            newRequestProfile.AuditId = requestProfile.AuditId;

            foreach (var menuItem in menuList)
            {
                var parentMenuName = menuItem.ParentMenu;

                //get new fc mode id based on fc mode name
                //var newParentMenuId = timeZoneList.Find(x => x.Name == fcModeName).TimeZoneId;

                var data           = new MenuDataModel();
                data.ApplicationId = newApplicationId;
                data.Name          = menuItem.Name;
                data.ParentMenuId  = menuItem.ParentMenuId;

                // check for existing record in new Application Id
                if(!DoesExist(data, newRequestProfile))
                {
                    data.Description       = menuItem.Description;
                    data.SortOrder         = menuItem.SortOrder;
                    data.IsChecked         = menuItem.IsChecked;
                    data.IsVisible         = menuItem.IsVisible;
                    data.NavigateURL       = menuItem.NavigateURL;
                    data.PrimaryDeveloper  = menuItem.PrimaryDeveloper;
                    data.Value             = menuItem.Value;
                    data.ApplicationModule = menuItem.ApplicationModule;

                    if (string.IsNullOrEmpty(data.PrimaryDeveloper))
                    {
                        data.PrimaryDeveloper = " ";
                    }

                    var menuDisplayNameValue = menuItem.Value;

                    try
                    {
                        menuDisplayNameValue = menuDisplayNameList.Find(x => x.MenuId == menuItem.MenuId && x.IsDefault == 1).Value;
                    }
                    catch { }                        

                    //create in new application id
                    var newMenuId              = Create(data, newRequestProfile);

                    var dataDisplayName        = new MenuDisplayNameDataModel();
                    dataDisplayName.MenuId     = newMenuId;
                    dataDisplayName.Value      = menuDisplayNameValue;
                    dataDisplayName.LanguageId = languageId;
                    dataDisplayName.IsDefault  = 1;

                    // create display name entry according to the default display name
                    MenuDisplayNameDataManager.Create(dataDisplayName, newRequestProfile);

                    tempMapping.Add(menuItem.MenuId.Value, newMenuId);

                }

            }
            
            //menuData.ApplicationId = newApplicationId;
            //var newMenuList = MenuDataManager.GetEntityDetails(menuData, newRequestProfile);

            //foreach (var newMenuItem in menuList)
            //{
            //    if (newMenuItem.ParentMenuId != null)
            //    {
            //        var newMenuData = new MenuDataModel();
            //        newMenuData.MenuId = newMenuItem.MenuId;
            //        if (tempMapping.ContainsKey(newMenuItem.ParentMenuId.Value))
            //        {
            //            menuData.ParentMenuId = tempMapping[newMenuItem.ParentMenuId.Value];
            //            try
            //            {
            //                UpdateParentMenuOnly(newMenuData, newRequestProfile.AuditId);
            //            }
            //            catch { }
            //        }
            //    }
            //}
        }

        public static void SyncModule(string newModule, int newApplicationId, 
            string sourceModule, int sourceApplicationId, RequestProfile requestProfile)
        {          

            var sourceRequestProfile = new RequestProfile();
            sourceRequestProfile.ApplicationId = sourceApplicationId;
            sourceRequestProfile.AuditId = requestProfile.AuditId;

            var newRequestProfile = new RequestProfile();
            newRequestProfile.ApplicationId = newApplicationId;
            newRequestProfile.AuditId = requestProfile.AuditId; 
            
            // get Language id of "Standard"
            var languageData = new LanguageDataModel();
            languageData.Name = "English";

            var languageRecords = LanguageDataManager.GetEntityDetails(languageData, newRequestProfile, 0);

            if (languageRecords.Count > 0)
            {

                DeleteHardExistingRecords(newModule, newRequestProfile);

                var languageId = languageRecords[0].LanguageId.Value;

                var dataQuery = new MenuDataModel();
                dataQuery.ApplicationModule = sourceModule;
                dataQuery.ApplicationId = sourceApplicationId;

                var dataSourceMenu = MenuDataManager.GetEntityDetails(dataQuery, sourceRequestProfile, 0);

                var menuDisplayNameData = new MenuDisplayNameDataModel();
                var menuDisplayNameList = MenuDisplayNameDataManager.GetEntityDetails(menuDisplayNameData, sourceRequestProfile);

                var sortOrderSeed = 101;

                var listSourceRecords = dataSourceMenu
                                .Where(t => t.ParentMenuId == null)
                                .Select(t => t)
                                .OrderBy(t => t.SortOrder).ToList();

                if (listSourceRecords.Count > 0)
                {
                    foreach (MenuDataModel menuSourceRecord in listSourceRecords)
                    {
                        var menuModel               = new MenuDataModel();

                        menuModel.Name              = menuSourceRecord.Name;
                        menuModel.Value             = menuSourceRecord.Value;
                        menuModel.Description       = menuSourceRecord.Description;
                        menuModel.IsVisible         = menuSourceRecord.IsVisible;
                        menuModel.IsChecked         = menuSourceRecord.IsChecked;
                        menuModel.ParentMenuId      = menuSourceRecord.ParentMenuId;
                        menuModel.PrimaryDeveloper  = menuSourceRecord.PrimaryDeveloper;
                        menuModel.NavigateURL       = menuSourceRecord.NavigateURL;

                        menuModel.ApplicationId     = newApplicationId;
                        menuModel.ApplicationModule = newModule;
                        menuModel.SortOrder         = sortOrderSeed;

                        if(!DoesExist(menuModel, newRequestProfile))
                        {

                            var newMenuId = MenuDataManager.Create(menuModel, newRequestProfile);

                            var menuDisplayNameValue = menuModel.Value;

                            try
                            {
                                menuDisplayNameValue = menuDisplayNameList.Find(x => x.MenuId == menuSourceRecord.MenuId && x.IsDefault == 1).Value;
                            }
                            catch { }

                            var dataDisplayName        = new MenuDisplayNameDataModel();
                            dataDisplayName.MenuId     = newMenuId;
                            dataDisplayName.Value      = menuDisplayNameValue;
                            dataDisplayName.LanguageId = languageId;
                            dataDisplayName.IsDefault  = 1;

                            // create display name entry according to the default display name
                            MenuDisplayNameDataManager.Create(dataDisplayName, newRequestProfile);
                            
                            sortOrderSeed++;

                            var childMenuRecordsList = dataSourceMenu
                                        .Where(t => t.ParentMenuId == menuSourceRecord.MenuId)
                                        .Select(t => t)
                                        .OrderBy(t => t.SortOrder).ToList();

                            CreateChildMenuRecords(newMenuId, newModule,
                                childMenuRecordsList, dataSourceMenu, menuDisplayNameList,
                                languageId, newRequestProfile);
                        }

                    }

                    // get menu category id of "Standard"
                    var menuCategoryData = new MenuCategoryDataModel();
                    menuCategoryData.Name = "Standard";

                    var menuCategoryRecords = MenuCategoryDataManager.GetEntityDetails(menuCategoryData, newRequestProfile, 0);

                    if (menuCategoryRecords.Count > 0)
                    {
                        var menuCategoryId = menuCategoryRecords[0].MenuCategoryId;

                        // get newly inserted records
                        dataQuery = new MenuDataModel();
                        dataQuery.ApplicationModule = newModule;
                        dataQuery.ApplicationId = newApplicationId;
                        var newMenuRecords = GetEntityDetails(dataQuery, newRequestProfile);

                        var listMenuIds = newMenuRecords.Select(x => x.MenuId.Value).ToArray();

                        // add MenuCategoryXMenu records
                        MenuCategoryXMenuDataManager.CreateByMenuCategory(menuCategoryId.Value, listMenuIds, newRequestProfile);
                    }
                }
            }

        }

        private static void CreateChildMenuRecords(int newParentMenuId, string newApplicationModule,
            List<MenuDataModel> childMenuRecords, List<MenuDataModel> dataSourceMenu, List<MenuDisplayNameDataModel> menuDisplayNameList,
            int languageId, RequestProfile newRequestProfile)
        {
            if (childMenuRecords.Count > 0)
            {
                var sortOrderSeed = 1;
                foreach (MenuDataModel menuSourceRecord in childMenuRecords)
                {
                    var menuModel               = new MenuDataModel();

                    menuModel.Name              = menuSourceRecord.Name;
                    menuModel.Value             = menuSourceRecord.Value;
                    menuModel.Description       = menuSourceRecord.Description;
                    menuModel.IsVisible         = menuSourceRecord.IsVisible;
                    menuModel.IsChecked         = menuSourceRecord.IsChecked;
                    menuModel.PrimaryDeveloper  = menuSourceRecord.PrimaryDeveloper;
                    menuModel.NavigateURL       = menuSourceRecord.NavigateURL;

                    menuModel.ParentMenuId      = newParentMenuId;
                    menuModel.ApplicationId     = newRequestProfile.ApplicationId;
                    menuModel.ApplicationModule = newApplicationModule;
                    menuModel.SortOrder         = sortOrderSeed;

                    if(!DoesExist(menuModel, newRequestProfile))
                    {

                        var newMenuId = MenuDataManager.Create(menuModel, newRequestProfile);

                        var menuDisplayNameValue = menuModel.Value;

                        try
                        {
                            menuDisplayNameValue = menuDisplayNameList.Find(x => x.MenuId == menuSourceRecord.MenuId && x.IsDefault == 1).Value;
                        }
                        catch { }

                        var dataDisplayName        = new MenuDisplayNameDataModel();
                        dataDisplayName.MenuId     = newMenuId;
                        dataDisplayName.Value      = menuDisplayNameValue;
                        dataDisplayName.LanguageId = languageId;
                        dataDisplayName.IsDefault  = 1;

                        // create display name entry according to the default display name
                        MenuDisplayNameDataManager.Create(dataDisplayName, newRequestProfile);

                        sortOrderSeed++;

                        var childMenuRecordsList = dataSourceMenu
                                    .Where(t => t.ParentMenuId == menuSourceRecord.MenuId)
                                    .Select(t => t)
                                    .OrderBy(t => t.SortOrder).ToList();

                        CreateChildMenuRecords(newMenuId, newApplicationModule,
                            childMenuRecordsList, dataSourceMenu, menuDisplayNameList,
                            languageId, newRequestProfile);
                    }

                }
            }
        }

        public static void DeleteHardExistingRecords(string newModule, RequestProfile requestProfile)
        {

            // get newly inserted records
            var dataQuery = new MenuDataModel();
            dataQuery.ApplicationModule = newModule;
            dataQuery.ApplicationId = requestProfile.ApplicationId;
            var newMenuRecords = GetEntityDetails(dataQuery, requestProfile);

            foreach (var menuRecord in newMenuRecords)
            {
                var menuData = new MenuDataModel();
                menuData.MenuId = menuRecord.MenuId;

                MenuDataManager.DeleteHard(menuData, requestProfile);
            }
        }

	}
}
