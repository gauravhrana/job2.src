using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
    public partial class TargetDetailsDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static TargetDetailsDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TargetDetails");
        }

        #region GetDetails

        public static TargetDetailsDataModel GetDetails(TargetDetailsDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<TargetDetailsDataModel> GetEntityDetails(TargetDetailsDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TargetDetailsSearch ";

            var parameters =
            new
            {
                    AuditId = requestProfile.AuditId 
   				,	ApplicationId = requestProfile.ApplicationId
				,	PersonId = dataQuery.PersonId
				,	EffectiveDate = dataQuery.EffectiveDate
                ,   ScheduleDetailActivityCategoryId = dataQuery.ScheduleDetailActivityCategoryId
                ,   TargetDetailsId = dataQuery.TargetDetailsId
				,   TargetValue = dataQuery.TargetValue
				
            };

            List<TargetDetailsDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<TargetDetailsDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion
        #region Create

        public static int Create(TargetDetailsDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            var id = DBDML.RunScalarSQL("TargetDetails.Insert", sql, DataStoreKey);
            return Convert.ToInt32(id);
        }

        #endregion

        #region Update

        public static void Update(TargetDetailsDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("TargetDetails.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(TargetDetailsDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TargetDetailsDelete ";

            var parameters =
            new
            {
                    AuditId = requestProfile.AuditId
                ,   TargetDetailsId = dataQuery.TargetDetailsId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

            }
        }

        #endregion

        #region Search

        public static string ToSQLParameter(TargetDetailsDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case TargetDetailsDataModel.DataColumns.TargetDetailsId:
                    if (data.TargetDetailsId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TargetDetailsDataModel.DataColumns.TargetDetailsId, data.TargetDetailsId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.TargetDetailsId);

                    }
                    break;

                case TargetDetailsDataModel.DataColumns.ScheduleDetailActivityCategoryId:
                    if (data.ScheduleDetailActivityCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TargetDetailsDataModel.DataColumns.ScheduleDetailActivityCategoryId, data.ScheduleDetailActivityCategoryId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.ScheduleDetailActivityCategoryId);

                    }
                    break;

                case TargetDetailsDataModel.DataColumns.PersonId:
                    if (data.PersonId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TargetDetailsDataModel.DataColumns.PersonId, data.PersonId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.PersonId);

                    }
                    break;

                case TargetDetailsDataModel.DataColumns.EffectiveDate:
                    if (data.EffectiveDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TargetDetailsDataModel.DataColumns.EffectiveDate, data.EffectiveDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.EffectiveDate);

                    }
                    break;

                case TargetDetailsDataModel.DataColumns.TargetValue:
                    if (data.TargetValue != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TargetDetailsDataModel.DataColumns.TargetValue, data.TargetValue);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.TargetValue);
                    }
                    break;

                case TargetDetailsDataModel.DataColumns.CreatedDate:
                    if (data.CreatedDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TargetDetailsDataModel.DataColumns.CreatedDate, data.CreatedDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.CreatedDate);

                    }
                    break;



				case TargetDetailsDataModel.DataColumns.UpdatedDate:
					if (data.UpdatedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TargetDetailsDataModel.DataColumns.UpdatedDate, data.UpdatedDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.UpdatedDate);

					}
					break;

				case TargetDetailsDataModel.DataColumns.ModifiedDate:
					if (data.ModifiedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TargetDetailsDataModel.DataColumns.ModifiedDate, data.ModifiedDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.ModifiedDate);

					}
					break;

				case TargetDetailsDataModel.DataColumns.CreatedByAuditId:
					if (data.CreatedByAuditId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TargetDetailsDataModel.DataColumns.CreatedByAuditId, data.CreatedByAuditId);
						//returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TargetDetailsDataModel.DataColumns.AuditId, data.CreatedByAuditId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.CreatedByAuditId);
						//returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.AuditId);
					}
					break;

				case TargetDetailsDataModel.DataColumns.ModifiedByAuditId:
					if (data.ModifiedByAuditId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TargetDetailsDataModel.DataColumns.ModifiedByAuditId, data.ModifiedByAuditId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TargetDetailsDataModel.DataColumns.ModifiedByAuditId);

					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
            return returnValue;
        }
        public static DataTable Search(TargetDetailsDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }
		
		#endregion
       
		#region DoesExist

		public static bool DoesExist(TargetDetailsDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TargetDetailsDataModel();
			doesExistRequest.PersonId = data.PersonId;
			doesExistRequest.EffectiveDate = data.EffectiveDate;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
			return list.Count > 0;
		}

		#endregion

		#region Save

        private static string Save(TargetDetailsDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
					sql += "dbo.TargetDetailsInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.CreatedDate, data.CreatedDate);
                    
                    break;

                case "Update":
					sql += "dbo.TargetDetailsUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					//	", " + ToSQLParameter(BaseDataModel.BaseDataColumns.CreatedByAuditId, requestProfile.AuditId) +
						//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ModifiedByAuditId, requestProfile.AuditId);
						//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.CreatedDate, data.CreatedDate);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.TargetDetailsId) +
				", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.PersonId) +
				", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.ScheduleDetailActivityCategoryId) +
				", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.EffectiveDate) +
				", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.TargetValue) +
				", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.CreatedDate) +
				", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.UpdatedDate) +
				", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.ModifiedDate);
				//", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.CreatedByAuditId) +
				//", " + ToSQLParameter(data, TargetDetailsDataModel.DataColumns.ModifiedByAuditId);
           
            return sql;
        }

        #endregion
    }
}



