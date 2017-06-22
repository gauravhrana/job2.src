using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;

namespace Framework.Components.UserPreference
{
    public partial class ApplicationModeXFieldConfigurationModeDataManager : BaseDataManager
    {
        static string DataStoreKey = "";

		static ApplicationModeXFieldConfigurationModeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationModeXFieldConfigurationMode");
		}

        #region ToSQLParameter

        public static string ToSQLParameter(ApplicationModeXFieldConfigurationModeDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case ApplicationModeXFieldConfigurationModeDataModel.DataColumns.ApplicationModeXFieldConfigurationModeId:
                    if (data.ApplicationModeXFieldConfigurationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationModeXFieldConfigurationModeDataModel.DataColumns.ApplicationModeXFieldConfigurationModeId, data.ApplicationModeXFieldConfigurationModeId);
                    }

                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationModeXFieldConfigurationModeDataModel.DataColumns.ApplicationModeXFieldConfigurationModeId);
                    }
                    break;

                case ApplicationModeXFieldConfigurationModeDataModel.DataColumns.ApplicationModeId:
                    if (data.ApplicationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationModeXFieldConfigurationModeDataModel.DataColumns.ApplicationModeId, data.ApplicationModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationModeXFieldConfigurationModeDataModel.DataColumns.ApplicationModeId);
                    }
                    break;

                case ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId:
                    if (data.FieldConfigurationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);
                    }
                    break;

            }
            return returnValue;
        }
        
        #endregion

        #region Create By ApplicationMode

        public static void CreateByApplicationMode(int ApplicationModeId, int[] FieldConfigurationModeIds, RequestProfile requestProfile)
		{
			foreach (var FieldConfigurationModeId in FieldConfigurationModeIds)
			{
                var sql = "EXEC dbo.ApplicationModeXFieldConfigurationModeInsert " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                        ", @ApplicationModeId		            = " + ApplicationModeId +
                        ", @FieldConfigurationModeId			= " + FieldConfigurationModeId;

				DBDML.RunSQL("ApplicationModeXFieldConfigurationMode_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Get Entity Details

		public static List<ApplicationModeXFieldConfigurationModeDataModel> GetEntityDetails(ApplicationModeXFieldConfigurationModeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationModeXFieldConfigurationModeSearch ";

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
				ApplicationModeId = dataQuery.ApplicationModeId
				 ,
				FieldConfigurationModeId = dataQuery.FieldConfigurationModeId
			};

			List<ApplicationModeXFieldConfigurationModeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationModeXFieldConfigurationModeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Search

		public static DataTable Search(ApplicationModeXFieldConfigurationModeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion


		#region Create

		public static void Create(ApplicationModeXFieldConfigurationModeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationModeXFieldConfigurationModeInsert " +
					" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId) +
					", " + ToSQLParameter(data,ApplicationModeXFieldConfigurationModeDataModel.DataColumns.ApplicationModeId) +
					", " + ToSQLParameter(data,ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);
					

				DBDML.RunSQL("ApplicationModeXFieldConfigurationMode_Insert", sql, DataStoreKey);
			
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ApplicationModeXFieldConfigurationModeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ApplicationModeXFieldConfigurationModeDataModel();
			doesExistRequest.ApplicationModeId = data.ApplicationModeId;
			doesExistRequest.FieldConfigurationModeId = data.FieldConfigurationModeId;
			doesExistRequest.ApplicationId = data.ApplicationId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region Create By FieldConfigurationModes

		public static void CreateByFieldConfigurationMode(int FieldConfigurationModeId, int[] ApplicationModeIds, RequestProfile requestProfile)
		{
			foreach (var ApplicationModeId in ApplicationModeIds)
			{
				var sql = "EXEC dbo.ApplicationModeXFieldConfigurationModeInsert " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
						", @ApplicationModeId					= " + ApplicationModeId + 
						", @FieldConfigurationModeId			= " + FieldConfigurationModeId ;
				DBDML.RunSQL("ApplicationModeXFieldConfigurationMode_Insert", sql, DataStoreKey);
			}
		}
		#endregion

		#region Get By ApplicationMode

		public static DataTable GetByApplicationMode(int applicationModeId, RequestProfile requestProfile)
		{
            var sql = "EXEC ApplicationModeXFieldConfigurationModeSearch" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationModeId		=" + applicationModeId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationModeXFieldConfigurationMode");
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

        #endregion

        #region Get By ApplicationMode

        public static DataTable GetByFieldConfigurationMode(int fieldConfigurationModeId, RequestProfile requestProfile)
        {
            var sql = "EXEC ApplicationModeXFieldConfigurationModeSearch" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @FieldConfigurationModeId		=" + fieldConfigurationModeId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationModeXFieldConfigurationMode");
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

		#region Delete By FieldConfigurationMode

		public static void DeleteByFieldConfigurationMode(int fieldConfigurationModeId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ApplicationModeXFieldConfigurationModeDelete ";

			var parameters =	new
								{
										AuditId								= requestProfile.AuditId
									,	FieldConfigurationModeId			= fieldConfigurationModeId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Delete By ApplicationMode

		public static void DeleteByApplicationMode(int applicationModeId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ApplicationModeXFieldConfigurationModeDelete ";

			var parameters =	new
								{
										AuditId								= requestProfile.AuditId
									,	ApplicationModeId					= applicationModeId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
		{
			//get all FCMode records for new Application Id

			var dataFC = new FieldConfigurationModeDataModel();

			dataFC.ApplicationId = newApplicationId;

			var dtFC = FieldConfigurationModeDataManager.Search(dataFC, requestProfile);

			//get all ApplicationMode records for new Application Id

			var dataAM = new ApplicationModeDataModel();

			dataAM.ApplicationId = newApplicationId;

			var dtAM = ApplicationModeDataManager.Search(dataAM, requestProfile);

			var newRequestProfile = new RequestProfile();
			newRequestProfile.ApplicationId = newApplicationId;
			newRequestProfile.AuditId = requestProfile.AuditId;

			foreach (DataRow dr in dtAM.Rows)
			{
				foreach (DataRow drFC in dtFC.Rows)
				{
					var data = new ApplicationModeXFieldConfigurationModeDataModel();

					data.ApplicationId = newApplicationId;
					data.ApplicationModeId = Convert.ToInt32(dr[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.ApplicationModeId]);
					data.FieldConfigurationModeId = Convert.ToInt32(drFC[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId]);

					if (!DoesExist(data, newRequestProfile))
					{						
						//create in new application id
						Create(data, newRequestProfile);

					}

				}
			}
		}	

    }
}
