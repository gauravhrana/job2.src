using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class ScheduleQuestionDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ScheduleQuestionDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ScheduleQuestion");
		}

		#region GetList

        public static List<ScheduleQuestionDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ScheduleQuestionDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Get By Schedule

		public static DataTable GetBySchedule(int scheduleId, RequestProfile requestProfile)
		{
			var sql = "EXEC ScheduleQuestionSearch @ScheduleId       =" + scheduleId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDT.DBTable;
		}


		#endregion

		#region GetDetails

		public static string ToSQLParameter(ScheduleQuestionDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId:
					if (data.ScheduleQuestionId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId, data.ScheduleQuestionId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId);
					}
					break;

				case ScheduleQuestionDataModel.DataColumns.ScheduleId:
					if (data.ScheduleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleQuestionDataModel.DataColumns.ScheduleId, data.ScheduleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleQuestionDataModel.DataColumns.ScheduleId);
					}
					break;

				case ScheduleQuestionDataModel.DataColumns.QuestionId:
					if (data.QuestionId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleQuestionDataModel.DataColumns.QuestionId, data.QuestionId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleQuestionDataModel.DataColumns.QuestionId);
					}
					break;

				case ScheduleQuestionDataModel.DataColumns.FromSearchDate:
					if (data.FromSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleQuestionDataModel.DataColumns.FromSearchDate, data.FromSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleQuestionDataModel.DataColumns.FromSearchDate);
					}
					break;

				case ScheduleQuestionDataModel.DataColumns.ToSearchDate:
					if (data.ToSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleQuestionDataModel.DataColumns.ToSearchDate, data.ToSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleQuestionDataModel.DataColumns.ToSearchDate);
					}
					break;

				case ScheduleQuestionDataModel.DataColumns.Answer:
					if (data.Answer != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleQuestionDataModel.DataColumns.Answer, data.Answer);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleQuestionDataModel.DataColumns.Answer);

					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static ScheduleQuestionDataModel GetDetails(ScheduleQuestionDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<ScheduleQuestionDataModel> GetEntityDetails(ScheduleQuestionDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ScheduleQuestionSearch ";

			var parameters =
			new
			{
				    AuditId                 = requestProfile.AuditId
				,   ApplicationId           = requestProfile.ApplicationId
				,   ScheduleQuestionId      = dataQuery.ScheduleQuestionId
				,   ScheduleId              = dataQuery.ScheduleId
				,   QuestionId              = dataQuery.QuestionId
				,   Answer                  = dataQuery.Answer
				,   FromSearchDate          = dataQuery.FromSearchDate
				,   ToSearchDate            = dataQuery.ToSearchDate
				,   ReturnAuditInfo         = returnAuditInfo
			};

			List<ScheduleQuestionDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ScheduleQuestionDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		public static List<ScheduleQuestionDataModel> GetScheduleQuestionList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ScheduleQuestionSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var result = new List<ScheduleQuestionDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new ScheduleQuestionDataModel();

					dataItem.ScheduleId = (int)dbReader[ScheduleQuestionDataModel.DataColumns.ScheduleId];
					dataItem.ScheduleQuestionId = (int?)dbReader[ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId];
					dataItem.QuestionId = (int?)dbReader[ScheduleQuestionDataModel.DataColumns.QuestionId];
					dataItem.Answer = (string)dbReader[ScheduleQuestionDataModel.DataColumns.Answer];
					dataItem.QuestionPhrase = (string)dbReader[ScheduleQuestionDataModel.DataColumns.QuestionPhrase];

					result.Add(dataItem);
				}

			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(ScheduleQuestionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("ScheduleQuestion.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ScheduleQuestionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("ScheduleQuestion.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ScheduleQuestionDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ScheduleQuestionDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ScheduleQuestionId = dataQuery.ScheduleQuestionId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(ScheduleQuestionDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(ScheduleQuestionDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ScheduleQuestionInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ScheduleQuestionUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId) +
						", " + ToSQLParameter(data, ScheduleQuestionDataModel.DataColumns.ScheduleId) +
						", " + ToSQLParameter(data, ScheduleQuestionDataModel.DataColumns.QuestionId) +
						", " + ToSQLParameter(data, ScheduleQuestionDataModel.DataColumns.Answer);

			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ScheduleQuestionDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ScheduleQuestionDataModel();
			doesExistRequest.ScheduleId = data.ScheduleId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

	}
}
