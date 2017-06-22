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
	public partial class BrokerGroupDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static BrokerGroupDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("BrokerGroup");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(BrokerGroupDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BrokerGroupDataModel.DataColumns.BrokerGroupId:
					if (data.BrokerGroupId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BrokerGroupDataModel.DataColumns.BrokerGroupId, data.BrokerGroupId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerGroupDataModel.DataColumns.BrokerGroupId);
					}
					break;

				case BrokerGroupDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BrokerGroupDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerGroupDataModel.DataColumns.Name);
					}
					break;

				case BrokerGroupDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BrokerGroupDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerGroupDataModel.DataColumns.Description);
					}
					break;

				case BrokerGroupDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BrokerGroupDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerGroupDataModel.DataColumns.SortOrder);
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

		public static List<BrokerGroupDataModel> GetEntityDetails(BrokerGroupDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.BrokerGroupSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	BrokerGroupId           = dataQuery.BrokerGroupId
				 ,	Name           = dataQuery.Name
			};

			List<BrokerGroupDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<BrokerGroupDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<BrokerGroupDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(BrokerGroupDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(BrokerGroupDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(BrokerGroupDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.BrokerGroupInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.BrokerGroupUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, BrokerGroupDataModel.DataColumns.BrokerGroupId); 
			sql = sql + ", " + ToSQLParameter(data, BrokerGroupDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, BrokerGroupDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, BrokerGroupDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(BrokerGroupDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("BrokerGroup.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(BrokerGroupDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("BrokerGroup.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(BrokerGroupDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.BrokerGroupDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   BrokerGroupId  = data.BrokerGroupId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(BrokerGroupDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new BrokerGroupDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
