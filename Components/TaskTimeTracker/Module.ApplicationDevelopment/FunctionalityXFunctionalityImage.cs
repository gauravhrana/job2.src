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
	public partial class FunctionalityXFunctionalityImageDataManager :BaseDataManager
	{
		static readonly string DataStoreKey = "";
		
		static FunctionalityXFunctionalityImageDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityXFunctionalityImage");
		}

		#region Create By Functionality

		public static void CreateByFunctionality(int functionalityId, int[] functionalityImageIds, string keystring, string title, string description, int sortorder, RequestProfile requestProfile)
		{
			foreach (int FunctionalityImageId in functionalityImageIds)
			{
				var sql = "EXEC FunctionalityXFunctionalityImageInsert " +
						  "@FunctionalityId				=" + functionalityId + ", " +
						  "@FunctionalityImageId		=" + FunctionalityImageId + ", " +
						  "@ApplicationId				=" + requestProfile.ApplicationId + ", " +
                          "@KeyString                   ='" + keystring + "', " +
                          "@Title                       ='" + title + "', " +
                          "@Description                 ='" + description + "', " +
                          "@SortOrder                   =" + sortorder + ", " +
                          "@CreatedBy                   =" + requestProfile.AuditId + ", " +
                          "@UpdatedBy                   =" + requestProfile.AuditId + ", " +
                          "@CreatedDate                 ='" + DateTime.Now.Date + "', " +
                          "@UpdatedDate                 ='" + DateTime.Now.Date + "', " +
						  "@AuditId						=" + requestProfile.AuditId;

				DBDML.RunSQL("FunctionalityXFunctionalityImageInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Create By FunctionalityImage

        public static void CreateByFunctionalityImage(int functionalityImageId, int[] functionalityIds, string keystring, string title, string description, int sortorder, RequestProfile requestProfile)
		{
			foreach (int FunctionalityId in functionalityIds)
			{
				var sql = "EXEC FunctionalityXFunctionalityImageInsert " +
						  "@FunctionalityId				    =" + FunctionalityId + ", " +
						  "@FunctionalityImageId			=" + functionalityImageId + ", " +
						  "@ApplicationId				    =" + requestProfile.ApplicationId + ", " +
                          "@KeyString                       ='" + keystring + "', " +
                          "@Title                           ='" + title + "', " +
                          "@Description                     ='" + description + "', " +
                          "@SortOrder                       =" + sortorder + ", " +
                          "@CreatedBy                       =" + requestProfile.AuditId + ", " +
                          "@UpdatedBy                       =" + requestProfile.AuditId + ", " +
                          "@CreatedDate                     ='" + DateTime.Now.Date + "', " +
                          "@UpdatedDate                     ='" + DateTime.Now.Date + "', " +
						  "@AuditId							=" + requestProfile.AuditId;
				DBDML.RunSQL("FunctionalityXFunctionalityImageInsert", sql, DataStoreKey);
			}
		}

		#endregion

		#region Get By FunctionalityImage

		public static DataTable GetByFunctionalityImage(int functionalityImageId, RequestProfile requestProfile)
		{
			var sql = "EXEC FunctionalityXFunctionalityImageSearch " +
					  "@FunctionalityImageId     =" + functionalityImageId + ", " +
				      "@AuditId					 =" + requestProfile.AuditId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Get By Functionality

		public static DataTable GetByFunctionality(int functionalityId, RequestProfile requestProfile)
		{
			var sql = "EXEC FunctionalityXFunctionalityImageSearch " +
					  "@FunctionalityId       =" + functionalityId + ", " +
					  "@AuditId				  =" + requestProfile.AuditId;
			var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Delete By FunctionalityImage

		public static void DeleteByFunctionalityImage(int functionalityImageId, RequestProfile requestProfile)
		{
			var sql = "EXEC FunctionalityXFunctionalityImageDelete " +
					  "@FunctionalityImageId       =" + functionalityImageId + ", " +
					  "@AuditId					   =" + requestProfile.AuditId;
			DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion

		#region Delete By Functionality

		public static void DeleteByFunctionality(int functionalityId, RequestProfile requestProfile)
		{
			var sql = "EXEC FunctionalityXFunctionalityImageDelete " + 
				      "@FunctionalityId  =" + functionalityId + ", " +
					  "@AuditId			 =" + requestProfile.AuditId;
			DBDML.RunSQL("Delete Details", sql, DataStoreKey);
		}

		#endregion


        public static List<FunctionalityXFunctionalityImageDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityXFunctionalityImageDataModel.Empty, requestProfile, 0);
        }



        #region GetDetails

        public static FunctionalityXFunctionalityImageDataModel GetDetails(FunctionalityXFunctionalityImageDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

		public static List<FunctionalityXFunctionalityImageDataModel> GetEntityDetails(FunctionalityXFunctionalityImageDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
			const string sql = @"dbo.FunctionalityXFunctionalityImageSearch ";

			var parameters =
			new
			{
					AuditId								= requestProfile.AuditId
				,	ApplicationId						= requestProfile.ApplicationId
				,	ApplicationMode						= requestProfile.ApplicationModeId
				,	FunctionalityXFunctionalityImageId	= dataQuery.FunctionalityXFunctionalityImageId
				,	FunctionalityId						= dataQuery.FunctionalityId
				,	FunctionalityImageId				= dataQuery.FunctionalityImageId				
				,	ReturnAuditInfo						= returnAuditInfo
				
			};

			List<FunctionalityXFunctionalityImageDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityXFunctionalityImageDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

            return result;
        }

        #endregion

        #region Create

        public static void Create(FunctionalityXFunctionalityImageDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            DBDML.RunSQL("FunctionalityXFunctionalityImage.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(FunctionalityXFunctionalityImageDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("FunctionalityXFunctionalityImage.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(FunctionalityXFunctionalityImageDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FunctionalityXFunctionalityImageDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityXFunctionalityImageId, data.FunctionalityXFunctionalityImageId);

            DBDML.RunSQL("FunctionalityXFunctionalityImage.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(FunctionalityXFunctionalityImageDataModel data, string dataColumnName, object value)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityXFunctionalityImageId:
                    if (data.FunctionalityXFunctionalityImageId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityXFunctionalityImageId, data.FunctionalityXFunctionalityImageId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityXFunctionalityImageId);
                    }
                    break;

                case FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityId:
                    if (data.FunctionalityId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityId, data.FunctionalityId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityId);
                    }
                    break;

                case FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityImageId:
                    if (data.FunctionalityImageId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityImageId, data.FunctionalityImageId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityImageId);
                    }
                    break;

                case FunctionalityXFunctionalityImageDataModel.DataColumns.KeyString:
                    if (data.KeyString != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityImageDataModel.DataColumns.KeyString, data.KeyString);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityImageDataModel.DataColumns.KeyString);
                    }
                    break;

                case FunctionalityXFunctionalityImageDataModel.DataColumns.Title:
                    if (data.Title != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityXFunctionalityImageDataModel.DataColumns.Title, data.Title);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityImageDataModel.DataColumns.Title);
                    }
                    break;                                

                case FunctionalityXFunctionalityImageDataModel.DataColumns.CreatedBy:
                    if (data.CreatedBy != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityXFunctionalityImageDataModel.DataColumns.CreatedBy, data.CreatedBy);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityImageDataModel.DataColumns.CreatedBy);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(FunctionalityXFunctionalityImageDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
           
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(FunctionalityXFunctionalityImageDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.FunctionalityXFunctionalityImageInsert  ";
                    break;

                case "Update":
                    sql += "dbo.FunctionalityXFunctionalityImageUpdate  ";
                    break;

                default:
                    break;

            }

            sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(data, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityXFunctionalityImageId, data.FunctionalityXFunctionalityImageId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                        ", " + ToSQLParameter(data, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityId, data.FunctionalityId) +
                        ", " + ToSQLParameter(data, FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityImageId, data.FunctionalityImageId) +
                        ", " + ToSQLParameter(data, FunctionalityXFunctionalityImageDataModel.DataColumns.KeyString, data.KeyString) +
                        ", " + ToSQLParameter(data, FunctionalityXFunctionalityImageDataModel.DataColumns.Title, data.Title) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description, data.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder, data.SortOrder) +
                        ", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.CreatedDate, data.CreatedDate) +
                        ", " + ToSQLParameter(data, FunctionalityXFunctionalityImageDataModel.DataColumns.CreatedBy, data.CreatedBy) +
						 ", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.UpdatedDate, data.UpdatedDate) +
						", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.UpdatedBy, data.UpdatedBy);
            return sql;
        }

        #endregion

	}
}
