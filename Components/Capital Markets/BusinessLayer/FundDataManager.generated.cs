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
	public partial class FundDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FundDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Fund");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FundDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FundDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundDataModel.DataColumns.FundId);
					}
					break;

				case FundDataModel.DataColumns.ManagementFirmId:
					if (data.ManagementFirmId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundDataModel.DataColumns.ManagementFirmId, data.ManagementFirmId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundDataModel.DataColumns.ManagementFirmId);
					}
					break;

				case FundDataModel.DataColumns.ManagementFirm:
					if (!string.IsNullOrEmpty(data.ManagementFirm))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FundDataModel.DataColumns.ManagementFirm, data.ManagementFirm);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundDataModel.DataColumns.ManagementFirm);
					}
					break;

				case FundDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FundDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundDataModel.DataColumns.Name);
					}
					break;

				case FundDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FundDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundDataModel.DataColumns.Description);
					}
					break;

				case FundDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FundDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FundDataModel.DataColumns.SortOrder);
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

		public static List<FundDataModel> GetEntityDetails(FundDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FundSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FundId           = dataQuery.FundId
				 ,	ManagementFirmId           = dataQuery.ManagementFirmId
				 ,	Name           = dataQuery.Name
			};

			List<FundDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FundDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FundDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FundDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FundDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(FundDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.FundInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FundUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FundDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, FundDataModel.DataColumns.ManagementFirmId); 
			sql = sql + ", " + ToSQLParameter(data, FundDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, FundDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, FundDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FundDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Fund.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FundDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Fund.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FundDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FundDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FundId  = data.FundId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FundDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FundDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.ManagementFirmId  = data.ManagementFirmId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
