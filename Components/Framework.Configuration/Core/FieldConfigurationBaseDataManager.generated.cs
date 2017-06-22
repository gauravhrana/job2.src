using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;

namespace Framework.Components.UserPreference
{
	public partial class FieldConfigurationBaseDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FieldConfigurationBaseDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationBase");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FieldConfigurationBaseDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FieldConfigurationBaseDataModel.DataColumns.FieldConfigurationBaseId:
					if (data.FieldConfigurationBaseId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationBaseDataModel.DataColumns.FieldConfigurationBaseId, data.FieldConfigurationBaseId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationBaseDataModel.DataColumns.FieldConfigurationBaseId);
					}
					break;

				case FieldConfigurationBaseDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationBaseDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationBaseDataModel.DataColumns.Name);
					}
					break;

				case FieldConfigurationBaseDataModel.DataColumns.Value:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationBaseDataModel.DataColumns.Value, data.Value);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationBaseDataModel.DataColumns.Value);
					}
					break;

				case FieldConfigurationBaseDataModel.DataColumns.ControlType:
					if (!string.IsNullOrEmpty(data.ControlType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationBaseDataModel.DataColumns.ControlType, data.ControlType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationBaseDataModel.DataColumns.ControlType);
					}
					break;

				case FieldConfigurationBaseDataModel.DataColumns.Formatting:
					if (!string.IsNullOrEmpty(data.Formatting))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationBaseDataModel.DataColumns.Formatting, data.Formatting);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationBaseDataModel.DataColumns.Formatting);
					}
					break;

				case FieldConfigurationBaseDataModel.DataColumns.Version:
					if (!string.IsNullOrEmpty(data.Version))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationBaseDataModel.DataColumns.Version, data.Version);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationBaseDataModel.DataColumns.Version);
					}
					break;

				case FieldConfigurationBaseDataModel.DataColumns.Width:
					if (data.Width != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationBaseDataModel.DataColumns.Width, data.Width);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationBaseDataModel.DataColumns.Width);
					}
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<FieldConfigurationBaseDataModel> GetEntityDetails(FieldConfigurationBaseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FieldConfigurationBaseSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FieldConfigurationBaseId           = dataQuery.FieldConfigurationBaseId
				 ,	Name           = dataQuery.Name
			};

			List<FieldConfigurationBaseDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FieldConfigurationBaseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FieldConfigurationBaseDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FieldConfigurationBaseDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FieldConfigurationBaseDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static FieldConfigurationBaseDataModel GetDetails(FieldConfigurationBaseDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save


		public static string Save(FieldConfigurationBaseDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FieldConfigurationBaseInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FieldConfigurationBaseUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationBaseDataModel.DataColumns.FieldConfigurationBaseId); 
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationBaseDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationBaseDataModel.DataColumns.Value); 
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationBaseDataModel.DataColumns.ControlType); 
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationBaseDataModel.DataColumns.Formatting); 
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationBaseDataModel.DataColumns.Version); 
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationBaseDataModel.DataColumns.Width); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FieldConfigurationBaseDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FieldConfigurationBase.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FieldConfigurationBaseDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FieldConfigurationBase.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FieldConfigurationBaseDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FieldConfigurationBaseDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FieldConfigurationBaseId  = data.FieldConfigurationBaseId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FieldConfigurationBaseDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FieldConfigurationBaseDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
