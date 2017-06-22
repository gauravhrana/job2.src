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
	public partial class TransactionTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TransactionTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TransactionType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TransactionTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TransactionTypeDataModel.DataColumns.TransactionTypeId:
					if (data.TransactionTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionTypeDataModel.DataColumns.TransactionTypeId, data.TransactionTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionTypeDataModel.DataColumns.TransactionTypeId);
					}
					break;

				case TransactionTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionTypeDataModel.DataColumns.Name);
					}
					break;

				case TransactionTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionTypeDataModel.DataColumns.Description);
					}
					break;

				case TransactionTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionTypeDataModel.DataColumns.SortOrder);
					}
					break;

				case TransactionTypeDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionTypeDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionTypeDataModel.DataColumns.Code);
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

		public static List<TransactionTypeDataModel> GetEntityDetails(TransactionTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TransactionTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TransactionTypeId           = dataQuery.TransactionTypeId
				 ,	Name           = dataQuery.Name
				 ,	Code           = dataQuery.Code
			};

			List<TransactionTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TransactionTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TransactionTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TransactionTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TransactionTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(TransactionTypeDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(TransactionTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.TransactionTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TransactionTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TransactionTypeDataModel.DataColumns.TransactionTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, TransactionTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, TransactionTypeDataModel.DataColumns.SortOrder); 
			sql = sql + ", " + ToSQLParameter(data, TransactionTypeDataModel.DataColumns.Code); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TransactionTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TransactionType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TransactionTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TransactionType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TransactionTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TransactionTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TransactionTypeId  = data.TransactionTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TransactionTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TransactionTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
