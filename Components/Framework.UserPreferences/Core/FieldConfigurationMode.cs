using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
	public partial class FieldConfigurationMode : StandardClass
    {
        static string DataStoreKey = "";
        static readonly int ApplicationId;

        static FieldConfigurationMode()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationMode");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationMode");
            var sql = "EXEC dbo.FieldConfigurationModeList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationMode.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static DataTable GetList(int auditId, int applicationId)
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationMode");
			var sql = "EXEC dbo.FieldConfigurationModeList" +
				" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, applicationId);

			var oDT = new DataAccess.DBDataTable("FieldConfigurationMode.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

        #endregion

        #region GetDetails

        public static DataTable GetDetails(DomainModel.FieldConfigurationMode data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeDetails " +
                " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationMode.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static List<DomainModel.FieldConfigurationMode> GetEntityDetails(DomainModel.FieldConfigurationMode data, int auditId)
		{
			var sql = "EXEC dbo.FieldConfigurationModeDetails " +
					  " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
					  ", " + ToSQLParameter(data,DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId);

			var result = new List<DomainModel.FieldConfigurationMode>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new DomainModel.FieldConfigurationMode();

					dataItem.FieldConfigurationModeId = (int)dbReader[DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId];

					SetStandardInfo(dataItem, dbReader);

					/*BaseData.SetBaseInfo(dataItem, dbReader);
					*/
					result.Add(dataItem);
				}
			}

			return result;
		}

        #endregion

        #region Create

		public static int Create(DomainModel.FieldConfigurationMode data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            var AEFLModeId = DataAccess.DBDML.RunScalarSQL("FieldConfigurationMode.Insert", sql, DataStoreKey);
            return Convert.ToInt32(AEFLModeId);
        }

        #endregion

        #region Update

		public static void Update(DomainModel.FieldConfigurationMode data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("FieldConfigurationMode.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(DomainModel.FieldConfigurationMode data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId);

            DataAccess.DBDML.RunSQL("FieldConfigurationMode.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

		public static string ToSQLParameter(DomainModel.FieldConfigurationMode data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId:
					if (data.FieldConfigurationModeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId);
					}
					break;

				default:
					returnValue = StandardClass.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(DomainModel.FieldConfigurationMode data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId) +
				", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationMode.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static int? GetFCModeIdByName(string name, int auditId)
        {
            int? fcModeId = null; 

            var obj = new DomainModel.FieldConfigurationMode();
            obj.Name = name;

            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(obj, StandardModel.StandardDataColumns.Name);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationMode.Search", sql, DataStoreKey);

            if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
            {
				fcModeId = Convert.ToInt32(oDT.DBTable.Rows[0][DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId]);
            }
            return fcModeId;
        }

        #endregion

        #region Save

		private static string Save(DomainModel.FieldConfigurationMode data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationModeInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationModeUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data,DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.SortOrder);	
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(DomainModel.FieldConfigurationMode data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeDoesExist " +
            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
            ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfigurationMode.DataColumns.Name);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationMode.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion
    }
}
