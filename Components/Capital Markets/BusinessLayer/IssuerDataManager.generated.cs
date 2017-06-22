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
	public partial class IssuerDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static IssuerDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Issuer");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(IssuerDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case IssuerDataModel.DataColumns.IssuerId:
					if (data.IssuerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, IssuerDataModel.DataColumns.IssuerId, data.IssuerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, IssuerDataModel.DataColumns.IssuerId);
					}
					break;

				case IssuerDataModel.DataColumns.Url:
					if (!string.IsNullOrEmpty(data.Url))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, IssuerDataModel.DataColumns.Url, data.Url);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, IssuerDataModel.DataColumns.Url);
					}
					break;

				case IssuerDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, IssuerDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, IssuerDataModel.DataColumns.Code);
					}
					break;

				case IssuerDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, IssuerDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, IssuerDataModel.DataColumns.Name);
					}
					break;

				case IssuerDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, IssuerDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, IssuerDataModel.DataColumns.Description);
					}
					break;

				case IssuerDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, IssuerDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, IssuerDataModel.DataColumns.SortOrder);
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

		public static List<IssuerDataModel> GetEntityDetails(IssuerDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.IssuerSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	IssuerId           = dataQuery.IssuerId
				 ,	Name           = dataQuery.Name
			};

			List<IssuerDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<IssuerDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<IssuerDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(IssuerDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(IssuerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(IssuerDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(IssuerDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.IssuerInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.IssuerUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, IssuerDataModel.DataColumns.IssuerId); 
			sql = sql + ", " + ToSQLParameter(data, IssuerDataModel.DataColumns.Url); 
			sql = sql + ", " + ToSQLParameter(data, IssuerDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, IssuerDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, IssuerDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, IssuerDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(IssuerDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Issuer.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(IssuerDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Issuer.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(IssuerDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.IssuerDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   IssuerId  = data.IssuerId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(IssuerDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new IssuerDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
