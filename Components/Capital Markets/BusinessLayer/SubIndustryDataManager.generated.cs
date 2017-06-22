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
	public partial class SubIndustryDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SubIndustryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SubIndustry");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SubIndustryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SubIndustryDataModel.DataColumns.SubIndustryId:
					if (data.SubIndustryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SubIndustryDataModel.DataColumns.SubIndustryId, data.SubIndustryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubIndustryDataModel.DataColumns.SubIndustryId);
					}
					break;

				case SubIndustryDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SubIndustryDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubIndustryDataModel.DataColumns.Code);
					}
					break;

				case SubIndustryDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SubIndustryDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubIndustryDataModel.DataColumns.Name);
					}
					break;

				case SubIndustryDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SubIndustryDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubIndustryDataModel.DataColumns.Description);
					}
					break;

				case SubIndustryDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SubIndustryDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubIndustryDataModel.DataColumns.SortOrder);
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

		public static List<SubIndustryDataModel> GetEntityDetails(SubIndustryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SubIndustrySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	Name           = dataQuery.Name
			};

			List<SubIndustryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SubIndustryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SubIndustryDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SubIndustryDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SubIndustryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(SubIndustryDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(SubIndustryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.SubIndustryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SubIndustryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SubIndustryDataModel.DataColumns.SubIndustryId); 
			sql = sql + ", " + ToSQLParameter(data, SubIndustryDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, SubIndustryDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, SubIndustryDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, SubIndustryDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SubIndustryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SubIndustry.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SubIndustryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SubIndustry.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SubIndustryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SubIndustryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SubIndustryId  = data.SubIndustryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SubIndustryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SubIndustryDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
