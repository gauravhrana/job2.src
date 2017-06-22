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
	public partial class TxOtherDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TxOtherDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TxOther");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TxOtherDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TxOtherDataModel.DataColumns.TxOtherId:
					if (data.TxOtherId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxOtherDataModel.DataColumns.TxOtherId, data.TxOtherId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.TxOtherId);
					}
					break;

				case TxOtherDataModel.DataColumns.TransactionEventId:
					if (data.TransactionEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxOtherDataModel.DataColumns.TransactionEventId, data.TransactionEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.TransactionEventId);
					}
					break;

				case TxOtherDataModel.DataColumns.TransactionEvent:
					if (!string.IsNullOrEmpty(data.TransactionEvent))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxOtherDataModel.DataColumns.TransactionEvent, data.TransactionEvent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.TransactionEvent);
					}
					break;

				case TxOtherDataModel.DataColumns.FundStructureId:
					if (data.FundStructureId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxOtherDataModel.DataColumns.FundStructureId, data.FundStructureId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.FundStructureId);
					}
					break;

				case TxOtherDataModel.DataColumns.CashSourceId:
					if (data.CashSourceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxOtherDataModel.DataColumns.CashSourceId, data.CashSourceId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.CashSourceId);
					}
					break;

				case TxOtherDataModel.DataColumns.StrategyId:
					if (data.StrategyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxOtherDataModel.DataColumns.StrategyId, data.StrategyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.StrategyId);
					}
					break;

				case TxOtherDataModel.DataColumns.GenericLegId:
					if (data.GenericLegId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxOtherDataModel.DataColumns.GenericLegId, data.GenericLegId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.GenericLegId);
					}
					break;

				case TxOtherDataModel.DataColumns.DistributionParentId:
					if (data.DistributionParentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxOtherDataModel.DataColumns.DistributionParentId, data.DistributionParentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.DistributionParentId);
					}
					break;

				case TxOtherDataModel.DataColumns.SettlementTypeId:
					if (data.SettlementTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxOtherDataModel.DataColumns.SettlementTypeId, data.SettlementTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxOtherDataModel.DataColumns.SettlementTypeId);
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

		public static List<TxOtherDataModel> GetEntityDetails(TxOtherDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TxOtherSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TxOtherId           = dataQuery.TxOtherId
				 ,	TransactionEventId           = dataQuery.TransactionEventId
			};

			List<TxOtherDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TxOtherDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TxOtherDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TxOtherDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TxOtherDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TxOtherDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TxOtherInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TxOtherUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TxOtherDataModel.DataColumns.TxOtherId); 
			sql = sql + ", " + ToSQLParameter(data, TxOtherDataModel.DataColumns.TransactionEventId); 
			sql = sql + ", " + ToSQLParameter(data, TxOtherDataModel.DataColumns.FundStructureId); 
			sql = sql + ", " + ToSQLParameter(data, TxOtherDataModel.DataColumns.CashSourceId); 
			sql = sql + ", " + ToSQLParameter(data, TxOtherDataModel.DataColumns.StrategyId); 
			sql = sql + ", " + ToSQLParameter(data, TxOtherDataModel.DataColumns.GenericLegId); 
			sql = sql + ", " + ToSQLParameter(data, TxOtherDataModel.DataColumns.DistributionParentId); 
			sql = sql + ", " + ToSQLParameter(data, TxOtherDataModel.DataColumns.SettlementTypeId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TxOtherDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TxOther.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TxOtherDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TxOther.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TxOtherDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TxOtherDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TxOtherId  = data.TxOtherId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TxOtherDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TxOtherDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.TransactionEventId  = data.TransactionEventId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
