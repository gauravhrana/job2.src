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
	public partial class ProvinceDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ProvinceDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Province");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ProvinceDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ProvinceDataModel.DataColumns.ProvinceId:
					if (data.ProvinceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProvinceDataModel.DataColumns.ProvinceId, data.ProvinceId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProvinceDataModel.DataColumns.ProvinceId);
					}
					break;

				case ProvinceDataModel.DataColumns.CountryId:
					if (data.CountryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProvinceDataModel.DataColumns.CountryId, data.CountryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProvinceDataModel.DataColumns.CountryId);
					}
					break;

				case ProvinceDataModel.DataColumns.Country:
					if (!string.IsNullOrEmpty(data.Country))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProvinceDataModel.DataColumns.Country, data.Country);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProvinceDataModel.DataColumns.Country);
					}
					break;

				case ProvinceDataModel.DataColumns.ProvinceTypeId:
					if (data.ProvinceTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProvinceDataModel.DataColumns.ProvinceTypeId, data.ProvinceTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProvinceDataModel.DataColumns.ProvinceTypeId);
					}
					break;

				case ProvinceDataModel.DataColumns.ProvinceType:
					if (!string.IsNullOrEmpty(data.ProvinceType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProvinceDataModel.DataColumns.ProvinceType, data.ProvinceType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProvinceDataModel.DataColumns.ProvinceType);
					}
					break;

				case ProvinceDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProvinceDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProvinceDataModel.DataColumns.Name);
					}
					break;

				case ProvinceDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProvinceDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProvinceDataModel.DataColumns.Description);
					}
					break;

				case ProvinceDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProvinceDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProvinceDataModel.DataColumns.SortOrder);
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

		public static List<ProvinceDataModel> GetEntityDetails(ProvinceDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ProvinceSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ProvinceId           = dataQuery.ProvinceId
				 ,	CountryId           = dataQuery.CountryId
				 ,	ProvinceTypeId           = dataQuery.ProvinceTypeId
				 ,	Name           = dataQuery.Name
			};

			List<ProvinceDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ProvinceDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ProvinceDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ProvinceDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ProvinceDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(ProvinceDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.ProvinceInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ProvinceUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ProvinceDataModel.DataColumns.ProvinceId); 
			sql = sql + ", " + ToSQLParameter(data, ProvinceDataModel.DataColumns.CountryId); 
			sql = sql + ", " + ToSQLParameter(data, ProvinceDataModel.DataColumns.ProvinceTypeId); 
			sql = sql + ", " + ToSQLParameter(data, ProvinceDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ProvinceDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, ProvinceDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ProvinceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Province.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ProvinceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Province.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ProvinceDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ProvinceDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ProvinceId  = data.ProvinceId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ProvinceDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ProvinceDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.CountryId  = data.CountryId;
			doesExistRequest.ProvinceTypeId  = data.ProvinceTypeId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
