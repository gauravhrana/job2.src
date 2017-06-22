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
	public partial class CustodianAccountDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CustodianAccountDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CustodianAccount");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CustodianAccountDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CustodianAccountDataModel.DataColumns.CustodianAccountId:
					if (data.CustodianAccountId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustodianAccountDataModel.DataColumns.CustodianAccountId, data.CustodianAccountId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianAccountDataModel.DataColumns.CustodianAccountId);
					}
					break;

				case CustodianAccountDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustodianAccountDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianAccountDataModel.DataColumns.FundId);
					}
					break;

				case CustodianAccountDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustodianAccountDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianAccountDataModel.DataColumns.Fund);
					}
					break;

				case CustodianAccountDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustodianAccountDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianAccountDataModel.DataColumns.Name);
					}
					break;

				case CustodianAccountDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustodianAccountDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianAccountDataModel.DataColumns.Description);
					}
					break;

				case CustodianAccountDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustodianAccountDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianAccountDataModel.DataColumns.SortOrder);
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

		public static List<CustodianAccountDataModel> GetEntityDetails(CustodianAccountDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CustodianAccountSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CustodianAccountId           = dataQuery.CustodianAccountId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<CustodianAccountDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CustodianAccountDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CustodianAccountDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CustodianAccountDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CustodianAccountDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CustodianAccountDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CustodianAccountInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CustodianAccountUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CustodianAccountDataModel.DataColumns.CustodianAccountId); 
			sql = sql + ", " + ToSQLParameter(data, CustodianAccountDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, CustodianAccountDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CustodianAccountDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, CustodianAccountDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CustodianAccountDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CustodianAccount.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CustodianAccountDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CustodianAccount.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CustodianAccountDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CustodianAccountDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CustodianAccountId  = data.CustodianAccountId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CustodianAccountDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CustodianAccountDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
