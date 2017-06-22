using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace Framework.Components.Core
{
	public partial class SuperKeyDetailDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static SuperKeyDetailDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SuperKeyDetail");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(SuperKeyDetailDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case SuperKeyDetailDataModel.DataColumns.SuperKeyDetailId:
					if (data.SuperKeyDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SuperKeyDetailDataModel.DataColumns.SuperKeyDetailId, data.SuperKeyDetailId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SuperKeyDetailDataModel.DataColumns.SuperKeyDetailId);

					}
					break;

				case SuperKeyDetailDataModel.DataColumns.SuperKeyId:
					if (data.SuperKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SuperKeyDetailDataModel.DataColumns.SuperKeyId, data.SuperKeyId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SuperKeyDetailDataModel.DataColumns.SuperKeyId);

					}
					break;

				case SuperKeyDetailDataModel.DataColumns.EntityKey:
					if (data.EntityKey != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SuperKeyDetailDataModel.DataColumns.EntityKey, data.EntityKey);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SuperKeyDetailDataModel.DataColumns.EntityKey);

					}
					break;

				case SuperKeyDetailDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SuperKeyDetailDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SuperKeyDetailDataModel.DataColumns.SystemEntityTypeId);

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
			var sql = "EXEC dbo.SuperKeyDetailSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("SuperKeyDetail.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(SuperKeyDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SuperKeyDetailSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, ReturnAuditInfoOnDetails) +
				", " + ToSQLParameter(data, SuperKeyDetailDataModel.DataColumns.SuperKeyDetailId);

			var oDT = new DBDataTable("SuperKeyDetail.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Create

		public static void Create(SuperKeyDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("SuperKeyDetail.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(SuperKeyDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("SuperKeyDetail.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SuperKeyDetailDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SuperKeyDetailDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SuperKeyDetailId = dataQuery.SuperKeyDetailId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(SuperKeyDetailDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.SuperKeyDetailSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, SuperKeyDetailDataModel.DataColumns.SuperKeyDetailId) +
				", " + ToSQLParameter(data, SuperKeyDetailDataModel.DataColumns.SuperKeyId) +
				", " + ToSQLParameter(data, SuperKeyDetailDataModel.DataColumns.SystemEntityTypeId);

			var oDT = new DBDataTable("SuperKeyDetail.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(SuperKeyDetailDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SuperKeyDetailInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SuperKeyDetailUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, SuperKeyDetailDataModel.DataColumns.SuperKeyDetailId) +
						", " + ToSQLParameter(data, SuperKeyDetailDataModel.DataColumns.SuperKeyId) +
						", " + ToSQLParameter(data, SuperKeyDetailDataModel.DataColumns.EntityKey);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(SuperKeyDetailDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SuperKeyDetailDataModel();
			doesExistRequest.EntityKey = data.EntityKey;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion

	}
}
