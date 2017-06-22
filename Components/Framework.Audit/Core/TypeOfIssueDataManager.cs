using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.Audit;
using DataModel.Framework.DataAccess;

namespace Framework.Components.Audit
{
    public partial class TypeOfIssueDataManager : StandardDataManager
    {
        static readonly string DataStoreKey = "";

        static TypeOfIssueDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TypeOfIssue");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(TypeOfIssueDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case TypeOfIssueDataModel.DataColumns.TypeOfIssueId:
                    if (data.TypeOfIssueId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TypeOfIssueDataModel.DataColumns.TypeOfIssueId, data.TypeOfIssueId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TypeOfIssueDataModel.DataColumns.TypeOfIssueId);
                    }
                    break;

                case TypeOfIssueDataModel.DataColumns.Category:
                    if (!string.IsNullOrEmpty(data.Category))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TypeOfIssueDataModel.DataColumns.Category, data.Category);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TypeOfIssueDataModel.DataColumns.Category);
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

        public static List<TypeOfIssueDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TypeOfIssueDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static TypeOfIssueDataModel GetDetails(TypeOfIssueDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        public static List<TypeOfIssueDataModel> GetEntityDetails(TypeOfIssueDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TypeOfIssueSearch ";

            var parameters =
            new
            {
                    AuditId         = requestProfile.AuditId
                ,   ApplicationId   = requestProfile.ApplicationId
                ,   TypeOfIssueId   = dataQuery.TypeOfIssueId
                ,   Name            = dataQuery.Name
                ,   Category        = dataQuery.Category
                ,   ReturnAuditInfo = returnAuditInfo
            };

            List<TypeOfIssueDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<TypeOfIssueDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        //public static List<TypeOfIssueDataModel> GetEntityDetails(TypeOfIssueDataModel data, RequestProfile requestProfile)
        //{
        //	var sql = "EXEC dbo.TypeOfIssueSearch " +
        //			" "  + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //			", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
        //			", " + ToSQLParameter(data, TypeOfIssueDataModel.DataColumns.TypeOfIssueId) +
        //			", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
        //			", " + ToSQLParameter(data, TypeOfIssueDataModel.DataColumns.Category);

        //	var result = new List<TypeOfIssueDataModel>();

        //	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //	{
        //		var dbReader = reader.DBReader;

        //		while (dbReader.Read())
        //		{
        //			var dataItem = new TypeOfIssueDataModel();

        //			dataItem.TypeOfIssueId = (int)dbReader[TypeOfIssueDataModel.DataColumns.TypeOfIssueId];
        //			dataItem.Category = dbReader[TypeOfIssueDataModel.DataColumns.Category].ToString();

        //			SetStandardInfo(dataItem, dbReader);

        //			SetBaseInfo(dataItem, dbReader);

        //			result.Add(dataItem);
        //		}
        //	}

        //	return result;
        //}

        #endregion

        #region Create

        public static void Create(TypeOfIssueDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("TypeOfIssue.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(TypeOfIssueDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("TypeOfIssue.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(TypeOfIssueDataModel data, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TypeOfIssueDelete ";

            var parameters = new
            {
                AuditId = requestProfile.AuditId
                ,
                TypeOfIssueId = data.TypeOfIssueId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static DataTable Search(TypeOfIssueDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region Save

        private static string Save(TypeOfIssueDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TypeOfIssueInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TypeOfIssueUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, TypeOfIssueDataModel.DataColumns.TypeOfIssueId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                        ", " + ToSQLParameter(data, TypeOfIssueDataModel.DataColumns.Category) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(TypeOfIssueDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new TypeOfIssueDataModel();
            doesExistRequest.Name = data.Name;

            return Search(doesExistRequest, requestProfile);
        }

        #endregion

    }

}
