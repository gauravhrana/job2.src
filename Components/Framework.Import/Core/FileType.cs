using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Import;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Import
{
    public partial class FileTypeDataManager : StandardDataManager
    {
        private static string DataStoreKey = "";

		static FileTypeDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FileType");
        }

		#region ToSQLParameter

		public static string ToSQLParameter(FileTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FileTypeDataModel.DataColumns.FileTypeId:
					if (data.FileTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FileTypeDataModel.DataColumns.FileTypeId, data.FileTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FileTypeDataModel.DataColumns.FileTypeId);
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
			var sql = "EXEC dbo.FileTypeSearch" +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
                + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
            var oDT = new DBDataTable("FileType.GetList", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion GetList

        #region Search

        public static DataTable Search(FileTypeDataModel data, RequestProfile requestProfile,int applicationMode=0)
        {
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
        }

        #endregion Search

        #region GetDetails

        public static DataTable GetDetails(FileTypeDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
        }

		#endregion 

		#region GetEntitySearch

		public static List<FileTypeDataModel> GetEntityDetails(FileTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
			const string sql = @"dbo.FileTypeSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	ApplicationId				= dataQuery.ApplicationId
				,	FileTypeId					= dataQuery.FileTypeId
				,	Name						= dataQuery.Name
				,	ReturnAuditInfo				= returnAuditInfo

			};

			List<FileTypeDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FileTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

            return result;
        }

        #endregion 

        #region CreateOrUpdate
        private static string CreateOrUpdate(FileTypeDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.FileTypeInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FileTypeUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);                            
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, FileTypeDataModel.DataColumns.FileTypeId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

            return sql;
        }

        #endregion CreateOrUpdate

        #region Create

        public static int Create(FileTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            var Id = Framework.Components.DataAccess.DBDML.RunScalarSQL("FileType.Insert", sql, DataStoreKey);
            return Convert.ToInt32(Id);
        }

        #endregion Create

        #region Update

        public static void Update(FileTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");

            DBDML.RunSQL("FileType.Update", sql, DataStoreKey);
        }
        #endregion Update

        #region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FileTypeRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                      ",@Seed = " + seed +
                      ",@Increment = " + increment;

            DBDML.RunSQL("FileType.Renumber", sql, DataStoreKey);
        }
        #endregion Renumber

        #region Delete

        public static void Delete(FileTypeDataModel dataQuery, RequestProfile requestProfile)
        {
			const string sql = @"dbo.FileTypeDelete ";

			var parameters =
			new
			{
					AuditId		= requestProfile.AuditId
				,	FileTypeId	= dataQuery.FileTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion Delete

        #region DoesExist

        public static DataTable DoesExist(FileTypeDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new FileTypeDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
        }

        #endregion DoesExist

        #region GetChildren

		private static DataSet GetChildren(FileTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FileTypeChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, FileTypeDataModel.DataColumns.FileTypeId);

            var oDT = new DBDataSet("FileType.GetChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region DeleteChildren

        public static DataSet DeleteChildren(FileTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FileTypeChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, FileTypeDataModel.DataColumns.FileTypeId);

            var oDT = new DBDataSet("FileType.DeleteChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(FileTypeDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;

            var ds = GetChildren(data, requestProfile);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
                {
                    isDeletable = false;
                }
            }

            return isDeletable;
        }

        #endregion
    }
}
