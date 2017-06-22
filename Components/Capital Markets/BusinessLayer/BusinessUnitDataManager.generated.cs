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
	public partial class BusinessUnitDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static BusinessUnitDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("BusinessUnit");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(BusinessUnitDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BusinessUnitDataModel.DataColumns.BusinessUnitId:
					if (data.BusinessUnitId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BusinessUnitDataModel.DataColumns.BusinessUnitId, data.BusinessUnitId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessUnitDataModel.DataColumns.BusinessUnitId);
					}
					break;

				case BusinessUnitDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BusinessUnitDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessUnitDataModel.DataColumns.FundId);
					}
					break;

				case BusinessUnitDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BusinessUnitDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessUnitDataModel.DataColumns.Fund);
					}
					break;

				case BusinessUnitDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BusinessUnitDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessUnitDataModel.DataColumns.Name);
					}
					break;

				case BusinessUnitDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BusinessUnitDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessUnitDataModel.DataColumns.Description);
					}
					break;

				case BusinessUnitDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BusinessUnitDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessUnitDataModel.DataColumns.SortOrder);
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

		public static List<BusinessUnitDataModel> GetEntityDetails(BusinessUnitDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.BusinessUnitSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	BusinessUnitId           = dataQuery.BusinessUnitId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<BusinessUnitDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<BusinessUnitDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<BusinessUnitDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(BusinessUnitDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(BusinessUnitDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(BusinessUnitDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.BusinessUnitInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.BusinessUnitUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, BusinessUnitDataModel.DataColumns.BusinessUnitId); 
			sql = sql + ", " + ToSQLParameter(data, BusinessUnitDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, BusinessUnitDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, BusinessUnitDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, BusinessUnitDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(BusinessUnitDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("BusinessUnit.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(BusinessUnitDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("BusinessUnit.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(BusinessUnitDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.BusinessUnitDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   BusinessUnitId  = data.BusinessUnitId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(BusinessUnitDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new BusinessUnitDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
