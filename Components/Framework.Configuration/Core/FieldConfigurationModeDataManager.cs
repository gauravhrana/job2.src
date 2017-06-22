using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;
using System.Runtime.CompilerServices;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.UserPreference
{
	public partial class FieldConfigurationModeDataManager : StandardDataManager
	{
		#region Get Entity Details By Application

		public static List<FieldConfigurationModeDataModel> GetEntityDetailsByApplication(FieldConfigurationModeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FieldConfigurationModeSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				 ,
				ApplicationId = dataQuery.ApplicationId
				 ,
				ApplicationMode = requestProfile.ApplicationModeId
				 ,
				ReturnAuditInfo = returnAuditInfo
				 ,
				FieldConfigurationModeId = dataQuery.FieldConfigurationModeId
				 ,
				Name = dataQuery.Name
			};

			List<FieldConfigurationModeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FieldConfigurationModeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion


        public static int? GetFCModeIdByName(string name, RequestProfile requestProfile)
        {
            int? fcModeId = null;

            var obj = new FieldConfigurationModeDataModel();
            obj.Name = name;

            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(obj, StandardDataModel.StandardDataColumns.Name);

            var oDT = new DBDataTable("FieldConfigurationMode.Search", sql, DataStoreKey);

            if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
            {
                fcModeId = Convert.ToInt32(oDT.DBTable.Rows[0][FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId]);
            }
            return fcModeId;
        }

        

		public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
		{
			// get all records for old Application Id
			var sql = "EXEC dbo.FieldConfigurationModeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

			var oDT = new DBDataTable("FieldConfigurationMode.Search", sql, DataStoreKey);

			// formaulate a new request Profile which will have new Applicationid
			var newRequestProfile = new RequestProfile();
			newRequestProfile.ApplicationId = newApplicationId;
			newRequestProfile.AuditId = requestProfile.AuditId;

			foreach (DataRow dr in oDT.DBTable.Rows)
			{
				var data = new FieldConfigurationModeDataModel();
				data.ApplicationId = newApplicationId;
				data.Name = dr[StandardDataModel.StandardDataColumns.Name].ToString();

				// check for existing record in new Application Id
				if(!DoesExist(data, newRequestProfile))
				{
					data.Description = dr[StandardDataModel.StandardDataColumns.Description].ToString();
					data.SortOrder = Convert.ToInt32(dr[StandardDataModel.StandardDataColumns.SortOrder]);

					//create in new application id
					Create(data, newRequestProfile);

				}

			}
		}

	}
}
