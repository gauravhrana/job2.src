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
	public partial class SearchKeyDetailDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static SearchKeyDetailDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SearchKeyDetail");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(SearchKeyDetailDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId:
					if (data.SearchKeyDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId, data.SearchKeyDetailId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId);

					}
					break;

				case SearchKeyDetailDataModel.DataColumns.SearchKeyId:
					if (data.SearchKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SearchKeyDetailDataModel.DataColumns.SearchKeyId, data.SearchKeyId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailDataModel.DataColumns.SearchKeyId);

					}
					break;

				case SearchKeyDetailDataModel.DataColumns.SearchParameter:
					if (!string.IsNullOrEmpty(data.SearchParameter))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SearchKeyDetailDataModel.DataColumns.SearchParameter, data.SearchParameter.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailDataModel.DataColumns.SearchParameter);

					}
					break;

				case SearchKeyDetailDataModel.DataColumns.SearchKey:
					if (!string.IsNullOrEmpty(data.SearchKey))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SearchKeyDetailDataModel.DataColumns.SearchKey, data.SearchKey.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailDataModel.DataColumns.SearchKey);

					}
					break;

				case SearchKeyDetailDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SearchKeyDetailDataModel.DataColumns.SortOrder, data.SortOrder);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailDataModel.DataColumns.SortOrder);

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

        public static List<SearchKeyDetailDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(SearchKeyDetailDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static SearchKeyDetailDataModel GetDetails(SearchKeyDetailDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<SearchKeyDetailDataModel> GetEntityDetails(SearchKeyDetailDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SearchKeyDetailSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SearchKeyDetailId = dataQuery.SearchKeyDetailId
				,
				SearchKeyId = dataQuery.SearchKeyId
				,
				SearchParamater = dataQuery.SearchParameter
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				ApplicationId = requestProfile.ApplicationId
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<SearchKeyDetailDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<SearchKeyDetailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}
		#endregion

		#region Create

		public static int Create(SearchKeyDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var SearchKeyDetailId = DBDML.RunScalarSQL("SearchKeyDetail.Insert", sql, DataStoreKey);
			return Convert.ToInt32(SearchKeyDetailId);
		}

		#endregion

		#region Update

		public static void Update(SearchKeyDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("SearchKeyDetail.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SearchKeyDetailDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SearchKeyDetailDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SearchKeyDetailId = dataQuery.SearchKeyDetailId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(SearchKeyDetailDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(SearchKeyDetailDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SearchKeyDetailInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SearchKeyDetailUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId) +
						", " + ToSQLParameter(data, SearchKeyDetailDataModel.DataColumns.SearchKeyId) +
						", " + ToSQLParameter(data, SearchKeyDetailDataModel.DataColumns.SearchParameter) +
						", " + ToSQLParameter(data, SearchKeyDetailDataModel.DataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(SearchKeyDetailDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SearchKeyDetailDataModel();
			doesExistRequest.SearchKeyDetailId = data.SearchKeyDetailId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

	}
}
