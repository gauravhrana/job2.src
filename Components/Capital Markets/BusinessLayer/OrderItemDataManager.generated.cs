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
	public partial class OrderItemDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static OrderItemDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("OrderItem");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(OrderItemDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case OrderItemDataModel.DataColumns.OrderItemId:
					if (data.OrderItemId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.OrderItemId, data.OrderItemId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.OrderItemId);
					}
					break;

				case OrderItemDataModel.DataColumns.OrderId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.OrderId, data.OrderId);
					break;

				case OrderItemDataModel.DataColumns.SecurityCode:
					if (!string.IsNullOrEmpty(data.SecurityCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.SecurityCode, data.SecurityCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.SecurityCode);
					}
					break;

				case OrderItemDataModel.DataColumns.Quantity:
					if (data.Quantity != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.Quantity, data.Quantity);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.Quantity);
					}
					break;

				case OrderItemDataModel.DataColumns.QuantityFilled:
					if (data.QuantityFilled != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.QuantityFilled, data.QuantityFilled);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.QuantityFilled);
					}
					break;

				case OrderItemDataModel.DataColumns.QuantityOriginal:
					if (data.QuantityOriginal != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.QuantityOriginal, data.QuantityOriginal);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.QuantityOriginal);
					}
					break;

				case OrderItemDataModel.DataColumns.PriceLimit:
					if (data.PriceLimit != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.PriceLimit, data.PriceLimit);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.PriceLimit);
					}
					break;

				case OrderItemDataModel.DataColumns.StrategyCode:
					if (!string.IsNullOrEmpty(data.StrategyCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.StrategyCode, data.StrategyCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.StrategyCode);
					}
					break;

				case OrderItemDataModel.DataColumns.StrategyGroupCode:
					if (!string.IsNullOrEmpty(data.StrategyGroupCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.StrategyGroupCode, data.StrategyGroupCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.StrategyGroupCode);
					}
					break;

				case OrderItemDataModel.DataColumns.ClassificationCode:
					if (!string.IsNullOrEmpty(data.ClassificationCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.ClassificationCode, data.ClassificationCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.ClassificationCode);
					}
					break;

				case OrderItemDataModel.DataColumns.BbergCode:
					if (!string.IsNullOrEmpty(data.BbergCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.BbergCode, data.BbergCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.BbergCode);
					}
					break;

				case OrderItemDataModel.DataColumns.Notes:
					if (!string.IsNullOrEmpty(data.Notes))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.Notes, data.Notes);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.Notes);
					}
					break;

				case OrderItemDataModel.DataColumns.AvgPrice:
					if (data.AvgPrice != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.AvgPrice, data.AvgPrice);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.AvgPrice);
					}
					break;

				case OrderItemDataModel.DataColumns.RefPrice:
					if (data.RefPrice != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.RefPrice, data.RefPrice);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.RefPrice);
					}
					break;

				case OrderItemDataModel.DataColumns.TargetBps:
					if (data.TargetBps != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.TargetBps, data.TargetBps);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.TargetBps);
					}
					break;

				case OrderItemDataModel.DataColumns.AutoGeneratedNotes:
					if (!string.IsNullOrEmpty(data.AutoGeneratedNotes))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.AutoGeneratedNotes, data.AutoGeneratedNotes);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.AutoGeneratedNotes);
					}
					break;

				case OrderItemDataModel.DataColumns.AutoPercentTraded:
					if (data.AutoPercentTraded != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.AutoPercentTraded, data.AutoPercentTraded);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.AutoPercentTraded);
					}
					break;

				case OrderItemDataModel.DataColumns.PositionSizeChange:
					if (data.PositionSizeChange != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.PositionSizeChange, data.PositionSizeChange);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.PositionSizeChange);
					}
					break;

				case OrderItemDataModel.DataColumns.SubmissionType:
					if (!string.IsNullOrEmpty(data.SubmissionType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.SubmissionType, data.SubmissionType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.SubmissionType);
					}
					break;

				case OrderItemDataModel.DataColumns.PrimaryBrokerCode:
					if (!string.IsNullOrEmpty(data.PrimaryBrokerCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.PrimaryBrokerCode, data.PrimaryBrokerCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.PrimaryBrokerCode);
					}
					break;

				case OrderItemDataModel.DataColumns.ExecutingBrokerCode:
					if (!string.IsNullOrEmpty(data.ExecutingBrokerCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.ExecutingBrokerCode, data.ExecutingBrokerCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.ExecutingBrokerCode);
					}
					break;

				case OrderItemDataModel.DataColumns.RoutingDestination:
					if (!string.IsNullOrEmpty(data.RoutingDestination))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.RoutingDestination, data.RoutingDestination);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.RoutingDestination);
					}
					break;

				case OrderItemDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.Description);
					}
					break;

				case OrderItemDataModel.DataColumns.TotalOrderPercent:
					if (data.TotalOrderPercent != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.TotalOrderPercent, data.TotalOrderPercent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.TotalOrderPercent);
					}
					break;

				case OrderItemDataModel.DataColumns.EventDate:
					if (data.EventDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.EventDate, data.EventDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.EventDate);
					}
					break;

				case OrderItemDataModel.DataColumns.LastModifiedOn:
					if (data.LastModifiedOn != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.LastModifiedOn, data.LastModifiedOn);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.LastModifiedOn);
					}
					break;

				case OrderItemDataModel.DataColumns.ExpireOn:
					if (data.ExpireOn != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.ExpireOn, data.ExpireOn);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.ExpireOn);
					}
					break;

				case OrderItemDataModel.DataColumns.AutoOrderResultTypeId:
					if (data.AutoOrderResultTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.AutoOrderResultTypeId, data.AutoOrderResultTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.AutoOrderResultTypeId);
					}
					break;

				case OrderItemDataModel.DataColumns.BbergUniqueId:
					if (data.BbergUniqueId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.BbergUniqueId, data.BbergUniqueId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.BbergUniqueId);
					}
					break;

				case OrderItemDataModel.DataColumns.LastOrderStatusId:
					if (data.LastOrderStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.LastOrderStatusId, data.LastOrderStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.LastOrderStatusId);
					}
					break;

				case OrderItemDataModel.DataColumns.RefFxRate:
					if (data.RefFxRate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.RefFxRate, data.RefFxRate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.RefFxRate);
					}
					break;

				case OrderItemDataModel.DataColumns.OrderRequestId:
					if (data.OrderRequestId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.OrderRequestId, data.OrderRequestId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.OrderRequestId);
					}
					break;

				case OrderItemDataModel.DataColumns.OrderRequest:
					if (!string.IsNullOrEmpty(data.OrderRequest))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.OrderRequest, data.OrderRequest);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.OrderRequest);
					}
					break;

				case OrderItemDataModel.DataColumns.OrderActionId:
					if (data.OrderActionId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.OrderActionId, data.OrderActionId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.OrderActionId);
					}
					break;

				case OrderItemDataModel.DataColumns.OrderAction:
					if (!string.IsNullOrEmpty(data.OrderAction))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.OrderAction, data.OrderAction);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.OrderAction);
					}
					break;

				case OrderItemDataModel.DataColumns.OrderTypeId:
					if (data.OrderTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.OrderTypeId, data.OrderTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.OrderTypeId);
					}
					break;

				case OrderItemDataModel.DataColumns.OrderType:
					if (!string.IsNullOrEmpty(data.OrderType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.OrderType, data.OrderType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.OrderType);
					}
					break;

				case OrderItemDataModel.DataColumns.StrategyId:
					if (data.StrategyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.StrategyId, data.StrategyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.StrategyId);
					}
					break;

				case OrderItemDataModel.DataColumns.Strategy:
					if (!string.IsNullOrEmpty(data.Strategy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.Strategy, data.Strategy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.Strategy);
					}
					break;

				case OrderItemDataModel.DataColumns.SecurityId:
					if (data.SecurityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.SecurityId, data.SecurityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.SecurityId);
					}
					break;

				case OrderItemDataModel.DataColumns.Security:
					if (!string.IsNullOrEmpty(data.Security))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.Security, data.Security);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.Security);
					}
					break;

				case OrderItemDataModel.DataColumns.PortfolioId:
					if (data.PortfolioId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderItemDataModel.DataColumns.PortfolioId, data.PortfolioId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.PortfolioId);
					}
					break;

				case OrderItemDataModel.DataColumns.Portfolio:
					if (!string.IsNullOrEmpty(data.Portfolio))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderItemDataModel.DataColumns.Portfolio, data.Portfolio);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderItemDataModel.DataColumns.Portfolio);
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

		public static List<OrderItemDataModel> GetEntityDetails(OrderItemDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.OrderItemSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	OrderItemId           = dataQuery.OrderItemId
				 ,	OrderId           = dataQuery.OrderId
				 ,	OrderRequestId           = dataQuery.OrderRequestId
				 ,	OrderActionId           = dataQuery.OrderActionId
				 ,	OrderTypeId           = dataQuery.OrderTypeId
				 ,	StrategyId           = dataQuery.StrategyId
				 ,	SecurityId           = dataQuery.SecurityId
				 ,	PortfolioId           = dataQuery.PortfolioId
			};

			List<OrderItemDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<OrderItemDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<OrderItemDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(OrderItemDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(OrderItemDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(OrderItemDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.OrderItemInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.OrderItemUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.OrderItemId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.OrderId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.SecurityCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.Quantity); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.QuantityFilled); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.QuantityOriginal); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.PriceLimit); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.StrategyCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.StrategyGroupCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.ClassificationCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.BbergCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.Notes); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.AvgPrice); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.RefPrice); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.TargetBps); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.AutoGeneratedNotes); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.AutoPercentTraded); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.PositionSizeChange); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.SubmissionType); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.PrimaryBrokerCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.ExecutingBrokerCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.RoutingDestination); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.TotalOrderPercent); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.EventDate); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.LastModifiedOn); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.ExpireOn); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.AutoOrderResultTypeId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.BbergUniqueId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.LastOrderStatusId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.RefFxRate); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.OrderRequestId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.OrderActionId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.OrderTypeId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.StrategyId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.SecurityId); 
			sql = sql + ", " + ToSQLParameter(data, OrderItemDataModel.DataColumns.PortfolioId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(OrderItemDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("OrderItem.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(OrderItemDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("OrderItem.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(OrderItemDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.OrderItemDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   OrderItemId  = data.OrderItemId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(OrderItemDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new OrderItemDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.OrderRequestId  = data.OrderRequestId;
			doesExistRequest.OrderActionId  = data.OrderActionId;
			doesExistRequest.OrderTypeId  = data.OrderTypeId;
			doesExistRequest.StrategyId  = data.StrategyId;
			doesExistRequest.SecurityId  = data.SecurityId;
			doesExistRequest.PortfolioId  = data.PortfolioId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
