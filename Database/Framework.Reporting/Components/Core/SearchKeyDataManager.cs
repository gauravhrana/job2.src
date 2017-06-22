using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class SearchKeyDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static SearchKeyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SearchKey");
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SearchKeySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("SearchKey.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(SearchKeyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch


		public static List<SearchKeyDataModel> GetEntityDetails(SearchKeyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SearchKeySearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SearchKeyId = dataQuery.SearchKeyId
				,
				Name = dataQuery.Name
				,
				View = dataQuery.View
				,
				ApplicationId = requestProfile.ApplicationId
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<SearchKeyDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<SearchKeyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion

		#region Create

		public static int Create(SearchKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var SearchKeyId = DBDML.RunScalarSQL("SearchKey.Insert", sql, DataStoreKey);
			return Convert.ToInt32(SearchKeyId);
		}

		#endregion

		#region Update

		public static void Update(SearchKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("SearchKey.Update", sql, DataStoreKey);
		}

		#endregion

		#region DeleteExpired

		public static void DeleteExpired(SearchKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SearchKeyDeleteExpired " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			DBDML.RunSQL("SearchKey.SearchKeyDeleteExpired", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SearchKeyDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SearchKeyDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SearchKeyId = dataQuery.SearchKeyId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(SearchKeyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SearchKeyDataModel.DataColumns.SearchKeyId:
					if (data.SearchKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SearchKeyDataModel.DataColumns.SearchKeyId, data.SearchKeyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDataModel.DataColumns.SearchKeyId);
					}
					break;

				case SearchKeyDataModel.DataColumns.View:
					if (data.View != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SearchKeyDataModel.DataColumns.View, data.View);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDataModel.DataColumns.View);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(SearchKeyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		public static DataSet SearchByKey(SearchKeyDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.SearchKeySearchByKey " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, SearchKeyDataModel.DataColumns.SearchKeyId);

			var oDT = new DBDataSet("SearchKey.Search", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(SearchKeyDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SearchKeyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SearchKeyUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, SearchKeyDataModel.DataColumns.SearchKeyId) +
						", " + ToSQLParameter(data, SearchKeyDataModel.DataColumns.Name) +
						", " + ToSQLParameter(data, SearchKeyDataModel.DataColumns.Description) +
						", " + ToSQLParameter(data, SearchKeyDataModel.DataColumns.SortOrder) +
						", " + ToSQLParameter(data, SearchKeyDataModel.DataColumns.View);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(SearchKeyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SearchKeyDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(SearchKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SearchKeyChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, SearchKeyDataModel.DataColumns.SearchKeyId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(SearchKeyDataModel data, RequestProfile requestProfile)
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
