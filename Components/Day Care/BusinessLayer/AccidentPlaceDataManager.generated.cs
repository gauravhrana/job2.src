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
	public partial class AccidentPlaceDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AccidentPlaceDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AccidentPlace");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AccidentPlaceDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AccidentPlaceDataModel.DataColumns.AccidentPlaceId:
					if (data.AccidentPlaceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccidentPlaceDataModel.DataColumns.AccidentPlaceId, data.AccidentPlaceId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccidentPlaceDataModel.DataColumns.AccidentPlaceId);
					}
					break;

				case AccidentPlaceDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccidentPlaceDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccidentPlaceDataModel.DataColumns.Name);
					}
					break;

				case AccidentPlaceDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccidentPlaceDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccidentPlaceDataModel.DataColumns.Description);
					}
					break;

				case AccidentPlaceDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccidentPlaceDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccidentPlaceDataModel.DataColumns.SortOrder);
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

		public static List<AccidentPlaceDataModel> GetEntityDetails(AccidentPlaceDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AccidentPlaceSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AccidentPlaceId           = dataQuery.AccidentPlaceId
				 ,	Name           = dataQuery.Name
			};

			List<AccidentPlaceDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AccidentPlaceDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AccidentPlaceDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AccidentPlaceDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AccidentPlaceDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AccidentPlaceDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AccidentPlaceInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AccidentPlaceUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AccidentPlaceDataModel.DataColumns.AccidentPlaceId); 
			sql = sql + ", " + ToSQLParameter(data, AccidentPlaceDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AccidentPlaceDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AccidentPlaceDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AccidentPlaceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AccidentPlace.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AccidentPlaceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AccidentPlace.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AccidentPlaceDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AccidentPlaceDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AccidentPlaceId  = data.AccidentPlaceId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AccidentPlaceDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AccidentPlaceDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
