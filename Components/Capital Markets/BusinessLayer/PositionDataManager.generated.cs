using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class PositionDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PositionDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Position");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PositionDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PositionDataModel.DataColumns.PositionId:
					if (data.PositionId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.PositionId, data.PositionId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.PositionId);
					}
					break;

				case PositionDataModel.DataColumns.InvestmentCode:
					if (!string.IsNullOrEmpty(data.InvestmentCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PositionDataModel.DataColumns.InvestmentCode, data.InvestmentCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.InvestmentCode);
					}
					break;

				case PositionDataModel.DataColumns.PeriodDate:
					if (data.PeriodDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PositionDataModel.DataColumns.PeriodDate, data.PeriodDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.PeriodDate);
					}
					break;

				case PositionDataModel.DataColumns.CustodianCode:
					if (!string.IsNullOrEmpty(data.CustodianCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PositionDataModel.DataColumns.CustodianCode, data.CustodianCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.CustodianCode);
					}
					break;

				case PositionDataModel.DataColumns.StrategyCode:
					if (!string.IsNullOrEmpty(data.StrategyCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PositionDataModel.DataColumns.StrategyCode, data.StrategyCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.StrategyCode);
					}
					break;

				case PositionDataModel.DataColumns.AccountCode:
					if (!string.IsNullOrEmpty(data.AccountCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PositionDataModel.DataColumns.AccountCode, data.AccountCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.AccountCode);
					}
					break;

				case PositionDataModel.DataColumns.Quantity:
					if (data.Quantity != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.Quantity, data.Quantity);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.Quantity);
					}
					break;

				case PositionDataModel.DataColumns.CostBasis:
					if (data.CostBasis != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.CostBasis, data.CostBasis);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.CostBasis);
					}
					break;

				case PositionDataModel.DataColumns.MarketValue:
					if (data.MarketValue != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.MarketValue, data.MarketValue);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.MarketValue);
					}
					break;

				case PositionDataModel.DataColumns.StartMarketValue:
					if (data.StartMarketValue != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.StartMarketValue, data.StartMarketValue);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.StartMarketValue);
					}
					break;

				case PositionDataModel.DataColumns.DeltaAdjustedExposure:
					if (data.DeltaAdjustedExposure != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.DeltaAdjustedExposure, data.DeltaAdjustedExposure);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.DeltaAdjustedExposure);
					}
					break;

				case PositionDataModel.DataColumns.StartDeltaAdjustedExposure:
					if (data.StartDeltaAdjustedExposure != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.StartDeltaAdjustedExposure, data.StartDeltaAdjustedExposure);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.StartDeltaAdjustedExposure);
					}
					break;

				case PositionDataModel.DataColumns.RealizedPnL:
					if (data.RealizedPnL != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.RealizedPnL, data.RealizedPnL);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.RealizedPnL);
					}
					break;

				case PositionDataModel.DataColumns.UnrealizedPnL:
					if (data.UnrealizedPnL != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.UnrealizedPnL, data.UnrealizedPnL);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PositionDataModel.DataColumns.UnrealizedPnL);
					}
					break;

				case PositionDataModel.DataColumns.Mark:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PositionDataModel.DataColumns.Mark, data.Mark);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<PositionDataModel> GetEntityDetails(PositionDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PositionSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PositionId           = dataQuery.PositionId
			};

			List<PositionDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PositionDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PositionDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PositionDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PositionDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PositionDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PositionInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PositionUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.PositionId); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.InvestmentCode); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.PeriodDate); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.CustodianCode); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.StrategyCode); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.AccountCode); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.Quantity); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.CostBasis); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.MarketValue); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.StartMarketValue); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.DeltaAdjustedExposure); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.StartDeltaAdjustedExposure); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.RealizedPnL); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.UnrealizedPnL); 
			sql = sql + ", " + ToSQLParameter(data, PositionDataModel.DataColumns.Mark); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PositionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Position.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PositionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Position.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PositionDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PositionDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PositionId  = data.PositionId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PositionDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PositionDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
