using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.ReferenceData;

namespace ReferenceData.Components.BusinessLayer
{
	public partial class GenderDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static GenderDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Gender");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(GenderDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case GenderDataModel.DataColumns.GenderId:
					if (data.GenderId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, GenderDataModel.DataColumns.GenderId, data.GenderId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, GenderDataModel.DataColumns.GenderId);
					}
					break;

				case GenderDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, GenderDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, GenderDataModel.DataColumns.Name);
					}
					break;

				case GenderDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, GenderDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, GenderDataModel.DataColumns.Description);
					}
					break;

				case GenderDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, GenderDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, GenderDataModel.DataColumns.SortOrder);
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

		public static List<GenderDataModel> GetEntityDetails(GenderDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.GenderSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	GenderId           = dataQuery.GenderId
				 ,	Name           = dataQuery.Name
			};

			List<GenderDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<GenderDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<GenderDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(GenderDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(GenderDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(GenderDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.GenderInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.GenderUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, GenderDataModel.DataColumns.GenderId); 
			sql = sql + ", " + ToSQLParameter(data, GenderDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, GenderDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, GenderDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(GenderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Gender.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(GenderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Gender.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(GenderDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.GenderDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   GenderId  = data.GenderId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(GenderDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new GenderDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
