//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using Dapper;
//using DataModel.TaskTimeTracker;
//using Framework.Components.DataAccess;
//using DataModel.Framework.DataAccess;
//using DataModel.TaskTimeTracker.TimeTracking;

//namespace TaskTimeTracker.Components.BusinessLayer
//{
//	public partial class QuestionCategoryDataManager : StandardDataManager
//	{
////		static string DataStoreKey = "";

////		static QuestionCategoryDataManager()
////		{
////			DataStoreKey = SetupConfiguration.GetDataStoreKey("QuestionCategory");
////		}

////		#region GetList

////		public static DataTable GetList(RequestProfile requestProfile)
////		{
////			var sql = "EXEC dbo.QuestionCategorySearch " +
////				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
////				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
////			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

////			return oDT.DBTable;
////		}

//		//public static List<QuestionCategoryDataModel> GetQuestionCategoryList(RequestProfile requestProfile)
//		//{
//		//	var list = GetEntityDetails(QuestionCategoryDataModel.Empty, requestProfile);

//		//	var result = list.Select(item => new QuestionCategoryDataModel()
//		//	{
//		//		Name = item.Name
//		//		,
//		//		QuestionCategoryId = item.QuestionCategoryId
//		//	}).ToList();

//		//	return result;
//		//}

////		//public static List<QuestionCategoryDataModel> GetQuestionCategoryList(RequestProfile requestProfile)
////		//{
////		//	var sql = "EXEC dbo.QuestionCategorySearch" +
////		//		" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
////		//		", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

////		//	var result = new List<QuestionCategoryDataModel>();

////		//	using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
////		//	{
////		//		var dbReader = reader.DBReader;

////		//		while (dbReader.Read())
////		//		{
////		//			var dataItem = new QuestionCategoryDataModel();

////		//			dataItem.QuestionCategoryId = (int)dbReader[QuestionCategoryDataModel.DataColumns.QuestionCategoryId];
////		//			dataItem.ApplicationId = (int)dbReader[BaseDataModel.BaseDataColumns.ApplicationId];

////		//			SetStandardInfo(dataItem, dbReader);

////		//			result.Add(dataItem);
////		//		}

////		//	}

////		//	return result;
////		//}

////		#endregion GetList

////		#region Search

////		public static string ToSQLParameter(QuestionCategoryDataModel data, string dataColumnName)
////		{
////			var returnValue = "NULL";

////			switch (dataColumnName)
////			{
////				case QuestionCategoryDataModel.DataColumns.QuestionCategoryId:
////					if (data.QuestionCategoryId != null)
////					{
////						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, QuestionCategoryDataModel.DataColumns.QuestionCategoryId, data.QuestionCategoryId);
////					}
////					else
////					{
////						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, QuestionCategoryDataModel.DataColumns.QuestionCategoryId);
////					}
////					break;

////				default:
////					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
////					break;

////			}

////			return returnValue;
////		}

////		public static DataTable Search(QuestionCategoryDataModel data, RequestProfile requestProfile)
////		{
////			var list = GetEntityDetails(data, requestProfile, 0);

////			var table = list.ToDataTable();

////			return table;

////		}

////		#endregion Search

////		#region GetDetails


////		public static DataTable GetDetails(QuestionCategoryDataModel data, RequestProfile requestProfile)
////		{
////			var list = GetEntityDetails(data, requestProfile);

////			var table = list.ToDataTable();

////			return table;
////		}

////		public static List<QuestionCategoryDataModel> GetEntityDetails(QuestionCategoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
////		{
////			const string sql = @"dbo.QuestionCategorySearch ";

////			var parameters =
////			new
////			{
////					AuditId                 = requestProfile.AuditId
////				,   ApplicationId           = requestProfile.ApplicationId
////				,	ApplicationMode			= requestProfile.ApplicationModeId
////				,   ReturnAuditInfo         = returnAuditInfo
////				,   QuestionCategoryId      = dataQuery.QuestionCategoryId
////				,   Name                    = dataQuery.Name
////				,   Description             = dataQuery.Description
////			};

////			List<QuestionCategoryDataModel> result;

////			using (var dataAccess = new DataAccessBase(DataStoreKey))
////			{
////				result = dataAccess.Connection.Query<QuestionCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
////			}

////			return result;
////		}

////		//public static List<QuestionCategoryDataModel> GetEntityDetails(QuestionCategoryDataModel data, int auditId)
////		//{
////		//    var sql = "EXEC dbo.QuestionCategorySearch " +
////		//              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
////		//              ", " + ToSQLParameter(data, QuestionCategoryDataModel.DataColumns.QuestionCategoryId);

////		//    var result = new List<QuestionCategoryDataModel>();

////		//    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
////		//    {
////		//        var dbReader = reader.DBReader;

////		//        while (dbReader.Read())
////		//        {
////		//            var dataItem = new QuestionCategoryDataModel();

////		//            dataItem.QuestionCategoryId = (int)dbReader[QuestionCategoryDataModel.DataColumns.QuestionCategoryId];

////		//            SetStandardInfo(dataItem, dbReader);

////		//            SetBaseInfo(dataItem, dbReader);

////		//            result.Add(dataItem);
////		//        }
////		//    }

////		//    return result;
////		//}

////		#endregion GetDetails

//		#region Get By Question

//		public static DataTable GetByQuestion(int questionId, RequestProfile requestProfile)
//		{
//			var sql = "EXEC QuestionCategorySearch @QuestionCategoryId     =" + questionId + ", " +
//						  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

//			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

//			return oDT.DBTable;
//		}

//		#endregion

//		#region Get By QuestionCategory

//		public static DataTable GetByQuestionCategory(int questionCategoryId, RequestProfile requestProfile)
//		{
//			var sql = "EXEC QuestionSearch @QuestionCategoryId       =" + questionCategoryId + ", " +
//						 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

//			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

//			return oDT.DBTable;
//		}

//		#endregion

////		#region CreateOrUpdate

////		private static string CreateOrUpdate(QuestionCategoryDataModel data, RequestProfile requestProfile, string action)
////		{
////			var sql = "EXEC ";

////			switch (action)
////			{
////				case "Create":
////					sql += "dbo.QuestionCategoryInsert  " + "\r\n" +
////						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
////						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
////					break;

////				case "Update":
////					sql += "dbo.QuestionCategoryUpdate  " + "\r\n" +
////						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
////						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
////					break;

////				default:
////					break;
////			}

////			sql = sql + ", " + ToSQLParameter(data, QuestionCategoryDataModel.DataColumns.QuestionCategoryId) +
////				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
////				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
////				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

////			return sql;

////		}
////		#endregion CreateOrUpdate

////		#region Create
////		public static void Create(QuestionCategoryDataModel data, RequestProfile requestProfile)
////		{
////			var sql = CreateOrUpdate(data, requestProfile, "Create");

////			DBDML.RunSQL("QuestionCategory.Insert", sql, DataStoreKey);
////		}
////		#endregion Create

////		#region Update
////		public static void Update(QuestionCategoryDataModel data, RequestProfile requestProfile)
////		{
////			var sql = CreateOrUpdate(data, requestProfile, "Update");

////			DBDML.RunSQL("QuestionCategory.Update", sql, DataStoreKey);
////		}
////		#endregion Update

////		#region Delete

////		public static void Delete(QuestionCategoryDataModel dataQuery, RequestProfile requestProfile)
////		{
////			const string sql = @"dbo.QuestionCategoryDelete ";

////			var parameters =
////			new
////			{
////				AuditId = requestProfile.AuditId
////				,
////				QuestionCategoryId = dataQuery.QuestionCategoryId
////			};

////			using (var dataAccess = new DataAccessBase(DataStoreKey))
////			{


////				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


////			}
////		}

////		#endregion Delete

////		#region DoesExist

////		public static DataTable DoesExist(QuestionCategoryDataModel data, RequestProfile requestProfile)
////		{
////			var doesExistRequest = new QuestionCategoryDataModel();
////			doesExistRequest.Name = data.Name;

////			return Search(doesExistRequest, requestProfile);
////		}

////		#endregion DoesExist

////		#region GetChildren

////		private static DataSet GetChildren(QuestionCategoryDataModel data, RequestProfile requestProfile)
////		{
////			var sql = "EXEC dbo.QuestionCategoryChildrenGet " +
////							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
////							", " + ToSQLParameter(data, QuestionCategoryDataModel.DataColumns.QuestionCategoryId);

////			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
////			return oDT.DBDataset;
////		}

////		#endregion

////		#region IsDeletable

////		public static bool IsDeletable(QuestionCategoryDataModel data, RequestProfile requestProfile)
////		{
////			var isDeletable = true;
////			var ds = GetChildren(data, requestProfile);
////			if (ds != null && ds.Tables.Count > 0)
////			{
////				foreach (DataTable dt in ds.Tables)
////				{
////					if (dt.Rows.Count > 0)
////					{
////						isDeletable = false;
////						break;
////					}
////				}
////			}
////			return isDeletable;
////		}

////		#endregion

////		#region Renumber
////		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
////		{
////			var sql = "EXEC dbo.QuestionCategoryRenumber " +
////					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
////					  ",@Seed = " + seed +
////					  ",@Increment = " + increment;

////			DBDML.RunSQL("QuestionCategory.Renumber", sql, DataStoreKey);
////		}
////		#endregion Renumber

//	}
//}
