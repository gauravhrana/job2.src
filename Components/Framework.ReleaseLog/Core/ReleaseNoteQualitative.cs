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
    
    public partial class ReleaseNoteQualitativeDataManager : StandardDataManager
    {
        static string DataStoreKey = "";

        
        static ReleaseNoteQualitativeDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseNoteQualitative");
        }

        public static string ToSQLParameter(ReleaseNoteQualitativeDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ReleaseNoteQualitativeDataModel.DataColumns.ReleaseNoteQualitativeId:
                    if (data.ReleaseNoteQualitativeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseNoteQualitativeDataModel.DataColumns.ReleaseNoteQualitativeId, data.ReleaseNoteQualitativeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteQualitativeDataModel.DataColumns.ReleaseNoteQualitativeId);
                    }
                    break;

                case ReleaseNoteQualitativeDataModel.DataColumns.DateCreated:
                    if (data.DateCreated != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteQualitativeDataModel.DataColumns.DateCreated, data.DateCreated);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteQualitativeDataModel.DataColumns.DateCreated);

                    }
                    break;
                case ReleaseNoteQualitativeDataModel.DataColumns.DateModified:
                    if (data.DateModified != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteQualitativeDataModel.DataColumns.DateModified, data.DateModified);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteQualitativeDataModel.DataColumns.DateModified);

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
			var sql = "EXEC dbo.ReleaseNoteQualitativeSearch" +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
            var oDT = new DBDataTable("Get List", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion GetList

        #region ToList

        static private List<ReleaseNoteQualitativeDataModel> ToList(DataTable dt)
        {
            var list = new List<ReleaseNoteQualitativeDataModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var dataItem = new ReleaseNoteQualitativeDataModel();

                    dataItem.ReleaseNoteQualitativeId = (int?)dr[ReleaseNoteQualitativeDataModel.DataColumns.ReleaseNoteQualitativeId];
                    dataItem.DateCreated              = (DateTime)dr[ReleaseNoteQualitativeDataModel.DataColumns.DateCreated];
                    dataItem.DateModified             = (DateTime)dr[ReleaseNoteQualitativeDataModel.DataColumns.DateModified];
                    dataItem.CreatedByAuditId         = (int?)dr[BaseDataModel.BaseDataColumns.CreatedByAuditId];
                    dataItem.ModifiedByAuditId        = (int?)dr[BaseDataModel.BaseDataColumns.ModifiedByAuditId];

                    SetStandardInfo(dataItem, dr);

                    list.Add(dataItem);
                }
            }
            return list;
        }

        #endregion

        #region Search

        public static DataTable Search(ReleaseNoteQualitativeDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
        }

        #endregion Search

        #region GetEntitySearch

        static public List<ReleaseNoteQualitativeDataModel> GetEntitySearch(ReleaseNoteQualitativeDataModel obj, RequestProfile requestProfile)
        {
            var dt = Search(obj, requestProfile);

            var list = ToList(dt);

            return list;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(ReleaseNoteQualitativeDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
        }

		public static List<ReleaseNoteQualitativeDataModel> GetEntityDetails(ReleaseNoteQualitativeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseNoteQualitativeSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ReleaseNoteQualitativeId	= dataQuery.ReleaseNoteQualitativeId
				,	ApplicationId				= requestProfile.ApplicationId
				,	Name						= dataQuery.Name
				,	Description					= dataQuery.Description
				,	DateCreated					= dataQuery.DateCreated
				,	DateModified				= dataQuery.DateModified
				,	CreatedByAuditId			= dataQuery.CreatedByAuditId
				,	ModifiedByAuditId			= dataQuery.ModifiedByAuditId
				,	ReturnAuditInfo				= returnAuditInfo
			};

			List<ReleaseNoteQualitativeDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseNoteQualitativeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseNoteQualitativeDataModel> GetEntityDetails(ReleaseNoteQualitativeDataModel data, RequestProfile requestProfile)
		//{
		//	var dt = GetDetails(data, requestProfile.AuditId);

		//	var list = ToList(dt);

		//	return list;
		//}

        #endregion GetDetails

        #region CreateOrUpdate

        private static string CreateOrUpdate(ReleaseNoteQualitativeDataModel data, RequestProfile requestProfile, string action)
        {

            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ReleaseNoteQualitativeInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ReleaseNoteQualitativeUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                         ", " + ToSQLParameter(data, ReleaseNoteQualitativeDataModel.DataColumns.ReleaseNoteQualitativeId) +
                         ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                         ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

            return sql;
        }

        #endregion CreateOrUpdate

        #region Create

        public static int Create(ReleaseNoteQualitativeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var ReleaseNoteQualitativeId = DBDML.RunScalarSQL("ReleaseNoteQualitative.Insert", sql, DataStoreKey);
            return Convert.ToInt32(ReleaseNoteQualitativeId);
        }

        #endregion Create

        #region Update

        public static void Update(ReleaseNoteQualitativeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");

            DBDML.RunSQL("ReleaseNoteQualitative.Update", sql, DataStoreKey);
        }

        #endregion Update

        #region Delete

        public static void Delete(ReleaseNoteQualitativeDataModel data, RequestProfile requestProfile)
        {
			const string sql = @"dbo.ReleaseNoteQualitativeDelete ";

			var parameters =	new
			{
					AuditId								= requestProfile.AuditId
				,	ReleaseNoteQualitativeId			= data.ReleaseNoteQualitativeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion Delete

        #region DoesExist

        public static DataTable DoesExist(ReleaseNoteQualitativeDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new ReleaseNoteQualitativeDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
        }

        #endregion DoesExist

    }

}
