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
	public partial class WithholdingTaxTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static WithholdingTaxTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("WithholdingTaxType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(WithholdingTaxTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case WithholdingTaxTypeDataModel.DataColumns.WithholdingTaxTypeId:
					if (data.WithholdingTaxTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, WithholdingTaxTypeDataModel.DataColumns.WithholdingTaxTypeId, data.WithholdingTaxTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, WithholdingTaxTypeDataModel.DataColumns.WithholdingTaxTypeId);
					}
					break;

				case WithholdingTaxTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, WithholdingTaxTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, WithholdingTaxTypeDataModel.DataColumns.Name);
					}
					break;

				case WithholdingTaxTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, WithholdingTaxTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, WithholdingTaxTypeDataModel.DataColumns.Description);
					}
					break;

				case WithholdingTaxTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, WithholdingTaxTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, WithholdingTaxTypeDataModel.DataColumns.SortOrder);
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

		public static List<WithholdingTaxTypeDataModel> GetEntityDetails(WithholdingTaxTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.WithholdingTaxTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	WithholdingTaxTypeId           = dataQuery.WithholdingTaxTypeId
				 ,	Name           = dataQuery.Name
			};

			List<WithholdingTaxTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<WithholdingTaxTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<WithholdingTaxTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(WithholdingTaxTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(WithholdingTaxTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(WithholdingTaxTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.WithholdingTaxTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.WithholdingTaxTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, WithholdingTaxTypeDataModel.DataColumns.WithholdingTaxTypeId); 
			sql = sql + ", " + ToSQLParameter(data, WithholdingTaxTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, WithholdingTaxTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, WithholdingTaxTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(WithholdingTaxTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("WithholdingTaxType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(WithholdingTaxTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("WithholdingTaxType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(WithholdingTaxTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.WithholdingTaxTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   WithholdingTaxTypeId  = data.WithholdingTaxTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(WithholdingTaxTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new WithholdingTaxTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
