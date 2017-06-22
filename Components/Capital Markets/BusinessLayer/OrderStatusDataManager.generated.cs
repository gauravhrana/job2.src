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
	public partial class OrderStatusDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static OrderStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("OrderStatus");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(OrderStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case OrderStatusDataModel.DataColumns.OrderStatusId:
					if (data.OrderStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderStatusDataModel.DataColumns.OrderStatusId, data.OrderStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusDataModel.DataColumns.OrderStatusId);
					}
					break;

				case OrderStatusDataModel.DataColumns.OrderId:
					if (data.OrderId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderStatusDataModel.DataColumns.OrderId, data.OrderId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusDataModel.DataColumns.OrderId);
					}
					break;

				case OrderStatusDataModel.DataColumns.Comments:
					if (!string.IsNullOrEmpty(data.Comments))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusDataModel.DataColumns.Comments, data.Comments);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusDataModel.DataColumns.Comments);
					}
					break;

				case OrderStatusDataModel.DataColumns.LastModifiedBy:
					if (!string.IsNullOrEmpty(data.LastModifiedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusDataModel.DataColumns.LastModifiedBy, data.LastModifiedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusDataModel.DataColumns.LastModifiedBy);
					}
					break;

				case OrderStatusDataModel.DataColumns.LastModifiedOn:
					if (data.LastModifiedOn != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusDataModel.DataColumns.LastModifiedOn, data.LastModifiedOn);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusDataModel.DataColumns.LastModifiedOn);
					}
					break;

				case OrderStatusDataModel.DataColumns.OrderStatusTypeId:
					if (data.OrderStatusTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderStatusDataModel.DataColumns.OrderStatusTypeId, data.OrderStatusTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusDataModel.DataColumns.OrderStatusTypeId);
					}
					break;

				case OrderStatusDataModel.DataColumns.OrderStatusType:
					if (!string.IsNullOrEmpty(data.OrderStatusType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusDataModel.DataColumns.OrderStatusType, data.OrderStatusType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusDataModel.DataColumns.OrderStatusType);
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

		public static List<OrderStatusDataModel> GetEntityDetails(OrderStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.OrderStatusSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	OrderStatusId           = dataQuery.OrderStatusId
				 ,	OrderId           = dataQuery.OrderId
				 ,	Comments           = dataQuery.Comments
				 ,	OrderStatusTypeId           = dataQuery.OrderStatusTypeId
			};

			List<OrderStatusDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<OrderStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<OrderStatusDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(OrderStatusDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(OrderStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(OrderStatusDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.OrderStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.OrderStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, OrderStatusDataModel.DataColumns.OrderStatusId); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusDataModel.DataColumns.OrderId); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusDataModel.DataColumns.Comments); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusDataModel.DataColumns.LastModifiedBy); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusDataModel.DataColumns.LastModifiedOn); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusDataModel.DataColumns.OrderStatusTypeId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(OrderStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("OrderStatus.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(OrderStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("OrderStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(OrderStatusDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.OrderStatusDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   OrderStatusId  = data.OrderStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(OrderStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new OrderStatusDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.OrderStatusTypeId  = data.OrderStatusTypeId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
