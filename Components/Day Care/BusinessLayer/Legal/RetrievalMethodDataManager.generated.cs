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
	public partial class RetrievalMethodDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static RetrievalMethodDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("RetrievalMethod");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(RetrievalMethodDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case RetrievalMethodDataModel.DataColumns.RetrievalMethodId:
					if (data.RetrievalMethodId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RetrievalMethodDataModel.DataColumns.RetrievalMethodId, data.RetrievalMethodId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RetrievalMethodDataModel.DataColumns.RetrievalMethodId);
					}
					break;

				case RetrievalMethodDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RetrievalMethodDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RetrievalMethodDataModel.DataColumns.Name);
					}
					break;

				case RetrievalMethodDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RetrievalMethodDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RetrievalMethodDataModel.DataColumns.Description);
					}
					break;

				case RetrievalMethodDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RetrievalMethodDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RetrievalMethodDataModel.DataColumns.SortOrder);
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

		public static List<RetrievalMethodDataModel> GetEntityDetails(RetrievalMethodDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.RetrievalMethodSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	Name           = dataQuery.Name
			};

			List<RetrievalMethodDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<RetrievalMethodDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<RetrievalMethodDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(RetrievalMethodDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(RetrievalMethodDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(RetrievalMethodDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(RetrievalMethodDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.RetrievalMethodInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.RetrievalMethodUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, RetrievalMethodDataModel.DataColumns.RetrievalMethodId); 
			sql = sql + ", " + ToSQLParameter(data, RetrievalMethodDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, RetrievalMethodDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, RetrievalMethodDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(RetrievalMethodDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("RetrievalMethod.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(RetrievalMethodDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("RetrievalMethod.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(RetrievalMethodDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.RetrievalMethodDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   RetrievalMethodId  = data.RetrievalMethodId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(RetrievalMethodDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new RetrievalMethodDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
