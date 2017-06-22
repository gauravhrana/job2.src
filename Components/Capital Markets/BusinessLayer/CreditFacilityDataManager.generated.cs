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
	public partial class CreditFacilityDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CreditFacilityDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CreditFacility");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CreditFacilityDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CreditFacilityDataModel.DataColumns.CreditFacilityId:
					if (data.CreditFacilityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CreditFacilityDataModel.DataColumns.CreditFacilityId, data.CreditFacilityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditFacilityDataModel.DataColumns.CreditFacilityId);
					}
					break;

				case CreditFacilityDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CreditFacilityDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditFacilityDataModel.DataColumns.Name);
					}
					break;

				case CreditFacilityDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CreditFacilityDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditFacilityDataModel.DataColumns.Description);
					}
					break;

				case CreditFacilityDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CreditFacilityDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CreditFacilityDataModel.DataColumns.SortOrder);
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

		public static List<CreditFacilityDataModel> GetEntityDetails(CreditFacilityDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CreditFacilitySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CreditFacilityId           = dataQuery.CreditFacilityId
				 ,	Name           = dataQuery.Name
			};

			List<CreditFacilityDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CreditFacilityDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CreditFacilityDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CreditFacilityDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CreditFacilityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CreditFacilityDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CreditFacilityInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CreditFacilityUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CreditFacilityDataModel.DataColumns.CreditFacilityId); 
			sql = sql + ", " + ToSQLParameter(data, CreditFacilityDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CreditFacilityDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, CreditFacilityDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CreditFacilityDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CreditFacility.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CreditFacilityDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CreditFacility.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CreditFacilityDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CreditFacilityDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CreditFacilityId  = data.CreditFacilityId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CreditFacilityDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CreditFacilityDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
