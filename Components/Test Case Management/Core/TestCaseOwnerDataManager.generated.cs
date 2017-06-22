using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TestCaseManagement;

namespace TestCaseManagement.Components.DataAccess
{
	public partial class TestCaseOwnerDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TestCaseOwnerDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TestCaseOwner");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TestCaseOwnerDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TestCaseOwnerDataModel.DataColumns.TestCaseOwnerId:
					if (data.TestCaseOwnerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestCaseOwnerDataModel.DataColumns.TestCaseOwnerId, data.TestCaseOwnerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestCaseOwnerDataModel.DataColumns.TestCaseOwnerId);
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

		public static List<TestCaseOwnerDataModel> GetEntityDetails(TestCaseOwnerDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TestCaseOwnerSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TestCaseOwnerId                  = dataQuery.TestCaseOwnerId
				 ,	Name                    = dataQuery.Name
			};

			List<TestCaseOwnerDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TestCaseOwnerDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TestCaseOwnerDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TestCaseOwnerDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TestCaseOwnerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static TestCaseOwnerDataModel GetDetails(TestCaseOwnerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(TestCaseOwnerDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TestCaseOwnerInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TestCaseOwnerUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TestCaseOwnerDataModel.DataColumns.TestCaseOwnerId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TestCaseOwnerDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TestCaseOwner.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TestCaseOwnerDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TestCaseOwner.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TestCaseOwnerDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TestCaseOwnerDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TestCaseOwnerId  = data.TestCaseOwnerId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TestCaseOwnerDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TestCaseOwnerDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TestCaseOwnerRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("TestCaseOwner.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(TestCaseOwnerDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TestCaseOwnerChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, TestCaseOwnerDataModel.DataColumns.TestCaseOwnerId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(TestCaseOwnerDataModel data, RequestProfile requestProfile)
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
