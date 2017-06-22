using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace Framework.Components.ApplicationUser
{
    public partial class ApplicationOperationDataManager : StandardDataManager
    {

        static readonly string DataStoreKey = "";
        
		static ApplicationOperationDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationOperation");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(ApplicationOperationDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case ApplicationOperationDataModel.DataColumns.ApplicationOperationId:
                    if (data.ApplicationOperationId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationOperationDataModel.DataColumns.ApplicationOperationId, data.ApplicationOperationId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationDataModel.DataColumns.ApplicationOperationId);

                    }
                    break;

                case BaseDataModel.BaseDataColumns.ApplicationId:
                    if (data.ApplicationId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.ApplicationId);

                    }
                    break;

                case ApplicationOperationDataModel.DataColumns.SystemEntityTypeId:
                    if (data.SystemEntityTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationOperationDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationDataModel.DataColumns.SystemEntityTypeId);

                    }
                    break;

                case ApplicationOperationDataModel.DataColumns.SystemEntityId:
                    if (data.SystemEntityTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationOperationDataModel.DataColumns.SystemEntityId, data.SystemEntityTypeId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationDataModel.DataColumns.SystemEntityId);

                    }
                    break;

                case StandardDataModel.StandardDataColumns.SortOrder:
                    if (data.SortOrder != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StandardDataModel.StandardDataColumns.SortOrder, data.SortOrder);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardDataModel.StandardDataColumns.SortOrder);

                    }
                    break;

                case ApplicationOperationDataModel.DataColumns.OperationValue:
                    if (!string.IsNullOrEmpty(data.OperationValue))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationOperationDataModel.DataColumns.OperationValue, data.OperationValue);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationDataModel.DataColumns.OperationValue);
                    }
                    break;

                case ApplicationOperationDataModel.DataColumns.Application:
                    if (!string.IsNullOrEmpty(data.Application))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationOperationDataModel.DataColumns.Application, data.Application);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationDataModel.DataColumns.Application);
                    }
                    break;

                case ApplicationOperationDataModel.DataColumns.SystemEntityType:
                    if (!string.IsNullOrEmpty(data.SystemEntityType))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationOperationDataModel.DataColumns.SystemEntityType, data.SystemEntityType);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationDataModel.DataColumns.SystemEntityType);
                    }
                    break;

                case StandardDataModel.StandardDataColumns.Name:
                    if (!string.IsNullOrEmpty(data.Name))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StandardDataModel.StandardDataColumns.Name, data.Name);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardDataModel.StandardDataColumns.Name);
                    }
                    break;

                case StandardDataModel.StandardDataColumns.Description:
                    if (!string.IsNullOrEmpty(data.Description))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StandardDataModel.StandardDataColumns.Description, data.Description);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardDataModel.StandardDataColumns.Description);
                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }
            return returnValue;
        }

        #endregion

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
			var data = new ApplicationOperationDataModel();
			var list = GetEntityDetails(data, requestProfile);
			return list.ToDataTable();
        }

        #endregion

        #region GetDetails

		public static DataTable GetDetails(ApplicationOperationDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);
			return list.ToDataTable();
        }

        #endregion

        #region Create

		public static void Create(ApplicationOperationDataModel data, RequestProfile requestProfile)
        {
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DataAccess.DBDML.RunSQL("ApplicationOperation.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(ApplicationOperationDataModel data, RequestProfile requestProfile)
        {
			var sql = CreateOrUpdate(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("ApplicationOperation.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(ApplicationOperationDataModel dataQuery, RequestProfile requestProfile)
        {
			const string sql = @"dbo.ApplicationOperationDelete ";

			var parameters =
			new
			{
					AuditId					= requestProfile.AuditId
				,	ApplicationOperationId	= dataQuery.ApplicationOperationId
				
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion

		static public List<ApplicationOperationDataModel> GetEntityDetails(ApplicationOperationDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationOperationSearch ";

			var parameters =
				new
				{
						AuditId                = requestProfile.AuditId
					,	ApplicationId          = dataQuery.ApplicationId
					,	ApplicationOperationId = dataQuery.ApplicationOperationId
					,	Name                   = dataQuery.Name
					,	SystemEntityId         = dataQuery.SystemEntityId
					,	ReturnAuditInfo        = returnAuditInfo
				};

			List<ApplicationOperationDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationOperationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

        #region Search

		public static DataTable Search(ApplicationOperationDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);
			return list.ToDataTable();
        }

        #endregion

        #region CreateOrUpdate

		private static string CreateOrUpdate(ApplicationOperationDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationOperationInsert  ";
                    break;

                case "Update":
                    sql += "dbo.ApplicationOperationUpdate  ";
                    break;

                default:
                    break;

            }

			sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(data, ApplicationOperationDataModel.DataColumns.ApplicationOperationId) +
						//", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                        ", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.ApplicationId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
                        ", " + ToSQLParameter(data, ApplicationOperationDataModel.DataColumns.SystemEntityTypeId) +
                        ", " + ToSQLParameter(data, ApplicationOperationDataModel.DataColumns.OperationValue);
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(ApplicationOperationDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new ApplicationOperationDataModel();
			doesExistRequest.SystemEntityTypeId = data.SystemEntityTypeId;
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
        }

        #endregion

        #region GetChildren

		private static DataSet GetChildren(ApplicationOperationDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationOperationChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, ApplicationOperationDataModel.DataColumns.ApplicationOperationId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

		public static bool IsDeletable(ApplicationOperationDataModel data, RequestProfile requestProfile)
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

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationOperationRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			Framework.Components.DataAccess.DBDML.RunSQL("ApplicationOperation.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

    }
}