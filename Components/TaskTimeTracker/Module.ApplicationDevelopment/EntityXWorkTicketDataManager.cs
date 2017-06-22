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
	public partial class EntityXWorkTicketDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

        static EntityXWorkTicketDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("EntityXWorkTicket");
		}

		#region GetList

        public static List<EntityXWorkTicketDataModel> GetList(RequestProfile requestProfile)
		{
            return GetEntityDetails(EntityXWorkTicketDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static EntityXWorkTicketDataModel GetDetails(EntityXWorkTicketDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<EntityXWorkTicketDataModel> GetEntityDetails(EntityXWorkTicketDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.EntityXWorkTicketSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ApplicationId = requestProfile.ApplicationId
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				EntityXWorkTicketId = dataQuery.EntityXWorkTicketId
				,
				EntityId = dataQuery.EntityId
				,
				WorkTicketId = dataQuery.WorkTicketId
				,
				ReturnAuditInfo = returnAuditInfo

			};

			List<EntityXWorkTicketDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<EntityXWorkTicketDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(EntityXWorkTicketDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("EntityXWorkTicket.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(EntityXWorkTicketDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("EntityXWorkTicket.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(EntityXWorkTicketDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.EntityXWorkTicketDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				EntityXWorkTicketId = dataQuery.EntityXWorkTicketId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(EntityXWorkTicketDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case EntityXWorkTicketDataModel.DataColumns.EntityXWorkTicketId:
					if (data.EntityXWorkTicketId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityXWorkTicketDataModel.DataColumns.EntityXWorkTicketId, data.EntityXWorkTicketId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityXWorkTicketDataModel.DataColumns.EntityXWorkTicketId);
					}
					break;

				case EntityXWorkTicketDataModel.DataColumns.EntityId:
					if (data.EntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityXWorkTicketDataModel.DataColumns.EntityId, data.EntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityXWorkTicketDataModel.DataColumns.EntityId);
					}
					break;

				case EntityXWorkTicketDataModel.DataColumns.WorkTicketId:
					if (data.WorkTicketId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityXWorkTicketDataModel.DataColumns.WorkTicketId, data.WorkTicketId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityXWorkTicketDataModel.DataColumns.WorkTicketId);
					}
					break;

				case EntityXWorkTicketDataModel.DataColumns.AcknowledgedBy:
					if (data.AcknowledgedBy != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityXWorkTicketDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityXWorkTicketDataModel.DataColumns.AcknowledgedBy);
					}
					break;

				case EntityXWorkTicketDataModel.DataColumns.KnowledgeDate:
					if (data.KnowledgeDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityXWorkTicketDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityXWorkTicketDataModel.DataColumns.KnowledgeDate);
					}
					break;

				case EntityXWorkTicketDataModel.DataColumns.Memo:
					if (data.Memo != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityXWorkTicketDataModel.DataColumns.Memo, data.Memo);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityXWorkTicketDataModel.DataColumns.Memo);
					}
					break;

				case EntityXWorkTicketDataModel.DataColumns.Entity:
					if (data.Entity != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityXWorkTicketDataModel.DataColumns.Entity, data.Entity);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityXWorkTicketDataModel.DataColumns.Entity);
					}
					break;

				case EntityXWorkTicketDataModel.DataColumns.WorkTicket:
					if (data.WorkTicket != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityXWorkTicketDataModel.DataColumns.WorkTicket, data.WorkTicket);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityXWorkTicketDataModel.DataColumns.WorkTicket);
					}
					break;



				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(EntityXWorkTicketDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(EntityXWorkTicketDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.EntityXWorkTicketDataModelInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

					break;

				case "Update":
					sql += "dbo.EntityXWorkTicketDataModelUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(EntityXWorkTicketDataModel.DataColumns.EntityXWorkTicketId, data.EntityXWorkTicketId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
					", " + ToSQLParameter(EntityXWorkTicketDataModel.DataColumns.EntityId, data.EntityId) +
				", " + ToSQLParameter(EntityXWorkTicketDataModel.DataColumns.WorkTicketId, data.WorkTicketId) +
				", " + ToSQLParameter(EntityXWorkTicketDataModel.DataColumns.Memo, data.Memo) +
				", " + ToSQLParameter(EntityXWorkTicketDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate) +
				", " + ToSQLParameter(EntityXWorkTicketDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);
			return sql;
		}

		#endregion

		#region CreateByEntity

		public static void CreateByEntity(int EntityId, int[] WorkTicketIds, RequestProfile requestProfile)
		{
			foreach (int WorkTicketId in WorkTicketIds)
			{
				var sql = "EXEC EntityXWorkTicketInsert " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							",   @EntityId					=   " + EntityId +
							",   @WorkTicketId	    =   " + WorkTicketId;

				DBDML.RunSQL("EntityXWorkTicketInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Delete By Entity

		public static void DeleteByEntity(int EntityId, RequestProfile requestProfile)
		{
			var sql = "EXEC EntityXWorkTicketDelete @EntityId		=" + EntityId + ", " +
						  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
			DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

		#region Get By WorkTicket

		public static DataTable GetByWorkTicket(int WorkTicketId, RequestProfile requestProfile)
		{
			var sql = "EXEC EntityXWorkTicketSearch @WorkTicketId     =" + WorkTicketId + ", " +
						  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion

		#region Get By Entity

		public static DataTable GetByEntity(int EntityId, RequestProfile requestProfile)
		{
			var sql = "EXEC EntityXWorkTicketSearch @EntityId       =" + EntityId + ", " +
						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion
	}
}
