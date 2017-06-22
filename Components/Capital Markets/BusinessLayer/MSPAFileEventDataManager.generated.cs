using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class MSPAFileEventDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static MSPAFileEventDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MSPAFileEvent");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(MSPAFileEventDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case MSPAFileEventDataModel.DataColumns.MSPAFileEventId:
					if (data.MSPAFileEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileEventDataModel.DataColumns.MSPAFileEventId, data.MSPAFileEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventDataModel.DataColumns.MSPAFileEventId);
					}
					break;

				case MSPAFileEventDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileEventDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventDataModel.DataColumns.Description);
					}
					break;

				case MSPAFileEventDataModel.DataColumns.CreatedBy:
					if (!string.IsNullOrEmpty(data.CreatedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileEventDataModel.DataColumns.CreatedBy, data.CreatedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventDataModel.DataColumns.CreatedBy);
					}
					break;

				case MSPAFileEventDataModel.DataColumns.CreatedOn:
					if (data.CreatedOn != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileEventDataModel.DataColumns.CreatedOn, data.CreatedOn);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventDataModel.DataColumns.CreatedOn);
					}
					break;

				case MSPAFileEventDataModel.DataColumns.MSPAFileId:
					if (data.MSPAFileId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileEventDataModel.DataColumns.MSPAFileId, data.MSPAFileId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventDataModel.DataColumns.MSPAFileId);
					}
					break;

				case MSPAFileEventDataModel.DataColumns.MSPAFile:
					if (!string.IsNullOrEmpty(data.MSPAFile))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileEventDataModel.DataColumns.MSPAFile, data.MSPAFile);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventDataModel.DataColumns.MSPAFile);
					}
					break;

				case MSPAFileEventDataModel.DataColumns.TradingEventTypeId:
					if (data.TradingEventTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileEventDataModel.DataColumns.TradingEventTypeId, data.TradingEventTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventDataModel.DataColumns.TradingEventTypeId);
					}
					break;

				case MSPAFileEventDataModel.DataColumns.TradingEventType:
					if (!string.IsNullOrEmpty(data.TradingEventType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileEventDataModel.DataColumns.TradingEventType, data.TradingEventType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventDataModel.DataColumns.TradingEventType);
					}
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<MSPAFileEventDataModel> GetEntityDetails(MSPAFileEventDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MSPAFileEventSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	MSPAFileEventId           = dataQuery.MSPAFileEventId
				 ,	Description           = dataQuery.Description
				 ,	MSPAFileId           = dataQuery.MSPAFileId
				 ,	TradingEventTypeId           = dataQuery.TradingEventTypeId
			};

			List<MSPAFileEventDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MSPAFileEventDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<MSPAFileEventDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(MSPAFileEventDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(MSPAFileEventDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(MSPAFileEventDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.MSPAFileEventInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MSPAFileEventUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventDataModel.DataColumns.MSPAFileEventId); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventDataModel.DataColumns.CreatedBy); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventDataModel.DataColumns.CreatedOn); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventDataModel.DataColumns.MSPAFileId); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventDataModel.DataColumns.TradingEventTypeId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(MSPAFileEventDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("MSPAFileEvent.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(MSPAFileEventDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("MSPAFileEvent.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MSPAFileEventDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.MSPAFileEventDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   MSPAFileEventId  = data.MSPAFileEventId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(MSPAFileEventDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MSPAFileEventDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.MSPAFileId  = data.MSPAFileId;
			doesExistRequest.TradingEventTypeId  = data.TradingEventTypeId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
