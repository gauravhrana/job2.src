using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog
{
	public partial class PublishXDevelopmentDataManager : BaseDataManager
	{
        static readonly string DataStoreKey = "";

		static PublishXDevelopmentDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseLogDetailMapping");
		}

		#region Create By Development Items

		public static void CreateByDevelopment(int developmentId, int[] publishIds, RequestProfile requestProfile)
		{
			foreach (var publishId in publishIds)
			{
				var sql = "EXEC ReleaseLogDetailMappingInsert " +
						  "@DevelopmentId = " + developmentId + ", " +
						  "@PublishId =" + publishId + ", " +
						  "@ApplicationId =" + requestProfile.ApplicationId + ", " +
						  "@AuditId	= " + requestProfile.AuditId;

				DBDML.RunSQL("ReleaseLogDetailMapping_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Search


		//public static DataTable Search(Data data, RequestProfile requestProfile)
		//{

		//    // formulate SQL  
		//    var sql = "EXEC dbo.ReleaseLogDetailMappingSearch " +
		//        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
		//        ", " + data.ToSQLParameter(DataColumns.PublishId) +
		//        ", " + data.ToSQLParameter(DataColumns.DevelopmentId);


		//    var oDT = new Framework.Components.DataAccess.DBDataTable("ReleaseLogDetailMapping.Search", sql, DataStoreKey);

		//    return oDT.DBTable;

		//}

		#endregion Search

		#region Create By Publish Items

		public static void CreateByPublish(int publishId, int[] developmentIds, RequestProfile requestProfile)
		{
			foreach (var developmentId in developmentIds)
			{
				var sql = "EXEC ReleaseLogDetailMappingInsert " +
						  "@DevelopmentId						=" + developmentId + ", " +
						  "@PublishId					=" + publishId + ", " +
						  "@ApplicationId			    =" + requestProfile.ApplicationId + ", " +
						  "@AuditId						=" + requestProfile.AuditId;
				DBDML.RunSQL("ReleaseLogDetailMapping_Insert", sql, DataStoreKey);
			}
		}
		#endregion

		#region Get By Publish Items

		public static DataTable GetByPublish(int publishId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogDetailMappingSearchByPublishItem @PublishId			=" + publishId + ", " +
						  "@AuditId									=" + requestProfile.AuditId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get By Development Items

		public static DataTable GetByDevelopment(int developmentId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogDetailMappingSearchByDevelopmentItem @DevelopmentId=" + developmentId + ", " +
						  "@AuditId	=" + requestProfile.AuditId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete By Publish Items

		public static void DeleteByPublish(int publishId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseLogDetailMappingDelete ";

			var parameters =	new
								{
										AuditId				= requestProfile.AuditId
									,	PublishId			= publishId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Delete By Development Items

		public static void DeleteByDevelopment(int developmentId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseLogDetailMappingDelete ";

			var parameters =	new
								{
										AuditId					= requestProfile.AuditId
									,	DevelopmentId			= developmentId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region DoesExist

		//public static DataTable DoesExist(ReleaseLogDetailMapping.Data data, RequestProfile requestProfile)
		//{
		//    var sql = "EXEC dbo.ReleaseLogDetailMappingSearch " +
		//                    "  @" + Columns.ReleaseLogDetailMappingId + "				=  " + data.ReleaseLogDetailMappingId.ToString() +
		//                    "  @" + Columns.DevelopmentId + "						=  " + data.DevelopmentId.ToString() +
		//                    ", @" + Columns.PublishId + "					=  " + data.PublishId.ToString() +
		//                    ", @" + BaseColumns.AuditId + "					=  " + requestProfile.AuditId.ToString();

		//    var oDT = new Framework.Components.DataAccess.DBDataTable("ReleaseLogDetailMapping.DoesExist", sql, DataStoreKey);

		//    return oDT.DBTable;
		//}


		#endregion DoesExist

	}
}
