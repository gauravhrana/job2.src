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
	public partial class ReleaseLogDetailMappingDataManager : BaseDataManager
	{
        static readonly string DataStoreKey = "";

		static ReleaseLogDetailMappingDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseLogDetailMapping");
		}

		#region Create By ChildReleaseLogDetail Items

		public static void CreateByChildReleaseLogDetail(int childReleaseLogDetailId, int[] parentReleaseLogDetailIds, RequestProfile requestProfile)
		{
			foreach (var parentReleaseLogDetailId in parentReleaseLogDetailIds)
			{
				var sql = "EXEC ReleaseLogDetailMappingInsert " +
						  "@ChildReleaseLogDetailId = " + childReleaseLogDetailId + ", " +
						  "@ParentReleaseLogDetailId =" + parentReleaseLogDetailId + ", " +
						  "@ApplicationId =" + requestProfile.ApplicationId + ", " +
						  "@AuditId	= " + requestProfile.AuditId;

				DBDML.RunSQL("ReleaseLogDetailMapping_Insert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By ParentReleaseLogDetail Items

		public static void CreateByParentReleaseLogDetail(int parentReleaseLogDetailId, int[] childReleaseLogDetailIds, RequestProfile requestProfile)
		{
			foreach (var childReleaseLogDetailId in childReleaseLogDetailIds)
			{
				var sql = "EXEC ReleaseLogDetailMappingInsert " +
						  "@ChildReleaseLogDetailId		=" + childReleaseLogDetailId + ", " +
						  "@ParentReleaseLogDetailId 		=" + parentReleaseLogDetailId + ", " +
						  "@ApplicationId						=" + requestProfile.ApplicationId + ", " +
						  "@AuditId								=" + requestProfile.AuditId;
				DBDML.RunSQL("ReleaseLogDetailMapping_Insert", sql, DataStoreKey);
			}
		}
		#endregion

		#region Get By ParentReleaseLogDetail Items

		public static DataTable GetByParentReleaseLogDetail(int parentReleaseLogDetailId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogDetailMappingSearchByPublishItem @ParentReleaseLogDetailId	=" + parentReleaseLogDetailId + ", " +
						  "@AuditId	=" + requestProfile.AuditId;
			var oDt = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDt.DBTable;
		}

		#endregion

		#region Get By ChildReleaseLogDetail Items

		public static DataTable GetByChildReleaseLogDetail(int childReleaseLogDetailId, RequestProfile requestProfile)
		{
			var sql = "EXEC ReleaseLogDetailMappingSearchByDevelopmentItem @ChildReleaseLogDetailId=" + childReleaseLogDetailId + ", " +
						  "@AuditId	=" + requestProfile.AuditId;
			var oDt = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDt.DBTable;
		}

		#endregion

		#region Delete By Publish Items

		public static void DeleteByParentReleaseLogDetail(int parentReleaseLogDetailId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseLogDetailMappingDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				ParentReleaseLogDetailId = parentReleaseLogDetailId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Delete By ChildReleaseLogDetail Items

		public static void DeleteByChildReleaseLogDetail(int childReleaseLogDetailId, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseLogDetailMappingDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				ChildReleaseLogDetailId = childReleaseLogDetailId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

	}
}
