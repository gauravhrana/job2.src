using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Configuration
{
    public partial class DBNameDataManager : StandardDataManager
    {
        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        private static DataTable DBNameList;

        static DBNameDataManager()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("DBName");
        }

        #region ToSQLParameter

        public static string ToSQLParameter(DBNameDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case DBNameDataModel.DataColumns.DBNameId:
                    if (data.DBNameId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DBNameDataModel.DataColumns.DBNameId, data.DBNameId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DBNameDataModel.DataColumns.DBNameId);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        #endregion

        #region GetList

        public static DataTable GetList(int auditId, bool forceRefresh = false)
        {
            if (DBNameList == null || forceRefresh)
            {
                var sql = "EXEC dbo.DBNameSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId);

                var oDT = new DBDataTable("DBName.List", sql, DataStoreKey);

                DBNameList = oDT.DBTable;
            }

            // return clone copy in case databound ... but still should not be required
            return DBNameList.Clone();
        }

        #endregion

        #region GetEntitySearch

        static public List<DBNameDataModel> GetEntitySearch(DBNameDataModel obj, int auditId)
        {
			var dt = Search(obj, auditId, 0);//SessionVariables.ApplicationMode);

            var list = ToList(dt);

            return list;
        }

        #endregion

        #region ToList

        static private List<DBNameDataModel> ToList(DataTable dt)
        {
            var list = new List<DBNameDataModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var dataItem = new DBNameDataModel();

                    dataItem.DBNameId = (int?)dr[DBNameDataModel.DataColumns.DBNameId];

                    SetStandardInfo(dataItem, dr);

                    list.Add(dataItem);
                }
            }
            return list;
        }

        #endregion


        #region GetDetails

        public static DataTable GetDetails(DBNameDataModel data, int auditId)
        {
            var sql = "EXEC dbo.DBNameSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo ,BaseDataManager .ReturnAduitInfoOnDetails ) +
                ", " + ToSQLParameter(data, DBNameDataModel.DataColumns.DBNameId);

            var oDT = new DBDataTable("DBName.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<DBNameDataModel> GetEntityDetails(DBNameDataModel dataQuery, int auditId, int applicationModeId = 0)
        {
			const string sql = @"dbo.DBNameSearch ";

			var parameters =
			new
			{
					AuditId							= auditId
				,	DBNameId						= dataQuery.DBNameId
				,	ApplicationMode					= applicationModeId 
				,	ReturnAuditInfo					= BaseDataManager.ReturnAuditInfoOnDetails 
				,	Name							= dataQuery.Name
			};

			List<DBNameDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				var sp = Log4Net.LogSQLInfoStart(sql, DataStoreKey, parameters);

				result = dataAccess.Connection.Query<DBNameDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

				Log4Net.LogSQLInfoStop(sp, result.Count);
			}

            return result;
        }

        #endregion

        #region Create

        public static void Create(DBNameDataModel data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            DBDML.RunSQL("DBName.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(DBNameDataModel data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DBDML.RunSQL("DBName.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(DBNameDataModel data, int auditId)
        {
			const string sql = @"dbo.DBNameDelete ";

			var parameters =	new
								{
										AuditId				= auditId
									,	DBNameId			= data.DBNameId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				var sp = Log4Net.LogSQLInfoStart(sql, DataStoreKey, parameters);

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				Log4Net.LogSQLInfoStop(sp);
			}
        }

        #endregion

        #region Search

        public static DataTable Search(DBNameDataModel data, int auditId, int applicationModeId = 0)
        {
            // formulate SQL
            var sql = "EXEC dbo.DBNameSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, applicationModeId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DBNameDataModel.DataColumns.DBNameId) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

            var oDT = new DBDataTable("DBName.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(DBNameDataModel data, int auditId, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.DBNameInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.DBNameUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, DBNameDataModel.DataColumns.DBNameId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(DBNameDataModel data, int auditId)
        {
            var sql = "EXEC dbo.DBNameSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                ", " + ToSQLParameter(data, DBNameDataModel.DataColumns.DBNameId);

            var oDT = new DBDataTable("DBName.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion
    }
}
