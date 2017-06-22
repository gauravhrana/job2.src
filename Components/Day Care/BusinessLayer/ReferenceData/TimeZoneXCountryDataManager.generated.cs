using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.ReferenceData;

namespace ReferenceData.Components.BusinessLayer
{
	public partial class TimeZoneXCountryDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TimeZoneXCountryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TimeZoneXCountry");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TimeZoneXCountryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TimeZoneXCountryDataModel.DataColumns.TimeZoneXCountryId:
					if (data.TimeZoneXCountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TimeZoneXCountryDataModel.DataColumns.TimeZoneXCountryId, data.TimeZoneXCountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneXCountryDataModel.DataColumns.TimeZoneXCountryId);
					}
					break;

				case TimeZoneXCountryDataModel.DataColumns.TimeZoneId:
					if (data.TimeZoneId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TimeZoneXCountryDataModel.DataColumns.TimeZoneId, data.TimeZoneId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneXCountryDataModel.DataColumns.TimeZoneId);
					}
					break;

				case TimeZoneXCountryDataModel.DataColumns.CountryId:
					if (data.CountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TimeZoneXCountryDataModel.DataColumns.CountryId, data.CountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneXCountryDataModel.DataColumns.CountryId);
					}
					break;

				case TimeZoneXCountryDataModel.DataColumns.TimeZone:
					if (!string.IsNullOrEmpty(data.TimeZone))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TimeZoneXCountryDataModel.DataColumns.TimeZone, data.TimeZone);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneXCountryDataModel.DataColumns.TimeZone);
					}
					break;

				case TimeZoneXCountryDataModel.DataColumns.Country:
					if (!string.IsNullOrEmpty(data.Country))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TimeZoneXCountryDataModel.DataColumns.Country, data.Country);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneXCountryDataModel.DataColumns.Country);
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

		public static List<TimeZoneXCountryDataModel> GetEntityDetails(TimeZoneXCountryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TimeZoneXCountrySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TimeZoneXCountryId           = dataQuery.TimeZoneXCountryId
				 ,	TimeZoneId           = dataQuery.TimeZoneId
				 ,	CountryId           = dataQuery.CountryId
			};

			List<TimeZoneXCountryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TimeZoneXCountryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TimeZoneXCountryDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TimeZoneXCountryDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TimeZoneXCountryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TimeZoneXCountryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TimeZoneXCountryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TimeZoneXCountryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TimeZoneXCountryDataModel.DataColumns.TimeZoneXCountryId); 
			sql = sql + ", " + ToSQLParameter(data, TimeZoneXCountryDataModel.DataColumns.TimeZoneId); 
			sql = sql + ", " + ToSQLParameter(data, TimeZoneXCountryDataModel.DataColumns.CountryId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TimeZoneXCountryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TimeZoneXCountry.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TimeZoneXCountryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TimeZoneXCountry.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TimeZoneXCountryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TimeZoneXCountryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				 ,	TimeZoneXCountryId           = data.TimeZoneXCountryId
				 ,	TimeZoneId           = data.TimeZoneId
				 ,	CountryId           = data.CountryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TimeZoneXCountryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TimeZoneXCountryDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
