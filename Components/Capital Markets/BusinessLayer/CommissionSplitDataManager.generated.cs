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
	public partial class CommissionSplitDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CommissionSplitDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CommissionSplit");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CommissionSplitDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CommissionSplitDataModel.DataColumns.CommissionSplitId:
					if (data.CommissionSplitId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionSplitDataModel.DataColumns.CommissionSplitId, data.CommissionSplitId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.CommissionSplitId);
					}
					break;

				case CommissionSplitDataModel.DataColumns.CommissionSplitCode:
					if (!string.IsNullOrEmpty(data.CommissionSplitCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionSplitDataModel.DataColumns.CommissionSplitCode, data.CommissionSplitCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.CommissionSplitCode);
					}
					break;

				case CommissionSplitDataModel.DataColumns.CommissionSplitDescription:
					if (!string.IsNullOrEmpty(data.CommissionSplitDescription))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionSplitDataModel.DataColumns.CommissionSplitDescription, data.CommissionSplitDescription);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.CommissionSplitDescription);
					}
					break;

				case CommissionSplitDataModel.DataColumns.FullRate:
					if (data.FullRate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionSplitDataModel.DataColumns.FullRate, data.FullRate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.FullRate);
					}
					break;

				case CommissionSplitDataModel.DataColumns.NoneCCA:
					if (data.NoneCCA != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionSplitDataModel.DataColumns.NoneCCA, data.NoneCCA);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.NoneCCA);
					}
					break;

				case CommissionSplitDataModel.DataColumns.CCA:
					if (data.CCA != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionSplitDataModel.DataColumns.CCA, data.CCA);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.CCA);
					}
					break;

				case CommissionSplitDataModel.DataColumns.StartDate:
					if (data.StartDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionSplitDataModel.DataColumns.StartDate, data.StartDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.StartDate);
					}
					break;

				case CommissionSplitDataModel.DataColumns.EndDate:
					if (data.EndDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionSplitDataModel.DataColumns.EndDate, data.EndDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.EndDate);
					}
					break;

				case CommissionSplitDataModel.DataColumns.LastModifiedBy:
					if (!string.IsNullOrEmpty(data.LastModifiedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionSplitDataModel.DataColumns.LastModifiedBy, data.LastModifiedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.LastModifiedBy);
					}
					break;

				case CommissionSplitDataModel.DataColumns.LastModifiedOn:
					if (data.LastModifiedOn != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionSplitDataModel.DataColumns.LastModifiedOn, data.LastModifiedOn);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.LastModifiedOn);
					}
					break;

				case CommissionSplitDataModel.DataColumns.CommissionCodeId:
					if (data.CommissionCodeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CommissionSplitDataModel.DataColumns.CommissionCodeId, data.CommissionCodeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.CommissionCodeId);
					}
					break;

				case CommissionSplitDataModel.DataColumns.CommissionCode:
					if (!string.IsNullOrEmpty(data.CommissionCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CommissionSplitDataModel.DataColumns.CommissionCode, data.CommissionCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CommissionSplitDataModel.DataColumns.CommissionCode);
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

		public static List<CommissionSplitDataModel> GetEntityDetails(CommissionSplitDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CommissionSplitSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CommissionSplitId           = dataQuery.CommissionSplitId
				 ,	CommissionSplitCode           = dataQuery.CommissionSplitCode
				 ,	CommissionCodeId           = dataQuery.CommissionCodeId
			};

			List<CommissionSplitDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CommissionSplitDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CommissionSplitDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CommissionSplitDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CommissionSplitDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CommissionSplitDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CommissionSplitInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CommissionSplitUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.CommissionSplitId); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.CommissionSplitCode); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.CommissionSplitDescription); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.FullRate); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.NoneCCA); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.CCA); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.StartDate); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.EndDate); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.LastModifiedBy); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.LastModifiedOn); 
			sql = sql + ", " + ToSQLParameter(data, CommissionSplitDataModel.DataColumns.CommissionCodeId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CommissionSplitDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CommissionSplit.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CommissionSplitDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CommissionSplit.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CommissionSplitDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CommissionSplitDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CommissionSplitId  = data.CommissionSplitId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CommissionSplitDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CommissionSplitDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.CommissionCodeId  = data.CommissionCodeId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
