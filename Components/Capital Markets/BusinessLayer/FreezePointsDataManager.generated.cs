using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class FreezePointsDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FreezePointsDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FreezePoints");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FreezePointsDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FreezePointsDataModel.DataColumns.FreezePointsId:
					if (data.FreezePointsId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FreezePointsDataModel.DataColumns.FreezePointsId, data.FreezePointsId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FreezePointsDataModel.DataColumns.FreezePointsId);
					}
					break;

				case FreezePointsDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FreezePointsDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FreezePointsDataModel.DataColumns.Name);
					}
					break;

				case FreezePointsDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FreezePointsDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FreezePointsDataModel.DataColumns.Description);
					}
					break;

				case FreezePointsDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FreezePointsDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FreezePointsDataModel.DataColumns.SortOrder);
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

		public static List<FreezePointsDataModel> GetEntityDetails(FreezePointsDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FreezePointsSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FreezePointsId           = dataQuery.FreezePointsId
				 ,	Name           = dataQuery.Name
			};

			List<FreezePointsDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FreezePointsDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FreezePointsDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FreezePointsDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FreezePointsDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(FreezePointsDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FreezePointsInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FreezePointsUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FreezePointsDataModel.DataColumns.FreezePointsId); 
			sql = sql + ", " + ToSQLParameter(data, FreezePointsDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, FreezePointsDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, FreezePointsDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FreezePointsDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FreezePoints.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FreezePointsDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FreezePoints.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FreezePointsDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FreezePointsDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FreezePointsId  = data.FreezePointsId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FreezePointsDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FreezePointsDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
