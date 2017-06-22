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
	public partial class CommissionCodeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CommissionCodeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CommissionCode");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CommissionCodeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CommissionCodeDataModel.DataColumns.CommissionCodeId:
					if (data.CommissionCodeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionCodeDataModel.DataColumns.CommissionCodeId, data.CommissionCodeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionCodeDataModel.DataColumns.CommissionCodeId);
					}
					break;

				case CommissionCodeDataModel.DataColumns.CommissionCodeCode:
					if (!string.IsNullOrEmpty(data.CommissionCodeCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionCodeDataModel.DataColumns.CommissionCodeCode, data.CommissionCodeCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionCodeDataModel.DataColumns.CommissionCodeCode);
					}
					break;

				case CommissionCodeDataModel.DataColumns.CommissionCodeDescription:
					if (!string.IsNullOrEmpty(data.CommissionCodeDescription))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionCodeDataModel.DataColumns.CommissionCodeDescription, data.CommissionCodeDescription);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionCodeDataModel.DataColumns.CommissionCodeDescription);
					}
					break;

				case CommissionCodeDataModel.DataColumns.BrokerId:
					if (data.BrokerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionCodeDataModel.DataColumns.BrokerId, data.BrokerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionCodeDataModel.DataColumns.BrokerId);
					}
					break;

				case CommissionCodeDataModel.DataColumns.Broker:
					if (!string.IsNullOrEmpty(data.Broker))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionCodeDataModel.DataColumns.Broker, data.Broker);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionCodeDataModel.DataColumns.Broker);
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

		public static List<CommissionCodeDataModel> GetEntityDetails(CommissionCodeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CommissionCodeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CommissionCodeId           = dataQuery.CommissionCodeId
				 ,	CommissionCodeCode           = dataQuery.CommissionCodeCode
				 ,	BrokerId           = dataQuery.BrokerId
			};

			List<CommissionCodeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CommissionCodeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CommissionCodeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CommissionCodeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CommissionCodeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CommissionCodeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CommissionCodeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CommissionCodeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CommissionCodeDataModel.DataColumns.CommissionCodeId); 
			sql = sql + ", " + ToSQLParameter(data, CommissionCodeDataModel.DataColumns.CommissionCodeCode); 
			sql = sql + ", " + ToSQLParameter(data, CommissionCodeDataModel.DataColumns.CommissionCodeDescription); 
			sql = sql + ", " + ToSQLParameter(data, CommissionCodeDataModel.DataColumns.BrokerId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CommissionCodeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CommissionCode.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CommissionCodeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CommissionCode.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CommissionCodeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CommissionCodeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CommissionCodeId  = data.CommissionCodeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CommissionCodeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CommissionCodeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.BrokerId  = data.BrokerId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
