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
	public partial class ManagementFirmDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ManagementFirmDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ManagementFirm");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ManagementFirmDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ManagementFirmDataModel.DataColumns.ManagementFirmId:
					if (data.ManagementFirmId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ManagementFirmDataModel.DataColumns.ManagementFirmId, data.ManagementFirmId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ManagementFirmDataModel.DataColumns.ManagementFirmId);
					}
					break;

				case ManagementFirmDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ManagementFirmDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ManagementFirmDataModel.DataColumns.Name);
					}
					break;

				case ManagementFirmDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ManagementFirmDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ManagementFirmDataModel.DataColumns.Description);
					}
					break;

				case ManagementFirmDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ManagementFirmDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ManagementFirmDataModel.DataColumns.SortOrder);
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

		public static List<ManagementFirmDataModel> GetEntityDetails(ManagementFirmDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ManagementFirmSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ManagementFirmId           = dataQuery.ManagementFirmId
				 ,	Name           = dataQuery.Name
			};

			List<ManagementFirmDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ManagementFirmDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ManagementFirmDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ManagementFirmDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ManagementFirmDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(ManagementFirmDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.ManagementFirmInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ManagementFirmUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ManagementFirmDataModel.DataColumns.ManagementFirmId); 
			sql = sql + ", " + ToSQLParameter(data, ManagementFirmDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ManagementFirmDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, ManagementFirmDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ManagementFirmDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ManagementFirm.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ManagementFirmDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ManagementFirm.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ManagementFirmDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ManagementFirmDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ManagementFirmId  = data.ManagementFirmId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ManagementFirmDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ManagementFirmDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
