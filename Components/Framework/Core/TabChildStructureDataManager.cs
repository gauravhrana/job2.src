using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class TabChildStructureDatManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static TabChildStructureDatManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TabChildStructure");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(TabChildStructureDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case TabChildStructureDataModel.DataColumns.TabChildStructureId:
					if (data.TabChildStructureId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TabChildStructureDataModel.DataColumns.TabChildStructureId, data.TabChildStructureId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TabChildStructureDataModel.DataColumns.TabChildStructureId);

					}
					break;

				case TabChildStructureDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TabChildStructureDataModel.DataColumns.Name, data.Name.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TabChildStructureDataModel.DataColumns.Name);

					}
					break;

				case TabChildStructureDataModel.DataColumns.InnerControlPath:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TabChildStructureDataModel.DataColumns.InnerControlPath, data.InnerControlPath.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TabChildStructureDataModel.DataColumns.InnerControlPath);

					}
					break;

				case TabChildStructureDataModel.DataColumns.EntityName:
					if (!string.IsNullOrEmpty(data.EntityName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TabChildStructureDataModel.DataColumns.EntityName, data.EntityName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TabChildStructureDataModel.DataColumns.EntityName);

					}
					break;

				case TabChildStructureDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TabChildStructureDataModel.DataColumns.SortOrder, data.SortOrder);

					}
					else
					{
						returnValue = returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TabChildStructureDataModel.DataColumns.SortOrder);

					}
					break;

				case TabChildStructureDataModel.DataColumns.TabParentStructureId:
					if (data.TabParentStructureId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TabChildStructureDataModel.DataColumns.TabParentStructureId, data.TabParentStructureId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TabChildStructureDataModel.DataColumns.TabParentStructureId);

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

        public static List<TabChildStructureDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TabChildStructureDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static TabChildStructureDataModel GetDetails(TabChildStructureDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<TabChildStructureDataModel> GetEntityDetails(TabChildStructureDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TabChildStructureSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				TabChildStructureId = dataQuery.TabChildStructureId
				,
				TabParentStructureId = dataQuery.TabParentStructureId
				,
				EntityName = dataQuery.EntityName
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				Name = dataQuery.Name
				,
				ApplicationId = dataQuery.ApplicationId
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<TabChildStructureDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<TabChildStructureDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}


		#endregion

		#region Create

		public static void Create(TabChildStructureDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("TabChildStructure.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(TabChildStructureDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("TabChildStructure.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TabChildStructureDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TabChildStructureDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				TabChildStructureId = dataQuery.TabChildStructureId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(TabChildStructureDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(TabChildStructureDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TabChildStructureInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TabChildStructureUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, TabChildStructureDataModel.DataColumns.TabChildStructureId) +
						", " + ToSQLParameter(data, TabChildStructureDataModel.DataColumns.Name) +
						", " + ToSQLParameter(data, TabChildStructureDataModel.DataColumns.TabParentStructureId) +
						", " + ToSQLParameter(data, TabChildStructureDataModel.DataColumns.EntityName) +
						", " + ToSQLParameter(data, TabChildStructureDataModel.DataColumns.InnerControlPath) +
						", " + ToSQLParameter(data, TabChildStructureDataModel.DataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(TabChildStructureDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TabChildStructureDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

	}
}
