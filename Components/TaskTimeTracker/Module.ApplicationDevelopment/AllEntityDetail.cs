using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class AllEntityDetailDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";
        
		static AllEntityDetailDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("AllEntityDetail");
        }

		#region SaveEntityDetails

		public static void SaveEntityDetails(String entityName, String dbName, String dbProjectName, String dbComponentName, RequestProfile requestProfile)
		{
			var dbNameId			= 0;
			var dbComponentNameId	= 0;
			var dbProjectNameId		= 0;

			var obj1				= new DBNameDataModel();
			obj1.Name				= dbName;
			var dt					= DBNameDataManager.Search(obj1, requestProfile);

			if (dt.Rows.Count >= 1)
			{
				if(dbName.Equals(obj1.Name))
				{
				dbNameId = Convert.ToInt32(dt.Rows[0][DBNameDataModel.DataColumns.DBNameId].ToString());
				}
			}

			var obj2		= new DBComponentNameDataModel();
			obj2.Name		= dbComponentName;
			dt				= DBComponentNameDataManager.Search(obj2, requestProfile);

			if (dt.Rows.Count >= 1)
			{
				if (dbComponentName.Equals(obj2.Name))
				{
					dbComponentNameId = Convert.ToInt32(dt.Rows[0][DBComponentNameDataModel.DataColumns.DBComponentNameId].ToString());
				}
			}

			var obj3		= new DBProjectNameDataModel();
			obj3.Name		= dbProjectName;
			dt				= DBProjectNameDataManager.Search(obj3, requestProfile);

			if (dt.Rows.Count >= 1)
			{
				if(dbProjectName.Equals(obj3.Name))
				{
					dbProjectNameId = Convert.ToInt32(dt.Rows[0][DBProjectNameDataModel.DataColumns.DBProjectNameId].ToString());
				}
			}

			var obj4				= new AllEntityDetailDataModel();
			var AllEntityDetailId	= 0;
			obj4.EntityName			= entityName;
			dt						= AllEntityDetailDataManager.Search(obj4, requestProfile);

			if (dt.Rows.Count >= 1)
			{
				if (entityName.Equals(obj4.EntityName))
				{
					AllEntityDetailId = Convert.ToInt32(dt.Rows[0][AllEntityDetailDataModel.DataColumns.AllEntityDetailId].ToString());
				}
			}

			var obj = new AllEntityDetailDataModel();

			obj.ApplicationId			= requestProfile.ApplicationId;
			obj.EntityName				= entityName;
			obj.DB_Name					= dbName;
			obj.DB_Project_Name			= dbProjectName;
			obj.Component_Project_Name	= dbComponentName;
			obj.DBNameId				= dbNameId;
			obj.DBProjectNameId			= dbProjectNameId;
			obj.DBComponentNameId		= dbComponentNameId;

			if (AllEntityDetailId==0)
			{
				AllEntityDetailDataManager.Create(obj, requestProfile);
			}

			else if (AllEntityDetailId !=0)
			{
				obj.AllEntityDetailId = AllEntityDetailId;
				AllEntityDetailDataManager.Update(obj, requestProfile);

			}

		}

		#endregion 

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.AllEntityDetailSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("AllEntityDetail.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(AllEntityDetailDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion
 
		#region GetEntitySearch

		public static List<AllEntityDetailDataModel> GetEntityDetails(AllEntityDetailDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AllEntityDetailSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= requestProfile.ApplicationId
				,	AllEntityDetailId			= dataQuery.AllEntityDetailId
				,	EntityName					= dataQuery.EntityName
				,	DB_Name						= dataQuery.DB_Name
				,	DB_Project_Name				= dataQuery.DB_Project_Name
				,	Component_Project_Name		= dataQuery.Component_Project_Name
				,	ReturnAuditInfo				= returnAuditInfo
			}; 

			List<AllEntityDetailDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AllEntityDetailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


			return result;
		}

		#endregion

		#region CreateOrUpdate
		private static string CreateOrUpdate(AllEntityDetailDataModel data, RequestProfile requestedProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.AllEntityDetailInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestedProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AllEntityDetailUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
						   ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestedProfile.ApplicationId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, AllEntityDetailDataModel.DataColumns.AllEntityDetailId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, AllEntityDetailDataModel.DataColumns.DBNameId) +
						", " + ToSQLParameter(data, AllEntityDetailDataModel.DataColumns.DBProjectNameId) +
						", " + ToSQLParameter(data, AllEntityDetailDataModel.DataColumns.DBComponentNameId) +
						", " + ToSQLParameter(data, AllEntityDetailDataModel.DataColumns.DBName) +
						", " + ToSQLParameter(data, AllEntityDetailDataModel.DataColumns.DBProjectName) +
						", " + ToSQLParameter(data, AllEntityDetailDataModel.DataColumns.Component_Project_Name) +
						", " + ToSQLParameter(data, AllEntityDetailDataModel.DataColumns.EntityName);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(AllEntityDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("AllEntityDetail.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(AllEntityDetailDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("AllEntityDetail.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AllEntityDetailDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.AllEntityDetailDelete ";

			var parameters =
			new
			{							
					AuditId					= requestProfile.AuditId					
				,	AllEntityDetailId		= dataQuery.AllEntityDetailId				
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(AllEntityDetailDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AllEntityDetailDataModel.DataColumns.AllEntityDetailId:
					if (data.AllEntityDetailId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AllEntityDetailDataModel.DataColumns.AllEntityDetailId, data.AllEntityDetailId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AllEntityDetailDataModel.DataColumns.AllEntityDetailId);
					}
					break;

				case AllEntityDetailDataModel.DataColumns.EntityName:
					if (data.EntityName != null && !string.IsNullOrEmpty(data.EntityName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AllEntityDetailDataModel.DataColumns.EntityName, data.EntityName);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AllEntityDetailDataModel.DataColumns.EntityName);
					}
					break;

				case AllEntityDetailDataModel.DataColumns.DB_Name:
                    if (data.DB_Name != null && !string.IsNullOrEmpty(data.DB_Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AllEntityDetailDataModel.DataColumns.DB_Name, data.DB_Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AllEntityDetailDataModel.DataColumns.DB_Name);
					}
					break;

				case AllEntityDetailDataModel.DataColumns.DB_Project_Name:
                    if (data.DB_Project_Name != null && !string.IsNullOrEmpty(data.DB_Project_Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AllEntityDetailDataModel.DataColumns.DB_Project_Name, data.DB_Project_Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AllEntityDetailDataModel.DataColumns.DB_Project_Name);
					}
					break;

				case AllEntityDetailDataModel.DataColumns.Component_Project_Name:
                    if (data.Component_Project_Name != null && !string.IsNullOrEmpty(data.Component_Project_Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AllEntityDetailDataModel.DataColumns.Component_Project_Name, data.Component_Project_Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AllEntityDetailDataModel.DataColumns.Component_Project_Name);
					}
					break;
				case AllEntityDetailDataModel.DataColumns.DBNameId:
					if (data.DBNameId!=null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AllEntityDetailDataModel.DataColumns.DBNameId, data.DBNameId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AllEntityDetailDataModel.DataColumns.DBNameId);
					}
					break;
				case AllEntityDetailDataModel.DataColumns.DBProjectNameId:
					if (data.DBProjectNameId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AllEntityDetailDataModel.DataColumns.DBProjectNameId, data.DBProjectNameId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AllEntityDetailDataModel.DataColumns.DBProjectNameId);
					}
					break;
				case AllEntityDetailDataModel.DataColumns.DBComponentNameId:
					if (data.DBComponentNameId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AllEntityDetailDataModel.DataColumns.DBComponentNameId, data.DBComponentNameId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AllEntityDetailDataModel.DataColumns.DBComponentNameId);
					}
					break;

				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static DataTable Search(AllEntityDetailDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table; 
		}

		#endregion

		

		#region DoesExist

		public static DataTable DoesExist(AllEntityDetailDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AllEntityDetailDataModel();
			doesExistRequest.EntityName = data.EntityName;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion
	}
}

