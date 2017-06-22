using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
	public partial class DateRangeTitle : StandardClass
	{
		static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

		private static DataTable DateRangeTitleList;

        static DateRangeTitle()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("DateRangeTitle");
        }

        #region GetList

        public static DataTable GetList(int auditId, bool forceRefresh = false)
        {
	        if (DateRangeTitleList == null || forceRefresh)
	        {
				var sql = "EXEC dbo.DateRangeTitleList" +
				" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

				var oDT = new DataAccess.DBDataTable("DateRangeTitle.List", sql, DataStoreKey);

				DateRangeTitleList = oDT.DBTable;
	        }

			// return clone copy in case databound ... but still should not be required
			return DateRangeTitleList.Clone();
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(DomainModel.DateRangeTitle data, int auditId)
        {
            var sql = "EXEC dbo.DateRangeTitleDetails " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data, DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId);

            var oDT = new DataAccess.DBDataTable("DateRangeTitle.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static List<DomainModel.DateRangeTitle> GetEntityDetails(DomainModel.DateRangeTitle data, int auditId)
		{
			var sql = "EXEC dbo.DateRangeTitleDetails " +
					  " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
					  ", " + ToSQLParameter(data, DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId);

			var result = new List<DomainModel.DateRangeTitle>();

			using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new DomainModel.DateRangeTitle();

					dataItem.DateRangeTitleId = (int)dbReader[DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId];
					
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

        public static void Create(DomainModel.DateRangeTitle data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            DataAccess.DBDML.RunSQL("DateRangeTitle.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(DomainModel.DateRangeTitle data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("DateRangeTitle.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(DomainModel.DateRangeTitle data, int auditId)
        {
            var sql = "EXEC dbo.DateRangeTitleDelete " +
                " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data,DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId);

            DataAccess.DBDML.RunSQL("DateRangeTitle.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

		public static string ToSQLParameter(DomainModel.DateRangeTitle data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId:
					if (data.DateRangeTitleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId, data.DateRangeTitleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId);
					}
					break;

				default:
					returnValue = StandardClass.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(DomainModel.DateRangeTitle data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.DateRangeTitleSearch " +
                " " +  ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data,DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId) +
                ", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name);

            var oDT = new DataAccess.DBDataTable("DateRangeTitle.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

		private static string Save(DomainModel.DateRangeTitle data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.DateRangeTitleInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.DateRangeTitleUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardModel.StandardDataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(DomainModel.DateRangeTitle data, int auditId)
        {
            var sql = "EXEC dbo.DateRangeTitleDoesExist " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, StandardModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, DomainModel.DateRangeTitle.DataColumns.DateRangeTitleId);

            var oDT = new DataAccess.DBDataTable("DateRangeTitle.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion
	}
}
