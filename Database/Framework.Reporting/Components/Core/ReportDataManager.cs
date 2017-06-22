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
	public partial class ReportDataManager : BaseDataManager
	{
		static string DataStoreKey = "";

		static ReportDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Report");
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReportSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(ReportDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReportDataModel.DataColumns.ReportId:
					if (data.ReportId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReportDataModel.DataColumns.ReportId, data.ReportId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportDataModel.DataColumns.ReportId);
					}
					break;

				case ReportDataModel.DataColumns.Title:
					if (!string.IsNullOrEmpty(data.Title))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReportDataModel.DataColumns.Title, data.Title);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportDataModel.DataColumns.Title);
					}

					break;

				case BaseDataModel.BaseDataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.ApplicationId);
					}
					break;

				case ReportDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReportDataModel.DataColumns.Application, data.Application.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportDataModel.DataColumns.Application);
					}
					break;



				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(ReportDataModel data, RequestProfile requestProfile)
		{

			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;

		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(ReportDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<ReportDataModel> GetEntityDetails(ReportDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReportSearch ";

			var parameters =
			new
			{
				    AuditId = requestProfile.AuditId
				,   ReportId = dataQuery.ReportId
                ,   ReturnAuditInfo      = returnAuditInfo
				,   Name = dataQuery.Name
				,   Title = dataQuery.Title
				,   Description = dataQuery.Description
				,   CreatedDate = dataQuery.CreatedDate
				,   ModifiedDate = dataQuery.ModifiedDate
				,   CreatedByAuditId = dataQuery.CreatedByAuditId
				,   ModifiedByAuditId = dataQuery.ModifiedByAuditId
				,   ApplicationId = dataQuery.ApplicationId

			};

			List<ReportDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<ReportDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(ReportDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReportInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.ApplicationId);

					break;

				case "Update":
					sql += "dbo.ReportUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.ApplicationId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, ReportDataModel.DataColumns.ReportId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, ReportDataModel.DataColumns.Title) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;

		}

		#endregion

		#region Create

		public static int Create(ReportDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var Id = DBDML.RunScalarSQL("Report.Insert", sql, DataStoreKey);
			return Convert.ToInt32(Id);
		}

		#endregion Create

		#region Update
		public static void Update(ReportDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("Report.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Delete

		public static void Delete(ReportDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReportDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ReportId = dataQuery.ReportId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(ReportDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReportDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}


		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(ReportDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReportChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReportDataModel.DataColumns.ReportId);

			var oDT = new DBDataSet("Report.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ReportDataModel data, RequestProfile requestProfile)
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

		static public DataSet DeleteChildren(ReportDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ReportChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ReportDataModel.DataColumns.ReportId);

			var oDT = new DBDataSet("Report.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

	}
}
