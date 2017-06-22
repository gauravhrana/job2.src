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
	public partial class CreditDefaultSwapIndexDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CreditDefaultSwapIndexDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CreditDefaultSwapIndex");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CreditDefaultSwapIndexDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CreditDefaultSwapIndexDataModel.DataColumns.CreditDefaultSwapIndexId:
					if (data.CreditDefaultSwapIndexId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CreditDefaultSwapIndexDataModel.DataColumns.CreditDefaultSwapIndexId, data.CreditDefaultSwapIndexId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDefaultSwapIndexDataModel.DataColumns.CreditDefaultSwapIndexId);
					}
					break;

				case CreditDefaultSwapIndexDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CreditDefaultSwapIndexDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDefaultSwapIndexDataModel.DataColumns.Name);
					}
					break;

				case CreditDefaultSwapIndexDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CreditDefaultSwapIndexDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDefaultSwapIndexDataModel.DataColumns.Description);
					}
					break;

				case CreditDefaultSwapIndexDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CreditDefaultSwapIndexDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditDefaultSwapIndexDataModel.DataColumns.SortOrder);
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

		public static List<CreditDefaultSwapIndexDataModel> GetEntityDetails(CreditDefaultSwapIndexDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CreditDefaultSwapIndexSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CreditDefaultSwapIndexId           = dataQuery.CreditDefaultSwapIndexId
				 ,	Name           = dataQuery.Name
			};

			List<CreditDefaultSwapIndexDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CreditDefaultSwapIndexDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CreditDefaultSwapIndexDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CreditDefaultSwapIndexDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CreditDefaultSwapIndexDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CreditDefaultSwapIndexDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CreditDefaultSwapIndexInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CreditDefaultSwapIndexUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CreditDefaultSwapIndexDataModel.DataColumns.CreditDefaultSwapIndexId); 
			sql = sql + ", " + ToSQLParameter(data, CreditDefaultSwapIndexDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CreditDefaultSwapIndexDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, CreditDefaultSwapIndexDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CreditDefaultSwapIndexDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CreditDefaultSwapIndex.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CreditDefaultSwapIndexDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CreditDefaultSwapIndex.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CreditDefaultSwapIndexDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CreditDefaultSwapIndexDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CreditDefaultSwapIndexId  = data.CreditDefaultSwapIndexId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CreditDefaultSwapIndexDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CreditDefaultSwapIndexDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
