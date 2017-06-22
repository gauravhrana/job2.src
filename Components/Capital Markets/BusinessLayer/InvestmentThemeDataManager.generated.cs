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
	public partial class InvestmentThemeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static InvestmentThemeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("InvestmentTheme");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(InvestmentThemeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case InvestmentThemeDataModel.DataColumns.InvestmentThemeId:
					if (data.InvestmentThemeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestmentThemeDataModel.DataColumns.InvestmentThemeId, data.InvestmentThemeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentThemeDataModel.DataColumns.InvestmentThemeId);
					}
					break;

				case InvestmentThemeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestmentThemeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentThemeDataModel.DataColumns.Name);
					}
					break;

				case InvestmentThemeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InvestmentThemeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentThemeDataModel.DataColumns.Description);
					}
					break;

				case InvestmentThemeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InvestmentThemeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InvestmentThemeDataModel.DataColumns.SortOrder);
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

		public static List<InvestmentThemeDataModel> GetEntityDetails(InvestmentThemeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.InvestmentThemeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	InvestmentThemeId           = dataQuery.InvestmentThemeId
				 ,	Name           = dataQuery.Name
			};

			List<InvestmentThemeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<InvestmentThemeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<InvestmentThemeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(InvestmentThemeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(InvestmentThemeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(InvestmentThemeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.InvestmentThemeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.InvestmentThemeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, InvestmentThemeDataModel.DataColumns.InvestmentThemeId); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentThemeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentThemeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, InvestmentThemeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(InvestmentThemeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("InvestmentTheme.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(InvestmentThemeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("InvestmentTheme.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(InvestmentThemeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.InvestmentThemeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   InvestmentThemeId  = data.InvestmentThemeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(InvestmentThemeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new InvestmentThemeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
