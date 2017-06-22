using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Reflection;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class ConnectionStringDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ConnectionStringDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ConnectionString");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(ConnectionStringDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case ConnectionStringDataModel.DataColumns.ConnectionStringId:
					if (data.ConnectionStringId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ConnectionStringDataModel.DataColumns.ConnectionStringId, data.ConnectionStringId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringDataModel.DataColumns.ConnectionStringId);

					}
					break;

				case ConnectionStringDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringDataModel.DataColumns.Name, data.Name.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringDataModel.DataColumns.Name);

					}
					break;

				case ConnectionStringDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringDataModel.DataColumns.Description, data.Description.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringDataModel.DataColumns.Description);

					}
					break;

				case ConnectionStringDataModel.DataColumns.DataSource:
					if (!string.IsNullOrEmpty(data.DataSource))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringDataModel.DataColumns.DataSource, data.DataSource.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringDataModel.DataColumns.DataSource);
					}
					break;

				case ConnectionStringDataModel.DataColumns.InitialCatalog:
					if (!string.IsNullOrEmpty(data.InitialCatalog))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringDataModel.DataColumns.InitialCatalog, data.InitialCatalog.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringDataModel.DataColumns.InitialCatalog);
					}
					break;

				case ConnectionStringDataModel.DataColumns.UserName:
					if (!string.IsNullOrEmpty(data.UserName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringDataModel.DataColumns.UserName, data.UserName.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringDataModel.DataColumns.UserName);
					}
					break;

				case ConnectionStringDataModel.DataColumns.Password:
					if (!string.IsNullOrEmpty(data.Password))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringDataModel.DataColumns.Password, data.Password.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringDataModel.DataColumns.Password);
					}
					break;

				case ConnectionStringDataModel.DataColumns.ProviderName:
					if (!string.IsNullOrEmpty(data.ProviderName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringDataModel.DataColumns.ProviderName, data.ProviderName.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringDataModel.DataColumns.ProviderName);
					}
					break;

				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ConnectionStringSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("ConnectionString.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(ConnectionStringDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<ConnectionStringDataModel> GetEntityDetails(ConnectionStringDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ConnectionStringSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ConnectionStringId = dataQuery.ConnectionStringId
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				Name = dataQuery.Name
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<ConnectionStringDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<ConnectionStringDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}


			return result;
		}

		#endregion

		#region Create

		public static void Create(ConnectionStringDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("ConnectionString.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ConnectionStringDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("ConnectionString.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ConnectionStringDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ConnectionStringDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ConnectionStringId = dataQuery.ConnectionStringId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(ConnectionStringDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(ConnectionStringDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ConnectionStringInsert  ";
					break;

				case "Update":
					sql += "dbo.ConnectionStringUpdate  ";
					break;

				default:
					break;

			}

			sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						",  " + ToSQLParameter(data, ConnectionStringDataModel.DataColumns.ConnectionStringId) +
						", " + ToSQLParameter(data, ConnectionStringDataModel.DataColumns.Name) +
						", " + ToSQLParameter(data, ConnectionStringDataModel.DataColumns.Description) +
						", " + ToSQLParameter(data, ConnectionStringDataModel.DataColumns.DataSource) +
						", " + ToSQLParameter(data, ConnectionStringDataModel.DataColumns.InitialCatalog) +
						", " + ToSQLParameter(data, ConnectionStringDataModel.DataColumns.UserName) +
						", " + ToSQLParameter(data, ConnectionStringDataModel.DataColumns.Password) +
						", " + ToSQLParameter(data, ConnectionStringDataModel.DataColumns.ProviderName);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(ConnectionStringDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ConnectionStringDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion

	}
}
