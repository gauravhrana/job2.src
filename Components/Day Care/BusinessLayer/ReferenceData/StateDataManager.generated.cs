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
	public partial class StateDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static StateDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("State");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(StateDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case StateDataModel.DataColumns.StateId:
					if (data.StateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StateDataModel.DataColumns.StateId, data.StateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StateDataModel.DataColumns.StateId);
					}
					break;

				case StateDataModel.DataColumns.CountryId:
					if (data.CountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StateDataModel.DataColumns.CountryId, data.CountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StateDataModel.DataColumns.CountryId);
					}
					break;

				case StateDataModel.DataColumns.Country:
					if (!string.IsNullOrEmpty(data.Country))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StateDataModel.DataColumns.Country, data.Country);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StateDataModel.DataColumns.Country);
					}
					break;

				case StateDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StateDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StateDataModel.DataColumns.Name);
					}
					break;

				case StateDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StateDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StateDataModel.DataColumns.Description);
					}
					break;

				case StateDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StateDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StateDataModel.DataColumns.SortOrder);
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

		public static List<StateDataModel> GetEntityDetails(StateDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.StateSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	StateId           = dataQuery.StateId
				 ,	CountryId           = dataQuery.CountryId
				 ,	Name           = dataQuery.Name
			};

			List<StateDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<StateDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<StateDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(StateDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(StateDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(StateDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.StateInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.StateUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, StateDataModel.DataColumns.StateId); 
			sql = sql + ", " + ToSQLParameter(data, StateDataModel.DataColumns.CountryId); 
			sql = sql + ", " + ToSQLParameter(data, StateDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, StateDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, StateDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(StateDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("State.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(StateDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("State.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(StateDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.StateDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   StateId  = data.StateId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(StateDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new StateDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.CountryId  = data.CountryId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
