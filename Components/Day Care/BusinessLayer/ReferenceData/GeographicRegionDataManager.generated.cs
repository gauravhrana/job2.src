using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.ReferenceData;

namespace ReferenceData.Components.BusinessLayer
{
	public partial class GeographicRegionDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static GeographicRegionDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("GeographicRegion");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(GeographicRegionDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case GeographicRegionDataModel.DataColumns.GeographicRegionId:
					if (data.GeographicRegionId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, GeographicRegionDataModel.DataColumns.GeographicRegionId, data.GeographicRegionId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, GeographicRegionDataModel.DataColumns.GeographicRegionId);
					}
					break;

				case GeographicRegionDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, GeographicRegionDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, GeographicRegionDataModel.DataColumns.Name);
					}
					break;

				case GeographicRegionDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, GeographicRegionDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, GeographicRegionDataModel.DataColumns.Description);
					}
					break;

				case GeographicRegionDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, GeographicRegionDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, GeographicRegionDataModel.DataColumns.SortOrder);
					}
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<GeographicRegionDataModel> GetEntityDetails(GeographicRegionDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.GeographicRegionSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	GeographicRegionId           = dataQuery.GeographicRegionId
				 ,	Name           = dataQuery.Name
			};

			List<GeographicRegionDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<GeographicRegionDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<GeographicRegionDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(GeographicRegionDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(GeographicRegionDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(GeographicRegionDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.GeographicRegionInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.GeographicRegionUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, GeographicRegionDataModel.DataColumns.GeographicRegionId); 
			sql = sql + ", " + ToSQLParameter(data, GeographicRegionDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, GeographicRegionDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, GeographicRegionDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(GeographicRegionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("GeographicRegion.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(GeographicRegionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("GeographicRegion.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(GeographicRegionDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.GeographicRegionDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   GeographicRegionId  = data.GeographicRegionId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(GeographicRegionDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new GeographicRegionDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
