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
	public partial class CompanyDealTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CompanyDealTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CompanyDealType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CompanyDealTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CompanyDealTypeDataModel.DataColumns.CompanyDealTypeId:
					if (data.CompanyDealTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CompanyDealTypeDataModel.DataColumns.CompanyDealTypeId, data.CompanyDealTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CompanyDealTypeDataModel.DataColumns.CompanyDealTypeId);
					}
					break;

				case CompanyDealTypeDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CompanyDealTypeDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CompanyDealTypeDataModel.DataColumns.FundId);
					}
					break;

				case CompanyDealTypeDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CompanyDealTypeDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CompanyDealTypeDataModel.DataColumns.Fund);
					}
					break;

				case CompanyDealTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CompanyDealTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CompanyDealTypeDataModel.DataColumns.Name);
					}
					break;

				case CompanyDealTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CompanyDealTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CompanyDealTypeDataModel.DataColumns.Description);
					}
					break;

				case CompanyDealTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CompanyDealTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CompanyDealTypeDataModel.DataColumns.SortOrder);
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

		public static List<CompanyDealTypeDataModel> GetEntityDetails(CompanyDealTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CompanyDealTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CompanyDealTypeId           = dataQuery.CompanyDealTypeId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<CompanyDealTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CompanyDealTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CompanyDealTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CompanyDealTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CompanyDealTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CompanyDealTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CompanyDealTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CompanyDealTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CompanyDealTypeDataModel.DataColumns.CompanyDealTypeId); 
			sql = sql + ", " + ToSQLParameter(data, CompanyDealTypeDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, CompanyDealTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CompanyDealTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, CompanyDealTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CompanyDealTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CompanyDealType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CompanyDealTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CompanyDealType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CompanyDealTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CompanyDealTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CompanyDealTypeId  = data.CompanyDealTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CompanyDealTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CompanyDealTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
