using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class FieldConfigurationModeCategory : StandardClass
    {
        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

		public static DataTable GetApplicableModesListByEntity(DataTable columns, DataTable modes)
		{
            var fieldConfigurationModeIdColumn = Framework.Components.UserPreference.DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId;

            var validmodes =    (from a in modes.AsEnumerable()
                                join b in columns.AsEnumerable()
                                on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
                                into g
                                where g.Count() > 0
                                select a).CopyToDataTable();

			return validmodes;

		}

        static FieldConfigurationModeCategory()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeCategory");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeCategoryList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeCategory.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

		public static DataTable GetDetails(DomainModel.FieldConfigurationModeCategory data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeCategoryDetails " +
                " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeCategory.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static List<DomainModel.FieldConfigurationModeCategory> GetEntityDetails(DomainModel.FieldConfigurationModeCategory data, int auditId)
		{
			var sql = "EXEC dbo.FieldConfigurationModeCategoryDetails " +
					  " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
					  ", " + ToSQLParameter(data,DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId);

			var result = new List<DomainModel.FieldConfigurationModeCategory>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new DomainModel.FieldConfigurationModeCategory();

					dataItem.FieldConfigurationModeCategoryId = (int)dbReader[DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId];

					SetStandardInfo(dataItem, dbReader);

					//SetBaseInfo(dataItem, dbReader);
					result.Add(dataItem);
				}
			}

			return result;
		}

        #endregion

        #region Create

		public static void Create(DomainModel.FieldConfigurationModeCategory data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            DataAccess.DBDML.RunSQL("FieldConfigurationModeCategory.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(DomainModel.FieldConfigurationModeCategory data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("FieldConfigurationModeCategory.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(DomainModel.FieldConfigurationModeCategory data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeCategoryDelete " +
                " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId);

            DataAccess.DBDML.RunSQL("FieldConfigurationModeCategory.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

		public static string ToSQLParameter(DomainModel.FieldConfigurationModeCategory data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId:
					if (data.FieldConfigurationModeCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId, data.FieldConfigurationModeCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId);
					}
					break;

				default:
					returnValue = StandardClass.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(DomainModel.FieldConfigurationModeCategory data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeCategorySearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId) +
				", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeCategory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

		private static string Save(DomainModel.FieldConfigurationModeCategory data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationModeCategoryInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationModeCategoryUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId)+
                      ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId) +
					    ", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(DomainModel.FieldConfigurationModeCategory data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeCategoryDoesExist " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeCategory.DataColumns.FieldConfigurationModeCategoryId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeCategory.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        
    }
}
