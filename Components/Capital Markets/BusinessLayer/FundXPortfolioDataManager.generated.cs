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
	public partial class FundXPortfolioDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FundXPortfolioDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FundXPortfolio");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FundXPortfolioDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FundXPortfolioDataModel.DataColumns.FundXPortfolioId:
					if (data.FundXPortfolioId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundXPortfolioDataModel.DataColumns.FundXPortfolioId, data.FundXPortfolioId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXPortfolioDataModel.DataColumns.FundXPortfolioId);
					}
					break;

				case FundXPortfolioDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundXPortfolioDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXPortfolioDataModel.DataColumns.FundId);
					}
					break;

				case FundXPortfolioDataModel.DataColumns.PortfolioId:
					if (data.PortfolioId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundXPortfolioDataModel.DataColumns.PortfolioId, data.PortfolioId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXPortfolioDataModel.DataColumns.PortfolioId);
					}
					break;

				case FundXPortfolioDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FundXPortfolioDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXPortfolioDataModel.DataColumns.Fund);
					}
					break;

				case FundXPortfolioDataModel.DataColumns.Portfolio:
					if (!string.IsNullOrEmpty(data.Portfolio))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FundXPortfolioDataModel.DataColumns.Portfolio, data.Portfolio);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXPortfolioDataModel.DataColumns.Portfolio);
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

		public static List<FundXPortfolioDataModel> GetEntityDetails(FundXPortfolioDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FundXPortfolioSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FundXPortfolioId           = dataQuery.FundXPortfolioId
				 ,	FundId           = dataQuery.FundId
				 ,	PortfolioId           = dataQuery.PortfolioId
			};

			List<FundXPortfolioDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FundXPortfolioDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FundXPortfolioDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FundXPortfolioDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FundXPortfolioDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(FundXPortfolioDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FundXPortfolioInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FundXPortfolioUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FundXPortfolioDataModel.DataColumns.FundXPortfolioId); 
			sql = sql + ", " + ToSQLParameter(data, FundXPortfolioDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, FundXPortfolioDataModel.DataColumns.PortfolioId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FundXPortfolioDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FundXPortfolio.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FundXPortfolioDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FundXPortfolio.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FundXPortfolioDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FundXPortfolioDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				 ,	FundXPortfolioId           = data.FundXPortfolioId
				 ,	FundId           = data.FundId
				 ,	PortfolioId           = data.PortfolioId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FundXPortfolioDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FundXPortfolioDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
