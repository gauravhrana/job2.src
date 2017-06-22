using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class ClassDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ClassDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Class");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ClassDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ClassDataModel.DataColumns.ClassId:
					if (data.ClassId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClassDataModel.DataColumns.ClassId, data.ClassId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassDataModel.DataColumns.ClassId);
					}
					break;

				case ClassDataModel.DataColumns.FundId:
					if (data.FundId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClassDataModel.DataColumns.FundId, data.FundId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassDataModel.DataColumns.FundId);
					}
					break;

				case ClassDataModel.DataColumns.Fund:
					if (!string.IsNullOrEmpty(data.Fund))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassDataModel.DataColumns.Fund, data.Fund);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassDataModel.DataColumns.Fund);
					}
					break;

				case ClassDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassDataModel.DataColumns.Name);
					}
					break;

				case ClassDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassDataModel.DataColumns.Description);
					}
					break;

				case ClassDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClassDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassDataModel.DataColumns.SortOrder);
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

		public static List<ClassDataModel> GetEntityDetails(ClassDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ClassSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ClassId           = dataQuery.ClassId
				 ,	FundId           = dataQuery.FundId
				 ,	Name           = dataQuery.Name
			};

			List<ClassDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ClassDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ClassDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ClassDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ClassDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static ClassDataModel GetDetails(ClassDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save


		public static string Save(ClassDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.ClassInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ClassUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ClassDataModel.DataColumns.ClassId); 
			sql = sql + ", " + ToSQLParameter(data, ClassDataModel.DataColumns.FundId); 
			sql = sql + ", " + ToSQLParameter(data, ClassDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ClassDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, ClassDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ClassDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Class.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ClassDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Class.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ClassDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ClassDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ClassId  = data.ClassId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ClassDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ClassDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.FundId  = data.FundId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
