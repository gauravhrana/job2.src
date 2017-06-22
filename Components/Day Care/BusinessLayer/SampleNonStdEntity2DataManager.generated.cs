using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;

namespace DayCare.Components.BusinessLayer
{
	public partial class SampleNonStdEntity2DataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SampleNonStdEntity2DataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SampleNonStdEntity2");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SampleNonStdEntity2DataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SampleNonStdEntity2DataModel.DataColumns.SampleNonStdEntity2Id:
					if (data.SampleNonStdEntity2Id != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntity2DataModel.DataColumns.SampleNonStdEntity2Id, data.SampleNonStdEntity2Id);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.SampleNonStdEntity2Id);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntity2DataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.Name);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.SortOrder:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntity2DataModel.DataColumns.SortOrder, data.SortOrder);
					break;

				case SampleNonStdEntity2DataModel.DataColumns.BathRoomId:
					if (data.BathRoomId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntity2DataModel.DataColumns.BathRoomId, data.BathRoomId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.BathRoomId);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.BathRoom:
					if (!string.IsNullOrEmpty(data.BathRoom))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntity2DataModel.DataColumns.BathRoom, data.BathRoom);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.BathRoom);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.CommentId:
					if (data.CommentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntity2DataModel.DataColumns.CommentId, data.CommentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.CommentId);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.Comment:
					if (!string.IsNullOrEmpty(data.Comment))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntity2DataModel.DataColumns.Comment, data.Comment);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.Comment);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.FieldConfigurationModeId:
					if (data.FieldConfigurationModeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntity2DataModel.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.FieldConfigurationModeId);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.FieldConfigurationMode:
					if (!string.IsNullOrEmpty(data.FieldConfigurationMode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntity2DataModel.DataColumns.FieldConfigurationMode, data.FieldConfigurationMode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.FieldConfigurationMode);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.RecordDate:
					if (data.RecordDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntity2DataModel.DataColumns.RecordDate, data.RecordDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.RecordDate);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.FromRecordDate:
					if (data.FromRecordDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntity2DataModel.DataColumns.FromRecordDate, data.FromRecordDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.FromRecordDate);
					}
					break;

				case SampleNonStdEntity2DataModel.DataColumns.ToRecordDate:
					if (data.ToRecordDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntity2DataModel.DataColumns.ToRecordDate, data.ToRecordDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntity2DataModel.DataColumns.ToRecordDate);
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

		public static List<SampleNonStdEntity2DataModel> GetEntityDetails(SampleNonStdEntity2DataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SampleNonStdEntity2Search ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SampleNonStdEntity2Id           = dataQuery.SampleNonStdEntity2Id
				 ,	Name           = dataQuery.Name
				 ,	BathRoomId           = dataQuery.BathRoomId
				 ,	CommentId           = dataQuery.CommentId
				 ,	FieldConfigurationModeId           = dataQuery.FieldConfigurationModeId
				 ,	FromRecordDate           = dataQuery.FromRecordDate
				 ,	ToRecordDate           = dataQuery.ToRecordDate
			};

			List<SampleNonStdEntity2DataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SampleNonStdEntity2DataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SampleNonStdEntity2DataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SampleNonStdEntity2DataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SampleNonStdEntity2DataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(SampleNonStdEntity2DataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.SampleNonStdEntity2Insert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SampleNonStdEntity2Update  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SampleNonStdEntity2DataModel.DataColumns.SampleNonStdEntity2Id); 
			sql = sql + ", " + ToSQLParameter(data, SampleNonStdEntity2DataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, SampleNonStdEntity2DataModel.DataColumns.SortOrder); 
			sql = sql + ", " + ToSQLParameter(data, SampleNonStdEntity2DataModel.DataColumns.BathRoomId); 
			sql = sql + ", " + ToSQLParameter(data, SampleNonStdEntity2DataModel.DataColumns.CommentId); 
			sql = sql + ", " + ToSQLParameter(data, SampleNonStdEntity2DataModel.DataColumns.FieldConfigurationModeId); 
			sql = sql + ", " + ToSQLParameter(data, SampleNonStdEntity2DataModel.DataColumns.RecordDate); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SampleNonStdEntity2DataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SampleNonStdEntity2.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SampleNonStdEntity2DataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SampleNonStdEntity2.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SampleNonStdEntity2DataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SampleNonStdEntity2Delete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SampleNonStdEntity2Id  = data.SampleNonStdEntity2Id
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SampleNonStdEntity2DataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SampleNonStdEntity2DataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
