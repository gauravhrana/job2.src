using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.Core
{
	public partial class MenuCategoryXMenuDataManager : BaseDataManager
	{
		static string DataStoreKey = "";
		//static int ApplicationId;

		static MenuCategoryXMenuDataManager()
		{
			//ApplicationId = SetupConfiguration.ApplicationId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MenuCategoryXMenu");
		}

        #region ToSqlParameter

        public static string ToSQLParameter(MenuCategoryXMenuDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case MenuCategoryXMenuDataModel.DataColumns.MenuCategoryXMenuId:
                    if (data.MenuCategoryXMenuId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuCategoryXMenuDataModel.DataColumns.MenuCategoryXMenuId, data.MenuCategoryXMenuId);


                    }

                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuCategoryXMenuDataModel.DataColumns.MenuCategoryXMenuId);

                    }
                    break;

                case MenuCategoryXMenuDataModel.DataColumns.MenuId:
                    if (data.MenuId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuCategoryXMenuDataModel.DataColumns.MenuId, data.MenuId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuCategoryXMenuDataModel.DataColumns.MenuId);

                    }
                    break;

                case MenuCategoryXMenuDataModel.DataColumns.MenuCategoryId:
                    if (data.MenuCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuCategoryXMenuDataModel.DataColumns.MenuCategoryId, data.MenuCategoryId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuCategoryXMenuDataModel.DataColumns.MenuCategoryId);

                    }
                    break;
                case MenuCategoryXMenuDataModel.DataColumns.MenuCategory:
                    if (!string.IsNullOrEmpty(data.MenuCategory))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuCategoryXMenuDataModel.DataColumns.MenuCategory, data.MenuCategory);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuCategoryXMenuDataModel.DataColumns.MenuCategory);

                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }
            return returnValue;
        }

        #endregion

        #region Create By Menu

        public static void Create(int menuId, int[] menuCategoryIds, RequestProfile requestProfile)
		{
			foreach (int menuCategoryId in menuCategoryIds)
			{
				var sql = "EXEC dbo.MenuCategoryXMenuInsert " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
						", @MenuId		= " + menuId +
						", @MenuCategoryId			= " + menuCategoryId;

				DBDML.RunSQL("MenuCategoryXMenu_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region CreateByMenu

		public static void CreateByMenu(int menuId, int[] menuCategoryIds, RequestProfile requestProfile)
		{
			foreach (var menuCategoryId in menuCategoryIds)
			{
				{
					var sql = "EXEC dbo.MenuCategoryXMenuInsert " +
								" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
								", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
								", @MenuId						=" + menuId +
								", @MenuCategoryId						=" + menuCategoryId;

					DBDML.RunSQL("MenuCategoryXMenu_Insert", sql,
																 DataStoreKey);
				}
			}
		}

		#endregion CreateByMenu

		#region Create By MenuCategorys

		public static void CreateByMenuCategory(int menuCategoryId, int[] menuIds, RequestProfile requestProfile)
		{
			foreach (int menuId in menuIds)
			{
				var sql = "EXEC dbo.MenuCategoryXMenuInsert " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
						", @MenuId					= " + menuId +
						", @MenuCategoryId						= " + menuCategoryId;
				DBDML.RunSQL("MenuCategoryXMenu_Insert", sql, DataStoreKey);
			}
		}
		#endregion

		#region Get By MenuCategory

		public static DataTable GetByMenuCategory(int menuCategoryId, RequestProfile requestProfile)
		{
			var sql = "EXEC MenuCategoryXMenuSearch " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", @MenuCategoryId="+menuCategoryId;

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

        #region Search

        public static DataTable Search(MenuCategoryXMenuDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.MenuCategoryXMenuSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, MenuCategoryXMenuDataModel.DataColumns.MenuId) +
                ", " + ToSQLParameter(data, MenuCategoryXMenuDataModel.DataColumns.MenuCategoryId) +
                ", " + ToSQLParameter(data, MenuCategoryXMenuDataModel.DataColumns.MenuCategory) +
                ", " + ToSQLParameter(data, MenuCategoryXMenuDataModel.DataColumns.MenuCategoryXMenuId);

            var oDT = new DBDataTable("Menu.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

		#region Get By Menu

		public static DataTable GetByMenu(int menuId, RequestProfile requestProfile)
		{
			var sql = "EXEC MenuCategoryXMenuSearch " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", @MenuId		=" + menuId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete By MenuCategory

		public static void DeleteByMenuCategory(int menuCategoryId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.MenuCategoryXMenuDelete ";

			var parameters =	new
								{
										AuditId			= requestProfile.AuditId
									,	MenuCategoryId	= menuCategoryId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Delete By Menu

		public static void DeleteByMenu(int menuId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.MenuCategoryXMenuDelete ";

			var parameters =	new
								{
										AuditId			= requestProfile.AuditId
									,	MenuId			= menuId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

	}
}
