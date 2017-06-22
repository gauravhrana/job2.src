using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using System.Data.SqlClient;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class SuperKeyDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static SuperKeyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SuperKey");
		}

		#region GetList

        public static List<SuperKeyDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(SuperKeyDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static SuperKeyDataModel GetDetails(SuperKeyDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<SuperKeyDataModel> GetEntityDetails(SuperKeyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SuperKeySearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SuperKeyId = dataQuery.SuperKeyId
				,
				ApplicationId = requestProfile.ApplicationId
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				SystemEntityTypeId = dataQuery.SystemEntityTypeId
				,
				ExpirationDate = dataQuery.ExpirationDate
				,
				Name = dataQuery.Name
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<SuperKeyDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<SuperKeyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}


		#endregion

		#region Create

		public static int Create(SuperKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var superKeyId = DBDML.RunScalarSQL("SuperKey.Insert", sql, DataStoreKey);
			return Convert.ToInt32(superKeyId);
		}

		#endregion

		#region Update

		public static void Update(SuperKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("SuperKey.Update", sql, DataStoreKey);
		}

		#endregion

		#region DeleteExpired

		public static void DeleteExpired(SuperKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SuperKeyDeleteExpired " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, SuperKeyDataModel.DataColumns.ExpirationDate);

			DBDML.RunSQL("SuperKey.SuperKeyDeleteExpired", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SuperKeyDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SuperKeyDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SuperKeyId = dataQuery.SuperKeyId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(SuperKeyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SuperKeyDataModel.DataColumns.SuperKeyId:
					if (data.SuperKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SuperKeyDataModel.DataColumns.SuperKeyId, data.SuperKeyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SuperKeyDataModel.DataColumns.SuperKeyId);
					}
					break;

				case SuperKeyDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SuperKeyDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SuperKeyDataModel.DataColumns.SystemEntityTypeId);
					}
					break;

				case SuperKeyDataModel.DataColumns.ExpirationDate:
					if (data.ExpirationDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SuperKeyDataModel.DataColumns.ExpirationDate, data.ExpirationDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SuperKeyDataModel.DataColumns.ExpirationDate);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(SuperKeyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(SuperKeyDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SuperKeyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SuperKeyUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, SuperKeyDataModel.DataColumns.SuperKeyId) +
						", " + ToSQLParameter(data, SuperKeyDataModel.DataColumns.Name) +
						", " + ToSQLParameter(data, SuperKeyDataModel.DataColumns.Description) +
						", " + ToSQLParameter(data, SuperKeyDataModel.DataColumns.SortOrder) +
						", " + ToSQLParameter(data, SuperKeyDataModel.DataColumns.SystemEntityTypeId) +
						", " + ToSQLParameter(data, SuperKeyDataModel.DataColumns.ExpirationDate);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(SuperKeyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SuperKeyDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(SuperKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SuperKeyChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, SuperKeyDataModel.DataColumns.SuperKeyId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(SuperKeyDataModel data, RequestProfile requestProfile)
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
