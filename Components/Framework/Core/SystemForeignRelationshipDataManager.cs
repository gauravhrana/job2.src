using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using System.Data.SqlClient;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class SystemForeignRelationshipDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";
		
		static SystemForeignRelationshipDataManager()
        {            
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SystemForeignRelationship");
		}

		#region GetList

        public static List<SystemForeignRelationshipDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(SystemForeignRelationshipDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static SystemForeignRelationshipDataModel GetDetails(SystemForeignRelationshipDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<SystemForeignRelationshipDataModel> GetEntityDetails(SystemForeignRelationshipDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SystemForeignRelationshipSearch ";

			var parameters =
			new
			{
					AuditId							= requestProfile.AuditId
				,	SystemForeignRelationshipId		= dataQuery.SystemForeignRelationshipId
				,	ForeignDatabaseId				= dataQuery.ForeignDatabaseId
				,	PrimaryDatabaseId				= dataQuery.PrimaryDatabaseId			
								
			};

			List<SystemForeignRelationshipDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				result = dataAccess.Connection.Query<SystemForeignRelationshipDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

				
			}


			return result;
		}

		#endregion

		#region Create

		public static void Create(SystemForeignRelationshipDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("SystemForeignRelationship.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(SystemForeignRelationshipDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("SystemForeignRelationship.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SystemForeignRelationshipDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SystemForeignRelationshipDelete ";

			var parameters =
			new
			{
					AuditId							= requestProfile.AuditId
				,	SystemForeignRelationshipId		= dataQuery.SystemForeignRelationshipId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(SystemForeignRelationshipDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipId:
					if (data.SystemForeignRelationshipId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipId, data.SystemForeignRelationshipId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipId);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabaseId:
					if (data.PrimaryDatabaseId!= null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabaseId, data.PrimaryDatabaseId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabaseId);

					}
					break;


				case SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabase:
					if (!string.IsNullOrEmpty(data.PrimaryDatabase))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabase, data.PrimaryDatabase.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabase);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.PrimaryEntityId:
					if (data.PrimaryEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemForeignRelationshipDataModel.DataColumns.PrimaryEntityId, data.PrimaryEntityId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.PrimaryEntityId);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.ForeignDatabase:
					if (!string.IsNullOrEmpty(data.ForeignDatabase))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemForeignRelationshipDataModel.DataColumns.ForeignDatabase, data.ForeignDatabase.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.ForeignDatabase);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.ForeignDatabaseId:
					if (data.ForeignDatabaseId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemForeignRelationshipDataModel.DataColumns.ForeignDatabaseId, data.ForeignDatabaseId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.ForeignDatabaseId);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.ForeignEntityId:
					if (data.ForeignEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemForeignRelationshipDataModel.DataColumns.ForeignEntityId, data.ForeignEntityId);

					}
					else
					{
						returnValue = returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.ForeignEntityId);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipTypeId:
					if (data.SystemForeignRelationshipTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipTypeId, data.SystemForeignRelationshipTypeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipTypeId);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipType:
					if (!string.IsNullOrEmpty(data.SystemForeignRelationshipType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipType, data.SystemForeignRelationshipType);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipType);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.FieldName:
					if (!string.IsNullOrEmpty(data.FieldName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemForeignRelationshipDataModel.DataColumns.FieldName, data.FieldName);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.FieldName);

					}
					break;

				case SystemForeignRelationshipDataModel.DataColumns.Source:
					if (!string.IsNullOrEmpty(data.Source))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemForeignRelationshipDataModel.DataColumns.Source, data.Source);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDataModel.DataColumns.Source);

					}
					break;
				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			return returnValue;
		}

		public static DataTable Search(SystemForeignRelationshipDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(SystemForeignRelationshipDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SystemForeignRelationshipInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SystemForeignRelationshipUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipId) +
						", " + ToSQLParameter(data, SystemForeignRelationshipDataModel.DataColumns.ForeignEntityId) +
						", " + ToSQLParameter(data, SystemForeignRelationshipDataModel.DataColumns.ForeignDatabaseId) +
						", " + ToSQLParameter(data, SystemForeignRelationshipDataModel.DataColumns.PrimaryEntityId) +
						", " + ToSQLParameter(data, SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabaseId) +
						", " + ToSQLParameter(data, SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipTypeId) +
						", " + ToSQLParameter(data, SystemForeignRelationshipDataModel.DataColumns.FieldName) +
						", " + ToSQLParameter(data, SystemForeignRelationshipDataModel.DataColumns.Source);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(SystemForeignRelationshipDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SystemForeignRelationshipDataModel();
			doesExistRequest.PrimaryEntityId = data.PrimaryEntityId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion
	}
}
