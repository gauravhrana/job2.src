using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.BatchFileDataModel;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Import;
using System.Data;
using Dapper;

namespace Framework.Components.Import
{
    public partial class BatchFileDataManager : StandardDataManager 
    {

        static readonly string DataStoreKey = "";

        static BatchFileDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("BatchFile");
        }

		#region ToSQLParameter



		public static string ToSQLParameter(BatchFileDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BatchFileDataModel.DataColumns.BatchFileId:
					if (data.BatchFileId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileDataModel.DataColumns.BatchFileId, data.BatchFileId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.BatchFileId);
					}
					break;

				case BatchFileDataModel.DataColumns.FileTypeId:
					if (data.FileTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileDataModel.DataColumns.FileTypeId, data.FileTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.FileTypeId);
					}
					break;

				case BatchFileDataModel.DataColumns.BatchFileSetId:
					if (data.BatchFileSetId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileDataModel.DataColumns.BatchFileSetId, data.BatchFileSetId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.BatchFileSetId);
					}
					break;

				case BatchFileDataModel.DataColumns.BatchFileStatusId:
					if (data.BatchFileStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileDataModel.DataColumns.BatchFileStatusId, data.BatchFileStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.BatchFileStatusId);
					}
					break;

				case BatchFileDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.SystemEntityTypeId);
					}
					break;

				case BatchFileDataModel.DataColumns.Folder:
					if (!string.IsNullOrEmpty(data.Folder))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BatchFileDataModel.DataColumns.Folder, data.Folder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.Folder);
					}
					break;

				case BatchFileDataModel.DataColumns.BatchFile:
					if (!string.IsNullOrEmpty(data.BatchFile))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BatchFileDataModel.DataColumns.BatchFile, data.BatchFile);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.BatchFile);
					}
					break;

				case BatchFileDataModel.DataColumns.Errors:
					if (!string.IsNullOrEmpty(data.Errors))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BatchFileDataModel.DataColumns.Errors, data.Errors);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.Errors);
					}
					break;

				case BatchFileDataModel.DataColumns.CreatedByPersonId:
					if (data.CreatedByPersonId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileDataModel.DataColumns.CreatedByPersonId, data.CreatedByPersonId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileDataModel.DataColumns.CreatedByPersonId);
					}
					break;			

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#endregion

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
			var sql = "EXEC dbo.BatchFileSearch" +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var oDT = new DataAccess.DBDataTable("BatchFile.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(BatchFileDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.BatchFileSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
                ", " +ToSQLParameter(data,BatchFileDataModel.DataColumns.BatchFileId);

            var oDT = new DataAccess.DBDataTable("BatchFile.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create

        public static void Create(BatchFileDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("BatchFile.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(BatchFileDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("BatchFile.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(BatchFileDataModel data, RequestProfile requestProfile)
        {
			const string sql = @"dbo.BatchFileDelete ";

			var parameters = new
			{
					AuditId			= requestProfile.AuditId
				,	BatchFileId		= data.BatchFileId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion

        #region Search

        public static DataTable Search(BatchFileDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.BatchFileSearch " +
                " "  + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data,BatchFileDataModel.DataColumns.BatchFileId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.BatchFileStatusId) +
				", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.BatchFileSetId) ;

            var oDT = new DataAccess.DBDataTable("BatchFile.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(BatchFileDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.BatchFileInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.BatchFileUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.Folder) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.BatchFile) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.BatchFileSetId) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.Description) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.FileTypeId) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.SystemEntityTypeId) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.BatchFileStatusId) +
						", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.CreatedDate) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.CreatedByPersonId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.UpdatedDate) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.UpdatedByPersonId) +
						", " + ToSQLParameter(data, BatchFileDataModel.DataColumns.Errors);


						//", " + data.ToSQLParameter(DataColumns.Name) +
						//", " + data.ToSQLParameter(DataColumns.Folder) +
						//", " + data.ToSQLParameter(DataColumns.BatchFile) +
						//", " + data.ToSQLParameter(DataColumns.BatchFileSetId) +
						//", " + data.ToSQLParameter(DataColumns.Description) +
						//", " + data.ToSQLParameter(DataColumns.FileTypeId) +
						//", " + data.ToSQLParameter(DataColumns.SystemEntityTypeId) +
						//", " + data.ToSQLParameter(DataColumns.BatchFileStatusId) +
						//", " + data.ToSQLParameter(DataColumns.CreatedDate) +
						//", " + data.ToSQLParameter(DataColumns.CreatedByPersonId) +
						//", " + data.ToSQLParameter(DataColumns.UpdatedDate) +
						//", " + data.ToSQLParameter(DataColumns.UpdatedByPersonId) +
						//", " + data.ToSQLParameter(DataColumns.Errors);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(BatchFileDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new BatchFileDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(BatchFileDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.BatchFileChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data,BatchFileDataModel.DataColumns.BatchFileId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(BatchFileDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;
            var ds = GetChildren(data, requestProfile);
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
