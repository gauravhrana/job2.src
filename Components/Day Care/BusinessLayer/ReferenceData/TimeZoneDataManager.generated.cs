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
	public partial class TimeZoneDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TimeZoneDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TimeZone");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TimeZoneDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TimeZoneDataModel.DataColumns.TimeZoneId:
					if (data.TimeZoneId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TimeZoneDataModel.DataColumns.TimeZoneId, data.TimeZoneId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneDataModel.DataColumns.TimeZoneId);
					}
					break;

				case TimeZoneDataModel.DataColumns.TimeDifference:
					if (data.TimeDifference != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TimeZoneDataModel.DataColumns.TimeDifference, data.TimeDifference);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneDataModel.DataColumns.TimeDifference);
					}
					break;

				case TimeZoneDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TimeZoneDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneDataModel.DataColumns.Name);
					}
					break;

				case TimeZoneDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TimeZoneDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneDataModel.DataColumns.Description);
					}
					break;

				case TimeZoneDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TimeZoneDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneDataModel.DataColumns.SortOrder);
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

		public static List<TimeZoneDataModel> GetEntityDetails(TimeZoneDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TimeZoneSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TimeZoneId           = dataQuery.TimeZoneId
				 ,	Name           = dataQuery.Name
			};

			List<TimeZoneDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TimeZoneDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TimeZoneDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TimeZoneDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TimeZoneDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TimeZoneInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TimeZoneUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.TimeZoneId); 
			sql = sql + ", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.TimeDifference); 
			sql = sql + ", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TimeZone.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TimeZone.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TimeZoneDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TimeZoneDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TimeZoneId  = data.TimeZoneId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TimeZoneDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
