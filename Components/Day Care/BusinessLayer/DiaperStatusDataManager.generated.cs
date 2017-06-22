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
	public partial class DiaperStatusDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static DiaperStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DiaperStatus");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(DiaperStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DiaperStatusDataModel.DataColumns.DiaperStatusId:
					if (data.DiaperStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DiaperStatusDataModel.DataColumns.DiaperStatusId, data.DiaperStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DiaperStatusDataModel.DataColumns.DiaperStatusId);
					}
					break;

				case DiaperStatusDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DiaperStatusDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DiaperStatusDataModel.DataColumns.Name);
					}
					break;

				case DiaperStatusDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DiaperStatusDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DiaperStatusDataModel.DataColumns.Description);
					}
					break;

				case DiaperStatusDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DiaperStatusDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DiaperStatusDataModel.DataColumns.SortOrder);
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

		public static List<DiaperStatusDataModel> GetEntityDetails(DiaperStatusDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.DiaperStatusSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	DiaperStatusId           = dataQuery.DiaperStatusId
				 ,	Name           = dataQuery.Name
			};

			List<DiaperStatusDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<DiaperStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<DiaperStatusDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(DiaperStatusDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(DiaperStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(DiaperStatusDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.DiaperStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.DiaperStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, DiaperStatusDataModel.DataColumns.DiaperStatusId); 
			sql = sql + ", " + ToSQLParameter(data, DiaperStatusDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, DiaperStatusDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, DiaperStatusDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(DiaperStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("DiaperStatus.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(DiaperStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("DiaperStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DiaperStatusDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.DiaperStatusDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   DiaperStatusId  = data.DiaperStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(DiaperStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new DiaperStatusDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
