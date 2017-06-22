using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class EntityDateRangeStateDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static EntityDateRangeStateDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("EntityDateRangeState");
        }

        #region GetList

        public static List<EntityDateRangeStateDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(EntityDateRangeStateDataModel.Empty, requestProfile, 1);
        }

		
        #endregion

        #region GetDetails

		public static EntityDateRangeStateDataModel GetDetails(EntityDateRangeStateDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            return list.FirstOrDefault();
        }

        public static List<EntityDateRangeStateDataModel> GetEntityDetails(EntityDateRangeStateDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.EntityDateRangeStateSearch ";

            var parameters =
            new
            {
                    AuditId                     = requestProfile.AuditId
                ,   ApplicationId               = requestProfile.ApplicationId
				,	EntityDateRangeStateId		= dataQuery.EntityDateRangeStateId
				,	EntityDateRangeStateTypeId	= dataQuery.EntityDateRangeStateTypeId				
				,	KeyId						= dataQuery.KeyId
				//,   ReturnAuditInfo             = returnAuditInfo       
				//,   EndDate                     = dataQuery.EndDate               
				//,   SystemEntityId              = dataQuery.SystemEntityId
				//,   Notes                       = dataQuery.Notes
				//,   EntityDateRangeStateType    = dataQuery.EntityDateRangeStateType

            };

            List<EntityDateRangeStateDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<EntityDateRangeStateDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        //public static List<EntityDateRangeStateDataModel> GetEntityDetails(EntityDateRangeStateDataModel data, int auditId)
        //{
        //    var oDT = GetDetails(data, auditId);

        //    var dataList = new List<EntityDateRangeStateDataModel>();

        //    if (oDT.Rows.Count == 1)
        //    {
        //        var oData = new EntityDateRangeStateDataModel();

        //        var row = oDT.Rows[0];

        //        oData.EntityDateRangeStateId = (int?)row[EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateId];
        //        oData.EntityDateRangeStateTypeId = (int?)row[EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateTypeId];
        //        oData.StartDate = (DateTime?)row[EntityDateRangeStateDataModel.DataColumns.StartDate];
        //        oData.EndDate = (DateTime?)row[EntityDateRangeStateDataModel.DataColumns.EndDate];
        //        oData.KeyId = (int?)row[EntityDateRangeStateDataModel.DataColumns.KeyId];
        //        oData.SystemEntityId = (int?)row[EntityDateRangeStateDataModel.DataColumns.SystemEntityId];
        //        oData.Notes = (string)row[EntityDateRangeStateDataModel.DataColumns.Notes];
        //        oData.EntityDateRangeStateType = (string)row[EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateType];

        //        SetBaseInfo(oData, row);

        //        dataList.Add(oData);
        //    }

        //    return dataList;
        //}

        #endregion

        #region Create

        public static int Create(EntityDateRangeStateDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var entityDateRangeStateId = DBDML.RunScalarSQL("EntityDateRangeState.Insert", sql, DataStoreKey);
            return Convert.ToInt32(entityDateRangeStateId);
        }

        #endregion

        #region Update

        public static void Update(EntityDateRangeStateDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("EntityDateRangeState.Update", sql, DataStoreKey);
        }


        #endregion

        #region Delete

        public static void Delete(EntityDateRangeStateDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.EntityDateRangeStateDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                EntityDateRangeStateId = dataQuery.EntityDateRangeStateId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static string ToSQLParameter(EntityDateRangeStateDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateId:
                    if (data.EntityDateRangeStateId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateId, data.EntityDateRangeStateId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateId);
                    }
                    break;

                case EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateTypeId:
                    if (data.EntityDateRangeStateTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateTypeId, data.EntityDateRangeStateTypeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateTypeId);
                    }
                    break;

                case EntityDateRangeStateDataModel.DataColumns.EndDate:
                    if (data.EndDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityDateRangeStateDataModel.DataColumns.EndDate, data.EndDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateDataModel.DataColumns.EndDate);
                    }
                    break;

                case EntityDateRangeStateDataModel.DataColumns.StartDate:
                    if (data.StartDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityDateRangeStateDataModel.DataColumns.StartDate, data.StartDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateDataModel.DataColumns.StartDate);

                    }
                    break;

                case EntityDateRangeStateDataModel.DataColumns.SystemEntityId:
                    if (data.SystemEntityId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityDateRangeStateDataModel.DataColumns.SystemEntityId, data.SystemEntityId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateDataModel.DataColumns.SystemEntityId);
                    }
                    break;

                case EntityDateRangeStateDataModel.DataColumns.KeyId:
                    if (data.KeyId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityDateRangeStateDataModel.DataColumns.KeyId, data.KeyId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateDataModel.DataColumns.KeyId);
                    }
                    break;

                case EntityDateRangeStateDataModel.DataColumns.Notes:
                    if (!string.IsNullOrEmpty(data.Notes))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityDateRangeStateDataModel.DataColumns.Notes, data.Notes);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateDataModel.DataColumns.Notes);

                    }
                    break;

                case EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateType:
                    if (!string.IsNullOrEmpty(data.EntityDateRangeStateType))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateType, data.EntityDateRangeStateType);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateType);

                    }
                    break;

            }
            return returnValue;
        }

        public static DataTable Search(EntityDateRangeStateDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(EntityDateRangeStateDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.EntityDateRangeStateInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.EntityDateRangeStateUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateId) +
                        ", " + ToSQLParameter(data, EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateTypeId) +
                        ", " + ToSQLParameter(data, EntityDateRangeStateDataModel.DataColumns.EndDate) +
                        ", " + ToSQLParameter(data, EntityDateRangeStateDataModel.DataColumns.StartDate) +
                        ", " + ToSQLParameter(data, EntityDateRangeStateDataModel.DataColumns.SystemEntityId) +
                        ", " + ToSQLParameter(data, EntityDateRangeStateDataModel.DataColumns.KeyId) +
                        ", " + ToSQLParameter(data, EntityDateRangeStateDataModel.DataColumns.Notes);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(EntityDateRangeStateDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new EntityDateRangeStateDataModel();
            doesExistRequest.KeyId = data.KeyId;
			doesExistRequest.SystemEntityId = data.SystemEntityId;
			doesExistRequest.StartDate = data.StartDate;
			doesExistRequest.EndDate = data.EndDate;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }

        #endregion
    }
}
