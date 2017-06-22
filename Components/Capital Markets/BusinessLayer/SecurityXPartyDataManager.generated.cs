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
	public partial class SecurityXPartyDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SecurityXPartyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SecurityXParty");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SecurityXPartyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SecurityXPartyDataModel.DataColumns.SecurityXPartyId:
					if (data.SecurityXPartyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXPartyDataModel.DataColumns.SecurityXPartyId, data.SecurityXPartyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.SecurityXPartyId);
					}
					break;

				case SecurityXPartyDataModel.DataColumns.ExchangeId:
					if (data.ExchangeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXPartyDataModel.DataColumns.ExchangeId, data.ExchangeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.ExchangeId);
					}
					break;

				case SecurityXPartyDataModel.DataColumns.Exchange:
					if (!string.IsNullOrEmpty(data.Exchange))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXPartyDataModel.DataColumns.Exchange, data.Exchange);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.Exchange);
					}
					break;

				case SecurityXPartyDataModel.DataColumns.IssuerId:
					if (data.IssuerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXPartyDataModel.DataColumns.IssuerId, data.IssuerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.IssuerId);
					}
					break;

				case SecurityXPartyDataModel.DataColumns.Issuer:
					if (!string.IsNullOrEmpty(data.Issuer))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXPartyDataModel.DataColumns.Issuer, data.Issuer);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.Issuer);
					}
					break;

				case SecurityXPartyDataModel.DataColumns.DeliveryAgentId:
					if (data.DeliveryAgentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXPartyDataModel.DataColumns.DeliveryAgentId, data.DeliveryAgentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.DeliveryAgentId);
					}
					break;

				case SecurityXPartyDataModel.DataColumns.DeliveryAgent:
					if (!string.IsNullOrEmpty(data.DeliveryAgent))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXPartyDataModel.DataColumns.DeliveryAgent, data.DeliveryAgent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.DeliveryAgent);
					}
					break;

				case SecurityXPartyDataModel.DataColumns.SecurityId:
					if (data.SecurityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXPartyDataModel.DataColumns.SecurityId, data.SecurityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.SecurityId);
					}
					break;

				case SecurityXPartyDataModel.DataColumns.Security:
					if (!string.IsNullOrEmpty(data.Security))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXPartyDataModel.DataColumns.Security, data.Security);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXPartyDataModel.DataColumns.Security);
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

		public static List<SecurityXPartyDataModel> GetEntityDetails(SecurityXPartyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SecurityXPartySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SecurityXPartyId           = dataQuery.SecurityXPartyId
				 ,	ExchangeId           = dataQuery.ExchangeId
				 ,	IssuerId           = dataQuery.IssuerId
				 ,	DeliveryAgentId           = dataQuery.DeliveryAgentId
				 ,	SecurityId           = dataQuery.SecurityId
			};

			List<SecurityXPartyDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SecurityXPartyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SecurityXPartyDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SecurityXPartyDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SecurityXPartyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SecurityXPartyDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SecurityXPartyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SecurityXPartyUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SecurityXPartyDataModel.DataColumns.SecurityXPartyId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXPartyDataModel.DataColumns.ExchangeId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXPartyDataModel.DataColumns.IssuerId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXPartyDataModel.DataColumns.DeliveryAgentId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXPartyDataModel.DataColumns.SecurityId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SecurityXPartyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SecurityXParty.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SecurityXPartyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SecurityXParty.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SecurityXPartyDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SecurityXPartyDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SecurityXPartyId  = data.SecurityXPartyId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SecurityXPartyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SecurityXPartyDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.ExchangeId  = data.ExchangeId;
			doesExistRequest.IssuerId  = data.IssuerId;
			doesExistRequest.DeliveryAgentId  = data.DeliveryAgentId;
			doesExistRequest.SecurityId  = data.SecurityId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
