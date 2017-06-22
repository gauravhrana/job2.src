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
	public partial class BusinessCalenderDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static BusinessCalenderDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("BusinessCalender");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(BusinessCalenderDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BusinessCalenderDataModel.DataColumns.BusinessCalenderId:
					if (data.BusinessCalenderId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BusinessCalenderDataModel.DataColumns.BusinessCalenderId, data.BusinessCalenderId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessCalenderDataModel.DataColumns.BusinessCalenderId);
					}
					break;

				case BusinessCalenderDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BusinessCalenderDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessCalenderDataModel.DataColumns.Name);
					}
					break;

				case BusinessCalenderDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BusinessCalenderDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessCalenderDataModel.DataColumns.Description);
					}
					break;

				case BusinessCalenderDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BusinessCalenderDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BusinessCalenderDataModel.DataColumns.SortOrder);
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

		public static List<BusinessCalenderDataModel> GetEntityDetails(BusinessCalenderDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.BusinessCalenderSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	BusinessCalenderId           = dataQuery.BusinessCalenderId
				 ,	Name           = dataQuery.Name
			};

			List<BusinessCalenderDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<BusinessCalenderDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<BusinessCalenderDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(BusinessCalenderDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(BusinessCalenderDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(BusinessCalenderDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.BusinessCalenderInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.BusinessCalenderUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, BusinessCalenderDataModel.DataColumns.BusinessCalenderId); 
			sql = sql + ", " + ToSQLParameter(data, BusinessCalenderDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, BusinessCalenderDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, BusinessCalenderDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(BusinessCalenderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("BusinessCalender.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(BusinessCalenderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("BusinessCalender.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(BusinessCalenderDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.BusinessCalenderDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   BusinessCalenderId  = data.BusinessCalenderId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(BusinessCalenderDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new BusinessCalenderDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}