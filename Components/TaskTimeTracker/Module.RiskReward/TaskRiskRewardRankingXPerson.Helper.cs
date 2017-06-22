using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.RiskReward
{
    public partial class TaskRiskRewardRankingXPersonDataManager
    { 
        public class DataColumns : BaseDataModel.BaseDataColumns
        {
            public const string TaskRiskRewardRankingXPersonId = "TaskRiskRewardRankingXPersonId";
            public const string TaskId                         = "TaskId";
            public const string RiskId                         = "RiskId";
            public const string RewardId                       = "RewardId";
            public const string PersonId                       = "PersonId";

            public const string Task                           = "Task";
            public const string Risk                           = "Risk";
            public const string Reward                         = "Reward";
            public const string Person                         = "Person";

            public const string Ranking                        = "Ranking";			
        }   

        public class Data : BaseDataModel
        {
            public int? TaskRiskRewardRankingXPersonId		{ get; set; }
            public int? TaskId								{ get; set; }
            public int? RiskId								{ get; set; }
            public int? RewardId							{ get; set; }
            public int? PersonId							{ get; set; }
            public int? Ranking								{ get; set; }            

			public string ToSQLParameter(string dataColumnName)
			{
				var returnValue = "NULL";

				switch (dataColumnName)

                {

					case DataColumns.TaskRiskRewardRankingXPersonId:
						if (TaskRiskRewardRankingXPersonId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskRiskRewardRankingXPersonId, TaskRiskRewardRankingXPersonId);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskRiskRewardRankingXPersonId);
														
						}
                        break;

					case DataColumns.TaskId:
						if (TaskId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskId, TaskId);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskId);
														
						}
                        break;

					case DataColumns.RiskId:
						if (RiskId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.RiskId, RiskId);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.RiskId);
														
						}
                        break;

					case DataColumns.RewardId:
						if (RewardId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.RewardId, RewardId);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.RewardId);
														
						}
                        break;

                    case DataColumns.PersonId:
						if (PersonId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.PersonId, PersonId);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.PersonId);
														
						}
                        break;

					case DataColumns.Ranking:
						if (Ranking != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.Ranking, Ranking);
														
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Ranking);
														
						}
                        break;

					

                }
                return returnValue;
            }

        }

    }
}
