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
	public partial class ClassInstanceDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ClassInstanceDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ClassInstance");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ClassInstanceDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ClassInstanceDataModel.DataColumns.ClassInstanceId:
					if (data.ClassInstanceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClassInstanceDataModel.DataColumns.ClassInstanceId, data.ClassInstanceId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.ClassInstanceId);
					}
					break;

				case ClassInstanceDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassInstanceDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.Name);
					}
					break;

				case ClassInstanceDataModel.DataColumns.CourseId:
					if (data.CourseId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClassInstanceDataModel.DataColumns.CourseId, data.CourseId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.CourseId);
					}
					break;

				case ClassInstanceDataModel.DataColumns.Course:
					if (!string.IsNullOrEmpty(data.Course))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassInstanceDataModel.DataColumns.Course, data.Course);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.Course);
					}
					break;

				case ClassInstanceDataModel.DataColumns.DepartmentId:
					if (data.DepartmentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClassInstanceDataModel.DataColumns.DepartmentId, data.DepartmentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.DepartmentId);
					}
					break;

				case ClassInstanceDataModel.DataColumns.Department:
					if (!string.IsNullOrEmpty(data.Department))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassInstanceDataModel.DataColumns.Department, data.Department);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.Department);
					}
					break;

				case ClassInstanceDataModel.DataColumns.TeacherId:
					if (data.TeacherId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClassInstanceDataModel.DataColumns.TeacherId, data.TeacherId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.TeacherId);
					}
					break;

				case ClassInstanceDataModel.DataColumns.Teacher:
					if (!string.IsNullOrEmpty(data.Teacher))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassInstanceDataModel.DataColumns.Teacher, data.Teacher);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.Teacher);
					}
					break;

				case ClassInstanceDataModel.DataColumns.StartTime:
					if (data.StartTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassInstanceDataModel.DataColumns.StartTime, data.StartTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.StartTime);
					}
					break;

				case ClassInstanceDataModel.DataColumns.EndTime:
					if (data.EndTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClassInstanceDataModel.DataColumns.EndTime, data.EndTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClassInstanceDataModel.DataColumns.EndTime);
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

		public static List<ClassInstanceDataModel> GetEntityDetails(ClassInstanceDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ClassInstanceSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ClassInstanceId           = dataQuery.ClassInstanceId
				 ,	Name           = dataQuery.Name
				 ,	CourseId           = dataQuery.CourseId
				 ,	DepartmentId           = dataQuery.DepartmentId
				 ,	TeacherId           = dataQuery.TeacherId
			};

			List<ClassInstanceDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ClassInstanceDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ClassInstanceDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ClassInstanceDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ClassInstanceDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(ClassInstanceDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.ClassInstanceInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ClassInstanceUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ClassInstanceDataModel.DataColumns.ClassInstanceId); 
			sql = sql + ", " + ToSQLParameter(data, ClassInstanceDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ClassInstanceDataModel.DataColumns.CourseId); 
			sql = sql + ", " + ToSQLParameter(data, ClassInstanceDataModel.DataColumns.DepartmentId); 
			sql = sql + ", " + ToSQLParameter(data, ClassInstanceDataModel.DataColumns.TeacherId); 
			sql = sql + ", " + ToSQLParameter(data, ClassInstanceDataModel.DataColumns.StartTime); 
			sql = sql + ", " + ToSQLParameter(data, ClassInstanceDataModel.DataColumns.EndTime); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ClassInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ClassInstance.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ClassInstanceDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ClassInstance.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ClassInstanceDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ClassInstanceDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ClassInstanceId  = data.ClassInstanceId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ClassInstanceDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ClassInstanceDataModel();
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
