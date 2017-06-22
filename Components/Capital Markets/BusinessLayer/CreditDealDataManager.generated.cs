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
	public partial class CreditDealDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CreditDealDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CreditDeal");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CreditDealDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CreditDealDataModel.DataColumns.CreditDealId:
					if (data.CreditDealId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CreditDealDataModel.DataColumns.CreditDealId, data.CreditDealId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDealDataModel.DataColumns.CreditDealId);
					}
					break;

				case CreditDealDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CreditDealDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDealDataModel.DataColumns.Name);
					}
					break;

				case CreditDealDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CreditDealDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDealDataModel.DataColumns.Description);
					}
					break;

				case CreditDealDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CreditDealDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDealDataModel.DataColumns.SortOrder);
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

		public static List<CreditDealDataModel> GetEntityDetails(CreditDealDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CreditDealSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CreditDealId           = dataQuery.CreditDealId
				 ,	Name           = dataQuery.Name
			};

			List<CreditDealDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CreditDealDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CreditDealDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CreditDealDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CreditDealDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CreditDealDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CreditDealInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CreditDealUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CreditDealDataModel.DataColumns.CreditDealId); 
			sql = sql + ", " + ToSQLParameter(data, CreditDealDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CreditDealDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, CreditDealDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CreditDealDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CreditDeal.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CreditDealDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CreditDeal.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CreditDealDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CreditDealDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CreditDealId  = data.CreditDealId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CreditDealDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CreditDealDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
