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
    public partial class NotificationRegistrarDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";        

        static NotificationRegistrarDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationRegistrar");
        }

        #region GetList

		public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.NotificationRegistrarSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationRegistrar.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

		public static DataTable GetDetails(NotificationRegistrarDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityList(data, requestProfile);

			var table = list.ToDataTable();

			return table;
        }

		#endregion

		#region GetEntitySearch

		public static List<NotificationRegistrarDataModel> GetEntityList(NotificationRegistrarDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.NotificationRegistrarSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,   NotificationRegistrarId		= dataQuery.NotificationRegistrarId
				,	ApplicationId				= dataQuery.ApplicationId
				,	NotificationeventTyepId		= dataQuery.NotificationEventTypeId
				,	NotificationPublisherId		= dataQuery.NotificationPublisherId
				,	Message						= dataQuery.Message
				,	PublishDateId				= dataQuery.PublishDateId
				,	PublishTimeId				= dataQuery.PublishTimeId
				,	ReturnAuditInfo				= returnAuditInfo
				,	ApplicationMode				= requestProfile.ApplicationModeId
			};

			List<NotificationRegistrarDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<NotificationRegistrarDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}		

        #endregion

		#region GetPublishDetails

		public static DataTable GetPublishDetails(DateTime currentPublishdate, decimal maxcreatedDateid)
        {
            var sql = "EXEC dbo.NotificationRegistrarPublishSearch " +               
               
				"  @MaxPublishDate=" + maxcreatedDateid+
			"  ,@CurrentPublishdate='"+currentPublishdate+"'";

            var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationRegistrar.PublishDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static DataTable GetPublishDetails(decimal maxcreatedDateid)
		{
			var sql = "EXEC dbo.NotificationRegistrarPublishSearch " +
				"  @MaxPublishDate=" + maxcreatedDateid;
			

			var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationRegistrar.PublishDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		 #endregion

        #region Create

		public static void Create(NotificationRegistrarDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("NotificationRegistrar.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(NotificationRegistrarDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("NotificationRegistrar.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(NotificationRegistrarDataModel dataQuery, RequestProfile requestProfile)
        {
			const string sql = @"dbo.NotificationRegistrarDelete ";

			var parameters =
			new
			{
					AuditId								= requestProfile.AuditId
				,	NotificationRegistrarId				= dataQuery.NotificationRegistrarId
				
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion

        #region Search

		public static string ToSQLParameter(NotificationRegistrarDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case NotificationRegistrarDataModel.DataColumns.NotificationPublisherId:
					if (data.NotificationPublisherId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationRegistrarDataModel.DataColumns.NotificationPublisherId, data.NotificationPublisherId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationRegistrarDataModel.DataColumns.NotificationPublisherId);
					}
					break;

				case NotificationRegistrarDataModel.DataColumns.NotificationRegistrarId:
					if (data.NotificationRegistrarId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationRegistrarDataModel.DataColumns.NotificationRegistrarId, data.NotificationRegistrarId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationRegistrarDataModel.DataColumns.NotificationRegistrarId);
					}
					break;

				case NotificationRegistrarDataModel.DataColumns.NotificationEventTypeId:
					if (data.NotificationEventTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationRegistrarDataModel.DataColumns.NotificationEventTypeId, data.NotificationEventTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationRegistrarDataModel.DataColumns.NotificationEventTypeId);
					}
					break;				

				case NotificationRegistrarDataModel.DataColumns.PublishDateId:
					if (data.PublishDateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationRegistrarDataModel.DataColumns.PublishDateId, data.PublishDateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationRegistrarDataModel.DataColumns.PublishDateId);
					}
					break;

				case NotificationRegistrarDataModel.DataColumns.PublishTimeId:
					if (data.PublishTimeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationRegistrarDataModel.DataColumns.PublishTimeId, data.PublishTimeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationRegistrarDataModel.DataColumns.PublishTimeId);
					}
					break;

				case NotificationRegistrarDataModel.DataColumns.Message:
					if (!string.IsNullOrEmpty(data.Message))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NotificationRegistrarDataModel.DataColumns.Message, data.Message);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationRegistrarDataModel.DataColumns.Message);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(NotificationRegistrarDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityList(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
        }

        #endregion

        #region Save

		private static string Save(NotificationRegistrarDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.NotificationRegistrarInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.NotificationRegistrarUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)+
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, NotificationRegistrarDataModel.DataColumns.NotificationRegistrarId) +
					 ", " + ToSQLParameter(data, NotificationRegistrarDataModel.DataColumns.NotificationEventTypeId) +
					 ", " + ToSQLParameter(data, NotificationRegistrarDataModel.DataColumns.NotificationPublisherId) +
					 ", " + ToSQLParameter(data, NotificationRegistrarDataModel.DataColumns.Message);
					

            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(NotificationRegistrarDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.NotificationRegistrarSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId) +
			  ", " + ToSQLParameter(data, NotificationRegistrarDataModel.DataColumns.NotificationRegistrarId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationRegistrar.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

    }
}
