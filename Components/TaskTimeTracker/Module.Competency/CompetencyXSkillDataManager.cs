using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CompetencyTimeTracker.Skill;

namespace TaskTimeTracker.Components.Module.Competency
{
    public partial class CompetencyXSkillDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static CompetencyXSkillDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("CompetencyXSkill");
        }

        #region GetList

        public static List<CompetencyXSkillDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(CompetencyXSkillDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static CompetencyXSkillDataModel GetDetails(CompetencyXSkillDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<CompetencyXSkillDataModel> GetEntityDetails(CompetencyXSkillDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.CompetencyXSkillSearch " +
              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
               ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
              ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.CompetencyXSkillId);

            var result = new List<CompetencyXSkillDataModel>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new CompetencyXSkillDataModel();

                    dataItem.CompetencyXSkillId = (int?)dbReader[CompetencyXSkillDataModel.DataColumns.CompetencyXSkillId];
                    dataItem.CompetencyId = (int?)dbReader[CompetencyXSkillDataModel.DataColumns.CompetencyId];
                    dataItem.SkillId = (int?)dbReader[CompetencyXSkillDataModel.DataColumns.SkillId];
                    dataItem.Competency = (string)dbReader[CompetencyXSkillDataModel.DataColumns.Competency];
                    dataItem.Skill = (string)dbReader[CompetencyXSkillDataModel.DataColumns.Skill];
                    SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(CompetencyXSkillDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("CompetencyXSkill.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(CompetencyXSkillDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("CompetencyXSkill.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(CompetencyXSkillDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.CompetencyXSkillDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.CompetencyXSkillId);

            Framework.Components.DataAccess.DBDML.RunSQL("CompetencyXSkill.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(CompetencyXSkillDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.CompetencyXSkillSearch " +
                " " + BaseDataManager.SetCommonParametersForSearch(requestProfile.AuditId, requestProfile.ApplicationId, requestProfile.ApplicationModeId) +
                ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.CompetencyXSkillId) +
                ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.CompetencyId) +
                ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.SkillId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("CompetencyXSkill.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(CompetencyXSkillDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.CompetencyXSkillInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.CompetencyXSkillUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.CompetencyXSkillId) +
                        ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.SkillId) +
                        ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.CompetencyId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(CompetencyXSkillDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.CompetencyXSkillSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.SkillId) +
            ", " + ToSQLParameter(data, CompetencyXSkillDataModel.DataColumns.CompetencyId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("CompetencyXSkill.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

    }
}
