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
	public partial class SearchKeyDetailItemDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static SearchKeyDetailItemDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SearchKeyDetailItem");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(SearchKeyDetailItemDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailItemId:
					if (data.SearchKeyDetailItemId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailItemId, data.SearchKeyDetailItemId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailItemId);
					}
					break;

				case SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailId:
					if (data.SearchKeyDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailId, data.SearchKeyDetailId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailId);
					}
					break;

                case SearchKeyDetailItemDataModel.DataColumns.Value:
                    returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SearchKeyDetailItemDataModel.DataColumns.Value, data.Value.Trim());
                    //if (!string.IsNullOrEmpty(data.Value))
                    //{
                        
                    //}
                    //else
                    //{
                    //    returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailItemDataModel.DataColumns.Value);
                    //}
                    break;

				case SearchKeyDetailItemDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SearchKeyDetailItemDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SearchKeyDetailItemDataModel.DataColumns.SortOrder);
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

        public static List<SearchKeyDetailItemDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(SearchKeyDetailItemDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static SearchKeyDetailItemDataModel GetDetails(SearchKeyDetailItemDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<SearchKeyDetailItemDataModel> GetEntityDetails(SearchKeyDetailItemDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SearchKeyDetailItemSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SearchKeyDetailItemId = dataQuery.SearchKeyDetailItemId
				,
				SearchKeyDetailId = dataQuery.SearchKeyDetailId
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				Name = dataQuery.Value
				,
				ApplicationId = dataQuery.ApplicationId
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<SearchKeyDetailItemDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<SearchKeyDetailItemDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(SearchKeyDetailItemDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("SearchKeyDetailItem.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(SearchKeyDetailItemDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("SearchKeyDetailItem.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SearchKeyDetailItemDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SearchKeyDetailItemDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SearchKeyDetailItemId = dataQuery.SearchKeyDetailItemId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(SearchKeyDetailItemDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(SearchKeyDetailItemDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SearchKeyDetailItemInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SearchKeyDetailItemUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailItemId) +
						", " + ToSQLParameter(data, SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailId) +
						", " + ToSQLParameter(data, SearchKeyDetailItemDataModel.DataColumns.Value) +
						", " + ToSQLParameter(data, SearchKeyDetailItemDataModel.DataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(SearchKeyDetailItemDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SearchKeyDetailItemDataModel();
			doesExistRequest.SearchKeyDetailItemId = data.SearchKeyDetailItemId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion
	}
}
