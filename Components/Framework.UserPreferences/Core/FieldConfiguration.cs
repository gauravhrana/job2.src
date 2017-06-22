using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class FieldConfiguration : BaseClass
    {
        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static FieldConfiguration()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfiguration");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("FieldConfiguration.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(DomainModel.FieldConfiguration data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationDetails " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId);

            var oDT = new DataAccess.DBDataTable("FieldConfiguration.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static List<DomainModel.FieldConfiguration> GetEntityList(DomainModel.FieldConfiguration data, int auditId)
		{
			var oDT = GetDetails(data, auditId);

			var dataList = new List<DomainModel.FieldConfiguration>();

			if (oDT.Rows.Count == 1)
			{
				var oData = new DomainModel.FieldConfiguration();

				var row = oDT.Rows[0];

				oData.FieldConfigurationId = (int?)row[DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId];
				oData.Name = (string)row[DomainModel.FieldConfiguration.DataColumns.Name];
				oData.Value = (string)row[DomainModel.FieldConfiguration.DataColumns.Value];
				oData.SystemEntityTypeId = (int?)row[DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId];
				oData.Width = (Decimal?)row[DomainModel.FieldConfiguration.DataColumns.Width];
				oData.Formatting = (string)row[DomainModel.FieldConfiguration.DataColumns.Formatting];
				oData.ControlType = (string)row[DomainModel.FieldConfiguration.DataColumns.ControlType];
				oData.HorizontalAlignment = (string)row[DomainModel.FieldConfiguration.DataColumns.HorizontalAlignment];
				oData.GridViewPriority = (int?)row[DomainModel.FieldConfiguration.DataColumns.GridViewPriority];
				oData.DetailsViewPriority = (int?)row[DomainModel.FieldConfiguration.DataColumns.DetailsViewPriority];
				oData.FieldConfigurationModeId = (int?)row[DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId];
				/*oData.UpdatedDate = Convert.ToDateTime(row[BaseColumns.UpdatedDate]);
				oData.UpdatedBy = Convert.ToString(row[BaseColumns.UpdatedBy]);
				oData.LastAction = Convert.ToString(row[BaseColumns.LastAction]);*/

				dataList.Add(oData);
			}

			return dataList;
		}

        #endregion

		#region GetGridViewColumns

		public static DataTable GetGridViewColumns(DomainModel.FieldConfiguration data, int auditId)
		{
			var sql = "EXEC dbo.FieldConfigurationGetGridViewColumns " +
				" "  + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId);

			var oDT = new DataAccess.DBDataTable("FieldConfiguration.GetGridViewColumns", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetGridViewColumns

		public static DataTable GetDetailsViewColumns(DomainModel.FieldConfiguration data, int auditId)
		{
			var sql = "EXEC dbo.FieldConfigurationGetDetailsViewColumns " +
				" "  + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId);

			var oDT = new DataAccess.DBDataTable("FieldConfiguration.GetDetailsViewColumns", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

        #region Create

        public static int Create(DomainModel.FieldConfiguration data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            var fieldConfigurationId = DataAccess.DBDML.RunScalarSQL("FieldConfiguration.Insert", sql, DataStoreKey);
            return Convert.ToInt32(fieldConfigurationId);
        }

        #endregion

        #region Update

        public static void Update(DomainModel.FieldConfiguration data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("FieldConfiguration.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(DomainModel.FieldConfiguration data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId);

            DataAccess.DBDML.RunSQL("FieldConfiguration.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

		public static string ToSQLParameter(DomainModel.FieldConfiguration data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId:
					if (data.FieldConfigurationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId, data.FieldConfigurationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId);
					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId:
					if (data.FieldConfigurationModeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId);
					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfiguration.DataColumns.Name, data.Name);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.Name);

					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.FieldConfigurationDisplayName:
					if (!string.IsNullOrEmpty(data.FieldConfigurationDisplayName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationDisplayName, data.FieldConfigurationDisplayName);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationDisplayName);
					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.Value:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfiguration.DataColumns.Value, data.Value);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.Value);

					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId);
					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.Width:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfiguration.DataColumns.Width, data.Width);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.Width);

					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.Formatting:
					if (!string.IsNullOrEmpty(data.Formatting))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfiguration.DataColumns.Formatting, data.Formatting);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfiguration.DataColumns.Formatting, data.Formatting);

					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.ControlType:
					if (!string.IsNullOrEmpty(data.ControlType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfiguration.DataColumns.ControlType, data.ControlType);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.ControlType);

					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.HorizontalAlignment:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfiguration.DataColumns.HorizontalAlignment, data.HorizontalAlignment);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.HorizontalAlignment);

					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.GridViewPriority:
					if (data.GridViewPriority != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfiguration.DataColumns.GridViewPriority, data.GridViewPriority);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.GridViewPriority);
					}
					break;

				case DomainModel.FieldConfiguration.DataColumns.DetailsViewPriority:
					if (data.DetailsViewPriority != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfiguration.DataColumns.DetailsViewPriority, data.DetailsViewPriority);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfiguration.DataColumns.DetailsViewPriority);
					}
					break;
			}
			return returnValue;
		}

		public static DataTable Search(DomainModel.FieldConfiguration data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId) +
				", " + ToSQLParameter(data,DomainModel.FieldConfiguration.DataColumns.Name);

            var oDT = new DataAccess.DBDataTable("FieldConfiguration.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static DataTable Search(DomainModel.FieldConfiguration data, int applicationId, int auditId)
		{
			// formulate SQL
			var sql = "EXEC dbo.FieldConfigurationSearch " +
				" " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, applicationId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId) +
				", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.Name);

			var oDT = new DataAccess.DBDataTable("FieldConfiguration.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

        #endregion

        #region Save

		private static string Save(DomainModel.FieldConfiguration data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data,DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.Name) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.Value) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.SystemEntityTypeId) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.Width) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.Formatting) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.ControlType) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.HorizontalAlignment) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.GridViewPriority) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.DetailsViewPriority) +
				  ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationModeId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(DomainModel.FieldConfiguration data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationDoesExist " +
            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
            ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data,DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId) +
                ", " + ToSQLParameter(data,DomainModel.FieldConfiguration.DataColumns.Name);

            var oDT = new DataAccess.DBDataTable("FieldConfiguration.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(DomainModel.FieldConfiguration data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationChildrenGet " +
                            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                            ", " + ToSQLParameter(data, DomainModel.FieldConfiguration.DataColumns.FieldConfigurationId);

            var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(DomainModel.FieldConfiguration data, int auditId)
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
