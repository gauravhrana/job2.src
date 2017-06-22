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
	public partial class PriceProviderDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PriceProviderDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PriceProvider");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PriceProviderDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PriceProviderDataModel.DataColumns.PriceProviderId:
					if (data.PriceProviderId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PriceProviderDataModel.DataColumns.PriceProviderId, data.PriceProviderId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceProviderDataModel.DataColumns.PriceProviderId);
					}
					break;

				case PriceProviderDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PriceProviderDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceProviderDataModel.DataColumns.Name);
					}
					break;

				case PriceProviderDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PriceProviderDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceProviderDataModel.DataColumns.Description);
					}
					break;

				case PriceProviderDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PriceProviderDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceProviderDataModel.DataColumns.SortOrder);
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

		public static List<PriceProviderDataModel> GetEntityDetails(PriceProviderDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PriceProviderSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PriceProviderId           = dataQuery.PriceProviderId
				 ,	Name           = dataQuery.Name
			};

			List<PriceProviderDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PriceProviderDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PriceProviderDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PriceProviderDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PriceProviderDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PriceProviderDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PriceProviderInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PriceProviderUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PriceProviderDataModel.DataColumns.PriceProviderId); 
			sql = sql + ", " + ToSQLParameter(data, PriceProviderDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, PriceProviderDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, PriceProviderDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PriceProviderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PriceProvider.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PriceProviderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PriceProvider.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PriceProviderDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PriceProviderDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PriceProviderId  = data.PriceProviderId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PriceProviderDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PriceProviderDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
