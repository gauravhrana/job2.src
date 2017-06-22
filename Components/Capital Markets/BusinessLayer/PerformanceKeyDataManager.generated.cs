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
	public partial class PerformanceKeyDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PerformanceKeyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PerformanceKey");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PerformanceKeyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PerformanceKeyDataModel.DataColumns.PerformanceKeyId:
					if (data.PerformanceKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PerformanceKeyDataModel.DataColumns.PerformanceKeyId, data.PerformanceKeyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PerformanceKeyDataModel.DataColumns.PerformanceKeyId);
					}
					break;

				case PerformanceKeyDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PerformanceKeyDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PerformanceKeyDataModel.DataColumns.Name);
					}
					break;

				case PerformanceKeyDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PerformanceKeyDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PerformanceKeyDataModel.DataColumns.Description);
					}
					break;

				case PerformanceKeyDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PerformanceKeyDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PerformanceKeyDataModel.DataColumns.SortOrder);
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

		public static List<PerformanceKeyDataModel> GetEntityDetails(PerformanceKeyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PerformanceKeySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PerformanceKeyId           = dataQuery.PerformanceKeyId
				 ,	Name           = dataQuery.Name
			};

			List<PerformanceKeyDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PerformanceKeyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PerformanceKeyDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PerformanceKeyDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PerformanceKeyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PerformanceKeyDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PerformanceKeyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PerformanceKeyUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PerformanceKeyDataModel.DataColumns.PerformanceKeyId); 
			sql = sql + ", " + ToSQLParameter(data, PerformanceKeyDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, PerformanceKeyDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, PerformanceKeyDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PerformanceKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PerformanceKey.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PerformanceKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PerformanceKey.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PerformanceKeyDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PerformanceKeyDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PerformanceKeyId  = data.PerformanceKeyId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PerformanceKeyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PerformanceKeyDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
