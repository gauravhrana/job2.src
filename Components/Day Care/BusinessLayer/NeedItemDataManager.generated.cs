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
	public partial class NeedItemDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static NeedItemDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("NeedItem");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(NeedItemDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case NeedItemDataModel.DataColumns.NeedItemId:
					if (data.NeedItemId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NeedItemDataModel.DataColumns.NeedItemId, data.NeedItemId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedItemDataModel.DataColumns.NeedItemId);
					}
					break;

				case NeedItemDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NeedItemDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedItemDataModel.DataColumns.Name);
					}
					break;

				case NeedItemDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, NeedItemDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedItemDataModel.DataColumns.Description);
					}
					break;

				case NeedItemDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NeedItemDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedItemDataModel.DataColumns.SortOrder);
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

		public static List<NeedItemDataModel> GetEntityDetails(NeedItemDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.NeedItemSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	NeedItemId           = dataQuery.NeedItemId
				 ,	Name           = dataQuery.Name
			};

			List<NeedItemDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<NeedItemDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<NeedItemDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(NeedItemDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(NeedItemDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(NeedItemDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.NeedItemInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.NeedItemUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, NeedItemDataModel.DataColumns.NeedItemId); 
			sql = sql + ", " + ToSQLParameter(data, NeedItemDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, NeedItemDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, NeedItemDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(NeedItemDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("NeedItem.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(NeedItemDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("NeedItem.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(NeedItemDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.NeedItemDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   NeedItemId  = data.NeedItemId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(NeedItemDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new NeedItemDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
