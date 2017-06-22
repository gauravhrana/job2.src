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
	public partial class InvestmentPricesDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static InvestmentPricesDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("InvestmentPrices");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(InvestmentPricesDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case InvestmentPricesDataModel.DataColumns.InvestmentPricesId:
					if (data.InvestmentPricesId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestmentPricesDataModel.DataColumns.InvestmentPricesId, data.InvestmentPricesId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentPricesDataModel.DataColumns.InvestmentPricesId);
					}
					break;

				case InvestmentPricesDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestmentPricesDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentPricesDataModel.DataColumns.Name);
					}
					break;

				case InvestmentPricesDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestmentPricesDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentPricesDataModel.DataColumns.Description);
					}
					break;

				case InvestmentPricesDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestmentPricesDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentPricesDataModel.DataColumns.SortOrder);
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

		public static List<InvestmentPricesDataModel> GetEntityDetails(InvestmentPricesDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.InvestmentPricesSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	InvestmentPricesId           = dataQuery.InvestmentPricesId
				 ,	Name           = dataQuery.Name
			};

			List<InvestmentPricesDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<InvestmentPricesDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<InvestmentPricesDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(InvestmentPricesDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(InvestmentPricesDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(InvestmentPricesDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.InvestmentPricesInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.InvestmentPricesUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, InvestmentPricesDataModel.DataColumns.InvestmentPricesId); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentPricesDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentPricesDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentPricesDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(InvestmentPricesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("InvestmentPrices.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(InvestmentPricesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("InvestmentPrices.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(InvestmentPricesDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.InvestmentPricesDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   InvestmentPricesId  = data.InvestmentPricesId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(InvestmentPricesDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new InvestmentPricesDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
