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
	public partial class ForwardFXContractDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ForwardFXContractDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ForwardFXContract");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ForwardFXContractDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ForwardFXContractDataModel.DataColumns.ForwardFXContractId:
					if (data.ForwardFXContractId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ForwardFXContractDataModel.DataColumns.ForwardFXContractId, data.ForwardFXContractId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ForwardFXContractDataModel.DataColumns.ForwardFXContractId);
					}
					break;

				case ForwardFXContractDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ForwardFXContractDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ForwardFXContractDataModel.DataColumns.Name);
					}
					break;

				case ForwardFXContractDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ForwardFXContractDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ForwardFXContractDataModel.DataColumns.Description);
					}
					break;

				case ForwardFXContractDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ForwardFXContractDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ForwardFXContractDataModel.DataColumns.SortOrder);
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

		public static List<ForwardFXContractDataModel> GetEntityDetails(ForwardFXContractDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ForwardFXContractSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ForwardFXContractId           = dataQuery.ForwardFXContractId
				 ,	Name           = dataQuery.Name
			};

			List<ForwardFXContractDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ForwardFXContractDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ForwardFXContractDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ForwardFXContractDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ForwardFXContractDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(ForwardFXContractDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.ForwardFXContractInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ForwardFXContractUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ForwardFXContractDataModel.DataColumns.ForwardFXContractId); 
			sql = sql + ", " + ToSQLParameter(data, ForwardFXContractDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ForwardFXContractDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, ForwardFXContractDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ForwardFXContractDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ForwardFXContract.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ForwardFXContractDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ForwardFXContract.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ForwardFXContractDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ForwardFXContractDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ForwardFXContractId  = data.ForwardFXContractId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ForwardFXContractDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ForwardFXContractDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
