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
	public partial class StoredProcedureLogDetailDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static StoredProcedureLogDetailDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("StoredProcedureLogDetail");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(StoredProcedureLogDetailDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{

				case StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogDetailId:
					if (data.StoredProcedureLogDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogDetailId, data.StoredProcedureLogDetailId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogDetailId);
					}
					break;

				case StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogId:
					if (data.StoredProcedureLogId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogId, data.StoredProcedureLogId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogId);
					}
					break;

				case StoredProcedureLogDetailDataModel.DataColumns.ParameterName:
					if (!string.IsNullOrEmpty(data.ParameterName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StoredProcedureLogDetailDataModel.DataColumns.ParameterName, data.ParameterName);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogDetailDataModel.DataColumns.ParameterName);
					}
					break;

				case StoredProcedureLogDetailDataModel.DataColumns.ParameterValue:
					if (!string.IsNullOrEmpty(data.ParameterValue))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StoredProcedureLogDetailDataModel.DataColumns.ParameterValue, data.ParameterValue);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StoredProcedureLogDetailDataModel.DataColumns.ParameterValue);
					}
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<StoredProcedureLogDetailDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(StoredProcedureLogDetailDataModel.Empty, requestProfile);
		}

		#endregion

		#region GetDetails

        public static StoredProcedureLogDetailDataModel GetDetails(StoredProcedureLogDetailDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		public static List<StoredProcedureLogDetailDataModel> GetEntityDetails(StoredProcedureLogDetailDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.StoredProcedureLogDetailSearch ";

			var parameters =
			new
			{
				    AuditId                     = requestProfile.AuditId
				,   ApplicationMode             = requestProfile.ApplicationModeId
			    ,   StoredProcedureLogDetailId  = dataQuery.StoredProcedureLogDetailId
				,   StoredProcedureLogId        = dataQuery.StoredProcedureLogId
			};

			List<StoredProcedureLogDetailDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<StoredProcedureLogDetailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(StoredProcedureLogDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("StoredProcedureLogDetail.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(StoredProcedureLogDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("StoredProcedureLogDetail.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(StoredProcedureLogDetailDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.StoredProcedureLogDetailDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				StoredProcedureLogDetailId = data.StoredProcedureLogDetailId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region GetStoredProcedureLogDetails

		public static DataTable GetStoredProcedureLogDetails(StoredProcedureLogDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.StoredProcedureLogDetailSearch " +
					" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					", " + ToSQLParameter(data, StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogDetailId);

			var oDT = new DBDataTable("Get StoredProcedureLogDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion GetStoredProcedureLogDetails

		#region Search

		public static DataTable Search(StoredProcedureLogDetailDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(StoredProcedureLogDetailDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.StoredProcedureLogDetailInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					//", " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.StoredProcedureLogDetailUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogDetailId) +
						", " + ToSQLParameter(data, StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogId) +
						", " + ToSQLParameter(data, StoredProcedureLogDetailDataModel.DataColumns.ParameterName) +
						", " + ToSQLParameter(data, StoredProcedureLogDetailDataModel.DataColumns.ParameterValue);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(StoredProcedureLogDetailDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new StoredProcedureLogDetailDataModel();
			doesExistRequest.StoredProcedureLogId = data.StoredProcedureLogId;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
		}

		#endregion

	}
}
