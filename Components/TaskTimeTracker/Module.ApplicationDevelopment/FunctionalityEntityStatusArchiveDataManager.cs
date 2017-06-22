using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class FunctionalityEntityStatusArchiveDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static FunctionalityEntityStatusArchiveDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityEntityStatusArchive");
		}

		#region GetList

        public static List<FunctionalityEntityStatusArchiveDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityEntityStatusArchiveDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static FunctionalityEntityStatusArchiveDataModel GetDetails(FunctionalityEntityStatusArchiveDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityEntityStatusArchiveDataModel> GetEntityDetails(FunctionalityEntityStatusArchiveDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityEntityStatusArchiveSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	FunctionalityEntityStatusArchiveId = dataQuery.FunctionalityEntityStatusArchiveId
				,	SystemEntityType = dataQuery.SystemEntityType
				,	Functionality = dataQuery.Functionality
				,	FunctionalityStatus = dataQuery.FunctionalityStatus
				,	FunctionalityPriority = dataQuery.FunctionalityPriority
				,	FunctionalityEntityStatusId = dataQuery.FunctionalityEntityStatusId
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<FunctionalityEntityStatusArchiveDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityEntityStatusArchiveDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}


		#endregion

		#region Create

		public static void Create(FunctionalityEntityStatusArchiveDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("FunctionalityEntityStatusArchive.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(FunctionalityEntityStatusArchiveDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("FunctionalityEntityStatusArchive.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FunctionalityEntityStatusArchiveDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityEntityStatusArchiveDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				FunctionalityEntityStatusArchiveId = dataQuery.FunctionalityEntityStatusArchiveId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(FunctionalityEntityStatusArchiveDataModel data, string dataColumnName, object value)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusArchiveId:
					if (data.FunctionalityEntityStatusArchiveId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusArchiveId, data.FunctionalityEntityStatusArchiveId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusArchiveId);
					}
					break;

				case FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusId:
					if (data.FunctionalityEntityStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusId, data.FunctionalityEntityStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusId);
					}
					break;

				case FunctionalityEntityStatusArchiveDataModel.DataColumns.Functionality:
					if (data.Functionality != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusArchiveDataModel.DataColumns.Functionality, data.Functionality);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.Functionality);
					}
					break;

				case FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityStatus:
					if (value != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityStatus, value);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityStatus);
					}
					break;

				case FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityPriority:
					if (data.FunctionalityPriority != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityPriority, data.FunctionalityPriority);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityPriority);
					}
					break;

				case FunctionalityEntityStatusArchiveDataModel.DataColumns.Memo:
					if (data.Memo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusArchiveDataModel.DataColumns.Memo, data.Memo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.Memo);
					}
					break;

				case FunctionalityEntityStatusArchiveDataModel.DataColumns.SystemEntityType:
					if (data.SystemEntityType != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusArchiveDataModel.DataColumns.SystemEntityType, data.SystemEntityType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.SystemEntityType);
					}
					break;

				case FunctionalityEntityStatusArchiveDataModel.DataColumns.StartDate:
					if (data.StartDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusArchiveDataModel.DataColumns.StartDate, data.StartDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.StartDate);
					}
					break;


				case FunctionalityEntityStatusArchiveDataModel.DataColumns.TargetDate:
					if (data.TargetDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusArchiveDataModel.DataColumns.TargetDate, data.TargetDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.TargetDate);
					}
					break;


				case FunctionalityEntityStatusArchiveDataModel.DataColumns.AssignedTo:
					if (data.AssignedTo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityEntityStatusArchiveDataModel.DataColumns.AssignedTo, data.AssignedTo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityEntityStatusArchiveDataModel.DataColumns.AssignedTo);
					}
					break;



				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(FunctionalityEntityStatusArchiveDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		public static DataTable SearchHistory(FunctionalityEntityStatusArchiveDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.FunctionalityEntityStatusArchiveSearchHistory " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusArchiveId, data.FunctionalityEntityStatusArchiveId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.SystemEntityType, data.SystemEntityType) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.Functionality, data.Functionality) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityStatus, data.FunctionalityStatus) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityPriority, data.FunctionalityPriority) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusId, data.FunctionalityEntityStatusId);

			var oDT = new DBDataTable("FunctionalityEntityStatusArchive.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region CreatOrUpdate

		private static string CreateOrUpdate(FunctionalityEntityStatusArchiveDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityEntityStatusArchiveInsert  ";
					break;

				case "Update":
					sql += "dbo.FunctionalityEntityStatusArchiveUpdate  ";
					break;

				default:
					break;

			}

			sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusArchiveId, data.FunctionalityEntityStatusArchiveId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.RecordDate, data.RecordDate) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.SystemEntityType, data.SystemEntityType) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.Functionality, data.Functionality) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityStatus, data.FunctionalityStatus) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityPriority, data.FunctionalityPriority) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusId, data.FunctionalityEntityStatusId) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.AssignedTo, data.AssignedTo) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.Memo, data.Memo) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.TargetDate, data.TargetDate) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.StartDate, data.StartDate) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.AcknowledgedById, data.AcknowledgedById) +
				", " + ToSQLParameter(data, FunctionalityEntityStatusArchiveDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(FunctionalityEntityStatusArchiveDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityEntityStatusArchiveDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion

	}
}