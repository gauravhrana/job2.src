using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Framework.Components.DataAccess;
using Framework.Components.ReleaseLog.DomainModel;
using System.Data;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog
{
	public partial class DevelopmentCategoryDataManager : StandardDataManager
	{
		static string DataStoreKey = "";

		static DevelopmentCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DevelopmentCategory");
		}

		public static string ToSQLParameter(DevelopmentCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DevelopmentCategoryDataModel.DataColumns.DevelopmentCategoryId:
					if (data.DevelopmentCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DevelopmentCategoryDataModel.DataColumns.DevelopmentCategoryId, data.DevelopmentCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DevelopmentCategoryDataModel.DataColumns.DevelopmentCategoryId);
					}
					break;

				case DevelopmentCategoryDataModel.DataColumns.DateCreated:
					if (data.DateCreated != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DevelopmentCategoryDataModel.DataColumns.DateCreated, data.DateCreated);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DevelopmentCategoryDataModel.DataColumns.DateCreated);
					}
					break;
				case DevelopmentCategoryDataModel.DataColumns.DateModified:
					if (data.DateModified != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DevelopmentCategoryDataModel.DataColumns.DateModified, data.DateModified);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DevelopmentCategoryDataModel.DataColumns.DateModified);

					}
					break;

				case DevelopmentCategoryDataModel.DataColumns.CreatedByAuditId:
					if (data.CreatedByAuditId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DevelopmentCategoryDataModel.DataColumns.CreatedByAuditId, data.CreatedByAuditId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DevelopmentCategoryDataModel.DataColumns.CreatedByAuditId);
					}
					break;


				case DevelopmentCategoryDataModel.DataColumns.ModifiedByAuditId:
					if (data.ModifiedByAuditId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DevelopmentCategoryDataModel.DataColumns.ModifiedByAuditId, data.ModifiedByAuditId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DevelopmentCategoryDataModel.DataColumns.ModifiedByAuditId);
					}
					break;


				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestedProfile)
		{
			var sql = "EXEC dbo.DevelopmentCategorySearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestedProfile.ApplicationId);
			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region ToList

		static private List<DevelopmentCategoryDataModel> ToList(DataTable dt)
		{
			var list = new List<DevelopmentCategoryDataModel>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var dataItem = new DevelopmentCategoryDataModel();

					dataItem.DevelopmentCategoryId = (int?)dr[DevelopmentCategoryDataModel.DataColumns.DevelopmentCategoryId];
					dataItem.DateCreated = (DateTime)dr[DevelopmentCategoryDataModel.DataColumns.DateCreated];
					dataItem.DateModified = (DateTime)dr[DevelopmentCategoryDataModel.DataColumns.DateModified];
					dataItem.CreatedByAuditId = (int?)dr[DevelopmentCategoryDataModel.DataColumns.CreatedByAuditId];
					dataItem.ModifiedByAuditId = (int?)dr[DevelopmentCategoryDataModel.DataColumns.ModifiedByAuditId];

					SetStandardInfo(dataItem, dr);

					list.Add(dataItem);
				}
			}
			return list;
		}

		#endregion

		#region Search

		public static DataTable Search(DevelopmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetEntitySearch

		static public List<DevelopmentCategoryDataModel> GetEntitySearch(DevelopmentCategoryDataModel obj, RequestProfile requestProfile)
		{
			var dt = Search(obj, requestProfile);

			var list = ToList(dt);

			return list;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(DevelopmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		public static List<DevelopmentCategoryDataModel> GetEntityDetails(DevelopmentCategoryDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.DevelopmentCategorySearch ";

			var parameters =
			new
			{
					AuditId					= requestProfile.AuditId
				,	ApplicationId			= requestProfile.ApplicationId
				,	Name					= dataQuery.Name
				,	Description				= dataQuery.Description
				,	DevelopmentCategoryId	= dataQuery.DevelopmentCategoryId	
				,	ApplicationMode			= requestProfile.ApplicationModeId
			};

			List<DevelopmentCategoryDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<DevelopmentCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(DevelopmentCategoryDataModel data, RequestProfile requestProfile, string action)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.DevelopmentCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.DevelopmentCategoryUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						 ", " + ToSQLParameter(data, DevelopmentCategoryDataModel.DataColumns.DevelopmentCategoryId) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static int Create(DevelopmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			var DevelopmentCategoryId = DBDML.RunScalarSQL("DevelopmentCategory.Insert", sql, DataStoreKey);
			return Convert.ToInt32(DevelopmentCategoryId);
		}

		#endregion Create

		#region Update

		public static void Update(DevelopmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("DevelopmentCategory.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Delete

		public static void Delete(DevelopmentCategoryDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.DevelopmentCategoryDelete ";

			var parameters =	new
								{
										AuditId					= requestProfile.AuditId
									,	DevelopmentCategoryId	= data.DevelopmentCategoryId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(DevelopmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new DevelopmentCategoryDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist
	}
}
