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
	public partial class StoredProcedureLogRawDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static StoredProcedureLogRawDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("StoredProcedureLogRaw");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(StoredProcedureLogRawDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{

				case StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogRawId:
					if (data.StoredProcedureLogRawId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogRawId, data.StoredProcedureLogRawId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogRawId);

					}
					break;

				case StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogId:
					if (data.StoredProcedureLogId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogId, data.StoredProcedureLogId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogId);

					}
					break;

				case StoredProcedureLogRawDataModel.DataColumns.InputParameters:
					if (!string.IsNullOrEmpty(data.InputParameters))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StoredProcedureLogRawDataModel.DataColumns.InputParameters, data.InputParameters);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogRawDataModel.DataColumns.InputParameters);

					}
					break;

				case StoredProcedureLogRawDataModel.DataColumns.InputValues:
					if (!string.IsNullOrEmpty(data.InputValues))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StoredProcedureLogRawDataModel.DataColumns.InputValues, data.InputValues);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogRawDataModel.DataColumns.InputValues);

					}
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<StoredProcedureLogRawDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(StoredProcedureLogRawDataModel.Empty, requestProfile);
		}

		#endregion

		#region GetDetails

        public static StoredProcedureLogRawDataModel GetDetails(StoredProcedureLogRawDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		public static List<StoredProcedureLogRawDataModel> GetEntityDetails(StoredProcedureLogRawDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.StoredProcedureLogRawSearch ";

			var parameters =
			new
			{
				    AuditId                     = requestProfile.AuditId
				,   ApplicationMode             = requestProfile.ApplicationModeId
				,   StoredProcedureLogRawId     = dataQuery.StoredProcedureLogRawId
				,   StoredProcedureLogId        = dataQuery.StoredProcedureLogId
			};

			List<StoredProcedureLogRawDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<StoredProcedureLogRawDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(StoredProcedureLogRawDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("StoredProcedureLogRaw.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(StoredProcedureLogRawDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("StoredProcedureLogRaw.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(StoredProcedureLogRawDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.StoredProcedureLogRawDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				StoredProcedureLogRawId = data.StoredProcedureLogRawId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region GetStoredProcedureLogRaws

		public static DataTable GetStoredProcedureLogRaws(StoredProcedureLogRawDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.StoredProcedureLogRawSearch " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogRawId);

			var oDT = new DBDataTable("Get StoredProcedureLogRaws", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion GetStoredProcedureLogRaws

		#region Search

		public static DataTable Search(StoredProcedureLogRawDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(StoredProcedureLogRawDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.StoredProcedureLogRawInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					//", " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.StoredProcedureLogRawUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogRawId) +
						", " + ToSQLParameter(data, StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogId) +
						", " + ToSQLParameter(data, StoredProcedureLogRawDataModel.DataColumns.InputParameters) +
						", " + ToSQLParameter(data, StoredProcedureLogRawDataModel.DataColumns.InputValues);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(StoredProcedureLogRawDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new StoredProcedureLogRawDataModel();
			doesExistRequest.StoredProcedureLogId = data.StoredProcedureLogId;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
		}

		#endregion

	}
}
