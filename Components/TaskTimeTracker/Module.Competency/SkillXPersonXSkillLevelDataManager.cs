using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.Competency;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.Module.Competency
{
    public partial class SkillXPersonXSkillLevelDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static SkillXPersonXSkillLevelDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("SkillXPersonXSkillLevel");
        }

        #region GetList

        public static List<SkillXPersonXSkillLevelDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(SkillXPersonXSkillLevelDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static SkillXPersonXSkillLevelDataModel GetDetails(SkillXPersonXSkillLevelDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region Create

        public static void Create(SkillXPersonXSkillLevelDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("SkillXPersonXSkillLevel.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(SkillXPersonXSkillLevelDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("SkillXPersonXSkillLevel.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(SkillXPersonXSkillLevelDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.SkillXPersonXSkillLevelDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.PersonId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId);

            Framework.Components.DataAccess.DBDML.RunSQL("SkillXPersonXSkillLevel.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(SkillXPersonXSkillLevelDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId:
                    if (data.SkillXPersonXSkillLevelId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId, data.SkillXPersonXSkillLevelId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId);
                    }
                    break;

                case SkillXPersonXSkillLevelDataModel.DataColumns.SkillId:
                    if (data.SkillId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SkillXPersonXSkillLevelDataModel.DataColumns.SkillId, data.SkillId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SkillXPersonXSkillLevelDataModel.DataColumns.SkillId);
                    }
                    break;

                case SkillXPersonXSkillLevelDataModel.DataColumns.PersonId:
                    if (data.PersonId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SkillXPersonXSkillLevelDataModel.DataColumns.PersonId, data.PersonId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SkillXPersonXSkillLevelDataModel.DataColumns.PersonId);
                    }
                    break;

                case SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId:
                    if (data.SkillLevelId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId, data.SkillLevelId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId);
                    }
                    break;


                case SkillXPersonXSkillLevelDataModel.DataColumns.Skill:
                    if (!string.IsNullOrEmpty(data.Skill))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SkillXPersonXSkillLevelDataModel.DataColumns.Skill, data.Skill);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SkillXPersonXSkillLevelDataModel.DataColumns.Skill);
                    }
                    break;

                case SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevel:
                    if (!string.IsNullOrEmpty(data.SkillLevel))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevel, data.SkillLevel);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevel);
                    }
                    break;

            }

            return returnValue;
        }

        public static List<SkillXPersonXSkillLevelDataModel> GetEntityDetails(SkillXPersonXSkillLevelDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.SkillXPersonXSkillLevelSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   SkillXPersonXSkillLevelId             = dataQuery.SkillXPersonXSkillLevelId
                ,   SkillId                    = dataQuery.SkillId
                ,   PersonId               = dataQuery.PersonId
                ,   SkillLevelId               = dataQuery.SkillLevelId
            };

            List<SkillXPersonXSkillLevelDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<SkillXPersonXSkillLevelDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        public static DataTable Search(SkillXPersonXSkillLevelDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.SkillXPersonXSkillLevelSearch " +
                " " + BaseDataManager.SetCommonParametersForSearch(requestProfile.AuditId, requestProfile.ApplicationId, requestProfile.ApplicationModeId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.PersonId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("SkillXPersonXSkillLevel.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(SkillXPersonXSkillLevelDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.SkillXPersonXSkillLevelInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.SkillXPersonXSkillLevelUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;
            }
            sql = sql + ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId) +
                        ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillId) +
                        ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.PersonId) +
                        ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(SkillXPersonXSkillLevelDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.SkillXPersonXSkillLevelSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("SkillXPersonXSkillLevel.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

    }
}
