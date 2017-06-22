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
	public partial class HelpLineDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static HelpLineDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("HelpLine");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(HelpLineDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case HelpLineDataModel.DataColumns.HelpLineId:
					if (data.HelpLineId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, HelpLineDataModel.DataColumns.HelpLineId, data.HelpLineId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HelpLineDataModel.DataColumns.HelpLineId);
					}
					break;

				case HelpLineDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, HelpLineDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HelpLineDataModel.DataColumns.Name);
					}
					break;

				case HelpLineDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, HelpLineDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HelpLineDataModel.DataColumns.Description);
					}
					break;

				case HelpLineDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, HelpLineDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, HelpLineDataModel.DataColumns.SortOrder);
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

		public static List<HelpLineDataModel> GetEntityDetails(HelpLineDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.HelpLineSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	Name           = dataQuery.Name
			};

			List<HelpLineDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<HelpLineDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<HelpLineDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(HelpLineDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(HelpLineDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(HelpLineDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.HelpLineInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.HelpLineUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, HelpLineDataModel.DataColumns.HelpLineId); 
			sql = sql + ", " + ToSQLParameter(data, HelpLineDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, HelpLineDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, HelpLineDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(HelpLineDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("HelpLine.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(HelpLineDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("HelpLine.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(HelpLineDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.HelpLineDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   HelpLineId  = data.HelpLineId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(HelpLineDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new HelpLineDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
