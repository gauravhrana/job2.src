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
	public partial class AnnotationsDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AnnotationsDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Annotations");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AnnotationsDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AnnotationsDataModel.DataColumns.AnnotationsId:
					if (data.AnnotationsId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AnnotationsDataModel.DataColumns.AnnotationsId, data.AnnotationsId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AnnotationsDataModel.DataColumns.AnnotationsId);
					}
					break;

				case AnnotationsDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AnnotationsDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AnnotationsDataModel.DataColumns.Name);
					}
					break;

				case AnnotationsDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AnnotationsDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AnnotationsDataModel.DataColumns.Description);
					}
					break;

				case AnnotationsDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AnnotationsDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AnnotationsDataModel.DataColumns.SortOrder);
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

		public static List<AnnotationsDataModel> GetEntityDetails(AnnotationsDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AnnotationsSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AnnotationsId           = dataQuery.AnnotationsId
				 ,	Name           = dataQuery.Name
			};

			List<AnnotationsDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AnnotationsDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AnnotationsDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AnnotationsDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AnnotationsDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AnnotationsDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AnnotationsInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AnnotationsUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AnnotationsDataModel.DataColumns.AnnotationsId); 
			sql = sql + ", " + ToSQLParameter(data, AnnotationsDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AnnotationsDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AnnotationsDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AnnotationsDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Annotations.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AnnotationsDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Annotations.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AnnotationsDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AnnotationsDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AnnotationsId  = data.AnnotationsId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AnnotationsDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AnnotationsDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
