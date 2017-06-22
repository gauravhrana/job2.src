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
	public partial class BrokerDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static BrokerDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Broker");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(BrokerDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BrokerDataModel.DataColumns.BrokerId:
					if (data.BrokerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BrokerDataModel.DataColumns.BrokerId, data.BrokerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerDataModel.DataColumns.BrokerId);
					}
					break;

				case BrokerDataModel.DataColumns.Url:
					if (!string.IsNullOrEmpty(data.Url))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BrokerDataModel.DataColumns.Url, data.Url);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerDataModel.DataColumns.Url);
					}
					break;

				case BrokerDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BrokerDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerDataModel.DataColumns.Code);
					}
					break;

				case BrokerDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BrokerDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerDataModel.DataColumns.Name);
					}
					break;

				case BrokerDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BrokerDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerDataModel.DataColumns.Description);
					}
					break;

				case BrokerDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BrokerDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BrokerDataModel.DataColumns.SortOrder);
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

		public static List<BrokerDataModel> GetEntityDetails(BrokerDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.BrokerSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	BrokerId           = dataQuery.BrokerId
				 ,	Name           = dataQuery.Name
			};

			List<BrokerDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<BrokerDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<BrokerDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(BrokerDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(BrokerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(BrokerDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(BrokerDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.BrokerInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.BrokerUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, BrokerDataModel.DataColumns.BrokerId); 
			sql = sql + ", " + ToSQLParameter(data, BrokerDataModel.DataColumns.Url); 
			sql = sql + ", " + ToSQLParameter(data, BrokerDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, BrokerDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, BrokerDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, BrokerDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(BrokerDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Broker.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(BrokerDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Broker.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(BrokerDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.BrokerDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   BrokerId  = data.BrokerId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(BrokerDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new BrokerDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
