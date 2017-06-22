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
	public partial class CommissionRateDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CommissionRateDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CommissionRate");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CommissionRateDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CommissionRateDataModel.DataColumns.CommissionRateId:
					if (data.CommissionRateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionRateDataModel.DataColumns.CommissionRateId, data.CommissionRateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionRateDataModel.DataColumns.CommissionRateId);
					}
					break;

				case CommissionRateDataModel.DataColumns.ClearingRate:
					if (data.ClearingRate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionRateDataModel.DataColumns.ClearingRate, data.ClearingRate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionRateDataModel.DataColumns.ClearingRate);
					}
					break;

				case CommissionRateDataModel.DataColumns.ExecutionRate:
					if (data.ExecutionRate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionRateDataModel.DataColumns.ExecutionRate, data.ExecutionRate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionRateDataModel.DataColumns.ExecutionRate);
					}
					break;

				case CommissionRateDataModel.DataColumns.BrokerId:
					if (data.BrokerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionRateDataModel.DataColumns.BrokerId, data.BrokerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionRateDataModel.DataColumns.BrokerId);
					}
					break;

				case CommissionRateDataModel.DataColumns.Broker:
					if (!string.IsNullOrEmpty(data.Broker))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionRateDataModel.DataColumns.Broker, data.Broker);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionRateDataModel.DataColumns.Broker);
					}
					break;

				case CommissionRateDataModel.DataColumns.ExchangeId:
					if (data.ExchangeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionRateDataModel.DataColumns.ExchangeId, data.ExchangeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionRateDataModel.DataColumns.ExchangeId);
					}
					break;

				case CommissionRateDataModel.DataColumns.Exchange:
					if (!string.IsNullOrEmpty(data.Exchange))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionRateDataModel.DataColumns.Exchange, data.Exchange);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionRateDataModel.DataColumns.Exchange);
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

		public static List<CommissionRateDataModel> GetEntityDetails(CommissionRateDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CommissionRateSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CommissionRateId           = dataQuery.CommissionRateId
				 ,	BrokerId           = dataQuery.BrokerId
				 ,	ExchangeId           = dataQuery.ExchangeId
			};

			List<CommissionRateDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CommissionRateDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CommissionRateDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CommissionRateDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CommissionRateDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CommissionRateDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CommissionRateInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CommissionRateUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CommissionRateDataModel.DataColumns.CommissionRateId); 
			sql = sql + ", " + ToSQLParameter(data, CommissionRateDataModel.DataColumns.ClearingRate); 
			sql = sql + ", " + ToSQLParameter(data, CommissionRateDataModel.DataColumns.ExecutionRate); 
			sql = sql + ", " + ToSQLParameter(data, CommissionRateDataModel.DataColumns.BrokerId); 
			sql = sql + ", " + ToSQLParameter(data, CommissionRateDataModel.DataColumns.ExchangeId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CommissionRateDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CommissionRate.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CommissionRateDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CommissionRate.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CommissionRateDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CommissionRateDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CommissionRateId  = data.CommissionRateId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CommissionRateDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CommissionRateDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.BrokerId  = data.BrokerId;
			doesExistRequest.ExchangeId  = data.ExchangeId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
