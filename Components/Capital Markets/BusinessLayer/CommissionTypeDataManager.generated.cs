using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class CommissionTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CommissionTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CommissionType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CommissionTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CommissionTypeDataModel.DataColumns.CommissionTypeId:
					if (data.CommissionTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionTypeDataModel.DataColumns.CommissionTypeId, data.CommissionTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionTypeDataModel.DataColumns.CommissionTypeId);
					}
					break;

				case CommissionTypeDataModel.DataColumns.CommissionTypeDescription:
					if (!string.IsNullOrEmpty(data.CommissionTypeDescription))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionTypeDataModel.DataColumns.CommissionTypeDescription, data.CommissionTypeDescription);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionTypeDataModel.DataColumns.CommissionTypeDescription);
					}
					break;

				case CommissionTypeDataModel.DataColumns.LastModifiedBy:
					if (!string.IsNullOrEmpty(data.LastModifiedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionTypeDataModel.DataColumns.LastModifiedBy, data.LastModifiedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionTypeDataModel.DataColumns.LastModifiedBy);
					}
					break;

				case CommissionTypeDataModel.DataColumns.LastModifiedOn:
					if (data.LastModifiedOn != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionTypeDataModel.DataColumns.LastModifiedOn, data.LastModifiedOn);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionTypeDataModel.DataColumns.LastModifiedOn);
					}
					break;

				case CommissionTypeDataModel.DataColumns.ShowInFilter:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionTypeDataModel.DataColumns.ShowInFilter, data.ShowInFilter);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<CommissionTypeDataModel> GetEntityDetails(CommissionTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CommissionTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CommissionTypeId           = dataQuery.CommissionTypeId
				 ,	CommissionTypeDescription           = dataQuery.CommissionTypeDescription
			};

			List<CommissionTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CommissionTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CommissionTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CommissionTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CommissionTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CommissionTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CommissionTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CommissionTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CommissionTypeDataModel.DataColumns.CommissionTypeId); 
			sql = sql + ", " + ToSQLParameter(data, CommissionTypeDataModel.DataColumns.CommissionTypeDescription); 
			sql = sql + ", " + ToSQLParameter(data, CommissionTypeDataModel.DataColumns.LastModifiedBy); 
			sql = sql + ", " + ToSQLParameter(data, CommissionTypeDataModel.DataColumns.LastModifiedOn); 
			sql = sql + ", " + ToSQLParameter(data, CommissionTypeDataModel.DataColumns.ShowInFilter); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CommissionTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CommissionType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CommissionTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CommissionType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CommissionTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CommissionTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CommissionTypeId  = data.CommissionTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CommissionTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CommissionTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
