using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class FieldConfigurationModeCategoryXFCMode : BaseClass
    {
        static string DataStoreKey = "";
		static int ApplicationId;

        static FieldConfigurationModeCategoryXFCMode()
		{
			ApplicationId = SetupConfiguration.ApplicationId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeCategoryXFCMode");
		}

		#region Create By FieldConfigurationModeCategory

        public static void CreateByFieldConfigurationModeCategory(int FieldConfigurationModeCategoryId, int[] FieldConfigurationModeIds, int auditId)
		{
			foreach (int FieldConfigurationModeId in FieldConfigurationModeIds)
			{
				var sql = "EXEC dbo.FieldConfigurationModeCategoryXFCModeInsert " +
						" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
						", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
						", @FieldConfigurationModeCategoryId	= " + FieldConfigurationModeCategoryId +
						", @FieldConfigurationModeId			= " + FieldConfigurationModeId;

				DataAccess.DBDML.RunSQL("FieldConfigurationModeCategoryXFCMode_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By FieldConfigurationModes

		public static void CreateByFieldConfigurationMode(int FieldConfigurationModeId, int[] FieldConfigurationModeCategoryIds, int auditId)
		{
			foreach (int FieldConfigurationModeCategoryId in FieldConfigurationModeCategoryIds)
			{
				var sql = "EXEC dbo.FieldConfigurationModeCategoryXFCModeInsert " +
						" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
						", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
						", @FieldConfigurationModeCategoryId		= " + FieldConfigurationModeCategoryId +
						", @FieldConfigurationModeId				= " + FieldConfigurationModeId;
				DataAccess.DBDML.RunSQL("FieldConfigurationModeCategoryXFCMode_Insert", sql, DataStoreKey);
			}
		}
		#endregion

		#region Get By FieldConfigurationMode

		public static DataTable GetByFieldConfigurationMode(int FieldConfigurationModeId, int auditId)
		{
			var sql = "EXEC FieldConfigurationModeCategoryXFCModeDetails " +
						" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
						", @FieldConfigurationModeId	=" + FieldConfigurationModeId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeCategoryXFCMode");

			var oDT = new DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get By FieldConfigurationModeCategory

		public static DataTable GetByFieldConfigurationModeCategory(int fieldConfigurationModeCategoryId, int auditId)
		{
			var sql = "EXEC FieldConfigurationModeCategoryXFCModeDetails " +
						" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
						", @FieldConfigurationModeCategoryId		=" + fieldConfigurationModeCategoryId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationModeXApplicationEntityFieldLabelMode");
			var oDT = new DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete By FieldConfigurationMode

		public static void DeleteByFieldConfigurationMode(int FieldConfigurationModeId, int auditId)
		{
			var sql = "EXEC FieldConfigurationModeCategoryXFCModeDelete " +
						" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
						", @FieldConfigurationModeId	=" + FieldConfigurationModeId;
			DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

		#region Delete By FieldConfigurationModeCategory

		public static void DeleteByFieldConfigurationModeCategory(int FieldConfigurationModeCategoryId, int auditId)
		{
			var sql = "EXEC FieldConfigurationModeCategoryXFCModeDelete " +
						" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
						", @FieldConfigurationModeCategoryId		=" + FieldConfigurationModeCategoryId;

			DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion
    }
}
