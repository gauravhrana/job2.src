using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Core;

namespace Framework.Components.Core
{
	public partial class LanguageDataManager : StandardDataManager
	{

		private static string DataStoreKey = string.Empty;

		static LanguageDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Language");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(LanguageDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case LanguageDataModel.DataColumns.LanguageId:
					if (data.LanguageId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, LanguageDataModel.DataColumns.LanguageId, data.LanguageId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, LanguageDataModel.DataColumns.LanguageId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<LanguageDataModel> GetEntityDetails(LanguageDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.LanguageSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	LanguageId                  = dataQuery.LanguageId
				 ,	Name                    = dataQuery.Name
			};

			List<LanguageDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<LanguageDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var list = GetEntityDetails(LanguageDataModel.Empty, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Search

		public static DataTable Search(LanguageDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(LanguageDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static string Save(LanguageDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.LanguageInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.LanguageUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, LanguageDataModel.DataColumns.LanguageId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(LanguageDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Language.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(LanguageDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Language.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(LanguageDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.LanguageDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   LanguageId  = data.LanguageId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static DataTable DoesExist(LanguageDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new LanguageDataModel();
			doesExistRequest.Name = data.Name;
			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.LanguageRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("Language.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(LanguageDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.LanguageChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, LanguageDataModel.DataColumns.LanguageId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(LanguageDataModel data, RequestProfile requestProfile)
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
