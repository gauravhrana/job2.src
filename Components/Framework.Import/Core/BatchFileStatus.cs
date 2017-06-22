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
    public partial class BatchFileStatusDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";

		static BatchFileStatusDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("BatchFileStatus");
        }

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
			var sql = "EXEC dbo.BatchFileStatusSearch" +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var oDT = new DataAccess.DBDataTable("BatchFileStatus.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

		#region ToSQLParameter

		public static string ToSQLParameter(BatchFileStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BatchFileStatusDataModel.DataColumns.BatchFileStatusId:
					if (data.BatchFileStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BatchFileStatusDataModel.DataColumns.BatchFileStatusId, data.BatchFileStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BatchFileStatusDataModel.DataColumns.BatchFileStatusId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}
		
		#endregion

        #region GetDetails

        public static DataTable GetDetails(BatchFileStatusDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
        }

		#endregion

		#region GetEntitySearch

		public static List<BatchFileStatusDataModel> GetEntityDetails(BatchFileStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
			const string sql = @"dbo.BatchFileStatusSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	ApplicationId				= dataQuery.ApplicationId
				,	BatchFileStatusId			= dataQuery.BatchFileStatusId
				,	Name						= dataQuery.Name
				,	ReturnAuditInfo				= returnAuditInfo

			};

			List<BatchFileStatusDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<BatchFileStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


            return result;
        }

        #endregion

        #region Create

        public static void Create(BatchFileStatusDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("BatchFileStatus.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(BatchFileStatusDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("BatchFileStatus.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(BatchFileStatusDataModel dataQuery, RequestProfile requestProfile)
        {
			const string sql = @"dbo.BatchFileStatusDelete ";

			var parameters =
			new
			{
					AuditId				= requestProfile.AuditId
				,	BatchFileStatusId	= dataQuery.BatchFileStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}

				
        }

        #endregion

        #region Search

        public static DataTable Search(BatchFileStatusDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
        }

        #endregion

        #region Save

        private static string Save(BatchFileStatusDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.BatchFileStatusInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.BatchFileStatusUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, BatchFileStatusDataModel.DataColumns.BatchFileStatusId) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(BatchFileStatusDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new BatchFileStatusDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);

        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(BatchFileStatusDataModel data, RequestProfile requestProfile)
        {
			var sql = "EXEC dbo.BatchFileStatusChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						   ", " + ToSQLParameter(data, BatchFileStatusDataModel.DataColumns.BatchFileStatusId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(BatchFileStatusDataModel data, RequestProfile requestProfile)
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
