using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class TaskAlgorithmDataManager : StandardDataManager
	{
        //private static string DataStoreKey = "";

        //static TaskAlgorithmDataManager()
        //{
        //    DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskAlgorithm");
        //}

        //#region GetList

        //public static DataTable GetList(RequestProfile requestProfile)
        //{
        //    var sql = "EXEC dbo.TaskAlgorithmSearch " +
        //        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
        //        + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
        //    var oDT = new DBDataTable("TaskAlgorithm.GetList", sql, DataStoreKey);

        //    return oDT.DBTable;
        //}

        //#endregion GetList

        //#region Search
        //public static string ToSQLParameter(TaskAlgorithmDataModel data, string dataColumnName)
        //{
        //    var returnValue = "NULL";

        //    switch (dataColumnName)
        //    {
        //        case TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId:
        //            if (data.TaskAlgorithmId != null)
        //            {
        //                returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId, data.TaskAlgorithmId);
        //            }
        //            else
        //            {
        //                returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId);
        //            }
        //            break;

        //        default:
        //            returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
        //            break;

        //    }

        //    return returnValue;
        //}

        //public static DataTable Search(TaskAlgorithmDataModel data, RequestProfile requestProfile)
        //{
        //    var list = GetEntityDetails(data, requestProfile);

        //    var table = list.ToDataTable();

        //    return table;
        //}

        //#endregion Search

        //#region GetDetails

        //public static DataTable GetDetails(TaskAlgorithmDataModel data, RequestProfile requestProfile)
        //{
        //    var list = GetEntityDetails(data, requestProfile);

        //    var table = list.ToDataTable();

        //    return table;
        //}

        //public static List<TaskAlgorithmDataModel> GetEntityDetails(TaskAlgorithmDataModel dataQuery, RequestProfile requestProfile)
        //{
        //    const string sql = @"dbo.TaskAlgorithmSearch ";

        //    var parameters =
        //    new
        //    {
        //            AuditId             = requestProfile.AuditId
        //        ,   ApplicationId       = requestProfile.ApplicationId
        //        ,   TaskAlgorithmId     = dataQuery.TaskAlgorithmId
        //        ,   Name                = dataQuery.Name
        //        ,   ApplicationMode     = requestProfile.ApplicationModeId
        //    };

        //    List<TaskAlgorithmDataModel> result;

        //    using (var dataAccess = new DataAccessBase(DataStoreKey))
        //    {
        //        result = dataAccess.Connection.Query<TaskAlgorithmDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
        //    }

        //    return result;
        //}

        //#endregion GetDetails

        //#region CreateOrUpdate
        //private static string CreateOrUpdate(TaskAlgorithmDataModel data, RequestProfile requestProfile, string action)
        //{
        //    var sql = "EXEC ";

        //    switch (action)
        //    {
        //        case "Create":
        //            sql += "dbo.TaskAlgorithmInsert  " + "\r\n" +
        //                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
        //            break;

        //        case "Update":
        //            sql += "dbo.TaskAlgorithmUpdate  " + "\r\n" +
        //                   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
        //            break;

        //        default:
        //            break;
        //    }

        //    sql = sql + ", " + ToSQLParameter(data, TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId) +
        //                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
        //                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
        //                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

        //    return sql;
        //}

        //#endregion CreateOrUpdate

        //#region Create

        //public static void Create(TaskAlgorithmDataModel data, RequestProfile requestProfile)
        //{
        //    var sql = CreateOrUpdate(data, requestProfile, "Create");

        //    DBDML.RunSQL("TaskAlgorithm.Insert", sql, DataStoreKey);

        //}

        //#endregion Create

        //#region Update

        //public static void Update(TaskAlgorithmDataModel data, RequestProfile requestProfile)
        //{
        //    var sql = CreateOrUpdate(data, requestProfile, "Update");

        //    DBDML.RunSQL("Clent.Update", sql, DataStoreKey);
        //}
        //#endregion Update

        //#region Renumber
        //public static void Renumber(int seed, int increment, RequestProfile requestProfile)
        //{
        //    var sql = "EXEC dbo.TaskAlgorithmRenumber " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //              ",@Seed = " + seed +
        //              ",@Increment = " + increment;

        //    DBDML.RunSQL("TaskAlgorithm.Renumber", sql, DataStoreKey);
        //}
        //#endregion Renumber

        //#region Delete

        //public static void Delete(TaskAlgorithmDataModel dataQuery, RequestProfile requestProfile)
        //{
        //    const string sql = @"dbo.TaskAlgorithmDelete ";

        //    var parameters =
        //    new
        //    {
        //        AuditId = requestProfile.AuditId
        //        ,
        //        TaskAlgorithmId = dataQuery.TaskAlgorithmId
        //    };

        //    using (var dataAccess = new DataAccessBase(DataStoreKey))
        //    {


        //        dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


        //    }
        //}

        //#endregion Delete

        //#region DoesExist

        //public static DataTable DoesExist(TaskAlgorithmDataModel data, RequestProfile requestProfile)
        //{
        //    var doesExistRequest = new TaskAlgorithmDataModel();
        //    doesExistRequest.Name = data.Name;

        //    return Search(doesExistRequest, requestProfile);
        //}

        //#endregion DoesExist

        //#region GetChildren

        //private static DataSet GetChildren(TaskAlgorithmDataModel data, RequestProfile requestProfile)
        //{
        //    var sql = "EXEC dbo.TaskAlgorithmChildrenGet " +
        //                    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //                    ", " + ToSQLParameter(data, TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId);

        //    var oDT = new DBDataSet("TaskAlgorithm.GetChildren", sql, DataStoreKey);

        //    return oDT.DBDataset;
        //}

        //#endregion

        //#region DeleteChildren

        //public static DataSet DeleteChildren(TaskAlgorithmDataModel data, RequestProfile requestProfile)
        //{
        //    var sql = "EXEC dbo.TaskAlgorithmChildrenDelete " +
        //                    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //                    ", " + ToSQLParameter(data, TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId);

        //    var oDT = new DBDataSet("TaskAlgorithm.DeleteChildren", sql, DataStoreKey);

        //    return oDT.DBDataset;
        //}

        //#endregion

        //#region IsDeletable

        //public static bool IsDeletable(TaskAlgorithmDataModel data, RequestProfile requestProfile)
        //{
        //    var isDeletable = true;

        //    var ds = GetChildren(data, requestProfile);

        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
        //        {
        //            isDeletable = false;
        //        }
        //    }

        //    return isDeletable;
        //}

        //#endregion
	}
}
