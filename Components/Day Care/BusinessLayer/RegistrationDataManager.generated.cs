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
	public partial class RegistrationDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static RegistrationDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Registration");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(RegistrationDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case RegistrationDataModel.DataColumns.RegistrationId:
					if (data.RegistrationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RegistrationDataModel.DataColumns.RegistrationId, data.RegistrationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RegistrationDataModel.DataColumns.RegistrationId);
					}
					break;

				case RegistrationDataModel.DataColumns.CourseId:
					if (data.CourseId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RegistrationDataModel.DataColumns.CourseId, data.CourseId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RegistrationDataModel.DataColumns.CourseId);
					}
					break;

				case RegistrationDataModel.DataColumns.Course:
					if (!string.IsNullOrEmpty(data.Course))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RegistrationDataModel.DataColumns.Course, data.Course);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RegistrationDataModel.DataColumns.Course);
					}
					break;

				case RegistrationDataModel.DataColumns.StudentId:
					if (data.StudentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RegistrationDataModel.DataColumns.StudentId, data.StudentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RegistrationDataModel.DataColumns.StudentId);
					}
					break;

				case RegistrationDataModel.DataColumns.Student:
					if (!string.IsNullOrEmpty(data.Student))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RegistrationDataModel.DataColumns.Student, data.Student);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RegistrationDataModel.DataColumns.Student);
					}
					break;

				case RegistrationDataModel.DataColumns.EnrollmentDate:
					if (data.EnrollmentDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RegistrationDataModel.DataColumns.EnrollmentDate, data.EnrollmentDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RegistrationDataModel.DataColumns.EnrollmentDate);
					}
					break;

				case RegistrationDataModel.DataColumns.FromSearchEnrollmentDate:
					if (data.FromSearchEnrollmentDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RegistrationDataModel.DataColumns.FromSearchEnrollmentDate, data.FromSearchEnrollmentDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RegistrationDataModel.DataColumns.FromSearchEnrollmentDate);
					}
					break;

				case RegistrationDataModel.DataColumns.ToSearchEnrollmentDate:
					if (data.ToSearchEnrollmentDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RegistrationDataModel.DataColumns.ToSearchEnrollmentDate, data.ToSearchEnrollmentDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RegistrationDataModel.DataColumns.ToSearchEnrollmentDate);
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

		public static List<RegistrationDataModel> GetEntityDetails(RegistrationDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.RegistrationSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	RegistrationId           = dataQuery.RegistrationId
				 ,	CourseId           = dataQuery.CourseId
				 ,	StudentId           = dataQuery.StudentId
				 ,	FromSearchEnrollmentDate           = dataQuery.FromSearchEnrollmentDate
				 ,	ToSearchEnrollmentDate           = dataQuery.ToSearchEnrollmentDate
			};

			List<RegistrationDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<RegistrationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<RegistrationDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(RegistrationDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(RegistrationDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(RegistrationDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.RegistrationInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.RegistrationUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, RegistrationDataModel.DataColumns.RegistrationId); 
			sql = sql + ", " + ToSQLParameter(data, RegistrationDataModel.DataColumns.CourseId); 
			sql = sql + ", " + ToSQLParameter(data, RegistrationDataModel.DataColumns.StudentId); 
			sql = sql + ", " + ToSQLParameter(data, RegistrationDataModel.DataColumns.EnrollmentDate); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(RegistrationDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Registration.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(RegistrationDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Registration.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(RegistrationDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.RegistrationDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   RegistrationId  = data.RegistrationId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(RegistrationDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new RegistrationDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.CourseId  = data.CourseId;
			doesExistRequest.StudentId  = data.StudentId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
