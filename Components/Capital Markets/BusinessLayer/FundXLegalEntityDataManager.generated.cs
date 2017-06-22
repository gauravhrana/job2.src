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
	public partial class FundXLegalEntityDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FundXLegalEntityDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FundXLegalEntity");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FundXLegalEntityDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FundXLegalEntityDataModel.DataColumns.FundXLegalEntityId:
					if (data.FundXLegalEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundXLegalEntityDataModel.DataColumns.FundXLegalEntityId, data.FundXLegalEntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXLegalEntityDataModel.DataColumns.FundXLegalEntityId);
					}
					break;

				case FundXLegalEntityDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundXLegalEntityDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXLegalEntityDataModel.DataColumns.FundId);
					}
					break;

				case FundXLegalEntityDataModel.DataColumns.LegalEntityId:
					if (data.LegalEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundXLegalEntityDataModel.DataColumns.LegalEntityId, data.LegalEntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXLegalEntityDataModel.DataColumns.LegalEntityId);
					}
					break;

				case FundXLegalEntityDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FundXLegalEntityDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXLegalEntityDataModel.DataColumns.Fund);
					}
					break;

				case FundXLegalEntityDataModel.DataColumns.LegalEntity:
					if (!string.IsNullOrEmpty(data.LegalEntity))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FundXLegalEntityDataModel.DataColumns.LegalEntity, data.LegalEntity);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundXLegalEntityDataModel.DataColumns.LegalEntity);
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

		public static List<FundXLegalEntityDataModel> GetEntityDetails(FundXLegalEntityDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FundXLegalEntitySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FundXLegalEntityId           = dataQuery.FundXLegalEntityId
				 ,	FundId           = dataQuery.FundId
				 ,	LegalEntityId           = dataQuery.LegalEntityId
			};

			List<FundXLegalEntityDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FundXLegalEntityDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FundXLegalEntityDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FundXLegalEntityDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FundXLegalEntityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(FundXLegalEntityDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FundXLegalEntityInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FundXLegalEntityUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FundXLegalEntityDataModel.DataColumns.FundXLegalEntityId); 
			sql = sql + ", " + ToSQLParameter(data, FundXLegalEntityDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, FundXLegalEntityDataModel.DataColumns.LegalEntityId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FundXLegalEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FundXLegalEntity.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FundXLegalEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FundXLegalEntity.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FundXLegalEntityDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FundXLegalEntityDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				 ,	FundXLegalEntityId           = data.FundXLegalEntityId
				 ,	FundId           = data.FundId
				 ,	LegalEntityId           = data.LegalEntityId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FundXLegalEntityDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FundXLegalEntityDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
