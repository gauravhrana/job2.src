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
	public partial class ApplicationRouteDataManager : DataAccess.BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ApplicationRouteDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationRoute");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(DataModel.Framework.Core.ApplicationRouteDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ApplicationRouteId:
					if (data.ApplicationRouteId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ApplicationRouteId, data.ApplicationRouteId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ApplicationRouteId);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.RelativeRoute:
					if (!string.IsNullOrEmpty(data.RelativeRoute))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.RelativeRoute, data.RelativeRoute.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.RelativeRoute);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.RouteName:
					if (!string.IsNullOrEmpty(data.RouteName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.RouteName, data.RouteName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.RouteName);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.EntityName:
					if (!string.IsNullOrEmpty(data.EntityName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.EntityName, data.EntityName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.EntityName);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ProposedRoute:
					if (!string.IsNullOrEmpty(data.ProposedRoute))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ProposedRoute, data.ProposedRoute.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ProposedRoute);

					}
					break;

				case DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.Description, data.Description.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.Description);

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
			var sql = "EXEC dbo.ApplicationRouteSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationRoute.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(DataModel.Framework.Core.ApplicationRouteDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<ApplicationRouteDataModel> GetEntityDetails(ApplicationRouteDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationRouteSearch ";

			var parameters =
			new
			{
					AuditId				= requestProfile.AuditId
				,	ApplicationRouteId	= dataQuery.ApplicationRouteId
				,	EntityName			= dataQuery.EntityName
				,	RouteName			= dataQuery.RouteName
				,	ApplicationMode		= requestProfile.ApplicationModeId
				,	ReturnAuditInfo		= returnAuditInfo
			};

			List<ApplicationRouteDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationRouteDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static int Create(DataModel.Framework.Core.ApplicationRouteDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var obj = Framework.Components.DataAccess.DBDML.RunScalarSQL("ApplicationRoute.Insert", sql, DataStoreKey);
			return Convert.ToInt32(obj);
		}

		#endregion

		#region Update

		public static void Update(ApplicationRouteDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			Framework.Components.DataAccess.DBDML.RunSQL("ApplicationRoute.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DataModel.Framework.Core.ApplicationRouteDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ApplicationRouteDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ApplicationRouteId = dataQuery.ApplicationRouteId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(ApplicationRouteDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(DataModel.Framework.Core.ApplicationRouteDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ApplicationRouteInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

					break;

				case "Update":
					sql += "dbo.ApplicationRouteUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ApplicationRouteId) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.RouteName) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.EntityName) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ProposedRoute) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.RelativeRoute) +
						", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.Description);

			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(DataModel.Framework.Core.ApplicationRouteDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ApplicationRouteDataModel();
			doesExistRequest.RouteName = data.RouteName;
			doesExistRequest.ApplicationId = requestProfile.ApplicationId;
			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(DataModel.Framework.Core.ApplicationRouteDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationRouteChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, DataModel.Framework.Core.ApplicationRouteDataModel.DataColumns.ApplicationRouteId);

			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(DataModel.Framework.Core.ApplicationRouteDataModel data, RequestProfile requestProfile)
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

		public static void AddSearchApplicationRoutes(RequestProfile requestProfile)
		{
			var concatEname = string.Empty;
			var Entityname = string.Empty;
			var ardata = new ApplicationRouteDataModel();
			ardata.ApplicationId = requestProfile.ApplicationId;
			var dt2 = Search(ardata, requestProfile);

			dt2.DefaultView.RowFilter = "RouteName LIKE '%EntityRouteSuperKey'";
			var dv = dt2.DefaultView;
			dv.Sort = "RouteName ASC";

			var searchAR = new ApplicationRouteDataModel();
			for (int j = 0; j < dv.Table.Rows.Count; j++)
			{
				Entityname = dv.Table.Rows[j]["EntityName"].ToString();
				concatEname = Entityname + "EntityRouteSearch";
				searchAR.EntityName=Entityname.ToString();
				searchAR.RouteName = concatEname;
				searchAR.ApplicationId = requestProfile.ApplicationId;
				searchAR.Description = concatEname;
				
				// u need to check with original route and extract path from there				
				searchAR.ProposedRoute = Entityname.ToString() + "/Search";
				var rRoute = dv.Table.Rows[j]["RelativeRoute"];
				searchAR.RelativeRoute = rRoute.ToString().Replace("{action}", "Default");

				// check DoesExist procedure and method both
				// ensure proper criteria is being used  in both
				var dtExists = DoesExist(searchAR, requestProfile);
				if (dtExists.Rows.Count == 0)
				{
					Create(searchAR, requestProfile);
				}

			}
			
			
		}

	}

}
