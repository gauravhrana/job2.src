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
    public partial class MilestoneDataManager : StandardDataManager
    {
        static readonly string DataStoreKey = "";

        public MilestoneDataManager()
        {
        }

        static MilestoneDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("Milestone");
        }

        #region GetList

        public static List<MilestoneDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(MilestoneDataModel.Empty, requestProfile, 1);
        }

        #endregion GetList

        #region Search

        public static string ToSQLParameter(MilestoneDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case MilestoneDataModel.DataColumns.MilestoneId:
                    if (data.MilestoneId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneDataModel.DataColumns.MilestoneId, data.MilestoneId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneDataModel.DataColumns.MilestoneId);
                    }
                    break;
                case MilestoneDataModel.DataColumns.ProjectId:
                    if (data.ProjectId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneDataModel.DataColumns.ProjectId, data.ProjectId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneDataModel.DataColumns.ProjectId);
                    }
                    break;

                case MilestoneDataModel.DataColumns.Project:
                    if (data.Project != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneDataModel.DataColumns.Project, data.Project);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneDataModel.DataColumns.Project);

                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(MilestoneDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion Search

        #region GetDetails

		public static MilestoneDataModel GetDetails(MilestoneDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            return list.FirstOrDefault();

        }

        public static List<MilestoneDataModel> GetEntityDetails(MilestoneDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.MilestoneSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   MilestoneId             = dataQuery.MilestoneId
                ,   Name                    = dataQuery.Name
                ,   ProjectId               = dataQuery.ProjectId
            };

            List<MilestoneDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<MilestoneDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        //public static List<MilestoneDataModel> GetEntityDetails(MilestoneDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.MilestoneSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //              ", " + ToSQLParameter(data, MilestoneDataModel.DataColumns.MilestoneId);

        //    var result = new List<MilestoneDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new MilestoneDataModel();

        //            dataItem.MilestoneId	= (int)dbReader[MilestoneDataModel.DataColumns.MilestoneId];
        //            dataItem.ProjectId		= (int)dbReader[MilestoneDataModel.DataColumns.ProjectId];
        //            dataItem.Project		= (string)dbReader[MilestoneDataModel.DataColumns.Project];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

        #endregion GetDetails

        #region CreateOrUpdate

        private static string CreateOrUpdate(MilestoneDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.MilestoneInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.MilestoneUpdate  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, MilestoneDataModel.DataColumns.MilestoneId) +
                ", " + ToSQLParameter(data, MilestoneDataModel.DataColumns.ProjectId) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

            return sql;

        }
        #endregion CreateOrUpdate

        #region Create

        public static int Create(MilestoneDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var milestoneId = DBDML.RunScalarSQL("Milestone.Insert", sql, DataStoreKey);
            return Convert.ToInt32(milestoneId);
        }

        #endregion Create

        #region Update

        public static void Update(MilestoneDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("Milestone.Update", sql, DataStoreKey);
        }

        #endregion Update

        #region Delete

        public static void Delete(MilestoneDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.MilestoneDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                MilestoneId = dataQuery.MilestoneId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion Delete

        #region DoesExist

        public static bool DoesExist(MilestoneDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new MilestoneDataModel();
            doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }


        #endregion DoesExist

        #region GetChildren

        private static DataSet GetChildren(MilestoneDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.MilestoneChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, MilestoneDataModel.DataColumns.MilestoneId);

            var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(MilestoneDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;
            var ds = GetChildren(data, requestProfile);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        isDeletable = false;
                        break;
                    }
                }
            }
            return isDeletable;
        }

        #endregion

        #region Renumber

        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.MilestoneRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                      ",@Seed = " + seed +
                      ",@Increment = " + increment;

            DBDML.RunSQL("Milestone.Renumber", sql, DataStoreKey);
        }

        #endregion Renumber

    }
}
