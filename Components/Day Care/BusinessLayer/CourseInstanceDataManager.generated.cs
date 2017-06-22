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
	public partial class CourseInstanceDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static CourseInstanceDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CourseInstance");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(CourseInstanceDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CourseInstanceDataModel.DataColumns.CourseInstanceId:
					if (data.CourseInstanceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CourseInstanceDataModel.DataColumns.CourseInstanceId, data.CourseInstanceId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.CourseInstanceId);
					}
					break;

				case CourseInstanceDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CourseInstanceDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.Name);
					}
					break;

				case CourseInstanceDataModel.DataColumns.CourseId:
					if (data.CourseId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CourseInstanceDataModel.DataColumns.CourseId, data.CourseId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.CourseId);
					}
					break;

				case CourseInstanceDataModel.DataColumns.Course:
					if (!string.IsNullOrEmpty(data.Course))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CourseInstanceDataModel.DataColumns.Course, data.Course);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.Course);
					}
					break;

				case CourseInstanceDataModel.DataColumns.DepartmentId:
					if (data.DepartmentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CourseInstanceDataModel.DataColumns.DepartmentId, data.DepartmentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.DepartmentId);
					}
					break;

				case CourseInstanceDataModel.DataColumns.Department:
					if (!string.IsNullOrEmpty(data.Department))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CourseInstanceDataModel.DataColumns.Department, data.Department);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.Department);
					}
					break;

				case CourseInstanceDataModel.DataColumns.TeacherId:
					if (data.TeacherId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CourseInstanceDataModel.DataColumns.TeacherId, data.TeacherId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.TeacherId);
					}
					break;

				case CourseInstanceDataModel.DataColumns.Teacher:
					if (!string.IsNullOrEmpty(data.Teacher))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CourseInstanceDataModel.DataColumns.Teacher, data.Teacher);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.Teacher);
					}
					break;

				case CourseInstanceDataModel.DataColumns.StartTime:
					if (data.StartTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CourseInstanceDataModel.DataColumns.StartTime, data.StartTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.StartTime);
					}
					break;

				case CourseInstanceDataModel.DataColumns.EndTime:
					if (data.EndTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CourseInstanceDataModel.DataColumns.EndTime, data.EndTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CourseInstanceDataModel.DataColumns.EndTime);
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

		public static List<CourseInstanceDataModel> GetEntityDetails(CourseInstanceDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CourseInstanceSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	CourseInstanceId           = dataQuery.CourseInstanceId
				 ,	Name           = dataQuery.Name
				 ,	CourseId           = dataQuery.CourseId
				 ,	DepartmentId           = dataQuery.DepartmentId
				 ,	TeacherId           = dataQuery.TeacherId
			};

			List<CourseInstanceDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CourseInstanceDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<CourseInstanceDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(CourseInstanceDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(CourseInstanceDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(CourseInstanceDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.CourseInstanceInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CourseInstanceUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, CourseInstanceDataModel.DataColumns.CourseInstanceId); 
			sql = sql + ", " + ToSQLParameter(data, CourseInstanceDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, CourseInstanceDataModel.DataColumns.CourseId); 
			sql = sql + ", " + ToSQLParameter(data, CourseInstanceDataModel.DataColumns.DepartmentId); 
			sql = sql + ", " + ToSQLParameter(data, CourseInstanceDataModel.DataColumns.TeacherId); 
			sql = sql + ", " + ToSQLParameter(data, CourseInstanceDataModel.DataColumns.StartTime); 
			sql = sql + ", " + ToSQLParameter(data, CourseInstanceDataModel.DataColumns.EndTime); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(CourseInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("CourseInstance.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(CourseInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("CourseInstance.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CourseInstanceDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.CourseInstanceDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   CourseInstanceId  = data.CourseInstanceId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(CourseInstanceDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CourseInstanceDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.CourseId  = data.CourseId;
			doesExistRequest.DepartmentId  = data.DepartmentId;
			doesExistRequest.TeacherId  = data.TeacherId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
