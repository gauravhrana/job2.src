using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.RiskReward
{
    public partial class TaskRiskRewardRankingXPersonDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = ""; 
        
        static TaskRiskRewardRankingXPersonDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskRiskRewardRankingXPerson");
        }

        #region GetList

        public static List<Data> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(new Data(), requestProfile);
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(Data data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskRiskRewardRankingXPersonSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails ) +
                ", " + data.ToSQLParameter(DataColumns.TaskRiskRewardRankingXPersonId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("TaskRiskRewardRankingXPerson.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<Data> GetEntityDetails(Data data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskRiskRewardRankingXPersonSearch " +
               " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +                
			   ", " + data.ToSQLParameter(DataColumns.TaskRiskRewardRankingXPersonId);

			var result = new List<Data>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new Data();

					dataItem.TaskRiskRewardRankingXPersonId = (int?)dbReader[DataColumns.TaskRiskRewardRankingXPersonId];
					dataItem.TaskId = (int?)dbReader[DataColumns.TaskId];
					dataItem.RiskId = (int?)dbReader[DataColumns.TaskId];
					dataItem.RewardId = (int?)dbReader[DataColumns.RewardId];
					dataItem.PersonId = (int?)dbReader[DataColumns.PersonId];
					dataItem.Ranking = (int?)dbReader[DataColumns.Ranking];	
				
					//SetBaseInfo(dataItem, dbReader);

					result.Add(dataItem);
				}
			}

			return result;
		}

        #endregion

        #region Create

        public static void Create(Data data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("TaskRiskRewardRankingXPerson.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(Data data, RequestProfile requestProfile )
        {
            var sql = Save(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("TaskRiskRewardRankingXPerson.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(Data data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskRiskRewardRankingXPersonDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + data.ToSQLParameter(DataColumns.TaskRiskRewardRankingXPersonId);

            Framework.Components.DataAccess.DBDML.RunSQL("TaskRiskRewardRankingXPerson.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(Data data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.TaskRiskRewardRankingXPersonSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.TaskRiskRewardRankingXPersonId) +
                ", " + data.ToSQLParameter(DataColumns.TaskId) +
                ", " + data.ToSQLParameter(DataColumns.RiskId) +
                ", " + data.ToSQLParameter(DataColumns.RewardId) +
                ", " + data.ToSQLParameter(DataColumns.PersonId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("TaskRiskRewardRankingXPerson.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(Data data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TaskRiskRewardRankingXPersonInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TaskRiskRewardRankingXPersonUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + data.ToSQLParameter(DataColumns.TaskRiskRewardRankingXPersonId) +
                        ", " + data.ToSQLParameter(DataColumns.TaskId) +
                        ", " + data.ToSQLParameter(DataColumns.RiskId) +
                        ", " + data.ToSQLParameter(DataColumns.RewardId) +
                        ", " + data.ToSQLParameter(DataColumns.PersonId) +
                        ", " + data.ToSQLParameter(DataColumns.Ranking) +
						", " + data.ToSQLParameter(BaseDataModel.BaseDataColumns.UpdatedBy) +
						", " + data.ToSQLParameter(BaseDataModel.BaseDataColumns.UpdatedDate);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(Data data, RequestProfile requestProfile)
        {
            var newData = new Data();
            newData.RiskId = data.RiskId;
            newData.TaskRiskRewardRankingXPersonId = data.TaskRiskRewardRankingXPersonId;

            var list = GetEntityDetails(newData, requestProfile);
            return list.Count > 0;
        }

        #endregion

    }
}
