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
	public class RenumberMigrationDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static RenumberMigrationDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("RenumberMigration");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(RenumberMigrationDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{

				case RenumberMigrationDataModel.DataColumns.RenumberMigrationId:
					if (data.RenumberMigrationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RenumberMigrationDataModel.DataColumns.RenumberMigrationId, data.RenumberMigrationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RenumberMigrationDataModel.DataColumns.RenumberMigrationId);
					}
					break;

				case RenumberMigrationDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RenumberMigrationDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RenumberMigrationDataModel.DataColumns.ApplicationId);
					}
					break;

				case RenumberMigrationDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RenumberMigrationDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RenumberMigrationDataModel.DataColumns.SystemEntityTypeId);
					}
					break;

				case RenumberMigrationDataModel.DataColumns.SystemEntityType:
					if (!string.IsNullOrEmpty(data.SystemEntityType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RenumberMigrationDataModel.DataColumns.SystemEntityType, data.SystemEntityType.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RenumberMigrationDataModel.DataColumns.SystemEntityType);
					}
					break;

				case RenumberMigrationDataModel.DataColumns.OriginalKey:
					if (data.OriginalKey != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RenumberMigrationDataModel.DataColumns.OriginalKey, data.OriginalKey);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RenumberMigrationDataModel.DataColumns.OriginalKey);
					}
					break;

				case RenumberMigrationDataModel.DataColumns.MigratedKey:
					if (data.MigratedKey != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RenumberMigrationDataModel.DataColumns.MigratedKey, data.MigratedKey);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RenumberMigrationDataModel.DataColumns.MigratedKey);
					}
					break;

				case RenumberMigrationDataModel.DataColumns.RecordDate:
					if (data.RecordDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RenumberMigrationDataModel.DataColumns.RecordDate, data.RecordDate.ToString());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RenumberMigrationDataModel.DataColumns.RecordDate);
					}
					break;
			}

			return returnValue;
		}

		#endregion

		#region GetDetails

        public static RenumberMigrationDataModel GetDetails(RenumberMigrationDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		public static List<RenumberMigrationDataModel> GetEntityDetails(RenumberMigrationDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.Log4NetSearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
				,   ApplicationMode     = requestProfile.ApplicationModeId
				,   RenumberMigrationId = dataQuery.RenumberMigrationId
			};

			List<RenumberMigrationDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<RenumberMigrationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Search

		public static DataTable Search(RenumberMigrationDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.RenumberMigrationSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, RenumberMigrationDataModel.DataColumns.RenumberMigrationId) +
				", " + ToSQLParameter(data, RenumberMigrationDataModel.DataColumns.ApplicationId) +
				", " + ToSQLParameter(data, RenumberMigrationDataModel.DataColumns.SystemEntityTypeId) +
				", " + ToSQLParameter(data, RenumberMigrationDataModel.DataColumns.OriginalKey) +
				", " + ToSQLParameter(data, RenumberMigrationDataModel.DataColumns.MigratedKey);

			var oDT = new DBDataTable("RenumberMigration.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

	}
}
