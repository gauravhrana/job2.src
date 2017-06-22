using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;

namespace DayCare.Components.BusinessLayer
{
	public partial class AccidentReportDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AccidentReportDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AccidentReport");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AccidentReportDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AccidentReportDataModel.DataColumns.AccidentReportId:
					if (data.AccidentReportId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccidentReportDataModel.DataColumns.AccidentReportId, data.AccidentReportId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccidentReportDataModel.DataColumns.AccidentReportId);
					}
					break;

				case AccidentReportDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccidentReportDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccidentReportDataModel.DataColumns.Name);
					}
					break;

				case AccidentReportDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccidentReportDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccidentReportDataModel.DataColumns.Description);
					}
					break;

				case AccidentReportDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccidentReportDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccidentReportDataModel.DataColumns.SortOrder);
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

		public static List<AccidentReportDataModel> GetEntityDetails(AccidentReportDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AccidentReportSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AccidentReportId           = dataQuery.AccidentReportId
				 ,	Name           = dataQuery.Name
			};

			List<AccidentReportDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AccidentReportDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AccidentReportDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AccidentReportDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AccidentReportDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AccidentReportDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AccidentReportInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AccidentReportUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AccidentReportDataModel.DataColumns.AccidentReportId); 
			sql = sql + ", " + ToSQLParameter(data, AccidentReportDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AccidentReportDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AccidentReportDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AccidentReportDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AccidentReport.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AccidentReportDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AccidentReport.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AccidentReportDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AccidentReportDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AccidentReportId  = data.AccidentReportId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AccidentReportDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AccidentReportDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
