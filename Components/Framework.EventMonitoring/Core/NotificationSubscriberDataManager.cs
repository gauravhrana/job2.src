using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.EventMonitoring;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.EventMonitoring
{
    public partial class NotificationSubscriberDataManager : StandardDataManager
    {
        //private static string DataStoreKey = "";

        //static NotificationSubscriberDataManager()
        //{
        //    DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationSubscriber");
        //}

        //#region GetList

        //#region GetList

        //public static DataTable GetList(int applicationId, int auditId)
        //{
        //	var sql = "EXEC dbo.NotificationSubscriberList "
        //		+ " " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, applicationId)
        //		+ "," + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

        //	var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, DataStoreKey);

        //	return oDT.DBTable;
        //}

        //#endregion GetList

        ////public static DataTable GetList(RequestProfile requestProfile)
        ////{
        ////    var sql = "EXEC dbo.NotificationSubscriberSearch " +
        ////        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
        ////        + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
        ////    var oDT = new DBDataTable("NotificationSubscriber.GetList", sql, DataStoreKey);

        ////    return oDT.DBTable;
        ////}

        //#endregion GetList

        //#region Search
        //public static string ToSQLParameter(NotificationSubscriberDataModel data, string dataColumnName)
        //{
        //    var returnValue = "NULL";

        //    switch (dataColumnName)
        //    {
        //        case NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId:
        //            if (data.NotificationSubscriberId != null)
        //            {
        //                returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId, data.NotificationSubscriberId);
        //            }
        //            else
        //            {
        //                returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId);
        //            }
        //            break;

        //        default:
        //            returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
        //            break;

        //    }

        //    return returnValue;
        //}

        //public static DataTable Search(NotificationSubscriberDataModel data, RequestProfile requestProfile)
        //{
        //    var list = GetEntityDetails(data, requestProfile, 0);

        //    var table = list.ToDataTable();

        //    return table;
        //}

        //#endregion Search

        //#region GetDetails

        //public static DataTable GetDetails(NotificationSubscriberDataModel data, RequestProfile requestProfile)
        //{
        //    var list = GetEntityDetails(data, requestProfile);

        //    var table = list.ToDataTable();

        //    return table;
        //}

        //#endregion

        //#region GetEntitySearch

        //public static List<NotificationSubscriberDataModel> GetEntityDetails(NotificationSubscriberDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        //{
        //    const string sql = @"dbo.NotificationSubscriberSearch ";

        //    var parameters =
        //    new
        //    {
        //        AuditId = requestProfile.AuditId
        //        ,
        //        ApplicationId = requestProfile.ApplicationId
        //        ,
        //        NotificationSubscriberId = dataQuery.NotificationSubscriberId
        //        ,
        //        Name = dataQuery.Name
        //        ,
        //        ReturnAuditInfo = returnAuditInfo
        //        ,
        //        ApplicationMode = requestProfile.ApplicationModeId
        //    };

        //    List<NotificationSubscriberDataModel> result;

        //    using (var dataAccess = new DataAccessBase(DataStoreKey))
        //    {
        //        result = dataAccess.Connection.Query<NotificationSubscriberDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
        //    }

        //    return result;
        //}

        //#endregion GetDetails

        //#region CreateOrUpdate
        //private static string CreateOrUpdate(NotificationSubscriberDataModel data, RequestProfile requestProfile, string action)
        //{
        //    var sql = "EXEC ";

        //    switch (action)
        //    {
        //        case "Create":
        //            sql += "dbo.NotificationSubscriberInsert  " + "\r\n" +
        //                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
        //            break;

        //        case "Update":
        //            sql += "dbo.NotificationSubscriberUpdate  " +
        //                   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
        //            break;

        //        default:
        //            break;
        //    }

        //    sql = sql + ", " + ToSQLParameter(data, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId) +
        //                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
        //                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
        //                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

        //    return sql;
        //}

        //#endregion CreateOrUpdate

        //#region Create

        //public static void Create(NotificationSubscriberDataModel data, RequestProfile requestProfile)
        //{
        //    var sql = CreateOrUpdate(data, requestProfile, "Create");

        //    Framework.Components.DataAccess.DBDML.RunSQL("NotificationSubscriber.Insert", sql, DataStoreKey);

        //}

        //#endregion Create

        //#region Update

        //public static void Update(NotificationSubscriberDataModel data, RequestProfile requestProfile)
        //{
        //    var sql = CreateOrUpdate(data, requestProfile, "Update");

        //    DBDML.RunSQL("Clent.Update", sql, DataStoreKey);
        //}
        //#endregion Update

        //#region Renumber
        //public static void Renumber(int seed, int increment, RequestProfile requestProfile)
        //{
        //    var sql = "EXEC dbo.NotificationSubscriberRenumber " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //              ",@Seed = " + seed +
        //              ",@Increment = " + increment;

        //    DBDML.RunSQL("NotificationSubscriber.Renumber", sql, DataStoreKey);
        //}
        //#endregion Renumber

        //#region Delete

        //public static void Delete(NotificationSubscriberDataModel dataQuery, RequestProfile requestProfile)
        //{
        //    const string sql = @"dbo.NotificationSubscriberDelete ";

        //    var parameters =
        //    new
        //    {
        //        AuditId = requestProfile.AuditId
        //        ,
        //        NotificationSubscriberId = dataQuery.NotificationSubscriberId

        //    };

        //    using (var dataAccess = new DataAccessBase(DataStoreKey))
        //    {


        //        dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


        //    }
        //}

        //#endregion Delete

        //#region DoesExist

        //public static bool DoesExist(NotificationSubscriberDataModel data, RequestProfile requestProfile)
        //{
        //    var doesExistRequest = new NotificationSubscriberDataModel();
        //    doesExistRequest.Name = data.Name;

        //    return Search(doesExistRequest, requestProfile);
        //}

        //#endregion DoesExist

        //#region GetChildren

        //private static DataSet GetChildren(NotificationSubscriberDataModel data, RequestProfile requestProfile)
        //{
        //    var sql = "EXEC dbo.NotificationSubscriberChildrenGet " +
        //                    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //                    ", " + ToSQLParameter(data, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId);

        //    var oDT = new DBDataSet("NotificationSubscriber.GetChildren", sql, DataStoreKey);

        //    return oDT.DBDataset;
        //}

        //#endregion

        //#region DeleteChildren

        //public static DataSet DeleteChildren(NotificationSubscriberDataModel data, RequestProfile requestProfile)
        //{
        //    var sql = "EXEC dbo.NotificationSubscriberChildrenDelete " +
        //                    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
        //                    ", " + ToSQLParameter(data, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId);

        //    var oDT = new DBDataSet("NotificationSubscriber.DeleteChildren", sql, DataStoreKey);

        //    return oDT.DBDataset;
        //}

        //#endregion

        //#region IsDeletable

        //public static bool IsDeletable(NotificationSubscriberDataModel data, RequestProfile requestProfile)
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
