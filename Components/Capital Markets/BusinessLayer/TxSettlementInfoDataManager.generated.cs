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
	public partial class TxSettlementInfoDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TxSettlementInfoDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TxSettlementInfo");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TxSettlementInfoDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TxSettlementInfoDataModel.DataColumns.TxSettlementInfoId:
					if (data.TxSettlementInfoId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxSettlementInfoDataModel.DataColumns.TxSettlementInfoId, data.TxSettlementInfoId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.TxSettlementInfoId);
					}
					break;

				case TxSettlementInfoDataModel.DataColumns.TransactionEventId:
					if (data.TransactionEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxSettlementInfoDataModel.DataColumns.TransactionEventId, data.TransactionEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.TransactionEventId);
					}
					break;

				case TxSettlementInfoDataModel.DataColumns.TransactionEvent:
					if (!string.IsNullOrEmpty(data.TransactionEvent))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxSettlementInfoDataModel.DataColumns.TransactionEvent, data.TransactionEvent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.TransactionEvent);
					}
					break;

				case TxSettlementInfoDataModel.DataColumns.SettlementCurrencyId:
					if (data.SettlementCurrencyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxSettlementInfoDataModel.DataColumns.SettlementCurrencyId, data.SettlementCurrencyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.SettlementCurrencyId);
					}
					break;

				case TxSettlementInfoDataModel.DataColumns.SellCurrencyId:
					if (data.SellCurrencyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxSettlementInfoDataModel.DataColumns.SellCurrencyId, data.SellCurrencyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.SellCurrencyId);
					}
					break;

				case TxSettlementInfoDataModel.DataColumns.TradeDateFXRate:
					if (!string.IsNullOrEmpty(data.TradeDateFXRate))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxSettlementInfoDataModel.DataColumns.TradeDateFXRate, data.TradeDateFXRate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.TradeDateFXRate);
					}
					break;

				case TxSettlementInfoDataModel.DataColumns.NetSettlementAmount:
					if (!string.IsNullOrEmpty(data.NetSettlementAmount))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxSettlementInfoDataModel.DataColumns.NetSettlementAmount, data.NetSettlementAmount);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.NetSettlementAmount);
					}
					break;

				case TxSettlementInfoDataModel.DataColumns.NetSettlementCashAmount:
					if (!string.IsNullOrEmpty(data.NetSettlementCashAmount))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxSettlementInfoDataModel.DataColumns.NetSettlementCashAmount, data.NetSettlementCashAmount);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.NetSettlementCashAmount);
					}
					break;

				case TxSettlementInfoDataModel.DataColumns.AccruedInterest:
					if (!string.IsNullOrEmpty(data.AccruedInterest))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxSettlementInfoDataModel.DataColumns.AccruedInterest, data.AccruedInterest);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxSettlementInfoDataModel.DataColumns.AccruedInterest);
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

		public static List<TxSettlementInfoDataModel> GetEntityDetails(TxSettlementInfoDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TxSettlementInfoSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TxSettlementInfoId           = dataQuery.TxSettlementInfoId
				 ,	TransactionEventId           = dataQuery.TransactionEventId
			};

			List<TxSettlementInfoDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TxSettlementInfoDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TxSettlementInfoDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TxSettlementInfoDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TxSettlementInfoDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TxSettlementInfoDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TxSettlementInfoInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TxSettlementInfoUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TxSettlementInfoDataModel.DataColumns.TxSettlementInfoId); 
			sql = sql + ", " + ToSQLParameter(data, TxSettlementInfoDataModel.DataColumns.TransactionEventId); 
			sql = sql + ", " + ToSQLParameter(data, TxSettlementInfoDataModel.DataColumns.SettlementCurrencyId); 
			sql = sql + ", " + ToSQLParameter(data, TxSettlementInfoDataModel.DataColumns.SellCurrencyId); 
			sql = sql + ", " + ToSQLParameter(data, TxSettlementInfoDataModel.DataColumns.TradeDateFXRate); 
			sql = sql + ", " + ToSQLParameter(data, TxSettlementInfoDataModel.DataColumns.NetSettlementAmount); 
			sql = sql + ", " + ToSQLParameter(data, TxSettlementInfoDataModel.DataColumns.NetSettlementCashAmount); 
			sql = sql + ", " + ToSQLParameter(data, TxSettlementInfoDataModel.DataColumns.AccruedInterest); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TxSettlementInfoDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TxSettlementInfo.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TxSettlementInfoDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TxSettlementInfo.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TxSettlementInfoDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TxSettlementInfoDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TxSettlementInfoId  = data.TxSettlementInfoId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TxSettlementInfoDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TxSettlementInfoDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.TransactionEventId  = data.TransactionEventId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
