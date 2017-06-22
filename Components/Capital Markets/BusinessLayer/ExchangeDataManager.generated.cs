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
	public partial class ExchangeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ExchangeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Exchange");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ExchangeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ExchangeDataModel.DataColumns.ExchangeId:
					if (data.ExchangeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ExchangeDataModel.DataColumns.ExchangeId, data.ExchangeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ExchangeDataModel.DataColumns.ExchangeId);
					}
					break;

				case ExchangeDataModel.DataColumns.Url:
					if (!string.IsNullOrEmpty(data.Url))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ExchangeDataModel.DataColumns.Url, data.Url);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ExchangeDataModel.DataColumns.Url);
					}
					break;

				case ExchangeDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ExchangeDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ExchangeDataModel.DataColumns.Code);
					}
					break;

				case ExchangeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ExchangeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ExchangeDataModel.DataColumns.Name);
					}
					break;

				case ExchangeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ExchangeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ExchangeDataModel.DataColumns.Description);
					}
					break;

				case ExchangeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ExchangeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ExchangeDataModel.DataColumns.SortOrder);
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

		public static List<ExchangeDataModel> GetEntityDetails(ExchangeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ExchangeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ExchangeId           = dataQuery.ExchangeId
				 ,	Name           = dataQuery.Name
			};

			List<ExchangeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ExchangeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ExchangeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ExchangeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ExchangeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(ExchangeDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(ExchangeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.ExchangeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ExchangeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ExchangeDataModel.DataColumns.ExchangeId); 
			sql = sql + ", " + ToSQLParameter(data, ExchangeDataModel.DataColumns.Url); 
			sql = sql + ", " + ToSQLParameter(data, ExchangeDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, ExchangeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ExchangeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, ExchangeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ExchangeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Exchange.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ExchangeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Exchange.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ExchangeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ExchangeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ExchangeId  = data.ExchangeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ExchangeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ExchangeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
