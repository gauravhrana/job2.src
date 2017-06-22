using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{
	public partial class StoredProcedureLogDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static StoredProcedureLogDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("StoredProcedureLog");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(StoredProcedureLogDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{

				case StoredProcedureLogDataModel.DataColumns.StoredProcedureLogId:
					if (data.StoredProcedureLogId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StoredProcedureLogDataModel.DataColumns.StoredProcedureLogId, data.StoredProcedureLogId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogDataModel.DataColumns.StoredProcedureLogId);
					}
					break;

				case StoredProcedureLogDataModel.DataColumns.TimeOfExecution:
					if (data.TimeOfExecution != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StoredProcedureLogDataModel.DataColumns.TimeOfExecution, data.TimeOfExecution);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogDataModel.DataColumns.TimeOfExecution);
					}
					break;

				case StoredProcedureLogDataModel.DataColumns.ExecutedBy:
					if (!string.IsNullOrEmpty(data.ExecutedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StoredProcedureLogDataModel.DataColumns.ExecutedBy, data.ExecutedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogDataModel.DataColumns.ExecutedBy);
					}
					break;
			}

			return returnValue;
		}

		#endregion

		#region GetList

        public static List<StoredProcedureLogDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(StoredProcedureLogDataModel.Empty, requestProfile);
		}

		#endregion

		#region GetDetails

        public static StoredProcedureLogDataModel GetDetails(StoredProcedureLogDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		public static List<StoredProcedureLogDataModel> GetEntityDetails(StoredProcedureLogDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.StoredProcedureLogSearch ";

			var parameters =
			new
			{
				    AuditId                 = requestProfile.AuditId
				,   ApplicationMode         = requestProfile.ApplicationModeId
				,   StoredProcedureLogId    = dataQuery.StoredProcedureLogId
				,   Name                    = dataQuery.Name
			};

			List<StoredProcedureLogDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<StoredProcedureLogDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(StoredProcedureLogDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("StoredProcedureLog.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(StoredProcedureLogDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("StoredProcedureLog.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(StoredProcedureLogDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.StoredProcedureLogDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				StoredProcedureLogId = data.StoredProcedureLogId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region GetStoredProcedureLogDetails

		public static DataTable GetStoredProcedureLogDetails(StoredProcedureLogDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.StoredProcedureLogSearch " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(data, StoredProcedureLogDataModel.DataColumns.StoredProcedureLogId);

			var oDT = new DBDataTable("Get StoredProcedureLogDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion GetStoredProcedureLogDetails

		#region Search

		public static DataTable Search(StoredProcedureLogDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable(); 

			return table;
		}

		#endregion

		#region Save

		private static string Save(StoredProcedureLogDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.StoredProcedureLogInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					//", " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.StoredProcedureLogUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, StoredProcedureLogDataModel.DataColumns.StoredProcedureLogId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StoredProcedureLogDataModel.DataColumns.TimeOfExecution) +
						", " + ToSQLParameter(data, StoredProcedureLogDataModel.DataColumns.ExecutedBy);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(StoredProcedureLogDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new StoredProcedureLogDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
		}

		#endregion

	}
}
