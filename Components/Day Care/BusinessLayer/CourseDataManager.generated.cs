using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;

namespace DayCare.Components.BusinessLayer
{
	public partial class CourseDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CourseDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Course");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CourseDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CourseDataModel.DataColumns.CourseId:
					if (data.CourseId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CourseDataModel.DataColumns.CourseId, data.CourseId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseDataModel.DataColumns.CourseId);
					}
					break;

				case CourseDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CourseDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseDataModel.DataColumns.Name);
					}
					break;

				case CourseDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CourseDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseDataModel.DataColumns.Description);
					}
					break;

				case CourseDataModel.DataColumns.Duration:
					if (data.Duration != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CourseDataModel.DataColumns.Duration, data.Duration);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseDataModel.DataColumns.Duration);
					}
					break;

				case CourseDataModel.DataColumns.Fees:
					if (data.Fees != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CourseDataModel.DataColumns.Fees, data.Fees);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseDataModel.DataColumns.Fees);
					}
					break;

				case CourseDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CourseDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseDataModel.DataColumns.SortOrder);
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

		public static List<CourseDataModel> GetEntityDetails(CourseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CourseSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CourseId           = dataQuery.CourseId
			};

			List<CourseDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CourseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CourseDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CourseDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CourseDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CourseDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CourseInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CourseUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CourseDataModel.DataColumns.CourseId); 
			sql = sql + ", " + ToSQLParameter(data, CourseDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CourseDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, CourseDataModel.DataColumns.Duration); 
			sql = sql + ", " + ToSQLParameter(data, CourseDataModel.DataColumns.Fees); 
			sql = sql + ", " + ToSQLParameter(data, CourseDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CourseDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Course.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CourseDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Course.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CourseDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CourseDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CourseId  = data.CourseId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CourseDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CourseDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
