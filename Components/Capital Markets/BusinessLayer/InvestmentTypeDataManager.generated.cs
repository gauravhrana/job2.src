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
	public partial class InvestmentTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static InvestmentTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("InvestmentType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(InvestmentTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case InvestmentTypeDataModel.DataColumns.InvestmentTypeId:
					if (data.InvestmentTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestmentTypeDataModel.DataColumns.InvestmentTypeId, data.InvestmentTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentTypeDataModel.DataColumns.InvestmentTypeId);
					}
					break;

				case InvestmentTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestmentTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentTypeDataModel.DataColumns.Name);
					}
					break;

				case InvestmentTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestmentTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentTypeDataModel.DataColumns.Description);
					}
					break;

				case InvestmentTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestmentTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentTypeDataModel.DataColumns.SortOrder);
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

		public static List<InvestmentTypeDataModel> GetEntityDetails(InvestmentTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.InvestmentTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	InvestmentTypeId           = dataQuery.InvestmentTypeId
				 ,	Name           = dataQuery.Name
			};

			List<InvestmentTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<InvestmentTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<InvestmentTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(InvestmentTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(InvestmentTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(InvestmentTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.InvestmentTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.InvestmentTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, InvestmentTypeDataModel.DataColumns.InvestmentTypeId); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(InvestmentTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("InvestmentType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(InvestmentTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("InvestmentType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(InvestmentTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.InvestmentTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   InvestmentTypeId  = data.InvestmentTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(InvestmentTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new InvestmentTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
