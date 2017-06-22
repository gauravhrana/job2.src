using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;

namespace DayCare.Components.BusinessLayer
{
	public partial class FoodTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FoodTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FoodType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FoodTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FoodTypeDataModel.DataColumns.FoodTypeId:
					if (data.FoodTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FoodTypeDataModel.DataColumns.FoodTypeId, data.FoodTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FoodTypeDataModel.DataColumns.FoodTypeId);
					}
					break;

				case FoodTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FoodTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FoodTypeDataModel.DataColumns.Name);
					}
					break;

				case FoodTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FoodTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FoodTypeDataModel.DataColumns.Description);
					}
					break;

				case FoodTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FoodTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FoodTypeDataModel.DataColumns.SortOrder);
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

		public static List<FoodTypeDataModel> GetEntityDetails(FoodTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FoodTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FoodTypeId           = dataQuery.FoodTypeId
				 ,	Name           = dataQuery.Name
			};

			List<FoodTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FoodTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FoodTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FoodTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FoodTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(FoodTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FoodTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FoodTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FoodTypeDataModel.DataColumns.FoodTypeId); 
			sql = sql + ", " + ToSQLParameter(data, FoodTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, FoodTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, FoodTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FoodTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FoodType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FoodTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FoodType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FoodTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FoodTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FoodTypeId  = data.FoodTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FoodTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FoodTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
