using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Framework.Components.DataAccess;
using Framework.Components.ReleaseLog.DomainModel;
using System.Data;
using DataModel.Framework.DataAccess;


namespace Framework.Components.ReleaseLog
{
	public partial class ReleaseFeatureDataManager : StandardDataManager
	{
		static string DataStoreKey = "";

		static ReleaseFeatureDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseFeature");
		}

		public static string ToSQLParameter(ReleaseFeatureDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReleaseFeatureDataModel.DataColumns.ReleaseFeatureId:
					if (data.ReleaseFeatureId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseFeatureDataModel.DataColumns.ReleaseFeatureId, data.ReleaseFeatureId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseFeatureDataModel.DataColumns.ReleaseFeatureId);
					}
					break;

				case ReleaseFeatureDataModel.DataColumns.DateCreated:
					if (data.DateCreated != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseFeatureDataModel.DataColumns.DateCreated, data.DateCreated);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseFeatureDataModel.DataColumns.DateCreated);
					}
					break;
				case ReleaseFeatureDataModel.DataColumns.DateModified:
					if (data.DateModified != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseFeatureDataModel.DataColumns.DateModified, data.DateModified);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseFeatureDataModel.DataColumns.DateModified);

					}
					break;
				
				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleaseFeatureSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		public static List<ReleaseFeatureDataModel> GetReleaseFeatureList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReleaseFeatureSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var result = new List<ReleaseFeatureDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new ReleaseFeatureDataModel();

					dataItem.ReleaseFeatureId = (int)dbReader[ReleaseFeatureDataModel.DataColumns.ReleaseFeatureId];
					dataItem.ApplicationId = (int)dbReader[BaseDataModel.BaseDataColumns.ApplicationId];

					SetStandardInfo(dataItem, dbReader);

					SetStandardInfo(dataItem, dbReader);

					result.Add(dataItem);
				}

			}

			return result;
		}

		#endregion GetList

		#region ToList

		static private List<ReleaseFeatureDataModel> ToList(DataTable dt)
		{
			var list = new List<ReleaseFeatureDataModel>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var dataItem = new ReleaseFeatureDataModel();

					dataItem.ReleaseFeatureId = (int?)dr[ReleaseFeatureDataModel.DataColumns.ReleaseFeatureId];
					dataItem.DateCreated = (DateTime)dr[ReleaseFeatureDataModel.DataColumns.DateCreated];
					dataItem.DateModified = (DateTime)dr[ReleaseFeatureDataModel.DataColumns.DateModified];
					dataItem.CreatedByAuditId = (int?)dr[BaseDataModel.BaseDataColumns.CreatedByAuditId];
					dataItem.ModifiedByAuditId = (int?)dr[BaseDataModel.BaseDataColumns.ModifiedByAuditId];
					
					SetStandardInfo(dataItem, dr);

					list.Add(dataItem);
				}
			}
			return list;
		}

		#endregion

		#region Search

		public static DataTable Search(ReleaseFeatureDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetEntitySearch

		static public List<ReleaseFeatureDataModel> GetEntitySearch(ReleaseFeatureDataModel obj, RequestProfile requestProfile)
		{
			var dt = Search(obj, requestProfile);

			var list = ToList(dt);

			return list;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(ReleaseFeatureDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		public static List<ReleaseFeatureDataModel> GetEntityDetails(ReleaseFeatureDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseFeatureSearch ";

			var parameters =
			new
			{
					AuditId					= requestProfile.AuditId
				,	ReleaseFeatureId		= dataQuery.ReleaseFeatureId
				,	Name					= dataQuery.Name
				,	Description				= dataQuery.Description
				,	ApplicationId			= requestProfile.ApplicationId
				,	ApplicationMode			= requestProfile.ApplicationModeId
				,	ReturnAuditInfo			= returnAuditInfo
			};

			List<ReleaseFeatureDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseFeatureDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseFeatureDataModel> GetEntityDetails(ReleaseFeatureDataModel data, RequestProfile requestProfile)
		//{
		//	var dt = GetDetails(data, requestProfile.AuditId);

		//	var list = ToList(dt);

		//	return list;
		//}

		#endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(ReleaseFeatureDataModel data, RequestProfile requestProfile, string action)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReleaseFeatureInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReleaseFeatureUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						 ", " + ToSQLParameter(data, ReleaseFeatureDataModel.DataColumns.ReleaseFeatureId) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static int Create(ReleaseFeatureDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			var ReleaseFeatureId = DBDML.RunScalarSQL("ReleaseFeature.Insert", sql, DataStoreKey);
			return Convert.ToInt32(ReleaseFeatureId);
		}

		#endregion Create

		#region Update

		public static void Update(ReleaseFeatureDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("ReleaseFeature.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Delete

		public static void Delete(ReleaseFeatureDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseFeatureDelete ";

			var parameters = new
			{
					AuditId = requestProfile.AuditId
				,	ReleaseFeatureId = data.ReleaseFeatureId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(ReleaseFeatureDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReleaseFeatureDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist

	}
}
