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
	public partial class LockdownPoolsDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static LockdownPoolsDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("LockdownPools");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(LockdownPoolsDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case LockdownPoolsDataModel.DataColumns.LockdownPoolsId:
					if (data.LockdownPoolsId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, LockdownPoolsDataModel.DataColumns.LockdownPoolsId, data.LockdownPoolsId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LockdownPoolsDataModel.DataColumns.LockdownPoolsId);
					}
					break;

				case LockdownPoolsDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, LockdownPoolsDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LockdownPoolsDataModel.DataColumns.Name);
					}
					break;

				case LockdownPoolsDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, LockdownPoolsDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LockdownPoolsDataModel.DataColumns.Description);
					}
					break;

				case LockdownPoolsDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, LockdownPoolsDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LockdownPoolsDataModel.DataColumns.SortOrder);
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

		public static List<LockdownPoolsDataModel> GetEntityDetails(LockdownPoolsDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.LockdownPoolsSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	LockdownPoolsId           = dataQuery.LockdownPoolsId
				 ,	Name           = dataQuery.Name
			};

			List<LockdownPoolsDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<LockdownPoolsDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<LockdownPoolsDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(LockdownPoolsDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(LockdownPoolsDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(LockdownPoolsDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.LockdownPoolsInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.LockdownPoolsUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, LockdownPoolsDataModel.DataColumns.LockdownPoolsId); 
			sql = sql + ", " + ToSQLParameter(data, LockdownPoolsDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, LockdownPoolsDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, LockdownPoolsDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(LockdownPoolsDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("LockdownPools.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(LockdownPoolsDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("LockdownPools.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(LockdownPoolsDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.LockdownPoolsDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   LockdownPoolsId  = data.LockdownPoolsId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(LockdownPoolsDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new LockdownPoolsDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
