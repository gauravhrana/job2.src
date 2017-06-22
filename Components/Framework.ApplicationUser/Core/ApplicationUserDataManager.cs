using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.ApplicationUser
{
    public partial class ApplicationUserDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ApplicationUserDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationUser");
        }

        #region ToSQLParameter

        public static string ToSQLParameter(ApplicationUserDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case ApplicationUserDataModel.DataColumns.ApplicationUserId:
                    if (data.ApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.ApplicationUserId);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.ApplicationUserName:
                    if (!string.IsNullOrEmpty(data.ApplicationUserName))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserDataModel.DataColumns.ApplicationUserName, data.ApplicationUserName);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.ApplicationUserName);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.EmailAddress:
                    if (!string.IsNullOrEmpty(data.EmailAddress))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserDataModel.DataColumns.EmailAddress, data.EmailAddress);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.EmailAddress);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.FirstName:
                    if (!string.IsNullOrEmpty(data.FirstName))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserDataModel.DataColumns.FirstName, data.FirstName);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.FirstName);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.LastName:
                    if (!string.IsNullOrEmpty(data.LastName))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserDataModel.DataColumns.LastName, data.LastName);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.LastName);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.MiddleName:
                    if (!string.IsNullOrEmpty(data.MiddleName))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserDataModel.DataColumns.MiddleName, data.MiddleName);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.MiddleName);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.FullName:
                    if (!string.IsNullOrEmpty(data.FullName))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserDataModel.DataColumns.FullName, data.FullName);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.FullName);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.ApplicationUserTitleId:
                    if (data.ApplicationUserTitleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserDataModel.DataColumns.ApplicationUserTitleId, data.ApplicationUserTitleId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.ApplicationUserTitleId);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.ApplicationUserTitle:
                    if (!string.IsNullOrEmpty(data.FullName))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserDataModel.DataColumns.ApplicationUserTitle, data.ApplicationUserTitle);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.ApplicationUserTitle);
                    }
                    break;

                case ApplicationUserDataModel.DataColumns.Application:
                    if (data.Application != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserDataModel.DataColumns.Application, data.Application);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserDataModel.DataColumns.Application);
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

        public static List<ApplicationUserDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ApplicationUserDataModel.Empty, requestProfile, 0);
        }

        public static List<ApplicationUserDataModel> GetApplicationUserList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserSearch " +
               " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
               ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var result = new List<ApplicationUserDataModel>();

            using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new ApplicationUserDataModel();

                    dataItem.ApplicationUserId = (int)dbReader[ApplicationUserDataModel.DataColumns.ApplicationUserId];
                    dataItem.ApplicationUserName = dbReader[ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
                    dataItem.FirstName = dbReader[ApplicationUserDataModel.DataColumns.FirstName].ToString();
                    dataItem.LastName = dbReader[ApplicationUserDataModel.DataColumns.LastName].ToString();
                    dataItem.MiddleName = dbReader[ApplicationUserDataModel.DataColumns.MiddleName].ToString();
                    dataItem.EmailAddress = dbReader[ApplicationUserDataModel.DataColumns.EmailAddress].ToString();
					dataItem.ApplicationCode = dbReader[ApplicationUserDataModel.DataColumns.ApplicationCode].ToString();
                    dataItem.FullName = dataItem.FirstName + " ";
                    dataItem.FullName += dataItem.MiddleName == string.Empty || dataItem.MiddleName == "." ? (dataItem.LastName) : (dataItem.MiddleName + " " + dataItem.LastName);

                    result.Add(dataItem);
                }
            }

            return result;
        }

		public static List<ApplicationUserDataModel> GetAllApplicationUserList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationUserSearch " +
			   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
			   ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, null);

			var result = new List<ApplicationUserDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new ApplicationUserDataModel();

					dataItem.ApplicationUserId = (int)dbReader[ApplicationUserDataModel.DataColumns.ApplicationUserId];
					dataItem.ApplicationUserName = dbReader[ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
					dataItem.FirstName = dbReader[ApplicationUserDataModel.DataColumns.FirstName].ToString();
					dataItem.LastName = dbReader[ApplicationUserDataModel.DataColumns.LastName].ToString();
					dataItem.MiddleName = dbReader[ApplicationUserDataModel.DataColumns.MiddleName].ToString();
					dataItem.EmailAddress = dbReader[ApplicationUserDataModel.DataColumns.EmailAddress].ToString();
					dataItem.ApplicationCode = dbReader[ApplicationUserDataModel.DataColumns.ApplicationCode].ToString();
					dataItem.FullName = dataItem.FirstName + " ";
					dataItem.FullName += dataItem.MiddleName == string.Empty || dataItem.MiddleName == "." ? (dataItem.LastName) : (dataItem.MiddleName + " " + dataItem.LastName);
					dataItem.ApplicationUserFullName = dbReader[ApplicationUserDataModel.DataColumns.FirstName]
						+ " " + dbReader[ApplicationUserDataModel.DataColumns.MiddleName]
						+ " " + dbReader[ApplicationUserDataModel.DataColumns.LastName];

					result.Add(dataItem);
				}
			}

			return result;
		}

        public static DataTable GetList(string prefixText, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", @Name='" + prefixText + "'";

            var oDT = new DataAccess.DBDataTable("ApplicationUser.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static ApplicationUserDataModel GetDetails(ApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

		public static DataTable GetFullNameDetails(ApplicationUserDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

        #endregion

        #region GetEntityDetails

        public static List<ApplicationUserDataModel> GetEntityDetails(ApplicationUserDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ApplicationUserSearch ";

            var parameters =
            new
            {

					AuditId                = requestProfile.AuditId
                ,	ApplicationId          = requestProfile.ApplicationId
                ,	ReturnAuditInfo        = returnAuditInfo
                ,	ApplicationMode        = requestProfile.ApplicationModeId
                ,	ApplicationUserId      = dataQuery.ApplicationUserId
                ,	ApplicationUserName    = dataQuery.ApplicationUserName
                ,	EmailAddress           = dataQuery.EmailAddress
                ,	FirstName              = dataQuery.FirstName
                ,	MiddleName             = dataQuery.MiddleName
                ,	LastName               = dataQuery.LastName
                ,	ApplicationUserTitleId = dataQuery.ApplicationUserTitleId
            };

            List<ApplicationUserDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ApplicationUserDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(ApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("ApplicationUser.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("ApplicationUser.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ApplicationUserDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ApplicationUserDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationUserId = dataQuery.ApplicationUserId

            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static DataTable Search(ApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        public static string GetFullName(ApplicationUserDataModel data, RequestProfile requestProfile)
        {
			var dr = GetFullNameDetails(data, requestProfile).Rows[0];
            var value = dr["FirstName"]
                        + " " + dr["MiddleName"]
                        + " " + dr["LastName"];

            return value;
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(ApplicationUserDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationUserInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) ;
                    break;

                case "Update":
                    sql += "dbo.ApplicationUserUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.ApplicationUserId) +
                        ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.ApplicationId) +
                        ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.ApplicationUserName) +
                        ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.EmailAddress) +
                        ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.FirstName) +
                        ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.LastName) +
                        ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.MiddleName) +
                        ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.ApplicationUserTitleId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest                 = new ApplicationUserDataModel();
            doesExistRequest.ApplicationUserName = data.ApplicationUserName;
            doesExistRequest.ApplicationId       = data.ApplicationId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(ApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, ApplicationUserDataModel.DataColumns.ApplicationUserId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(ApplicationUserDataModel data, RequestProfile requestProfile)
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
            var sql = "EXEC dbo.ApplicationUserRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                      ",@Seed = " + seed +
                      ",@Increment = " + increment;

            Framework.Components.DataAccess.DBDML.RunSQL("ApplicationUser.Renumber", sql, DataStoreKey);
        }

        #endregion Renumber

		
    }
}
