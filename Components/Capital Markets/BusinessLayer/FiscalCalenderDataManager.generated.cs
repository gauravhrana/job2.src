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
	public partial class FiscalCalenderDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FiscalCalenderDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FiscalCalender");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FiscalCalenderDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FiscalCalenderDataModel.DataColumns.FiscalCalenderId:
					if (data.FiscalCalenderId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FiscalCalenderDataModel.DataColumns.FiscalCalenderId, data.FiscalCalenderId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FiscalCalenderDataModel.DataColumns.FiscalCalenderId);
					}
					break;

				case FiscalCalenderDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FiscalCalenderDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FiscalCalenderDataModel.DataColumns.Name);
					}
					break;

				case FiscalCalenderDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FiscalCalenderDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FiscalCalenderDataModel.DataColumns.Description);
					}
					break;

				case FiscalCalenderDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FiscalCalenderDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FiscalCalenderDataModel.DataColumns.SortOrder);
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

		public static List<FiscalCalenderDataModel> GetEntityDetails(FiscalCalenderDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FiscalCalenderSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FiscalCalenderId           = dataQuery.FiscalCalenderId
				 ,	Name           = dataQuery.Name
			};

			List<FiscalCalenderDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FiscalCalenderDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FiscalCalenderDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FiscalCalenderDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FiscalCalenderDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(FiscalCalenderDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FiscalCalenderInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FiscalCalenderUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FiscalCalenderDataModel.DataColumns.FiscalCalenderId); 
			sql = sql + ", " + ToSQLParameter(data, FiscalCalenderDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, FiscalCalenderDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, FiscalCalenderDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FiscalCalenderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FiscalCalender.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FiscalCalenderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FiscalCalender.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FiscalCalenderDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FiscalCalenderDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FiscalCalenderId  = data.FiscalCalenderId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FiscalCalenderDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FiscalCalenderDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
