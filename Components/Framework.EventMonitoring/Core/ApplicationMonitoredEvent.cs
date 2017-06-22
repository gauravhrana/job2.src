using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.EventMonitoring;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.EventMonitoring
{
    public partial class ApplicationMonitoredEventDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";        

        static ApplicationMonitoredEventDataManager()
        {            
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationMonitoredEvent");
        }

        #region GetList

		public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationMonitoredEventSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
				 var oDT = new DataAccess.DBDataTable("ApplicationMonitoredEvent.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

		#endregion

		#region GetEntitySearch

		public static List<ApplicationMonitoredEvenDataModel> GetEntityDetails(ApplicationMonitoredEvenDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationMonitoredEventSearch ";

			var parameters =
			new
			{
					AuditId										= requestProfile.AuditId
				,	ApplicationMode								= requestProfile.ApplicationModeId
				,	ApplicationId								= requestProfile.ApplicationId
				,	ApplicationMonitoredEventId					= dataQuery.ApplicationMonitoredEventId
				,	ReturnAuditInfo								= returnAuditInfo
				,	ApplicationMonitoriedEventProcessingStateId	= dataQuery.ApplicationMonitoredEventProcessingStateId
				,	ApplicationMonitoredEventSourceId			= dataQuery.ApplicationMonitoredEventSourceId
				,	LastModifiedBy								= dataQuery.LastModifiedBy
			};

			List<ApplicationMonitoredEvenDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationMonitoredEvenDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


			return result;
		}

        #endregion

        #region GetDetails

		public static DataTable GetDetails(ApplicationMonitoredEvenDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
        }

        #endregion

        #region Create

		public static void Create(ApplicationMonitoredEvenDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEvent.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(ApplicationMonitoredEvenDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("ApplicationMonitoredEvent.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(ApplicationMonitoredEvenDataModel dataQuery, RequestProfile requestProfile)
        {
			const string sql = @"dbo.ApplicationMonitoredEventDelete ";

			var parameters =
			new
			{
					AuditId							= requestProfile.AuditId
				,	ApplicationMonitoredEventId		= dataQuery.ApplicationMonitoredEventId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion

        #region Search

		public static string ToSQLParameter(ApplicationMonitoredEvenDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventId:
					if (data.ApplicationMonitoredEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventId, data.ApplicationMonitoredEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventId);
					}
					break;
				

				case ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventSourceId:
					if (data.ApplicationMonitoredEventSourceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventSourceId, data.ApplicationMonitoredEventSourceId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventSourceId);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId:
					if (data.ApplicationMonitoredEventProcessingStateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId, data.ApplicationMonitoredEventProcessingStateId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.ReferenceId:
					if (data.ReferenceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEvenDataModel.DataColumns.ReferenceId, data.ReferenceId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.ReferenceId);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.ReferenceCode:
					if (!string.IsNullOrEmpty(data.ReferenceCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEvenDataModel.DataColumns.ReferenceCode, data.ReferenceCode.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.ReferenceCode);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.Category:
					if (!string.IsNullOrEmpty(data.Category))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEvenDataModel.DataColumns.Category, data.Category.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.Category);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.Message:
					if (!string.IsNullOrEmpty(data.Message))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEvenDataModel.DataColumns.Message, data.Message.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.Message);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.IsDuplicate:
					if (data.IsDuplicate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationMonitoredEvenDataModel.DataColumns.IsDuplicate, data.IsDuplicate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.IsDuplicate);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.LastModifiedBy:
					if (!string.IsNullOrEmpty(data.LastModifiedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEvenDataModel.DataColumns.LastModifiedBy, data.LastModifiedBy.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.LastModifiedBy);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.LastModifiedOn:
					if (data.LastModifiedOn != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEvenDataModel.DataColumns.LastModifiedOn, data.LastModifiedOn);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.LastModifiedOn);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventSource:
					if (!string.IsNullOrEmpty(data.ApplicationMonitoredEventSource))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventSource, data.ApplicationMonitoredEventSource.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventSource);

					}
					break;

				case ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventProcessingState:
					if (!string.IsNullOrEmpty(data.ApplicationMonitoredEventProcessingState))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventProcessingState,data.ApplicationMonitoredEventProcessingState.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventProcessingState);

					}
					break;	


				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(ApplicationMonitoredEvenDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table; 
        }

        #endregion

        #region Save

		private static string Save(ApplicationMonitoredEvenDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationMonitoredEventInsert  " +
					   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
                    break;

                case "Update":
					sql += "dbo.ApplicationMonitoredEventUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventId) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventSourceId) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.ReferenceId) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.ReferenceCode) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.Category) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.Message) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.IsDuplicate) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.LastModifiedOn) +
					", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.LastModifiedBy);              

				
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(ApplicationMonitoredEvenDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationMonitoredEventSearch " +
			" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
			", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId) +
			", " + ToSQLParameter(data, ApplicationMonitoredEvenDataModel.DataColumns.ApplicationMonitoredEventId);

            var oDT = new DataAccess.DBDataTable("ApplicationMonitoredEvent.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

    }
}
