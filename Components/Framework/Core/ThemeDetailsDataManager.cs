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
	public partial class ThemeDetailsDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ThemeDetailsDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ThemeDetails");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(ThemeDetailsDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case ThemeDetailsDataModel.DataColumns.ThemeDetailId:
					if (data.ThemeDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeDetailsDataModel.DataColumns.ThemeDetailId, data.ThemeDetailId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailsDataModel.DataColumns.ThemeDetailId);

					}
					break;

				case ThemeDetailsDataModel.DataColumns.ThemeKeyId:
					if (data.ThemeKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeDetailsDataModel.DataColumns.ThemeKeyId, data.ThemeKeyId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailsDataModel.DataColumns.ThemeKeyId);

					}
					break;

				case ThemeDetailsDataModel.DataColumns.Value:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ThemeDetailsDataModel.DataColumns.Value, data.Value.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailsDataModel.DataColumns.Value);

					}
					break;

				case ThemeDetailsDataModel.DataColumns.ThemeId:
					if (data.ThemeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeDetailsDataModel.DataColumns.ThemeId, data.ThemeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailsDataModel.DataColumns.ThemeId);

					}
					break;

				case ThemeDetailsDataModel.DataColumns.ThemeCategoryId:
					if (data.ThemeCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeDetailsDataModel.DataColumns.ThemeCategoryId, data.ThemeCategoryId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeDetailsDataModel.DataColumns.ThemeCategoryId);

					}
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<ThemeDetailsDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ThemeDetailsDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static ThemeDetailsDataModel GetDetails(ThemeDetailsDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<ThemeDetailsDataModel> GetEntityDetails(ThemeDetailsDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ThemeDetailsSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ApplicationId = requestProfile.ApplicationId
				,
				ThemeDetailId = dataQuery.ThemeDetailId
				,
				Name = dataQuery.Value
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

			List<ThemeDetailsDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<ThemeDetailsDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}


		#endregion

		#region Create

		public static void Create(ThemeDetailsDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("ThemeDetails.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ThemeDetailsDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("ThemeDetails.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ThemeDetailsDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ThemeDetailsDelete ";

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

		public static DataTable Search(ThemeDetailsDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(ThemeDetailsDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ThemeDetailsInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ThemeDetailsUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ThemeDetailsDataModel.DataColumns.ThemeDetailId) +
						", " + ToSQLParameter(data, ThemeDetailsDataModel.DataColumns.ThemeKeyId) +
						", " + ToSQLParameter(data, ThemeDetailsDataModel.DataColumns.Value) +
						", " + ToSQLParameter(data, ThemeDetailsDataModel.DataColumns.ThemeId) +
						", " + ToSQLParameter(data, ThemeDetailsDataModel.DataColumns.ThemeCategoryId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ThemeDetailsDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ThemeDetailsDataModel();
			doesExistRequest.Value = data.Value;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(ThemeDetailsDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ThemeDetailsChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ThemeDetailsDataModel.DataColumns.ThemeDetailId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ThemeDetailsDataModel data, RequestProfile requestProfile)
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
