using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Configuration
{
	public partial class DBComponentNameDataManager : StandardDataManager
	{
		static readonly string DataStoreKey = "";
		static readonly int ApplicationId;

		private static DataTable DBComponentNameList;

		static DBComponentNameDataManager()
		{
			ApplicationId = SetupConfiguration.ApplicationId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DBComponentName");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(DBComponentNameDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DBComponentNameDataModel.DataColumns.DBComponentNameId:
					if (data.DBComponentNameId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DBComponentNameDataModel.DataColumns.DBComponentNameId, data.DBComponentNameId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DBComponentNameDataModel.DataColumns.DBComponentNameId);
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

		public static DataTable GetList(int auditId, bool forceRefresh = false)
		{
			if (DBComponentNameList == null || forceRefresh)
			{
				var sql = "EXEC dbo.DBComponentNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId);

				var oDT = new DBDataTable("DBComponentName.List", sql, DataStoreKey);

				DBComponentNameList = oDT.DBTable;
			}

			// return clone copy in case databound ... but still should not be required
			return DBComponentNameList.Clone();
		}

		#endregion

		#region GetEntitySearch

		static public List<DBComponentNameDataModel> GetEntitySearch(DBComponentNameDataModel obj, int auditId)
		{
			var dt = Search(obj, auditId, 0);//SessionVariables.ApplicationMode);

			var list = ToList(dt);

			return list;
		}

		#endregion

		#region ToList

		static private List<DBComponentNameDataModel> ToList(DataTable dt)
		{
			var list = new List<DBComponentNameDataModel>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var dataItem = new DBComponentNameDataModel();

					dataItem.DBComponentNameId = (int?)dr[DBComponentNameDataModel.DataColumns.DBComponentNameId];

					SetStandardInfo(dataItem, dr);

					list.Add(dataItem);
				}
			}
			return list;
		}

		#endregion


		#region GetDetails

		public static DataTable GetDetails(DBComponentNameDataModel data, int auditId)
		{
			var sql = "EXEC dbo.DBComponentNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo ,BaseDataManager .ReturnAduitInfoOnDetails ) +
				", " + ToSQLParameter(data, DBComponentNameDataModel.DataColumns.DBComponentNameId);

			var oDT = new DBDataTable("DBComponentName.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

		public static List<DBComponentNameDataModel> GetEntityDetails(DBComponentNameDataModel dataQuery, int auditId, int applicationModeId = 0)
		{
			const string sql = @"dbo.DBComponentNameSearch ";

			var parameters =
			new
			{
					AuditId						= auditId
				,	ApplicationMode				= applicationModeId
				,	DBComponentNameId			= dataQuery.DBComponentNameId
				,	ReturnAuditInfoOnDetails	= BaseDataManager.ReturnAuditInfoOnDetails
				,	Name						= dataQuery.Name
			};

			List<DBComponentNameDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				var sp = Log4Net.LogSQLInfoStart(sql, DataStoreKey, parameters);

				result = dataAccess.Connection.Query<DBComponentNameDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

				Log4Net.LogSQLInfoStop(sp, result.Count);
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(DBComponentNameDataModel data, int auditId)
		{
			var sql = Save(data, auditId, "Create");
			DBDML.RunSQL("DBComponentName.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(DBComponentNameDataModel data, int auditId)
		{
			var sql = Save(data, auditId, "Update");
			DBDML.RunSQL("DBComponentName.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DBComponentNameDataModel data, int auditId)
		{
			const string sql = @"dbo.DBComponentNameDelete ";

			var parameters =	new
								{
										AuditId				= auditId
									,	DBComponentNameId	= data.DBComponentNameId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				var sp = Log4Net.LogSQLInfoStart(sql, DataStoreKey, parameters);

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				Log4Net.LogSQLInfoStop(sp);
			}
		}

		#endregion

		#region Search

		public static DataTable Search(DBComponentNameDataModel data, int auditId, int applicationModeId = 0)
		{
			// formulate SQL
			var sql = "EXEC dbo.DBComponentNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, applicationModeId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, DBComponentNameDataModel.DataColumns.DBComponentNameId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			var oDT = new DBDataTable("DBComponentName.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

		private static string Save(DBComponentNameDataModel data, int auditId, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.DBComponentNameInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId);
					break;

				case "Update":
					sql += "dbo.DBComponentNameUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, DBComponentNameDataModel.DataColumns.DBComponentNameId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(DBComponentNameDataModel data, int auditId)
		{
			var sql = "EXEC dbo.DBComponentNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, DBComponentNameDataModel.DataColumns.DBComponentNameId);

			var oDT = new DBDataTable("DBComponentName.DoesExist", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion
	}
}