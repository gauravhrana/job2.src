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
	public partial class ScheduleEmailDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ScheduleEmailDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ScheduleEmail");
		}

		#region GetDetails

		public static ScheduleEmailDataModel GetDetails(ScheduleEmailDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);
			return list.FirstOrDefault();
		}

		public static List<ScheduleEmailDataModel> GetEntityDetails(ScheduleEmailDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ScheduleEmailSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
					//,   ApplicationId       = requestProfile.ApplicationId
				,
				ScheduleId = dataQuery.ScheduleId
				,
				ScheduleEmailId = dataQuery.ScheduleEmailId
			};

			List<ScheduleEmailDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ScheduleEmailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion
		#region Create

		public static int Create(ScheduleEmailDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			var id = DBDML.RunScalarSQL("ScheduleEmail.Insert", sql, DataStoreKey);
			return Convert.ToInt32(id);
		}

		#endregion

		#region Update

		public static void Update(ScheduleEmailDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("ScheduleEmail.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ScheduleEmailDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ScheduleEmailDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ScheduleEmailId = dataQuery.ScheduleEmailId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion
		#region Search

		public static string ToSQLParameter(ScheduleEmailDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ScheduleEmailDataModel.DataColumns.ScheduleEmailId:
					if (data.ScheduleEmailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleEmailDataModel.DataColumns.ScheduleEmailId, data.ScheduleEmailId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.ScheduleEmailId);

					}
					break;

				case ScheduleEmailDataModel.DataColumns.ScheduleId:
					if (data.ScheduleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.ScheduleId, data.ScheduleId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.ScheduleId);

					}
					break;

				case ScheduleEmailDataModel.DataColumns.SentDate:
					if (data.SentDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.SentDate, data.SentDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.SentDate);

					}
					break;

				case ScheduleEmailDataModel.DataColumns.CreatedDate:
					if (data.CreatedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.CreatedDate, data.CreatedDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.CreatedDate);

					}
					break;


				case ScheduleEmailDataModel.DataColumns.SentTo:
					if (!string.IsNullOrEmpty(data.SentTo))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.SentTo, data.SentTo);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.SentTo);

					}
					break;

			

				case ScheduleEmailDataModel.DataColumns.UpdatedDate:
					if (data.UpdatedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.UpdatedDate, data.UpdatedDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.UpdatedDate);

					}
					break;
				//case ScheduleEmailDataModel.DataColumns.LastAction:
				//	if (data.LastAction != null)
				//	{
				//		returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.LastAction, data.LastAction);

				//	}
				//	else
				//	{
				//		returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.LastAction);

				//	}
				//	break;

				case ScheduleEmailDataModel.DataColumns.ModifiedDate:
					if (data.ModifiedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.ModifiedDate, data.ModifiedDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.ModifiedDate);

					}
					break;

				case ScheduleEmailDataModel.DataColumns.CreatedByAuditId:
					if (data.CreatedByAuditId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.CreatedByAuditId, data.CreatedByAuditId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.CreatedByAuditId);

					}
					break;

				case ScheduleEmailDataModel.DataColumns.ModifiedByAuditId:
					if (data.ModifiedByAuditId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.ModifiedByAuditId, data.ModifiedByAuditId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.ModifiedByAuditId);

					}
					break;
				case ScheduleEmailDataModel.DataColumns.FromSearchDate:
					if (data.FromSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.FromSearchDate, data.FromSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.FromSearchDate);
					}
					break;

				case ScheduleEmailDataModel.DataColumns.ToSearchDate:
					if (data.ToSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleEmailDataModel.DataColumns.ToSearchDate, data.ToSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleEmailDataModel.DataColumns.ToSearchDate);
					}
					break;



				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			return returnValue;
		}
		public static DataTable Search(ScheduleEmailDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile,0);

			var table = list.ToDataTable();

			return table;
		}
		#endregion

		#region DoesExist

		public static bool DoesExist(ScheduleEmailDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ScheduleEmailDataModel();
			doesExistRequest.ScheduleId = data.ScheduleId;


			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
			return list.Count > 0;
		}

		#endregion

		#region Save

		private static string Save(ScheduleEmailDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ScheduleEmailInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
						//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ScheduleEmailUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
						//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.ScheduleEmailId) +
				", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.ScheduleId) +
				", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.SentDate) +
				", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.CreatedDate) +
				", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.UpdatedDate) +
				", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.SentTo) +
				", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.ModifiedDate) +
				", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.CreatedByAuditId) +
				", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.ModifiedByAuditId);
				//", " + ToSQLParameter(data, ScheduleEmailDataModel.DataColumns.LastAction);
				

			return sql;
		}

		#endregion
	}
}



