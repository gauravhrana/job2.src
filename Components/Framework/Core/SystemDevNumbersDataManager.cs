using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace Framework.Components.Core
{
	public partial class SystemDevNumbersDataManager : BaseDataManager
	{
        static readonly string DataStoreKey = "";

		static SystemDevNumbersDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SystemDevNumbers");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(SystemDevNumbersDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{
				case SystemDevNumbersDataModel.DataColumns.SystemDevNumbersId:
					if (data.SystemDevNumbersId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemDevNumbersDataModel.DataColumns.SystemDevNumbersId, data.SystemDevNumbersId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemDevNumbersDataModel.DataColumns.SystemDevNumbersId);

					}
					break;

				case SystemDevNumbersDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemDevNumbersDataModel.DataColumns.Description, data.Description);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemDevNumbersDataModel.DataColumns.Description);

					}
					break;

				case SystemDevNumbersDataModel.DataColumns.RangeFrom:
					if (data.RangeFrom != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemDevNumbersDataModel.DataColumns.RangeFrom, data.RangeFrom);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemDevNumbersDataModel.DataColumns.RangeFrom);

					}
					break;

				case SystemDevNumbersDataModel.DataColumns.RangeTo:
					if (data.RangeTo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemDevNumbersDataModel.DataColumns.RangeTo, data.RangeTo);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemDevNumbersDataModel.DataColumns.RangeTo);

					}
					break;

				case SystemDevNumbersDataModel.DataColumns.PersonId:
					if (data.PersonId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemDevNumbersDataModel.DataColumns.PersonId, data.PersonId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemDevNumbersDataModel.DataColumns.PersonId);

					}
					break;

				case SystemDevNumbersDataModel.DataColumns.Person:
					if (!string.IsNullOrEmpty(data.Person))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemDevNumbersDataModel.DataColumns.Person, data.Person);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemDevNumbersDataModel.DataColumns.Person);

					}
					break;

				case SystemDevNumbersDataModel.DataColumns.ApplicationUserId:
					if (data.ApplicationUserId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemDevNumbersDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemDevNumbersDataModel.DataColumns.ApplicationUserId);

					}
					break;

				case SystemDevNumbersDataModel.DataColumns.ApplicationUser:
					if (!string.IsNullOrEmpty(data.ApplicationUser))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemDevNumbersDataModel.DataColumns.ApplicationUser, data.ApplicationUser);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemDevNumbersDataModel.DataColumns.ApplicationUser);

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

        public static List<SystemDevNumbersDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(SystemDevNumbersDataModel.Empty, requestProfile, 0);
		}

		#endregion GetList

		#region Search

		public static DataTable Search(SystemDevNumbersDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL  
			var sql = "EXEC dbo.SystemDevNumbersSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, SystemDevNumbersDataModel.DataColumns.ApplicationUserId) +
				", " + ToSQLParameter(data, SystemDevNumbersDataModel.DataColumns.SystemDevNumbersId);

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

        public static List<SystemDevNumbersDataModel> GetEntityDetails(SystemDevNumbersDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.SystemDevNumbersSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   ApplicationUserId             = dataQuery.ApplicationUserId
                ,   SystemDevNumbersId               = dataQuery.SystemDevNumbersId
            };

            List<SystemDevNumbersDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<SystemDevNumbersDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

		#endregion Search

		#region GetDetails

        public static SystemDevNumbersDataModel GetDetails(SystemDevNumbersDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion GetDetails

		#region Save
		private static string Save(SystemDevNumbersDataModel data, RequestProfile requestProfile, string action)
		{


			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SystemDevNumbersInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SystemDevNumbersUpdate  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, SystemDevNumbersDataModel.DataColumns.SystemDevNumbersId) +
				", " + ToSQLParameter(data, SystemDevNumbersDataModel.DataColumns.Description) +
				", " + ToSQLParameter(data, SystemDevNumbersDataModel.DataColumns.PersonId) +
				", " + ToSQLParameter(data, SystemDevNumbersDataModel.DataColumns.RangeFrom) +
				", " + ToSQLParameter(data, SystemDevNumbersDataModel.DataColumns.RangeTo);

			return sql;

		}
		#endregion Save

		#region Create
		public static void Create(SystemDevNumbersDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");

			DBDML.RunSQL("SystemDevNumbers.Insert", sql, DataStoreKey);
		}
		#endregion Create

		#region Update
		public static void Update(SystemDevNumbersDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");

			DBDML.RunSQL("SystemDevNumbers.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Delete

		public static void Delete(SystemDevNumbersDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SystemDevNumbersDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SystemDevNumbersId = dataQuery.SystemDevNumbersId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(SystemDevNumbersDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SystemDevNumbersDataModel();
			doesExistRequest.PersonId = data.PersonId;

            var list = Search(doesExistRequest, requestProfile);
            return list.Rows.Count > 0;
		}


		#endregion DoesExist

	}
}
