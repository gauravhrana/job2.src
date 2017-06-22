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
	public partial class SecurityXExternalMarketDataIdentifierDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SecurityXExternalMarketDataIdentifierDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SecurityXExternalMarketDataIdentifier");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SecurityXExternalMarketDataIdentifierDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.SecurityXExternalMarketDataIdentifierId:
					if (data.SecurityXExternalMarketDataIdentifierId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.SecurityXExternalMarketDataIdentifierId, data.SecurityXExternalMarketDataIdentifierId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.SecurityXExternalMarketDataIdentifierId);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergGlobalId:
					if (data.BloombergGlobalId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergGlobalId, data.BloombergGlobalId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergGlobalId);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergTicker:
					if (!string.IsNullOrEmpty(data.BloombergTicker))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergTicker, data.BloombergTicker);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergTicker);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergUniqueId:
					if (data.BloombergUniqueId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergUniqueId, data.BloombergUniqueId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergUniqueId);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergMarketSector:
					if (!string.IsNullOrEmpty(data.BloombergMarketSector))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergMarketSector, data.BloombergMarketSector);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergMarketSector);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.RICCode:
					if (!string.IsNullOrEmpty(data.RICCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.RICCode, data.RICCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.RICCode);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.IDCCode:
					if (!string.IsNullOrEmpty(data.IDCCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.IDCCode, data.IDCCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.IDCCode);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.RedCode:
					if (!string.IsNullOrEmpty(data.RedCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.RedCode, data.RedCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.RedCode);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.PriceWithSuperDerivatives:
					if (!string.IsNullOrEmpty(data.PriceWithSuperDerivatives))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.PriceWithSuperDerivatives, data.PriceWithSuperDerivatives);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.PriceWithSuperDerivatives);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.SecurityId:
					if (data.SecurityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.SecurityId, data.SecurityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.SecurityId);
					}
					break;

				case SecurityXExternalMarketDataIdentifierDataModel.DataColumns.Security:
					if (!string.IsNullOrEmpty(data.Security))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.Security, data.Security);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.Security);
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

		public static List<SecurityXExternalMarketDataIdentifierDataModel> GetEntityDetails(SecurityXExternalMarketDataIdentifierDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SecurityXExternalMarketDataIdentifierSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SecurityXExternalMarketDataIdentifierId           = dataQuery.SecurityXExternalMarketDataIdentifierId
				 ,	SecurityId           = dataQuery.SecurityId
			};

			List<SecurityXExternalMarketDataIdentifierDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SecurityXExternalMarketDataIdentifierDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SecurityXExternalMarketDataIdentifierDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SecurityXExternalMarketDataIdentifierDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SecurityXExternalMarketDataIdentifierDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SecurityXExternalMarketDataIdentifierDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SecurityXExternalMarketDataIdentifierInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SecurityXExternalMarketDataIdentifierUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.SecurityXExternalMarketDataIdentifierId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergGlobalId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergTicker); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergUniqueId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.BloombergMarketSector); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.RICCode); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.IDCCode); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.RedCode); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.PriceWithSuperDerivatives); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXExternalMarketDataIdentifierDataModel.DataColumns.SecurityId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SecurityXExternalMarketDataIdentifierDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SecurityXExternalMarketDataIdentifier.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SecurityXExternalMarketDataIdentifierDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SecurityXExternalMarketDataIdentifier.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SecurityXExternalMarketDataIdentifierDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SecurityXExternalMarketDataIdentifierDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SecurityXExternalMarketDataIdentifierId  = data.SecurityXExternalMarketDataIdentifierId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SecurityXExternalMarketDataIdentifierDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SecurityXExternalMarketDataIdentifierDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.SecurityId  = data.SecurityId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
