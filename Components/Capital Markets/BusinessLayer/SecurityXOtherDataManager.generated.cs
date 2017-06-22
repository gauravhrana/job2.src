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
	public partial class SecurityXOtherDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SecurityXOtherDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SecurityXOther");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SecurityXOtherDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SecurityXOtherDataModel.DataColumns.SecurityXOtherId:
					if (data.SecurityXOtherId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXOtherDataModel.DataColumns.SecurityXOtherId, data.SecurityXOtherId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXOtherDataModel.DataColumns.SecurityXOtherId);
					}
					break;

				case SecurityXOtherDataModel.DataColumns.SourcedFromThomsonReuters:
					if (!string.IsNullOrEmpty(data.SourcedFromThomsonReuters))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXOtherDataModel.DataColumns.SourcedFromThomsonReuters, data.SourcedFromThomsonReuters);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXOtherDataModel.DataColumns.SourcedFromThomsonReuters);
					}
					break;

				case SecurityXOtherDataModel.DataColumns.WhenIssued:
					if (!string.IsNullOrEmpty(data.WhenIssued))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXOtherDataModel.DataColumns.WhenIssued, data.WhenIssued);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXOtherDataModel.DataColumns.WhenIssued);
					}
					break;

				case SecurityXOtherDataModel.DataColumns.SecurityId:
					if (data.SecurityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityXOtherDataModel.DataColumns.SecurityId, data.SecurityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXOtherDataModel.DataColumns.SecurityId);
					}
					break;

				case SecurityXOtherDataModel.DataColumns.Security:
					if (!string.IsNullOrEmpty(data.Security))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityXOtherDataModel.DataColumns.Security, data.Security);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityXOtherDataModel.DataColumns.Security);
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

		public static List<SecurityXOtherDataModel> GetEntityDetails(SecurityXOtherDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SecurityXOtherSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SecurityXOtherId           = dataQuery.SecurityXOtherId
				 ,	SecurityId           = dataQuery.SecurityId
			};

			List<SecurityXOtherDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SecurityXOtherDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SecurityXOtherDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SecurityXOtherDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SecurityXOtherDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SecurityXOtherDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SecurityXOtherInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SecurityXOtherUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SecurityXOtherDataModel.DataColumns.SecurityXOtherId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXOtherDataModel.DataColumns.SourcedFromThomsonReuters); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXOtherDataModel.DataColumns.WhenIssued); 
			sql = sql + ", " + ToSQLParameter(data, SecurityXOtherDataModel.DataColumns.SecurityId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SecurityXOtherDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SecurityXOther.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SecurityXOtherDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SecurityXOther.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SecurityXOtherDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SecurityXOtherDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SecurityXOtherId  = data.SecurityXOtherId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SecurityXOtherDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SecurityXOtherDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.SecurityId  = data.SecurityId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
