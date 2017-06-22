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
	public partial class BathRoomDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static BathRoomDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("BathRoom");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(BathRoomDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BathRoomDataModel.DataColumns.BathRoomId:
					if (data.BathRoomId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BathRoomDataModel.DataColumns.BathRoomId, data.BathRoomId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BathRoomDataModel.DataColumns.BathRoomId);
					}
					break;

				case BathRoomDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BathRoomDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BathRoomDataModel.DataColumns.Name);
					}
					break;

				case BathRoomDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BathRoomDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BathRoomDataModel.DataColumns.Description);
					}
					break;

				case BathRoomDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BathRoomDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BathRoomDataModel.DataColumns.SortOrder);
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

		public static List<BathRoomDataModel> GetEntityDetails(BathRoomDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.BathRoomSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	BathRoomId           = dataQuery.BathRoomId
				 ,	Name           = dataQuery.Name
			};

			List<BathRoomDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<BathRoomDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<BathRoomDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(BathRoomDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(BathRoomDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(BathRoomDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.BathRoomInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.BathRoomUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, BathRoomDataModel.DataColumns.BathRoomId); 
			sql = sql + ", " + ToSQLParameter(data, BathRoomDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, BathRoomDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, BathRoomDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(BathRoomDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("BathRoom.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(BathRoomDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("BathRoom.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(BathRoomDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.BathRoomDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   BathRoomId  = data.BathRoomId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(BathRoomDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new BathRoomDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
