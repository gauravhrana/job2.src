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
	public partial class OrderStatusTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static OrderStatusTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("OrderStatusType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(OrderStatusTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case OrderStatusTypeDataModel.DataColumns.OrderStatusTypeId:
					if (data.OrderStatusTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderStatusTypeDataModel.DataColumns.OrderStatusTypeId, data.OrderStatusTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusTypeDataModel.DataColumns.OrderStatusTypeId);
					}
					break;

				case OrderStatusTypeDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusTypeDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusTypeDataModel.DataColumns.Code);
					}
					break;

				case OrderStatusTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusTypeDataModel.DataColumns.Description);
					}
					break;

				case OrderStatusTypeDataModel.DataColumns.OrderStatusGroupId:
					if (data.OrderStatusGroupId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderStatusTypeDataModel.DataColumns.OrderStatusGroupId, data.OrderStatusGroupId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusTypeDataModel.DataColumns.OrderStatusGroupId);
					}
					break;

				case OrderStatusTypeDataModel.DataColumns.OrderStatusGroup:
					if (!string.IsNullOrEmpty(data.OrderStatusGroup))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusTypeDataModel.DataColumns.OrderStatusGroup, data.OrderStatusGroup);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusTypeDataModel.DataColumns.OrderStatusGroup);
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

		public static List<OrderStatusTypeDataModel> GetEntityDetails(OrderStatusTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.OrderStatusTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	OrderStatusTypeId           = dataQuery.OrderStatusTypeId
				 ,	Code           = dataQuery.Code
				 ,	OrderStatusGroupId           = dataQuery.OrderStatusGroupId
			};

			List<OrderStatusTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<OrderStatusTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<OrderStatusTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(OrderStatusTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(OrderStatusTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(OrderStatusTypeDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(OrderStatusTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.OrderStatusTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.OrderStatusTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, OrderStatusTypeDataModel.DataColumns.OrderStatusTypeId); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusTypeDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusTypeDataModel.DataColumns.OrderStatusGroupId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(OrderStatusTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("OrderStatusType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(OrderStatusTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("OrderStatusType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(OrderStatusTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.OrderStatusTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   OrderStatusTypeId  = data.OrderStatusTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(OrderStatusTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new OrderStatusTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.OrderStatusGroupId  = data.OrderStatusGroupId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
