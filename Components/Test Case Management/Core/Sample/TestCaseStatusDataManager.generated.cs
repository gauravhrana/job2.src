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
//	public partial class TestCaseStatusDataManager : StandardDataManager
//	{

//		private static string DataStoreKey = string.Empty;

//		static TestCaseStatusDataManager()
//		{
//			DataStoreKey = SetupConfiguration.GetDataStoreKey("TestCaseStatus");
//		}

//		#region ToSQLParameter

//		public static string ToSQLParameter(TestCaseStatusDataModel data, string dataColumnName)
//		{
//			var returnValue = "NULL";

//			switch (dataColumnName)
//			{
//				case TestCaseStatusDataModel.DataColumns.TestCaseStatusId:
//					if (data.TestCaseStatusId != null)
//					{
//						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestCaseStatusDataModel.DataColumns.TestCaseStatusId, data.TestCaseStatusId);
//					}
//					else
//					{
//						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestCaseStatusDataModel.DataColumns.TestCaseStatusId);
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

//		public static List<TestCaseStatusDataModel> GetEntityDetails(TestCaseStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
//		{
//			const string sql = @"dbo.TestCaseStatusSearch ";

//			var parameters =
//			new
//			{
//					AuditId                 = requestProfile.AuditId
//				 ,	ApplicationId           = requestProfile.ApplicationId
//				 ,	ApplicationMode         = requestProfile.ApplicationModeId
//				 ,	ReturnAuditInfo         = returnAuditInfo
//				 ,	TestCaseStatusId                  = dataQuery.TestCaseStatusId
//				 ,	Name                    = dataQuery.Name
//			};

//			List<TestCaseStatusDataModel> result;
//			using (var dataAccess = new DataAccessBase(DataStoreKey))
//			{
//				result = dataAccess.Connection.Query<TestCaseStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
//			}

//			return result;

//		}

//		#endregion

//		#region Get List

//		public static DataTable GetList(RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(TestCaseStatusDataModel.Empty, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region Search

//		public static DataTable Search(TestCaseStatusDataModel data, RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(data, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region GetDetails

//		public static DataTable GetDetails(TestCaseStatusDataModel data, RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(data, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region Save

//		public static string Save(TestCaseStatusDataModel data, string action, RequestProfile requestProfile)
//		{

//			var sql = "EXEC ";

//			switch (action)
//			{
//				case "Create":
//					sql += "dbo.TestCaseStatusInsert  " +
//						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
//					break;

//				case "Update":
//					sql += "dbo.TestCaseStatusUpdate  " +
//						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
//					break;

//				default:
//					break;
//			}
//			sql = sql + ", " + ToSQLParameter(data, TestCaseStatusDataModel.DataColumns.TestCaseStatusId) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

//			return sql;
//		}

//		#endregion

//		#region Create

//		public static int Create(TestCaseStatusDataModel data, RequestProfile requestProfile)
//		{
//			var sql = Save(data, "Create", requestProfile);
//			var newId = DBDML.RunScalarSQL("TestCaseStatus.Insert", sql, DataStoreKey);
//			return Convert.ToInt32(newId);
//		}

//		#endregion

//		#region Update

//		public static void Update(TestCaseStatusDataModel data, RequestProfile requestProfile)
//		{
//			var sql = Save(data, "Update", requestProfile);
//			DBDML.RunSQL("TestCaseStatus.Update", sql, DataStoreKey);
//		}

//		#endregion

//		#region Delete

//		public static void Delete(TestCaseStatusDataModel data, RequestProfile requestProfile)
//		{

//			const string sql = @"dbo.TestCaseStatusDelete ";

//			var parameters =
//			new
//			{
//					AuditId = requestProfile.AuditId
//				,   TestCaseStatusId  = data.TestCaseStatusId
//			};

//			using (var dataAccess = new DataAccessBase(DataStoreKey))
//			{
//				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
//			}
//		}

//		#endregion

//		#region Does Exist

//		public static DataTable DoesExist(TestCaseStatusDataModel data, RequestProfile requestProfile)
//		{
//			var doesExistRequest = new TestCaseStatusDataModel();
//			doesExistRequest.Name = data.Name;
//			return Search(doesExistRequest, requestProfile);
//		}

//		#endregion

//		#region Renumber

//		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
//		{
//			var sql = "EXEC dbo.TestCaseStatusRenumber" +
//				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//				", @Seed = " + seed +
//				", @Increment = " + increment;
//			Framework.Components.DataAccess.DBDML.RunSQL("TestCaseStatus.Renumber", sql, DataStoreKey);
//		}

//		#endregion

//		#region Get Children

//		public static DataSet GetChildren(TestCaseStatusDataModel data, RequestProfile requestProfile)
//		{
//			var sql = "EXEC dbo.TestCaseStatusChildrenGet" +
//				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//				", " + ToSQLParameter(data, TestCaseStatusDataModel.DataColumns.TestCaseStatusId);
//			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
//			return oDT.DBDataset;
//		}

//		#endregion

//		#region IsDeletable

//		public static bool IsDeletable(TestCaseStatusDataModel data, RequestProfile requestProfile)
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
