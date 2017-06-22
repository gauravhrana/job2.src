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
	public partial class HolidayXCountryDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static HolidayXCountryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("HolidayXCountry");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(HolidayXCountryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case HolidayXCountryDataModel.DataColumns.HolidayXCountryId:
					if (data.HolidayXCountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, HolidayXCountryDataModel.DataColumns.HolidayXCountryId, data.HolidayXCountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayXCountryDataModel.DataColumns.HolidayXCountryId);
					}
					break;

				case HolidayXCountryDataModel.DataColumns.HolidayId:
					if (data.HolidayId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, HolidayXCountryDataModel.DataColumns.HolidayId, data.HolidayId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayXCountryDataModel.DataColumns.HolidayId);
					}
					break;

				case HolidayXCountryDataModel.DataColumns.CountryId:
					if (data.CountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, HolidayXCountryDataModel.DataColumns.CountryId, data.CountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayXCountryDataModel.DataColumns.CountryId);
					}
					break;

				case HolidayXCountryDataModel.DataColumns.Holiday:
					if (!string.IsNullOrEmpty(data.Holiday))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, HolidayXCountryDataModel.DataColumns.Holiday, data.Holiday);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayXCountryDataModel.DataColumns.Holiday);
					}
					break;

				case HolidayXCountryDataModel.DataColumns.Country:
					if (!string.IsNullOrEmpty(data.Country))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, HolidayXCountryDataModel.DataColumns.Country, data.Country);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HolidayXCountryDataModel.DataColumns.Country);
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

		public static List<HolidayXCountryDataModel> GetEntityDetails(HolidayXCountryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.HolidayXCountrySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	HolidayXCountryId           = dataQuery.HolidayXCountryId
				 ,	HolidayId           = dataQuery.HolidayId
				 ,	CountryId           = dataQuery.CountryId
			};

			List<HolidayXCountryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<HolidayXCountryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<HolidayXCountryDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(HolidayXCountryDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(HolidayXCountryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(HolidayXCountryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.HolidayXCountryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.HolidayXCountryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, HolidayXCountryDataModel.DataColumns.HolidayXCountryId); 
			sql = sql + ", " + ToSQLParameter(data, HolidayXCountryDataModel.DataColumns.HolidayId); 
			sql = sql + ", " + ToSQLParameter(data, HolidayXCountryDataModel.DataColumns.CountryId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(HolidayXCountryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("HolidayXCountry.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(HolidayXCountryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("HolidayXCountry.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(HolidayXCountryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.HolidayXCountryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				 ,	HolidayXCountryId           = data.HolidayXCountryId
				 ,	HolidayId           = data.HolidayId
				 ,	CountryId           = data.CountryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(HolidayXCountryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new HolidayXCountryDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
