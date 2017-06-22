using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;

namespace DayCare.Components.BusinessLayer
{
	public partial class PaymentMethodDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PaymentMethodDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PaymentMethod");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PaymentMethodDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PaymentMethodDataModel.DataColumns.PaymentMethodId:
					if (data.PaymentMethodId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PaymentMethodDataModel.DataColumns.PaymentMethodId, data.PaymentMethodId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PaymentMethodDataModel.DataColumns.PaymentMethodId);
					}
					break;

				case PaymentMethodDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PaymentMethodDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PaymentMethodDataModel.DataColumns.Name);
					}
					break;

				case PaymentMethodDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PaymentMethodDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PaymentMethodDataModel.DataColumns.Description);
					}
					break;

				case PaymentMethodDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PaymentMethodDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PaymentMethodDataModel.DataColumns.SortOrder);
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

		public static List<PaymentMethodDataModel> GetEntityDetails(PaymentMethodDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PaymentMethodSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PaymentMethodId           = dataQuery.PaymentMethodId
				 ,	Name           = dataQuery.Name
			};

			List<PaymentMethodDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PaymentMethodDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PaymentMethodDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PaymentMethodDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PaymentMethodDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PaymentMethodDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PaymentMethodInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PaymentMethodUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PaymentMethodDataModel.DataColumns.PaymentMethodId); 
			sql = sql + ", " + ToSQLParameter(data, PaymentMethodDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, PaymentMethodDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, PaymentMethodDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PaymentMethodDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PaymentMethod.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PaymentMethodDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PaymentMethod.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PaymentMethodDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PaymentMethodDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PaymentMethodId  = data.PaymentMethodId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PaymentMethodDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PaymentMethodDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
