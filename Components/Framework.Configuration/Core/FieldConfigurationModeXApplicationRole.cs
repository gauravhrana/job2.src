using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.UserPreference
{
	
	public class FieldConfigurationModeXApplicationRoleDataManager : BaseDataManager
	{
        static readonly string DataStoreKey = "";

		static FieldConfigurationModeXApplicationRoleDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeXApplicationRole");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FieldConfigurationModeXApplicationRoleDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeXApplicationRoleId:
					if (data.FieldConfigurationModeXApplicationRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeXApplicationRoleId, data.FieldConfigurationModeXApplicationRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeXApplicationRoleId);
					}
					break;

				case FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId:
					if (data.FieldConfigurationModeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId);
					}
					break;

				case FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId:
					if (data.ApplicationRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId, data.ApplicationRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId);
					}
					break;

				case FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessModeId:
					if (data.FieldConfigurationModeAccessModeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessModeId, data.FieldConfigurationModeAccessModeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessModeId);
					}
					break;

				case FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationMode:
					if (!string.IsNullOrEmpty(data.FieldConfigurationMode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationMode, data.FieldConfigurationMode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationMode);
					}
					break;

				case FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRole:
					if (!string.IsNullOrEmpty(data.ApplicationRole))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRole, data.ApplicationRole);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRole);
					}
					break;

				case FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessMode:
					if (!string.IsNullOrEmpty(data.FieldConfigurationModeAccessMode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessMode, data.FieldConfigurationModeAccessMode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessMode);
					}
					break;

				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<FieldConfigurationModeXApplicationRoleDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityList(FieldConfigurationModeXApplicationRoleDataModel.Empty, requestProfile);
		}

		#endregion

		#region GetDetails

        public static FieldConfigurationModeXApplicationRoleDataModel GetDetails(FieldConfigurationModeXApplicationRoleDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityList(data, requestProfile);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FieldConfigurationModeXApplicationRoleDataModel> GetEntityList(FieldConfigurationModeXApplicationRoleDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FieldConfigurationModeXApplicationRoleSearch ";

			var parameters =
			new
			{
					AuditId										= requestProfile.AuditId
				,	ApplicationId								= requestProfile.ApplicationId
				,	FieldConfigurationModeXApplicationRoleId	= dataQuery.FieldConfigurationModeXApplicationRoleId
				,	Name										= dataQuery.FieldConfigurationMode 
				,	FieldConfigurationModeId					= dataQuery.FieldConfigurationModeId
				,	ApplicationRoleId							= dataQuery.ApplicationRoleId
				,	FieldConfigurationModeAccessModeId			= dataQuery.FieldConfigurationModeAccessModeId
			};

			List<FieldConfigurationModeXApplicationRoleDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FieldConfigurationModeXApplicationRoleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static int Create(FieldConfigurationModeXApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			var FieldConfigurationModeXApplicationRoleId = DBDML.RunScalarSQL("FieldConfigurationModeXApplicationRole.Insert", sql, DataStoreKey);
			return Convert.ToInt32(FieldConfigurationModeXApplicationRoleId);
		}

		#endregion

		#region Update

		public static void Update(FieldConfigurationModeXApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("FieldConfigurationModeXApplicationRole.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FieldConfigurationModeXApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FieldConfigurationModeXApplicationRoleDelete ";

			var parameters =	new
								{
										AuditId										= requestProfile.AuditId
									,	FieldConfigurationModeXApplicationRoleId	= data.FieldConfigurationModeXApplicationRoleId
									,	FieldConfigurationModeId					= data.FieldConfigurationModeId
									,	ApplicationRoleId							= data.ApplicationRoleId
									,	FieldConfigurationModeAccessModeId			= data.FieldConfigurationModeAccessModeId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Search

		public static DataTable Search(FieldConfigurationModeXApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityList(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		public static DataTable SearchByFCModeAccessMode(FieldConfigurationModeXApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.FieldConfigurationModeXApplicationRoleSearchByFCModeAccessModeByFCModeAccessMode " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId) +
				", " + ToSQLParameter(data, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessMode);

			var oDT = new DBDataTable("FieldConfigurationModeXApplicationRole.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

		private static string Save(FieldConfigurationModeXApplicationRoleDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FieldConfigurationModeXApplicationRoleInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FieldConfigurationModeXApplicationRoleUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeXApplicationRoleId) +
						", " + ToSQLParameter(data, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId) +
						", " + ToSQLParameter(data, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId) +
						", " + ToSQLParameter(data, FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeAccessModeId);

			return sql;
		}

		#endregion

	}

}
