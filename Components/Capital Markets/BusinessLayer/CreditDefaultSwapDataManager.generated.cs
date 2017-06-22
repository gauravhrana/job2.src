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
	public partial class CreditDefaultSwapDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CreditDefaultSwapDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CreditDefaultSwap");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CreditDefaultSwapDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CreditDefaultSwapDataModel.DataColumns.CreditDefaultSwapId:
					if (data.CreditDefaultSwapId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CreditDefaultSwapDataModel.DataColumns.CreditDefaultSwapId, data.CreditDefaultSwapId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDefaultSwapDataModel.DataColumns.CreditDefaultSwapId);
					}
					break;

				case CreditDefaultSwapDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CreditDefaultSwapDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDefaultSwapDataModel.DataColumns.Name);
					}
					break;

				case CreditDefaultSwapDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CreditDefaultSwapDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDefaultSwapDataModel.DataColumns.Description);
					}
					break;

				case CreditDefaultSwapDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CreditDefaultSwapDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDefaultSwapDataModel.DataColumns.SortOrder);
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

		public static List<CreditDefaultSwapDataModel> GetEntityDetails(CreditDefaultSwapDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CreditDefaultSwapSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CreditDefaultSwapId           = dataQuery.CreditDefaultSwapId
				 ,	Name           = dataQuery.Name
			};

			List<CreditDefaultSwapDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CreditDefaultSwapDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CreditDefaultSwapDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CreditDefaultSwapDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CreditDefaultSwapDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CreditDefaultSwapDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CreditDefaultSwapInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CreditDefaultSwapUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CreditDefaultSwapDataModel.DataColumns.CreditDefaultSwapId); 
			sql = sql + ", " + ToSQLParameter(data, CreditDefaultSwapDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CreditDefaultSwapDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, CreditDefaultSwapDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CreditDefaultSwapDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CreditDefaultSwap.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CreditDefaultSwapDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CreditDefaultSwap.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CreditDefaultSwapDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CreditDefaultSwapDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CreditDefaultSwapId  = data.CreditDefaultSwapId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CreditDefaultSwapDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CreditDefaultSwapDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
