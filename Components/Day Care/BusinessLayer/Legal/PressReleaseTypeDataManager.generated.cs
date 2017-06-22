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
	public partial class PressReleaseTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PressReleaseTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PressReleaseType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PressReleaseTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PressReleaseTypeDataModel.DataColumns.PressReleaseTypeId:
					if (data.PressReleaseTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PressReleaseTypeDataModel.DataColumns.PressReleaseTypeId, data.PressReleaseTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PressReleaseTypeDataModel.DataColumns.PressReleaseTypeId);
					}
					break;

				case PressReleaseTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PressReleaseTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PressReleaseTypeDataModel.DataColumns.Name);
					}
					break;

				case PressReleaseTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PressReleaseTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PressReleaseTypeDataModel.DataColumns.Description);
					}
					break;

				case PressReleaseTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PressReleaseTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PressReleaseTypeDataModel.DataColumns.SortOrder);
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

		public static List<PressReleaseTypeDataModel> GetEntityDetails(PressReleaseTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PressReleaseTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	Name           = dataQuery.Name
			};

			List<PressReleaseTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PressReleaseTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PressReleaseTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PressReleaseTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PressReleaseTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(PressReleaseTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PressReleaseTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PressReleaseTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PressReleaseTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PressReleaseTypeDataModel.DataColumns.PressReleaseTypeId); 
			sql = sql + ", " + ToSQLParameter(data, PressReleaseTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, PressReleaseTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, PressReleaseTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PressReleaseTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PressReleaseType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PressReleaseTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PressReleaseType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PressReleaseTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PressReleaseTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PressReleaseTypeId  = data.PressReleaseTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PressReleaseTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PressReleaseTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
