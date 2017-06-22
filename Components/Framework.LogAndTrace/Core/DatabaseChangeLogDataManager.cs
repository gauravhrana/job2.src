using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Dapper;

namespace Framework.Components.LogAndTrace
{
	public class DatabaseChangeLogDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static DatabaseChangeLogDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DatabaseChangeLog");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(DatabaseChangeLogDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{

				case DatabaseChangeLogDataModel.DataColumns.Id:
					if (data.Id != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DatabaseChangeLogDataModel.DataColumns.Id, data.Id);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.Id);
					}
					break;

				case DatabaseChangeLogDataModel.DataColumns.CurrentUser:
					if (!string.IsNullOrEmpty(data.CurrentUser))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DatabaseChangeLogDataModel.DataColumns.CurrentUser, data.CurrentUser.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.CurrentUser);
					}
					break;

				case DatabaseChangeLogDataModel.DataColumns.FromSearchDate:
					if (data.FromSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DatabaseChangeLogDataModel.DataColumns.FromSearchDate, data.FromSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.FromSearchDate);
					}
					break;

				case DatabaseChangeLogDataModel.DataColumns.ToSearchDate:
					if (data.ToSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DatabaseChangeLogDataModel.DataColumns.ToSearchDate, data.ToSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.ToSearchDate);
					}
					break;

				case DatabaseChangeLogDataModel.DataColumns.DataBaseName:
					if (!string.IsNullOrEmpty(data.DataBaseName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DatabaseChangeLogDataModel.DataColumns.DataBaseName, data.DataBaseName.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.DataBaseName);
					}
					break;

				case DatabaseChangeLogDataModel.DataColumns.ObjectName:
					if (!string.IsNullOrEmpty(data.ObjectName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DatabaseChangeLogDataModel.DataColumns.ObjectName, data.ObjectName.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.ObjectName);
					}
					break;

				case DatabaseChangeLogDataModel.DataColumns.ObjectType:
					if (!string.IsNullOrEmpty(data.ObjectType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DatabaseChangeLogDataModel.DataColumns.ObjectType, data.ObjectType.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.ObjectType);
					}
					break;

				case DatabaseChangeLogDataModel.DataColumns.HostName:
					if (!string.IsNullOrEmpty(data.HostName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DatabaseChangeLogDataModel.DataColumns.HostName, data.HostName.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.HostName);
					}
					break;

				case DatabaseChangeLogDataModel.DataColumns.RecordDate:
					if (data.RecordDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DatabaseChangeLogDataModel.DataColumns.RecordDate, data.RecordDate.ToString());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DatabaseChangeLogDataModel.DataColumns.RecordDate);
					}
					break;
			}

			return returnValue;
		}

		#endregion

		#region GetDetails

        public static DatabaseChangeLogDataModel GetDetails(DatabaseChangeLogDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<DatabaseChangeLogDataModel> GetEntityDetails(DatabaseChangeLogDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.DatabaseChangeLogSearch ";

			var parameters =
			new
			{
				    AuditId         = requestProfile.AuditId
				,   Id              = dataQuery.Id
                ,   DataBaseName    = dataQuery.DataBaseName
                ,   ObjectName      = dataQuery.ObjectName
                ,   ObjectType      = dataQuery.ObjectType
                ,   CurrentUser     = dataQuery.CurrentUser
                ,   HostName        = dataQuery.HostName
                ,   FromSearchDate  = dataQuery.FromSearchDate                
                ,   ToSearchDate    = dataQuery.ToSearchDate
				,   ReturnAuditInfo = returnAuditInfo
			};

			List<DatabaseChangeLogDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<DatabaseChangeLogDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Search

		public static DataTable Search(DatabaseChangeLogDataModel data, RequestProfile requestProfile)
		{			
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;

		}

		#endregion

	}
}
