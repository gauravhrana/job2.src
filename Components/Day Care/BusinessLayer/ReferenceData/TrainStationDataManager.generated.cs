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
	public partial class TrainStationDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TrainStationDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TrainStation");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TrainStationDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TrainStationDataModel.DataColumns.TrainStationId:
					if (data.TrainStationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TrainStationDataModel.DataColumns.TrainStationId, data.TrainStationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TrainStationDataModel.DataColumns.TrainStationId);
					}
					break;

				case TrainStationDataModel.DataColumns.CountryId:
					if (data.CountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TrainStationDataModel.DataColumns.CountryId, data.CountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TrainStationDataModel.DataColumns.CountryId);
					}
					break;

				case TrainStationDataModel.DataColumns.Country:
					if (!string.IsNullOrEmpty(data.Country))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TrainStationDataModel.DataColumns.Country, data.Country);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TrainStationDataModel.DataColumns.Country);
					}
					break;

				case TrainStationDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TrainStationDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TrainStationDataModel.DataColumns.Name);
					}
					break;

				case TrainStationDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TrainStationDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TrainStationDataModel.DataColumns.Description);
					}
					break;

				case TrainStationDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TrainStationDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TrainStationDataModel.DataColumns.SortOrder);
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

		public static List<TrainStationDataModel> GetEntityDetails(TrainStationDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TrainStationSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TrainStationId           = dataQuery.TrainStationId
				 ,	CountryId           = dataQuery.CountryId
				 ,	Name           = dataQuery.Name
			};

			List<TrainStationDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TrainStationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TrainStationDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TrainStationDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TrainStationDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TrainStationDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TrainStationInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TrainStationUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TrainStationDataModel.DataColumns.TrainStationId); 
			sql = sql + ", " + ToSQLParameter(data, TrainStationDataModel.DataColumns.CountryId); 
			sql = sql + ", " + ToSQLParameter(data, TrainStationDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, TrainStationDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, TrainStationDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TrainStationDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TrainStation.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TrainStationDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TrainStation.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TrainStationDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TrainStationDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TrainStationId  = data.TrainStationId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TrainStationDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TrainStationDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.CountryId  = data.CountryId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
