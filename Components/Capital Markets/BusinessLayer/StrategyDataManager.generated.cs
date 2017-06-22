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
	public partial class StrategyDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static StrategyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Strategy");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(StrategyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case StrategyDataModel.DataColumns.StrategyId:
					if (data.StrategyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyDataModel.DataColumns.StrategyId, data.StrategyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyDataModel.DataColumns.StrategyId);
					}
					break;

				case StrategyDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyDataModel.DataColumns.FundId);
					}
					break;

				case StrategyDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StrategyDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyDataModel.DataColumns.Fund);
					}
					break;

				case StrategyDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StrategyDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyDataModel.DataColumns.Name);
					}
					break;

				case StrategyDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StrategyDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyDataModel.DataColumns.Description);
					}
					break;

				case StrategyDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyDataModel.DataColumns.SortOrder);
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

		public static List<StrategyDataModel> GetEntityDetails(StrategyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.StrategySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	StrategyId           = dataQuery.StrategyId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<StrategyDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<StrategyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<StrategyDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(StrategyDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(StrategyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(StrategyDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.StrategyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.StrategyUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, StrategyDataModel.DataColumns.StrategyId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, StrategyDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, StrategyDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(StrategyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Strategy.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(StrategyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Strategy.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(StrategyDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.StrategyDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   StrategyId  = data.StrategyId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(StrategyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new StrategyDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
