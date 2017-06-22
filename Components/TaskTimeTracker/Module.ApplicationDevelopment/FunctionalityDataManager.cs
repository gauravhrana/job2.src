using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{

	public partial class FunctionalityDataManager : StandardDataManager
	{
        private static readonly string DataStoreKey = "";

		static FunctionalityDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Functionality");
		}

		#region GetList

        public static List<FunctionalityDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityDataModel.Empty, requestProfile, 1);
		}

		#endregion GetList

		public static List<FunctionalityDataModel> GetFunctionalityList(RequestProfile requestedProfile)
		{
			var sql = "EXEC dbo.FunctionalitySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId);// +
			//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestedProfile.ApplicationId);

			var result = new List<FunctionalityDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new FunctionalityDataModel();

					dataItem.ApplicationId = (int)dbReader[FunctionalityDataModel.DataColumns.ApplicationId];
					dataItem.FunctionalityId = (int)dbReader[FunctionalityDataModel.DataColumns.FunctionalityId];
					dataItem.FunctionalityPriorityId = (int)dbReader[FunctionalityDataModel.DataColumns.FunctionalityPriorityId];
					dataItem.FunctionalityActiveStatusId = (int)dbReader[FunctionalityDataModel.DataColumns.FunctionalityActiveStatusId];
					dataItem.Description = dbReader[StandardDataModel.StandardDataColumns.Description].ToString();
					dataItem.SortOrder = (int)dbReader[StandardDataModel.StandardDataColumns.SortOrder];
					dataItem.Name = dbReader[StandardDataModel.StandardDataColumns.Name].ToString();

					result.Add(dataItem);
				}

			}

			return result;
		}


		#region Search
		public static string ToSQLParameter(FunctionalityDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				
				case FunctionalityDataModel.DataColumns.FunctionalityId:
					if (data.FunctionalityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityDataModel.DataColumns.FunctionalityId, data.FunctionalityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityDataModel.DataColumns.FunctionalityId);
					}
					break;
				case FunctionalityDataModel.DataColumns.FunctionalityActiveStatusId:
					if (data.FunctionalityActiveStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityDataModel.DataColumns.FunctionalityActiveStatusId, data.FunctionalityActiveStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityDataModel.DataColumns.FunctionalityActiveStatusId);
					}
					break;
				case FunctionalityDataModel.DataColumns.FunctionalityPriorityId:
					if (data.FunctionalityPriorityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityDataModel.DataColumns.FunctionalityPriorityId, data.FunctionalityPriorityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityDataModel.DataColumns.FunctionalityPriorityId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(FunctionalityDataModel data, RequestProfile requestedProfile)
		{
			var list = GetEntityDetails(data, requestedProfile, 0);

			var table = list.ToDataTable();

			return table;

		}


		#endregion Search

		#region GetDetails

		public static FunctionalityDataModel GetDetails(FunctionalityDataModel data, RequestProfile requestedProfile)
		{
			var list = GetEntityDetails(data, requestedProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityDataModel> GetEntityDetails(FunctionalityDataModel dataQuery, RequestProfile requestedProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalitySearch ";

			var parameters =
			new
			{
					AuditId = requestedProfile.AuditId
				,	ApplicationId = dataQuery.ApplicationId
				,	FunctionalityId = dataQuery.FunctionalityId
				,	Name = dataQuery.Name
				,	FunctionalityPriorityId = dataQuery.FunctionalityPriorityId
				,	FunctionalityActiveStatusId = dataQuery.FunctionalityActiveStatus
				,	NumberOfImages = dataQuery.NumberOfImages
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<FunctionalityDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region CreateOrUpdate
		private static string CreateOrUpdate(FunctionalityDataModel data, RequestProfile requestedProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestedProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
						   ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestedProfile.ApplicationId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, FunctionalityDataModel.DataColumns.FunctionalityId) +
						", " + ToSQLParameter(data, FunctionalityDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, FunctionalityDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, FunctionalityDataModel.DataColumns.FunctionalityActiveStatusId) +
						", " + ToSQLParameter(data, FunctionalityDataModel.DataColumns.FunctionalityPriorityId);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(FunctionalityDataModel data, RequestProfile requestedProfile)
		{
			var sql = CreateOrUpdate(data, requestedProfile, "Create");

			DBDML.RunSQL("Functionality.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(FunctionalityDataModel data, RequestProfile requestedProfile)
		{
			var sql = CreateOrUpdate(data, requestedProfile, "Update");

			DBDML.RunSQL("Functionality.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("Functionality.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(FunctionalityDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				FunctionalityId = dataQuery.FunctionalityId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}

		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(FunctionalityDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(FunctionalityDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityDataModel.DataColumns.FunctionalityId);

			var oDT = new DBDataSet("Functionality.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(FunctionalityDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityDataModel.DataColumns.FunctionalityId);

			var oDT = new DBDataSet("Functionality.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityDataModel data, RequestProfile requestProfile)
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
	}
}
