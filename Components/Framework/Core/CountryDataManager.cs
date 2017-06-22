using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class CountryDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static CountryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Country");
		}

		#region GetList

        public static List<CountryDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(CountryDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(CountryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<CountryDataModel> GetEntityDetails(CountryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CountrySearch ";

			var parameters =
			new
			{
				    AuditId         = requestProfile.AuditId
				,   CountryId       = dataQuery.CountryId
				,   Name            = dataQuery.Name
				//,   TimeZoneId      = dataQuery.TimeZoneId
                //,   ApplicationMode = requestProfile.ApplicationModeId
                //,   ReturnAuditInfo = returnAuditInfo
			};

			List<CountryDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<CountryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(CountryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("Country.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(CountryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("Country.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CountryDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.CountryDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				CountryId = dataQuery.CountryId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(CountryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CountryDataModel.DataColumns.CountryId:
					if (data.CountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CountryDataModel.DataColumns.CountryId, data.CountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CountryDataModel.DataColumns.CountryId);
					}
					break;

				case CountryDataModel.DataColumns.TimeZoneId:
					if (data.TimeZoneId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CountryDataModel.DataColumns.TimeZoneId, data.TimeZoneId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CountryDataModel.DataColumns.TimeZoneId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(CountryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(CountryDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.CountryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CountryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, CountryDataModel.DataColumns.CountryId) +
						", " + ToSQLParameter(data, CountryDataModel.DataColumns.Name) +
						", " + ToSQLParameter(data, CountryDataModel.DataColumns.TimeZoneId) +
						", " + ToSQLParameter(data, CountryDataModel.DataColumns.Description) +
						", " + ToSQLParameter(data, CountryDataModel.DataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(CountryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest           = new CountryDataModel();
			doesExistRequest.Name          = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

        #endregion

        public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
        {
            // get all records for old Application Id
            var sql = "EXEC dbo.CountrySearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

            var oDT = new DBDataTable("Country.Search", sql, DataStoreKey);

            // formaulate a new request Profile which will have new Applicationid
            var newRequestProfile = new RequestProfile();
            newRequestProfile.ApplicationId = newApplicationId;
            newRequestProfile.AuditId = requestProfile.AuditId;

            var timeZoneData = new TimeZoneDataModel();
            var timeZoneList = TimeZoneDataManger.GetEntityDetails(timeZoneData, newRequestProfile);

            foreach (DataRow dr in oDT.DBTable.Rows)
            {
                var fcModeName = dr[CountryDataModel.DataColumns.TimeZone].ToString();

                //get new fc mode id based on fc mode name
                var newTimeZoneId  = timeZoneList.Find(x => x.Name == fcModeName).TimeZoneId;

                var data           = new CountryDataModel();
                data.ApplicationId = newApplicationId;
                data.Name          = dr[CountryDataModel.DataColumns.Name].ToString();

                // check for existing record in new Application Id
                if(!DoesExist(data, newRequestProfile))
                {

                    data.TimeZoneId  = newTimeZoneId;
                    data.Description = dr[CountryDataModel.DataColumns.Description].ToString();
                    data.SortOrder   = Convert.ToInt32(dr[CountryDataModel.DataColumns.SortOrder]);

                    //create in new application id
                    Create(data, newRequestProfile);
                }

            }
        }



	}
}
