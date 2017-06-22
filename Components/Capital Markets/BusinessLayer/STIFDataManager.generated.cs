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
	public partial class STIFDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static STIFDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("STIF");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(STIFDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case STIFDataModel.DataColumns.STIFId:
					if (data.STIFId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, STIFDataModel.DataColumns.STIFId, data.STIFId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, STIFDataModel.DataColumns.STIFId);
					}
					break;

				case STIFDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, STIFDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, STIFDataModel.DataColumns.Name);
					}
					break;

				case STIFDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, STIFDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, STIFDataModel.DataColumns.Description);
					}
					break;

				case STIFDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, STIFDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, STIFDataModel.DataColumns.SortOrder);
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

		public static List<STIFDataModel> GetEntityDetails(STIFDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.STIFSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	STIFId           = dataQuery.STIFId
				 ,	Name           = dataQuery.Name
			};

			List<STIFDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<STIFDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<STIFDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(STIFDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(STIFDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(STIFDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.STIFInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.STIFUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, STIFDataModel.DataColumns.STIFId); 
			sql = sql + ", " + ToSQLParameter(data, STIFDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, STIFDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, STIFDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(STIFDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("STIF.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(STIFDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("STIF.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(STIFDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.STIFDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   STIFId  = data.STIFId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(STIFDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new STIFDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
