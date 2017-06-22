using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using System.Data.SqlClient;
using DataModel.Framework.DataAccess;
using Dapper;

namespace Framework.Components.Core
{
	public partial class QuickPaginationRunDatatManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static QuickPaginationRunDatatManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("QuickPaginationRun");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(QuickPaginationRunDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId:
					if (data.QuickPaginationRunId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId, data.QuickPaginationRunId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId);

					}
					break;

				case QuickPaginationRunDataModel.DataColumns.SortClause:
					if (!string.IsNullOrEmpty(data.SortClause))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, QuickPaginationRunDataModel.DataColumns.SortClause, data.SortClause.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuickPaginationRunDataModel.DataColumns.SortClause);

					}
					break;

				case QuickPaginationRunDataModel.DataColumns.WhereClause:
					if (!string.IsNullOrEmpty(data.WhereClause))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, QuickPaginationRunDataModel.DataColumns.WhereClause, data.WhereClause.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuickPaginationRunDataModel.DataColumns.WhereClause);

					}
					break;

				case QuickPaginationRunDataModel.DataColumns.SystemEntityType:
					if (!string.IsNullOrEmpty(data.SystemEntityType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, QuickPaginationRunDataModel.DataColumns.SystemEntityType, data.SystemEntityType.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuickPaginationRunDataModel.DataColumns.SystemEntityType);

					}
					break;

				case QuickPaginationRunDataModel.DataColumns.ApplicationUserName:
					if (!string.IsNullOrEmpty(data.ApplicationUserName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, QuickPaginationRunDataModel.DataColumns.ApplicationUserName, data.ApplicationUserName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuickPaginationRunDataModel.DataColumns.ApplicationUserName);

					}
					break;

				case QuickPaginationRunDataModel.DataColumns.ApplicationUserId:
					if (data.ApplicationUserId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, QuickPaginationRunDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuickPaginationRunDataModel.DataColumns.ApplicationUserId);

					}
					break;

				case QuickPaginationRunDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, QuickPaginationRunDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuickPaginationRunDataModel.DataColumns.SystemEntityTypeId);

					}
					break;

				case QuickPaginationRunDataModel.DataColumns.ExpirationTime:
					if (data.ExpirationTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, QuickPaginationRunDataModel.DataColumns.ExpirationTime, data.ExpirationTime);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuickPaginationRunDataModel.DataColumns.ExpirationTime);

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
			var sql = "EXEC dbo.QuickPaginationRunSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("QuickPaginationRun.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(QuickPaginationRunDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.QuickPaginationRunSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, ReturnAuditInfoOnDetails) +
				", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId);

			var oDT = new DBDataTable("QuickPaginationRun.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Create

		public static int Create(QuickPaginationRunDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var QuickPaginationRunId = DBDML.RunScalarSQL("QuickPaginationRun.Insert", sql, DataStoreKey);
			return Convert.ToInt32(QuickPaginationRunId);
		}

		#endregion

		#region Update

		public static void Update(QuickPaginationRunDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("QuickPaginationRun.Update", sql, DataStoreKey);
		}

		#endregion

		#region DeleteExpired

		public static void DeleteExpired(QuickPaginationRunDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.QuickPaginationRunDeleteExpired " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.ExpirationTime);

			DBDML.RunSQL("QuickPaginationRun.QuickPaginationRunDeleteExpired", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(QuickPaginationRunDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.QuickPaginationRunDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				QuickPaginationRunId = dataQuery.QuickPaginationRunId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(QuickPaginationRunDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.QuickPaginationRunSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId) +
				", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.SystemEntityTypeId) +
				", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.ApplicationUserId) +
				", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.SortClause) +
				", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.WhereClause);

			var oDT = new DBDataTable("QuickPaginationRun.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(QuickPaginationRunDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.QuickPaginationRunInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.QuickPaginationRunUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId) +
						", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.ApplicationUserId) +
						", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.SystemEntityTypeId) +
						", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.SortClause) +
						", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.WhereClause) +
						", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.ExpirationTime);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(QuickPaginationRunDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new QuickPaginationRunDataModel();
			doesExistRequest.SortClause = data.SortClause;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(QuickPaginationRunDataModel data, RequestProfile requestProfile)
		{
			//var sql = "EXEC dbo.QuickPaginationRunChildrenGet " +
			//                " " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
			//                ", " + ToSQLParameter(data, QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId);

			//var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			//return oDT.DBDataset;
			return null;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(QuickPaginationRunDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;
			var ds = GetChildren(data, requestProfile);
			if (ds != null && ds.Tables.Count > 0)
			{
				foreach (DataTable dt in ds.Tables)
				{
					if (dt.Rows.Count > 0)
					{
						isDeletable = false;
						break;
					}
				}
			}
			return isDeletable;
		}

		#endregion

	}
}
