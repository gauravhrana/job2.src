using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.UserPreference
{    
    public partial class FieldConfigurationModeCategoryXFCModeDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static FieldConfigurationModeCategoryXFCModeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeCategoryXFCMode");
		}

        #region ToSQLParameter

        public static string ToSQLParameter(FieldConfigurationModeCategoryXFCModeDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeCategoryXFCModeId:
                    if (data.FieldConfigurationModeCategoryXFCModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeCategoryXFCModeId, data.FieldConfigurationModeCategoryXFCModeId);
                    }

                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeCategoryXFCModeId);
                    }
                    break;

                case FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeCategoryId:
                    if (data.FieldConfigurationModeCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeCategoryId, data.FieldConfigurationModeCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeCategoryId);
                    }
                    break;

                case FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeId:
                    if (data.FieldConfigurationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeCategoryXFCModeDataModel.DataColumns.FieldConfigurationModeId);
                    }
                    break;

            }
            return returnValue;
        }

        #endregion

		#region Get Entity Details

		public static List<FieldConfigurationModeCategoryXFCModeDataModel> GetEntityDetails(FieldConfigurationModeCategoryXFCModeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FieldConfigurationModeCategoryXFCModeSearch ";

			var parameters =
			new
			{
					AuditId                             = requestProfile.AuditId
				 ,	ApplicationId                       = requestProfile.ApplicationId
				 ,	ApplicationMode                     = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo                     = returnAuditInfo
				 ,	FieldConfigurationModeCategoryId    = dataQuery.FieldConfigurationModeCategoryId
                 ,	FieldConfigurationModeId            = dataQuery.FieldConfigurationModeId
			};

			List<FieldConfigurationModeCategoryXFCModeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FieldConfigurationModeCategoryXFCModeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

        #region Create By FieldConfigurationModeCategory

        public static void CreateByFieldConfigurationModeCategory(int FieldConfigurationModeCategoryId, int[] FieldConfigurationModeIds, RequestProfile requestProfile)
		{
			foreach (var FieldConfigurationModeId in FieldConfigurationModeIds)
			{
				var sql = "EXEC dbo.FieldConfigurationModeCategoryXFCModeInsert " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
						", @FieldConfigurationModeCategoryId	= " + FieldConfigurationModeCategoryId +
						", @FieldConfigurationModeId			= " + FieldConfigurationModeId;

				DBDML.RunSQL("FieldConfigurationModeCategoryXFCMode_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By FieldConfigurationModes

		public static void CreateByFieldConfigurationMode(int FieldConfigurationModeId, int[] FieldConfigurationModeCategoryIds, RequestProfile requestProfile)
		{
			foreach (var FieldConfigurationModeCategoryId in FieldConfigurationModeCategoryIds)
			{
				var sql = "EXEC dbo.FieldConfigurationModeCategoryXFCModeInsert " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
						", @FieldConfigurationModeCategoryId		= " + FieldConfigurationModeCategoryId +
						", @FieldConfigurationModeId				= " + FieldConfigurationModeId;
				DBDML.RunSQL("FieldConfigurationModeCategoryXFCMode_Insert", sql, DataStoreKey);
			}
		}
		#endregion

		#region Get By FieldConfigurationMode

		public static DataTable GetByFieldConfigurationMode(int FieldConfigurationModeId, RequestProfile requestProfile)
		{
			
			var sql = "EXEC FieldConfigurationModeCategoryXFCModeSearch " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", @FieldConfigurationModeId	=" + FieldConfigurationModeId;

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get By FieldConfigurationModeCategory

		public static DataTable GetByFieldConfigurationModeCategory(int fieldConfigurationModeCategoryId, RequestProfile requestProfile)
		{
			var sql = "EXEC FieldConfigurationModeCategoryXFCModeSearch " +" "+
				ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId)+
				" , " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @FieldConfigurationModeCategoryId		=" + fieldConfigurationModeCategoryId;

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete By FieldConfigurationMode

		public static void DeleteByFieldConfigurationMode(int fieldConfigurationModeId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FieldConfigurationModeCategoryXFCModeDelete ";

			var parameters =	new
								{
										AuditId						= requestProfile.AuditId
									,	FieldConfigurationModeId	= fieldConfigurationModeId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Delete By FieldConfigurationModeCategory

		public static void DeleteByFieldConfigurationModeCategory(int fieldConfigurationModeCategoryId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FieldConfigurationModeCategoryXFCModeDelete ";

			var parameters =	new
								{
										AuditId								= requestProfile.AuditId
									,	FieldConfigurationModeCategoryId	= fieldConfigurationModeCategoryId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

    }

}
