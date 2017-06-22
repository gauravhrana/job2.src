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
	public partial class MealDetailDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static MealDetailDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MealDetail");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(MealDetailDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case MealDetailDataModel.DataColumns.MealDetailId:
					if (data.MealDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MealDetailDataModel.DataColumns.MealDetailId, data.MealDetailId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MealDetailDataModel.DataColumns.MealDetailId);
					}
					break;

				case MealDetailDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MealDetailDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MealDetailDataModel.DataColumns.Name);
					}
					break;

				case MealDetailDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MealDetailDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MealDetailDataModel.DataColumns.Description);
					}
					break;

				case MealDetailDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MealDetailDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MealDetailDataModel.DataColumns.SortOrder);
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

		public static List<MealDetailDataModel> GetEntityDetails(MealDetailDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MealDetailSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	Name           = dataQuery.Name
			};

			List<MealDetailDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MealDetailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<MealDetailDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(MealDetailDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(MealDetailDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(MealDetailDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.MealDetailInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MealDetailUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, MealDetailDataModel.DataColumns.MealDetailId); 
			sql = sql + ", " + ToSQLParameter(data, MealDetailDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, MealDetailDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, MealDetailDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(MealDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("MealDetail.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(MealDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("MealDetail.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MealDetailDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.MealDetailDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   MealDetailId  = data.MealDetailId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(MealDetailDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MealDetailDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
