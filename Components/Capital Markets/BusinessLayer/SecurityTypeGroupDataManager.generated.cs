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
	public partial class SecurityTypeGroupDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SecurityTypeGroupDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SecurityTypeGroup");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SecurityTypeGroupDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SecurityTypeGroupDataModel.DataColumns.SecurityTypeGroupId:
					if (data.SecurityTypeGroupId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityTypeGroupDataModel.DataColumns.SecurityTypeGroupId, data.SecurityTypeGroupId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityTypeGroupDataModel.DataColumns.SecurityTypeGroupId);
					}
					break;

				case SecurityTypeGroupDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityTypeGroupDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityTypeGroupDataModel.DataColumns.Name);
					}
					break;

				case SecurityTypeGroupDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityTypeGroupDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityTypeGroupDataModel.DataColumns.Description);
					}
					break;

				case SecurityTypeGroupDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityTypeGroupDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityTypeGroupDataModel.DataColumns.SortOrder);
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

		public static List<SecurityTypeGroupDataModel> GetEntityDetails(SecurityTypeGroupDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SecurityTypeGroupSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SecurityTypeGroupId           = dataQuery.SecurityTypeGroupId
				 ,	Name           = dataQuery.Name
			};

			List<SecurityTypeGroupDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SecurityTypeGroupDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SecurityTypeGroupDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SecurityTypeGroupDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SecurityTypeGroupDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SecurityTypeGroupDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SecurityTypeGroupInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SecurityTypeGroupUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SecurityTypeGroupDataModel.DataColumns.SecurityTypeGroupId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityTypeGroupDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, SecurityTypeGroupDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, SecurityTypeGroupDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SecurityTypeGroupDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SecurityTypeGroup.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SecurityTypeGroupDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SecurityTypeGroup.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SecurityTypeGroupDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SecurityTypeGroupDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SecurityTypeGroupId  = data.SecurityTypeGroupId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SecurityTypeGroupDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SecurityTypeGroupDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
