using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.Core
{
	public partial class SystemEntityXSystemEntityCategoryDataManager : BaseDataManager
	{
		static string DataStoreKey = "";
		
		static SystemEntityXSystemEntityCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SystemEntityXSystemEntityCategory");
		}

        #region ToSqlParameter

        public static string ToSQLParameter(SystemEntityXSystemEntityCategoryDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityXSystemEntityCategoryId:
                    if (data.SystemEntityXSystemEntityCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityXSystemEntityCategoryId, data.SystemEntityXSystemEntityCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityXSystemEntityCategoryId);
                    }
                    break;

                case SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityId:
                    if (data.SystemEntityId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityId, data.SystemEntityId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityId);
                    }
                    break;

                case SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId:
                    if (data.SystemEntityCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId, data.SystemEntityCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId);
                    }
                    break;

                case SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntity:
                    if (!string.IsNullOrEmpty(data.SystemEntity))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntity, data.SystemEntity);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntity);
                    }
                    break;

                case SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityCategory:
                    if (!string.IsNullOrEmpty(data.SystemEntityCategory))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityCategory, data.SystemEntityCategory);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityCategory);
                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;
            }
            return returnValue;
        }
        #endregion

        #region Create By SystemEntity

        public static void CreateBySystemEntity(int SystemEntityId, int[] SystemEntityCategoryIds, RequestProfile requestProfile)
		{
			foreach (int SystemEntityCategoryId in SystemEntityCategoryIds)
			{
				var sql = "EXEC SystemEntityXSystemEntityCategoryInsert " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @SystemEntityId					=   " + SystemEntityId +
							",      @SystemEntityCategoryId				=   " + SystemEntityCategoryId;

				DBDML.RunSQL("SystemEntityXSystemEntityCategoryInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By SystemEntityCategory

		public static void CreateBySystemEntityCategory(int SystemEntityCategoryId, int[] SystemEntityIds, RequestProfile requestProfile)
		{
			foreach (int SystemEntityId in SystemEntityIds)
			{
				var sql = "EXEC SystemEntityXSystemEntityCategoryInsert " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @SystemEntityId					=   " + SystemEntityId +
							",      @SystemEntityCategoryId				=   " + SystemEntityCategoryId;
				DBDML.RunSQL("SystemEntityXSystemEntityCategoryInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Get By SystemEntityCategory

		public static DataTable GetBySystemEntityCategory(int SystemEntityCategoryId, RequestProfile requestProfile)
		{
			var sql = "EXEC SystemEntityXSystemEntityCategorySearch @SystemEntityCategoryId     =" + SystemEntityCategoryId + ", " +
						  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get By SystemEntity

		public static DataTable GetBySystemEntity(int SystemEntityId, RequestProfile requestProfile)
		{
			var sql = "EXEC SystemEntityXSystemEntityCategorySearch @SystemEntityId       =" + SystemEntityId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete

		public static void Delete(SystemEntityXSystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemEntityXSystemEntityCategoryDelete " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityXSystemEntityCategoryId);

			DBDML.RunSQL("SystemEntityXSystemEntityCategory.Delete", sql, DataStoreKey);
		}

		#endregion

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemEntityXSystemEntityCategorySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("SystemEntityXSystemEntityCategory.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(SystemEntityXSystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemEntityXSystemEntityCategorySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, ReturnAuditInfoOnDetails) +
				", " + ToSQLParameter(data, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityXSystemEntityCategoryId);

			var oDT = new DBDataTable("SystemEntityXSystemEntityCategory.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Search

		public static DataTable Search(SystemEntityXSystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.SystemEntityXSystemEntityCategorySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityXSystemEntityCategoryId) +
				", " + ToSQLParameter(data, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityId) +
				", " + ToSQLParameter(data, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId);

			var oDT = new DBDataTable("SystemEntityXSystemEntityCategory.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Create

		public static void Create(SystemEntityXSystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("SystemEntityXSystemEntityCategory.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(SystemEntityXSystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("SystemEntityXSystemEntityCategory.Update", sql, DataStoreKey);
		}

		#endregion


		#region CreateOrUpdate

		private static string CreateOrUpdate(SystemEntityXSystemEntityCategoryDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SystemEntityXSystemEntityCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SystemEntityXSystemEntityCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityXSystemEntityCategoryId) +
						", " + ToSQLParameter(data, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId) +
						", " + ToSQLParameter(data, SystemEntityXSystemEntityCategoryDataModel.DataColumns.SystemEntityId);
			return sql;
		}

		#endregion




		#region Delete By SystemEntityCategory

		public static void DeleteBySystemEntityCategory(int systemEntityCategoryId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SystemEntityXSystemEntityCategoryDelete ";

			var parameters =	new
								{
										AuditId								= requestProfile.AuditId
									,	SystemEntityCategoryId				= systemEntityCategoryId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Delete By SystemEntity

		public static void DeleteBySystemEntity(int systemEntityId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SystemEntityXSystemEntityCategoryDelete ";

			var parameters =	new
								{
										AuditId						= requestProfile.AuditId
									,	SystemEntityId				= systemEntityId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

	}
}
