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
	public partial class PriceScheduleXPriceListDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PriceScheduleXPriceListDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PriceScheduleXPriceList");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PriceScheduleXPriceListDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PriceScheduleXPriceListDataModel.DataColumns.PriceScheduleXPriceListId:
					if (data.PriceScheduleXPriceListId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PriceScheduleXPriceListDataModel.DataColumns.PriceScheduleXPriceListId, data.PriceScheduleXPriceListId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceScheduleXPriceListDataModel.DataColumns.PriceScheduleXPriceListId);
					}
					break;

				case PriceScheduleXPriceListDataModel.DataColumns.PriceScheduleId:
					if (data.PriceScheduleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PriceScheduleXPriceListDataModel.DataColumns.PriceScheduleId, data.PriceScheduleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceScheduleXPriceListDataModel.DataColumns.PriceScheduleId);
					}
					break;

				case PriceScheduleXPriceListDataModel.DataColumns.PriceListId:
					if (data.PriceListId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PriceScheduleXPriceListDataModel.DataColumns.PriceListId, data.PriceListId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceScheduleXPriceListDataModel.DataColumns.PriceListId);
					}
					break;

				case PriceScheduleXPriceListDataModel.DataColumns.PriceSchedule:
					if (!string.IsNullOrEmpty(data.PriceSchedule))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PriceScheduleXPriceListDataModel.DataColumns.PriceSchedule, data.PriceSchedule);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceScheduleXPriceListDataModel.DataColumns.PriceSchedule);
					}
					break;

				case PriceScheduleXPriceListDataModel.DataColumns.PriceList:
					if (!string.IsNullOrEmpty(data.PriceList))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PriceScheduleXPriceListDataModel.DataColumns.PriceList, data.PriceList);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PriceScheduleXPriceListDataModel.DataColumns.PriceList);
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

		public static List<PriceScheduleXPriceListDataModel> GetEntityDetails(PriceScheduleXPriceListDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PriceScheduleXPriceListSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PriceScheduleXPriceListId           = dataQuery.PriceScheduleXPriceListId
				 ,	PriceScheduleId           = dataQuery.PriceScheduleId
				 ,	PriceListId           = dataQuery.PriceListId
			};

			List<PriceScheduleXPriceListDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PriceScheduleXPriceListDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PriceScheduleXPriceListDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PriceScheduleXPriceListDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PriceScheduleXPriceListDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PriceScheduleXPriceListDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PriceScheduleXPriceListInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PriceScheduleXPriceListUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PriceScheduleXPriceListDataModel.DataColumns.PriceScheduleXPriceListId); 
			sql = sql + ", " + ToSQLParameter(data, PriceScheduleXPriceListDataModel.DataColumns.PriceScheduleId); 
			sql = sql + ", " + ToSQLParameter(data, PriceScheduleXPriceListDataModel.DataColumns.PriceListId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PriceScheduleXPriceListDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PriceScheduleXPriceList.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PriceScheduleXPriceListDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PriceScheduleXPriceList.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PriceScheduleXPriceListDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PriceScheduleXPriceListDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				 ,	PriceScheduleXPriceListId           = data.PriceScheduleXPriceListId
				 ,	PriceScheduleId           = data.PriceScheduleId
				 ,	PriceListId           = data.PriceListId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PriceScheduleXPriceListDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PriceScheduleXPriceListDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
