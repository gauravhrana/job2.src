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
	public partial class ReportCategoryDataManager : BaseDataManager
	{
		static string DataStoreKey = "";

		static ReportCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReportCategory");
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReportCategorySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(ReportCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReportCategoryDataModel.DataColumns.ReportCategoryId:
					if (data.ReportCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReportCategoryDataModel.DataColumns.ReportCategoryId, data.ReportCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportCategoryDataModel.DataColumns.ReportCategoryId);
					}
					break;

				case ReportCategoryDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReportCategoryDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportCategoryDataModel.DataColumns.ApplicationId);
					}
					break;

				case ReportCategoryDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReportCategoryDataModel.DataColumns.Application, data.Application.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportCategoryDataModel.DataColumns.Application);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(ReportCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;

		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(ReportCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

        public static List<ReportCategoryDataModel> GetEntityDetails(ReportCategoryDataModel dataQuery, RequestProfile requestProfile, 
            int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReportCategorySearch ";

			var parameters =
			new
			{
				    AuditId              = requestProfile.AuditId
				,	ReportCategoryId     = dataQuery.ReportCategoryId
				,	Name                 = dataQuery.Name
				,   ReturnAuditInfo      = returnAuditInfo
				,   Description          = dataQuery.Description
				,	CreatedDate          = dataQuery.CreatedDate
				,	ModifiedDate         = dataQuery.ModifiedDate
				,	CreatedByAuditId     = dataQuery.CreatedByAuditId
				,   ModifiedByAuditId    = dataQuery.ModifiedByAuditId
				,	ApplicationId        = dataQuery.ApplicationId
			};

			List<ReportCategoryDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<ReportCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}


			return result;
		}

		#endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(ReportCategoryDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReportCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					   ", " + ToSQLParameter(data, ReportCategoryDataModel.DataColumns.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReportCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(data, ReportCategoryDataModel.DataColumns.ApplicationId);

					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, ReportCategoryDataModel.DataColumns.ReportCategoryId) +
						", " + ToSQLParameter(data, ReportCategoryDataModel.DataColumns.Name) +
						", " + ToSQLParameter(data, ReportCategoryDataModel.DataColumns.Description) +
						", " + ToSQLParameter(data, ReportCategoryDataModel.DataColumns.SortOrder);
			return sql;

		}

		#endregion

		#region Create

		public static int Create(ReportCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var Id = DBDML.RunScalarSQL("ReportCategory.Insert", sql, DataStoreKey);
			return Convert.ToInt32(Id);
		}

		#endregion Create

		#region Update

		public static void Update(ReportCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("ReportCategory.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Delete

		public static void Delete(ReportCategoryDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReportCategoryDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ReportCategoryId = dataQuery.ReportCategoryId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(ReportCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReportCategoryDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}


		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(ReportCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReportCategoryChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReportCategoryDataModel.DataColumns.ReportCategoryId);

			var oDT = new DBDataSet("ReportCategory.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ReportCategoryDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;

			var ds = GetChildren(data, requestProfile);

			if (ds != null && ds.Tables.Count > 0)
			{
				if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
				{
					isDeletable = false;
				}
			}

			return isDeletable;
		}

		#endregion

		#region DeleteChildren

		static public DataSet DeleteChildren(ReportCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReportCategoryChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReportCategoryDataModel.DataColumns.ReportCategoryId);

			var oDT = new DBDataSet("ReportCategory.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

	}
}
