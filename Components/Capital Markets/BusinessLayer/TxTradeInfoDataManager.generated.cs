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
	public partial class TxTradeInfoDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TxTradeInfoDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TxTradeInfo");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TxTradeInfoDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TxTradeInfoDataModel.DataColumns.TxTradeInfoId:
					if (data.TxTradeInfoId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeInfoDataModel.DataColumns.TxTradeInfoId, data.TxTradeInfoId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeInfoDataModel.DataColumns.TxTradeInfoId);
					}
					break;

				case TxTradeInfoDataModel.DataColumns.TransactionEventId:
					if (data.TransactionEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeInfoDataModel.DataColumns.TransactionEventId, data.TransactionEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeInfoDataModel.DataColumns.TransactionEventId);
					}
					break;

				case TxTradeInfoDataModel.DataColumns.TransactionEvent:
					if (!string.IsNullOrEmpty(data.TransactionEvent))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeInfoDataModel.DataColumns.TransactionEvent, data.TransactionEvent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeInfoDataModel.DataColumns.TransactionEvent);
					}
					break;

				case TxTradeInfoDataModel.DataColumns.TradeCurrencyId:
					if (data.TradeCurrencyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeInfoDataModel.DataColumns.TradeCurrencyId, data.TradeCurrencyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeInfoDataModel.DataColumns.TradeCurrencyId);
					}
					break;

				case TxTradeInfoDataModel.DataColumns.BuyCurrencyId:
					if (data.BuyCurrencyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeInfoDataModel.DataColumns.BuyCurrencyId, data.BuyCurrencyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeInfoDataModel.DataColumns.BuyCurrencyId);
					}
					break;

				case TxTradeInfoDataModel.DataColumns.CrossSettlementFXRate:
					if (!string.IsNullOrEmpty(data.CrossSettlementFXRate))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeInfoDataModel.DataColumns.CrossSettlementFXRate, data.CrossSettlementFXRate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeInfoDataModel.DataColumns.CrossSettlementFXRate);
					}
					break;

				case TxTradeInfoDataModel.DataColumns.NetTradeAmount:
					if (!string.IsNullOrEmpty(data.NetTradeAmount))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeInfoDataModel.DataColumns.NetTradeAmount, data.NetTradeAmount);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeInfoDataModel.DataColumns.NetTradeAmount);
					}
					break;

				case TxTradeInfoDataModel.DataColumns.TradeAccruedInterest:
					if (!string.IsNullOrEmpty(data.TradeAccruedInterest))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeInfoDataModel.DataColumns.TradeAccruedInterest, data.TradeAccruedInterest);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeInfoDataModel.DataColumns.TradeAccruedInterest);
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

		public static List<TxTradeInfoDataModel> GetEntityDetails(TxTradeInfoDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TxTradeInfoSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TxTradeInfoId           = dataQuery.TxTradeInfoId
				 ,	TransactionEventId           = dataQuery.TransactionEventId
			};

			List<TxTradeInfoDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TxTradeInfoDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TxTradeInfoDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TxTradeInfoDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TxTradeInfoDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TxTradeInfoDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TxTradeInfoInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TxTradeInfoUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TxTradeInfoDataModel.DataColumns.TxTradeInfoId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeInfoDataModel.DataColumns.TransactionEventId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeInfoDataModel.DataColumns.TradeCurrencyId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeInfoDataModel.DataColumns.BuyCurrencyId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeInfoDataModel.DataColumns.CrossSettlementFXRate); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeInfoDataModel.DataColumns.NetTradeAmount); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeInfoDataModel.DataColumns.TradeAccruedInterest); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TxTradeInfoDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TxTradeInfo.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TxTradeInfoDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TxTradeInfo.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TxTradeInfoDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TxTradeInfoDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TxTradeInfoId  = data.TxTradeInfoId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TxTradeInfoDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TxTradeInfoDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.TransactionEventId  = data.TransactionEventId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
