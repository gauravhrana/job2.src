using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class ApplicationModeXFieldConfigurationMode:BaseClass
    {
        static string DataStoreKey = "";
		static int ApplicationId;

		static ApplicationModeXFieldConfigurationMode()
		{
			ApplicationId = SetupConfiguration.ApplicationId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationModeXFieldConfigurationMode");
		}

		#region Create By ApplicationMode

        public static void CreateByApplicationMode(int ApplicationModeId, int[] FieldConfigurationModeIds, int auditId)
		{
			foreach (int FieldConfigurationModeId in FieldConfigurationModeIds)
			{
                var sql = "EXEC dbo.ApplicationModeXFieldConfigurationModeInsert " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                        ", @ApplicationModeId		            = " + ApplicationModeId +
                        ", @FieldConfigurationModeId			= " + FieldConfigurationModeId;

				DataAccess.DBDML.RunSQL("ApplicationModeXFieldConfigurationMode_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By FieldConfigurationModes

		public static void CreateByFieldConfigurationMode(int FieldConfigurationModeId, int[] ApplicationModeIds, int auditId)
		{
			foreach (int ApplicationModeId in ApplicationModeIds)
			{
				var sql = "EXEC dbo.ApplicationModeXFieldConfigurationModeInsert " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
						", @ApplicationModeId					= " + ApplicationModeId + 
						", @FieldConfigurationModeId			= " + FieldConfigurationModeId ;
				DataAccess.DBDML.RunSQL("ApplicationModeXFieldConfigurationMode_Insert", sql, DataStoreKey);
			}
		}
		#endregion

		#region Get By ApplicationMode

		public static DataTable GetByApplicationMode(int applicationModeId, int auditId)
		{
            var sql = "EXEC ApplicationModeXFieldConfigurationModeSearch" +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", @ApplicationModeId		=" + applicationModeId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationModeXFieldConfigurationMode");
			var oDT = new DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

        #endregion

        #region Get By ApplicationMode

        public static DataTable GetByFieldConfigurationMode(int fieldConfigurationModeId, int auditId)
        {
            var sql = "EXEC ApplicationModeXFieldConfigurationModeSearch" +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", @FieldConfigurationModeId		=" + fieldConfigurationModeId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationModeXFieldConfigurationMode");
            var oDT = new DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

		#region Delete By FieldConfigurationMode

		public static void DeleteByFieldConfigurationMode(int FieldConfigurationModeId, int auditId)
		{
			var sql = "EXEC ApplicationModeXFieldConfigurationModeDelete "+
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", @FieldConfigurationModeId	=" + FieldConfigurationModeId;
			DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

		#region Delete By ApplicationMode

		public static void DeleteByApplicationMode(int ApplicationModeId, int auditId)
		{
            var sql = "EXEC ApplicationModeXFieldConfigurationModeDelete " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", @ApplicationModeId		=" + ApplicationModeId;

			DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion
    }
}
