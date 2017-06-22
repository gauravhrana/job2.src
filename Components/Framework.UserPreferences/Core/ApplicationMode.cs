using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class ApplicationMode : StandardClass
    {
        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static ApplicationMode()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationMode");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.ApplicationModeList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("ApplicationMode.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(DomainModel.ApplicationMode data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationModeDetails " +
                " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data,DomainModel.ApplicationMode.DataColumns.ApplicationModeId);

            var oDT = new DataAccess.DBDataTable("ApplicationMode.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static List<DomainModel.ApplicationMode> GetEntityDetails(DomainModel.ApplicationMode data, int auditId)
		{
			var sql = "EXEC dbo.ApplicationModeDetails " +
					  " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
					  ", " + ToSQLParameter(data, DomainModel.ApplicationMode.DataColumns.ApplicationModeId);

			var result = new List<DomainModel.ApplicationMode>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new DomainModel.ApplicationMode();

					dataItem.ApplicationModeId = (int)dbReader[DomainModel.ApplicationMode.DataColumns.ApplicationModeId];
					
					SetStandardInfo(dataItem, dbReader);

					SetBaseInfo(dataItem, dbReader);
					
					result.Add(dataItem);
				}
			}

			return result;
		}

        #endregion

        #region Create

		public static void Create(DomainModel.ApplicationMode data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            DataAccess.DBDML.RunSQL("ApplicationMode.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(DomainModel.ApplicationMode data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("ApplicationMode.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(DomainModel.ApplicationMode data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationModeDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data, DomainModel.ApplicationMode.DataColumns.ApplicationModeId);

            DataAccess.DBDML.RunSQL("ApplicationMode.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

		public static string ToSQLParameter(DomainModel.ApplicationMode data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DomainModel.ApplicationMode.DataColumns.ApplicationModeId:
					if (data.ApplicationModeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.ApplicationMode.DataColumns.ApplicationModeId, data.ApplicationModeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.ApplicationMode.DataColumns.ApplicationModeId);
					}
					break;

				default:
					returnValue = StandardClass.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static DataTable Search(DomainModel.ApplicationMode data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.ApplicationModeSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data,DomainModel.ApplicationMode.DataColumns.ApplicationModeId) +
				", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name);

            var oDT = new DataAccess.DBDataTable("ApplicationMode.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

		private static string Save(DomainModel.ApplicationMode data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.ApplicationModeInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationModeUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, DomainModel.ApplicationMode.DataColumns.ApplicationModeId) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.SortOrder);	
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(DomainModel.ApplicationMode data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationModeDoesExist " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data,DomainModel.ApplicationMode.DataColumns.ApplicationModeId);

            var oDT = new DataAccess.DBDataTable("ApplicationMode.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        
    }
}
