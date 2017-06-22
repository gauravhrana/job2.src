using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Legal;

namespace Legal.Components.BusinessLayer
{
	public partial class ReportingRequirementDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ReportingRequirementDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReportingRequirement");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ReportingRequirementDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReportingRequirementDataModel.DataColumns.ReportingRequirementId:
					if (data.ReportingRequirementId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReportingRequirementDataModel.DataColumns.ReportingRequirementId, data.ReportingRequirementId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportingRequirementDataModel.DataColumns.ReportingRequirementId);
					}
					break;

				case ReportingRequirementDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReportingRequirementDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportingRequirementDataModel.DataColumns.Name);
					}
					break;

				case ReportingRequirementDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReportingRequirementDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportingRequirementDataModel.DataColumns.Description);
					}
					break;

				case ReportingRequirementDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReportingRequirementDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReportingRequirementDataModel.DataColumns.SortOrder);
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

		public static List<ReportingRequirementDataModel> GetEntityDetails(ReportingRequirementDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReportingRequirementSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	Name           = dataQuery.Name
			};

			List<ReportingRequirementDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReportingRequirementDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ReportingRequirementDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ReportingRequirementDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ReportingRequirementDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(ReportingRequirementDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(ReportingRequirementDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.ReportingRequirementInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReportingRequirementUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ReportingRequirementDataModel.DataColumns.ReportingRequirementId); 
			sql = sql + ", " + ToSQLParameter(data, ReportingRequirementDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ReportingRequirementDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, ReportingRequirementDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ReportingRequirementDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ReportingRequirement.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ReportingRequirementDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ReportingRequirement.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ReportingRequirementDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ReportingRequirementDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ReportingRequirementId  = data.ReportingRequirementId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ReportingRequirementDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReportingRequirementDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
