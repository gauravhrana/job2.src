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
	public partial class TaxAccountTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TaxAccountTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaxAccountType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TaxAccountTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaxAccountTypeDataModel.DataColumns.TaxAccountTypeId:
					if (data.TaxAccountTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaxAccountTypeDataModel.DataColumns.TaxAccountTypeId, data.TaxAccountTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaxAccountTypeDataModel.DataColumns.TaxAccountTypeId);
					}
					break;

				case TaxAccountTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaxAccountTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaxAccountTypeDataModel.DataColumns.Name);
					}
					break;

				case TaxAccountTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaxAccountTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaxAccountTypeDataModel.DataColumns.Description);
					}
					break;

				case TaxAccountTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaxAccountTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaxAccountTypeDataModel.DataColumns.SortOrder);
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

		public static List<TaxAccountTypeDataModel> GetEntityDetails(TaxAccountTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TaxAccountTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TaxAccountTypeId           = dataQuery.TaxAccountTypeId
				 ,	Name           = dataQuery.Name
			};

			List<TaxAccountTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TaxAccountTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TaxAccountTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TaxAccountTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TaxAccountTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TaxAccountTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TaxAccountTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaxAccountTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TaxAccountTypeDataModel.DataColumns.TaxAccountTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TaxAccountTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, TaxAccountTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, TaxAccountTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TaxAccountTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TaxAccountType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TaxAccountTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TaxAccountType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TaxAccountTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TaxAccountTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TaxAccountTypeId  = data.TaxAccountTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TaxAccountTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TaxAccountTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
