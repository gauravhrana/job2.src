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
	public partial class TWRBatchProcessingDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TWRBatchProcessingDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TWRBatchProcessing");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TWRBatchProcessingDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TWRBatchProcessingDataModel.DataColumns.TWRBatchProcessingId:
					if (data.TWRBatchProcessingId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TWRBatchProcessingDataModel.DataColumns.TWRBatchProcessingId, data.TWRBatchProcessingId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TWRBatchProcessingDataModel.DataColumns.TWRBatchProcessingId);
					}
					break;

				case TWRBatchProcessingDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TWRBatchProcessingDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TWRBatchProcessingDataModel.DataColumns.Name);
					}
					break;

				case TWRBatchProcessingDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TWRBatchProcessingDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TWRBatchProcessingDataModel.DataColumns.Description);
					}
					break;

				case TWRBatchProcessingDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TWRBatchProcessingDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TWRBatchProcessingDataModel.DataColumns.SortOrder);
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

		public static List<TWRBatchProcessingDataModel> GetEntityDetails(TWRBatchProcessingDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TWRBatchProcessingSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TWRBatchProcessingId           = dataQuery.TWRBatchProcessingId
				 ,	Name           = dataQuery.Name
			};

			List<TWRBatchProcessingDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TWRBatchProcessingDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TWRBatchProcessingDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TWRBatchProcessingDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TWRBatchProcessingDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TWRBatchProcessingDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TWRBatchProcessingInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TWRBatchProcessingUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TWRBatchProcessingDataModel.DataColumns.TWRBatchProcessingId); 
			sql = sql + ", " + ToSQLParameter(data, TWRBatchProcessingDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, TWRBatchProcessingDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, TWRBatchProcessingDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TWRBatchProcessingDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TWRBatchProcessing.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TWRBatchProcessingDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TWRBatchProcessing.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TWRBatchProcessingDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TWRBatchProcessingDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TWRBatchProcessingId  = data.TWRBatchProcessingId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TWRBatchProcessingDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TWRBatchProcessingDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
