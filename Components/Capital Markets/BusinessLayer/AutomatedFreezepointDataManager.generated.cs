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
	public partial class AutomatedFreezepointDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AutomatedFreezepointDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AutomatedFreezepoint");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AutomatedFreezepointDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AutomatedFreezepointDataModel.DataColumns.AutomatedFreezepointId:
					if (data.AutomatedFreezepointId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AutomatedFreezepointDataModel.DataColumns.AutomatedFreezepointId, data.AutomatedFreezepointId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AutomatedFreezepointDataModel.DataColumns.AutomatedFreezepointId);
					}
					break;

				case AutomatedFreezepointDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AutomatedFreezepointDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AutomatedFreezepointDataModel.DataColumns.Name);
					}
					break;

				case AutomatedFreezepointDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AutomatedFreezepointDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AutomatedFreezepointDataModel.DataColumns.Description);
					}
					break;

				case AutomatedFreezepointDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AutomatedFreezepointDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AutomatedFreezepointDataModel.DataColumns.SortOrder);
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

		public static List<AutomatedFreezepointDataModel> GetEntityDetails(AutomatedFreezepointDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AutomatedFreezepointSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AutomatedFreezepointId           = dataQuery.AutomatedFreezepointId
				 ,	Name           = dataQuery.Name
			};

			List<AutomatedFreezepointDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AutomatedFreezepointDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AutomatedFreezepointDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AutomatedFreezepointDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AutomatedFreezepointDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AutomatedFreezepointDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AutomatedFreezepointInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AutomatedFreezepointUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AutomatedFreezepointDataModel.DataColumns.AutomatedFreezepointId); 
			sql = sql + ", " + ToSQLParameter(data, AutomatedFreezepointDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AutomatedFreezepointDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AutomatedFreezepointDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AutomatedFreezepointDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AutomatedFreezepoint.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AutomatedFreezepointDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AutomatedFreezepoint.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AutomatedFreezepointDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AutomatedFreezepointDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AutomatedFreezepointId  = data.AutomatedFreezepointId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AutomatedFreezepointDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AutomatedFreezepointDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
