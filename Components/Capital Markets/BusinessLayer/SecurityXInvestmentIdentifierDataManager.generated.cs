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
	public partial class SecurityXInvestmentIdentifierDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SecurityXInvestmentIdentifierDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SecurityXInvestmentIdentifier");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SecurityXInvestmentIdentifierDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SecurityXInvestmentIdentifierDataModel.DataColumns.SecurityXInvestmentIdentifierId:
					if (data.SecurityXInvestmentIdentifierId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXInvestmentIdentifierDataModel.DataColumns.SecurityXInvestmentIdentifierId, data.SecurityXInvestmentIdentifierId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.SecurityXInvestmentIdentifierId);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.SecurityId:
					if (data.SecurityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXInvestmentIdentifierDataModel.DataColumns.SecurityId, data.SecurityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.SecurityId);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.Security:
					if (!string.IsNullOrEmpty(data.Security))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXInvestmentIdentifierDataModel.DataColumns.Security, data.Security);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.Security);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.Ticker:
					if (!string.IsNullOrEmpty(data.Ticker))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXInvestmentIdentifierDataModel.DataColumns.Ticker, data.Ticker);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.Ticker);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.CUSIP:
					if (!string.IsNullOrEmpty(data.CUSIP))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXInvestmentIdentifierDataModel.DataColumns.CUSIP, data.CUSIP);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.CUSIP);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.SEDOL:
					if (!string.IsNullOrEmpty(data.SEDOL))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXInvestmentIdentifierDataModel.DataColumns.SEDOL, data.SEDOL);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.SEDOL);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.ISIN:
					if (!string.IsNullOrEmpty(data.ISIN))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXInvestmentIdentifierDataModel.DataColumns.ISIN, data.ISIN);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.ISIN);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.WKN:
					if (!string.IsNullOrEmpty(data.WKN))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXInvestmentIdentifierDataModel.DataColumns.WKN, data.WKN);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.WKN);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.AltID1:
					if (data.AltID1 != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID1, data.AltID1);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID1);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.AltID2:
					if (data.AltID2 != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID2, data.AltID2);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID2);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.AltID3:
					if (data.AltID3 != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID3, data.AltID3);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID3);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.AltID4:
					if (data.AltID4 != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID4, data.AltID4);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID4);
					}
					break;

				case SecurityXInvestmentIdentifierDataModel.DataColumns.AltID5:
					if (data.AltID5 != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID5, data.AltID5);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID5);
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

		public static List<SecurityXInvestmentIdentifierDataModel> GetEntityDetails(SecurityXInvestmentIdentifierDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SecurityXInvestmentIdentifierSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SecurityXInvestmentIdentifierId           = dataQuery.SecurityXInvestmentIdentifierId
				 ,	SecurityId           = dataQuery.SecurityId
			};

			List<SecurityXInvestmentIdentifierDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SecurityXInvestmentIdentifierDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SecurityXInvestmentIdentifierDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SecurityXInvestmentIdentifierDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SecurityXInvestmentIdentifierDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SecurityXInvestmentIdentifierDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SecurityXInvestmentIdentifierInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SecurityXInvestmentIdentifierUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.SecurityXInvestmentIdentifierId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.SecurityId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.Ticker); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.CUSIP); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.SEDOL); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.ISIN); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.WKN); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID1); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID2); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID3); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID4); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXInvestmentIdentifierDataModel.DataColumns.AltID5); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SecurityXInvestmentIdentifierDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SecurityXInvestmentIdentifier.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SecurityXInvestmentIdentifierDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SecurityXInvestmentIdentifier.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SecurityXInvestmentIdentifierDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SecurityXInvestmentIdentifierDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SecurityXInvestmentIdentifierId  = data.SecurityXInvestmentIdentifierId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SecurityXInvestmentIdentifierDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SecurityXInvestmentIdentifierDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.SecurityId  = data.SecurityId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
