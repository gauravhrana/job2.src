using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace Framework.Components.Core
{
	public partial class MenuDisplayNameDataManager : BaseDataManager
	{

		static string DataStoreKey = "";

		static MenuDisplayNameDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MenuDisplayName");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(MenuDisplayNameDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case MenuDisplayNameDataModel.DataColumns.MenuDisplayNameId:
					if (data.MenuDisplayNameId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDisplayNameDataModel.DataColumns.MenuDisplayNameId, data.MenuDisplayNameId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDisplayNameDataModel.DataColumns.MenuDisplayNameId);

					}
					break;

				case MenuDisplayNameDataModel.DataColumns.MenuId:
					if (data.MenuId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDisplayNameDataModel.DataColumns.MenuId, data.MenuId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDisplayNameDataModel.DataColumns.MenuId);

					}
					break;

				case MenuDisplayNameDataModel.DataColumns.IsDefault:
					if (data.IsDefault != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDisplayNameDataModel.DataColumns.IsDefault, data.IsDefault);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDisplayNameDataModel.DataColumns.IsDefault);

					}
					break;

				case MenuDisplayNameDataModel.DataColumns.Value:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDisplayNameDataModel.DataColumns.Value, data.Value.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDisplayNameDataModel.DataColumns.Value);

					}
					break;

				case MenuDisplayNameDataModel.DataColumns.Menu:
					if (!string.IsNullOrEmpty(data.Menu))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDisplayNameDataModel.DataColumns.Menu, data.Menu.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDisplayNameDataModel.DataColumns.Menu);

					}
					break;

				case MenuDisplayNameDataModel.DataColumns.Language:
					if (!string.IsNullOrEmpty(data.Language))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MenuDisplayNameDataModel.DataColumns.Language, data.Language.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDisplayNameDataModel.DataColumns.Language);

					}
					break;

				case MenuDisplayNameDataModel.DataColumns.LanguageId:
					if (data.LanguageId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MenuDisplayNameDataModel.DataColumns.LanguageId, data.LanguageId);

					}
					else
					{
						returnValue = returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MenuDisplayNameDataModel.DataColumns.LanguageId);

					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{

			DataStoreKey = SetupConfiguration.GetDataStoreKey("MenuDisplayName");
			var sql = "EXEC dbo.MenuDisplayNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("MenuDisplayName.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(MenuDisplayNameDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
		}

        public static List<MenuDisplayNameDataModel> GetEntityDetails(MenuDisplayNameDataModel dataQuery, RequestProfile requestProfile)
		{
            const string sql = @"dbo.MenuDisplayNameSearch ";

			var parameters =
			new
			{
				    AuditId          = requestProfile.AuditId
				,   ApplicationId    = dataQuery.ApplicationId
				,   MenuId           = dataQuery.MenuId
				,   LanguageId       = dataQuery.LanguageId
                ,   Value            = dataQuery.Value
			};

            List<MenuDisplayNameDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
                result = dataAccess.Connection.Query<MenuDisplayNameDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}


		#endregion

		#region Create

		public static void Create(MenuDisplayNameDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("MenuDisplayName.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(MenuDisplayNameDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("MenuDisplayName.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MenuDisplayNameDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.MenuDisplayNameDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				MenuDisplayNameId = dataQuery.MenuDisplayNameId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(MenuDisplayNameDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(MenuDisplayNameDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.MenuDisplayNameInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MenuDisplayNameUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, MenuDisplayNameDataModel.DataColumns.MenuDisplayNameId) +
				", " + ToSQLParameter(data, MenuDisplayNameDataModel.DataColumns.LanguageId) +
				", " + ToSQLParameter(data, MenuDisplayNameDataModel.DataColumns.Value) +
				", " + ToSQLParameter(data, MenuDisplayNameDataModel.DataColumns.MenuId) +
				", " + ToSQLParameter(data, MenuDisplayNameDataModel.DataColumns.IsDefault);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(MenuDisplayNameDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MenuDisplayNameDataModel();
			doesExistRequest.MenuId = data.MenuId;
			doesExistRequest.LanguageId = data.LanguageId;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion

	}
}
