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
	public partial class LegalEntityDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static LegalEntityDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("LegalEntity");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(LegalEntityDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case LegalEntityDataModel.DataColumns.LegalEntityId:
					if (data.LegalEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, LegalEntityDataModel.DataColumns.LegalEntityId, data.LegalEntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LegalEntityDataModel.DataColumns.LegalEntityId);
					}
					break;

				case LegalEntityDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, LegalEntityDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LegalEntityDataModel.DataColumns.FundId);
					}
					break;

				case LegalEntityDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, LegalEntityDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LegalEntityDataModel.DataColumns.Fund);
					}
					break;

				case LegalEntityDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, LegalEntityDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LegalEntityDataModel.DataColumns.Name);
					}
					break;

				case LegalEntityDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, LegalEntityDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LegalEntityDataModel.DataColumns.Description);
					}
					break;

				case LegalEntityDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, LegalEntityDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LegalEntityDataModel.DataColumns.SortOrder);
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

		public static List<LegalEntityDataModel> GetEntityDetails(LegalEntityDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.LegalEntitySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	LegalEntityId           = dataQuery.LegalEntityId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<LegalEntityDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<LegalEntityDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<LegalEntityDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(LegalEntityDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(LegalEntityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static LegalEntityDataModel GetDetails(LegalEntityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save


		public static string Save(LegalEntityDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.LegalEntityInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.LegalEntityUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, LegalEntityDataModel.DataColumns.LegalEntityId); 
			sql = sql + ", " + ToSQLParameter(data, LegalEntityDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, LegalEntityDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, LegalEntityDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, LegalEntityDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(LegalEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("LegalEntity.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(LegalEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("LegalEntity.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(LegalEntityDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.LegalEntityDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   LegalEntityId  = data.LegalEntityId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(LegalEntityDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new LegalEntityDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
