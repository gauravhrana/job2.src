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
	
	public partial class ConnectionStringXApplicationDataManager : BaseDataManager
	{
		static string DataStoreKey = "";
		
		static ConnectionStringXApplicationDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ConnectionStringXApplication");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(ConnectionStringXApplicationDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case ConnectionStringXApplicationDataModel.DataColumns.ConnectionStringXApplicationId:
					if (data.ConnectionStringXApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ConnectionStringXApplicationDataModel.DataColumns.ConnectionStringXApplicationId, data.ConnectionStringXApplicationId);


					}

					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringXApplicationDataModel.DataColumns.ConnectionStringXApplicationId);

					}
					break;

				case ConnectionStringXApplicationDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ConnectionStringXApplicationDataModel.DataColumns.ApplicationId, data.ApplicationId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringXApplicationDataModel.DataColumns.ApplicationId);

					}
					break;

				case ConnectionStringXApplicationDataModel.DataColumns.ConnectionStringId:
					if (data.ConnectionStringId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ConnectionStringXApplicationDataModel.DataColumns.ConnectionStringId, data.ConnectionStringId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringXApplicationDataModel.DataColumns.ConnectionStringId);

					}
					break;
				case ConnectionStringXApplicationDataModel.DataColumns.ConnectionString:
					if (!string.IsNullOrEmpty(data.ConnectionString))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringXApplicationDataModel.DataColumns.ConnectionString, data.ConnectionString);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringXApplicationDataModel.DataColumns.ConnectionString);

					}
					break;
				case ConnectionStringXApplicationDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ConnectionStringXApplicationDataModel.DataColumns.Application, data.Application);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ConnectionStringXApplicationDataModel.DataColumns.Application);

					}
					break;

				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			return returnValue;
		}

		#endregion

		#region Create By Application

		public static void Create(int applicationId, int[] connectionStringIds, RequestProfile requestProfile)
		{
			foreach (int connectionStringId in connectionStringIds)
			{
				var sql = "EXEC dbo.ConnectionStringXApplicationInsert " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", @ApplicationId				= " + applicationId +
						", @ConnectionStringId			= " + connectionStringId;

				DBDML.RunSQL("ConnectionStringXApplication_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By Application

		public static void CreateByApplication(int applicationId, int[] connectionStringIds, RequestProfile requestProfile)
		{
			Create(applicationId, connectionStringIds, requestProfile);
		}

		#endregion CreateByApplication

		#region Create By ConnectionStrings

		public static void CreateByConnectionString(int connectionStringId, int[] applicationIds, RequestProfile requestProfile)
		{
			foreach (int applicationId in applicationIds)
			{
				var sql = "EXEC dbo.ConnectionStringXApplicationInsert " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", @ApplicationId							= " + applicationId +
						", @ConnectionStringId						= " + connectionStringId;
				DBDML.RunSQL("ConnectionStringXApplication_Insert", sql, DataStoreKey);
			}
		}
		
		#endregion

		#region Get By ConnectionString

		public static DataTable GetByConnectionString(int connectionStringId, RequestProfile requestProfile)
		{
			var sql = "EXEC ConnectionStringXApplicationSearch " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", @ConnectionStringId=" + connectionStringId;

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

        #region GetEntityDetails

        public static List<ConnectionStringXApplicationDataModel> GetEntityDetails(ConnectionStringXApplicationDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
            const string sql = @"dbo.ConnectionStringXApplicationSearch ";

			var parameters =
			new
			{
				    AuditId                        = requestProfile.AuditId
				,   ApplicationId                  = requestProfile.ApplicationId                
                ,   ConnectionStringId             = dataQuery.ConnectionStringId
                ,   ConnectionStringXApplicationId = dataQuery.ConnectionStringXApplicationId
			};

            List<ConnectionStringXApplicationDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
                result = dataAccess.Connection.Query<ConnectionStringXApplicationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}


        #endregion

        #region Search

        public static DataTable Search(ConnectionStringXApplicationDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 0);
            var dt = list.ToDataTable();
            return dt;
		}

		#endregion

		#region Get By Application

		public static DataTable GetByApplication(int applicationId, RequestProfile requestProfile)
		{
			var sql = "EXEC ConnectionStringXApplicationSearch " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", @ApplicationId		=" + applicationId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete By ConnectionString

		public static void DeleteByConnectionString(int connectionStringId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ConnectionStringXApplicationDelete ";

			var parameters =	new
								{
										AuditId						= requestProfile.AuditId
									,	ConnectionStringId			= connectionStringId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Delete By Application

		public static void DeleteByApplication(int applicationId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ConnectionStringXApplicationDelete ";

			var parameters =	new
								{
										AuditId						= requestProfile.AuditId
									,	ApplicationId				= applicationId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

	}
}
