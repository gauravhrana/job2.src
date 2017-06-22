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
	public partial class PerformanceParametersDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PerformanceParametersDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PerformanceParameters");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PerformanceParametersDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PerformanceParametersDataModel.DataColumns.PerformanceParametersId:
					if (data.PerformanceParametersId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PerformanceParametersDataModel.DataColumns.PerformanceParametersId, data.PerformanceParametersId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PerformanceParametersDataModel.DataColumns.PerformanceParametersId);
					}
					break;

				case PerformanceParametersDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PerformanceParametersDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PerformanceParametersDataModel.DataColumns.Name);
					}
					break;

				case PerformanceParametersDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PerformanceParametersDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PerformanceParametersDataModel.DataColumns.Description);
					}
					break;

				case PerformanceParametersDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PerformanceParametersDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PerformanceParametersDataModel.DataColumns.SortOrder);
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

		public static List<PerformanceParametersDataModel> GetEntityDetails(PerformanceParametersDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PerformanceParametersSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PerformanceParametersId           = dataQuery.PerformanceParametersId
				 ,	Name           = dataQuery.Name
			};

			List<PerformanceParametersDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PerformanceParametersDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PerformanceParametersDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PerformanceParametersDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PerformanceParametersDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PerformanceParametersDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PerformanceParametersInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PerformanceParametersUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PerformanceParametersDataModel.DataColumns.PerformanceParametersId); 
			sql = sql + ", " + ToSQLParameter(data, PerformanceParametersDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, PerformanceParametersDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, PerformanceParametersDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PerformanceParametersDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PerformanceParameters.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PerformanceParametersDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PerformanceParameters.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PerformanceParametersDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PerformanceParametersDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PerformanceParametersId  = data.PerformanceParametersId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PerformanceParametersDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PerformanceParametersDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
