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
	public partial class RatingServicesDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static RatingServicesDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("RatingServices");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(RatingServicesDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case RatingServicesDataModel.DataColumns.RatingServicesId:
					if (data.RatingServicesId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RatingServicesDataModel.DataColumns.RatingServicesId, data.RatingServicesId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingServicesDataModel.DataColumns.RatingServicesId);
					}
					break;

				case RatingServicesDataModel.DataColumns.Url:
					if (!string.IsNullOrEmpty(data.Url))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RatingServicesDataModel.DataColumns.Url, data.Url);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingServicesDataModel.DataColumns.Url);
					}
					break;

				case RatingServicesDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RatingServicesDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingServicesDataModel.DataColumns.Code);
					}
					break;

				case RatingServicesDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RatingServicesDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingServicesDataModel.DataColumns.Name);
					}
					break;

				case RatingServicesDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RatingServicesDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingServicesDataModel.DataColumns.Description);
					}
					break;

				case RatingServicesDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RatingServicesDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingServicesDataModel.DataColumns.SortOrder);
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

		public static List<RatingServicesDataModel> GetEntityDetails(RatingServicesDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.RatingServicesSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	RatingServicesId           = dataQuery.RatingServicesId
				 ,	Name           = dataQuery.Name
			};

			List<RatingServicesDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<RatingServicesDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<RatingServicesDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(RatingServicesDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(RatingServicesDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(RatingServicesDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(RatingServicesDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.RatingServicesInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.RatingServicesUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, RatingServicesDataModel.DataColumns.RatingServicesId); 
			sql = sql + ", " + ToSQLParameter(data, RatingServicesDataModel.DataColumns.Url); 
			sql = sql + ", " + ToSQLParameter(data, RatingServicesDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, RatingServicesDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, RatingServicesDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, RatingServicesDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(RatingServicesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("RatingServices.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(RatingServicesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("RatingServices.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(RatingServicesDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.RatingServicesDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   RatingServicesId  = data.RatingServicesId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(RatingServicesDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new RatingServicesDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
