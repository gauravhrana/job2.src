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
	public partial class SwapInvestmentDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SwapInvestmentDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SwapInvestment");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SwapInvestmentDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SwapInvestmentDataModel.DataColumns.SwapInvestmentId:
					if (data.SwapInvestmentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SwapInvestmentDataModel.DataColumns.SwapInvestmentId, data.SwapInvestmentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SwapInvestmentDataModel.DataColumns.SwapInvestmentId);
					}
					break;

				case SwapInvestmentDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SwapInvestmentDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SwapInvestmentDataModel.DataColumns.Name);
					}
					break;

				case SwapInvestmentDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SwapInvestmentDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SwapInvestmentDataModel.DataColumns.Description);
					}
					break;

				case SwapInvestmentDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SwapInvestmentDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SwapInvestmentDataModel.DataColumns.SortOrder);
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

		public static List<SwapInvestmentDataModel> GetEntityDetails(SwapInvestmentDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SwapInvestmentSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SwapInvestmentId           = dataQuery.SwapInvestmentId
				 ,	Name           = dataQuery.Name
			};

			List<SwapInvestmentDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SwapInvestmentDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SwapInvestmentDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SwapInvestmentDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SwapInvestmentDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SwapInvestmentDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SwapInvestmentInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SwapInvestmentUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SwapInvestmentDataModel.DataColumns.SwapInvestmentId); 
			sql = sql + ", " + ToSQLParameter(data, SwapInvestmentDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, SwapInvestmentDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, SwapInvestmentDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SwapInvestmentDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SwapInvestment.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SwapInvestmentDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SwapInvestment.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SwapInvestmentDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SwapInvestmentDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SwapInvestmentId  = data.SwapInvestmentId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SwapInvestmentDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SwapInvestmentDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
