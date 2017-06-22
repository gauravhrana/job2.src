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
	public partial class StrategyGroupDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static StrategyGroupDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("StrategyGroup");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(StrategyGroupDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case StrategyGroupDataModel.DataColumns.StrategyGroupId:
					if (data.StrategyGroupId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.StrategyGroupId, data.StrategyGroupId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.StrategyGroupId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.FundId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StrategyGroupDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.Fund);
					}
					break;

				case StrategyGroupDataModel.DataColumns.ClassificationId:
					if (data.ClassificationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.ClassificationId, data.ClassificationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.ClassificationId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.PortfolioId:
					if (data.PortfolioId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.PortfolioId, data.PortfolioId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.PortfolioId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.ParentStrategyGroupId:
					if (data.ParentStrategyGroupId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.ParentStrategyGroupId, data.ParentStrategyGroupId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.ParentStrategyGroupId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.DefaultUSecurityId:
					if (data.DefaultUSecurityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.DefaultUSecurityId, data.DefaultUSecurityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.DefaultUSecurityId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.ActiveYN:
					if (data.ActiveYN != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.ActiveYN, data.ActiveYN);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.ActiveYN);
					}
					break;

				case StrategyGroupDataModel.DataColumns.OpenDateId:
					if (data.OpenDateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.OpenDateId, data.OpenDateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.OpenDateId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.CloseDateId:
					if (data.CloseDateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.CloseDateId, data.CloseDateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.CloseDateId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.ClosedYN:
					if (data.ClosedYN != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.ClosedYN, data.ClosedYN);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.ClosedYN);
					}
					break;

				case StrategyGroupDataModel.DataColumns.ThemeId:
					if (data.ThemeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StrategyGroupDataModel.DataColumns.ThemeId, data.ThemeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.ThemeId);
					}
					break;

				case StrategyGroupDataModel.DataColumns.StrategyGroupCode:
					if (!string.IsNullOrEmpty(data.StrategyGroupCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StrategyGroupDataModel.DataColumns.StrategyGroupCode, data.StrategyGroupCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.StrategyGroupCode);
					}
					break;

				case StrategyGroupDataModel.DataColumns.StrategyGroupDesc:
					if (!string.IsNullOrEmpty(data.StrategyGroupDesc))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StrategyGroupDataModel.DataColumns.StrategyGroupDesc, data.StrategyGroupDesc);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StrategyGroupDataModel.DataColumns.StrategyGroupDesc);
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

		public static List<StrategyGroupDataModel> GetEntityDetails(StrategyGroupDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.StrategyGroupSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	StrategyGroupId           = dataQuery.StrategyGroupId
				 ,	FundId           = dataQuery.FundId
			};

			List<StrategyGroupDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<StrategyGroupDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<StrategyGroupDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(StrategyGroupDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(StrategyGroupDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(StrategyGroupDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.StrategyGroupInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.StrategyGroupUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.StrategyGroupId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.ClassificationId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.PortfolioId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.ParentStrategyGroupId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.DefaultUSecurityId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.ActiveYN); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.OpenDateId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.CloseDateId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.ClosedYN); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.ThemeId); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.StrategyGroupCode); 
			sql = sql + ", " + ToSQLParameter(data, StrategyGroupDataModel.DataColumns.StrategyGroupDesc); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(StrategyGroupDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("StrategyGroup.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(StrategyGroupDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("StrategyGroup.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(StrategyGroupDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.StrategyGroupDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   StrategyGroupId  = data.StrategyGroupId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(StrategyGroupDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new StrategyGroupDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
