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
	public partial class ReleaseNoteBusinessValueDataManager : StandardDataManager
	{
        static readonly string DataStoreKey = "";

		static ReleaseNoteBusinessValueDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseNoteBusinessValue");
		}

		public static string ToSQLParameter(ReleaseNoteBusinessValueDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReleaseNoteBusinessValueDataModel.DataColumns.ReleaseNoteBusinessValueId:
					if (data.ReleaseNoteBusinessValueId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseNoteBusinessValueDataModel.DataColumns.ReleaseNoteBusinessValueId, data.ReleaseNoteBusinessValueId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteBusinessValueDataModel.DataColumns.ReleaseNoteBusinessValueId);
					}
					break;

				case ReleaseNoteBusinessValueDataModel.DataColumns.DateCreated:
					if (data.DateCreated != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteBusinessValueDataModel.DataColumns.DateCreated, data.DateCreated);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteBusinessValueDataModel.DataColumns.DateCreated);
					}
					break;
				case ReleaseNoteBusinessValueDataModel.DataColumns.DateModified:
					if (data.DateModified != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteBusinessValueDataModel.DataColumns.DateModified, data.DateModified);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteBusinessValueDataModel.DataColumns.DateModified);

					}
					break;


				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#region GetList

        public static List<ReleaseNoteBusinessValueDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ReleaseNoteBusinessValueDataModel.Empty, requestProfile, 0);
		}

		#endregion GetList

		#region Search

		public static DataTable Search(ReleaseNoteBusinessValueDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetEntitySearch

		static public List<ReleaseNoteBusinessValueDataModel> GetEntitySearch(ReleaseNoteBusinessValueDataModel obj, RequestProfile requestProfile)
		{
            return GetEntityDetails(obj, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static ReleaseNoteBusinessValueDataModel GetDetails(ReleaseNoteBusinessValueDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<ReleaseNoteBusinessValueDataModel> GetEntityDetails(ReleaseNoteBusinessValueDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseNoteBusinessValueSearch ";

			var parameters =
			new
			{
				    AuditId                             = requestProfile.AuditId
				,   ReleaseNoteBusinessValueId          = dataQuery.ReleaseNoteBusinessValueId
				,   Name                                = dataQuery.Name
				,   Description                         = dataQuery.Description
				,   ApplicationId                       = requestProfile.ApplicationId
				,   ReturnAuditInfo                     = returnAuditInfo
			};

			List<ReleaseNoteBusinessValueDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseNoteBusinessValueDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseNoteBusinessValueDataModel> GetEntityDetails(ReleaseNoteBusinessValueDataModel data, RequestProfile requestProfile)
		//{
		//	var dt = GetDetails(data, requestProfile.AuditId);

		//	var list = ToList(dt);

		//	return list;
		//}

		#endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(ReleaseNoteBusinessValueDataModel data, RequestProfile requestProfile, string action)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReleaseNoteBusinessValueInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReleaseNoteBusinessValueUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						 ", " + ToSQLParameter(data, ReleaseNoteBusinessValueDataModel.DataColumns.ReleaseNoteBusinessValueId) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static int Create(ReleaseNoteBusinessValueDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			var releaseNoteBusinessValueId = DBDML.RunScalarSQL("ReleaseNoteBusinessValue.Insert", sql, DataStoreKey);
			return Convert.ToInt32(releaseNoteBusinessValueId);
		}

		#endregion Create

		#region Update

		public static void Update(ReleaseNoteBusinessValueDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("ReleaseNoteBusinessValue.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Delete

		public static void Delete(ReleaseNoteBusinessValueDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseNoteBusinessValueDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				ReleaseNoteBusinessValueId = data.ReleaseNoteBusinessValueId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(ReleaseNoteBusinessValueDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReleaseNoteBusinessValueDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion DoesExist

	}
}
