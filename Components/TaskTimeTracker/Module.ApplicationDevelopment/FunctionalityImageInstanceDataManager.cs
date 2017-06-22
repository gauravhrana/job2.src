using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class FunctionalityImageInstanceDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static FunctionalityImageInstanceDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityImageInstance");
		}

		#region GetList

        public static List<FunctionalityImageInstanceDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityImageInstanceDataModel.Empty, requestProfile, 1);
		}

		#endregion

		#region GetDetails

		public static FunctionalityImageInstanceDataModel GetDetails(FunctionalityImageInstanceDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityImageInstanceDataModel> GetEntityDetails(FunctionalityImageInstanceDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityImageInstanceSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	ApplicationId = requestProfile.ApplicationId
				,	ApplicationMode = requestProfile.ApplicationModeId
				,	FunctionalityImageInstanceId = dataQuery.FunctionalityImageInstanceId
				,	FunctionalityImageId = dataQuery.FunctionalityImageId
				,	FunctionalityImageAttributeId = dataQuery.FunctionalityImageAttributeId
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<FunctionalityImageInstanceDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityImageInstanceDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}
		#endregion

		#region Create

		public static void Create(FunctionalityImageInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("FunctionalityImageInstance.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(FunctionalityImageInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("FunctionalityImageInstance.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FunctionalityImageInstanceDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityImageInstanceDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				FunctionalityImageInstanceId = dataQuery.FunctionalityImageInstanceId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(FunctionalityImageInstanceDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageInstanceId:
					if (data.FunctionalityImageInstanceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageInstanceId, data.FunctionalityImageInstanceId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageInstanceId);

					}
					break;

				case FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageId:
					if (data.FunctionalityImageId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageId, data.FunctionalityImageId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageId);

					}
					break;

				case FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttributeId:
					if (data.FunctionalityImageAttributeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttributeId, data.FunctionalityImageAttributeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttributeId);

					}
					break;

				case FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImage:
					if (!string.IsNullOrEmpty(data.FunctionalityImage))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImage, data.FunctionalityImage.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImage);

					}
					break;
				case FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttribute:
					if (!string.IsNullOrEmpty(data.FunctionalityImageAttribute))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttribute, data.FunctionalityImageAttribute.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttribute);

					}
					break;

			}
			return returnValue;
		}


		public static DataTable Search(FunctionalityImageInstanceDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(FunctionalityImageInstanceDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityImageInstanceInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityImageInstanceUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageInstanceId) +
						", " + ToSQLParameter(data, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageId) +
						", " + ToSQLParameter(data, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttributeId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(FunctionalityImageInstanceDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityImageInstanceDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(FunctionalityImageInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityImageInstanceChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageInstanceId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityImageInstanceDataModel data, RequestProfile requestProfile)
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
