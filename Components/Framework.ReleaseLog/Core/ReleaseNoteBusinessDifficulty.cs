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
    
    public partial class ReleaseNoteBusinessDifficultyDataManager : StandardDataManager
    {
        static string DataStoreKey = "";

		static ReleaseNoteBusinessDifficultyDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseNoteBusinessDifficulty");
        }

        public static string ToSQLParameter(ReleaseNoteBusinessDifficultyDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ReleaseNoteBusinessDifficultyDataModel.DataColumns.ReleaseNoteBusinessDifficultyId:
                    if (data.ReleaseNoteBusinessDifficultyId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseNoteBusinessDifficultyDataModel.DataColumns.ReleaseNoteBusinessDifficultyId, data.ReleaseNoteBusinessDifficultyId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteBusinessDifficultyDataModel.DataColumns.ReleaseNoteBusinessDifficultyId);
                    }
                    break;

                case ReleaseNoteBusinessDifficultyDataModel.DataColumns.DateCreated:
                    if (data.DateCreated != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteBusinessDifficultyDataModel.DataColumns.DateCreated, data.DateCreated);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteBusinessDifficultyDataModel.DataColumns.DateCreated);
                    }
                    break;
                case ReleaseNoteBusinessDifficultyDataModel.DataColumns.DateModified:
                    if (data.DateModified != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteBusinessDifficultyDataModel.DataColumns.DateModified, data.DateModified);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteBusinessDifficultyDataModel.DataColumns.DateModified);

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
            var sql = "EXEC dbo.ReleaseNoteBusinessDifficultySearch" +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
            var oDT = new DBDataTable("Get List", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion GetList

        #region ToList

        static private List<ReleaseNoteBusinessDifficultyDataModel> ToList(DataTable dt)
        {
            var list = new List<ReleaseNoteBusinessDifficultyDataModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var dataItem = new ReleaseNoteBusinessDifficultyDataModel();

                    dataItem.ReleaseNoteBusinessDifficultyId = (int?)dr[ReleaseNoteBusinessDifficultyDataModel.DataColumns.ReleaseNoteBusinessDifficultyId];
                    dataItem.DateCreated              = (DateTime)dr[ReleaseNoteBusinessDifficultyDataModel.DataColumns.DateCreated];
                    dataItem.DateModified             = (DateTime)dr[ReleaseNoteBusinessDifficultyDataModel.DataColumns.DateModified];
                    dataItem.CreatedByAuditId         = (int?)dr[BaseDataModel.BaseDataColumns.CreatedByAuditId];
					dataItem.ModifiedByAuditId		  = (int?)dr[BaseDataModel.BaseDataColumns.ModifiedByAuditId];

                    SetStandardInfo(dataItem, dr);

                    list.Add(dataItem);
                }
            }
            return list;
        }

        #endregion

        #region Search

        public static DataTable Search(ReleaseNoteBusinessDifficultyDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
        }

        #endregion Search

        #region GetEntitySearch

        static public List<ReleaseNoteBusinessDifficultyDataModel> GetEntitySearch(ReleaseNoteBusinessDifficultyDataModel obj, RequestProfile requestProfile)
        {
            var dt = Search(obj, requestProfile);

            var list = ToList(dt);

            return list;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(ReleaseNoteBusinessDifficultyDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
        }

		public static List<ReleaseNoteBusinessDifficultyDataModel> GetEntityDetails(ReleaseNoteBusinessDifficultyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseNoteBusinessDifficultySearch ";

			var parameters =
			new
			{
					AuditId							= requestProfile.AuditId				
				,	ReleaseNoteBusinessDifficultyId = dataQuery.ReleaseNoteBusinessDifficultyId
				,	Name							= dataQuery.Name
				,	Description						= dataQuery.Description
				,	ApplicationId					= requestProfile.ApplicationId
				,	ReturnAuditInfo					= returnAuditInfo
			};

			List<ReleaseNoteBusinessDifficultyDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseNoteBusinessDifficultyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseNoteBusinessDifficultyDataModel> GetEntityDetails(ReleaseNoteBusinessDifficultyDataModel data, RequestProfile requestProfile)
		//{
		//	var dt = GetDetails(data, requestProfile.AuditId);

		//	var list = ToList(dt);

		//	return list;
		//}

        #endregion GetDetails

        #region CreateOrUpdate

        private static string CreateOrUpdate(ReleaseNoteBusinessDifficultyDataModel data, RequestProfile requestProfile, string action)
        {

            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ReleaseNoteBusinessDifficultyInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ReleaseNoteBusinessDifficultyUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                         ", " + ToSQLParameter(data, ReleaseNoteBusinessDifficultyDataModel.DataColumns.ReleaseNoteBusinessDifficultyId) +
                         ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                         ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

            return sql;
        }

        #endregion CreateOrUpdate

        #region Create

        public static int Create(ReleaseNoteBusinessDifficultyDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var ReleaseNoteBusinessDifficultyId = DBDML.RunScalarSQL("ReleaseNoteBusinessDifficulty.Insert", sql, DataStoreKey);
            return Convert.ToInt32(ReleaseNoteBusinessDifficultyId);
        }

        #endregion Create

        #region Update

        public static void Update(ReleaseNoteBusinessDifficultyDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");

            DBDML.RunSQL("ReleaseNoteBusinessDifficulty.Update", sql, DataStoreKey);
        }

        #endregion Update

        #region Delete

        public static void Delete(ReleaseNoteBusinessDifficultyDataModel data, RequestProfile requestProfile)
        {
			const string sql = @"dbo.ReleaseNoteBusinessDifficultyDelete ";

			var parameters =	new
			{
					AuditId								= requestProfile.AuditId
				,	ReleaseNoteBusinessDifficultyId		= data.ReleaseNoteBusinessDifficultyId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion Delete

        #region DoesExist

        public static DataTable DoesExist(ReleaseNoteBusinessDifficultyDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new ReleaseNoteBusinessDifficultyDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
        }

        #endregion DoesExist

    }

}
