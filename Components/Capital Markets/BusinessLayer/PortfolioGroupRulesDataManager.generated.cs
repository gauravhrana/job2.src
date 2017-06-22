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
	public partial class PortfolioGroupRulesDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PortfolioGroupRulesDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PortfolioGroupRules");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PortfolioGroupRulesDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PortfolioGroupRulesDataModel.DataColumns.PortfolioGroupRulesId:
					if (data.PortfolioGroupRulesId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PortfolioGroupRulesDataModel.DataColumns.PortfolioGroupRulesId, data.PortfolioGroupRulesId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioGroupRulesDataModel.DataColumns.PortfolioGroupRulesId);
					}
					break;

				case PortfolioGroupRulesDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PortfolioGroupRulesDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioGroupRulesDataModel.DataColumns.FundId);
					}
					break;

				case PortfolioGroupRulesDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PortfolioGroupRulesDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioGroupRulesDataModel.DataColumns.Fund);
					}
					break;

				case PortfolioGroupRulesDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PortfolioGroupRulesDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioGroupRulesDataModel.DataColumns.Name);
					}
					break;

				case PortfolioGroupRulesDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PortfolioGroupRulesDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioGroupRulesDataModel.DataColumns.Description);
					}
					break;

				case PortfolioGroupRulesDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PortfolioGroupRulesDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioGroupRulesDataModel.DataColumns.SortOrder);
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

		public static List<PortfolioGroupRulesDataModel> GetEntityDetails(PortfolioGroupRulesDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PortfolioGroupRulesSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PortfolioGroupRulesId           = dataQuery.PortfolioGroupRulesId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<PortfolioGroupRulesDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PortfolioGroupRulesDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PortfolioGroupRulesDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PortfolioGroupRulesDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PortfolioGroupRulesDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PortfolioGroupRulesDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PortfolioGroupRulesInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PortfolioGroupRulesUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PortfolioGroupRulesDataModel.DataColumns.PortfolioGroupRulesId); 
			sql = sql + ", " + ToSQLParameter(data, PortfolioGroupRulesDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, PortfolioGroupRulesDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, PortfolioGroupRulesDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, PortfolioGroupRulesDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PortfolioGroupRulesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PortfolioGroupRules.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PortfolioGroupRulesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PortfolioGroupRules.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PortfolioGroupRulesDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PortfolioGroupRulesDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PortfolioGroupRulesId  = data.PortfolioGroupRulesId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PortfolioGroupRulesDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PortfolioGroupRulesDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
