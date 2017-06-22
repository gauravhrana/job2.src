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
	public partial class MSPAFileDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static MSPAFileDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MSPAFile");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(MSPAFileDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case MSPAFileDataModel.DataColumns.MSPAFileId:
					if (data.MSPAFileId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileDataModel.DataColumns.MSPAFileId, data.MSPAFileId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileDataModel.DataColumns.MSPAFileId);
					}
					break;

				case MSPAFileDataModel.DataColumns.Filename:
					if (!string.IsNullOrEmpty(data.Filename))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileDataModel.DataColumns.Filename, data.Filename);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileDataModel.DataColumns.Filename);
					}
					break;

				case MSPAFileDataModel.DataColumns.DropDate:
					if (data.DropDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileDataModel.DataColumns.DropDate, data.DropDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileDataModel.DataColumns.DropDate);
					}
					break;

				case MSPAFileDataModel.DataColumns.BusinessDate:
					if (data.BusinessDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileDataModel.DataColumns.BusinessDate, data.BusinessDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileDataModel.DataColumns.BusinessDate);
					}
					break;

				case MSPAFileDataModel.DataColumns.MSPAExtractTaskRunId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileDataModel.DataColumns.MSPAExtractTaskRunId, data.MSPAExtractTaskRunId);
					break;

				case MSPAFileDataModel.DataColumns.MSPAHoldingTaskRunId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileDataModel.DataColumns.MSPAHoldingTaskRunId, data.MSPAHoldingTaskRunId);
					break;

				case MSPAFileDataModel.DataColumns.MSPATradeTaskRunId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileDataModel.DataColumns.MSPATradeTaskRunId, data.MSPATradeTaskRunId);
					break;

				case MSPAFileDataModel.DataColumns.MSPASecurityTaskRunId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileDataModel.DataColumns.MSPASecurityTaskRunId, data.MSPASecurityTaskRunId);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<MSPAFileDataModel> GetEntityDetails(MSPAFileDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MSPAFileSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	MSPAFileId           = dataQuery.MSPAFileId
				 ,	Filename           = dataQuery.Filename
			};

			List<MSPAFileDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MSPAFileDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<MSPAFileDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(MSPAFileDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(MSPAFileDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(MSPAFileDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.MSPAFileInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MSPAFileUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, MSPAFileDataModel.DataColumns.MSPAFileId); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileDataModel.DataColumns.Filename); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileDataModel.DataColumns.DropDate); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileDataModel.DataColumns.BusinessDate); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileDataModel.DataColumns.MSPAExtractTaskRunId); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileDataModel.DataColumns.MSPAHoldingTaskRunId); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileDataModel.DataColumns.MSPATradeTaskRunId); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileDataModel.DataColumns.MSPASecurityTaskRunId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(MSPAFileDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("MSPAFile.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(MSPAFileDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("MSPAFile.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MSPAFileDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.MSPAFileDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   MSPAFileId  = data.MSPAFileId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(MSPAFileDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MSPAFileDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
