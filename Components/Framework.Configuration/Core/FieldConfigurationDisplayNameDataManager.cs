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

	public partial class FieldConfigurationDisplayNameDataManager : BaseDataManager
	{

        static readonly string DataStoreKey = "";

		static FieldConfigurationDisplayNameDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationDisplayName");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FieldConfigurationDisplayNameDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationDisplayNameId:
					if (data.FieldConfigurationDisplayNameId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationDisplayNameId, data.FieldConfigurationDisplayNameId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationDisplayNameId);

					}
					break;

				case FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationId:
					if (data.FieldConfigurationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationId, data.FieldConfigurationId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationId);

					}
					break;

				case FieldConfigurationDisplayNameDataModel.DataColumns.IsDefault:
					if (data.IsDefault != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDisplayNameDataModel.DataColumns.IsDefault, data.IsDefault);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDisplayNameDataModel.DataColumns.IsDefault);

					}
					break;

				case FieldConfigurationDisplayNameDataModel.DataColumns.Value:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDisplayNameDataModel.DataColumns.Value, data.Value.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDisplayNameDataModel.DataColumns.Value);

					}
					break;

				case FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfiguration:
					if (!string.IsNullOrEmpty(data.FieldConfiguration))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfiguration, data.FieldConfiguration.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfiguration);

					}
					break;

				case FieldConfigurationDisplayNameDataModel.DataColumns.Language:
					if (!string.IsNullOrEmpty(data.Language))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDisplayNameDataModel.DataColumns.Language, data.Language.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDisplayNameDataModel.DataColumns.Language);

					}
					break;

				case FieldConfigurationDisplayNameDataModel.DataColumns.LanguageId:
					if (data.LanguageId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDisplayNameDataModel.DataColumns.LanguageId, data.LanguageId);

					}
					else
					{
						returnValue = returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDisplayNameDataModel.DataColumns.LanguageId);

					}
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<FieldConfigurationDisplayNameDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FieldConfigurationDisplayNameDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static FieldConfigurationDisplayNameDataModel GetDetails(FieldConfigurationDisplayNameDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region Create

		public static void Create(FieldConfigurationDisplayNameDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("FieldConfigurationDisplayName.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(FieldConfigurationDisplayNameDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("FieldConfigurationDisplayName.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FieldConfigurationDisplayNameDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FieldConfigurationDisplayNameDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				FieldConfigurationDisplayNameId = data.FieldConfigurationDisplayNameId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(FieldConfigurationDisplayNameDataModel data, RequestProfile requestProfile, int applicationModeId = 0)
		{
			// formulate SQL
			var sql = "EXEC dbo.FieldConfigurationDisplayNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, applicationModeId) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationDisplayNameId) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.LanguageId) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.Value) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationId) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.IsDefault);

			var oDT = new DBDataTable("FieldConfigurationDisplayName.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

        public static List<FieldConfigurationDisplayNameDataModel> GetEntityDetails(FieldConfigurationDisplayNameDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.FieldConfigurationDisplayNameSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   LanguageId             = dataQuery.LanguageId
                ,   FieldConfigurationId                    = dataQuery.FieldConfigurationId
                ,   Value               = dataQuery.Value
                ,   FieldConfigurationDisplayNameId               = dataQuery.FieldConfigurationDisplayNameId
                ,   IsDefault               = dataQuery.IsDefault
            };

            List<FieldConfigurationDisplayNameDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<FieldConfigurationDisplayNameDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }


		#endregion

		#region Save

		private static string Save(FieldConfigurationDisplayNameDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FieldConfigurationDisplayNameInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FieldConfigurationDisplayNameUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationDisplayNameId) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.LanguageId) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.Value) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationId) +
				", " + ToSQLParameter(data, FieldConfigurationDisplayNameDataModel.DataColumns.IsDefault);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(FieldConfigurationDisplayNameDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FieldConfigurationDisplayNameDataModel();
			doesExistRequest.FieldConfigurationId = data.FieldConfigurationId;
			doesExistRequest.LanguageId = data.LanguageId;

            var dt = Search(doesExistRequest, requestProfile, 0);
            return dt.Rows.Count > 0;
		}

		#endregion

	}

}
