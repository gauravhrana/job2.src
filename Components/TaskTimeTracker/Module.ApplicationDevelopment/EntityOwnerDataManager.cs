using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class EntityOwnerDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static EntityOwnerDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("EntityOwner");
		}

		#region GetList

		public static List<EntityOwnerDataModel> GetList(RequestProfile requestProfile)
		{
            return GetEntityDetails(EntityOwnerDataModel.Empty, requestProfile, 1);
		}

		#endregion

		#region GetDetails

		public static EntityOwnerDataModel GetDetails(EntityOwnerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<EntityOwnerDataModel> GetEntityDetails(EntityOwnerDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.EntityOwnerSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	EntityOwnerId = dataQuery.EntityOwnerId
				,	ApplicationId = requestProfile.ApplicationId
				,	EntityId = dataQuery.EntityId
				,	FeatureOwnerStatusId = dataQuery.FeatureOwnerStatusId
				,	DeveloperRoleId = dataQuery.DeveloperRoleId
				,	Developer = dataQuery.Developer
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<EntityOwnerDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<EntityOwnerDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(EntityOwnerDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("EntityOwner.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(EntityOwnerDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("EntityOwner.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(EntityOwnerDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.EntityOwnerDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				EntityOwnerId = dataQuery.EntityOwnerId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(EntityOwnerDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case EntityOwnerDataModel.DataColumns.EntityOwnerId:
					if (data.EntityOwnerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityOwnerDataModel.DataColumns.EntityOwnerId, data.EntityOwnerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.EntityOwnerId);
					}
					break;

				case EntityOwnerDataModel.DataColumns.EntityId:
					if (data.EntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityOwnerDataModel.DataColumns.EntityId, data.EntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.EntityId);
					}
					break;

				case EntityOwnerDataModel.DataColumns.DeveloperRoleId:
					if (data.DeveloperRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityOwnerDataModel.DataColumns.DeveloperRoleId, data.DeveloperRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.DeveloperRoleId);
					}
					break;

				case EntityOwnerDataModel.DataColumns.FeatureOwnerStatusId:
					if (data.FeatureOwnerStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityOwnerDataModel.DataColumns.FeatureOwnerStatusId, data.FeatureOwnerStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.FeatureOwnerStatusId);
					}
					break;

				case EntityOwnerDataModel.DataColumns.Developer:
					if (!string.IsNullOrEmpty(data.Developer))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityOwnerDataModel.DataColumns.Developer, data.Developer);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.Developer);
					}
					break;

				case EntityOwnerDataModel.DataColumns.Entity:
					if (!string.IsNullOrEmpty(data.Entity))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityOwnerDataModel.DataColumns.Entity, data.Entity);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.Entity);
					}
					break;

				case EntityOwnerDataModel.DataColumns.DeveloperRole:
					if (!string.IsNullOrEmpty(data.DeveloperRole))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityOwnerDataModel.DataColumns.DeveloperRole, data.DeveloperRole);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.DeveloperRole);
					}
					break;

				case EntityOwnerDataModel.DataColumns.FeatureOwnerStatus:
					if (!string.IsNullOrEmpty(data.FeatureOwnerStatus))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityOwnerDataModel.DataColumns.FeatureOwnerStatus, data.FeatureOwnerStatus);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.FeatureOwnerStatus);
					}
					break;

				case EntityOwnerDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityOwnerDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.ApplicationId);
					}
					break;

				case EntityOwnerDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EntityOwnerDataModel.DataColumns.Application, data.Application.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityOwnerDataModel.DataColumns.Application);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(EntityOwnerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(EntityOwnerDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.EntityOwnerInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.EntityOwnerUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					   ", " + ToSQLParameter(data, FunctionalityOwnerDataModel.DataColumns.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, EntityOwnerDataModel.DataColumns.EntityOwnerId) +
					", " + ToSQLParameter(data, EntityOwnerDataModel.DataColumns.EntityId) +
					", " + ToSQLParameter(data, EntityOwnerDataModel.DataColumns.DeveloperRoleId) +
					", " + ToSQLParameter(data, EntityOwnerDataModel.DataColumns.Developer) +
					", " + ToSQLParameter(data, EntityOwnerDataModel.DataColumns.FeatureOwnerStatusId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(EntityOwnerDataModel data, RequestProfile requestProfile)
		{
			//var doesExistRequest = new EntityOwnerDataModel();
			//doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(data, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion

	}
}
