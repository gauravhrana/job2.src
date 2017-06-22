using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;

namespace TaskTimeTracker.Components.BusinessLayer
{
	public partial class ProductivityAreaFeatureDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ProductivityAreaFeatureDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ProductivityAreaFeature");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ProductivityAreaFeatureDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ProductivityAreaFeatureDataModel.DataColumns.ProductivityAreaFeatureId:
					if (data.ProductivityAreaFeatureId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProductivityAreaFeatureDataModel.DataColumns.ProductivityAreaFeatureId, data.ProductivityAreaFeatureId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProductivityAreaFeatureDataModel.DataColumns.ProductivityAreaFeatureId);
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

		public static List<ProductivityAreaFeatureDataModel> GetEntityDetails(ProductivityAreaFeatureDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ProductivityAreaFeatureSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ProductivityAreaFeatureId                  = dataQuery.ProductivityAreaFeatureId
				 ,	Name                    = dataQuery.Name
			};

			List<ProductivityAreaFeatureDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ProductivityAreaFeatureDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ProductivityAreaFeatureDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ProductivityAreaFeatureDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ProductivityAreaFeatureDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static ProductivityAreaFeatureDataModel GetDetails(ProductivityAreaFeatureDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(ProductivityAreaFeatureDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ProductivityAreaFeatureInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ProductivityAreaFeatureUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ProductivityAreaFeatureDataModel.DataColumns.ProductivityAreaFeatureId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ProductivityAreaFeatureDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ProductivityAreaFeature.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ProductivityAreaFeatureDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ProductivityAreaFeature.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ProductivityAreaFeatureDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ProductivityAreaFeatureDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ProductivityAreaFeatureId  = data.ProductivityAreaFeatureId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ProductivityAreaFeatureDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ProductivityAreaFeatureDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProductivityAreaFeatureRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("ProductivityAreaFeature.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(ProductivityAreaFeatureDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProductivityAreaFeatureChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, ProductivityAreaFeatureDataModel.DataColumns.ProductivityAreaFeatureId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ProductivityAreaFeatureDataModel data, RequestProfile requestProfile)
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
