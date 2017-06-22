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
	public partial class MealTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static MealTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MealType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(MealTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case MealTypeDataModel.DataColumns.MealTypeId:
					if (data.MealTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MealTypeDataModel.DataColumns.MealTypeId, data.MealTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MealTypeDataModel.DataColumns.MealTypeId);
					}
					break;

				case MealTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MealTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MealTypeDataModel.DataColumns.Name);
					}
					break;

				case MealTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MealTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MealTypeDataModel.DataColumns.Description);
					}
					break;

				case MealTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MealTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MealTypeDataModel.DataColumns.SortOrder);
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

		public static List<MealTypeDataModel> GetEntityDetails(MealTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MealTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	MealTypeId           = dataQuery.MealTypeId
				 ,	Name           = dataQuery.Name
			};

			List<MealTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MealTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<MealTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(MealTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(MealTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(MealTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.MealTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MealTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, MealTypeDataModel.DataColumns.MealTypeId); 
			sql = sql + ", " + ToSQLParameter(data, MealTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, MealTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, MealTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(MealTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("MealType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(MealTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("MealType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MealTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.MealTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   MealTypeId  = data.MealTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(MealTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MealTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
