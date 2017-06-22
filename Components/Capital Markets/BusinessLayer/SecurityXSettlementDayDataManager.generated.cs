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
	public partial class SecurityXSettlementDayDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SecurityXSettlementDayDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SecurityXSettlementDay");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SecurityXSettlementDayDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SecurityXSettlementDayDataModel.DataColumns.SecurityXSettlementDayId:
					if (data.SecurityXSettlementDayId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXSettlementDayDataModel.DataColumns.SecurityXSettlementDayId, data.SecurityXSettlementDayId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXSettlementDayDataModel.DataColumns.SecurityXSettlementDayId);
					}
					break;

				case SecurityXSettlementDayDataModel.DataColumns.SettlementDay:
					if (data.SettlementDay != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXSettlementDayDataModel.DataColumns.SettlementDay, data.SettlementDay);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXSettlementDayDataModel.DataColumns.SettlementDay);
					}
					break;

				case SecurityXSettlementDayDataModel.DataColumns.SecurityId:
					if (data.SecurityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXSettlementDayDataModel.DataColumns.SecurityId, data.SecurityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXSettlementDayDataModel.DataColumns.SecurityId);
					}
					break;

				case SecurityXSettlementDayDataModel.DataColumns.Security:
					if (!string.IsNullOrEmpty(data.Security))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXSettlementDayDataModel.DataColumns.Security, data.Security);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXSettlementDayDataModel.DataColumns.Security);
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

		public static List<SecurityXSettlementDayDataModel> GetEntityDetails(SecurityXSettlementDayDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SecurityXSettlementDaySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SecurityXSettlementDayId           = dataQuery.SecurityXSettlementDayId
				 ,	SecurityId           = dataQuery.SecurityId
			};

			List<SecurityXSettlementDayDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SecurityXSettlementDayDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SecurityXSettlementDayDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SecurityXSettlementDayDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SecurityXSettlementDayDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SecurityXSettlementDayDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SecurityXSettlementDayInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SecurityXSettlementDayUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SecurityXSettlementDayDataModel.DataColumns.SecurityXSettlementDayId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXSettlementDayDataModel.DataColumns.SettlementDay); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXSettlementDayDataModel.DataColumns.SecurityId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SecurityXSettlementDayDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SecurityXSettlementDay.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SecurityXSettlementDayDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SecurityXSettlementDay.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SecurityXSettlementDayDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SecurityXSettlementDayDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SecurityXSettlementDayId  = data.SecurityXSettlementDayId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SecurityXSettlementDayDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SecurityXSettlementDayDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.SecurityId  = data.SecurityId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
