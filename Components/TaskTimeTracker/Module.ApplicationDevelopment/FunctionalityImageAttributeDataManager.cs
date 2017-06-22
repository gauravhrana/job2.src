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
	public partial class FunctionalityImageAttributeDataManager : StandardDataManager
	{
        private static readonly string DataStoreKey = "";

		static FunctionalityImageAttributeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityImageAttribute");
		}

		#region GetList

        public static List<FunctionalityImageAttributeDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityImageAttributeDataModel.Empty, requestProfile, 0);
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(FunctionalityImageAttributeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId:
					if (data.FunctionalityImageAttributeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId, data.FunctionalityImageAttributeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId);
					}
					break;
				case FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId:
					if (data.FunctionalityImageId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId, data.FunctionalityImageId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId);
					}
					break;
				case FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage:
					if (data.FunctionalityImage != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage, data.FunctionalityImage);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static FunctionalityImageAttributeDataModel GetDetails(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntityDetails

		public static List<FunctionalityImageAttributeDataModel> GetEntityDetails(FunctionalityImageAttributeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{

			const string sql = @"dbo.FunctionalityImageAttributeSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	ApplicationId = requestProfile.ApplicationId
				,	ApplicationMode = requestProfile.ApplicationModeId
				,	FunctionalityImageAttributeId = dataQuery.FunctionalityImageAttributeId
				,	Name = dataQuery.Name
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<FunctionalityImageAttributeDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityImageAttributeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		public static List<StandardListDataModel> GetFunctionalityImageAttributeList(RequestProfile requestProfile)
		{
			var data = new FunctionalityImageAttributeDataModel();
			var list = GetEntityDetails(data, requestProfile);

			var result = list.Select(item => new StandardListDataModel()
			{
				Name = item.Name,
				Value = item.FunctionalityImageAttributeId.ToString()
			})
			.ToList();

			return result;
		}

		#region CreateOrUpdate
		private static string CreateOrUpdate(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityImageAttributeInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityImageAttributeUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId) +
						", " + ToSQLParameter(data, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			DBDML.RunSQL("FunctionalityImageAttribute.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("FunctionalityImageAttribute.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityImageAttributeRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("FunctionalityImageAttribute.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(FunctionalityImageAttributeDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityImageAttributeDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				FunctionalityImageAttributeId = dataQuery.FunctionalityImageAttributeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityImageAttributeDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityImageAttributeChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId);

			var oDT = new DBDataSet("FunctionalityImageAttribute.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityImageAttributeChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId);

			var oDT = new DBDataSet("FunctionalityImageAttribute.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityImageAttributeDataModel data, RequestProfile requestProfile)
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
