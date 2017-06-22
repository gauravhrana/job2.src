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
	public partial class SubBusinessUnitDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SubBusinessUnitDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SubBusinessUnit");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SubBusinessUnitDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SubBusinessUnitDataModel.DataColumns.SubBusinessUnitId:
					if (data.SubBusinessUnitId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SubBusinessUnitDataModel.DataColumns.SubBusinessUnitId, data.SubBusinessUnitId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubBusinessUnitDataModel.DataColumns.SubBusinessUnitId);
					}
					break;

				case SubBusinessUnitDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SubBusinessUnitDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubBusinessUnitDataModel.DataColumns.FundId);
					}
					break;

				case SubBusinessUnitDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SubBusinessUnitDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubBusinessUnitDataModel.DataColumns.Fund);
					}
					break;

				case SubBusinessUnitDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SubBusinessUnitDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubBusinessUnitDataModel.DataColumns.Name);
					}
					break;

				case SubBusinessUnitDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SubBusinessUnitDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubBusinessUnitDataModel.DataColumns.Description);
					}
					break;

				case SubBusinessUnitDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SubBusinessUnitDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubBusinessUnitDataModel.DataColumns.SortOrder);
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

		public static List<SubBusinessUnitDataModel> GetEntityDetails(SubBusinessUnitDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SubBusinessUnitSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SubBusinessUnitId           = dataQuery.SubBusinessUnitId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<SubBusinessUnitDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SubBusinessUnitDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SubBusinessUnitDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SubBusinessUnitDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SubBusinessUnitDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SubBusinessUnitDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SubBusinessUnitInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SubBusinessUnitUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SubBusinessUnitDataModel.DataColumns.SubBusinessUnitId); 
			sql = sql + ", " + ToSQLParameter(data, SubBusinessUnitDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, SubBusinessUnitDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, SubBusinessUnitDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, SubBusinessUnitDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SubBusinessUnitDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SubBusinessUnit.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SubBusinessUnitDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SubBusinessUnit.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SubBusinessUnitDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SubBusinessUnitDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SubBusinessUnitId  = data.SubBusinessUnitId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SubBusinessUnitDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SubBusinessUnitDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
