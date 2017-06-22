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
	public partial class InvestingFeederDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static InvestingFeederDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("InvestingFeeder");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(InvestingFeederDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case InvestingFeederDataModel.DataColumns.InvestingFeederId:
					if (data.InvestingFeederId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestingFeederDataModel.DataColumns.InvestingFeederId, data.InvestingFeederId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestingFeederDataModel.DataColumns.InvestingFeederId);
					}
					break;

				case InvestingFeederDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestingFeederDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestingFeederDataModel.DataColumns.FundId);
					}
					break;

				case InvestingFeederDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestingFeederDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestingFeederDataModel.DataColumns.Fund);
					}
					break;

				case InvestingFeederDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestingFeederDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestingFeederDataModel.DataColumns.Name);
					}
					break;

				case InvestingFeederDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestingFeederDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestingFeederDataModel.DataColumns.Description);
					}
					break;

				case InvestingFeederDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestingFeederDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestingFeederDataModel.DataColumns.SortOrder);
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

		public static List<InvestingFeederDataModel> GetEntityDetails(InvestingFeederDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.InvestingFeederSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	InvestingFeederId           = dataQuery.InvestingFeederId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<InvestingFeederDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<InvestingFeederDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<InvestingFeederDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(InvestingFeederDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(InvestingFeederDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static InvestingFeederDataModel GetDetails(InvestingFeederDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save


		public static string Save(InvestingFeederDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.InvestingFeederInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.InvestingFeederUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, InvestingFeederDataModel.DataColumns.InvestingFeederId); 
			sql = sql + ", " + ToSQLParameter(data, InvestingFeederDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, InvestingFeederDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, InvestingFeederDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, InvestingFeederDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(InvestingFeederDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("InvestingFeeder.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(InvestingFeederDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("InvestingFeeder.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(InvestingFeederDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.InvestingFeederDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   InvestingFeederId  = data.InvestingFeederId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(InvestingFeederDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new InvestingFeederDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
