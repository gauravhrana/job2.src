using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Core;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class ApplicationRelationDataManager : DataAccess.BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static ApplicationRelationDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationRelation");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(DataModel.Framework.Core.ApplicationRelationDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.ApplicationRelationId:
					if (data.ApplicationRelationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.ApplicationRelationId, data.ApplicationRelationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.ApplicationRelationId);
					}
					break;

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.PublisherApplicationId:
					if (data.PublisherApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.PublisherApplicationId, data.PublisherApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.PublisherApplicationId);
					}
					break;

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationId:
					if (data.SubscriberApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationId, data.SubscriberApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationId);
					}
					break;

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SystemEntityTypeId);
					}
					break;

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId:
					if (data.SubscriberApplicationRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId, data.SubscriberApplicationRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId);
					}
					break;

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.PublisherApplication:
					if (!string.IsNullOrEmpty(data.PublisherApplication))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.PublisherApplication, data.PublisherApplication.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.PublisherApplication);
					}
					break;

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplication:
					if (!string.IsNullOrEmpty(data.SubscriberApplication))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplication, data.SubscriberApplication.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplication);
					}
					break;

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SystemEntityType:
					if (!string.IsNullOrEmpty(data.SystemEntityType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SystemEntityType, data.SystemEntityType.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SystemEntityType);
					}
					break;

				case DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationRole:
					if (!string.IsNullOrEmpty(data.SubscriberApplicationRole))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationRole, data.SubscriberApplicationRole.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationRole);
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
			var sql = "EXEC dbo.ApplicationRelationSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationRelation.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(DataModel.Framework.Core.ApplicationRelationDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<DataModel.Framework.Core.ApplicationRelationDataModel> GetEntityDetails(DataModel.Framework.Core.ApplicationRelationDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationRelationSearch ";

			var parameters =
			new
			{
				    AuditId                      = requestProfile.AuditId
				,   ApplicationRelationId        = dataQuery.ApplicationRelationId				
				,	SystemEntityTypeId           = dataQuery.SystemEntityTypeId
				,	SubscriberApplicationRoleId  = dataQuery.SubscriberApplicationRoleId
				,	ApplicationMode              = requestProfile.ApplicationModeId
				,	ReturnAuditInfo              = returnAuditInfo
			};

			List<ApplicationRelationDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationRelationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static int Create(DataModel.Framework.Core.ApplicationRelationDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var obj = Framework.Components.DataAccess.DBDML.RunScalarSQL("ApplicationRelation.Insert", sql, DataStoreKey);
			return Convert.ToInt32(obj);
		}

		#endregion

		#region Update

		public static void Update(DataModel.Framework.Core.ApplicationRelationDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			Framework.Components.DataAccess.DBDML.RunSQL("ApplicationRelation.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DataModel.Framework.Core.ApplicationRelationDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ApplicationRelationDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ApplicationRelationId = dataQuery.ApplicationRelationId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(DataModel.Framework.Core.ApplicationRelationDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(DataModel.Framework.Core.ApplicationRelationDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC dbo.ApplicationRelationInsert  " +
					" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.ApplicationRelationId) +
					", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.PublisherApplicationId) +
					", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationId) +
					", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SystemEntityTypeId) +
					", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId);

			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(DataModel.Framework.Core.ApplicationRelationDataModel data, RequestProfile requestProfile)
		{
			return Search(data, requestProfile);
		}

		#endregion

	}
}
