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
	public partial class SubjectMatterDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SubjectMatterDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SubjectMatter");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SubjectMatterDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SubjectMatterDataModel.DataColumns.SubjectMatterId:
					if (data.SubjectMatterId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SubjectMatterDataModel.DataColumns.SubjectMatterId, data.SubjectMatterId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubjectMatterDataModel.DataColumns.SubjectMatterId);
					}
					break;

				case SubjectMatterDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SubjectMatterDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubjectMatterDataModel.DataColumns.Name);
					}
					break;

				case SubjectMatterDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SubjectMatterDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubjectMatterDataModel.DataColumns.Description);
					}
					break;

				case SubjectMatterDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SubjectMatterDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubjectMatterDataModel.DataColumns.SortOrder);
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

		public static List<SubjectMatterDataModel> GetEntityDetails(SubjectMatterDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SubjectMatterSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SubjectMatterId           = dataQuery.SubjectMatterId
				 ,	Name           = dataQuery.Name
			};

			List<SubjectMatterDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SubjectMatterDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SubjectMatterDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SubjectMatterDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SubjectMatterDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SubjectMatterDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SubjectMatterInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SubjectMatterUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SubjectMatterDataModel.DataColumns.SubjectMatterId); 
			sql = sql + ", " + ToSQLParameter(data, SubjectMatterDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, SubjectMatterDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, SubjectMatterDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SubjectMatterDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SubjectMatter.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SubjectMatterDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SubjectMatter.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SubjectMatterDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SubjectMatterDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SubjectMatterId  = data.SubjectMatterId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SubjectMatterDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SubjectMatterDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
