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
	public partial class ReleaseNoteTechnicalDifficultyDataManager : StandardDataManager
	{
        static readonly string DataStoreKey = "";

		static ReleaseNoteTechnicalDifficultyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseNoteTechnicalDifficulty");
		}

		public static string ToSQLParameter(ReleaseNoteTechnicalDifficultyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReleaseNoteTechnicalDifficultyDataModel.DataColumns.ReleaseNoteTechnicalDifficultyId:
					if (data.ReleaseNoteTechnicalDifficultyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseNoteTechnicalDifficultyDataModel.DataColumns.ReleaseNoteTechnicalDifficultyId, data.ReleaseNoteTechnicalDifficultyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteTechnicalDifficultyDataModel.DataColumns.ReleaseNoteTechnicalDifficultyId);
					}
					break;

				case ReleaseNoteTechnicalDifficultyDataModel.DataColumns.DateCreated:
					if (data.DateCreated != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteTechnicalDifficultyDataModel.DataColumns.DateCreated, data.DateCreated);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteTechnicalDifficultyDataModel.DataColumns.DateCreated);
					}
					break;
				case ReleaseNoteTechnicalDifficultyDataModel.DataColumns.DateModified:
					if (data.DateModified != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteTechnicalDifficultyDataModel.DataColumns.DateModified, data.DateModified);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteTechnicalDifficultyDataModel.DataColumns.DateModified);

					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#region GetList

        public static List<ReleaseNoteTechnicalDifficultyDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ReleaseNoteTechnicalDifficultyDataModel.Empty, requestProfile, 0);
		}

		#endregion GetList

		#region Search

		public static DataTable Search(ReleaseNoteTechnicalDifficultyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetEntitySearch

		static public List<ReleaseNoteTechnicalDifficultyDataModel> GetEntitySearch(ReleaseNoteTechnicalDifficultyDataModel obj, RequestProfile requestProfile)
		{
            return GetEntityDetails(obj, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static ReleaseNoteTechnicalDifficultyDataModel GetDetails(ReleaseNoteTechnicalDifficultyDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<ReleaseNoteTechnicalDifficultyDataModel> GetEntityDetails(ReleaseNoteTechnicalDifficultyDataModel dataQuery, RequestProfile requestProfile, int applicationModeId = 0)
		{
			const string sql = @"dbo.ReleaseNoteTechnicalDifficultySearch ";

			var parameters =
			new
			{
				    AuditId                             = requestProfile.AuditId
				,   ReleaseNoteTechnicalDifficultyId    = dataQuery.ReleaseNoteTechnicalDifficultyId
				,   Name                                = dataQuery.Name
				,   Description                         = dataQuery.Description
				,   ApplicationId                       = requestProfile.ApplicationId
			};

			List<ReleaseNoteTechnicalDifficultyDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseNoteTechnicalDifficultyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseNoteTechnicalDifficultyDataModel> GetEntityDetails(ReleaseNoteTechnicalDifficultyDataModel data, RequestProfile requestProfile)
		//{
		//	var dt = GetDetails(data, requestProfile.AuditId);

		//	var list = ToList(dt);

		//	return list;
		//}

		#endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(ReleaseNoteTechnicalDifficultyDataModel data, RequestProfile requestProfile, string action)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReleaseNoteTechnicalDifficultyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReleaseNoteTechnicalDifficultyUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						 ", " + ToSQLParameter(data, ReleaseNoteTechnicalDifficultyDataModel.DataColumns.ReleaseNoteTechnicalDifficultyId) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static int Create(ReleaseNoteTechnicalDifficultyDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			var ReleaseNoteTechnicalDifficultyId = DBDML.RunScalarSQL("ReleaseNoteTechnicalDifficulty.Insert", sql, DataStoreKey);
			return Convert.ToInt32(ReleaseNoteTechnicalDifficultyId);
		}

		#endregion Create

		#region Update

		public static void Update(ReleaseNoteTechnicalDifficultyDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("ReleaseNoteTechnicalDifficulty.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Delete

		public static void Delete(ReleaseNoteTechnicalDifficultyDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseNoteTechnicalDifficultyDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				ReleaseNoteTechnicalDifficultyId = data.ReleaseNoteTechnicalDifficultyId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(ReleaseNoteTechnicalDifficultyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReleaseNoteTechnicalDifficultyDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion DoesExist

	}
}
