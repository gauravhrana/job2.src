using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Core;

namespace Framework.Components.Core
{
	public partial class ReportXReportCategoryDataManager : BaseDataManager
	{
		static string DataStoreKey = "";
		
		static ReportXReportCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReportXReportCategory");
		}

        #region ToSqlParameter

        public static string ToSQLParameter(ReportXReportCategoryDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case ReportXReportCategoryDataModel.DataColumns.ReportXReportCategoryId:
                    if (data.ReportXReportCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReportXReportCategoryDataModel.DataColumns.ReportXReportCategoryId, data.ReportXReportCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportXReportCategoryDataModel.DataColumns.ReportXReportCategoryId);
                    }
                    break;

                case ReportXReportCategoryDataModel.DataColumns.ReportId:
                    if (data.ReportId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReportXReportCategoryDataModel.DataColumns.ReportId, data.ReportId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportXReportCategoryDataModel.DataColumns.ReportId);
                    }
                    break;

                case ReportXReportCategoryDataModel.DataColumns.ReportCategoryId:
                    if (data.ReportCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReportXReportCategoryDataModel.DataColumns.ReportCategoryId, data.ReportCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportXReportCategoryDataModel.DataColumns.ReportCategoryId);
                    }
                    break;

                case ReportXReportCategoryDataModel.DataColumns.Report:
                    if (!string.IsNullOrEmpty(data.Report))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReportXReportCategoryDataModel.DataColumns.Report, data.Report);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportXReportCategoryDataModel.DataColumns.Report);
                    }
                    break;

                case ReportXReportCategoryDataModel.DataColumns.ReportCategory:
                    if (!string.IsNullOrEmpty(data.ReportCategory))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReportXReportCategoryDataModel.DataColumns.ReportCategory, data.ReportCategory);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportXReportCategoryDataModel.DataColumns.ReportCategory);
                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;
            }
            return returnValue;
        }

        #endregion

        #region Create By Report

        public static void CreateByReport(int reportId, int[] reportCategoryIds, RequestProfile requestProfile)
		{
			foreach (int reportCategoryId in reportCategoryIds)
			{
				var sql = "EXEC ReportXReportCategoryInsert " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @ReportId					=   " + reportId +
							",      @ReportCategoryId				=   " + reportCategoryId;

				DBDML.RunSQL("ReportXReportCategoryInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By ReportCategory

		public static void CreateByReportCategory(int ReportCategoryId, int[] ReportIds, RequestProfile requestProfile)
		{
			foreach (int ReportId in ReportIds)
			{
				var sql = "EXEC ReportXReportCategoryInsert " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @ReportId					=   " + ReportId +
							",      @ReportCategoryId				=   " + ReportCategoryId;
				DBDML.RunSQL("ReportXReportCategoryInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Get By ReportCategory

		public static DataTable GetByReportCategory(int reportCategoryId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReportXReportCategorySearch @ReportCategoryId     =" + reportCategoryId + ", " +
						  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get By Report

		public static DataTable GetByReport(int reportId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReportXReportCategorySearch @ReportId       =" + reportId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete

        public static void Delete(ReportXReportCategoryDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ReportXReportCategoryDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, ReportXReportCategoryDataModel.DataColumns.ReportXReportCategoryId);

            DBDML.RunSQL("ReportXReportCategory.Delete", sql, DataStoreKey);
        }

		#endregion

		#region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ReportXReportCategorySearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

            var oDT = new DBDataTable("ReportXReportCategory.List", sql, DataStoreKey);
            return oDT.DBTable;
        }


		#endregion

        #region GetDetails

        public static DataTable GetDetails(ReportXReportCategoryDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ReportXReportCategorySearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, ReportXReportCategoryDataModel.DataColumns.ReportXReportCategoryId);

            var oDT = new DBDataTable("ReportXReportCategory.Details", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

		#region Search

        public static DataTable Search(ReportXReportCategoryDataModel data, RequestProfile requestProfile)
        {           
            var sql = "EXEC dbo.ReportXReportCategorySearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, ReportXReportCategoryDataModel.DataColumns.ReportXReportCategoryId) +
                ", " + ToSQLParameter(data, ReportXReportCategoryDataModel.DataColumns.ReportCategoryId) +
                ", " + ToSQLParameter(data, ReportXReportCategoryDataModel.DataColumns.ReportId);

            var oDT = new DBDataTable("ReportXReportCategory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

		#endregion

		#region Create

        public static void Create(ReportXReportCategoryDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            DBDML.RunSQL("ReportXReportCategory.Insert", sql, DataStoreKey);
        }

		#endregion

		#region Update

        public static void Update(ReportXReportCategoryDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("ReportXReportCategory.Update", sql, DataStoreKey);
        }

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(ReportXReportCategoryDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReportXReportCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReportXReportCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, ReportXReportCategoryDataModel.DataColumns.ReportXReportCategoryId) +
						", " + ToSQLParameter(data, ReportXReportCategoryDataModel.DataColumns.ReportCategoryId) +
						", " + ToSQLParameter(data, ReportXReportCategoryDataModel.DataColumns.ReportId);
			return sql;
		}

		#endregion

		#region Delete By ReportCategory

		public static void DeleteByReportCategory(int reportCategoryId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReportXReportCategoryDelete ";

			var parameters =	new
								{
										AuditId				= requestProfile.AuditId
									,	ReportCategoryId	= reportCategoryId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Delete By Report

		public static void DeleteByReport(int reportId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReportXReportCategoryDelete ";

			var parameters =	new
								{
										AuditId		= requestProfile.AuditId
									,	ReportId	= reportId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

	}
}
