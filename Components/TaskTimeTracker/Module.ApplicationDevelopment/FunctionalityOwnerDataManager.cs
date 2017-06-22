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
	public partial class FunctionalityOwnerDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static FunctionalityOwnerDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityOwner");
		}

		#region GetList

        public static List<FunctionalityOwnerDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityOwnerDataModel.Empty, requestProfile, 1);
		}

		

		#endregion

		#region GetDetails

		public static FunctionalityOwnerDataModel GetDetails(FunctionalityOwnerDataModel data, RequestProfile requestedProfile)
		{
			var list = GetEntityDetails(data, requestedProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityOwnerDataModel> GetEntityDetails(FunctionalityOwnerDataModel dataQuery, RequestProfile requestedProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityOwnerSearch ";

			var parameters =
			new
			{
					AuditId = requestedProfile.AuditId
				,	ApplicationId = dataQuery.ApplicationId
				,	FunctionalityId = dataQuery.FunctionalityId
				,	FunctionalityOwnerId = dataQuery.FunctionalityOwnerId
				,	DeveloperRoleId = dataQuery.DeveloperRoleId
				,	Developer = dataQuery.Developer
				,	FeatureOwnerStatusId = dataQuery.FeatureOwnerStatusId
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<FunctionalityOwnerDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityOwnerDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


			return result;
		}

		#endregion

		#region Create

		public static void Create(FunctionalityOwnerDataModel data, RequestProfile requestedProfile)
		{
			var sql = Save(data, requestedProfile, "Create");
			DBDML.RunSQL("FunctionalityOwner.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(FunctionalityOwnerDataModel data, RequestProfile requestedProfile)
		{
			var sql = Save(data, requestedProfile, "Update");
			DBDML.RunSQL("FunctionalityOwner.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FunctionalityOwnerDataModel dataQuery, RequestProfile requestedProfile)
		{
			const string sql = @"dbo.FunctionalityOwnerDelete ";

			var parameters =
			new
			{
				AuditId = requestedProfile.AuditId
				,
				FunctionalityOwnerId = dataQuery.FunctionalityOwnerId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(FunctionalityOwnerDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityOwnerDataModel.DataColumns.FunctionalityOwnerId:
					if (data.FunctionalityOwnerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.FunctionalityOwnerId, data.FunctionalityOwnerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.FunctionalityOwnerId);
					}
					break;

				case FunctionalityOwnerDataModel.DataColumns.FunctionalityId:
					if (data.FunctionalityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.FunctionalityId, data.FunctionalityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.FunctionalityId);
					}
					break;

				case FunctionalityOwnerDataModel.DataColumns.DeveloperRoleId:
					if (data.DeveloperRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.DeveloperRoleId, data.DeveloperRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.DeveloperRoleId);
					}
					break;

				case FunctionalityOwnerDataModel.DataColumns.Developer:
					if (data.Developer != null && !string.IsNullOrEmpty(data.Developer))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.Developer, data.Developer);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.Developer);
					}
					break;

				case FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatusId:
					if (data.FeatureOwnerStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatusId, data.FeatureOwnerStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatusId);
					}
					break;

				case FunctionalityOwnerDataModel.DataColumns.Functionality:
					if (data.Functionality != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.Functionality, data.Functionality);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.Functionality);
					}
					break;

				case FunctionalityOwnerDataModel.DataColumns.DeveloperRole:
					if (data.DeveloperRole != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.DeveloperRole, data.DeveloperRole);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.DeveloperRole);
					}
					break;


				case FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatus:
					if (data.FeatureOwnerStatus != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatus, data.FeatureOwnerStatus);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatus);
					}
					break;

				case FunctionalityOwnerDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityOwnerDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.ApplicationId);
					}
					break;

				case FunctionalityOwnerDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityOwnerDataModel.DataColumns.Application, data.Application.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityOwnerDataModel.DataColumns.Application);
					}
					break;



				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(FunctionalityOwnerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(FunctionalityOwnerDataModel data, RequestProfile requestedProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityOwnerInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
						", " + ToSQLParameter(FunctionalityOwnerDataModel.DataColumns.ApplicationId, requestedProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityOwnerUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
					 ", " + ToSQLParameter(data, FunctionalityOwnerDataModel.DataColumns.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, FunctionalityOwnerDataModel.DataColumns.FunctionalityOwnerId) +
					", " + ToSQLParameter(data, FunctionalityOwnerDataModel.DataColumns.FunctionalityId) +
					", " + ToSQLParameter(data, FunctionalityOwnerDataModel.DataColumns.DeveloperRoleId) +
					", " + ToSQLParameter(data, FunctionalityOwnerDataModel.DataColumns.Developer) +
					", " + ToSQLParameter(data, FunctionalityOwnerDataModel.DataColumns.FeatureOwnerStatusId);
			return sql;
		}

		#endregion

		#region DoesExist

        public static bool DoesExist(FunctionalityOwnerDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityOwnerDataModel();
			doesExistRequest.Name = data.Name;
			doesExistRequest.FunctionalityId = data.FunctionalityId;
			doesExistRequest.Developer = data.Developer;
			doesExistRequest.ApplicationId = data.ApplicationId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion

	}
}
