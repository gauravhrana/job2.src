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
	public partial class SecurityClassDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SecurityClassDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SecurityClass");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SecurityClassDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SecurityClassDataModel.DataColumns.SecurityClassId:
					if (data.SecurityClassId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityClassDataModel.DataColumns.SecurityClassId, data.SecurityClassId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityClassDataModel.DataColumns.SecurityClassId);
					}
					break;

				case SecurityClassDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityClassDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityClassDataModel.DataColumns.Name);
					}
					break;

				case SecurityClassDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SecurityClassDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityClassDataModel.DataColumns.Description);
					}
					break;

				case SecurityClassDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SecurityClassDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SecurityClassDataModel.DataColumns.SortOrder);
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

		public static List<SecurityClassDataModel> GetEntityDetails(SecurityClassDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SecurityClassSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SecurityClassId           = dataQuery.SecurityClassId
				 ,	Name           = dataQuery.Name
			};

			List<SecurityClassDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SecurityClassDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SecurityClassDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SecurityClassDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SecurityClassDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SecurityClassDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SecurityClassInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SecurityClassUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SecurityClassDataModel.DataColumns.SecurityClassId); 
			sql = sql + ", " + ToSQLParameter(data, SecurityClassDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, SecurityClassDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, SecurityClassDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SecurityClassDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SecurityClass.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SecurityClassDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SecurityClass.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SecurityClassDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SecurityClassDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SecurityClassId  = data.SecurityClassId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SecurityClassDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SecurityClassDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
