//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using Dapper;
//using Framework.Components.DataAccess;
//using DataModel.Framework.DataAccess;
//using DataModel.TestCaseManagement;

//namespace TestCaseManagement.Components.DataAccess
//{
//	public partial class TestCaseDataManager : StandardDataManager
//	{

//		private static string DataStoreKey = string.Empty;

//		static TestCaseDataManager()
//		{
//			DataStoreKey = SetupConfiguration.GetDataStoreKey("TestCase");
//		}

//		#region ToSQLParameter

//		public static string ToSQLParameter(TestCaseDataModel data, string dataColumnName)
//		{
//			var returnValue = "NULL";

//			switch (dataColumnName)
//			{
//				case TestCaseDataModel.DataColumns.TestCaseId:
//					if (data.TestCaseId != null)
//					{
//						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestCaseDataModel.DataColumns.TestCaseId, data.TestCaseId);
//					}
//					else
//					{
//						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestCaseDataModel.DataColumns.TestCaseId);
//					}
//					break;

//				default:
//					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
//					break;
//			}

//			return returnValue;

//		}

//		#endregion

//		#region Get Entity Details

//		public static List<TestCaseDataModel> GetEntityDetails(TestCaseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
//		{
//			const string sql = @"dbo.TestCaseSearch ";

//			var parameters =
//			new
//			{
//					AuditId                 = requestProfile.AuditId
//				 ,	ApplicationId           = requestProfile.ApplicationId
//				 ,	ApplicationMode         = requestProfile.ApplicationModeId
//				 ,	ReturnAuditInfo         = returnAuditInfo
//				 ,	TestCaseId                  = dataQuery.TestCaseId
//				 ,	Name                    = dataQuery.Name
//			};

//			List<TestCaseDataModel> result;
//			using (var dataAccess = new DataAccessBase(DataStoreKey))
//			{
//				result = dataAccess.Connection.Query<TestCaseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
//			}

//			return result;

//		}

//		#endregion

//		#region Get List

//		public static DataTable GetList(RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(TestCaseDataModel.Empty, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region Search

//		public static DataTable Search(TestCaseDataModel data, RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(data, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region GetDetails

//		public static DataTable GetDetails(TestCaseDataModel data, RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(data, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region Save

//		public static string Save(TestCaseDataModel data, string action, RequestProfile requestProfile)
//		{

//			var sql = "EXEC ";

//			switch (action)
//			{
//				case "Create":
//					sql += "dbo.TestCaseInsert  " +
//						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
//					break;

//				case "Update":
//					sql += "dbo.TestCaseUpdate  " +
//						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
//					break;

//				default:
//					break;
//			}
//			sql = sql + ", " + ToSQLParameter(data, TestCaseDataModel.DataColumns.TestCaseId) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

//			return sql;
//		}

//		#endregion

//		#region Create

//		public static int Create(TestCaseDataModel data, RequestProfile requestProfile)
//		{
//			var sql = Save(data, "Create", requestProfile);
//			var newId = DBDML.RunScalarSQL("TestCase.Insert", sql, DataStoreKey);
//			return Convert.ToInt32(newId);
//		}

//		#endregion

//		#region Update

//		public static void Update(TestCaseDataModel data, RequestProfile requestProfile)
//		{
//			var sql = Save(data, "Update", requestProfile);
//			DBDML.RunSQL("TestCase.Update", sql, DataStoreKey);
//		}

//		#endregion

//		#region Delete

//		public static void Delete(TestCaseDataModel data, RequestProfile requestProfile)
//		{

//			const string sql = @"dbo.TestCaseDelete ";

//			var parameters =
//			new
//			{
//					AuditId = requestProfile.AuditId
//				,   TestCaseId  = data.TestCaseId
//			};

//			using (var dataAccess = new DataAccessBase(DataStoreKey))
//			{
//				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
//			}
//		}

//		#endregion

//		#region Does Exist

//		public static DataTable DoesExist(TestCaseDataModel data, RequestProfile requestProfile)
//		{
//			var doesExistRequest = new TestCaseDataModel();
//			doesExistRequest.Name = data.Name;
//			return Search(doesExistRequest, requestProfile);
//		}

//		#endregion

//		#region Renumber

//		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
//		{
//			var sql = "EXEC dbo.TestCaseRenumber" +
//				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//				", @Seed = " + seed +
//				", @Increment = " + increment;
//			Framework.Components.DataAccess.DBDML.RunSQL("TestCase.Renumber", sql, DataStoreKey);
//		}

//		#endregion

//		#region Get Children

//		public static DataSet GetChildren(TestCaseDataModel data, RequestProfile requestProfile)
//		{
//			var sql = "EXEC dbo.TestCaseChildrenGet" +
//				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//				", " + ToSQLParameter(data, TestCaseDataModel.DataColumns.TestCaseId);
//			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
//			return oDT.DBDataset;
//		}

//		#endregion

//		#region IsDeletable

//		public static bool IsDeletable(TestCaseDataModel data, RequestProfile requestProfile)
//		{
//			var isDeletable = true;
//			var ds = GetChildren(data, requestProfile);
//			if (ds != null && ds.Tables.Count > 0)
//			{
//				foreach (DataTable dt in ds.Tables)
//				{
//					if (dt.Rows.Count > 0)
//					{
//						isDeletable = false;
//						break;
//					}
//				}
//			}
//			return isDeletable;
//		}

//		#endregion

//	}
//}
