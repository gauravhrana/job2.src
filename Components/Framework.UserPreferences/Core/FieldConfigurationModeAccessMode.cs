using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public class FieldConfigurationModeAccessMode: StandardClass
    {
        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

		public static DataTable GetApplicableModesListByEntity(DataTable columns, DataTable modes)
		{
			var validmodes = new DataTable();
			validmodes = modes.Clone();

			for (var j = 0; j < modes.Rows.Count; j++)
			{
				for (var i = 0; i < columns.Rows.Count; i++)
				{
					var x = columns.Rows[i][DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId];
					var y = modes.Rows[j][DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId];

					if (x == y)
					{
						var temp = validmodes.Select(DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId + " = " + y);

						if (temp.Length == 0)
							validmodes.ImportRow(modes.Rows[j]);
					}
				}

			}

			return validmodes;

		}

        static FieldConfigurationModeAccessMode()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeAccessMode");
        }

        #region ToSQLParameter

        public static string ToSQLParameter(DomainModel.FieldConfigurationModeAccessMode data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId:
                    if (data.FieldConfigurationModeAccessModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId, data.FieldConfigurationModeAccessModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId);
                    }
                    break;

                default:
                    returnValue = StandardClass.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        #endregion

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeAccessModeList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeAccessMode.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

		public static DataTable GetDetails(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeAccessModeDetails " +
                " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeAccessMode.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static List<DomainModel.FieldConfigurationModeAccessMode> GetEntityDetails(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
		{
			var sql = "EXEC dbo.FieldConfigurationModeAccessModeDetails " +
					  " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
					  ", " + ToSQLParameter(data,DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId);

			var result = new List<DomainModel.FieldConfigurationModeAccessMode>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new DomainModel.FieldConfigurationModeAccessMode();

					dataItem.FieldConfigurationModeAccessModeId = (int)dbReader[DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId];

					SetStandardInfo(dataItem, dbReader);

					//SetBaseInfo(dataItem, dbReader);
					result.Add(dataItem);
				}
			}

			return result;
		}

        #endregion

        #region Create

		public static void Create(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            DataAccess.DBDML.RunSQL("FieldConfigurationModeAccessMode.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("FieldConfigurationModeAccessMode.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeAccessModeDelete " +
                " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId);

            DataAccess.DBDML.RunSQL("FieldConfigurationModeAccessMode.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

		public static DataTable Search(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeAccessModeSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId) +
				", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeAccessMode.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

		private static string Save(DomainModel.FieldConfigurationModeAccessMode data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationModeAccessModeInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationModeAccessModeUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId)+
                      ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId) +
					    ", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeAccessModeDoesExist " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeAccessMode.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationRoleChildrenGet " +
                            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                            ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeAccessMode.DataColumns.FieldConfigurationModeAccessModeId);

            var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(DomainModel.FieldConfigurationModeAccessMode data, int auditId)
        {
            var isDeletable = true;
            var ds = GetChildren(data, auditId);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        isDeletable = false;
                        break;
                    }
                }
            }
            return isDeletable;
        }

        #endregion

    }

}
