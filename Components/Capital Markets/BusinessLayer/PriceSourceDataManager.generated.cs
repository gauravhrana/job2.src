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
	public partial class PriceSourceDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PriceSourceDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PriceSource");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PriceSourceDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PriceSourceDataModel.DataColumns.PriceSourceId:
					if (data.PriceSourceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PriceSourceDataModel.DataColumns.PriceSourceId, data.PriceSourceId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceSourceDataModel.DataColumns.PriceSourceId);
					}
					break;

				case PriceSourceDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PriceSourceDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceSourceDataModel.DataColumns.Name);
					}
					break;

				case PriceSourceDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PriceSourceDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceSourceDataModel.DataColumns.Description);
					}
					break;

				case PriceSourceDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PriceSourceDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceSourceDataModel.DataColumns.SortOrder);
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

		public static List<PriceSourceDataModel> GetEntityDetails(PriceSourceDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PriceSourceSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PriceSourceId           = dataQuery.PriceSourceId
				 ,	Name           = dataQuery.Name
			};

			List<PriceSourceDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PriceSourceDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PriceSourceDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PriceSourceDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PriceSourceDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PriceSourceDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PriceSourceInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PriceSourceUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PriceSourceDataModel.DataColumns.PriceSourceId); 
			sql = sql + ", " + ToSQLParameter(data, PriceSourceDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, PriceSourceDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, PriceSourceDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PriceSourceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PriceSource.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PriceSourceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PriceSource.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PriceSourceDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PriceSourceDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PriceSourceId  = data.PriceSourceId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PriceSourceDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PriceSourceDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
