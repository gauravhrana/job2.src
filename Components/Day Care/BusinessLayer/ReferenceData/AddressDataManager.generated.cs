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
	public partial class AddressDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AddressDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Address");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AddressDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AddressDataModel.DataColumns.AddressId:
					if (data.AddressId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AddressDataModel.DataColumns.AddressId, data.AddressId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.AddressId);
					}
					break;

				case AddressDataModel.DataColumns.CityId:
					if (data.CityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AddressDataModel.DataColumns.CityId, data.CityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.CityId);
					}
					break;

				case AddressDataModel.DataColumns.City:
					if (!string.IsNullOrEmpty(data.City))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AddressDataModel.DataColumns.City, data.City);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.City);
					}
					break;

				case AddressDataModel.DataColumns.StateId:
					if (data.StateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AddressDataModel.DataColumns.StateId, data.StateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.StateId);
					}
					break;

				case AddressDataModel.DataColumns.State:
					if (!string.IsNullOrEmpty(data.State))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AddressDataModel.DataColumns.State, data.State);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.State);
					}
					break;

				case AddressDataModel.DataColumns.CountryId:
					if (data.CountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AddressDataModel.DataColumns.CountryId, data.CountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.CountryId);
					}
					break;

				case AddressDataModel.DataColumns.Country:
					if (!string.IsNullOrEmpty(data.Country))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AddressDataModel.DataColumns.Country, data.Country);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.Country);
					}
					break;

				case AddressDataModel.DataColumns.Address1:
					if (!string.IsNullOrEmpty(data.Address1))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AddressDataModel.DataColumns.Address1, data.Address1);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.Address1);
					}
					break;

				case AddressDataModel.DataColumns.Address2:
					if (!string.IsNullOrEmpty(data.Address2))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AddressDataModel.DataColumns.Address2, data.Address2);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.Address2);
					}
					break;

				case AddressDataModel.DataColumns.PostalCode:
					if (!string.IsNullOrEmpty(data.PostalCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AddressDataModel.DataColumns.PostalCode, data.PostalCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AddressDataModel.DataColumns.PostalCode);
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

		public static List<AddressDataModel> GetEntityDetails(AddressDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AddressSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AddressId           = dataQuery.AddressId
				 ,	CityId           = dataQuery.CityId
				 ,	StateId           = dataQuery.StateId
				 ,	CountryId           = dataQuery.CountryId
			};

			List<AddressDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AddressDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AddressDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AddressDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AddressDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AddressDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AddressInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AddressUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AddressDataModel.DataColumns.AddressId); 
			sql = sql + ", " + ToSQLParameter(data, AddressDataModel.DataColumns.CityId); 
			sql = sql + ", " + ToSQLParameter(data, AddressDataModel.DataColumns.StateId); 
			sql = sql + ", " + ToSQLParameter(data, AddressDataModel.DataColumns.CountryId); 
			sql = sql + ", " + ToSQLParameter(data, AddressDataModel.DataColumns.Address1); 
			sql = sql + ", " + ToSQLParameter(data, AddressDataModel.DataColumns.Address2); 
			sql = sql + ", " + ToSQLParameter(data, AddressDataModel.DataColumns.PostalCode); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AddressDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Address.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AddressDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Address.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AddressDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AddressDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AddressId  = data.AddressId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AddressDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AddressDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.CityId  = data.CityId;
			doesExistRequest.StateId  = data.StateId;
			doesExistRequest.CountryId  = data.CountryId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
