using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.ReferenceData;

namespace ReferenceData.Components.BusinessLayer
{
	public partial class CountryXReligionDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CountryXReligionDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CountryXReligion");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CountryXReligionDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CountryXReligionDataModel.DataColumns.CountryXReligionId:
					if (data.CountryXReligionId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CountryXReligionDataModel.DataColumns.CountryXReligionId, data.CountryXReligionId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CountryXReligionDataModel.DataColumns.CountryXReligionId);
					}
					break;

				case CountryXReligionDataModel.DataColumns.CountryId:
					if (data.CountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CountryXReligionDataModel.DataColumns.CountryId, data.CountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CountryXReligionDataModel.DataColumns.CountryId);
					}
					break;

				case CountryXReligionDataModel.DataColumns.ReligionId:
					if (data.ReligionId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CountryXReligionDataModel.DataColumns.ReligionId, data.ReligionId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CountryXReligionDataModel.DataColumns.ReligionId);
					}
					break;

				case CountryXReligionDataModel.DataColumns.Country:
					if (!string.IsNullOrEmpty(data.Country))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CountryXReligionDataModel.DataColumns.Country, data.Country);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CountryXReligionDataModel.DataColumns.Country);
					}
					break;

				case CountryXReligionDataModel.DataColumns.Religion:
					if (!string.IsNullOrEmpty(data.Religion))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CountryXReligionDataModel.DataColumns.Religion, data.Religion);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CountryXReligionDataModel.DataColumns.Religion);
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

		public static List<CountryXReligionDataModel> GetEntityDetails(CountryXReligionDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CountryXReligionSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CountryXReligionId           = dataQuery.CountryXReligionId
				 ,	CountryId           = dataQuery.CountryId
				 ,	ReligionId           = dataQuery.ReligionId
			};

			List<CountryXReligionDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CountryXReligionDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CountryXReligionDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CountryXReligionDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CountryXReligionDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CountryXReligionDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CountryXReligionInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CountryXReligionUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CountryXReligionDataModel.DataColumns.CountryXReligionId); 
			sql = sql + ", " + ToSQLParameter(data, CountryXReligionDataModel.DataColumns.CountryId); 
			sql = sql + ", " + ToSQLParameter(data, CountryXReligionDataModel.DataColumns.ReligionId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CountryXReligionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CountryXReligion.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CountryXReligionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CountryXReligion.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CountryXReligionDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CountryXReligionDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				 ,	CountryXReligionId           = data.CountryXReligionId
				 ,	CountryId           = data.CountryId
				 ,	ReligionId           = data.ReligionId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CountryXReligionDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CountryXReligionDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
