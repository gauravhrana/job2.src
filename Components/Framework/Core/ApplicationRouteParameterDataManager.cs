using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;
using DataModel.Framework.Core;

namespace Framework.Components.Core
{
	public partial class ApplicationRouteParameterDataManager : DataAccess.BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static ApplicationRouteParameterDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationRouteParameter");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteParameterId:
					if (data.ApplicationRouteParameterId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteParameterId, data.ApplicationRouteParameterId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteParameterId);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId:
					if (data.ApplicationRouteId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId, data.ApplicationRouteId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRoute:
					if (!string.IsNullOrEmpty(data.ApplicationRoute))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRoute, data.ApplicationRoute.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRoute);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ParameterName:
					if (!string.IsNullOrEmpty(data.ParameterName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ParameterName, data.ParameterName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ParameterName);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ParameterValue:
					if (!string.IsNullOrEmpty(data.ParameterValue))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ParameterValue, data.ParameterValue.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ParameterValue);

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

        public static List<ApplicationRouteParameterDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ApplicationRouteParameterDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static ApplicationRouteParameterDataModel GetDetails(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GeEntitySearch

		public static List<DataModel.Framework.Core.ApplicationRouteParameterDataModel> GetEntityDetails(DataModel.Framework.Core.ApplicationRouteParameterDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationRouteParameterSearch ";

			var parameters =
			new
			{
					AuditId                     = requestProfile.AuditId
				,	ApplicationRouteParameterId = dataQuery.ApplicationRouteParameterId
				,	ApplicationId               = requestProfile.ApplicationId
				,	ApplicationMode             = requestProfile.ApplicationModeId
				,	ApplicationrouteId          = dataQuery.ApplicationRouteId
				,	ReturnAuditInfo             = returnAuditInfo
			};

			List<ApplicationRouteParameterDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<ApplicationRouteParameterDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			Framework.Components.DataAccess.DBDML.RunSQL("ApplicationRouteParameter.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			Framework.Components.DataAccess.DBDML.RunSQL("ApplicationRouteParameter.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DataModel.Framework.Core.ApplicationRouteParameterDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ApplicationRouteParameterDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ApplicationRouteParameterId = dataQuery.ApplicationRouteParameterId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ApplicationRouteParameterInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

					break;

				case "Update":
					sql += "dbo.ApplicationRouteParameterUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteParameterId) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ParameterName) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ParameterValue);


			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ApplicationRouteParameterDataModel();
			doesExistRequest.ParameterName = data.ParameterName;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationRouteParameterChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteParameterId);

			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(DataModel.Framework.Core.ApplicationRouteParameterDataModel data, RequestProfile requestProfile)
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
