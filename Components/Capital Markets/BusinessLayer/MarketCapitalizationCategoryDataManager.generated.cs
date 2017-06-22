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
	public partial class MarketCapitalizationCategoryDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static MarketCapitalizationCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MarketCapitalizationCategory");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(MarketCapitalizationCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case MarketCapitalizationCategoryDataModel.DataColumns.MarketCapitalizationCategoryId:
					if (data.MarketCapitalizationCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MarketCapitalizationCategoryDataModel.DataColumns.MarketCapitalizationCategoryId, data.MarketCapitalizationCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MarketCapitalizationCategoryDataModel.DataColumns.MarketCapitalizationCategoryId);
					}
					break;

				case MarketCapitalizationCategoryDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MarketCapitalizationCategoryDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MarketCapitalizationCategoryDataModel.DataColumns.Name);
					}
					break;

				case MarketCapitalizationCategoryDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MarketCapitalizationCategoryDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MarketCapitalizationCategoryDataModel.DataColumns.Description);
					}
					break;

				case MarketCapitalizationCategoryDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MarketCapitalizationCategoryDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MarketCapitalizationCategoryDataModel.DataColumns.SortOrder);
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

		public static List<MarketCapitalizationCategoryDataModel> GetEntityDetails(MarketCapitalizationCategoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MarketCapitalizationCategorySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	MarketCapitalizationCategoryId           = dataQuery.MarketCapitalizationCategoryId
				 ,	Name           = dataQuery.Name
			};

			List<MarketCapitalizationCategoryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MarketCapitalizationCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<MarketCapitalizationCategoryDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(MarketCapitalizationCategoryDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(MarketCapitalizationCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(MarketCapitalizationCategoryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.MarketCapitalizationCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MarketCapitalizationCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, MarketCapitalizationCategoryDataModel.DataColumns.MarketCapitalizationCategoryId); 
			sql = sql + ", " + ToSQLParameter(data, MarketCapitalizationCategoryDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, MarketCapitalizationCategoryDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, MarketCapitalizationCategoryDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(MarketCapitalizationCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("MarketCapitalizationCategory.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(MarketCapitalizationCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("MarketCapitalizationCategory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MarketCapitalizationCategoryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.MarketCapitalizationCategoryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   MarketCapitalizationCategoryId  = data.MarketCapitalizationCategoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(MarketCapitalizationCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MarketCapitalizationCategoryDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
