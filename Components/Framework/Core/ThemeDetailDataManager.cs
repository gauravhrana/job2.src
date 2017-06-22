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
	public partial class ThemeDetailDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ThemeDetailDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ThemeDetail");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(ThemeDetailDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case ThemeDetailDataModel.DataColumns.ThemeDetailId:
					if (data.ThemeDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeDetailDataModel.DataColumns.ThemeDetailId, data.ThemeDetailId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailDataModel.DataColumns.ThemeDetailId);

					}
					break;

				case ThemeDetailDataModel.DataColumns.ThemeKeyId:
					if (data.ThemeKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeDetailDataModel.DataColumns.ThemeKeyId, data.ThemeKeyId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailDataModel.DataColumns.ThemeKeyId);

					}
					break;

				case ThemeDetailDataModel.DataColumns.Value:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ThemeDetailDataModel.DataColumns.Value, data.Value.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailDataModel.DataColumns.Value);

					}
					break;

				case ThemeDetailDataModel.DataColumns.ThemeId:
					if (data.ThemeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeDetailDataModel.DataColumns.ThemeId, data.ThemeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailDataModel.DataColumns.ThemeId);

					}
					break;

				case ThemeDetailDataModel.DataColumns.ThemeCategoryId:
					if (data.ThemeCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeDetailDataModel.DataColumns.ThemeCategoryId, data.ThemeCategoryId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailDataModel.DataColumns.ThemeCategoryId);

					}
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<ThemeDetailDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ThemeDetailDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static ThemeDetailDataModel GetDetails(ThemeDetailDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<ThemeDetailDataModel> GetEntityDetails(ThemeDetailDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ThemeDetailSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ApplicationId = requestProfile.ApplicationId
				,
				ThemeDetailId = dataQuery.ThemeDetailId
				,
				Value = dataQuery.Value
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				ThemeId = dataQuery.ThemeId
				,
				ThemeKeyId = dataQuery.ThemeKeyId
				,
				ThemeCategoryId = dataQuery.ThemeCategoryId
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<ThemeDetailDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<ThemeDetailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}


		#endregion

		#region Create

		public static void Create(ThemeDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("ThemeDetail.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ThemeDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("ThemeDetail.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ThemeDetailDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ThemeDetailDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ThemeDetailId = dataQuery.ThemeDetailId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(ThemeDetailDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(ThemeDetailDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ThemeDetailInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ThemeDetailUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ThemeDetailDataModel.DataColumns.ThemeDetailId) +
						", " + ToSQLParameter(data, ThemeDetailDataModel.DataColumns.ThemeKeyId) +
						", " + ToSQLParameter(data, ThemeDetailDataModel.DataColumns.Value) +
						", " + ToSQLParameter(data, ThemeDetailDataModel.DataColumns.ThemeId) +
						", " + ToSQLParameter(data, ThemeDetailDataModel.DataColumns.ThemeCategoryId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ThemeDetailDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ThemeDetailDataModel();
			doesExistRequest.ThemeKeyId			= data.ThemeKeyId;
			doesExistRequest.ThemeId			= data.ThemeId;
			doesExistRequest.ThemeCategoryId	= data.ThemeCategoryId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(ThemeDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ThemeDetailChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ThemeDetailDataModel.DataColumns.ThemeDetailId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ThemeDetailDataModel data, RequestProfile requestProfile)
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
