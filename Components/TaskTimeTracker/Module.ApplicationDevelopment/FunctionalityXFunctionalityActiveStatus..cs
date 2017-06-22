using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils; 

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class FunctionalityXFunctionalityActiveStatusDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";
		
		static FunctionalityXFunctionalityActiveStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityXFunctionalityActiveStatus");
		}

		#region GetList

        public static List<FunctionalityXFunctionalityActiveStatusDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityXFunctionalityActiveStatusDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static FunctionalityXFunctionalityActiveStatusDataModel GetDetails(FunctionalityXFunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityXFunctionalityActiveStatusDataModel> GetEntityDetails(FunctionalityXFunctionalityActiveStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
			const string sql = @"dbo.FunctionalityXFunctionalityActiveStatusSearch ";

			var parameters =
			new
			{
					AuditId										= requestProfile.AuditId
				,	ApplicationId								= requestProfile.ApplicationId
				,	ApplicationMode								= requestProfile.ApplicationModeId
				,	FunctionalityXFunctionalityActiveStatusId	= dataQuery.FunctionalityXFunctionalityActiveStatusId
				,	FunctionalityId								= dataQuery.FunctionalityId
				,	FunctionalityActiveStatusId					= dataQuery.FunctionalityActiveStatusId
				,	ReturnAuditInfo								= returnAuditInfo
				
			};

			List<FunctionalityXFunctionalityActiveStatusDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityXFunctionalityActiveStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

            return result;
        }

		#endregion

		#region Create

        public static void Create(FunctionalityXFunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("FunctionalityXFunctionalityActiveStatus.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

        public static void Update(FunctionalityXFunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("FunctionalityXFunctionalityActiveStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

        public static void Delete(FunctionalityXFunctionalityActiveStatusDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityXFunctionalityActiveStatusDelete ";

			var parameters =
			new
			{
					AuditId										= requestProfile.AuditId
				,	FunctionalityXFunctionalityActiveStatusId	= dataQuery.FunctionalityXFunctionalityActiveStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Search

        public static string ToSQLParameter(FunctionalityXFunctionalityActiveStatusDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityXFunctionalityActiveStatusId:
                    if (data.FunctionalityXFunctionalityActiveStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityXFunctionalityActiveStatusId, data.FunctionalityXFunctionalityActiveStatusId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityXFunctionalityActiveStatusId);
                    }
                    break;

                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityId:
                    if (data.FunctionalityId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityId, data.FunctionalityId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityId);
                    }
                    break;

                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId:
                    if (data.FunctionalityActiveStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId, data.FunctionalityActiveStatusId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);
                    }
                    break;

                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.AcknowledgedBy:
                    if (data.AcknowledgedBy != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.AcknowledgedBy);
                    }
                    break;

                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.KnowledgeDate:
                    if (data.KnowledgeDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.KnowledgeDate);
                    }
                    break;

                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Memo:
                    if (data.Memo != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Memo, data.Memo);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Memo);
                    }
                    break;

                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Functionality:
                    if (data.Functionality != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Functionality, data.Functionality);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Functionality);
                    }
                    break;

                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatus:
                    if (data.FunctionalityActiveStatus != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatus, data.FunctionalityActiveStatus);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatus);
                    }
                    break;

                case FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityPriority:
                    if (data.FunctionalityPriority != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatus, data.FunctionalityActiveStatus);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatus);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }


        public static DataTable Search(FunctionalityXFunctionalityActiveStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

        private static string CreateOrUpdate(FunctionalityXFunctionalityActiveStatusDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityXFunctionalityActiveStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
						
					break;

				case "Update":
					sql += "dbo.FunctionalityXFunctionalityActiveStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

            sql = sql + ", " + ToSQLParameter(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityXFunctionalityActiveStatusId, data.FunctionalityXFunctionalityActiveStatusId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                    ", " + ToSQLParameter(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityId, data.FunctionalityId) +
                ", " + ToSQLParameter(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId, data.FunctionalityActiveStatusId) +
                ", " + ToSQLParameter(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Memo, data.Memo) +
                ", " + ToSQLParameter(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate) +
                ", " + ToSQLParameter(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);
			return sql;
		}

		#endregion

        #region CreateByFunctionality

        public static void CreateByFunctionality(int functionalityId, int[] functionalityActiveStatusIds, RequestProfile requestProfile)
        {
            foreach (int functionalityActiveStatusId in functionalityActiveStatusIds)
            {
                var sql = "EXEC FunctionalityXFunctionalityActiveStatusInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @FunctionalityId					=   " + functionalityId +
                            ",   @FunctionalityActiveStatusId	    =   " + functionalityActiveStatusId;

                DBDML.RunSQL("FunctionalityXFunctionalityActiveStatusInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Delete By Functionality

        public static void DeleteByFunctionality(int functionalityId, RequestProfile requestProfile)
        {
            var sql = "EXEC FunctionalityXFunctionalityActiveStatusDelete @FunctionalityId		=" + functionalityId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Get By FunctionalityActiveStatus

        public static DataTable GetByFunctionalityActiveStatus(int functionalityActiveStatusId, RequestProfile requestProfile)
        {
            var sql = "EXEC FunctionalityXFunctionalityActiveStatusSearch @FunctionalityActiveStatusId     =" + functionalityActiveStatusId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Get By Functionality

        public static DataTable GetByFunctionality(int functionalityId, RequestProfile requestProfile)
        {
            var sql = "EXEC FunctionalityXFunctionalityActiveStatusSearch @FunctionalityId       =" + functionalityId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion
    }
}
