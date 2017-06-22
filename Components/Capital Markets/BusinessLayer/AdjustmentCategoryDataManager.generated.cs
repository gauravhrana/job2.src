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
	public partial class AdjustmentCategoryDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AdjustmentCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AdjustmentCategory");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AdjustmentCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AdjustmentCategoryDataModel.DataColumns.AdjustmentCategoryId:
					if (data.AdjustmentCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AdjustmentCategoryDataModel.DataColumns.AdjustmentCategoryId, data.AdjustmentCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AdjustmentCategoryDataModel.DataColumns.AdjustmentCategoryId);
					}
					break;

				case AdjustmentCategoryDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AdjustmentCategoryDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AdjustmentCategoryDataModel.DataColumns.Code);
					}
					break;

				case AdjustmentCategoryDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AdjustmentCategoryDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AdjustmentCategoryDataModel.DataColumns.Name);
					}
					break;

				case AdjustmentCategoryDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AdjustmentCategoryDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AdjustmentCategoryDataModel.DataColumns.Description);
					}
					break;

				case AdjustmentCategoryDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AdjustmentCategoryDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AdjustmentCategoryDataModel.DataColumns.SortOrder);
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

		public static List<AdjustmentCategoryDataModel> GetEntityDetails(AdjustmentCategoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AdjustmentCategorySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AdjustmentCategoryId           = dataQuery.AdjustmentCategoryId
				 ,	Name           = dataQuery.Name
			};

			List<AdjustmentCategoryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AdjustmentCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AdjustmentCategoryDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AdjustmentCategoryDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AdjustmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(AdjustmentCategoryDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(AdjustmentCategoryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.AdjustmentCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AdjustmentCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AdjustmentCategoryDataModel.DataColumns.AdjustmentCategoryId); 
			sql = sql + ", " + ToSQLParameter(data, AdjustmentCategoryDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, AdjustmentCategoryDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AdjustmentCategoryDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AdjustmentCategoryDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AdjustmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AdjustmentCategory.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AdjustmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AdjustmentCategory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AdjustmentCategoryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AdjustmentCategoryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AdjustmentCategoryId  = data.AdjustmentCategoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AdjustmentCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AdjustmentCategoryDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
