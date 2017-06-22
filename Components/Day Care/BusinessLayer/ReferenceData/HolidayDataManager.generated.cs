using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.ReferenceData;

namespace ReferenceData.Components.BusinessLayer
{
	public partial class HolidayDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static HolidayDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Holiday");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(HolidayDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case HolidayDataModel.DataColumns.HolidayId:
					if (data.HolidayId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, HolidayDataModel.DataColumns.HolidayId, data.HolidayId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayDataModel.DataColumns.HolidayId);
					}
					break;

				case HolidayDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, HolidayDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayDataModel.DataColumns.Name);
					}
					break;

				case HolidayDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, HolidayDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayDataModel.DataColumns.Description);
					}
					break;

				case HolidayDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, HolidayDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayDataModel.DataColumns.SortOrder);
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

		public static List<HolidayDataModel> GetEntityDetails(HolidayDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.HolidaySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	HolidayId           = dataQuery.HolidayId
				 ,	Name           = dataQuery.Name
			};

			List<HolidayDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<HolidayDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<HolidayDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(HolidayDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(HolidayDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(HolidayDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.HolidayInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.HolidayUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, HolidayDataModel.DataColumns.HolidayId); 
			sql = sql + ", " + ToSQLParameter(data, HolidayDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, HolidayDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, HolidayDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(HolidayDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Holiday.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(HolidayDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Holiday.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(HolidayDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.HolidayDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   HolidayId  = data.HolidayId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(HolidayDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new HolidayDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
