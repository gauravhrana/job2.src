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
	public partial class CustodianDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CustodianDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Custodian");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CustodianDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CustodianDataModel.DataColumns.CustodianId:
					if (data.CustodianId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustodianDataModel.DataColumns.CustodianId, data.CustodianId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianDataModel.DataColumns.CustodianId);
					}
					break;

				case CustodianDataModel.DataColumns.Url:
					if (!string.IsNullOrEmpty(data.Url))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustodianDataModel.DataColumns.Url, data.Url);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianDataModel.DataColumns.Url);
					}
					break;

				case CustodianDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustodianDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianDataModel.DataColumns.Code);
					}
					break;

				case CustodianDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustodianDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianDataModel.DataColumns.Name);
					}
					break;

				case CustodianDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustodianDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianDataModel.DataColumns.Description);
					}
					break;

				case CustodianDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustodianDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustodianDataModel.DataColumns.SortOrder);
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

		public static List<CustodianDataModel> GetEntityDetails(CustodianDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CustodianSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CustodianId           = dataQuery.CustodianId
				 ,	Name           = dataQuery.Name
			};

			List<CustodianDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CustodianDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CustodianDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CustodianDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CustodianDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(CustodianDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(CustodianDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.CustodianInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CustodianUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CustodianDataModel.DataColumns.CustodianId); 
			sql = sql + ", " + ToSQLParameter(data, CustodianDataModel.DataColumns.Url); 
			sql = sql + ", " + ToSQLParameter(data, CustodianDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, CustodianDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CustodianDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, CustodianDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CustodianDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Custodian.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CustodianDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Custodian.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CustodianDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CustodianDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CustodianId  = data.CustodianId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CustodianDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CustodianDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
