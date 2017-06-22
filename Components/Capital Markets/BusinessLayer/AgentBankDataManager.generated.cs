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
	public partial class AgentBankDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AgentBankDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AgentBank");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AgentBankDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AgentBankDataModel.DataColumns.AgentBankId:
					if (data.AgentBankId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AgentBankDataModel.DataColumns.AgentBankId, data.AgentBankId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AgentBankDataModel.DataColumns.AgentBankId);
					}
					break;

				case AgentBankDataModel.DataColumns.Url:
					if (!string.IsNullOrEmpty(data.Url))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AgentBankDataModel.DataColumns.Url, data.Url);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AgentBankDataModel.DataColumns.Url);
					}
					break;

				case AgentBankDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AgentBankDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AgentBankDataModel.DataColumns.Code);
					}
					break;

				case AgentBankDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AgentBankDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AgentBankDataModel.DataColumns.Name);
					}
					break;

				case AgentBankDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AgentBankDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AgentBankDataModel.DataColumns.Description);
					}
					break;

				case AgentBankDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AgentBankDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AgentBankDataModel.DataColumns.SortOrder);
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

		public static List<AgentBankDataModel> GetEntityDetails(AgentBankDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AgentBankSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AgentBankId           = dataQuery.AgentBankId
				 ,	Name           = dataQuery.Name
			};

			List<AgentBankDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AgentBankDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AgentBankDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AgentBankDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AgentBankDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(AgentBankDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(AgentBankDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.AgentBankInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AgentBankUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AgentBankDataModel.DataColumns.AgentBankId); 
			sql = sql + ", " + ToSQLParameter(data, AgentBankDataModel.DataColumns.Url); 
			sql = sql + ", " + ToSQLParameter(data, AgentBankDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, AgentBankDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AgentBankDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AgentBankDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AgentBankDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AgentBank.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AgentBankDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AgentBank.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AgentBankDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AgentBankDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AgentBankId  = data.AgentBankId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AgentBankDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AgentBankDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
