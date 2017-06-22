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
	public partial class FinancialAccountTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FinancialAccountTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FinancialAccountType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FinancialAccountTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FinancialAccountTypeDataModel.DataColumns.FinancialAccountTypeId:
					if (data.FinancialAccountTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FinancialAccountTypeDataModel.DataColumns.FinancialAccountTypeId, data.FinancialAccountTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FinancialAccountTypeDataModel.DataColumns.FinancialAccountTypeId);
					}
					break;

				case FinancialAccountTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FinancialAccountTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FinancialAccountTypeDataModel.DataColumns.Name);
					}
					break;

				case FinancialAccountTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FinancialAccountTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FinancialAccountTypeDataModel.DataColumns.Description);
					}
					break;

				case FinancialAccountTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FinancialAccountTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FinancialAccountTypeDataModel.DataColumns.SortOrder);
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

		public static List<FinancialAccountTypeDataModel> GetEntityDetails(FinancialAccountTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FinancialAccountTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FinancialAccountTypeId           = dataQuery.FinancialAccountTypeId
				 ,	Name           = dataQuery.Name
			};

			List<FinancialAccountTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FinancialAccountTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FinancialAccountTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FinancialAccountTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FinancialAccountTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(FinancialAccountTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FinancialAccountTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FinancialAccountTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FinancialAccountTypeDataModel.DataColumns.FinancialAccountTypeId); 
			sql = sql + ", " + ToSQLParameter(data, FinancialAccountTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, FinancialAccountTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, FinancialAccountTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FinancialAccountTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FinancialAccountType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FinancialAccountTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FinancialAccountType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FinancialAccountTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FinancialAccountTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FinancialAccountTypeId  = data.FinancialAccountTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FinancialAccountTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FinancialAccountTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
