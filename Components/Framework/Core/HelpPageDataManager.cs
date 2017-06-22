using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using System.Data.SqlClient;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Core;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class HelpPageDataManager : DataAccess.BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static HelpPageDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("HelpPage");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(DataModel.Framework.Core.HelpPageDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageId:
					if (data.HelpPageId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageId, data.HelpPageId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageId);

					}
					break;

				case DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageContextId:
					if (data.HelpPageContextId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageContextId, data.HelpPageContextId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageContextId);

					}
					break;

				case DataModel.Framework.Core.HelpPageDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.HelpPageDataModel.DataColumns.Name, data.Name.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.HelpPageDataModel.DataColumns.Name);

					}
					break;

				case DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageContext:
					if (!string.IsNullOrEmpty(data.HelpPageContext))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageContext, data.HelpPageContext.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageContext);

					}
					break;

				case DataModel.Framework.Core.HelpPageDataModel.DataColumns.SystemEntityType:
					if (!string.IsNullOrEmpty(data.SystemEntityType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.HelpPageDataModel.DataColumns.SystemEntityType, data.SystemEntityType.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.HelpPageDataModel.DataColumns.SystemEntityType);

					}
					break;

				case DataModel.Framework.Core.HelpPageDataModel.DataColumns.Content:
					if (!string.IsNullOrEmpty(data.Content))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.HelpPageDataModel.DataColumns.Content, data.Content.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.HelpPageDataModel.DataColumns.Content);

					}
					break;

				case DataModel.Framework.Core.HelpPageDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.HelpPageDataModel.DataColumns.SortOrder, data.SortOrder);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.HelpPageDataModel.DataColumns.SortOrder);

					}
					break;

				case DataModel.Framework.Core.HelpPageDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.HelpPageDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.HelpPageDataModel.DataColumns.SystemEntityTypeId);

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

        public static List<HelpPageDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(HelpPageDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static HelpPageDataModel GetDetails(DataModel.Framework.Core.HelpPageDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<DataModel.Framework.Core.HelpPageDataModel> GetEntityDetails(DataModel.Framework.Core.HelpPageDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.HelpPageSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				HelpPageId = dataQuery.HelpPageId
				,
				ApplicationId = requestProfile.ApplicationId
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				Name = dataQuery.Name
				,
				SystemEntityTypeId = dataQuery.SystemEntityTypeId
				,
				HelpPageContextId = dataQuery.HelpPageContextId
				,
				ReturnAuditInfo = returnAuditInfo
			};

			List<HelpPageDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<HelpPageDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion

		#region Create

		public static int Create(DataModel.Framework.Core.HelpPageDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var HelpPageId = DataAccess.DBDML.RunScalarSQL("HelpPage.Insert", sql, DataStoreKey);
			return Convert.ToInt32(HelpPageId);
		}

		#endregion

		#region Update

		public static void Update(DataModel.Framework.Core.HelpPageDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DataAccess.DBDML.RunSQL("HelpPage.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DataModel.Framework.Core.HelpPageDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.HelpPageDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				HelpPageId = dataQuery.HelpPageId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(DataModel.Framework.Core.HelpPageDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(DataModel.Framework.Core.HelpPageDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.HelpPageInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.HelpPageUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageId) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.HelpPageDataModel.DataColumns.Name) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.HelpPageDataModel.DataColumns.Content) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.HelpPageDataModel.DataColumns.SortOrder) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageContextId) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.HelpPageDataModel.DataColumns.SystemEntityTypeId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(DataModel.Framework.Core.HelpPageDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new HelpPageDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(DataModel.Framework.Core.HelpPageDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.HelpPageChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, DataModel.Framework.Core.HelpPageDataModel.DataColumns.HelpPageId);

			var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(DataModel.Framework.Core.HelpPageDataModel data, RequestProfile requestProfile)
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

	}
}
