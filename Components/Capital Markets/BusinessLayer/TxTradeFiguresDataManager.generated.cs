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
	public partial class TxTradeFiguresDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TxTradeFiguresDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TxTradeFigures");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TxTradeFiguresDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TxTradeFiguresDataModel.DataColumns.TxTradeFiguresId:
					if (data.TxTradeFiguresId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeFiguresDataModel.DataColumns.TxTradeFiguresId, data.TxTradeFiguresId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.TxTradeFiguresId);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.TransactionEventId:
					if (data.TransactionEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeFiguresDataModel.DataColumns.TransactionEventId, data.TransactionEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.TransactionEventId);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.TransactionEvent:
					if (!string.IsNullOrEmpty(data.TransactionEvent))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.TransactionEvent, data.TransactionEvent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.TransactionEvent);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.Quantity:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeFiguresDataModel.DataColumns.Quantity, data.Quantity);
					break;

				case TxTradeFiguresDataModel.DataColumns.Price:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeFiguresDataModel.DataColumns.Price, data.Price);
					break;

				case TxTradeFiguresDataModel.DataColumns.TotalCommission:
					if (!string.IsNullOrEmpty(data.TotalCommission))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.TotalCommission, data.TotalCommission);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.TotalCommission);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.BrokerCodeId:
					if (data.BrokerCodeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeFiguresDataModel.DataColumns.BrokerCodeId, data.BrokerCodeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.BrokerCodeId);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.GlobalFacilityAmount:
					if (!string.IsNullOrEmpty(data.GlobalFacilityAmount))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.GlobalFacilityAmount, data.GlobalFacilityAmount);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.GlobalFacilityAmount);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.ExemptUnrealizedPLfromCapitalRatios:
					if (!string.IsNullOrEmpty(data.ExemptUnrealizedPLfromCapitalRatios))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.ExemptUnrealizedPLfromCapitalRatios, data.ExemptUnrealizedPLfromCapitalRatios);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.ExemptUnrealizedPLfromCapitalRatios);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.InternalTradeExcludeInByPassStrategy:
					if (!string.IsNullOrEmpty(data.InternalTradeExcludeInByPassStrategy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.InternalTradeExcludeInByPassStrategy, data.InternalTradeExcludeInByPassStrategy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.InternalTradeExcludeInByPassStrategy);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.ForwardFXBookCurrencyPricing:
					if (!string.IsNullOrEmpty(data.ForwardFXBookCurrencyPricing))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.ForwardFXBookCurrencyPricing, data.ForwardFXBookCurrencyPricing);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.ForwardFXBookCurrencyPricing);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.OriginalFace:
					if (!string.IsNullOrEmpty(data.OriginalFace))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.OriginalFace, data.OriginalFace);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.OriginalFace);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.IndexRatio:
					if (!string.IsNullOrEmpty(data.IndexRatio))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.IndexRatio, data.IndexRatio);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.IndexRatio);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.PerShareAmount:
					if (!string.IsNullOrEmpty(data.PerShareAmount))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.PerShareAmount, data.PerShareAmount);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.PerShareAmount);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.OpeningRate:
					if (!string.IsNullOrEmpty(data.OpeningRate))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.OpeningRate, data.OpeningRate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.OpeningRate);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.PercentageOwned:
					if (!string.IsNullOrEmpty(data.PercentageOwned))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.PercentageOwned, data.PercentageOwned);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.PercentageOwned);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.DelayedCompensationId:
					if (data.DelayedCompensationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeFiguresDataModel.DataColumns.DelayedCompensationId, data.DelayedCompensationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.DelayedCompensationId);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.ReceiveFinancing:
					if (!string.IsNullOrEmpty(data.ReceiveFinancing))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.ReceiveFinancing, data.ReceiveFinancing);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.ReceiveFinancing);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.Yield:
					if (!string.IsNullOrEmpty(data.Yield))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.Yield, data.Yield);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.Yield);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.NotionalAmount:
					if (!string.IsNullOrEmpty(data.NotionalAmount))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.NotionalAmount, data.NotionalAmount);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.NotionalAmount);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.TradesAsId:
					if (data.TradesAsId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeFiguresDataModel.DataColumns.TradesAsId, data.TradesAsId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.TradesAsId);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.DirtyPrice:
					if (!string.IsNullOrEmpty(data.DirtyPrice))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.DirtyPrice, data.DirtyPrice);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.DirtyPrice);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.TradesFlat:
					if (!string.IsNullOrEmpty(data.TradesFlat))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.TradesFlat, data.TradesFlat);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.TradesFlat);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.RestateUnrealizedGainOrLossAtPeriodEndSpotRate:
					if (!string.IsNullOrEmpty(data.RestateUnrealizedGainOrLossAtPeriodEndSpotRate))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.RestateUnrealizedGainOrLossAtPeriodEndSpotRate, data.RestateUnrealizedGainOrLossAtPeriodEndSpotRate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.RestateUnrealizedGainOrLossAtPeriodEndSpotRate);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.OverridingFinancingId:
					if (data.OverridingFinancingId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeFiguresDataModel.DataColumns.OverridingFinancingId, data.OverridingFinancingId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.OverridingFinancingId);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.AccrueCommission:
					if (!string.IsNullOrEmpty(data.AccrueCommission))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.AccrueCommission, data.AccrueCommission);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.AccrueCommission);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.EffectiveYield:
					if (!string.IsNullOrEmpty(data.EffectiveYield))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.EffectiveYield, data.EffectiveYield);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.EffectiveYield);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.NetTrade:
					if (!string.IsNullOrEmpty(data.NetTrade))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.NetTrade, data.NetTrade);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.NetTrade);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.PayOrReceiveFullCoupon:
					if (!string.IsNullOrEmpty(data.PayOrReceiveFullCoupon))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.PayOrReceiveFullCoupon, data.PayOrReceiveFullCoupon);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.PayOrReceiveFullCoupon);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.ExpirationDate:
					if (!string.IsNullOrEmpty(data.ExpirationDate))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.ExpirationDate, data.ExpirationDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.ExpirationDate);
					}
					break;

				case TxTradeFiguresDataModel.DataColumns.SweepCashOnSettlementDate:
					if (!string.IsNullOrEmpty(data.SweepCashOnSettlementDate))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeFiguresDataModel.DataColumns.SweepCashOnSettlementDate, data.SweepCashOnSettlementDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeFiguresDataModel.DataColumns.SweepCashOnSettlementDate);
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

		public static List<TxTradeFiguresDataModel> GetEntityDetails(TxTradeFiguresDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TxTradeFiguresSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TxTradeFiguresId           = dataQuery.TxTradeFiguresId
				 ,	TransactionEventId           = dataQuery.TransactionEventId
			};

			List<TxTradeFiguresDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TxTradeFiguresDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TxTradeFiguresDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TxTradeFiguresDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TxTradeFiguresDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TxTradeFiguresDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TxTradeFiguresInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TxTradeFiguresUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.TxTradeFiguresId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.TransactionEventId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.Quantity); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.Price); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.TotalCommission); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.BrokerCodeId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.GlobalFacilityAmount); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.ExemptUnrealizedPLfromCapitalRatios); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.InternalTradeExcludeInByPassStrategy); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.ForwardFXBookCurrencyPricing); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.OriginalFace); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.IndexRatio); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.PerShareAmount); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.OpeningRate); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.PercentageOwned); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.DelayedCompensationId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.ReceiveFinancing); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.Yield); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.NotionalAmount); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.TradesAsId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.DirtyPrice); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.TradesFlat); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.RestateUnrealizedGainOrLossAtPeriodEndSpotRate); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.OverridingFinancingId); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.AccrueCommission); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.EffectiveYield); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.NetTrade); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.PayOrReceiveFullCoupon); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.ExpirationDate); 
			sql = sql + ", " + ToSQLParameter(data, TxTradeFiguresDataModel.DataColumns.SweepCashOnSettlementDate); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TxTradeFiguresDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TxTradeFigures.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TxTradeFiguresDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TxTradeFigures.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TxTradeFiguresDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TxTradeFiguresDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TxTradeFiguresId  = data.TxTradeFiguresId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TxTradeFiguresDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TxTradeFiguresDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.TransactionEventId  = data.TransactionEventId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
