using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class MediumOfExchangeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static MediumOfExchangeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MediumOfExchange");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(MediumOfExchangeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case MediumOfExchangeDataModel.DataColumns.MediumOfExchangeId:
					if (data.MediumOfExchangeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MediumOfExchangeDataModel.DataColumns.MediumOfExchangeId, data.MediumOfExchangeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MediumOfExchangeDataModel.DataColumns.MediumOfExchangeId);
					}
					break;

				case MediumOfExchangeDataModel.DataColumns.ExtendedDescription:
					if (!string.IsNullOrEmpty(data.ExtendedDescription))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MediumOfExchangeDataModel.DataColumns.ExtendedDescription, data.ExtendedDescription);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MediumOfExchangeDataModel.DataColumns.ExtendedDescription);
					}
					break;

				case MediumOfExchangeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MediumOfExchangeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MediumOfExchangeDataModel.DataColumns.Name);
					}
					break;

				case MediumOfExchangeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MediumOfExchangeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MediumOfExchangeDataModel.DataColumns.Description);
					}
					break;

				case MediumOfExchangeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MediumOfExchangeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MediumOfExchangeDataModel.DataColumns.SortOrder);
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

		public static List<MediumOfExchangeDataModel> GetEntityDetails(MediumOfExchangeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MediumOfExchangeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	MediumOfExchangeId           = dataQuery.MediumOfExchangeId
				 ,	Name           = dataQuery.Name
			};

			List<MediumOfExchangeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MediumOfExchangeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<MediumOfExchangeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(MediumOfExchangeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(MediumOfExchangeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(MediumOfExchangeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.MediumOfExchangeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MediumOfExchangeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, MediumOfExchangeDataModel.DataColumns.MediumOfExchangeId); 
			sql = sql + ", " + ToSQLParameter(data, MediumOfExchangeDataModel.DataColumns.ExtendedDescription); 
			sql = sql + ", " + ToSQLParameter(data, MediumOfExchangeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, MediumOfExchangeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, MediumOfExchangeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(MediumOfExchangeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("MediumOfExchange.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(MediumOfExchangeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("MediumOfExchange.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MediumOfExchangeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.MediumOfExchangeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   MediumOfExchangeId  = data.MediumOfExchangeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(MediumOfExchangeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MediumOfExchangeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
