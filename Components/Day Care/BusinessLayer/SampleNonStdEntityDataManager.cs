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
    public class SampleNonStdEntityDataManager : BaseDataManager
    {
        
        private static readonly string DataStoreKey = string.Empty;

		static SampleNonStdEntityDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SampleNonStdEntity");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SampleNonStdEntityDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SampleNonStdEntityDataModel.DataColumns.SampleNonStdEntityId:
					if (data.SampleNonStdEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntityDataModel.DataColumns.SampleNonStdEntityId, data.SampleNonStdEntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntityDataModel.DataColumns.SampleNonStdEntityId);
					}
                    break;

                case SampleNonStdEntityDataModel.DataColumns.Name:
                    if (!string.IsNullOrEmpty(data.Name))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntityDataModel.DataColumns.Name, data.Name);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntityDataModel.DataColumns.Name);
                    }
                    break;

                case SampleNonStdEntityDataModel.DataColumns.SortOrder:
                    if (data.SortOrder != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntityDataModel.DataColumns.SortOrder, data.SortOrder);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntityDataModel.DataColumns.SortOrder);
                    }
                    break;

                case SampleNonStdEntityDataModel.DataColumns.BathRoomId:
                    if (data.BathRoomId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntityDataModel.DataColumns.BathRoomId, data.BathRoomId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntityDataModel.DataColumns.BathRoomId);
                    }
                    break;

                case SampleNonStdEntityDataModel.DataColumns.BathRoom:
                    if (!string.IsNullOrEmpty(data.BathRoom))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntityDataModel.DataColumns.BathRoom, data.BathRoom);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntityDataModel.DataColumns.BathRoom);
                    }
                    break;

                case SampleNonStdEntityDataModel.DataColumns.CommentId:
                    if (data.CommentId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SampleNonStdEntityDataModel.DataColumns.CommentId, data.CommentId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntityDataModel.DataColumns.CommentId);
                    }
                    break;

                case SampleNonStdEntityDataModel.DataColumns.Comment:
                    if (!string.IsNullOrEmpty(data.Comment))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SampleNonStdEntityDataModel.DataColumns.Comment, data.Comment);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SampleNonStdEntityDataModel.DataColumns.Comment);
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

		public static List<SampleNonStdEntityDataModel> GetEntityDetails(SampleNonStdEntityDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SampleNonStdEntitySearch ";

			var parameters =
			new
			{
				    AuditId              = requestProfile.AuditId
				,   ApplicationId        = requestProfile.ApplicationId
				,   ReturnAuditInfo      = returnAuditInfo
				,   SampleNonStdEntityId = dataQuery.SampleNonStdEntityId
				,   Name                 = dataQuery.Name
                ,   BathRoomId           = dataQuery.BathRoomId
                ,   CommentId            = dataQuery.CommentId
			};

			List<SampleNonStdEntityDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SampleNonStdEntityDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

        public static List<SampleNonStdEntityDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(SampleNonStdEntityDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SampleNonStdEntityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

        public static SampleNonStdEntityDataModel GetDetails(SampleNonStdEntityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(SampleNonStdEntityDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SampleNonStdEntityInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SampleNonStdEntityUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SampleNonStdEntityDataModel.DataColumns.SampleNonStdEntityId) +
                ", " + ToSQLParameter(data, SampleNonStdEntityDataModel.DataColumns.Name) +
                ", " + ToSQLParameter(data, SampleNonStdEntityDataModel.DataColumns.BathRoomId) +
                ", " + ToSQLParameter(data, SampleNonStdEntityDataModel.DataColumns.CommentId) +
                ", " + ToSQLParameter(data, SampleNonStdEntityDataModel.DataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SampleNonStdEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SampleNonStdEntity.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SampleNonStdEntityDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SampleNonStdEntity.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SampleNonStdEntityDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SampleNonStdEntityDelete ";

			var parameters =
			new
			{
				    AuditId              = requestProfile.AuditId
				,   SampleNonStdEntityId = data.SampleNonStdEntityId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

	}
}
