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
	public partial class FinancialAccountClassDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FinancialAccountClassDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FinancialAccountClass");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FinancialAccountClassDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FinancialAccountClassDataModel.DataColumns.FinancialAccountClassId:
					if (data.FinancialAccountClassId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FinancialAccountClassDataModel.DataColumns.FinancialAccountClassId, data.FinancialAccountClassId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FinancialAccountClassDataModel.DataColumns.FinancialAccountClassId);
					}
					break;

				case FinancialAccountClassDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FinancialAccountClassDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FinancialAccountClassDataModel.DataColumns.Name);
					}
					break;

				case FinancialAccountClassDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FinancialAccountClassDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FinancialAccountClassDataModel.DataColumns.Description);
					}
					break;

				case FinancialAccountClassDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FinancialAccountClassDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FinancialAccountClassDataModel.DataColumns.SortOrder);
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

		public static List<FinancialAccountClassDataModel> GetEntityDetails(FinancialAccountClassDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FinancialAccountClassSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FinancialAccountClassId           = dataQuery.FinancialAccountClassId
				 ,	Name           = dataQuery.Name
			};

			List<FinancialAccountClassDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FinancialAccountClassDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FinancialAccountClassDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FinancialAccountClassDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FinancialAccountClassDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(FinancialAccountClassDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FinancialAccountClassInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FinancialAccountClassUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FinancialAccountClassDataModel.DataColumns.FinancialAccountClassId); 
			sql = sql + ", " + ToSQLParameter(data, FinancialAccountClassDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, FinancialAccountClassDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, FinancialAccountClassDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FinancialAccountClassDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FinancialAccountClass.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FinancialAccountClassDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FinancialAccountClass.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FinancialAccountClassDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FinancialAccountClassDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FinancialAccountClassId  = data.FinancialAccountClassId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FinancialAccountClassDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FinancialAccountClassDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
